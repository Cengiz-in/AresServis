import '../fake-db';
import { Provider } from 'react-redux';
import { useRoutes } from 'react-router-dom';
import { positions, transitions, Provider as AlertProvider } from "react-alert";
import AlertTemplate from './components/AlertTemplate/index'
import { MatxTheme } from './components';
import { AuthProvider } from './contexts/JWTAuthContext';
import { SettingsProvider } from './contexts/SettingsContext';
import { Store } from './redux/Store';
import routes from './routes';

const options = {
  position: positions.BOTTOM_CENTER,
  timeout: 5000,
  offset: '24px',
  transition: transitions.FADE,
}

const App = () => {
  const content = useRoutes(routes);

 

  return (
    <Provider store={Store}>
      <SettingsProvider>
        <MatxTheme>
          <AlertProvider template={AlertTemplate} {...options}>
            <AuthProvider>{content}</AuthProvider>
          </AlertProvider>
        </MatxTheme>
      </SettingsProvider>
    </Provider>
  );
};

export default App;
