﻿using System.Collections.Generic;
using System.IO;

namespace DotNetCliPerf
{
    public class ClassLibCore : CoreApp
    {
        protected override string SourceDir => Path.Combine("classlib", "core");

        protected override string SourcePath => Path.Combine("classlib", "Class1.cs");

        protected override string ExpectedOutput => $"Actual:<{NewValue}>";

        protected override IEnumerable<string> CleanPaths => new string[]
        {
            Path.Combine("classlib", "bin"),
            Path.Combine("classlib", "obj"),
            Path.Combine("mstest", "bin"),
            Path.Combine("mstest", "obj"),
        };

        protected override string Run(bool first = false)
        {
            if (MSBuildVersion == MSBuildVersion.Desktop)
            {
                Build(first);
                return Run(restore: false, build: false);
            }
            else
            {
                return Run(restore: first || Restore);
            }
        }

        private string Run(bool restore, bool build = true)
        {
            return DotNet("test mstest" + (build ? "" : " --no-build"), restore: restore, throwOnError: false);
        }

    }
}
