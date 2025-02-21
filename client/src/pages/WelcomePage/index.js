import { Button, Text } from '@components/common';
import BGImage from '@assets/images/login-bg.png';
import Box from '@mui/material/Box';
import { Stack } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

export const WelcomePage = () => {
  const { t } = useTranslation('translation');
  const navigate = useNavigate();

  const handleRedirectSignIn = () => {
    navigate('/sign-in');
  };

  const handleRedirectSignUp = () => {
    navigate('/sign-up');
  };

  return (
    <Box
      width="100%"
      minHeight="100vh"
      display="flex"
      flexDirection="column"
      justifyContent={{
        xs: 'end',
        md: 'center',
      }}
      alignItems="center"
      sx={{
        backgroundImage: `url(${BGImage})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
      }}
    >
      <Stack
        maxWidth={{ xs: '100%', md: '560px' }}
        width="100%"
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        padding="20px"
        bgcolor={{ xs: '#242424', md: 'transparent' }}
        sx={{
          borderTopLeftRadius: {
            xs: '86px',
            md: 0,
          },
          borderTopRightRadius: {
            xs: '86px',
            md: 0,
          },
        }}
      >
        <Text variant="h2" fontWeight={600} mb="10px" color="#fff" >
          {t('welcomePage.title')}
        </Text>
        <Text maxWidth="560px" align="center" mb="30px" color="#f7f7f8" opacity="50%">
          {t('welcomePage.subtitle')}
        </Text>

        <Stack width="100%" gap="30px">
          <Button fullWidth $variant="secondary" onClick={handleRedirectSignIn}>
            <Text fontWeight={600}>{t('general.signin')}</Text>
          </Button>

          <Button fullWidth $variant="primary" onClick={handleRedirectSignUp}>
            <Text fontWeight={600}>{t('general.signup')}</Text>
          </Button>
        </Stack>
      </Stack>
    </Box>
  );
};
