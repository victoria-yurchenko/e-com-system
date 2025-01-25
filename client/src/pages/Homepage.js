import React from 'react';
import { Box, Typography, Container, Grid, Card, CardContent } from '@mui/material';
import { Star, CreditCard, Notifications } from '@mui/icons-material';

const HomePage = () => {
  return (
    <Container>
      <Box sx={{ textAlign: 'center', my: 4 }}>
        <Typography variant="h3" gutterBottom>
          Manage Your Subscriptions Easily and Effectively
        </Typography>
        <Typography variant="h5" color="textSecondary">
          A platform for managing subscriptions with flexible plans, seamless payment integrations, and a user-friendly interface.
        </Typography>
      </Box>

      {/* Секция преимуществ */}
      <Box sx={{ my: 4 }}>
        <Typography variant="h4" gutterBottom>
          Advantages
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={12} md={4}>
            <Card>
              <CardContent>
                <Star fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  Easy subscription setup.
                </Typography>
                <Typography variant="body1" color="textSecondary">
                  Flexible plans: Free, Premium, and Business.
                </Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={4}>
            <Card>
              <CardContent>
                <CreditCard fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  Безопасные платежи
                </Typography>
                <Typography variant="body1" color="textSecondary">
                  Secure payments using Stripe.
                </Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={4}>
            <Card>
              <CardContent>
                <Notifications fontSize="large" color="primary" />
                <Typography variant="h6" gutterBottom>
                  Уведомления
                </Typography>
                <Typography variant="body1" color="textSecondary">
                  Получайте напоминания об окончании срока подписки.
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </Box>

      {/* О нас */}
      <Box sx={{ my: 4 }}>
        <Typography variant="h4" gutterBottom>
          О нас
        </Typography>
        <Typography variant="body1" color="textSecondary">
          Наша система подписок предоставляет пользователям простой и интуитивный способ управления своими тарифами,
          платежами и уведомлениями. Мы объединяем мощную архитектуру и современные технологии, чтобы ваши подписки стали
          удобными и прозрачными.
        </Typography>
      </Box>
    </Container>
  );
};

export default HomePage;
