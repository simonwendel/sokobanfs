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

namespace SokobanFS.Game.Tests.Parse

module toLevelCollection =

    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Game
    open SokobanFS.Game.GameTypes
    open SokobanFS.Lib

    [<Fact>]
    let ``Given known level format, produces valid LevelCollection`` () = 

        let text = 
            [ "  ####";
              "###  ####";
              "#     $ #";
              "# #  #$ #";
              "# . .#@ #";
              "#########" ]

        let input = [ text; text; text ];

        let level =
            Level <| Sequence2D.toArray2D 
                Square.Empty
                [ [ Empty; Empty; Wall; Wall; Wall; Wall ];
                  [ Wall; Wall; Wall; Floor; Floor; Wall; Wall; Wall; Wall ];
                  [ Wall; Floor; Floor; Floor; Floor; Floor; Box; Floor; Wall ];
                  [ Wall; Floor; Wall; Floor; Floor; Wall; Box; Floor; Wall ];
                  [ Wall; Floor; Goal; Floor; Goal; Wall; Player; Floor; Wall ];
                  [ Wall; Wall; Wall; Wall; Wall; Wall; Wall; Wall; Wall ] ]

        let expectation = 
            { name = "Kickass Level"; 
              levels = [ level; level; level ] }

        input |> Parse.toLevelCollection "Kickass Level" |> should equal expectation