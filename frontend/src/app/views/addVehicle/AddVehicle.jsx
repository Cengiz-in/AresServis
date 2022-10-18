import * as React from 'react';
import { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { DataGrid } from '@mui/x-data-grid';
import { useDispatch, useSelector } from 'react-redux';
import {
  getVehicles,
  putVehicles,
  postVehicles,
  resetVehicleErrors,
} from 'app/redux/actions/VehicleActions';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { Grid } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';

export default function Vehicle() {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch();
  const { vehicles, vehicleSucceded, vehicleErrors } = useSelector(
    (state) => state.vehicle
  );
  const [openEdit, setOpenEdit] = useState(false);
  const [editVehicleId, setEditVehicleId] = useState(null);
  const [vehicleInformation, setVehicleInformation] = useState({
    plateNumber: '',
    deviceId: 0,
  });

  // form field validation schema
  const validationSchema = Yup.object().shape({
    plateNumber: Yup.string().required('Plaka boş geçilemez!'),
    deviceId: Yup.string().required('Device alanı boş geçilemez!'),
  });

  const addInitilaValue = {
    plateNumber: '',
  }

  const handleEditOpen = (event, cellValues) => {
    setVehicleInformation({
      plateNumber: cellValues.row.plateNumber,
      deviceId: cellValues.row.device.id,
    });
    setEditVehicleId(cellValues.row.id);
    setOpenEdit(true);
  };

  const handleEditClose = () => {
    setOpenEdit(false);
  };

  useEffect(() => {
    try {
      dispatch(getVehicles());
    } catch (e) {
      alert.error(e);
    }
  }, []);


  useEffect(() => {
    if (vehicleErrors != null) {
      const variant = 'error';
      // enqueueSnackbar(vehicleErrors, { variant });
      dispatch(resetVehicleErrors());
    }
  }, [vehicleErrors, dispatch]);

  useEffect(() => {
    if (vehicleSucceded != null) {
      const variant = 'success';
      //enqueueSnackbar("Başarılı", { variant });
      dispatch(resetVehicleErrors());
      handleEditClose();
    }
  }, [vehicleSucceded, dispatch]);

  const submitVehicleForm = async (values) => {
    dispatch(putVehicles(editVehicleId, values));
    handleEditClose();
    dispatch(getVehicles());
  };

  const submitAddVehicleForm = async (values) => {
    dispatch(postVehicles(values));
    handleEditClose();
    dispatch(getVehicles());
  };

  function getDevice(params) {
    return `${params.value.name || ''}`;
  }

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const columns = [
    { field: 'id', headerName: '#', type: 'number', editable: false },
    {
      field: 'plateNumber',
      headerName: 'Plaka',
      type: 'string',
      editable: true,
    },
    {
      field: 'Actions',
      headerName: 'İşlemler',
      type: 'string',
      width: 220,
      renderCell: (cellValues) => {
        return (
          <Button
            variant="contained"
            color="primary"
            icon="Edit"
            onClick={(event) => {
              handleEditOpen(event, cellValues);
            }}
          >
            <EditIcon />
            
          </Button>
          
        );
      },
    },
    {
        field: 'AddAction',
        headerName: 'İşlemler',
        type: 'string',
        width: 220,
        renderCell: () => {
          return (
            <Button
            variant="contained"
            color="primary"
            sx={{ m:1}}
            onClick={handleClickOpen}
          >
            Araç Ekle
          </Button>
            
          );
        },
      },
  ];

  return (
    <Box
      sx={{
        height: 500,
        width: '100%',
        '& .actions': {
          color: 'text.secondary',
        },
        '& .textPrimary': {
          color: 'text.primary',
        },
      }}
    >
        
      <DataGrid rows={vehicles} columns={columns} />

      <Dialog open={openEdit}>
        <DialogTitle>Araç plaka değiştir</DialogTitle>
        <DialogContent>
          <Formik
            onSubmit={submitVehicleForm}
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
              isValid,
              dirty,
            }) => (
              <form onSubmit={handleSubmit}>
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="plateNumber"
                  label="Plaka"
                  variant="outlined"
                  onBlur={handleBlur}
                  value={values.plateNumber}
                  onChange={handleChange}
                  helperText={touched.plateNumber && errors.plateNumber}
                  error={Boolean(errors.plateNumber && touched.plateNumber)}
                  sx={{ mb: 3 }}
                />
                
                <DialogActions>
                  <Button onClick={handleEditClose}>Kapat</Button>
                  <Button type="submit" disabled={!isValid || !dirty}>
                    Güncelle
                  </Button>
                </DialogActions>
              </form>
            )}
          </Formik>
        </DialogContent>
      </Dialog>
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
                  label="Plaka"
                  variant="outlined"
                  onBlur={handleBlur}
                  value={values.plateNumber}
                  onChange={handleChange}
                  helperText={touched.plateNumber && errors.plateNumber}
                  error={Boolean(errors.plateNumber && touched.plateNumber)}
                  sx={{ mb: 3 }}
                />
                
                 <DialogActions>
                 <Grid container spacing={4} direction="row" justifyContent="center" alignItems="center">
                  <Grid item={true}>
                 
                    <Button
                      type="submit"
                      color="primary"
                      
                      variant="contained"
                      sx={{ my: 2 }}
                       disabled={!isValid || !dirty}
                    >
                      Kaydet
                    </Button>
                  </Grid>
                  <Grid item={true}>
                    <Button variant="contained" color="error" onClick={handleClose}>
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
