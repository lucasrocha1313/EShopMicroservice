import {defineRouting} from "next-intl/routing";
import {createNavigation} from "next-intl/navigation";

export const routing = defineRouting({
    locales: ['en', 'pt-br'],
    defaultLocale: 'en',
    pathnames: {
        '/': '/',
        '/pathnames': {
            en: '/pathnames',
            "pt-br": '/caminhos', //TODO: Check if this is correct
        }
    }
});

export type Pathnames = keyof typeof routing.pathnames;
export type Locale = (typeof routing.locales)[number];

export const {Link, getPathname, redirect, usePathname, useRouter} =
    createNavigation(routing);