import React, { useEffect } from 'react';
import { useState } from 'react';
import GoogleMapReact from 'google-map-react';
import useWindowDimensions from 'app/hooks/useWindowDimensions';
import { sidenavCompactWidth } from 'app/utils/constant';

const GoogleMap = ({ history }) => {
  const { height, width } = useWindowDimensions();
  const [map, setMap] = useState(null);
  const [maps, setMaps] = useState(null);
  const defaultProps = {
    center: {
      lat: 38.423733,
      lng: 27.142826,
    },
    zoom: 11,
  };

  const getMaps = (map,maps) => {
    setMap(map);
    setMaps(maps);
  }

  useEffect(() => {
    if(history){
    const locations = history.map((item) => {
      return {
        lat: item.latitude,
        lng: item.longitude
      };
    });
    renderPolylines(map,maps,locations);
  }
  }, [history]);

  const renderPolylines = (map, maps,markers) => {
    // /** Example of rendering geodesic polyline */
    let geodesicPolyline = new maps.Polyline({
      path: markers,
      geodesic: true,
      strokeColor: '#00a1e1',
      strokeOpacity: 1.0,
      strokeWeight: 4,
    });
    geodesicPolyline.setMap(map);

    fitBounds(map, maps,markers);
  };

  const fitBounds = (map, maps,markers) => {
    var bounds = new maps.LatLngBounds();
    for (let marker of markers) {
      bounds.extend(new maps.LatLng(marker.lat, marker.lng));
    }
    map.fitBounds(bounds);
  };

  return (
    <div style={{ height: height - sidenavCompactWidth, width: '100%' }}>
      <GoogleMapReact
        bootstrapURLKeys={{ key: 'AIzaSyAXy8dgvhceCnZtQgSVcKK1uj23-0fD_TY' }}
        resetBoundsOnResize={true}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
        onGoogleApiLoaded={({ map, maps }) => getMaps(map, maps)}
      ></GoogleMapReact>
    </div>
  );
};

export default GoogleMap;
