import React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux';
import { store } from '@store/store';
import App from './App';
import reportWebVitals from './reportWebVitals';
import './index.css';
import './i18n';
import { ThemeProvider } from '@mui/material/styles';
import { theme } from '@styles/themes/theme';

const container = document.getElementById('root');
const root = createRoot(container);

const render = () => {
  root.render(
    <React.StrictMode>
      <Provider store={store}>
        <ThemeProvider theme={theme}>
          <App />
        </ThemeProvider>
      </Provider>
    </React.StrictMode>
  );
};

render();

if (module.hot) {
  module.hot.accept('./App', () => {
    console.clear(); 
    render();
  });
}

reportWebVitals();
