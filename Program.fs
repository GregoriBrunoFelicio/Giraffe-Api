open Giraffe.Poc.Api.GiraffeContext
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Microsoft.EntityFrameworkCore

let webApp =
    choose
        [ route "/product" >=> ProductHandler.getAllProducts
          routef "/product/%i" ProductHandler.getById
          routef "/product/delete/%i" ProductHandler.remove ]

let configureApp (app: IApplicationBuilder) = app.UseGiraffe webApp

let configureServices (services: IServiceCollection) =
    services.AddGiraffe() |> ignore
    services.AddDbContext<GiraffeContext>(fun options ->
        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=giraffe;") |> ignore
    ) |> ignore

[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder ->
            webHostBuilder.Configure(configureApp).ConfigureServices(configureServices)
            |> ignore)
        .Build()
        .Run()

    0
