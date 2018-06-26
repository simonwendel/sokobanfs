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

namespace SokobanFS.Lib

module Sequence =

    module private HigherOrder =

        let trimReplace mapFoldFn revFn =
    
            fun value replacement seq ->

                let folder shouldContinue element =
                    if shouldContinue && element = value
                    then (replacement, true)
                    else (element, false)

                seq 
                |> mapFoldFn folder true |> fst
                |> revFn 
                |> mapFoldFn folder true |> fst 
                |> revFn

    module Array =
    
        let trimReplace value replacement array = 
            let arrayTrimReplace = HigherOrder.trimReplace Array.mapFold Array.rev
            arrayTrimReplace value replacement array

    module List =

        let trimReplace value replacement list = 
            let listTrimReplace = HigherOrder.trimReplace List.mapFold List.rev
            listTrimReplace value replacement list
