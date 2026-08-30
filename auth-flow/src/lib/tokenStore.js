/**
 * Single source of truth for tokens.
 *
 * Access token: kept in memory only (module-level variable). Never persisted.
 * This means a hard page refresh loses it — that's intentional; AuthContext
 * re-derives it on boot via a silent refresh call (see AuthContext.jsx).
 *
 * Refresh token: two supported modes —
 *   - "cookie" (recommended): backend sets an httpOnly cookie on login /
 *     select-workspace responses. The frontend never touches it directly;
 *     the browser attaches it automatically to requests (with
 *     `withCredentials: true` on the axios instance). REFRESH_MODE stays
 *     'cookie' and refreshToken getters/setters below become no-ops.
 *   - "localStorage" (fallback): used only if your backend can't yet set
 *     httpOnly cookies. Explicit XSS trade-off — see AUTH_FLOW.md §2.
 */

export const REFRESH_MODE = "cookie"; // "cookie" | "localStorage"

const REFRESH_TOKEN_KEY = "auth.refreshToken";

let accessToken = null;

export const tokenStore = {
    getAccessToken() {
        return accessToken;
    },

    setAccessToken(token) {
        accessToken = token;
    },

    clearAccessToken() {
        accessToken = null;
    },

    getRefreshToken() {
        if (REFRESH_MODE === "cookie") return undefined; // browser handles it
        return localStorage.getItem(REFRESH_TOKEN_KEY);
    },

    setRefreshToken(token) {
        if (REFRESH_MODE === "cookie") return; // backend Set-Cookie handles it
        if (token) localStorage.setItem(REFRESH_TOKEN_KEY, token);
    },

    clearRefreshToken() {
        if (REFRESH_MODE === "cookie") return; // cleared by backend on logout
        localStorage.removeItem(REFRESH_TOKEN_KEY);
    },

    clearAll() {
        tokenStore.clearAccessToken();
        tokenStore.clearRefreshToken();
    },
};

/**
 * Best-effort decode for DISPLAY PURPOSES ONLY.
 * Never use this to make auth/routing decisions — see AUTH_FLOW.md §7.
 */
export function decodeJwtPayload(token) {
    if (!token) return null;
    try {
        const [, payload] = token.split(".");
        return JSON.parse(atob(payload.replace(/-/g, "+").replace(/_/g, "/")));
    } catch {
        return null;
    }
}
