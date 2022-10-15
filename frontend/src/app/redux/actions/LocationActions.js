import axios from "axios.js";

export const GET_LOCATIONS = "GET_LOCATIONS";
export const GET_VEHICLEHISTORY = "GET_VEHICLEHISTORY";

export const getLocations = () => (dispatch) => {
  axios.get("/Location").then((res) => {
    dispatch({
      type: GET_LOCATIONS,
      payload: res.data.result,
    });
  });
};

export const getHistory =
  (vehicleId, startDate, endDate) => async (dispatch) => {
    const res = await axios.get(`/Location/${vehicleId}?startDate=${startDate}&endDate=${endDate}`
    );
    dispatch({
      type: GET_VEHICLEHISTORY,
      payload: res.data.result,
    });
  };
