import axios from "axios.js";

export const GET_ENTERPRISE = " GET_ENTERPRISE";


export const getEnterprise = () => (dispatch) => {
  axios.get('/Enterprise').then((res) => {
    dispatch({
      type: GET_ENTERPRISE,
      payload: res.data.result,
    });
  });
};