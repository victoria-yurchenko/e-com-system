import React from 'react';
import Typography from '@mui/material/Typography';
import { useTranslation } from 'react-i18next';

const Text = ({ children, ...otherProps }) => {
  const { i18n } = useTranslation();

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