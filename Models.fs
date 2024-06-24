module Models

[<CLIMutable>]
type Product =
    { id: int
      Name: string
      Description: string
      price: decimal }

type Cart =
    { Products: List<Product>
      Total: decimal
      Discount: decimal }
