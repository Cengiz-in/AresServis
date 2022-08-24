import * as React from 'react';
import { useEffect, useState } from 'react';
import Select from '@mui/material/Select';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
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
  resetVehicleErrors,
} from 'app/redux/actions/VehicleActions';
import { Formik } from 'formik';
import * as Yup from 'yup';

export default function Vehicle() {
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

  const deviceList = [
    { id: 1, value: 'ARES01' },
    { id: 3, value: 'ARES02' },
    { id: 4, value: 'ARES03' },
    { id: 5, value: 'ARES04' },
    { id: 6, value: 'ARES05' },
    { id: 7, value: 'ARES06' },
    { id: 8, value: 'ARES07' },
    { id: 10, value: 'ARES08' },
    { id: 11, value: 'ARES09' },
    { id: 13, value: 'ARES10' },
    { id: 14, value: 'ARES' },
  ];

  // form field validation schema
  const validationSchema = Yup.object().shape({
    plateNumber: Yup.string().required('Plaka boş geçilemez!'),
    deviceId: Yup.string().required('Device alanı boş geçilemez!'),
  });

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

  function getDevice(params) {
    return `${params.value.name || ''}`;
  }

  const columns = [
    { field: 'id', headerName: '#', type: 'number', editable: false },
    {
      field: 'plateNumber',
      headerName: 'Plaka',
      type: 'string',
      editable: true,
    },
    {
      field: 'vehicleDriver',
      headerName: 'Sürücü bilgileri',
      type: 'string',
      width: 200,
      editable: false,
    },
    {
      field: 'device',
      headerName: 'Cihaz',
      type: 'string',
      width: 220,
      editable: true,
      valueGetter: getDevice,
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
            Düzenle
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
                <Select
                  fullWidth
                  value={values.deviceId}
                  name="deviceId"
                  variant="outlined"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  autoWidth
                  label="Device"
                  error={Boolean(errors.deviceId && touched.deviceId)}
                  sx={{ mb: 3 }}
                >
                  {deviceList.map((element, idx) => {
                    return (
                      <MenuItem value={element.id} key={idx}>
                        {element.value}
                      </MenuItem>
                    );
                  })}
                </Select>
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
    </Box>
  );
}
