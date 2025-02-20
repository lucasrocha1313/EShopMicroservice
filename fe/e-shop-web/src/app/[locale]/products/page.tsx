"use client";

import ProductsGrid from "@/components/products/products-grid";
import Pagination from "@/components/ui/pagination";
import { getProducts } from "@/lib/api/products";
import { useTranslations } from "next-intl";
import { useState, useEffect } from "react";

export default function Products() {
    const t = useTranslations('Products');
      
      const [products, setProducts] = useState<Product[]>([]);
      const [loading, setLoading] = useState(true);
      const [page, setPage] = useState(1);
      const [totalPages, setTotalPages] = useState(3);
    
      useEffect(() => {
        getProducts(page, totalPages)
          .then((result) => {
            setProducts(result.data);
            setTotalPages(result.pageSize);
            setLoading(false);
          })
          .catch((error) => {
            console.error("Failed to load products:", error);
            setLoading(false); 
          });
      }, [page, totalPages]);
      
    
      if (loading) {
        return <div>Loading products...</div>;
      }
    
      if (!products || products.length === 0) {
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
            {/* TODO - move to component */}
            <Pagination totalPages={totalPages} currentPage={page} onPageChange={setPage} />
          </main>      
        </div>
      );
}