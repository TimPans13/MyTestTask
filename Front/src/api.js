// api.js
import axios from 'axios';

const API_BASE_URL = 'https://localhost:7045/api/v1/contacts/';

const api = axios.create({
  baseURL: API_BASE_URL,
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('Request error:', error);
    return Promise.reject(error);
  }
);

export default api;
