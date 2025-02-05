import MainHeader from "@/components/main-header/main-header";
import {ReactNode} from "react";
import {notFound} from "next/navigation";
import {routing} from "@/i18n/routing";
import {setRequestLocale} from "next-intl/server";
import BaseLayout from "@/components/layouts/base-layout";

type Props = {
  children: ReactNode;
  params: {locale: string};
};


export default async function LocaleLayout({
  children, params
}: Props) {

  const {locale} = await params;

  if (!routing?.locales.includes(locale as never)) {
    notFound();
  }

  // Enable static rendering
  setRequestLocale(locale);

  //TODO - find why cant I use IntlProvider here - https://next-intl.dev/docs/usage/configuration#client-server-components

  return (
    <BaseLayout locale={locale}>
      <MainHeader params={{locale}} />
      {children}   
    </BaseLayout>
  );
}
