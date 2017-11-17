using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using static Mastersign.DisplayManager.ConsoleFormat;

namespace Mastersign.DisplayManager
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("At least one command line argument expected: show|record|restore|list-devices|list-settings");
                return -1;
            }
            var mode = args[0];
            switch (mode)
            {
                case "show":
                    return ShowConfig(QueryDisplayFlags.OnlyActivePaths);
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
                case "list-devices":
                    return ListDevices();
                case "list-settings":
                    if (args.Length != 2)
                    {
                        Console.Error.WriteLine("Expected display name as second command line argument.");
                        return -1;
                    }
                    var deviceName = args[1];
                    if (!deviceName.StartsWith(@"\\"))
                    {
                        deviceName = @"\\.\" + deviceName;
                    }
                    return ListSettings(deviceName);
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
            var s = new XmlSerializer(typeof(DisplayConfig));
            using (var f = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                s.Serialize(f, config);
            }
        });

        static int Restore(string fileName) => SafeExecution(() =>
        {
            var s = new XmlSerializer(typeof(DisplayConfig));
            DisplayConfig config;
            using (var f = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                config = (DisplayConfig)s.Deserialize(f);
            }
            Manager.SetDisplayConfig(config);
        });

        static int ListDevices() => SafeExecution(() =>
        {
            foreach (var display in Manager.EnumerateDisplayDevices())
            {
                Console.WriteLine(Indent(display.ToString()));
                Console.WriteLine(Indent("Monitors:", 0, isArrayItem: false));
                foreach (var monitor in Manager.EnumerateMonitorDevices(display.DeviceName))
                {
                    Console.WriteLine(Indent(monitor.ToString(), 1));
                }
            }
        });

        static int ListSettings(string deviceName) => SafeExecution(() =>
        {
            WriteList(() => Manager.EnumerateDisplaySettings(deviceName));
        });

        public static void SetAsPrimaryMonitor(uint id)
        {
            var device = new DISPLAY_DEVICE();
            var deviceMode = new DEVMODE();
            device.ddSize = Marshal.SizeOf(device);

            User32.EnumDisplayDevices(null, id, ref device, 0);
            User32.EnumDisplaySettings(device.ddDeviceName, -1, ref deviceMode);
            var offsetx = deviceMode.dmPosition.x;
            var offsety = deviceMode.dmPosition.y;
            deviceMode.dmPosition.x = 0;
            deviceMode.dmPosition.y = 0;

            User32.ChangeDisplaySettingsEx(
                device.ddDeviceName,
                ref deviceMode,
                (IntPtr)null,
                (ChangeDisplaySettingsFlags.SetPrimary | ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset),
                IntPtr.Zero);

            device = new DISPLAY_DEVICE();
            device.ddSize = Marshal.SizeOf(device);

            // Update remaining devices
            for (uint otherid = 0; User32.EnumDisplayDevices(null, otherid, ref device, 0); otherid++)
            {
                if (device.ddStateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop) && otherid != id)
                {
                    device.ddSize = Marshal.SizeOf(device);
                    var otherDeviceMode = new DEVMODE();

                    User32.EnumDisplaySettings(device.ddDeviceName, -1, ref otherDeviceMode);

                    otherDeviceMode.dmPosition.x -= offsetx;
                    otherDeviceMode.dmPosition.y -= offsety;

                    User32.ChangeDisplaySettingsEx(
                        device.ddDeviceName,
                        ref otherDeviceMode,
                        (IntPtr)null,
                        (ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.NoReset),
                        IntPtr.Zero);

                }

                device.ddSize = Marshal.SizeOf(device);
            }

            // Apply settings
            User32.ChangeDisplaySettingsEx(null, IntPtr.Zero, (IntPtr)null, ChangeDisplaySettingsFlags.None, (IntPtr)null);
        }
    }
}
