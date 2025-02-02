"use client";

import styles from "./page.module.css";
import { useState, useEffect } from "react";
import ProductsGrid from "@/components/products/products-grid";


const API_URL = "http://localhost:6004/catalog-service/products"; //TODO - move to env

async function getProducts(): Promise<Product[]> {
  const res = await fetch(API_URL, { cache: "no-store" }); // no-store ensures fresh data
  const data: ProductResponse =  await res.json();
  return data.products;
}


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
