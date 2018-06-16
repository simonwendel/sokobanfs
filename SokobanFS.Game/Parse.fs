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

    open System
    
    open SokobanFS.Lib
    open MapsTypes

    type private CharacterQualifier =
        | Digit of int
        | Letter of char

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
            for colNum = 0 to Array2D.length2 arr - 1 do
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

    let internal decodeRLE (str : string) =

        let qualifyElements row = 
            row 
            |> List.map (fun character ->
                match Int32.TryParse (character.ToString()) with
                | (true, num) -> Digit (num)
                | (false, _) -> Letter (character))

        let sumUpNumbers state head =
            match head with
            | Letter (_) -> head :: state
            | Digit n ->
                match state with
                | [] -> [head]
                | Digit (h) :: tail -> Digit (n + 10 * h) :: tail
                | Letter(_) :: _ -> Digit (n) :: state

        let rec expandCharacters list = 
            match list with 
            | [] -> [""]
            | Letter (c) :: tail -> (c.ToString()) :: (expandCharacters tail)
            | (Digit (n)) :: (Letter (c)) :: tail -> (String.replicate n (c.ToString())) :: (expandCharacters tail)
            | _ -> raise (ParseTypes.InvalidFormat "Invalid format")

        str
            .Replace("-", " ")
            .Split('|')
        |> List.ofSeq
        |> List.map
            (  List.ofSeq
            >> qualifyElements
            >> (List.fold sumUpNumbers [])
            >> List.rev
            >> expandCharacters
            >> List.reduce (+))
        |> toBoard
