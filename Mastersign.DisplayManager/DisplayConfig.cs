using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mastersign.DisplayManager.ConsoleFormat;

namespace Mastersign.DisplayManager
{
    public class DisplayConfiguration
    {
        public DisplayConfigPathInfo[] DisplayPaths { get; set; }

        public DisplayConfigModeInfo[] DisplayModes { get; set; }

        public DisplayConfiguration()
        {
        }

        public DisplayConfiguration(DisplayConfigPathInfo[] displayPaths, DisplayConfigModeInfo[] displayModes)
        {
            DisplayPaths = displayPaths;
            DisplayModes = displayModes;
        }

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return 
                $"DisplayPaths:{nl}{FormatArray(DisplayPaths)}{nl}" +
                $"DisplayModes:{nl}{FormatArray(DisplayModes)}";
        }
    }
}
