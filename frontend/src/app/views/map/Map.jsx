import { Grid } from '@mui/material';
import { Fragment, useEffect } from 'react';
import { GoogleMaps } from 'app/components';
import { useDispatch, useSelector } from 'react-redux';
import { getLocations } from 'app/redux/actions/LocationActions';

const Map = () => {
  const dispatch = useDispatch();
  const { locations } = useSelector((state) => state.location);

  useEffect(() => {
    const interval = setInterval(() => {
      try {
        dispatch(getLocations());
      } catch (e) {
        alert.error(e);
      }
    }, 3000);
    return () => clearInterval(interval);
  }, []);

  return (
    <Fragment>
      <Grid container spacing={3}>
        <Grid item lg={12} md={12} sm={12} xs={12}>
          <GoogleMaps locations={locations} />
        </Grid>
      </Grid>
    </Fragment>
  );
};

export default Map;