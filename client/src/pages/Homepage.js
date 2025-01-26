import React from 'react';
import { useTranslation } from 'react-i18next';
import { Box, Typography, Container, Card, CardContent, Grid2 } from '@mui/material';
import { Star, CreditCard, Notifications } from '@mui/icons-material';
import TopNavBar from '../components/common/TopNavBar';
import Text from '../components/common/Text';

const HomePage = () => {
  const { t } = useTranslation();

  return (
    <>
      <TopNavBar />
      <Container>
        <Box sx={{ textAlign: 'center', my: 4 }}>
          <Text variant="h3">
            {t('welcome')}
          </Text>
          <Text variant="h3" color="textSecondary">
            {t('about.title')}
          </Text>
          <Text variant="h5" color="textSecondary">
            {t('about.description')}
          </Text>
        </Box>
        <Text variant="h4" gutterBottom>
          {t('advantages.title')}
        </Text>
        <Box sx={{ gris: 4 }}>

          <Grid2 container spacing={3}>
            <Grid2 item xs={12} md={4}>
              <Card>
                <CardContent>
                  <Star fontSize="large" color="primary" />
                  <Text variant="h6" gutterBottom>
                    {t('advantages.convenience')}
                  </Text>
                  <Text variant="body1" color="textSecondary">
                    {t('advantages.convenience_description')}
                  </Text>
                </CardContent>
              </Card>
            </Grid2>
            <Grid2 item xs={12} md={4}>
              <Card>
                <CardContent>
                  <CreditCard fontSize="large" color="primary" />
                  <Text variant="h6" gutterBottom>
                    {t('advantages.stripe')}
                  </Text>
                  <Text variant="body1" color="textSecondary">
                    {t('advantages.stripe_description')}
                  </Text>
                </CardContent>
              </Card>
            </Grid2>
            <Grid2 item xs={12} md={4}>
              <Card>
                <CardContent>
                  <Notifications fontSize="large" color="primary" />
                  <Text variant="h6" gutterBottom>
                    {t('advantages.support')}
                  </Text>
                  <Text variant="body1" color="textSecondary">
                    {t('advantages.support_description')}
                  </Text>
                </CardContent>
              </Card>
            </Grid2>
          </Grid2>
        </Box>

        <Box sx={{ my: 4 }}>
          <Text variant="h4" gutterBottom>
            {t('reviews.title')}
          </Text>
          <Text variant="h5" gutterBottom>
            {t('reviews.review1')}
          </Text>
          <Text variant="h5" gutterBottom>
            {t('reviews.review2')}
          </Text>
        </Box>
      </Container>
    </>
  );
};

export default HomePage;
