import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { selectBaseURL } from './apiSlice';
import axios from 'axios';
import apiClient from '../api/axiosInstance';

// User registration
export const registerUser = createAsyncThunk(
  'auth/register-user',
  async (userData, { rejectWithValue }) => {
    try {
      const response = await apiClient.post('/auth/register', userData);
      return response.data;
    } catch (error) {
      return rejectWithValue(error.response?.data?.message || "Registration failed");
    }
  }
);

// Send verification code
export const sendVerificationCode = createAsyncThunk(
  'auth/send-verify-account-code',
  async (identifierObj, { getState, rejectWithValue }) => {
    try {
      const baseURL = selectBaseURL(getState());
      const response = await axios.post(`${baseURL}/auth/send-verify-account-code`, identifierObj);
      return response.data;
    } catch (error) {
      return rejectWithValue(error.response?.data?.message || "Sending of verification code has failed");
    }
  }
);

// Verify code
export const verifyCode = createAsyncThunk(
  'auth/verify-account',
  async (verificationObj, { getState, rejectWithValue }) => {
    try {
      const baseURL = selectBaseURL(getState());
      console.log("base url: ", baseURL);
      const response = await axios.post(`${baseURL}/auth/verify-account`, verificationObj);
      return response.data;
    } catch (error) {
      return rejectWithValue(error.response?.data?.message || "Verification failed");
    }
  }
);

// User login
export const loginUser = createAsyncThunk(
  'auth/login-user',
  async (credentials, { getState, rejectWithValue }) => {
    try {
      const baseURL = selectBaseURL(getState());
      const response = await axios.post(`${baseURL}/auth/login`, credentials);
      return response.data;
    } catch (error) {
      return rejectWithValue(error.response?.data?.message || "Login failed");
    }
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('token') || null,
    loading: false,
    error: null,
  },
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    },
  },
  extraReducers: (builder) => {
    builder
      // Registration
      .addCase(registerUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(registerUser.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload.user;
      })
      .addCase(registerUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      })

      // Authorization
      .addCase(loginUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.token;
        state.user = action.payload.user;
        localStorage.setItem('token', action.payload.token);
        localStorage.setItem('user', JSON.stringify(action.payload.user));
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
