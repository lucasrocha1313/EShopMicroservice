"use client";

import { usePathname, useRouter } from "next/navigation";
import { useTransition } from "react";

type Props = {
    locale: string;
    newLocale: string;
    switchTo: string;
  };
  
  export default function LanguageSwitcher({ locale, newLocale, switchTo }: Props) {

    const pathname = usePathname();
    const [isPending, startTransition] = useTransition();
    const router = useRouter();

    const switchLanguage = () => {
        startTransition(() => {
            router.push(`/${newLocale}${pathname.replace(`/${locale}`, "/")}`);
        });
    };

    
    if(isPending)
        return  <span>Switching...</span>
  
    return (
        <button onClick={switchLanguage} disabled={isPending} style={{cursor: isPending ? "not-allowed" : "pointer"}}>
            {switchTo}
        </button>
    );
  }