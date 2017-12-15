using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    class StartInfo
    {
        public bool ShowHelp { get; private set; }

        public bool ShowVersion { get; private set; }

        public bool ShowConfig { get; private set; }

        public string RecordTargetPath { get; private set; }

        public string RestoreSourcePath { get; private set; }

        public bool PersistentRestore { get; private set; }

        public bool Reset { get; private set; }

        public StartInfo(
            bool showHelp,
            bool showVersion,
            bool showConfig, 
            string recordTargetPath, 
            string restoreTargetPath, 
            bool persistentRestore,
            bool reset)
        {
            ShowHelp = showHelp;
            ShowVersion = showVersion;
            ShowConfig = showConfig;
            RecordTargetPath = recordTargetPath;
            RestoreSourcePath = restoreTargetPath;
            PersistentRestore = persistentRestore;
            Reset = reset;
        }
    }
}
