using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mastersign.DisplayManager
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                return ShowConfig(QueryDisplayFlags.OnlyActivePaths);
            }
            var mode = args[0];
            switch (mode)
            {
                case "record":
                    if (args.Length != 2)
                    {
                        Console.Error.WriteLine("Expected a target filename (*.xml) as second command line argument.");
                        return -1;
                    }
                    return Record(args[1]);
                case "restore":
                    if (args.Length != 2)
                    {
                        Console.Error.WriteLine("Expected a source filename (*.xml) as second command line argument.");
                        return -1;
                    }
                    return Restore(args[1]);
                default:
                    Console.Error.WriteLine("Invalid mode. Expected 'record' or 'restore'.");
                    return -1;
            }
        }

        static int SafeExecution(Action a)
        {
            try
            {
                a();
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        static int ShowConfig(QueryDisplayFlags flags) => SafeExecution(() =>
        {
            Console.WriteLine(Manager.QueryDisplayConfig(flags));
        });

        static int Record(string fileName) => SafeExecution(() =>
        {
            var config = Manager.QueryDisplayConfig(QueryDisplayFlags.OnlyActivePaths);
            var s = new XmlSerializer(typeof(DisplayConfiguration));
            using (var f = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                s.Serialize(f, config);
            }
        });

        static int Restore(string fileName) => SafeExecution(() =>
        {
            var s = new XmlSerializer(typeof(DisplayConfiguration));
            DisplayConfiguration config;
            using (var f = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                config = (DisplayConfiguration)s.Deserialize(f);
            }
            Manager.SetDisplayConfig(config);
        });
    }
}
