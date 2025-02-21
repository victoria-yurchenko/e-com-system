import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { loginUser } from '@store/authSlice';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, CircularProgress, Grid2, Link } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { TextField, Button, Text } from "@components/common";

const LoginPage = () => {
  const { register, handleSubmit, formState: { errors } } = useForm();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { loading, error } = useSelector((state) => state.auth);
  const { t } = useTranslation();
  const [formInputs, setFormInputs] = useState({
    email: '',
    password: ''
  });

  useState(() => {
console.log(formInputs);

  });

  const onSubmit = async (data) => {
    try {
      await dispatch(loginUser(data)).unwrap();
      navigate('/profile');
    } catch (err) {
      console.error("Error during login:", err);
    }
  };

  const handleChange = (e) => {
    setFormInputs((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  return (
    <Container maxWidth="sm">
      <Grid2
        container
        justifyContent="center"
        alignItems="center"
        sx={{ minHeight: '100vh' }}
      >
        <Grid2 item xs={12}>
          <Box sx={{ textAlign: 'center' }}>
            <Text variant="h4" sx={{
              fontSize: '7vh',
              fontWeight: 'bold',
              letterSpacing: '-0.04em'
            }}>
              {t('signInPage.welcomeBack')}
            </Text>
            <Text variant="h6" gutterBottom sx={{
              fontWeight: 'bold',
              letterSpacing: '-0.04em'
            }}>
              {t('signInPage.subtitle')}
            </Text>
            <form onSubmit={handleSubmit(onSubmit)}>
              <TextField
                label={t('general.email')}
                fullWidth
                margin="normal"
                {...register('email', { required: "Enter email", pattern: /^\S+@\S+\.\S+$/ })}
                error={!!errors.email}
                helperText={errors.email?.message}
                onChange={handleChange}
              />
              <TextField
                label={t('general.password')}
                type="password"
                fullWidth
                margin="normal"
                {...register('password', { required: "Enter password" })}
                error={!!errors.password}
                helperText={errors.password?.message}
                onChange={handleChange}
              />
              {loading ? <CircularProgress /> : null}
              {error && <Typography color="error">{error}</Typography>}
              <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
                <Text>
                  {t("general.signin")}
                </Text>
              </Button>
              <Link
                href="/sign-up"
                underline="hover"
                sx={{
                  mt: 2,
                  display: 'inline-block',
                }}
              >
                <Text>
                  {t("general.signup")}
                </Text>
              </Link>
            </form>
          </Box>
        </Grid2>
      </Grid2>
    </Container>
  );
};

export default LoginPage;