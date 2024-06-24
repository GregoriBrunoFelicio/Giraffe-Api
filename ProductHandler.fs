module ProductHandler

open Giraffe.Poc.Api.GiraffeContext
open Microsoft.AspNetCore.Http
open Giraffe
open System.Linq
open Models

let getAllProducts: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let products =
                ctx.GetService<GiraffeContext>().products.ToList<Product>() |> List.ofSeq

            return! json products next ctx
        }

let getById id : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            // REFACT!!!!!
            let products =
                ctx.GetService<GiraffeContext>().products.ToList<Product>() |> List.ofSeq
            let product =
                match products with
                | head :: _ when head.id = id -> Some(head)
                | _ -> None

            return! json product next ctx
        }

let add: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! product = ctx.BindJsonAsync<Product>()
            let context =  ctx.GetService<GiraffeContext>()
            let! _ = context.AddAsync(product)
            let! _ = context.SaveChangesAsync()
            return! json product next ctx
        }

let remove id : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            // REFACT
            let products =
                ctx.GetService<GiraffeContext>().products.ToList<Product>() |> List.ofSeq

            let rec remove (array: List<Product>, aux) =
                match array with
                | head :: tail when head.id = id -> tail @ aux
                | head :: tail -> remove (tail, (head :: aux))
                | [] -> aux

            return! json (remove (products, [])) next ctx
        }
