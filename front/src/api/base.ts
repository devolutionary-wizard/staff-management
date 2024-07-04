import axios from 'axios'
import { API_ENDPOINTS } from '../config/apiEndpoints'

const API = axios.create({
    baseURL: API_ENDPOINTS.BASE_URL,
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',

    }
})

export { API }