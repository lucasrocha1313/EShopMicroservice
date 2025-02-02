"use client";

import styles from "./page.module.css";
import { useState, useEffect } from "react";


const API_URL = "http://localhost:6004/catalog-service/products"; //TODO - move to env

async function getProducts(): Promise<Product[]> {
  const res = await fetch(API_URL, { cache: "no-store" }); // no-store ensures fresh data
  const data: ProductResponse =  await res.json();
  return data.products;
}


export default function Home() {

  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  //TODO move this to a component
  //TODO create product card
  //TODO create Product grid
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
          <h1>Products</h1>
          <ul>
            {products.map((product) => (
              <li key={product.id}>{product.name} - ${product.price}</li>
            ))}
          </ul>
        </div>  
      </main>      
    </div>
  );
}
