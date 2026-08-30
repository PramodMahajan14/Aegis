import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth, stageAtLeast, AuthStage } from "../context/AuthContext";

/**
 * Usage:
 *   <Route path="/workspaces" element={
 *     <ProtectedRoute minStage={AuthStage.AUTHENTICATED_NO_WORKSPACE}>
 *       <WorkspaceSelect />
 *     </ProtectedRoute>
 *   } />
 *
 *   <Route path="/app/*" element={
 *     <ProtectedRoute minStage={AuthStage.WORKSPACE_SCOPED}>
 *       <AppShell />
 *     </ProtectedRoute>
 *   } />
 */
export function ProtectedRoute({ children, minStage }) {
    const { stage, booting } = useAuth();
    const location = useLocation();

    if (booting) {
        // Still attempting silent refresh on load — render nothing (or a
        // full-page spinner) rather than flashing a redirect to /login.
        return null;
    }

    if (!stageAtLeast(stage, minStage)) {
        // Special case: user is authenticated but hasn't picked a workspace
        // yet, and tries to hit an org-scoped route directly (e.g. bookmark) —
        // send them to workspace selection instead of all the way to login.
        if (
            minStage === AuthStage.WORKSPACE_SCOPED &&
            stageAtLeast(stage, AuthStage.AUTHENTICATED_NO_WORKSPACE)
        ) {
            return <Navigate to="/workspaces" replace />;
        }
        return <Navigate to="/login" replace state={{ from: location }} />;
    }

    return children;
}
