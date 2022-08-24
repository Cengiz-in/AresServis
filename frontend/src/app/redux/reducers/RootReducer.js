import { combineReducers } from 'redux';
import EcommerceReducer from './EcommerceReducer';
import NavigationReducer from './NavigationReducer';
import NotificationReducer from './NotificationReducer';
import LocationReducer from './LocationReducer';
import VehicleReducer from './VehicleReducer';

const RootReducer = combineReducers({
  notifications: NotificationReducer,
  navigations: NavigationReducer,
  ecommerce: EcommerceReducer,
  location: LocationReducer,
  vehicle: VehicleReducer,
});

export default RootReducer;
