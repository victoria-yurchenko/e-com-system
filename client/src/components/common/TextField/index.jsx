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
                color: "#066767",
                backgroundColor: $variant === 'secondary' ? 'secondary.main' : 'transparent',
                ...sx,
            }}
            slotProps={{
                input: {
                    sx: {
                        fontFamily: 'Bahnschrift',
                        border: '1px solid #066767',
                        height: '65px',
                        fontSize: '14pt',
                        borderRadius: '10px',
                        backgroundColor: '#fffffc',
                        transition: 'opacity 0.3s, border-color 0.3s',
                        opacity: 0.5,
                        ':hover': {
                            border: '2px solid #044646',
                            opacity: 1,
                        },
                        ':focus': {
                            border: '2px solid #044646',
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
