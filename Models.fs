module Models

[<CLIMutable>]
type Product =
    { id: int
      name: string
      description: string
      price: decimal }

type PaymentType =
    | creditcard = 0
    | cash = 1
    
type Cart =
    { products: List<Product>
      total: decimal
      discount: decimal
      paymentType: PaymentType }
