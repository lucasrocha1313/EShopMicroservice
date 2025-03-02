import classes from './main-header.module.css';
import {getTranslations, setRequestLocale} from "next-intl/server";
import LanguageSwitcher from '../language-switcher';
import Menu from './menu';
import { Link } from '@/i18n/routing';
import Cart from '../cart/cart';

type Props = {
    params: {locale: string};
};

export default async function MainHeader({params}: Props){
    // Enable static rendering
    const {locale} = await params;

    setRequestLocale(locale);

    // getTranslations("MainHeader") is a function that returns the translations for the MainHeader component
    // I need it on server side due the locale switcher
    const t = await getTranslations("MainHeader"); 
    
    const newLocale = locale === "en" ? "pt-br" : "en";
       

    return (
        <header className={classes.header}>
            <Link className={classes.logo} href="/">E-Shopping Marketplace</Link>
            <div className={classes.headerContent}>
                <Menu>
                    <li>
                        <Link href="/">{t('home')}</Link>
                    </li>
                    <li>
                        <Link href="/products">{t('products')}</Link>
                    </li>
                    <li>
                        <Link href="/cart">{t('cart')} <span>({0})</span></Link>
                    </li>
                    <li>
                        <Link href="/order">{t('order')}</Link>
                    </li>  
                    <li>
                        <Link href="/contact">{t('contact')}</Link>
                    </li>
                    <li>
                        <div className={classes.mobileBottom}>
                            <LanguageSwitcher locale={locale} newLocale={newLocale} switchTo={t("switchTo", { locale: newLocale.toUpperCase() })} />
                            <Link href="/login">{t('login')}</Link>
                        </div>
                    </li>
                </Menu>                
                <div className={classes.rightContent}>
                    {/* TODO - Search */}
                    <input type="text" placeholder='Search' />
                    <LanguageSwitcher locale={locale} newLocale={newLocale} switchTo={t("switchTo", { locale: newLocale.toUpperCase() })} />
                    <Link href="/login">{t('login')}</Link>
                    <Cart />
                </div>
                {/* TODO - create component to search         */}
                <input className={classes.searchMobile} type="text" placeholder='Search' />
            </div>
        </header>
    );
}