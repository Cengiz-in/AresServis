import * as React from "react";
import { useEffect, useState } from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { getVehicles, putVehicles,resetVehicleErrors } from "app/redux/actions/VehicleActions";
import { Formik } from "formik";
import * as Yup from "yup";


const initialValues = {
  plateNumber: "",
  device: 0,
};
// form field validation schema
const validationSchema = Yup.object().shape({
  plateNumber: Yup.string().required("Plaka boş geçilemez!"),
  device: Yup.string().required("device alanı boş geçilemez!"),
});

export default function Vehicle() {
  const dispatch = useDispatch();
  const { vehicles, vehicleSucceded, vehicleErrors } = useSelector((state) => state.vehicle);
  const [openEdit, setOpenEdit] = useState(false);
  const [editVehicleId, setEditVehicleId] = useState(null);
  //const { enqueueSnackbar, Snackbar } = useSnackbar();

  const handleEditOpen = (event, cellValues) => {
    
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

  const submitVehicleForm = async (values) => {
    console.log(values)
    dispatch(putVehicles(editVehicleId, values));
  };

  function getDevice(params) {
    return `${params.row.id || ''}`;
  }


  const columns = [
    { field: "id", headerName: "#", type: "number", editable: false },
    {
      field: "plateNumber",
      headerName: "Plaka",
      type: "string",
      editable: true,
    },
    {
      field: "vehicleDriver",
      headerName: "Sürücü bilgileri",
      type: "string",
      width: 200,
      editable: false,
    },
    {
      field: "device",
      headerName: "Cihaz",
      type: "string",
      width: 220,
      editable: true,
      valueGetter:getDevice
    },
    {
      field: "Actions",
      headerName: "İşlemler",
      type: "string",
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
        width: "100%",
        "& .actions": {
          color: "text.secondary",
        },
        "& .textPrimary": {
          color: "text.primary",
        },
      }}
    >
      <DataGrid rows={vehicles} columns={columns} />

      <Dialog open={openEdit}>
        <DialogTitle>Araç plaka değiştir</DialogTitle>
        <DialogContent>
          <Formik
            onSubmit={submitVehicleForm}
            initialValues={initialValues}
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
                  <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="device"
                  label="Device"
                  variant="outlined"
                  onBlur={handleBlur}
                  value={values.device}
                  onChange={handleChange}
                  helperText={touched.deviceId && errors.deviceId}
                  error={Boolean(errors.deviceId && touched.deviceId)}
                  sx={{ mb: 3 }}
                />
                <DialogActions>
                  <Button onClick={handleEditClose}>Kapat</Button>
                  <Button type="submit">Güncelle</Button>
                </DialogActions>
              </form>
            )}
          </Formik>
        </DialogContent>
      </Dialog>
    </Box>
  );
}
