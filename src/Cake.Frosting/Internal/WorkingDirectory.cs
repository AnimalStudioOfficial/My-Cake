// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Cake.Core.IO;

namespace Cake.Frosting.Internal
{
    internal sealed class WorkingDirectory
    {
        public DirectoryPath Path { get; }

        public WorkingDirectory(DirectoryPath path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }
    }
}
