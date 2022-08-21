import axios from 'axios.js';

export const GET_VEHICLE = 'GET_VEHICLES';
export const PUT_VEHICLE = 'PUT_VEHICLE';

export const getVehicles = () => (dispatch) => {
    axios.get(`/vehicle/vehicles`).then((res) => {
        dispatch({
            type:GET_VEHICLE,
            payload: res.data.result,
        });
    });
};

export const putVehicles = (values) => (dispatch) => {
    axios.put(`/Vehicle/${vehicleId}/update`,values).then((res)=>{
        dispatch({
            type: PUT_VEHICLE,
            payload: res.data.result,
        });
    });
};