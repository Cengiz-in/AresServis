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
import { Formik } from "formik";
import * as Yup from "yup";
import { Grid } from "@mui/material";

const Corporation = () => {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch();
  const { vehicles } = useSelector((state) => state.vehicle);
  // form field validation schema
  const validationSchema = Yup.object().shape({
    corporationName: Yup.string().required("Bu alan boş geçilemez!"),
    Adress: Yup.string().required("Bu alan boş geçilemez!"),
    Email: Yup.string()
      .email("E-mail adresi geçersiz!")
      .required("Email alanı boş geçilemez!"),
    authorizedName: Yup.string().required("Bu alan boş geçilemez!"),
    authorizedPhone: Yup.string().required("Bu alan boş geçilemez!"),
    Phone: Yup.string().required("Bu alan boş geçilemez!"),
    Name: Yup.string().required("Bu alan boş geçilemez!"),
  });

  const addInitilaValue = {
    corporationName: "",
    Adress: "",
    Email: "",
    authorizedName: "",
    authorizedPhone: "",
    Name: "",
    Phone: "",
  };
  /* todo
  useEffect(() => {
    try {
      dispatch();
    } catch (e) {
      alert.error(e);
    }
  }, []);

  useEffect(() => {
    try {
      dispatch();
    } catch (e) {
      alert.error(e);
    }
  }, []);
  */

  const submitNewCorporation = async (values) => {
    //todo  await dispatch(values);

    handleClose();
  };
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const columns = [
    {
      field: "",
      headerName: "Kurum Adi",
      type: "string",
      editable: false,
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
        Kurum Ekle
      </Button>
      <DataGrid rows={vehicles} columns={columns} />

      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="edit-apartment"
      >
        <DialogTitle>Kurum Ekle</DialogTitle>
        <DialogContent>
          <Formik
            onSubmit={submitNewCorporation}
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
                  name="corporationName"
                  value={values.corporationName}
                  label="Kurum"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(
                    errors.corporationName && touched.corporationName
                  )}
                  sx={{ mb: 3, mt: 1 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="Adress"
                  value={values.Adress}
                  label="Kurumun Adresi"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(errors.Adress && touched.Adress)}
                  sx={{ mb: 3 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="authorizedPhone"
                  value={values.corporationPhone}
                  label="Kurum Telefon Numarası"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(
                    errors.corporationPhone && touched.corporationPhone
                  )}
                  sx={{ mb: 3 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="Email"
                  value={values.Email}
                  label="E-Mail"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(errors.Email && touched.Email)}
                  sx={{ mb: 3 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="authorizedName"
                  value={values.authorizedName}
                  label="Kurum Yetkilisi
                  "
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(
                    errors.authorizedName && touched.authorizedName
                  )}
                  sx={{ mb: 3 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="authorizedPhone"
                  value={values.authorizedPhone}
                  label="Kurum Yetkilisi Telefon Numarası"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(
                    errors.authorizedPhone && touched.authorizedPhone
                  )}
                  sx={{ mb: 3 }}
                />

                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="Name"
                  value={values.Name}
                  label="Kurucu / Mudur
                  "
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(errors.Name && touched.Name)}
                  sx={{ mb: 3 }}
                />
                <TextField
                  fullWidth
                  size="small"
                  type="text"
                  name="Phone"
                  value={values.Phone}
                  label="Kurucunun Telefon Numarasi"
                  variant="outlined"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  error={Boolean(errors.Phone && touched.Phone)}
                  sx={{ mb: 3 }}
                />

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
};

export default Corporation;
