import MainHeader from "@/components/main-header/main-header";
import {ReactNode} from "react";
import {notFound} from "next/navigation";
import {routing} from "@/i18n/routing";
import {setRequestLocale} from "next-intl/server";


type Props = {
  children: ReactNode;
  params: {locale: string};
};


export default async function RootLayout({
  children, params
}: Props) {

  const {locale} = await params;

  if (!routing?.locales.includes(locale as never)) {
    notFound();
  }

  // Enable static rendering
  setRequestLocale(locale);

  return (
    <>
      <MainHeader params={{locale}} />
      {children}
    </>
  );
}
