import axios from 'axios.js';

export const POST_VEHICLE = 'POST_VEHICLE';
export const GET_VEHICLE = 'GET_VEHICLE';
export const PUT_VEHICLE = 'PUT_VEHICLE';
export const VEHICLE_SUCCEEDED ='VEHICLE_SUCCEEDED';
export const VEHICLE_ERRORS ='VEHICLE_ERRORS';
export const getVehicles = () => (dispatch) => {
  axios.get(`/vehicle/vehicles`).then((res) => {
    dispatch({
      type: GET_VEHICLE,
      payload: res.data.result,
    });
  });
};

export const putVehicles = (vehicleId,body) => (dispatch) => {
  axios.put(`/Vehicle/update/${vehicleId}`,{...body}).then((res) => {
    dispatch({
      type: PUT_VEHICLE,
      payload: res.data.result,
    });
  });
};

export const postVehicles = (values) => async (dispatch) => {
 const res = await axios.post(`/Vehicle/${values.id}`,{...values}); 
 console.log('arac ekleme')
 console.log(res)
    dispatch({
      type: POST_VEHICLE,
      payload: res.data.result,
    });

};

export const resetVehicleSucceeded = () => (dispatch) => {
  dispatch({
    type: VEHICLE_SUCCEEDED,
    payload: null,
  })
};

export const resetVehicleErrors = () => (dispatch) => {
  dispatch({
    type: VEHICLE_ERRORS,
    payload: null,
  })
};
