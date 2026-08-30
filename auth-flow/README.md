# Setup checklist

1. **Read `AUTH_FLOW.md` first** — explains the 3-stage state machine and why tokens are stored the way they are.

2. **Backend requirements**
   - `POST /auth/login` → `{ accessToken, refreshToken? }`
   - `GET /workspaces` → `[{ id, name, role }]` (requires `Authorization: Bearer <accessToken>`)
   - `POST /auth/select-workspace` → `{ accessToken, refreshToken? }` (requires header token + `{ workspaceId }` body)
   - `POST /auth/refresh` → `{ accessToken, refreshToken? }`
   - `POST /auth/logout` → invalidates the refresh token server-side
   - If using `REFRESH_MODE = 'cookie'` (recommended), the backend must set the refresh token via `Set-Cookie: refreshToken=...; HttpOnly; Secure; SameSite=Strict` on the login, select-workspace, and refresh responses, and clear it on logout.

3. **Set `REFRESH_MODE`** in `src/lib/tokenStore.js` to `'cookie'` or `'localStorage'` depending on what your backend supports today.

4. **Set `VITE_API_URL`** (or swap for your bundler's env var syntax) in `.env` to point at your API base URL.

5. **Wrap your app** with `<AuthProvider>` (already done in `App.jsx`) and use `<ProtectedRoute minStage={...}>` for anything that needs gating.

6. **Install dependencies**: `axios`, `react-router-dom` (v6+).

7. Everywhere else in your app, just import `api` from `src/lib/httpClient.js` and call `api.get(...)` / `api.post(...)` as normal — the token attachment and refresh-on-401 logic is automatic.
