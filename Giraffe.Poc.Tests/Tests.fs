module Tests

open FluentAssertions
open System
open Xunit

let sum a b = a + b

[<Fact>]
let ``My test`` () =
    Assert.True(true)
    
[<Fact>]
let ``Test test`` () =
    let result = (sum 10 10)
    result.Should().Be(20, "")

[<Fact>]
let ``Remove product`` () =
   let result = (ProductHandler.remove 1)
   result.Should().BeNull("")
   
