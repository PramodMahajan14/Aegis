import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider, AuthStage } from "./context/AuthContext";
import { ProtectedRoute } from "./routes/ProtectedRoute";
import Login from "./pages/Login";
import WorkspaceSelect from "./pages/WorkspaceSelect";
// import AppShell from './pages/AppShell'; // your main authenticated app

export default function App() {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Routes>
                    <Route path="/login" element={<Login />} />

                    <Route
                        path="/workspaces"
                        element={
                            <ProtectedRoute minStage={AuthStage.AUTHENTICATED_NO_WORKSPACE}>
                                <WorkspaceSelect />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/app/*"
                        element={
                            <ProtectedRoute minStage={AuthStage.WORKSPACE_SCOPED}>
                                {/* <AppShell /> */}
                                <div>Your authenticated app goes here</div>
                            </ProtectedRoute>
                        }
                    />
                </Routes>
            </AuthProvider>
        </BrowserRouter>
    );
}
