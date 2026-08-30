import React, { createContext, useContext, useEffect, useMemo, useState, useCallback } from "react";
import { tokenStore, decodeJwtPayload } from "../lib/tokenStore";
import { registerAuthFailureHandler, registerRefreshFn } from "../lib/httpClient";
import * as authApi from "../lib/authApi";

export const AuthStage = {
    UNAUTHENTICATED: "unauthenticated",
    AUTHENTICATED_NO_WORKSPACE: "authenticated_no_workspace",
    WORKSPACE_SCOPED: "workspace_scoped",
};

// Order matters — used for minStage comparisons in ProtectedRoute
const STAGE_ORDER = [
    AuthStage.UNAUTHENTICATED,
    AuthStage.AUTHENTICATED_NO_WORKSPACE,
    AuthStage.WORKSPACE_SCOPED,
];

export function stageAtLeast(current, required) {
    return STAGE_ORDER.indexOf(current) >= STAGE_ORDER.indexOf(required);
}

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
    const [stage, setStage] = useState(AuthStage.UNAUTHENTICATED);
    const [user, setUser] = useState(null); // { userId }
    const [organization, setOrganization] = useState(null); // { organizationId } once selected
    const [booting, setBooting] = useState(true); // true while we attempt silent refresh on load

    const applyTokens = useCallback((accessToken, refreshToken, nextStage) => {
        tokenStore.setAccessToken(accessToken);
        tokenStore.setRefreshToken(refreshToken);

        const payload = decodeJwtPayload(accessToken); // display-only, see AUTH_FLOW.md §7
        setUser(payload?.userId ? { userId: payload.userId } : null);
        setOrganization(payload?.organizationId ? { organizationId: payload.organizationId } : null);
        setStage(nextStage);
    }, []);

    const login = useCallback(
        async (credentials) => {
            const { accessToken, refreshToken } = await authApi.login(credentials);
            applyTokens(accessToken, refreshToken, AuthStage.AUTHENTICATED_NO_WORKSPACE);
        },
        [applyTokens]
    );

    const selectWorkspace = useCallback(
        async (workspaceId) => {
            const { accessToken, refreshToken } = await authApi.selectWorkspace(workspaceId);
            applyTokens(accessToken, refreshToken, AuthStage.WORKSPACE_SCOPED);
        },
        [applyTokens]
    );

    const logout = useCallback(async () => {
        try {
            await authApi.logout(); // invalidate refresh token server-side
        } catch {
            // best-effort — clear client state regardless
        }
        tokenStore.clearAll();
        setUser(null);
        setOrganization(null);
        setStage(AuthStage.UNAUTHENTICATED);
    }, []);

    // Wire the httpClient's refresh + hard-failure hooks to this context
    useEffect(() => {
        registerRefreshFn(async () => {
            const { accessToken, refreshToken } = await authApi.refreshTokens();
            // Preserve current stage — a refresh doesn't change scope,
            // it just renews the same-scoped token pair.
            tokenStore.setAccessToken(accessToken);
            tokenStore.setRefreshToken(refreshToken);
            const payload = decodeJwtPayload(accessToken);
            setUser(payload?.userId ? { userId: payload.userId } : null);
            setOrganization(payload?.organizationId ? { organizationId: payload.organizationId } : null);
            return accessToken;
        });

        registerAuthFailureHandler(() => {
            tokenStore.clearAll();
            setUser(null);
            setOrganization(null);
            setStage(AuthStage.UNAUTHENTICATED);
        });
    }, []);

    // On app boot: attempt a silent refresh to restore session from the
    // httpOnly cookie (or localStorage refresh token). This is what makes
    // a hard page reload not force a fresh login.
    useEffect(() => {
        (async () => {
            try {
                const { accessToken, refreshToken } = await authApi.refreshTokens();
                const payload = decodeJwtPayload(accessToken);
                const nextStage = payload?.organizationId
                    ? AuthStage.WORKSPACE_SCOPED
                    : AuthStage.AUTHENTICATED_NO_WORKSPACE;
                applyTokens(accessToken, refreshToken, nextStage);
            } catch {
                // no valid session — stay UNAUTHENTICATED, this is expected
                // for a logged-out visitor and not an error to surface
            } finally {
                setBooting(false);
            }
        })();
    }, [applyTokens]);

    const value = useMemo(
        () => ({ stage, user, organization, booting, login, selectWorkspace, logout }),
        [stage, user, organization, booting, login, selectWorkspace, logout]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within an AuthProvider");
    return ctx;
}
