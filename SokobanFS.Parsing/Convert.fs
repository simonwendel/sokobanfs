namespace SokobanFS.Parsing

module Convert =

    open SokobanFS.Game.MapsTypes

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