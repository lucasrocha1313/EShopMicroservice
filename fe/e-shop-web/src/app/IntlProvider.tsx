'use client';

import { ReactNode } from 'react';
import { NextIntlClientProvider } from 'next-intl';
import { useLocale, useMessages } from 'next-intl';

interface IntlProviderProps {
    children: ReactNode;
}

export function IntlProvider({ children }: IntlProviderProps) {
    const locale = useLocale();
    const messages = useMessages();

    return (
        <NextIntlClientProvider locale={locale} messages={messages}>
            {children}
        </NextIntlClientProvider>
    );
}