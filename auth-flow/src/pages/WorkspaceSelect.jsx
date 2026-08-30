import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { getWorkspaces } from "../lib/authApi";

export default function WorkspaceSelect() {
    const { selectWorkspace } = useAuth();
    const navigate = useNavigate();

    const [workspaces, setWorkspaces] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selectingId, setSelectingId] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        getWorkspaces()
            .then(setWorkspaces)
            .catch(() => setError("Couldn't load your workspaces. Please try again."))
            .finally(() => setLoading(false));
    }, []);

    const handleSelect = async (workspaceId) => {
        setSelectingId(workspaceId);
        setError(null);
        try {
            await selectWorkspace(workspaceId);
            navigate("/app", { replace: true });
        } catch {
            setError("Couldn't switch to that workspace. Please try again.");
            setSelectingId(null);
        }
    };

    if (loading) {
        return (
            <div className="d-flex align-items-center justify-content-center" style={{ minHeight: "100vh" }}>
                <div className="spinner-border text-primary" role="status" />
            </div>
        );
    }

    return (
        <div className="d-flex align-items-center justify-content-center" style={{ minHeight: "100vh" }}>
            <div style={{ width: 420 }}>
                <h5 className="mb-3">Choose a workspace</h5>

                {error && <div className="alert alert-danger py-2 shadow-none" style={{ fontSize: 13.5 }}>{error}</div>}

                <div className="border rounded overflow-hidden">
                    {workspaces.map((ws, i) => (
                        <button
                            key={ws.id}
                            onClick={() => handleSelect(ws.id)}
                            disabled={selectingId !== null}
                            className={`btn w-100 text-start d-flex align-items-center justify-content-between px-3 py-3 rounded-0 shadow-none ${i !== 0 ? "border-top" : ""}`}
                        >
                            <div>
                                <div className="fw-medium" style={{ fontSize: 14 }}>{ws.name}</div>
                                <div className="text-muted" style={{ fontSize: 12 }}>{ws.role}</div>
                            </div>
                            {selectingId === ws.id && (
                                <span className="spinner-border spinner-border-sm text-primary" />
                            )}
                        </button>
                    ))}
                    {workspaces.length === 0 && (
                        <div className="text-center text-muted p-4" style={{ fontSize: 13.5 }}>
                            No workspaces found for your account.
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}
