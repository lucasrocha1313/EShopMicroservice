//Get products from the API using the fetch API and return the products array or an empty array if the request fails.
//Pagination is implemented by passing the page and size parameters to the API.
export async function getProducts(page: number = 1, size: number= 3): Promise<Product[]> {

    const API_URL = `${process.env.NEXT_PUBLIC_API_BASE_URL}/${process.env.NEXT_PUBLIC_CATALOG_SERVICE}/products?pageNumber=${page}&pageSize=${size}`;
    
  
    try {
      const res = await fetch(API_URL, { cache: "no-store" }); // Avoid stale data
      if (!res.ok) throw new Error(`Failed to fetch products: ${res.statusText}`);
  
      const data: ProductResponse = await res.json();
      return data.products;
    } catch (error) {
      console.error("Error fetching products:", error);
      return []; // Return an empty array to prevent UI crashes
    }
  }