import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { TopBar } from '@components/common';
import { Grid2 } from '@mui/material';
import { RegisterPage, LoginPage, HomePage, WelcomePage, NotFoundPlaceholder, SubscriptionsPage, VerificationPage } from "@pages";

const App = () => {
  return (
    <Grid2  sx={{ bgcolor: '#fffffc' }}>
      <TopBar />
      <BrowserRouter>
        <Routes>
          <Route path="home" element={<HomePage />} />
          <Route path="welcome" element={<WelcomePage />} />
          <Route path="sign-in" element={<LoginPage />} />
          <Route path="sign-up" element={<RegisterPage />} />
          <Route path="verification" element={<VerificationPage />} />
          <Route path="subscriptions" element={<SubscriptionsPage />} />
          <Route path="*" element={<NotFoundPlaceholder />} />
        </Routes>
      </BrowserRouter>
    </Grid2>
  );
};

export default App;
