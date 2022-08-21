import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const Vehicle = Loadable(lazy(() => import('./Vehicle')));

const vehicleRoutes = [
  { path: '/vehicle/default', element: <Vehicle />, auth: authRoles.Admin },
];

export default vehicleRoutes;
