"use client";

import { Link } from "@/i18n/routing";
import classes from './cart.module.css';
import { useSelector } from "react-redux";

export default function Cart() {

    //TODO: type the state
    const totalItems = useSelector((state: any) => state.cart.items.reduce((acc: number, item: any) => acc + item.quantity, 0));

    return (
        <Link className={classes.cart} href="/cart">🛒 <span>{totalItems}</span></Link>
    )
}