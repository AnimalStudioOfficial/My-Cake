// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cake.Core.IO
{
    internal sealed class Directory : IDirectory
    {
        private readonly DirectoryInfo _directory;

        public DirectoryPath Path { get; }

        Path IFileSystemInfo.Path => Path;

        public bool Exists => _directory.Exists;

        public bool Hidden => (_directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

        public DateTime LastWriteTime => _directory.LastWriteTime;

        public Directory(DirectoryPath path)
        {
            Path = path;
            _directory = new DirectoryInfo(Path.FullPath);
        }

        public void Create()
        {
            _directory.Create();
        }

        public void Move(DirectoryPath destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            _directory.MoveTo(destination.FullPath);
        }

        public void Delete(bool recursive)
        {
            
            _directory.Delete(recursive); // Delete the directory recursively.
        }

        // Returns a list of directories matching the filter.
        public IEnumerable<IDirectory> GetDirectories(string filter, SearchScope scope)
        {
            var option = scope == SearchScope.Current ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            return _directory.GetDirectories(filter, option)
                .Select(directory => new Directory(directory.FullName));
        }

        // Returns a list of files matching the given filter.
        public IEnumerable<IFile> GetFiles(string filter, SearchScope scope)
        {
            var option = scope == SearchScope.Current ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            return _directory.GetFiles(filter, option)
                .Select(file => new File(file.FullName));
        }
    }
}