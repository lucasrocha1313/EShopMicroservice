"use client";

import styles from "./page.module.css";
import { useState, useEffect } from "react";
import ProductsGrid from "@/components/products/products-grid";
import { getProducts } from "@/lib/api/products";



export default function Home() {

  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getProducts()
      .then((data) => {
        setProducts(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Failed to load products:", error);
        setLoading(false); 
      });
  }, []);
  

  if (loading) {
    return <div>Loading products...</div>;
  }

  if (products.length === 0) {
    return <div>No products found.</div>;
  }

  return (
    <div className={styles.page}>
      <main className={styles.main}>
        <div>          
          <div>
            <ProductsGrid products={products} />
          </div>
        </div>  
      </main>      
    </div>
  );
}
