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

namespace SokobanFS.Lib.AssemblyInfo

open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<assembly: AssemblyTitle("SokobanFS library of utility functions")>]
[<assembly: AssemblyDescription("Non game-specific functions.")>]

[<assembly: AssemblyProduct("SokobanFS")>]
[<assembly: AssemblyCopyright("Copyright © Simon Wendel 2018-2021")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("96cfc20e-8d5a-4a5b-8c4f-fb03c93d4cd1")>]

[<assembly: AssemblyVersion("0.1.*")>]
[<assembly: AssemblyFileVersion("0.1.*")>]

[<assembly: InternalsVisibleTo("SokobanFS.Lib.Tests")>]

do ()
