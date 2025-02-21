import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, CircularProgress, Grid, Button, Card, CardContent } from '@mui/material';
import { useTranslation } from 'react-i18next';
import { Text } from '@components/common';
// import { fetchSubscriptions, subscribeToPlan } from '@store/subscriptionSlice'; // Предположим, что у вас есть срез для подписок

const SubscriptionsPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { subscriptions, loading, error } = {subscriptions: [], loading: false, error: ''};
  const [selectedPlan, setSelectedPlan] = useState(null);

  // Загрузка подписок при монтировании компонента
  useEffect(() => {
    // dispatch(fetchSubscriptions());
  }, [dispatch]);

  // Оформление подписки
  const handleSubscribe = async (planId) => {
    // try {
    //   await dispatch(subscribeToPlan(planId)).unwrap();
    //   navigate('/subscriptions/success'); // Перенаправление на страницу успешного оформления
    // } catch (err) {
    //   console.error("Ошибка при оформлении подписки:", err);
    // }
  };

  return (
    <Container maxWidth="md">
      <Grid
        container
        justifyContent="center"
        alignItems="center"
        sx={{ minHeight: '100vh', py: 4 }}
      >
        <Grid item xs={12}>
          <Box sx={{ textAlign: 'center' }}>
            <Text variant="h4" sx={{
              fontSize: '7vh',
              fontWeight: 'bold',
              letterSpacing: '-0.04em'
            }}>
              {t('subscriptions.title')}
            </Text>
            <Text variant="h6" gutterBottom sx={{
              fontWeight: 'bold',
              letterSpacing: '-0.04em',
              opacity: '50%'
            }}>
              {t('subscriptions.subtitle')}
            </Text>

            {loading ? (
              <CircularProgress />
            ) : error ? (
              <Typography color="error">{error}</Typography>
            ) : (
              <Grid container spacing={4} justifyContent="center">
                {subscriptions.map((subscription) => (
                  <Grid item key={subscription.id} xs={12} sm={6} md={4}>
                    <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
                      <CardContent sx={{ flexGrow: 1 }}>
                        <Typography variant="h5" gutterBottom>
                          {subscription.name}
                        </Typography>
                        <Typography variant="body1" gutterBottom>
                          {subscription.description}
                        </Typography>
                        <Typography variant="h6" gutterBottom>
                          {t('subscriptions.price', { price: subscription.price })}
                        </Typography>
                        <Typography variant="body2" gutterBottom>
                          {t('subscriptions.duration', { duration: subscription.duration })}
                        </Typography>
                      </CardContent>
                      <Box sx={{ p: 2 }}>
                        <Button
                          variant="contained"
                          color="primary"
                          fullWidth
                          // onClick={() => handleSubscribe(subscription.id)}
                        >
                          {t('subscriptions.subscribe')}
                        </Button>
                      </Box>
                    </Card>
                  </Grid>
                ))}
              </Grid>
            )}
          </Box>
        </Grid>
      </Grid>
    </Container>
  );
};

export default SubscriptionsPage;