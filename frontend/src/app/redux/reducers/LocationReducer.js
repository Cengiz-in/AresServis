import {
  GET_LOCATIONS,
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
    default: {
      return {
        ...state,
      };
    }
  }
};

export default LocationReducer;