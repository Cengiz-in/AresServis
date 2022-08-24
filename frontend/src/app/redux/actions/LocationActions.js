import axios from 'axios.js';

export const GET_LOCATIONS = 'GET_LOCATIONS';

export const getLocations = () => (dispatch) => {
  axios.get('/Location').then((res) => {
    console.log(res);
    dispatch({
      type: GET_LOCATIONS,
      payload: res.data.result,
    });
  });
};

