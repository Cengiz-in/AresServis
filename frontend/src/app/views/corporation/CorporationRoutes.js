import Loadable from 'app/components/Loadable';
import { lazy } from 'react';
import { authRoles } from '../../auth/authRoles';

const Corporation = Loadable(lazy(() => import('./Corporation')));

const corporationRoutes = [
  { path: '/corporation/default', element: <Corporation />, auth: authRoles.Admin },
];

export default corporationRoutes;
