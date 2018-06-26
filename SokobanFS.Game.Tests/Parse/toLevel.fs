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

module toLevel =

    open FsCheck
    open FsUnit
    open Xunit

    open SokobanFS.Game
    open SokobanFS.Game.GameTypes
    open SokobanFS.Game.ParseTypes
    open SokobanFS.Lib

    [<Fact>]
    let ``Given known level format, produces valid Level`` () = 

        let input = 
            [ "  ####";
              "###  ####";
              "#     $ #";
              "# #  #$ #";
              "# . .#@ #";
              "#########" ]

        let expectation =
            Level <| Sequence2D.toArray2D 
                Square.Empty
                [ [ Empty; Empty; Wall; Wall; Wall; Wall ];
                  [ Wall; Wall; Wall; Floor; Floor; Wall; Wall; Wall; Wall ];
                  [ Wall; Floor; Floor; Floor; Floor; Floor; Box; Floor; Wall ];
                  [ Wall; Floor; Wall; Floor; Floor; Wall; Box; Floor; Wall ];
                  [ Wall; Floor; Goal; Floor; Goal; Wall; Player; Floor; Wall ];
                  [ Wall; Wall; Wall; Wall; Wall; Wall; Wall; Wall; Wall ] ]

        input |> Parse.toLevel |> should equal expectation

    [<Fact>]
    let ``Given extra spaces on the right, trims them off`` () =

        let input = 
            [ "#       ";
              "*       " ]

        let expectation =
           Level <| array2D 
            [ [ Wall ]; 
              [ BoxOnGoal ] ]

        input |> Parse.toLevel |> should equal expectation

    [<Fact>]
    let ``Given extra spaces on the left, cleans them up with empty squares`` () =

        let input = 
            [ "  #";
              "  *" ]

        let expectation =
           Level <| array2D 
            [ [ Empty; Empty; Wall ]; 
              [ Empty; Empty; BoxOnGoal ] ]

        input |> Parse.toLevel |> should equal expectation

    [<Fact>]
    let ``Given extra spaces on the top, cleans them up with empty squares`` () =

        let input = 
            [ "* *";
              "###" ]

        let expectation =
           Level <| array2D 
            [ [ BoxOnGoal; Empty; BoxOnGoal ]; 
              [ Wall; Wall; Wall ] ]

        input |> Parse.toLevel |> should equal expectation

    [<Fact>]
    let ``Given extra spaces on the bottom, cleans them up with empty squares`` () =

        let input = 
            [ "###"; 
              "* *" ]

        let expectation =
           Level <| array2D 
            [ [ Wall; Wall; Wall ];
              [ BoxOnGoal; Empty; BoxOnGoal ] ]

        input |> Parse.toLevel |> should equal expectation

    [<Fact>]
    let ``Converting known level format character produces corresponding square`` () = 
        
        // all known mappings, except for the Space -> (Floor or Empty)
        // that is implicitly handled in the other tests
        let knownMappings = 
            [ [ "#" ], [[ Wall ]];
              [ "@" ], [[ Player ]];
              [ "+" ], [[ PlayerOnGoal ]];
              [ "$" ], [[ Box ]];
              [ "*" ], [[ BoxOnGoal ]];
              [ "." ], [[ Goal ]] ]
            |> Map.ofList
        
        knownMappings |> Map.iter (fun input expectation -> Parse.toLevel input |> should equal (Level <| array2D expectation))

    [<Property>]
    let ``Converting unknown level format character, throws InvalidFormat exception`` (character : char) =

        let validSquareCharacters = [ "#"; "@"; "+"; "$"; "*"; "."; " " ]

        let input = character.ToString()
        
        not (validSquareCharacters |> List.contains input) ==> 
            lazy (Assert.Throws<InvalidFormatException> (fun () -> Parse.toLevel [ input ] |> ignore) |> ignore)