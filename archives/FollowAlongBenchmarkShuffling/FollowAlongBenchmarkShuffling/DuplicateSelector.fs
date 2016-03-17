module FSharp.TV.DupSelector

let dupSelector listOfItems seed =
  let rng = System.Random(seed)
  let size = listOfItems |> List.length

  let rec shuffler shuffled =
    let count = shuffled |> List.length
    if count = size then shuffled
    else
      let index = rng.Next(size)
      let item = listOfItems.[index]

      if shuffled |> List.contains item 
      then shuffler shuffled
      else shuffler (item::shuffled)
  
  shuffler []