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
    open ParseTypes

    type private Token =
        | Number of int
        | Character of char

    let private mappings =
        [ '#', Wall;
          '@', Player;
          '+', PlayerOnGoal;
          '$', Box;
          '*', BoxOnGoal;
          '.', Goal;
          ' ', Floor ]

    let private tileLookup =
        mappings |> Map.ofList

    let private toTile character = 
        
        match tileLookup.TryFind character with
        | Some tile -> tile
        | None -> raise (IllegalFormatException "Invalid format")

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

    let internal decodeRLE (input : string) =

        let split (separator : char) (str : string) = 
            str.Split(separator)

        let replace (find : char) (replacement : char) (str : string) = 
            str.Replace(find, replacement)

        let toCharListList = 
            (List.ofArray >> List.map List.ofSeq)

        let rec qualifyCharacters list = 
            match list with 
            | [] -> []
            | head :: tail ->
                match Int32.TryParse(head.ToString()) with
                | (true, num) -> Number (num) :: qualifyCharacters tail
                | (false, _) -> Character (head) :: qualifyCharacters tail

        let rec sumUpNumbers list =
            match list with
            | [] -> []
            | Number (major) :: Number (minor) :: tail -> sumUpNumbers (Number (10 * major + minor) :: tail)
            | otherToken :: tail -> otherToken :: sumUpNumbers tail

        let rec expandCharacters list = 
            match list with 
            | [] -> [""]
            | Number (numberOfTimes) :: Character (character) :: tail -> (String.replicate numberOfTimes (character.ToString())) :: expandCharacters tail
            | Character (character) :: tail -> character.ToString() :: expandCharacters tail
            | illegal -> raise (IllegalFormatException (illegal.ToString()))
        
        input 
        |> replace '-' ' ' 
        |> split '|'
        |> toCharListList
        |> List.map (qualifyCharacters >> sumUpNumbers >> expandCharacters >> List.reduce (+))
        |> toBoard
