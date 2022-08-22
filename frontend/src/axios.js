import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7079/api'
});

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => Promise.reject((error.response && error.response.data && error.response.data.message) || 'Something went wrong!')
);

export default axiosInstance;
