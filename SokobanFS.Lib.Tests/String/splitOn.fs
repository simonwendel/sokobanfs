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

namespace SokobanFS.Lib.Tests.String

module splitOn =

    open FsUnit
    open Xunit

    open SokobanFS.Lib

    [<Fact>]
    let ``Given string and separator, splits string around separator`` () =

        let input = "string with some separation by spaces"

        let expected =
            [| "string"
               "with"
               "some"
               "separation"
               "by"
               "spaces" |]

        input
        |> String.splitOn ' '
        |> should equal expected
