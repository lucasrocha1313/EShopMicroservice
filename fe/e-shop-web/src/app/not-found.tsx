import BaseLayout from "@/components/layouts/base-layout";
import MainHeader from "@/components/main-header/main-header";

export default function NotFoundPage() {
    return (
        <BaseLayout locale="en">
            <MainHeader params={{locale: "en"}} />
            <main className="not-found">
                <h1>Page not found</h1>
                <p>Sorry, we could not find the page you were looking for.</p>
            </main>
        </BaseLayout>
        
    )
}