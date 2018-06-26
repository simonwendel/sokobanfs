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

///
/// Functions for parsing text into game data and back again.
module Parse =

    open System

    open GameTypes
    open ParseTypes
    open SokobanFS.Lib
    open SokobanFS.Lib.String

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

    let private squareLookup =
        mappings |> Map.ofList

    let private characterLookup =
        Map.ofList ((Empty, ' ') :: (List.map Tuple.swap mappings))

    let private toSquare character = 
        
        match squareLookup.TryFind character with
        | Some square -> square
        | None -> raise (InvalidFormatException "Invalid format")

    let private toChar square = characterLookup.[square]

    let private trimEnd (s : string) =  
        s.TrimEnd (' ')

    let private toChars = List.ofSeq

    let rec private toArrayList array =
        match Array2D.length1 array with
        | 0 -> []
        | _ -> List.ofArray array.[0,*] :: toArrayList array.[1..,*]

    let rec private qualifyCharacters list = 
        match list with 
        | [] -> []
        | head :: tail ->
            match Int32.TryParse(head.ToString()) with
            | (true, num) -> Number (num) :: qualifyCharacters tail
            | (false, _) -> Character (head) :: qualifyCharacters tail

    ///
    /// Converting string sequences (rows) into Level data, using common 
    /// Sokoban text level format.
    let internal toLevel rows =    
        
        let cleanColumnsTopAndBottom arr =
            for colNum = 0 to Array2D.length2 arr - 1 do
                arr.[*,colNum] 
                |> Sequence.Array.trimReplace Floor Empty
                |> Array.iteri (fun rowNum value -> arr.[rowNum,colNum] <- value)
            arr

        rows
        |> Array.ofSeq
        |> Array.map (trimEnd
                      >> Array.ofSeq 
                      >> Array.map toSquare 
                      >> Sequence.Array.trimReplace Floor Empty)
        |> Sequence2D.toArray2D Square.Empty
        |> cleanColumnsTopAndBottom
        |> GameTypes.Level
    
    ///
    /// Decoding a level from a run-length encoded string into Level data. 
    let internal decodeRLE (input : string) =

        let toCharListList = (List.ofArray >> List.map List.ofSeq)

        let rec joinNumbers list =
            match list with
            | [] -> []
            | Number (major) :: Number (minor) :: tail -> joinNumbers (Number (10 * major + minor) :: tail)
            | otherToken :: tail -> otherToken :: joinNumbers tail

        let rec expandCharacters list = 
            match list with 
            | [] -> [""]
            | Number (numberOfTimes) :: Character (character) :: tail -> (String.replicate numberOfTimes (character.ToString())) :: expandCharacters tail
            | Character (character) :: tail -> character.ToString() :: expandCharacters tail
            | illegal -> raise (InvalidFormatException (illegal.ToString()))
        
        input 
        |> replace '-' ' ' 
        |> splitOn '|'
        |> toCharListList
        |> List.map (qualifyCharacters >> joinNumbers >> expandCharacters >> List.reduce (+))
        |> toLevel
    
    ///
    /// Encoding Level data into a string, using a Sokoban-specific run-length encoding.
    let internal encodeRLE level = 

        let rec insertOnes list = 
            match list with 
            | [] -> []
            | head :: tail -> Number (1) :: head :: insertOnes tail

        let rec countRepetitions list = 
            match list with 
            | [] -> []
            | Number (n1) :: Character (c1) :: Number (n2) :: Character (c2) :: tail -> 
                if c1 = c2 then 
                    // not very performant for extremely long repetitions... so what?
                    countRepetitions (Number (n1 + n2) :: Character (c1) :: tail)
                else
                    Number (n1) :: Character (c1) :: countRepetitions (Number (n2) :: Character (c2) :: tail)
            | q :: tail -> q :: countRepetitions tail

        let rec stringifyTokens list =
            match list with 
            | [] -> []
            | Number (n) :: Character (c) :: tail ->
                let character = c.ToString()
                let number = if n = 1 then "" else n.ToString()

                if (n = 2) then
                    character :: character :: stringifyTokens tail
                else
                    number :: character :: stringifyTokens tail
            | illegal -> raise (InvalidFormatException (illegal.ToString()))
        
        match level with
        | Level level ->
            level 
            |> Array2D.map toChar
            |> toArrayList 
            |> List.map (String.fromSeq >> trimEnd >> toChars >> qualifyCharacters >> insertOnes >> countRepetitions >> stringifyTokens >> String.fromSeq)
            |> String.concat "|"
            |> replace ' ' '-' 
