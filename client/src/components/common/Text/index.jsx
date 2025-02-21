import React from 'react';
import Typography from '@mui/material/Typography';

const Text = ({ children, ...otherProps }) => {
  return (
    <Typography
      {...otherProps}
      style={{
        fontFamily: 'Bahnschrift',
      }}
    >
      {children}
    </Typography>
  );
};

export default Text;