import React from 'react';
import { Button as MuiButton } from '@mui/material';

const Button = ({
  $variant = 'primary', 
  children,
  sx={},
  ...otherProps
}) => {
  return (
    <MuiButton
      variant={$variant === 'primary' ? 'contained' : 'outlined'}
      {...otherProps}
      sx={{
        textTransform: 'none',
        fontFamily: 'Archivo',
        padding: '16px',
        fontSize: '16px',
        fontWeight: 600,
        height: '65px',
        backgroundColor:
          $variant === 'secondary' ? 'secondary.main' : undefined,
        border: $variant === 'secondary' ? '2px solid #5D6AD1' : undefined,
        borderRadius: '10px',
        ':hover': {
          backgroundColor: $variant === 'secondary' ? 'bg.light' : undefined,
          borderWidth: '2px',
        },
        ...sx,
      }}
    >
      {children}
    </MuiButton>
  );
};

export default Button;