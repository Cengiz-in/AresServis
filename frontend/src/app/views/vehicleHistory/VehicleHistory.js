import { Grid, Select } from "@mui/material";
import { useEffect, useState } from "react";
import { GoogleMap } from "app/components";
import { useDispatch, useSelector } from "react-redux";
import { getHistory } from "app/redux/actions/LocationActions";
import {
  Box,
  Flex,
  HStack,
  IconButton,
} from "@chakra-ui/react";
import MenuItem from "@mui/material/MenuItem";
import { Button, ButtonGroup } from "@mui/material";
import { FaLocationArrow, FaTimes } from "react-icons/fa";
import {getVehicles,} from "app/redux/actions/VehicleActions";
import TextField from "@mui/material/TextField";
import { FormControl,InputLabel } from "@mui/material";
import * as React from "react";
import { Formik } from "formik";
import * as Yup from "yup";

const VehicleHistory = () => {
  const dispatch = useDispatch();
  const { history } = useSelector((state) => state.history);
  const { vehicles } = useSelector((state) => state.vehicle);
  const [inputValue, setValue] = useState("");
  const vehicleInformation = {
    id: "",
    startDate: "",
    endDate: "",
  };

  const validationSchema = Yup.object().shape({
    id: Yup.number().required("Plaka boş geçilemez!"),
    startDate: Yup.string().required("Tarih alanı boş geçilemez!"),
    endDate: Yup.string().required("Tarih alanı boş geçilemez!"),
  });

  useEffect(() => {
    try {
      dispatch(getVehicles());
    } catch (e) {
      alert.error(e);
    }
  }, []);

 

  const showRoute = async (values) => {
    await dispatch(getHistory(values.id, values.startDate, values.endDate));
  };

  return (
    <Flex
      position="relative"
      flexDirection="column"
      alignItems="center"
      h="100vh"
      w="100vw"
    >
      <Box position="absolute" left={0} top={0} h="100%" w="100%">
        <Grid container spacing={3}>
          <Grid item lg={12} md={12} sm={12} xs={12}>
            <GoogleMap history={history} />
          </Grid>
        </Grid>
      </Box>
      <Box
        p={4}
        borderRadius="lg"
        m={4}
        bgColor="#ffffff"
        shadow="base"
        minW="container.md"
        zIndex="1"
      >
        <HStack spacing={140} justifyContent="space-between">
          <Box flexGrow={40}>
            <Formik
              onSubmit={showRoute}
              initialValues={vehicleInformation}
              validationSchema={validationSchema}
            >
              {({
                values,
                errors,
                touched,
                handleChange,
                handleBlur,
                handleSubmit,
              }) => (
                <form onSubmit={handleSubmit}>
                  <FormControl sx={{ m: 1, minWidth: 160, p: -2 }}>
                    <InputLabel id="demo-simple-select-helper-label">
                      Araç Plaka
                    </InputLabel>
                    <Select
                      fullWidth                     
                      name="id"
                      value={values.id}
                      variant="outlined"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      autoWidth
                      error={Boolean(errors.id && touched.id)}
                      sx={{ mb: 3 }}
                    >
                      {vehicles.map((element, idx) => {
                        return (
                          <MenuItem value={element.id} key={idx}>
                            {element.plateNumber}
                          </MenuItem>
                        );
                      })}
                    </Select>
                  </FormControl>
                  <FormControl sx={{ m: 1, minWidth: 160, p: -2 }}>
                    <TextField
                    sx={{ mb: 1.5, Color: '1px  pink', borderRadius: 1}}
                      InputLabelProps={{ shrink: true }}
                      fullWidth
                      type="date"
                      value={values.startDate}
                      onChange={handleChange}
                      onBlur={handleBlur}
                      label="Başlangıç Tarihi"
                      name="startDate"
                      variant="outlined"
                      helperText="Başlangıç Tarihini Seçin"
                    //  helperText={touched.startDate && errors.startDate}
                      error={Boolean(errors.startDate && touched.startDate)}
                      
                    />
                  </FormControl>
                  <FormControl sx={{ m: 1, minWidth: 160, p: -2 }}>
                    <TextField
                      InputLabelProps={{ shrink: true }}
                      fullWidth
                      type="date"
                      value={values.endDate}
                      name="endDate"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      label="Bitiş Tarihi"
                      variant="outlined"
                      helperText="Bitiş Tarihini Seçin"
                      error={Boolean(errors.endDate && touched.endDate)}
                      sx={{ mb: 1.5 }}
                    />
                  </FormControl>
                  <FormControl sx={{ m: 1, minWidth: 150, p: 1 }}>
                    <ButtonGroup>
                      <Button
                        style={{
                          borderRadius: 35,
                          backgroundColor: "#21b6ae",
                          padding: "10px 20px",
                          fontSize: "14px",
                        }}
                        variant="contained"
                        type="submit"
                      >
                        Konum Göster
                      </Button>
                    </ButtonGroup>
                  </FormControl>
                </form>
              )}
            </Formik>
          </Box>
        </HStack>
      </Box>
    </Flex>
  );
};

export default VehicleHistory;
