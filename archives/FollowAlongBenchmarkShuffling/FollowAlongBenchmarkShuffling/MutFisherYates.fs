module FSharp.TV.MutFisherYates

let mutFisherYates arrayOfItems seed =
  let rng = System.Random(seed)

  let size = arrayOfItems |> Array.length
  let shuffled = ResizeArray()
  let unshuffled = ResizeArray(arrayOfItems)

  while shuffled.Count < size do
    let index = rng.Next(unshuffled.Count)
    let item = unshuffled.[index]
    unshuffled.Remove(item) |> ignore
    shuffled.Add(item)

  shuffled.CopyTo(arrayOfItems)
