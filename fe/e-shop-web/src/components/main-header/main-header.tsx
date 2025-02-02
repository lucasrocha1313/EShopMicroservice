import classes from './main-header.module.css';
import Link from "next/link";
import {getTranslations, setRequestLocale} from "next-intl/server";
// import LanguageSwitcher from '../language-switcher';

type Props = {
    params: {locale: string};
};

export default async function MainHeader({params}: Props){
    // Enable static rendering

    const {locale} = await params;

    setRequestLocale(locale);
    const t = await getTranslations("MainHeader");
    
    const newLocale = locale === "en" ? "pt-br" : "en";

    

    return (
        <header className={classes.header}>
            <Link className={classes.logo} href="/">E-Shopping Marketplace</Link>
            <div className={classes.navigation}>
                <nav>
                    <ul>
                        <li>
                            <Link href="/products">{t('products')}</Link>
                        </li>
                        <li>
                            <Link href="/categories">{t('categories')}</Link>
                        </li>
                        <li>
                            <Link href="/login">{t('login')}</Link>
                        </li>
                        <li>
                            {/* <LanguageSwitcher locale={locale} newLocale={newLocale} switchTo={t("switchTo", { locale: newLocale.toUpperCase() })} /> */}
                            <Link href={`/${newLocale}`}>{t("switchTo", { locale: newLocale.toUpperCase() })}</Link>
                        </li>
                    </ul>
                </nav>
            </div>
        </header>
    );
}