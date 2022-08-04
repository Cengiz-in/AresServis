import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const Map = Loadable(lazy(() => import('./Map')));

const mapRoutes = [
  { path: '/map/default', element: <Map />, auth: authRoles.Admin },
];

export default mapRoutes;
