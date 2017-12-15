using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Mastersign.DisplayManager
{
    class Program
    {
        static int Main(string[] args)
        {
            var argParser = new ArgumentParser(args);
            var startInfo = argParser.GetStartInfo();

            if (startInfo.ShowHelp)
            {
                PrintHelp();
                return 0;
            }
            if (startInfo.ShowVersion)
            {
                PrintVersionInfo();
                return 0;
            }

            var success = true;
            if (startInfo.ShowConfig || startInfo.RecordTargetPath == null || startInfo.RestoreSourcePath == null)
            {
                success = success && ShowConfig(QueryDisplayFlags.OnlyActivePaths);
            }
            if (startInfo.RecordTargetPath != null)
            {
                success = success && Record(startInfo.RecordTargetPath);
            }
            if (startInfo.RestoreSourcePath != null)
            {
                success = success && Restore(startInfo.RestoreSourcePath, startInfo.PersistentRestore);
            }
            if (startInfo.Reset && startInfo.RestoreSourcePath == null)
            {
                success = success && Reset();
            }
            return success ? 0 : -1;
        }

        static bool SafeExecution(Action a)
        {
            try
            {
                a();
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.Error.WriteLine(ex);
#else
                Console.Error.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        private static void PrintHelp()
        {
            PrintVersionInfo();
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  DisplayMan");
            Console.WriteLine("      No arguments: Show the current display configuration.");
            Console.WriteLine("  DisplayMan <file path>");
            Console.WriteLine("      One file path as the only argument:");
            Console.WriteLine("      Load display configuration from XML file persistently.");
            Console.WriteLine("  DisplayMan [<options>]*");
            Console.WriteLine("      With options: See descriptions below.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine();
            Console.WriteLine("  -h, --help, -?, /?");
            Console.WriteLine("      Does print this help text. Ignores all other options.");
            Console.WriteLine();
            Console.WriteLine("  -v, --version");
            Console.WriteLine("      Prints version information. Ignores all other options.");
            Console.WriteLine();
            Console.WriteLine("  -i, --info, --show-config");
            Console.WriteLine("      Show the current display configuration.");
            Console.WriteLine();
            Console.WriteLine("  -s, --save, --record <target file>");
            Console.WriteLine("      Write the current display configuration to an XML file.");
            Console.WriteLine();
            Console.WriteLine("  -l, --load, --restore <source file>");
            Console.WriteLine("      Loads the display configuration from the specified XML file.");
            Console.WriteLine("      If this option is used, the --reset option is ignored.");
            Console.WriteLine("      By default, the configuration is loaded temporarily.");
            Console.WriteLine();
            Console.WriteLine("  -p, --persistent");
            Console.WriteLine("      If loading a display configuration, makes the new configuration");
            Console.WriteLine("      persistent.");
            Console.WriteLine();
            Console.WriteLine("  -r, --reset");
            Console.WriteLine("      Resets the display configuration to the last persistent state.");
            Console.WriteLine("      Can be used to switch back after loading a display configuration");
            Console.WriteLine("      temporarily.");
        }

        private static void PrintVersionInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine("Mastersign DisplayManager");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Version: " + version);
        }

        static bool ShowConfig(QueryDisplayFlags flags) => SafeExecution(() =>
        {
            Console.WriteLine(Manager.QueryDisplayConfig(flags));
        });

        static bool Record(string fileName) => SafeExecution(() =>
        {
            var config = Manager.QueryDisplayConfig(QueryDisplayFlags.OnlyActivePaths);
            var s = new XmlSerializer(typeof(DisplayConfiguration));
            using (var f = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                s.Serialize(f, config);
            }
        });

        static bool Restore(string fileName, bool persistent) => SafeExecution(() =>
        {
            var s = new XmlSerializer(typeof(DisplayConfiguration));
            DisplayConfiguration config;
            using (var f = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                config = (DisplayConfiguration)s.Deserialize(f);
            }

            Debug.WriteLine("---- LOADED CONFIGURATION ---------------");
            Debug.WriteLine(config);
            Debug.WriteLine("-----PATCHED CONFIGURATION --------------");
            Manager.PatchDisplayConfig(config);
            Debug.WriteLine(config);

            Manager.SetDisplayConfig(config, persistent);
        });

        static bool Reset() => SafeExecution(() =>
        {
            Manager.ResetDisplayConfig();
        });
    }
}
