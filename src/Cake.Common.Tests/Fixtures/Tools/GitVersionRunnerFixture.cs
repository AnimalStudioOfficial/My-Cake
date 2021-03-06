// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Cake.Common.Tools.GitVersion;
using Cake.Core.Diagnostics;
using Cake.Testing.Fixtures;
using NSubstitute;

namespace Cake.Common.Tests.Fixtures.Tools
{
    internal sealed class GitVersionRunnerFixture : ToolFixture<GitVersionSettings>
    {
        public ICakeLog Log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public GitVersionRunnerFixture(ICollection<string> standardOutput = null)
             : base("GitVersion.exe")
        {
            if (standardOutput == null)
            {
                var resultJson = new GitVersion
                {
                    Major = 1,
                    Minor = 0,
                    Patch = 0
                };

                var serializer = new DataContractJsonSerializer(typeof(GitVersion));
                using (var memoryStream = new MemoryStream())
                using (var reader = new StreamReader(memoryStream))
                {
                    serializer.WriteObject(memoryStream, resultJson);
                    memoryStream.Position = 0;
                    var output = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        output.Add(reader.ReadLine());
                    }
                    standardOutput = output;
                }
            }

            ProcessRunner.Process.SetStandardOutput(standardOutput);
            Log = Substitute.For<ICakeLog>();
            Log.Verbosity = Verbosity.Normal;
        }

        public void SetLogVerbosity(Verbosity verbosity)
        {
            Log.Verbosity = verbosity;
        }

        protected override void RunTool()
        {
            RunGitVersion();
        }

        public GitVersion RunGitVersion()
        {
            var tool = new GitVersionRunner(FileSystem, Environment, ProcessRunner, Tools, Log);
            return tool.Run(Settings);
        }
    }
}