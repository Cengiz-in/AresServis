import * as React from 'react';
import { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import Box from '@mui/material/Box';
import FormDialog from "app/components/FormDialog";
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import EditIcon from '@mui/icons-material/Edit';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import {
  DataGrid,
} from '@mui/x-data-grid';
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { getVehicles, putVehicles } from 'app/redux/actions/VehicleActions';




export default function Vehicle() {
  const dispatch = useDispatch();
  const { id } = useParams();
  const {vehicles} = useSelector((state) => state.vehicle);
  const [openEdit, setOpenEdit] = useState(false);

  const handleEditOpen = (event, cellValues) => {
    //console.log(cellValues)
    setOpenEdit(true);
  };

  const handleEditClose = () => {
    setOpenEdit(false);
  }

  const handleClick = (event, cellValues) => {
    console.log(cellValues.row);
  };
  const handleCellClick = (param, event) => {
    event.stopPropagation();
  };
  
  const handleRowClick = (param, event) => {
    event.stopPropagation();
  };

  useEffect (() => {
    try {
     dispatch(getVehicles(id));
    } catch (e) {
      alert.error(e);
    }
  }, []);

  const submitVehicleForm =  (id) => {
    console.log(id)
     dispatch(putVehicles(id));
  
  };
  
  const columns = [
    { field: "id", headerName: '#', type: 'number', editable: false },
    { field: "plateNumber", headerName: 'Plaka', type: 'string', editable: true },
    { field: "vehicleDriver", headerName: 'Sürücü bilgileri', type: 'string', width: 200, editable: false},
    {
      field: "device",
      headerName: 'cihaz',
      type: 'string',
      width: 220,
      editable: true,
    },  
    {
      field: "Actions",
      headerName: 'İşlemler',
      type: 'string',
      width: 220,
      field: "Actions",
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
      }
      
    },  
   
  ]
  
  

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
      
      <DataGrid
        rows={vehicles}
        columns={columns}
      />  
        
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
