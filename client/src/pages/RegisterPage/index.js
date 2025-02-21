import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, CircularProgress, Grid2, Link } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { TextField, Button, Text } from "@components/common";

const RegisterPage = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { loading, error } = useSelector((state) => state.auth);
    const { t } = useTranslation();
    const [formInputs, setFormInputs] = useState({
        email: '',
        password: '',
        passwordConfirm: '',
    });


    const handleChange = (e) => {
        setFormInputs((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    }

    const onSubmit = async (data) => {
        try {
            // await dispatch(registerUser(data)).unwrap();
            navigate('/auth/login');
        } catch (err) {
            console.error("Error during registration:", err);
        }
    };

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
                            {t('signUpPage.title')}
                        </Text>
                        <Text variant="h6" gutterBottom sx={{
                            fontWeight: 'bold',
                            letterSpacing: '-0.04em',
                            opacity: '50%'
                        }}>
                            {t('signUpPage.subtitle')}
                        </Text>
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <TextField
                                label={t('general.email')}
                                fullWidth
                                margin="normal"
                                {...register('email', { required: "Enter email", pattern: /^\S+@\S+\.\S+$/ })}
                                error={!!errors.email}
                                helperText={errors.email?.message}
                            />
                            <TextField
                                label={t('general.password')}
                                type="password"
                                fullWidth
                                margin="normal"
                                {...register('password', { required: "Enter password" })}
                                error={!!errors.password}
                                helperText={errors.password?.message}
                            />

                            <TextField
                                label={t('general.confirmPassword')}
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
                            <Button type="submit" variant="contained" color="primary" fullWidth>
                                <Text>
                                    {t("general.signup")}
                                </Text>
                            </Button>
                            <Link
                                href="/sign-in"
                                underline="hover"
                                sx={{
                                    mt: 2,
                                    display: 'inline-block',
                                }}
                            >
                                <Text>
                                    {t("signUpPage.hasAccount")}
                                </Text>
                            </Link>
                        </form>
                    </Box>
                </Grid2>
            </Grid2>
        </Container>
    );
};

export default RegisterPage;
