(*
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

namespace SokobanFS.Game.Tests.Parse

module encodeRLE =

    open FsUnit
    open Xunit

    open SokobanFS.Game
    open SokobanFS.Game.GameTypes

    [<Fact>]
    let ``Given Level, produces RLE encoded level string`` () =

        let input =
            Level
            <| array2D [ [ Wall; Wall; Wall; Empty; Empty ]
                         [ Wall; Goal; Wall; Wall; Wall ]
                         [ Wall; BoxOnGoal; Box; Floor; Wall ]
                         [ Wall; Floor; Floor; Player; Wall ]
                         [ Wall; Wall; Wall; Wall; Wall ] ]

        let expectation = "3#|#.3#|#*$-#|#--@#|5#"

        input
        |> Parse.encodeRLE
        |> should equal expectation

    [<Fact>]
    let ``Given two consecutive Squares of same type, outputs same typed characters`` () =

        let input = Level <| array2D [ [ Wall; Wall ] ]

        let expectation = "##"

        input
        |> Parse.encodeRLE
        |> should equal expectation

    [<Fact>]
    let ``Given 1230 Floor squares, produces 1230 space characters`` () =

        let input =
            Parse.toLevel
            <| [ String.concat "" [ "#"; String.replicate 1230 " "; "#" ] ]

        let expectation = "#1230-#"

        input
        |> Parse.encodeRLE
        |> should equal expectation
