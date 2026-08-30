import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function Login() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const [submitting, setSubmitting] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setSubmitting(true);
        try {
            await login({ email, password });
            navigate("/workspaces", { replace: true });
        } catch (err) {
            setError(err?.response?.data?.message || "Invalid email or password.");
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <div className="d-flex align-items-center justify-content-center" style={{ minHeight: "100vh" }}>
            <form onSubmit={handleSubmit} className="border rounded p-4" style={{ width: 360, boxShadow: "none" }}>
                <h5 className="mb-3">Sign in</h5>

                {error && <div className="alert alert-danger py-2 shadow-none" style={{ fontSize: 13.5 }}>{error}</div>}

                <div className="mb-3">
                    <label className="form-label" style={{ fontSize: 13 }}>Email</label>
                    <input
                        type="email"
                        className="form-control shadow-none"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label" style={{ fontSize: 13 }}>Password</label>
                    <input
                        type="password"
                        className="form-control shadow-none"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary w-100 shadow-none" disabled={submitting}>
                    {submitting ? "Signing in..." : "Sign in"}
                </button>
            </form>
        </div>
    );
}
