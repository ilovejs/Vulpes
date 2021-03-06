﻿namespace Common

module Utils =
    open System
    open Microsoft.FSharp.Collections
    open Analytics

    let nextMultipleOf n i =
        let r = i % n
        if r = 0 then i else i + n - r

    let disposeAll ([<ParamArray>] arr : 'a list array when 'a :> IDisposable) =
        for items in arr do
            for item in items do item.Dispose()

    let toArray (M : 'a[,]) =
        let h = height M
        let w = width M
        [|0..h - 1|] |> Array.map (fun i -> Array.init w (fun j -> M.[i, j]))

    let mapMatrix f M =
        Array2D.init (height M) (width M) (fun i j -> f M.[i, j])

    type RandomSingle(seed) =
        let random = new Random(seed)
        let mutable singleIndex = 0
        let mutable intIndex = 0
        member this.SingleIndex with get() = singleIndex
        member this.IntIndex with get() = intIndex
        member this.NextSingle = 
            singleIndex <- singleIndex + 1
            random.NextDouble() |> float32
        member this.Next = 
            intIndex <- intIndex + 1
            random.Next
