
import {
    GET_VEHICLE, 
    POST_VEHICLE, 
    PUT_VEHICLE,
    VEHICLE_ERRORS,
    VEHICLE_SUCCEEDED,
 } from '../actions/VehicleActions';

 const initialState = {
    vehicles :[]
} 



 const VehicleReducer = function (state = initialState, action) {
    
    switch (action.type) {
       
      case GET_VEHICLE: {
        return {
          ...state,
          vehicles: [...action.payload],
        };
      }
      case PUT_VEHICLE: {
        return {
          ...state,
          vehicleUpdate: action.payload,
        };
      }
      case POST_VEHICLE: {
        return {
          ...state,
          vehiclePost: action.payload,
        };
      }
      case VEHICLE_SUCCEEDED: {
        return {
          ...state,
          vehicleSucceded: action.payload
        };
      }
      case VEHICLE_ERRORS:{
        return {
          ...state,
          vehicleErrors: action.payload
        };
      }
      default: {
        return {
          ...state,
        };
      }
    }
  };

 export default VehicleReducer;