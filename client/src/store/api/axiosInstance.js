import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5000/api',  
  headers: {
    'Content-Type': 'application/json',
  },
});

import("../store").then(({ store }) => {
    apiClient.defaults.baseURL = store.getState().api.baseURL;
  
    apiClient.interceptors.request.use((config) => {
      const token = store.getState().auth.token;
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    }, (error) => Promise.reject(error));
  });
  
  export default apiClient;
