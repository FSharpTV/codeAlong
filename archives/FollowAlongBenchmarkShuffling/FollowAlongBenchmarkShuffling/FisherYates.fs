module FSharp.TV.FisherYates

let fisherYates listOfItems seed =
  let rng = System.Random(seed)

  let rec shuffler unshuffled shuffled =
    let count = unshuffled |> List.length

    if count = 0
    then shuffled
    else
      let index = rng.Next(count)
      let item = unshuffled.[index]
      let newUnshuffled = unshuffled |> List.filter (fun i -> i <> item)
      let newShuffled = item::shuffled
      shuffler newUnshuffled newShuffled

  shuffler listOfItems []
