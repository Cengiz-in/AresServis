import {
  GET_LOCATIONS,
  GET_VEHICLEHISTORY,
} from '../actions/LocationActions';

const initialState = {
  locations: [],
};

const LocationReducer = function (state = initialState, action) {
  
  switch (action.type) {
    case GET_LOCATIONS: {
      return {
        ...state,
        locations: [...action.payload],
      };
    }
    case GET_VEHICLEHISTORY: {
      return {
        ...state,
        history: [...action.payload],
      }
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default LocationReducer;