module FSharp.TV.CodeGolf
let codeGolf (arrayOfItems:'a []) seed =
  let rng = System.Random(seed)
  let keys = [|for _ in arrayOfItems -> rng.Next()|]
  System.Array.Sort(keys, arrayOfItems)
