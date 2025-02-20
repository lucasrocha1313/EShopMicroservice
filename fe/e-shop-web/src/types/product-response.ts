type Product = {
    id: string;
    name: string;
    category: number[];
    description: string;
    imageFile: string;
    price: number;
  };
  
  type ProductsPaginated = {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: Product[];
  };

  type ProductsResponse = {
    productsPaginated: ProductsPaginated;
  } 
  