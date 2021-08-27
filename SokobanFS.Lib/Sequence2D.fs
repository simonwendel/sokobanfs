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

namespace SokobanFS.Lib

module Sequence2D =

    open Sequence2DTypes

    /// <summary>
    /// Calculates dimensions of the smallest possible 2D array that could hold the
    /// jagged sequence of sequences.
    /// </summary>
    /// <returns>
    /// Width and height from the provided sequence of sequences.
    /// </returns>
    let sizeOfJagged sequences =
        let height = Seq.length sequences

        let width =
            sequences |> Seq.map Seq.length |> Seq.max

        Dimensions2D(width, height)

    /// <summary>
    /// Converts a jagged sequence of sequences into Array2D.
    /// </summary>
    /// <param name="padding">Value to pad with if row has too few columns.</param>
    /// <param name="jagged">A possibly jagged sequence of sequences to convert to Array2D.</param>
    /// <return>
    /// Array2D initialized from provided sequence of sequences, possibly padded.
    /// </return>
    let toArray2D padding jagged =
        let (Dimensions2D (cols, rows)) = sizeOfJagged jagged
        let array = Array2D.create rows cols padding

        jagged
        |> Seq.iteri (fun rowNum row -> Seq.iteri (fun colNum value -> array.[rowNum, colNum] <- value) row)

        array
