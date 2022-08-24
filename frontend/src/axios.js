import axios from 'axios';
const { REACT_APP_API_HOST } = process.env;

const axiosInstance = axios.create({
  baseURL: `${REACT_APP_API_HOST}/api`
});

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => Promise.reject((error.response && error.response.data && error.response.data.message) || 'Something went wrong!')
);

export default axiosInstance;
