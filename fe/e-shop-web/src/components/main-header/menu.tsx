"use client";

import classes from './menu.module.css';
import { useState } from "react";
// import { useState } from "react";

type Props = {
    children: React.ReactNode;
  };

export default function Menu({ children}: Props) {

    // State for mobile menu
    const [menuOpen, setMenuOpen] = useState(false); 

    return (
        <>

            <button className={classes.menuToggle} onClick={() => setMenuOpen(!menuOpen)}>
                ☰
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