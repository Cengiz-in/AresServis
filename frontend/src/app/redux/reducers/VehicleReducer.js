
import {
    GET_VEHICLES,
    PUT_VEHICLE, 
 } from '../actions/LocationActions';

 const initialState = {

 };

 const VehicleReducer = function (state = initialState, action) {
    switch (action.type) {
        case GET_VEHICLES: {
            return {
                ...state,
                vehicles: [...action.payload],
            };
        }
        case PUT_VEHICLE: {
            return {
                ...state,
                vehicles: [...action.payload],
            };
        }
        default:{
            return {
                ...state,
            };
        }
    }
 };

 export default VehicleReducer;