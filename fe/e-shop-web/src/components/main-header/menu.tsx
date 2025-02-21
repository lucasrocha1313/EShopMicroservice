"use client";

import { Link } from '@/i18n/routing';
import classes from './menu.module.css';
import { useState } from "react";
import { useTranslations } from 'next-intl';
// import { useState } from "react";

type Props = {
    children: React.ReactNode;
  };

export default function Menu({ children}: Props) {

    const t = useTranslations('MainHeader');

    // State for mobile menu
    const [menuOpen, setMenuOpen] = useState(false); 

    return (
        <>

            <button className={classes.menuToggle} >                
                {!menuOpen ? 
                    <label onClick={() => setMenuOpen(!menuOpen)}>☰</label> : <label>Menu</label>}
                {menuOpen ? 
                    <label onClick={() => setMenuOpen(false)}>✖</label> :
                    <Link className={classes.cartMobile} href="/cart">{t('cart')}<span>0</span></Link>}
            </button>
            <div className={`${classes.navigation} ${menuOpen ? classes.open : ''}`}>
                <nav className={classes.menu}>
                    <ul>
                        {children}                         
                    </ul>
                </nav>
            </div>
        </>
        
    )
}