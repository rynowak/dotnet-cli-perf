﻿using System.Collections.Generic;
using System.IO;

namespace DotNetCliPerf
{
    public class WebAppCore : CoreApp
    {       
        protected override string SourceDir => Path.Combine("web", "core");

        protected override string SourcePath => Path.Combine(RootTempDir, "mvc", "Controllers", "HomeController.cs");

        protected override string ExpectedOutput => $"<title>{NewValue}";

        protected override IEnumerable<string> CleanPaths => new string[]
        {
            Path.Combine("mvc", "bin"),
            Path.Combine("mvc", "obj"),
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
            return DotNet(
                "run" + (build ? "" : " --no-build"),
                appArguments: "--mode=singleRequest",
                restore: restore,
                workingSubDirectory: "mvc",
                throwOnError: false);
        }
    }
}
