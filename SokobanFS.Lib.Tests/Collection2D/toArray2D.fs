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

namespace SokobanFS.Lib.Tests.Collection2D

module toArray2D =
    
    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Lib
    
    [<Fact>]
    let ``Given integer list list and padding element, returns Array2D representation`` () = 
        
        let jaggedCollectionOfInts = 
            [[ 1; 2; 3 ]; 
             [ 1; 2 ]; 
             [ 1; 2; 3 ]; 
             [ 1; 2; 3; 4 ]; 
             [ 1; 2; 3 ]]

        let expectedArray = 
            array2D 
                [| [| 1; 2; 3; 9 |]; 
                   [| 1; 2; 9; 9 |]; 
                   [| 1; 2; 3; 9 |]; 
                   [| 1; 2; 3; 4 |]; 
                   [| 1; 2; 3; 9 |] |]

        jaggedCollectionOfInts |> Collection2D.toArray2D 9 |> should equal expectedArray
 
    [<Fact>]
    let ``Given string array array and padding element, returns Array2D representation`` () = 

         let jaggedCollectionOfStrings =
            [| [| "1"; "2"; "3"; "4" |]; 
               [| "1"; "2"; "3"; "4"; "5" |]; 
               [| "1"; "2"; "3" |]; 
               [| "1"; "2" |]; 
               [| "1"; "2"; "3" |];
               [| "1"; "2"; "3" |] |]

         let expectedArray =
            array2D  
                [| [| "1"; "2"; "3"; "4"; "9" |]; 
                   [| "1"; "2"; "3"; "4"; "5" |]; 
                   [| "1"; "2"; "3"; "9"; "9" |]; 
                   [| "1"; "2"; "9"; "9"; "9" |]; 
                   [| "1"; "2"; "3"; "9"; "9" |];
                   [| "1"; "2"; "3"; "9"; "9" |] |]
        
         jaggedCollectionOfStrings |> Collection2D.toArray2D "9" |> should equal expectedArray

