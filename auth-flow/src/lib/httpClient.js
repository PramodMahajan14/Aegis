import axios from "axios";
import { tokenStore, REFRESH_MODE } from "./tokenStore";

const BASE_URL = import.meta.env?.VITE_API_URL || "/api";

export const api = axios.create({
    baseURL: BASE_URL,
    withCredentials: REFRESH_MODE === "cookie", // send/receive httpOnly cookie
});

// --- Attach access token to every outgoing request ---
api.interceptors.request.use((config) => {
    const token = tokenStore.getAccessToken();
    if (token) {
        config.headers = config.headers || {};
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// --- Refresh-on-401, with a single in-flight refresh shared across
//     any requests that 401 at the same time ---

let refreshPromise = null;
let onAuthFailure = () => {}; // set by AuthContext via registerAuthFailureHandler

export function registerAuthFailureHandler(fn) {
    onAuthFailure = fn;
}

// Injected lazily to avoid a circular import with authApi.js
let refreshFn = null;
export function registerRefreshFn(fn) {
    refreshFn = fn;
}

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const { config, response } = error;

        // Only attempt refresh for 401s, and only once per request
        if (response?.status !== 401 || config._retried || !refreshFn) {
            return Promise.reject(error);
        }
        // Don't try to refresh the refresh call itself
        if (config.url?.includes("/auth/refresh")) {
            onAuthFailure();
            return Promise.reject(error);
        }

        config._retried = true;

        try {
            // Share one in-flight refresh across all concurrently-failing requests
            if (!refreshPromise) {
                refreshPromise = refreshFn().finally(() => {
                    refreshPromise = null;
                });
            }
            const newAccessToken = await refreshPromise;

            config.headers.Authorization = `Bearer ${newAccessToken}`;
            return api(config); // retry the original request exactly once
        } catch (refreshError) {
            onAuthFailure(); // refresh itself failed -> force logout
            return Promise.reject(refreshError);
        }
    }
);
