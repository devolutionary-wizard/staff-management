export const API_ENDPOINTS = {
    BASE_URL: import.meta.env.VITE_APP_API_BASE_URL,
    STAFFS: {
        GET_ALL: () => `${API_ENDPOINTS.BASE_URL}/staff`,
        GET_BY_ID: (id: string) => `${API_ENDPOINTS.BASE_URL}/staff/${id}`,
        CREATE: () => `${API_ENDPOINTS.BASE_URL}/staff`,
        UPDATE: (id: string) => `${API_ENDPOINTS.BASE_URL}/staff/${id}`,
        DELETE: (id: string) => `${API_ENDPOINTS.BASE_URL}/staff/${id}`,
    }
};