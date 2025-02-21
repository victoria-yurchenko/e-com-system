import { configureStore } from '@reduxjs/toolkit';
import apiReducer from './apiSlice';
import authReducer from './authSlice';

export const store = configureStore({
  reducer: {
    api: apiReducer,
    auth: authReducer,
  },
});

export default store;
