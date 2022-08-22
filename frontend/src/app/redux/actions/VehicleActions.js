import axios from 'axios.js';

export const GET_VEHICLE = 'GET_VEHICLE';
export const PUT_VEHICLE = 'PUT_VEHICLE';

export const getVehicles = (id) => (dispatch) => {
    axios.get(`/vehicle/vehicles`).then((res) => {
   console.log('res');
   console.log(res);
        dispatch({
            type:GET_VEHICLE,
            payload: res.data.result,
        });
    });
}


export const putVehicles = (vehicleId) => (dispatch) => {
    axios.put(`/Vehicle/update/${vehicleId}`).then((res)=>{
        dispatch({
            type: PUT_VEHICLE,
            payload: res.data.result,
        });
    });
};