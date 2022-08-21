import axios from 'axios.js';

export const GET_VEHICLE = 'GET_VEHICLE';
export const PUT_VEHICLE = 'PUT_VEHICLE';

export const getVehicles = () => (dispatch) => {
    axios.get(`/vehicle/vehicles/`).then((res) => {
        dispatch({
            type:GET_VEHICLE,
            payload: res.data.result,
        });
    });
};

export const putVehicles = (vehicleId) => (dispatch) => {
    axios.put(`/Vehicle/${vehicleId}/update/`).then((res)=>{
        dispatch({
            type: PUT_VEHICLE,
            payload: res.data.result,
        });
    });
};