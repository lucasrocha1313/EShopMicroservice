import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { useDispatch } from "react-redux";

type CartItem = {
    id: number;
    name: string;
    price: number;
    quantity: number;
};

const cartSlice = createSlice({
    name: "cart",
    initialState: {
        items: [] as CartItem[],
    },
    reducers: {        
        addItem(state, action: PayloadAction<CartItem>) {
            const item = state.items.find((i) => i.id === action.payload.id);
            if (item) {
                item.quantity += 1;
            } else {
                state.items.push({ ...action.payload, quantity: 1 });
            }
        },
        removeItem(state, action: PayloadAction<number>) {
            state.items = state.items.filter((item) => item.id !== action.payload);
        },
    },
})

export const cartActions = cartSlice.actions;

export const useCart = () => (
    {
        dispatch: useDispatch()
    }
)

export default cartSlice