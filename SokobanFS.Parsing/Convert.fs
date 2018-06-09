namespace SokobanFS.Parsing

module Convert =

    open SokobanFS.Game.MapsTypes
    open SokobanFS.Lib
    open SokobanFS.Game

    let internal toTile character = 
        
        match character with
        | '#' -> Wall
        | '@' -> Player
        | '+' -> PlayerOnGoal
        | '$' -> Box
        | '*' -> BoxOnGoal
        | '.' -> Goal
        | ' ' -> Floor
        | _ -> Empty

    let toBoard rows =    
        
        let trimEndsOffEachRow (s : string) =  
            s.TrimEnd (' ')

        let cleanColumnsTopToBottom arr =
            for colNum = 0 to (Array2D.length2 arr) - 1 do
                arr.[*,colNum] 
                |> Sequence.Array.trimReplace Floor Empty
                |> Array.iteri (fun rowNum value -> arr.[rowNum,colNum] <- value)
            arr

        rows
        |> Array.ofSeq
        |> Array.map trimEndsOffEachRow
        |> Array.map (Array.ofSeq >> Array.map toTile >> Sequence.Array.trimReplace Floor Empty)
        |> Sequence2D.toArray2D Tile.Empty
        |> cleanColumnsTopToBottom
        |> MapsTypes.Board