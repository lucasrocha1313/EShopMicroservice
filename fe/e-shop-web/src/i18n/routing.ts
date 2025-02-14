import {defineRouting} from "next-intl/routing";
import {createNavigation} from "next-intl/navigation";

export const routing = defineRouting({
    locales: ['en', 'pt-br'],
    defaultLocale: 'en',
    pathnames: {
        '/': '/',
        '/products': '/products',
        '/cart': {
            en: '/cart',
            "pt-br": '/carrinho', 
        },
        '/order': '/order',
        '/contact':'/contact',
        '/login':'/login'
    }
});

export type Pathnames = keyof typeof routing.pathnames;
export type Locale = (typeof routing.locales)[number];

export const {Link, getPathname, redirect, usePathname, useRouter} =
    createNavigation(routing);