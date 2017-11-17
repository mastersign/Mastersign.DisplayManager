using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    static class Manager
    {
        public static DisplayConfiguration QueryDisplayConfig(QueryDisplayFlags flags)
        {
            int numPathArrayElements;
            int numModeInfoArrayElements;

            var queryErrorCode = User32.GetDisplayConfigBufferSizes(flags,
                out numPathArrayElements, out numModeInfoArrayElements);
            if (queryErrorCode == 0)
            {
                var pathInfoArray = new DisplayConfigPathInfo[numPathArrayElements];
                var modeInfoArray = new DisplayConfigModeInfo[numModeInfoArrayElements];

                queryErrorCode = User32.QueryDisplayConfig(flags,
                                        ref numPathArrayElements, pathInfoArray, 
                                        ref numModeInfoArrayElements, modeInfoArray,
                                        IntPtr.Zero);

                return queryErrorCode == 0 ? new DisplayConfiguration(pathInfoArray, modeInfoArray) : null;
            }
            return null;
        }

        public static bool SetDisplayConfig(DisplayConfiguration config)
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
    }
}
