import React from 'react';
import { useTranslation } from 'react-i18next';
import { Box, Container, Card, CardContent, Grid2 } from '@mui/material';
import { Star, CreditCard, Notifications } from '@mui/icons-material';
import BusinessManThumbsUp from '@assets/images/business_man_thumbs_up.jpg';
import HappyBusinessWoman from '@assets/images/happy_business_woman.jpg';
import { Text, ImageCard } from '@components/common'

const HomePage = () => {
  const { t } = useTranslation();

  return (
    <Grid2>
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
              <Card sx={{width: '100%'}}>
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
          <ImageCard text={t("reviews.review1")} imgSrc={BusinessManThumbsUp} imgAlt="Business Man Thumbs Up" imgHeight="350" isImgFirst={false}/>
          <ImageCard text={t("reviews.review2")} imgSrc={HappyBusinessWoman} imgAlt="Happy business woman" imgHeight="350" isImgFirst={true}/>         
        </Box>
      </Container>
    </Grid2>
  );
};

export default HomePage;
