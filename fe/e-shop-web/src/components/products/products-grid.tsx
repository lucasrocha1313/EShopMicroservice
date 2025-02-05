// import { useTranslations } from "next-intl";
import { useTranslations } from "next-intl";
import ProductCard from "./product-card";
import styles from "./products-grid.module.css";

type Props = {
    products: Product[];
}

export default function ProductsGrid({ products }: Props) {

    const t = useTranslations('Products');

    //TODO - Handle empty products
    return (
        <>
            <h1>{t('title')}</h1>
            <p>{t('description')}</p>
            <div className={styles.grid}>
                {products.map((product) => (
                    <ProductCard key={product.id} product={product} />
                ))}
            </div>
        </>
        
    );
}