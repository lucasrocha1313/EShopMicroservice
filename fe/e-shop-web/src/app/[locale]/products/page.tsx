"use client";

import ProductsGrid from "@/components/products/products-grid";
import { getProducts } from "@/lib/api/products";
import { useTranslations } from "next-intl";
import { useState, useEffect } from "react";

export default function Products() {
    const t = useTranslations('Products');
      
      const [products, setProducts] = useState<Product[]>([]);
      const [loading, setLoading] = useState(true);
    
      useEffect(() => {
        getProducts(1, 6)
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
        <div>
          <main>
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