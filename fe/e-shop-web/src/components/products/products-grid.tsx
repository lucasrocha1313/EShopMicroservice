import ProductCard from "./product-card";
import styles from "./products-grid.module.css";

type Props = {
    products: Product[];
}

export default function ProductsGrid({ products }: Props) {

    //TODO - Add translation
    //TODO - Handle empty products
    return (
        <>
            <h1>Products</h1>
            <div className={styles.grid}>
                {products.map((product) => (
                    <ProductCard key={product.id} product={product} />
                ))}
            </div>
        </>
        
    );
}