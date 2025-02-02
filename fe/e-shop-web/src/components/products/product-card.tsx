import styles from "./product-card.module.css";


type Props = {
    product: Product;
}


export default function ProductCard({ product }: Props) {
    return <div className={styles.card}>
    <h1>{product.name}</h1>
    <p>${product.price}</p>
    <p>{product.description}</p>
    </div>;
}