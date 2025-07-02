import React from 'react';
import { useTranslation } from 'react-i18next';
import { Box, Container, Button } from '@mui/material';

const TopBar = () => {
    const { i18n } = useTranslation();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    return (
        <Box sx={{ textAlign: 'center', backgroundColor: "#066767" }}>
            <Container style={{ display: 'flex', justifyContent: 'flex-end', padding: '10px' }}>
                <Button onClick={() => changeLanguage('en')} sx={{ color: '#fffffc', fontFamily: 'Bahnschrift' }}>
                    eng
                </Button>
                <Button onClick={() => changeLanguage('ru')} sx={{ color: '#fffffc', fontFamily: 'Bahnschrift' }}>
                    rus
                </Button>
            </Container>
        </Box>
    );
};

export default TopBar;
