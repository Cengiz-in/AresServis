import * as React from "react";
import { useEffect, useState } from "react";
import Box from "@mui/material/Box";
import { positions } from '@mui/system';
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import {
  getVehicles,
  postVehicles,
  resetVehicleErrors,
} from "app/redux/actions/VehicleActions";
import {getEnterprise} from "app/redux/actions/EnterpriseActions";
import { Formik } from "formik";
import * as Yup from "yup";
import { Grid } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { useParams } from "react-router-dom";




const AddVehicle = () => {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch();
  const { vehicles, vehicleSucceded, vehicleErrors } = useSelector((state) => state.vehicle);
  const [openEdit, setOpenEdit] = useState(false);
  const {enterprise} =useSelector((state)=> state.enterprise)
  // form field validation schema
  const validationSchema = Yup.object().shape({
    //plateNumber: Yup.string().required("Plaka boş geçilemez!"),
  });


  const addInitilaValue = {
    plateNumber: "",
  };

  const handleEditClose = () => {
    setOpenEdit(false);
  };

  useEffect(() => {
    if (vehicleErrors != null) {
      const variant = "error";
      // enqueueSnackbar(vehicleErrors, { variant });
      dispatch(resetVehicleErrors());
    }
  }, [vehicleErrors, dispatch]);

  useEffect(() => {
    if (vehicleSucceded != null) {
      const variant = "success";
      //enqueueSnackbar("Başarılı", { variant });
      dispatch(resetVehicleErrors());
      handleEditClose();
    }
  }, [vehicleSucceded, dispatch]);

  useEffect(() => {
    try {
      dispatch(getEnterprise());
    } catch (e) {
      alert.error(e);
    }
  }, []);

  useEffect(() => {
    try {
      dispatch(getVehicles());
    } catch (e) {
      alert.error(e);
    }
  }, []);

  const submitAddVehicleForm = async (values) => {
    console.log('asd')
    console.log(values)
   await dispatch(postVehicles(values));
    handleEditClose();
  };
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  
 

  const columns = [
    
    { field: "id", headerName: "#", type: "number", editable: false},
    {
      field: "plateNumber",
      headerName: "Plaka",  
     
      type: "string",
      editable: true,

    }, 
  ];
  return (
    <Box
      textAlign="center"
      sx={{
        height: 500,
        width: "100%",
        "& .actions": {
          color: "text.secondary",
        },
        "& .textPrimary": {
          color: "text.primary",
        },
      }}
    >
      <Button
        variant="contained"
        color="primary"
        sx={{ m: 2 }}
        onClick={handleClickOpen}
      >
        Araç Ekle
      </Button>
      <DataGrid rows={vehicles} columns={columns} />

      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="edit-apartment"
      >
        <DialogTitle>Araç Ekle</DialogTitle>
        <DialogContent>
          <Formik
            onSubmit={submitAddVehicleForm}
            initialValues={addInitilaValue}
            validationSchema={validationSchema}
          >
            {({
              values,
              errors,
              touched,
              handleChange,
              handleBlur,
              handleSubmit,
              isValid,
              dirty,
            }) => (
              <form onSubmit={handleSubmit}>
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="plateNumber"
                  value={values.plateNumber}
                  label="Plaka"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  helperText={touched.plateNumber && errors.plateNumber}
                  error={Boolean(errors.plateNumber && touched.plateNumber)}
                  sx={{ mb: 3 }}
                />
                 <Select
                  fullWidth
                  value={values.id}
                  name="id"
                  variant="outlined"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  autoWidth
                  label="Kurum"
                  error={Boolean(errors.id && touched.id)}
                  sx={{ mb: 3 }}
                >
                  {enterprise.map((element, idx) => {
                    return (
                      <MenuItem value={element.id} key={idx}>
                        {element.name}
                      </MenuItem>
                    );
                  })}
                </Select>

                <DialogActions>
                  <Grid
                    container
                    spacing={4}
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                  >
                    <Grid item={true}>
                      <Button
                        type="submit"
                        color="primary"
                        variant="contained"
                        sx={{ my: 2 }}
                      >
                        Kaydet
                      </Button>
                    </Grid>
                    <Grid item={true}>
                      <Button
                        variant="contained"
                        color="error"
                        onClick={handleClose}
                      >
                        Kapat
                      </Button>
                    </Grid>
                  </Grid>
                </DialogActions>
              </form>
            )}
          </Formik>
        </DialogContent>
      </Dialog>
    </Box>
  );
}

export default AddVehicle;