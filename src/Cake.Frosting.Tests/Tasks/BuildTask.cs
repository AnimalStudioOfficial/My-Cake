// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Core;

namespace Cake.Frosting.Tests
{
    [IsDependentOn(typeof(CleanTask))]
    public sealed class BuildTask : FrostingTask<ICakeContext>
    {
    }
}