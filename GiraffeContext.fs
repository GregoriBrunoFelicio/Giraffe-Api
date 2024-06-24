module Giraffe.Poc.Api.GiraffeContext

open Microsoft.EntityFrameworkCore
open Models

type GiraffeContext(options: DbContextOptions<GiraffeContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable products: DbSet<Product>

    member public this.Product
        with get () = this.products
        and set p = this.products <- p

    override this_.OnModelCreating(modelBuilder: ModelBuilder) =
        modelBuilder.Entity<Product>().HasKey(fun (x: Product) -> x.id :> obj) |> ignore
