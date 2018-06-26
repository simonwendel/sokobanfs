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

namespace SokobanFS.Lib.Tests.Sequence2D

module sizeOfJagged =
    
    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Lib
    open SokobanFS.Lib.Sequence2DTypes
    
    [<Fact>]
    let ``Given integer list list, computes min 2D array size that would hold the jagged collection`` () = 
        
        let jaggedCollectionOfInts = 
            [ [ 1; 2; 3 ]; 
              [ 1; 2 ]; 
              [ 1; 2; 3 ]; 
              [ 1; 2; 3; 4 ]; 
              [ 1; 2; 3 ] ]
        
        jaggedCollectionOfInts |> Sequence2D.sizeOfJagged |> should equal (Dimensions2D (4, 5))
 
    [<Fact>]
    let ``Given string array array, computes min 2D array size that would hold the jagged collection`` () = 

         let jaggedCollectionOfStrings =
            [| [| "1"; "2"; "3"; "4" |]; 
               [| "1"; "2"; "3"; "4"; "5" |]; 
               [| "1"; "2"; "3" |]; 
               [| "1"; "2" |]; 
               [| "1"; "2"; "3" |];
               [| "1"; "2"; "3" |] |]
        
         jaggedCollectionOfStrings |> Sequence2D.sizeOfJagged |> should equal (Dimensions2D (5, 6))
