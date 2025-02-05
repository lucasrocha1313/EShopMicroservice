import { NextIntlClientProvider } from "next-intl";
import { getMessages } from "next-intl/server";
import { ReactNode } from "react";

type Props = {
    children: ReactNode;
    locale: string;
  };

export default async function BaseLayout({children, locale}: Props) {

    // Enable static rendering for client-side
    const messages = await getMessages();

    return (
        <html className="h-full" lang={locale}>
          <body>
            <NextIntlClientProvider messages={messages}>
              {children}
            </NextIntlClientProvider>
          </body>
        </html>
      );
}