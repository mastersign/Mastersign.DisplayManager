using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    static class Manager
    {
        public static DisplayConfig QueryDisplayConfig(QueryDisplayFlags flags)
        {
            int numPathArrayElements;
            int numModeInfoArrayElements;

            var queryErrorCode = User32.GetDisplayConfigBufferSizes(flags,
                out numPathArrayElements, out numModeInfoArrayElements);
            if (queryErrorCode == 0)
            {
                var pathInfoArray = new DisplayConfigPathInfo[numPathArrayElements];
                var modeInfoArray = new DisplayConfigModeInfo[numModeInfoArrayElements];

                //var first = Marshal.SizeOf(new DisplayConfigPathInfo());
                //var second = Marshal.SizeOf(new DisplayConfigModeInfo());

                queryErrorCode = User32.QueryDisplayConfig(flags,
                                        ref numPathArrayElements, pathInfoArray, 
                                        ref numModeInfoArrayElements, modeInfoArray,
                                        IntPtr.Zero);

                return queryErrorCode == 0 ? new DisplayConfig(pathInfoArray, modeInfoArray) : null;
            }
            return null;
        }

        public static bool SetDisplayConfig(DisplayConfig config)
        {
            var errorCode = User32.SetDisplayConfig(
                (uint)config.DisplayPaths.Length, config.DisplayPaths,
                (uint)config.DisplayModes.Length, config.DisplayModes,
                SdcFlags.Apply | SdcFlags.UseSuppliedDisplayConfig);
            if (errorCode != 0)
            {
                Console.Error.WriteLine("SetDisplayConfig Error: " + errorCode);
            }
            return errorCode == 0;
        }

        public static IEnumerable<DisplayDevice> EnumerateDisplayDevices()
        {
            var device = new DISPLAY_DEVICE();
            device.ddSize = Marshal.SizeOf(device);
            for (uint id = 0; id < uint.MaxValue; id++)
            {
                if (!User32.EnumDisplayDevices(null, id, ref device, 0)) break;
                yield return new DisplayDevice(id, device);
            }
        }

        public static IEnumerable<MonitorDevice> EnumerateMonitorDevices(string displayDeviceName)
        {
            var device = new DISPLAY_DEVICE();
            device.ddSize = Marshal.SizeOf(device);
            for (uint id = 0; id < uint.MaxValue; id++)
            {
                if (!User32.EnumDisplayDevices(displayDeviceName, id, ref device, User32.EDD_GET_DEVICE_INTERFACE_NAME)) break;
                yield return new MonitorDevice(id, device);
            }
        }

        public static DisplaySetting GetCurrentDisplaySetting(string deviceName)
        {
            var devMode = new DEVMODE();
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            if (!User32.EnumDisplaySettingsEx(deviceName, User32.EDS_ENUM_CURRENT_SETTINGS, ref devMode, 0))
            {
                return null;
            }
            return new DisplaySetting(User32.EDS_ENUM_CURRENT_SETTINGS, devMode);
        }

        public static DisplaySetting GetDisplaySettingFromRegistry(string deviceName)
        {
            var devMode = new DEVMODE();
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            if (!User32.EnumDisplaySettingsEx(deviceName, User32.EDS_ENUM_REGISTRY_SETTINGS, ref devMode, 0))
            {
                return null;
            }
            return new DisplaySetting(User32.EDS_ENUM_REGISTRY_SETTINGS, devMode);
        }

        public static IEnumerable<DisplaySetting> EnumerateDisplaySettings(string deviceName)
        {
            var devMode = new DEVMODE();
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            for (int num = -2; num < 1024; num++)
            {
                if (!User32.EnumDisplaySettingsEx(deviceName, num, ref devMode, 0)) break;
                yield return new DisplaySetting(num, devMode);
            }
        }
    }
}
