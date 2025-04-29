import React from 'react';
import { Box } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { Button, Text } from "@components/common";

const VerificationStep1 = ({ onVerificationMethodChanged }) => {
    const { t } = useTranslation();

    const handleVerificationMethodChange = (methodName) => {
        const newMethod = {
            methodChoosed: true,
            methods: {
                email: methodName === 'email',
            }
        };

        if (onVerificationMethodChanged) {
            onVerificationMethodChanged(newMethod);
        }
    };

    return (
        <Box>
            <Text variant="h6" gutterBottom sx={{
                fontWeight: '600',
                opacity: '60%',
                mb: 1
            }}>
                {t('verification.chooseMethod')}
            </Text>
            <Button
                sx={{ marginBottom: 1 }}
                type="button"
                variant="outlined"
                color="primary"
                fullWidth
                onClick={() => handleVerificationMethodChange('email')}
            >
                <Text>
                    {t("verification.methods.email.title")}
                </Text>
                <Box style={{ marginLeft: 'auto' }}>➜</Box>
            </Button>
        </Box>
    );
};

export default VerificationStep1;