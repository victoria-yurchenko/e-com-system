import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Link, Grid2 } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { Text } from "@components/common";
import { registerUser } from '@store/slices/authSlice';
import VerificationStep1 from './VerificationStep1';
import VerificationStep2 from './VerificationStep2';
import React, { useEffect, useState } from 'react';

const VerificationPage = () => {
    const { t } = useTranslation();
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [verificationMethod, setVerificationMethod] = useState({
        methodChoosed: false,
        methods: {
            email: false,
        }
    });

    useEffect(() => {
        console.log(verificationMethod);
    }, [verificationMethod]);

    const [formInputs, setFormInputs] = useState({
        identifier: '',
    });

    const handleChange = (e) => {
        setFormInputs((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const onSubmit = async () => {
        try {
            const result = await dispatch(registerUser(formInputs.identifier)).unwrap();
            console.log("Code was sent ", result);
            navigate("/sign-up");
        } catch (err) {
            let errorMessage = err?.message || "An unknown error occurred. Please try again.";
            alert(errorMessage);
        }
    };

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
                        {!verificationMethod.methodChoosed ? (
                            <VerificationStep1 onVerificationMethodChanged={setVerificationMethod} />
                        ) : (
                            <VerificationStep2 verificationMethod={verificationMethod} />
                        )}
                        <Link href="/sign-in" underline="hover" sx={{ display: 'inline-block', mt: 1 }}>
                            <Text>
                                {t("signUpPage.hasAccount")}
                            </Text>
                        </Link>
                    </Box>
                </Grid2>
            </Grid2>
        </Container>
    );
};

export default VerificationPage;