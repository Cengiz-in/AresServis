import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const VehicleHistoryLocation = Loadable(lazy(() => import('./VehicleHistory')));

const vehicleHistory = [
  { path: '/VehicleHistory/default', element: <VehicleHistoryLocation />, auth: authRoles.Admin },
];

export default vehicleHistory;