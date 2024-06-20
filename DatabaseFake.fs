module DatabaseFake

open AutoBogus
open Models

let getAllProducts =
    AutoFaker<Product>().Generate 100