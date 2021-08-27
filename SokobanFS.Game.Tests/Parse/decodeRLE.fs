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

module decodeRLE =

    open FsUnit
    open Xunit

    open SokobanFS.Game
    open SokobanFS.Game.GameTypes

    [<Fact>]
    let ``Given RLE encoded level, produces valid Level`` () = 

        let input = "3#|#.3#|#*$-#|#--@#|5#"

        let expectation = 
            Level <| array2D 
                [ [ Wall; Wall; Wall; Empty; Empty ]
                  [ Wall; Goal; Wall; Wall; Wall ]
                  [ Wall; BoxOnGoal; Box; Floor; Wall ]
                  [ Wall; Floor; Floor; Player; Wall ]
                  [ Wall; Wall; Wall; Wall; Wall ] ]

        input |> Parse.decodeRLE |> should equal expectation

    [<Fact>]
    let ``Given two consecutive characters of same type, outputs same typed squares`` () =

        let input = "##"

        let expectation = 
            Level <| array2D
                [ [ Wall; Wall ] ]

        input |> Parse.decodeRLE |> should equal expectation

    [<Fact>]
    let ``Given two RLE encoded characters of same type, outputs same typed squares`` () =

        let input = "2#"

        let expectation = 
            Level <| array2D
                [ [ Wall; Wall ] ]

        input |> Parse.decodeRLE |> should equal expectation

    [<Fact>]
    let ``Given 12300 space characters, outputs 12300 floor squares`` () =

        let input = "#12300-#"

        let expectation = 
            Parse.toLevel <| [ String.concat "" [ "#"; String.replicate 12300 " "; "#" ] ]

        input |> Parse.decodeRLE |> should equal expectation
        
    [<Fact>]
    let ``Given number without following square character, throws exception`` () =

        let input = "$$|#100"

        (fun () -> input |> Parse.decodeRLE |> ignore) |> should throw typeof<ParseTypes.InvalidFormatException>
