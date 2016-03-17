#load "DuplicateSelector.fs"
#load "MutDuplicateSelector.fs"
#load "FisherYates.fs"
#load "MutFisherYates.fs"
#load "CodeGolf.fs"
#load "Short.fs"

open FSharp.TV.DupSelector
open FSharp.TV.MutDupSelector
open FSharp.TV.FisherYates
open FSharp.TV.MutFisherYates
open FSharp.TV.CodeGolf
open FSharp.TV.Short

let benchmarkDupSelectorShuffle size =
  dupSelector [1..size] 1 |> ignore

let benchmarkMutDupSelectorShuffle size =
  mutDupSelector [|1..size|] 1 |> ignore

let benchmarkFisherYatesShuffle size =
  fisherYates [1..size] 1 |> ignore

let benchmarkMutFisherYatesShuffle size =
  mutFisherYates [|1..size|] 1 |> ignore

let benchmarkCodeGolfShuffle size =
  codeGolf [|1..size|] 1 |> ignore

let benchmarkShortShuffle size =
  short [|1..size|] 1 |> ignore

let time countN label f =
  let stopwatch = System.Diagnostics.Stopwatch()

  System.GC.Collect()
  printfn "Started"

  let getGcStats() =
    let gen0 = System.GC.CollectionCount(0)
    let gen1 = System.GC.CollectionCount(1)
    let gen2 = System.GC.CollectionCount(2)
    let mem = System.GC.GetTotalMemory(false)
    gen0, gen1, gen2, mem

  printfn "========================"
  printfn "%s" label
  printfn "========================"

  for iteration in [1..countN] do
    let gen0, gen1, gen2, mem = getGcStats()
    stopwatch.Restart()
    f()
    stopwatch.Stop()
    let gen0', gen1', gen2', mem' = getGcStats()

    let changeInMem = (mem'-mem)/1000L
    printfn "#%2i elapsed:%6ims gen0:%3i gen1:%3i gen2:%3i mem:%6iK" iteration stopwatch.ElapsedMilliseconds (gen0'-gen0) (gen1'-gen1) (gen2'-gen2) changeInMem

for size in [1000; 2000; 5000; 10000; 20000; 50000; 100000; 200000; 500000; 1000000; 10000000] do
  if size < 5000 then
    let label = sprintf "benchmarkDupSelectorShuffle: %i records" size
    time 5 label (fun () -> benchmarkDupSelectorShuffle size)

  if size < 5000 then
    let label = sprintf "benchmarkMutDupSelectorShuffle: %i records" size
    time 5 label (fun () -> benchmarkMutDupSelectorShuffle size)

  if size < 5000 then
    let label = sprintf "benchmarkFisherYatesShuffle: %i records" size
    time 5 label (fun () -> benchmarkFisherYatesShuffle size)

  if size < 100000 then
    let label = sprintf "benchmarkMutFisherYatesShuffle: %i records" size
    time 5 label (fun () -> benchmarkMutFisherYatesShuffle size)

  let codeGolfLabel = sprintf "benchmarkCodeGolfShuffle: %i records" size
  time 5 codeGolfLabel (fun () -> benchmarkCodeGolfShuffle size)

  let shortLabel = sprintf "benchmarkShortShuffle: %i records" size
  time 5 shortLabel (fun () -> benchmarkShortShuffle size)