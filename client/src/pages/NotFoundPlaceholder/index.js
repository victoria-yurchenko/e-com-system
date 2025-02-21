import { Navigate } from 'react-router-dom';
import React from "react";

export const NotFoundPlaceholder = () => {
  return <Navigate to="/home" />;
};

export default NotFoundPlaceholder;