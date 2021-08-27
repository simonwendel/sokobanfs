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

namespace SokobanFS.Lib.Tests.Tuple

module swap =

    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Lib

    let private always = true

    [<Property>]
    let ``Given int*string tuple, will swap first and second value `` (first: int, second: string) =
        always
        ==> ((first, second) |> Tuple.swap = (second, first))

    [<Property>]
    let ``Given string*string tuple, will swap first and second value `` (first: string, second: string) =
        always
        ==> ((first, second) |> Tuple.swap = (second, first))


    [<Property>]
    let ``Given float*char tuple, fst not NaN, will swap first and second value `` (first: NormalFloat, second: char) =
        always
        ==> ((first, second) |> Tuple.swap = (second, first))

    [<Property>]
    let ``Given float*int tuple, fst = NaN, will swap first and second value `` (second: int) =
        let first = nan

        let nanIsSwapped swapped =
            fst swapped = second
            && System.Double.IsNaN(snd swapped)

        always
        ==> ((first, second) |> Tuple.swap |> nanIsSwapped)

    [<Property>]
    let ``Given float*string tuple, fst = NaN, will swap first and second value `` (second: string) =
        let first = infinity

        let infinityIsSwapped swapped =
            fst swapped = second
            && System.Double.IsInfinity(snd swapped)

        always
        ==> ((first, second) |> Tuple.swap |> infinityIsSwapped)
