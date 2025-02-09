import React from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { registerUser } from '../app/authSlice';
import { useNavigate } from 'react-router-dom';
import { TextField, Button, Box, Container, Typography, CircularProgress } from '@mui/material';
import { useTranslation } from 'react-i18next';

const RegisterPage = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { loading, error } = useSelector((state) => state.auth);
    const { t } = useTranslation();

    const onSubmit = async (data) => {
        try {
            await dispatch(registerUser(data)).unwrap();
            navigate('/auth/login');
        } catch (err) {
            console.error("Error during registration:", err);
        }
    };

    return (
        <Container maxWidth="sm">
            <Box sx={{ mt: 5, textAlign: 'center' }}>
                <Typography variant="h4" gutterBottom>
                    {t("auth.login")}
                </Typography>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <TextField
                        label="E-mail"
                        fullWidth
                        margin="normal"
                        {...register('email', { required: "Enter email", pattern: /^\S+@\S+\.\S+$/ })}
                        error={!!errors.email}
                        helperText={errors.email?.message}
                    />
                    <TextField
                        label="Password"
                        type="password"
                        fullWidth
                        margin="normal"
                        {...register('password', { required: "Enter password", minLength: 6 })}
                        error={!!errors.password}
                        helperText={errors.password?.message}
                    />
                    {loading ? <CircularProgress /> : null}
                    {error && <Typography color="error">{error}</Typography>}
                    <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
                        {t("auth.signup")}
                    </Button>
                </form>
            </Box>
        </Container>
    );
};

export default RegisterPage;
