"use client";

import styles from "./page.module.css";
import { useState, useEffect } from "react";
import ProductsGrid from "@/components/products/products-grid";
import { getProducts } from "@/lib/api/products";
import { useTranslations } from "next-intl";



export default function Home() {

  const t = useTranslations('Home');
  
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getProducts()
      .then((response) => {
        setProducts(response.data);
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
          <h1>{t('title')}</h1>
          <p>{t('description')}</p>         
          <div>
            <ProductsGrid products={products} />
          </div>
        </div>  
      </main>      
    </div>
  );
}
