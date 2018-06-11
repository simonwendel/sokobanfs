(*
 * SokobanFS - A Sokoban clone.
 * Copyright (C) 2018  Simon Wendel
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*)

namespace SokobanFS.Game

module Parse =

    open MapsTypes
    open SokobanFS.Lib

    let private toTile character = 
        
        match character with
        | '#' -> Wall
        | '@' -> Player
        | '+' -> PlayerOnGoal
        | '$' -> Box
        | '*' -> BoxOnGoal
        | '.' -> Goal
        | ' ' -> Floor
        | _ -> Empty

    let internal toBoard rows =    
        
        let trimEndsOffEachRow (s : string) =  
            s.TrimEnd (' ')

        let cleanColumnsTopAndBottom arr =
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
        |> cleanColumnsTopAndBottom
        |> MapsTypes.Board