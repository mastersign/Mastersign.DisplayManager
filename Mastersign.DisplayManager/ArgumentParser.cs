using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    class ArgumentParser
    {
        private static readonly string[] HELP_FLAGS = new[] { "-h", "-?", "/?", "--help" };
        private static readonly string[] VERSION_FLAGS = new[] { "-v", "--version" };
        private static readonly string[] SHOW_CONFIG_FLAGS = new[] { "-i", "--info", "--show-config" };
        private static readonly string[] RECORD_OPTIONS = new[] { "-s", "--save", "--record" };
        private static readonly string[] RESTORE_OPTIONS = new[] { "-l", "--load", "--restore" };
        private static readonly string[] PERSISTENT_RESTORE_FLAGS = new[] { "-p", "--persistent" };
        private static readonly string[] RESET_FLAGS = new[] { "-r", "--reset" };

        public string[] Arguments { get; private set; }

        public ArgumentParser(string[] args)
        {
            Arguments = args;
        }

        public StartInfo GetStartInfo()
        {
            var showHelp = default(bool);
            var showVersion = default(bool);
            var showConfig = default(bool);
            var recordTarget = default(string);
            var restoreSource = default(string);
            var persistentRestore = default(bool);
            var reset = default(bool);

            var recognizedValidOption = false;
            for (int i = 0; i < Arguments.Length; i++)
            {
                switch (Arguments[i])
                {
                    case var flag when HELP_FLAGS.Contains(flag):
                        showHelp = true;
                        recognizedValidOption = true;
                        break;
                    case var flag when VERSION_FLAGS.Contains(flag):
                        showVersion = true;
                        recognizedValidOption = true;
                        break;
                    case var flag when SHOW_CONFIG_FLAGS.Contains(flag):
                        showConfig = true;
                        recognizedValidOption = true;
                        break;
                    case var flag when RECORD_OPTIONS.Contains(flag):
                        i++;
                        if (i < Arguments.Length)
                        {
                            recordTarget = Arguments[i];
                            recognizedValidOption = true;
                        }
                        break;
                    case var flag when RESTORE_OPTIONS.Contains(flag):
                        i++;
                        if (i < Arguments.Length)
                        {
                            restoreSource = Arguments[i];
                            recognizedValidOption = true;
                        }
                        break;
                    case var flag when PERSISTENT_RESTORE_FLAGS.Contains(flag):
                        persistentRestore = true;
                        recognizedValidOption = true;
                        break;
                    case var flag when RESET_FLAGS.Contains(flag):
                        reset = true;
                        recognizedValidOption = true;
                        break;
                }
            }

            if (!recognizedValidOption && Arguments.Length == 1)
            {
                restoreSource = Arguments[0];
                persistentRestore = true;
            }

            return new StartInfo(
                showHelp,
                showVersion,
                showConfig,
                recordTarget,
                restoreSource,
                persistentRestore,
                reset);
        }
    }
}
