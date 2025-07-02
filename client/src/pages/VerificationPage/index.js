
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Link, Grid2 } from '@mui/material';
import { useTranslation } from 'react-i18next';
import React, { useState, useCallback } from 'react';
import { Button, Text, TextField } from "@components/common";
import { sendVerificationCode, verifyCode } from '@store/slices/authSlice';

const VerificationPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { t } = useTranslation();

    const [step, setStep] = useState(1);
    const [identifier, setIdentifier] = useState('');
    const [code, setCode] = useState('');
    const [error, setError] = useState('');

    const handleNext = useCallback(() => setStep(2), []);
    const handleSend = useCallback(async () => {
        setError('');
        try {
            await dispatch(sendVerificationCode({ identifier })).unwrap();
            
            setStep(3);
        } catch (e) {
            setError(e.message || t('verification.errorSend'));
        }
    }, [dispatch, identifier, t]);
    const handleVerify = useCallback(async () => {
        setError('');
        try {
            await dispatch(verifyCode({ identifier, code })).unwrap();
            navigate('/sign-up');
        } catch (e) {
            setError(e.message || t('verification.errorSend'))
        }
    }, [dispatch, identifier, code, navigate, t]);

    // steps config
    const steps = [
        {
            // TODO set translation in the fields
            title: 'Choose verification method',
            content:
                <>
                    <Button variant="outlined" color="primary" fullWidth onClick={handleNext} sx={{ marginBottom: 1 }}>
                        <Text>{t("verification.methods.email.title")}</Text>
                        <Box ml="auto">➜</Box>
                    </Button>
                    <Link href="/sign-in" underline="hover" sx={{ display: 'inline-block', mt: 1 }}>
                        <Text>{t("signUpPage.hasAccount")}</Text>
                    </Link>
                </>
        },
        {
            title: t("verification.methods.email.description"),
            content:
                <>
                    <TextField
                        label={t('general.email')}
                        variant="outlined"
                        color="primary"
                        fullWidth
                        value={identifier}
                        onChange={e => setIdentifier(e.target.value)}
                        placeholder={t("general.email") || "Email"}
                    />
                    {error && <Text color="error">{error}</Text>}
                    <Button sx={{ mt: 2 }} type="submit" variant="contained" color="primary" fullWidth onClick={handleSend}>
                        <Text>{t("general.sendCode")}</Text>
                    </Button>
                    <Box component="span" sx={{ display: 'inline-flex', gap: 1, mt: 1 }}>
                        <Link href="/sign-in" underline="hover">
                            <Text>{t("signUpPage.hasAccount")}</Text>
                        </Link>
                        <Link href="/verification" underline="hover">
                            <Text>{t("general.goBack")}</Text>
                        </Link>
                    </Box>
                </>
        },
        {
            title: 'Please enter your verification code',
            content:
                <>
                    <TextField
                        label="Verification Code"
                        variant="outlined"
                        color="primary"
                        fullWidth
                        value=""
                        onChange={e => setCode(e.target.value)}
                        placeholder="Verification Code"
                    />
                    <Button sx={{ mt: 2 }} type="submit" variant="contained" color="primary" fullWidth onClick={handleVerify}>
                        <Text>Submit Code</Text>
                    </Button>
                    <Box component="span" sx={{ display: 'inline-flex', gap: 1, mt: 1 }}>
                        <Link href="/sign-in" underline="hover">
                            <Text>{t("signUpPage.hasAccount")}</Text>
                        </Link>
                        <Link href="/verification" underline="hover">
                            <Text>{t("general.goBack")}</Text>
                        </Link>
                    </Box>
                </>
        }
    ];

    const { title, content, field, action } = steps[step - 1];

    return (
        <Container maxWidth="sm">
            <Grid2 container justifyContent="center" alignItems="center" sx={{ minHeight: '100vh' }}>
                <Grid2 item xs={12} sx={{ width: '100%' }}>
                    <Box sx={{
                        width: '100%',
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        textAlign: 'center'
                    }}>
                        <Text variant="h6" gutterBottom sx={{
                            fontWeight: '600',
                            opacity: '60%',
                            mb: 1,
                        }}>
                            {title}
                        </Text>

                        {content || (
                            <TextField
                                label={field.label}
                                type={field.type}
                                fullWidth
                                value={field.value}
                                onChange={field.onChange}
                            />
                        )}

                        {error && <Text color="error">{error}</Text>}

                        {action && (
                            <Button variant="contained" fullWidth onClick={action.handler}>
                                <Text>
                                    {action.label}
                                </Text>
                            </Button>
                        )}
                    </Box>
                </Grid2>
            </Grid2>
        </Container>
    );
};

export default VerificationPage;