using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static void PatchDisplayConfig(DisplayConfiguration config)
        {
            var lookupConfig = QueryDisplayConfig(QueryDisplayFlags.AllPaths);
            var idCache = new Dictionary<uint, LUID>();
            for (int i = 0; i < config.DisplayPaths.Length; i++)
            {
                uint id;
                LUID adapterId;

                id = config.DisplayPaths[i].SourceInfo.Id;
                adapterId = config.DisplayPaths[i].SourceInfo.AdapterId;
                config.DisplayPaths[i].SourceInfo.AdapterId = LookupAdapterId(lookupConfig, idCache, id, adapterId);

                id = config.DisplayPaths[i].TargetInfo.Id;
                adapterId = config.DisplayPaths[i].TargetInfo.AdapterId;
                config.DisplayPaths[i].TargetInfo.AdapterId = LookupAdapterId(lookupConfig, idCache, id, adapterId);
            }
            for (int i = 0; i < config.DisplayModes.Length; i++)
            {
                uint id;
                LUID adapterId;

                id = config.DisplayModes[i].Id;
                adapterId = config.DisplayModes[i].AdapterId;
                config.DisplayModes[i].AdapterId = LookupAdapterId(lookupConfig, idCache, id, adapterId);
            }
        }

        private static LUID LookupAdapterId(DisplayConfiguration config, Dictionary<uint, LUID> idCache, uint id, LUID oldAdapterId)
        {
            if (idCache.TryGetValue(id, out LUID adapterId)) return adapterId;
            adapterId = FindAdapterId(config, id) ?? FindAdapterId(config, 0) ?? oldAdapterId;
            idCache[id] = adapterId;
            return adapterId;
        }

        private static LUID? FindAdapterId(DisplayConfiguration config, uint id)
        {
            var matchingTargetPath = config.DisplayPaths
                .Cast<DisplayConfigPathInfo?>()
                .FirstOrDefault(p => p.Value.TargetInfo.Id == id);
            if (matchingTargetPath.HasValue)
            {
                var adapterId = matchingTargetPath.Value.TargetInfo.AdapterId;
                Debug.WriteLine($"TARGET PATH MATCH: Id {id} -> AdapterId {adapterId}");
                return adapterId;
            }
            var matchingSourcePath = config.DisplayPaths
                .Cast<DisplayConfigPathInfo?>()
                .FirstOrDefault(p => p.Value.SourceInfo.Id == id);
            if (matchingSourcePath.HasValue)
            {
                var adapterId = matchingSourcePath.Value.SourceInfo.AdapterId;
                Debug.WriteLine($"SOURCE PATH MATCH: Id {id} -> AdapterId {adapterId}");
                return adapterId;
            }
            Debug.WriteLine($"No MATCH: Id {id}");
            return null;
        }
    }
}
