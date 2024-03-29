﻿(*
 * SokobanFS - A Sokoban clone.
 * Copyright (C) 2018-2021  Simon Wendel
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

namespace SokobanFS.Lib.Tests.Sequence

module trimReplace =

    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Lib

    module List =

        [<Fact>]
        let ``Given integer list and replacement options, should trim off and replace elements`` () =
            let input = [ 1; 1; 1; 2; 1; 2; 3; 1 ]

            let expectation = [ 9; 9; 9; 2; 1; 2; 3; 9 ]

            input
            |> Sequence.List.trimReplace 1 9
            |> should equal expectation

    module Array =

        [<Fact>]
        let ``Given string array and replacement options, should trim off and replace elements`` () =
            let input =
                [| "1"
                   "1"
                   "1"
                   "2"
                   "1"
                   "2"
                   "3"
                   "1" |]

            let expectation =
                [| "9"
                   "9"
                   "9"
                   "2"
                   "1"
                   "2"
                   "3"
                   "9" |]

            input
            |> Sequence.Array.trimReplace "1" "9"
            |> should equal expectation
