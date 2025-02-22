import { configureStore } from '@reduxjs/toolkit';
import apiReducer from './slices/apiSlice';
import authReducer from './slices/authSlice';

export const store = configureStore({
  reducer: {
    api: apiReducer,
    auth: authReducer,
  },
});

export default store;
