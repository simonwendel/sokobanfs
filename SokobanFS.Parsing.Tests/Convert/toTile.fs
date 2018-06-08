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

namespace SokobanFS.Parsing.Tests.Convert

module toTile =
    
    open FsCheck
    open FsUnit
    open Xunit
    
    open SokobanFS.Game.MapsTypes
    open SokobanFS.Parsing
    
    [<Fact>]
    let ``Converting known level format character produces corresponding tile`` () = 
        
        let knownMappings = 
            [ '#', Wall;
              '@', Player;
              '+', PlayerOnGoal;
              '$', Box;
              '*', BoxOnGoal;
              '.', Goal;
              ' ', Floor;]
            |> Map.ofList
        
        knownMappings |> Map.iter (fun character tile -> Convert.toTile character |> should equal tile)

    [<Property>]
    let ``Converting unknown level format character produces Empty tile`` (character:char) =

        let validTileCharacters = [ '#'; '@'; '+'; '$'; '*'; '.'; ' ' ]
        
        not (validTileCharacters |> List.contains character) ==> (Convert.toTile character = Empty)