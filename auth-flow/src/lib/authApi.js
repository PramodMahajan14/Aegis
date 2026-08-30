import { api } from "./httpClient";

/** Step 1 — POST email + password, get user-level tokens back */
export function login({ email, password }) {
    return api.post("/auth/login", { email, password }).then((res) => res.data);
    // -> { accessToken, refreshToken? }
}

/** Step 2 — GET list of workspaces, requires user-level access token */
export function getWorkspaces() {
    return api.get("/workspaces").then((res) => res.data);
    // -> [{ id, name, role }, ...]
}

/** Step 3 — POST workspaceId, get back a NEW org-scoped token pair */
export function selectWorkspace(workspaceId) {
    return api.post("/auth/select-workspace", { workspaceId }).then((res) => res.data);
    // -> { accessToken, refreshToken? }  (accessToken now carries organizationId too)
}

/** Silent refresh — called by the httpClient interceptor on 401 */
export function refreshTokens() {
    // If REFRESH_MODE is 'cookie', the refresh token rides along automatically
    // (withCredentials: true). If 'localStorage', send it explicitly:
    //
    // return api.post('/auth/refresh', { refreshToken: tokenStore.getRefreshToken() })
    return api.post("/auth/refresh").then((res) => res.data);
    // -> { accessToken, refreshToken? }
}

export function logout() {
    return api.post("/auth/logout").then((res) => res.data);
}
