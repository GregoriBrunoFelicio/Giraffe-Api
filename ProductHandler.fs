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
            let context = ctx.GetService<GiraffeContext>()
            let product = context.products.FirstOrDefault(fun x -> x.id = id)
            return! json product next ctx
        }

let add: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! product = ctx.BindJsonAsync<Product>()
            let context = ctx.GetService<GiraffeContext>()
            let! _ = context.AddAsync(product)
            let! _ = context.SaveChangesAsync()
            return! json product next ctx
        }

let remove id : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let context = ctx.GetService<GiraffeContext>()
            // REfACT use existing method to get by id
            let product = context.products.FirstOrDefault(fun f -> f.id = id)
            context.Remove(product) |> ignore
            let! _ = context.SaveChangesAsync()
            return! json () next ctx
        }
