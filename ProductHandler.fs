module ProductHandler

open AutoBogus
open Microsoft.AspNetCore.Http
open Giraffe
open Models

let getAllProducts: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let product1 =  [{ id = 1; price = 2000; Description = Some("sony video game"); Name = "PS5" }]
            let products = product1 @ (AutoFaker<Product>().Generate(20) |> Seq.toList)
            return! json products next ctx
        }
