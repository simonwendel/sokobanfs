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

namespace SokobanFS.Parsing.AssemblyInfo

open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<assembly: AssemblyTitle("SokobanFS Game Library")>]
[<assembly: AssemblyDescription("Defining the rules and types needed to play Sokoban.")>]

[<assembly: AssemblyProduct("SokobanFS")>]
[<assembly: AssemblyCopyright("Copyright © Simon Wendel 2018-2021")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("6f85b679-e4f3-4a4e-a212-5131d107f651")>]

[<assembly: AssemblyVersion("0.1.*")>]
[<assembly: AssemblyFileVersion("0.1.*")>]

[<assembly: InternalsVisibleTo("SokobanFS.Game.Tests")>]

do
    ()
