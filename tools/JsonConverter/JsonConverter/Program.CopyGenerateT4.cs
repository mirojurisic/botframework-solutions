﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Text;

namespace JsonConverter
{
    partial class Program
    {
        private static readonly string ttContent = @"
<#@ template debug=""false"" hostspecific=""true"" language=""C#"" #>
<#@ output extension="".cs"" #>
<#@ include file=""{0}""#>";

        public void CopyGenerateT4(params string[] folders)
        {
            var destFolder = GetFullPath(folders);
            Directory.CreateDirectory(destFolder);
            var target = Path.Combine(destFolder, "LgIdCollection.t4");
            try
            {
                File.Copy("LgIdCollection.t4", target, false);
            }
            catch (IOException ex)
            {
                Console.Write($"{target} already exists! {ex.Message}");
            }

            foreach (var file in convertedActivityFiles)
            {
                var ttFile = Path.ChangeExtension(file, "ttnew");
                var relative = Path.GetRelativePath(Path.GetDirectoryName(file), target);
                var content = string.Format(ttContent, relative);
                using(var sw = new StreamWriter(ttFile))
                {
                    sw.Write(content);
                }
            }
        }
    }
}