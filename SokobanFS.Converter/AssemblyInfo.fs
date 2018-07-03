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

namespace SokobanFS.Parsing.AssemblyInfo

open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<assembly: AssemblyTitle("SokobanFS Level Data Converter")>]
[<assembly: AssemblyDescription("Application converting common level format Sokoban levels to SokobanFS format.")>]

[<assembly: AssemblyProduct("SokobanFS")>]
[<assembly: AssemblyCopyright("Copyright © Simon Wendel 2018")>]

[<assembly: ComVisible(false)>]
[<assembly: Guid("bb80bf28-f13e-4714-a72f-78d13691cb33")>]

[<assembly: AssemblyVersion("0.1.*")>]
[<assembly: AssemblyFileVersion("0.1.*")>]

[<assembly: InternalsVisibleTo("SokobanFS.Converter.Tests")>]

do
    ()
