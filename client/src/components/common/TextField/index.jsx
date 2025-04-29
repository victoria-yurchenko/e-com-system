import React from 'react';
import { TextField as MuiTextField } from '@mui/material';

const TextField = ({ sx, $variant, $type, ...otherProps }) => {
    return (
        <MuiTextField
            {...otherProps}
            variant="outlined"
            type={$type}
            sx={{
                fontFamily: 'Bahnschrift',
                width: '100%',
                minWidth: '200px',
                textTransform: 'none',
                fontSize: '16px',
                fontWeight: 600,
                color: "#5D6AD1",
                backgroundColor: $variant === 'secondary' ? 'secondary.main' : 'transparent',
                ...sx,
            }}
            slotProps={{
                input: {
                    sx: {
                        fontFamily: 'Bahnschrift',
                        border: '1px solid #5D6AD1',
                        height: '65px',
                        fontSize: '14pt',
                        borderRadius: '10px',
                        backgroundColor: 'transparent',
                        transition: 'opacity 0.3s, border-color 0.3s',
                        opacity: 0.5,
                        ':hover': {
                            borderColor: '#3B4CCA',
                            opacity: 1,
                        },
                        ':focus': {
                            borderColor: '#2A3BB1',
                            opacity: 1,
                        }
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
