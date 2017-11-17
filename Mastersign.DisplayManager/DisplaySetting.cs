using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    partial class DisplaySetting
    {
        public DisplaySetting(int id, DEVMODE devMode)
        {
            Id = id;
            DeviceName = devMode.dmDeviceName;
            Position = new Point(devMode.dmPosition.x, devMode.dmPosition.y);
            Orientation = devMode.dmDisplayOrientation;
            DisplayFixedOutput = devMode.dmDisplayFixedOutput;
            LogPixels = devMode.dmLogPixels;
            BitsPerPel = devMode.dmBitsPerPel;
            PelsWidth = devMode.dmPelsWidth;
            PelsHeight = devMode.dmPelsHeight;
            DisplayFlags = devMode.dmDisplayFlags;
            Nup = devMode.dmNup;
            DisplayFrequency = devMode.dmDisplayFrequency;
        }

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return 
                $"ID: {Id:000}{nl}" +
                $"DeviceName:      {DeviceName}{nl}" +
                $"Position:        {Position}{nl}" +
                $"Orientation:     {Orientation}{nl}" +
                $"FixedOutput:     {DisplayFixedOutput}{nl}" +
                //$"LogPixels:       {LogPixels}{nl}" +
                $"BitsPerPel:      {BitsPerPel}{nl}" +
                $"PelsWidth:       {PelsWidth}{nl}" +
                $"PelsHeight:      {PelsHeight}{nl}" +
                //$"Flags:           {DisplayFlags}{nl}" +
                //$"Nup:             {Nup}{nl}" +
                $"Frequency:       {DisplayFrequency}{nl}" +
                $"Fields:          {Fields}";
        }
    }
}
