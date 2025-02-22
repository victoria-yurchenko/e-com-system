import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, CircularProgress, Grid2, Link } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { TextField, Button, Text } from "@components/common";
import { registerUser } from '@store/slices/authSlice';

const RegisterPage = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();
    const { loading, error } = useSelector((state) => state.auth);
    const { t } = useTranslation();
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [formInputs, setFormInputs] = useState({
        email: '',
        password: '',
        passwordConfirm: '',
    });

    const handleChange = (e) => {
        setFormInputs((prev) => ({ ...prev, [e.target.name]: e.target.value }));
        console.log(e.target.name, e.target.value); // Выводит актуальное значение
    };
    
    const doesPasswordsMatch = () => {
        return formInputs.password === formInputs.passwordConfirm;
    };

    const onSubmit = async (data) => {
        if (!doesPasswordsMatch()) {
            alert("Passwords do not match");
            return;
        }
        //TODO remove console logs for production
        try {
            await dispatch(registerUser({ email: data.email, password: data.password })).unwrap();
            console.log("Registration successful:", result);
            navigate("/sign-in");
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
                                type="email"
                                margin="normal"
                                name="email"
                                onChange={handleChange}
                                error={!!errors.email}
                                helperText={errors.email?.message}
                            />
                            <TextField
                                label={t('general.password')}
                                type="password"
                                onChange={handleChange}
                                name="password"
                                fullWidth
                                margin="normal"
                                error={!!errors.password}
                                helperText={errors.password?.message}
                            />

                            <TextField
                                label={t('general.confirmPassword')}
                                type="password"
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                                name="passwordConfirm"
                                error={!!errors.passwordConfirm}
                                helperText={errors.passwordConfirm?.message}
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
