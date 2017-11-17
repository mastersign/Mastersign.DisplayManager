using Mastersign.DisplayManager.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastersign.DisplayManager
{
    partial class MonitorDevice
    {
        public MonitorDevice(uint id, DISPLAY_DEVICE device)
        {
            Id = id;
            DeviceID = device.ddDeviceID;
            DeviceName = device.ddDeviceName;
            DeviceKey = device.ddDeviceKey;
            DeviceString = device.ddDeviceString;
            StateFlags = device.ddStateFlags;
        }

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return
                $"Name: {DeviceName}{nl}" +
                $"Num:    {Id}{nl}" +
                $"String: {DeviceString}{nl}" +
                $"Key:    {DeviceKey}{nl}" +
                $"ID:     {DeviceID}{nl}" +
                $"State:  {StateFlags}";
        }
    }
}
