import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, CircularProgress, Grid, Link, Grid2 } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { TextField, Button, Text } from "@components/common";
import { verifyAccount } from '@store/slices/authSlice';

const VerificationStep2 = ({ verificationMethod }) => {
    const { t } = useTranslation();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    // TODO setup verify-account
    const [formInputs, setFormInputs] = useState({ identifier: '' });
    const [serverError, setServerError] = useState("");

    const handleChange = (e) => {
        setFormInputs((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const onSubmit = async (e) => {
        e.preventDefault();
        setServerError("");

        try {
            console.log("Form inputs:", formInputs);
            await dispatch(verifyAccount({ identifier: formInputs.identifier })).unwrap();
            console.log("Verification successful:", result);
            navigate("/sign-up");
        } catch (err) {
            console.error("Verification error:", err);
            const errorMessage = err?.message || "An unknown error occurred. Please try again.";
            setServerError(errorMessage);
        }
    };

    //TODO make title Welcome back in one line (inline)

    return (
        <Box>
            <Box sx={{ width: '100%' }}>
                {verificationMethod?.methods?.email && (
                    <Box sx={{ width: '100%', textAlign: 'center' }}>
                        <Text variant="h6" gutterBottom sx={{
                            fontWeight: '600',
                            opacity: '60%',
                            mb: 1,
                        }}>
                            {t("verification.methods.email.description")}
                        </Text>
                        <form onSubmit={onSubmit} style={{ width: '100%' }}>
                            <TextField
                                variant="outlined"
                                color="primary"
                                fullWidth
                                name="identifier"
                                value={formInputs.identifier}
                                onChange={handleChange}
                                placeholder={t("general.email") || "Email"}
                            />

                            {/* TODO remove after debug */}
                            {serverError && (
                                <Typography color="error" sx={{ mt: 1 }}>
                                    {serverError}
                                </Typography>
                            )}
                            {/* TODO remove after debug */}

                            <Button sx={{ mt: 2 }} type="submit" variant="contained" color="primary" fullWidth>
                                <Text>{t("general.sendCode")}</Text>
                            </Button>
                        </form>
                    </Box>
                )}
            </Box>
            <Box sx={{ mt: 3 }}>
                <Link href="/verification" underline="hover" sx={{ display: 'inline-block', mt: 1 }}>
                    <Text>⬅ {t("general.goBack")}</Text>
                </Link>
            </Box>
        </Box>
    );
};

export default VerificationStep2;