type Product = {
    id: string;
    name: string;
    category: number[];
    description: string;
    imageFile: string;
    price: number;
  };
  
  type ProductResponse = {
    products: Product[];
  };
  