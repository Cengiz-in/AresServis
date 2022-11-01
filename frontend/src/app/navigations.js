import { authRoles } from "./auth/authRoles";
import DirectionsBusIcon from '@mui/icons-material/DirectionsBus';
import AddBoxIcon from '@mui/icons-material/AddBox';
import NotListedLocationIcon from '@mui/icons-material/NotListedLocation';
import EmojiTransportationIcon from '@mui/icons-material/EmojiTransportation';

export const navigations = [
  { name: 'Anasayfa', path: '/dashboard/default', icon: 'dashboard', auth: authRoles.Driver },
  { name: 'Harita', path: '/map/default', icon: 'map', auth: authRoles.Admin },
  { name: 'Araçlar', icon: <DirectionsBusIcon fontSize="small"/>,
   children:[
    { name: 'Araç Bilgileri', path: '/vehicle/default', icon: <EmojiTransportationIcon fontSize="small"/>, auth: authRoles.Admin },
    { name: 'Araç Bilgileri Ekle', path: '/addVehicle/default', icon: <AddBoxIcon fontSize="small"/>, auth: authRoles.Admin },
   ]},
  
  { name: 'Lokasyon Bilgileri', path: '/vehicleHistory/default', icon: <NotListedLocationIcon fontSize="small" />, auth: authRoles.Admin },

  // { label: 'PAGES', type: 'label' },
  // {
  //   name: 'Session/Auth',
  //   icon: 'security',
  //   children: [
  //     { name: 'Sign in', iconText: 'SI', path: '/session/signin' },
  //     { name: 'Sign up', iconText: 'SU', path: '/session/signup' },
  //     { name: 'Forgot Password', iconText: 'FP', path: '/session/forgot-password' },
  //     { name: 'Error', iconText: '404', path: '/session/404' },
  //   ],
  // },
  // { label: 'Components', type: 'label' },
  // {
  //   name: 'Components',
  //   icon: 'favorite',
  //   badge: { value: '30+', color: 'secondary' },
  //   children: [
  //     { name: 'Auto Complete', path: '/material/autocomplete', iconText: 'A' },
  //     { name: 'Buttons', path: '/material/buttons', iconText: 'B' },
  //     { name: 'Checkbox', path: '/material/checkbox', iconText: 'C' },
  //     { name: 'Dialog', path: '/material/dialog', iconText: 'D' },
  //     { name: 'Expansion Panel', path: '/material/expansion-panel', iconText: 'E' },
  //     { name: 'Form', path: '/material/form', iconText: 'F' },
  //     { name: 'Icons', path: '/material/icons', iconText: 'I' },
  //     { name: 'Menu', path: '/material/menu', iconText: 'M' },
  //     { name: 'Progress', path: '/material/progress', iconText: 'P' },
  //     { name: 'Radio', path: '/material/radio', iconText: 'R' },
  //     { name: 'Switch', path: '/material/switch', iconText: 'S' },
  //     { name: 'Slider', path: '/material/slider', iconText: 'S' },
  //     { name: 'Snackbar', path: '/material/snackbar', iconText: 'S' },
  //     { name: 'Table', path: '/material/table', iconText: 'T' },
  //   ],
  // },
  // {
  //   name: 'Charts',
  //   icon: 'trending_up',
  //   children: [{ name: 'Echarts', path: '/charts/echarts', iconText: 'E' }],
  // },
  // {
  //   name: 'Documentation',
  //   icon: 'launch',
  //   type: 'extLink',
  //   path: 'http://demos.ui-lib.com/matx-react-doc/',
  // },
];
