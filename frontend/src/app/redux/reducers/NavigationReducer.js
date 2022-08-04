import { navigations } from 'app/navigations';
import { SET_USER_NAVIGATION } from '../actions/NavigationAction';

const initialState = {
  navigations,
  userNavigations: []
};

const NavigationReducer = function (state = initialState, action) {
  switch (action.type) {
    case SET_USER_NAVIGATION: {
      return {
        ...state,
        userNavigations: [...action.payload],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default NavigationReducer;
