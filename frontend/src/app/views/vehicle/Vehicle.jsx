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
import { getVehicles, putVehicles } from 'app/redux/actions/VehicleActions';

export default function Vehicle() {
  const dispatch = useDispatch();
  const { vehicles } = useSelector((state) => state.vehicle);
  const [openEdit, setOpenEdit] = useState(false);
  const [editVehicleItem, setEditVehicleItem] = useState(null);

  const handleEditOpen = (event, cellValues) => {
    setEditVehicleItem(cellValues.row);
    setOpenEdit(true);
  };

  const handleEditClose = () => {
    setEditVehicleItem(null);
    setOpenEdit(false);
  };

  useEffect(() => {
    try {
      dispatch(getVehicles());
    } catch (e) {
      alert.error(e);
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const submitVehicleForm = () => {
    dispatch(putVehicles(editVehicleItem));
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
      valueGetter: (params) => {
        return params.row.device.name;
      },
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

      <Dialog open={openEdit} onClose={handleEditClose}>
        <DialogTitle>Araç plaka değiştir</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="outlined-error-helper-text"
            name="plateNumber"
            label="Plaka"
            type="text"
            fullWidth
            variant="standard"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleEditClose}>İptal</Button>
          <Button onClick={submitVehicleForm}>Güncelle</Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
