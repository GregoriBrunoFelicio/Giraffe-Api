module CartHandler

open Models

let sendEmail email = "Email sent"

let buy (products: list<Product>, paymentType: PaymentType) =
    let discount =
        match products.Length with
        | n when n = 5 && paymentType = PaymentType.creditcard -> 0.5m
        | n when n = 10 && paymentType = PaymentType.creditcard -> 0.10m
        | n when n > 20 && paymentType = PaymentType.creditcard -> 0.50m
        | _ -> 0m

    let total = (products |> List.sumBy (fun p -> p.price)) * (1m - discount)

    let cart =
        { products = products
          total = total
          discount = discount
          paymentType = paymentType }

    sendEmail () |> ignore

    cart
