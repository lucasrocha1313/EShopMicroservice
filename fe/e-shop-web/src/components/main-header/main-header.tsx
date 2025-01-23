import classes from './main-header.module.css';
import Link from "next/link";

export default function MainHeader(){
    return (
        <header className={classes.header}>
            <Link className={classes.logo} href="/">E-Shopping Marketplace</Link>
            <div className={classes.navigation}>
                <nav>
                    <ul>
                        <li>
                            {/*TODO Replace to Link*/}
                            <a href="/products">Products</a>
                        </li>
                        <li>
                            <a href="/categories">Categories</a>
                        </li>
                        <li>
                            <a href="/login">Login</a>
                        </li>
                    </ul>
                </nav>
            </div>
        </header>
    );
}