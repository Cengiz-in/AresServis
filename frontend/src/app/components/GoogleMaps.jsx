import {
  Grid,
  Icon,
  Typography,
} from '@mui/material';
import React from "react";
import GoogleMapReact from 'google-map-react';
import useWindowDimensions from 'app/hooks/useWindowDimensions';
import { sidenavCompactWidth } from 'app/utils/constant';

const BusIcon = ({ text }) => {
  return (
    <Grid container direction="row" alignItems="center" justifyContent="center">
      <Grid item>
        <Typography variant="subtitle1" color="secondary">
          {text}
        </Typography>
      </Grid>
      <Grid item>
        <Icon className="icon" color="secondary">
          directions_bus
        </Icon>
      </Grid>
    </Grid>
  );
};

const GoogleMaps = ({ locations }) => {
  const { height, width } = useWindowDimensions();
  const defaultProps = {
    center: {
      lat: 38.423733,
      lng: 27.142826
    },
    zoom: 11
  };

  return (
    <div style={{ height: height - sidenavCompactWidth, width: '100%' }}>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "" }}
        resetBoundsOnResize={true}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
      >
        {locations.map((location, i) => (
          <BusIcon
            key={location.id}
            lat={location.latitude}
            lng={location.longitude}
            text={location.plateNumber}
          />
        ))}
      </GoogleMapReact>
    </div>
  );
}

export default GoogleMaps