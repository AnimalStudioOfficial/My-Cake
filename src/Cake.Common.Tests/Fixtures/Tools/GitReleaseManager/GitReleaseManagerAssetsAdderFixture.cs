// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Common.Tools.GitReleaseManager.AddAssets;

namespace Cake.Common.Tests.Fixtures.Tools.GitReleaseManager
{
    internal sealed class GitReleaseManagerAssetsAdderFixture : GitReleaseManagerFixture<GitReleaseManagerAddAssetsSettings>
    {
        private bool _useToken = false;

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }
        public string Owner { get; set; }
        public string Repository { get; set; }
        public string TagName { get; set; }
        public string Assets { get; set; }

        public GitReleaseManagerAssetsAdderFixture()
        {
            UserName = "bob";
            Password = "password";
            Token = "token";
            Owner = "repoOwner";
            Repository = "repo";
            TagName = "0.1.0";
            Assets = @"/temp/asset1.txt";
        }

        public void UseToken()
        {
            _useToken = true;
        }

        protected override void RunTool()
        {
            var tool = new GitReleaseManagerAssetsAdder(FileSystem, Environment, ProcessRunner, Tools);

            if (_useToken)
            {
                tool.AddAssets(Token, Owner, Repository, TagName, Assets, Settings);
            }
            else
            {
                tool.AddAssets(UserName, Password, Owner, Repository, TagName, Assets, Settings);
            }
        }
    }
}
