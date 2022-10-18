import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const AddVehicle = Loadable(lazy(() => import('./AddVehicle')));

const addVehicleRoutes = [
  { path: '/addVehicle/default', element: <AddVehicle />, auth: authRoles.Admin },
];

export default addVehicleRoutes;