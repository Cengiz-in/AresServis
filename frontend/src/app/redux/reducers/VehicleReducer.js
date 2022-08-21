
import {
    GET_VEHICLES, 
 } from '../actions/VehicleActions';

 const initialState = {
        succeeded:"",
        message: "",
        errors: "",
        result: [
          {
            id: id,
            plateNumber: "",
            isActive: false,
            device: "",
            vehicleAppUsers: [
              {
                appUserId: id,
                appUserName: ""
              }
            ]       
} 
]
 };

 const VehicleReducer = function (state = initialState, action) {
    switch (action.type) {
      case GET_VEHICLES: {
        return {
          ...state,
          vehicles: [...action.payload],
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