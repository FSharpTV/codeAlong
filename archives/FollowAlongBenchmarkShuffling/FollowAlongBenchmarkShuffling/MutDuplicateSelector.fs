module FSharp.TV.MutDupSelector

let mutDupSelector arrayOfItems seed =
  let rng = System.Random(seed)

  let size = arrayOfItems |> Array.length
  let shuffled = ResizeArray()

  while shuffled.Count < size do
    let index = rng.Next(size)
    let item = arrayOfItems.[index]
    if shuffled.Contains(item) 
    then () // Continue
    else shuffled.Add(item)

  shuffled.CopyTo(arrayOfItems)
