import { configureStore } from "@reduxjs/toolkit";
import cartSlice from "./slices";

const store = configureStore({
    reducer: { cart: cartSlice.reducer },
});


export default store;