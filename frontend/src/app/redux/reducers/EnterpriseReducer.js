import {
  GET_ENTERPRISE, 
} from '../actions/EnterpriseActions';

const initialState = {
  enterprise :[]
} 

const EnterpriseReducer = function (state = initialState, action) {
  
  switch (action.type) {
    case GET_ENTERPRISE: {
      return {
        ...state,
        enterprise: [...action.payload],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default EnterpriseReducer;