import styles from "./product-card.module.css";


type Props = {
    product: Product;
}


export default function ProductCard({ product }: Props) {
    return (
    <div className={styles.card}>
        <h1 className={styles.title}>{product.name}</h1>
        <p>{product.description}</p>
        <div className={styles.price}>
         <label >${product.price}</label>
        </div>
        <div className={styles.addCartBtn}>
            <button>Add to cart</button>
        </div>        
        
    </div>
    );
}