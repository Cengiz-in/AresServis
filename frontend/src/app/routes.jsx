import AuthGuard from 'app/auth/AuthGuard';
import chartsRoute from 'app/views/charts/ChartsRoute';
import dashboardRoutes from 'app/views/dashboard/DashboardRoutes';
import mapRoutes from 'app/views/map/MapRoutes';
import materialRoutes from 'app/views/material-kit/MaterialRoutes';
import NotFound from 'app/views/sessions/NotFound';
import sessionRoutes from 'app/views/sessions/SessionRoutes';
import { Navigate } from 'react-router-dom';
import MatxLayout from './components/MatxLayout/MatxLayout';
import addVehicleRoutes from './views/addVehicle/AddVehicleRoutes';
import vehicleRoutes from './views/vehicle/VehicleRoutes';
import vehicleHistory from './views/vehicleHistory/VehicleHistoryRoutes'

const routes = [
  {
    element: (
      <AuthGuard>
        <MatxLayout />
      </AuthGuard>
    ),
    children: [...dashboardRoutes, ...mapRoutes, ...vehicleRoutes, ...vehicleHistory, ...addVehicleRoutes], //...chartsRoute, ...materialRoutes, ],
  },
  ...sessionRoutes,
  { path: '/', element: <Navigate to="dashboard/default" /> },
  { path: '*', element: <NotFound /> },
];

export default routes;
