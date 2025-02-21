import React from 'react';
import { TextField as MuiTextField } from '@mui/material';

const TextField = ({ sx, ...otherProps }) => {
    return (
        <MuiTextField
            {...otherProps}
            sx={{
                fontFamily: 'Bahnschrift',
                width: '100%',
                minWidth: '200px',
                ...sx,
            }}
            slotProps={{
                input: {
                    sx: {
                        fontFamily: 'Bahnschrift',
                        minHeight: '5vh',
                        fontSize: '14pt',
                    },
                },
                label: {
                    sx: {
                        fontFamily: 'Bahnschrift',
                    },
                },
            }}
        />
    );
};

export default TextField;