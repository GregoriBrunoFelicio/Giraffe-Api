module Models

type Product =
    { id: int
      Name: string
      Description: Option<string>
      price: decimal }

type Cart =
    { Products: List<Product>
      Total: decimal }
