import React from 'react';
import { useTranslation } from 'react-i18next';
import { Box, Typography, Container, Grid, Card, CardContent, Grid2 } from '@mui/material';
import { Star, CreditCard, Notifications } from '@mui/icons-material';

const HomePage = () => {
  const { t } = useTranslation();

  return (
    <Container>
      <Box sx={{ textAlign: 'center', my: 4 }}>
        <Typography variant="h3" gutterBottom>
          {t('welcome')}
        </Typography>
        <Typography variant="h3" color="textSecondary">
          {t('about.title')}
        </Typography>
        <Typography variant="h5" color="textSecondary">
          {t('about.description')}
        </Typography>
      </Box>

      <Box sx={{ my: 4 }}>
        <Typography variant="h4" gutterBottom>
          {t('advantages.title')}
        </Typography>
        <Grid2 container spacing={3}>
          <Grid2 item xs={12} md={4}>
            <Card>
              <CardContent>
                <Star fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  {t('advantages.convenience')}                </Typography>
                <Typography variant="body1" color="textSecondary">
                  {t('advantages.convenience_description')}                </Typography>
              </CardContent>
            </Card>
          </Grid2>
          <Grid2 item xs={12} md={4}>
            <Card>
              <CardContent>
                <CreditCard fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  {t('advantages.stripe')}
                </Typography>
                <Typography variant="body1" color="textSecondary">
                  {t('advantages.stripe_description')}                </Typography>
              </CardContent>
            </Card>
          </Grid2>
          <Grid2 item xs={12} md={4}>
            <Card>
              <CardContent>
                <Notifications fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  {t('advantages.support')}
                </Typography>
                <Typography variant="body1" color="textSecondary">
                  {t('advantages.support_description')}                </Typography>
              </CardContent>
            </Card>
          </Grid2>
        </Grid2>
      </Box>

      {/* О нас */}
      <Box sx={{ my: 4 }}>
        <Typography variant="h4" gutterBottom>
          {t('reviews.title')}
        </Typography>
        <Typography variant="h4" gutterBottom>
          {t('reviews.review1')}
        </Typography>
        <Typography variant="h4" gutterBottom>
          {t('reviews.review2')}
        </Typography>
      </Box>
    </Container>
  );
};

export default HomePage;
