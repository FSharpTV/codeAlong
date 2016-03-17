module FSharp.TV.Short

let short arrayOfItems seed =
  let rng = System.Random(seed)
  arrayOfItems |> Array.sortBy (fun _ -> rng.Next())
