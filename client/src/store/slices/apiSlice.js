import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  baseURL: 'http://127.0.0.1:5000/api',  // TODO set to config file
};

const apiSlice = createSlice({
  name: 'api',
  initialState,
  reducers: {
    setBaseURL: (state, action) => {
      state.baseURL = action.payload;
    },
  },
});

export const { setBaseURL } = apiSlice.actions;
export const selectBaseURL = (state) => state.api.baseURL;
export default apiSlice.reducer;
