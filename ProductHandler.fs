module ProductHandler

open AutoBogus
open Microsoft.AspNetCore.Http
open Giraffe
open Models

let product1 =
    [ { id = 1
        price = 2000
        Description = Some("sony video game")
        Name = "PS5" } ]

let products = product1 @ (AutoFaker<Product>().Generate(10) |> Seq.toList)

let getAllProducts: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) -> task { return! json products next ctx }

let getById id : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let product =
                match products with
                | head :: _ when head.id = id -> Some(head)
                | _ -> None
            return! json product next ctx
        }
