using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using static Mastersign.DisplayManager.ConsoleFormat;

namespace Mastersign.DisplayManager.WinApi
{
    // https://stackoverflow.com/questions/16082330/communicating-with-windows7-display-api

    static class User32
    {
        [DllImport("User32.dll")]
        public static extern int SetDisplayConfig(
            uint numPathArrayElements,
            [In] DisplayConfigPathInfo[] pathArray,
            uint numModeInfoArrayElements,
            [In] DisplayConfigModeInfo[] modeInfoArray,
            SdcFlags flags
        );

        [DllImport("User32.dll")]
        public static extern int QueryDisplayConfig(
            QueryDisplayFlags flags,
            ref int numPathArrayElements,
            [Out] DisplayConfigPathInfo[] pathInfoArray,
            ref int modeInfoArrayElements,
            [Out] DisplayConfigModeInfo[] modeInfoArray,
            IntPtr z
        );

        [DllImport("User32.dll")]
        public static extern int GetDisplayConfigBufferSizes(QueryDisplayFlags flags, out int numPathArrayElements, out int numModeInfoArrayElements);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LUID
    {
        public uint LowPart;
        public uint HighPart;

        public override string ToString()
        {
            return HighPart.ToString("X8") + ":" + LowPart.ToString("X8");
        }
    }

    [Flags]
    public enum DisplayConfigVideoOutputTechnology : uint
    {
        Other = 4294967295, // -1
        Hd15 = 0,
        Svideo = 1,
        CompositeVideo = 2,
        ComponentVideo = 3,
        Dvi = 4,
        Hdmi = 5,
        Lvds = 6,
        DJpn = 8,
        Sdi = 9,
        DisplayportExternal = 10,
        DisplayportEmbedded = 11,
        UdiExternal = 12,
        UdiEmbedded = 13,
        Sdtvdongle = 14,
        Internal = 0x80000000,
        ForceUint32 = 0xFFFFFFFF
    }

    [Flags]
    public enum SdcFlags : uint
    {
        Zero = 0,

        TopologyInternal = 0x00000001,
        TopologyClone = 0x00000002,
        TopologyExtend = 0x00000004,
        TopologyExternal = 0x00000008,
        TopologySupplied = 0x00000010,

        UseSuppliedDisplayConfig = 0x00000020,
        Validate = 0x00000040,
        Apply = 0x00000080,
        NoOptimization = 0x00000100,
        SaveToDatabase = 0x00000200,
        AllowChanges = 0x00000400,
        PathPersistIfRequired = 0x00000800,
        ForceModeEnumeration = 0x00001000,
        AllowPathOrderChanges = 0x00002000,

        UseDatabaseCurrent = TopologyInternal | TopologyClone | TopologyExtend | TopologyExternal
    }

    [Flags]
    public enum DisplayConfigFlags : uint
    {
        Zero = 0x0,
        PathActive = 0x00000001
    }

    [Flags]
    public enum DisplayConfigSourceStatus : int
    {
        Zero = 0x0,
        InUse = 0x00000001
    }

    [Flags]
    public enum DisplayConfigTargetStatus : uint
    {
        Zero = 0x0,

        InUse = 0x00000001,
        FORCIBLE = 0x00000002,
        ForcedAvailabilityBoot = 0x00000004,
        ForcedAvailabilityPath = 0x00000008,
        ForcedAvailabilitySystem = 0x00000010,
    }

    [Flags]
    public enum DisplayConfigRotation : uint
    {
        Zero = 0x0,

        Identity = 1,
        Rotate90 = 2,
        Rotate180 = 3,
        Rotate270 = 4,
        ForceUint32 = 0xFFFFFFFF
    }

    [Flags]
    public enum DisplayConfigPixelFormat : uint
    {
        Zero = 0x0,

        Pixelformat8Bpp = 1,
        Pixelformat16Bpp = 2,
        Pixelformat24Bpp = 3,
        Pixelformat32Bpp = 4,
        PixelformatNongdi = 5,
        PixelformatForceUint32 = 0xffffffff
    }

    [Flags]
    public enum DisplayConfigScaling : uint
    {
        Zero = 0x0,

        Identity = 1,
        Centered = 2,
        Stretched = 3,
        Aspectratiocenteredmax = 4,
        Custom = 5,
        Preferred = 128,
        ForceUint32 = 0xFFFFFFFF
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigRational
    {
        public uint numerator;
        public uint denominator;
    }

    [Flags]
    public enum DisplayConfigScanLineOrdering : uint
    {
        Unspecified = 0,
        Progressive = 1,
        Interlaced = 2,
        InterlacedUpperfieldfirst = Interlaced,
        InterlacedLowerfieldfirst = 3,
        ForceUint32 = 0xFFFFFFFF
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigPathInfo
    {
        public DisplayConfigPathSourceInfo SourceInfo;
        public DisplayConfigPathTargetInfo TargetInfo;
        public uint Flags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"SourceInfo:{nl}{Indent(SourceInfo, 0, isArrayItem: false)}{nl}" +
                $"TargetInfo:{nl}{Indent(TargetInfo, 0, isArrayItem: false)}";
        }
    }

    [Flags]
    public enum DisplayConfigModeInfoType : uint
    {
        Zero = 0,

        Source = 1,
        Target = 2,
        ForceUint32 = 0xFFFFFFFF
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DisplayConfigModeInfo
    {
        [FieldOffset((0))]
        public DisplayConfigModeInfoType InfoType;

        [FieldOffset(4)]
        public uint Id;

        [FieldOffset(8)]
        public LUID AdapterId;

        [FieldOffset(16)]
        public DisplayConfigTargetMode TargetMode;

        [FieldOffset(16)]
        public DisplayConfigSourceMode SourceMode;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"InfoType: {InfoType}{nl}" +
                $"Id: {Id}{nl}" +
                $"AdapterId: {AdapterId}{nl}" +
                $"TargetMode:{nl}{Indent(TargetMode, 0, isArrayItem: false)}{nl}" +
                $"SourceMode:{nl}{Indent(SourceMode, 0, isArrayItem: false)}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfig2DRegion
    {
        public uint X;
        public uint Y;
    }

    [Flags]
    public enum D3DmdtVideoSignalStandard : uint
    {
        Uninitialized = 0,
        VesaDmt = 1,
        VesaGtf = 2,
        VesaCvt = 3,
        Ibm = 4,
        Apple = 5,
        NtscM = 6,
        NtscJ = 7,
        Ntsc443 = 8,
        PalB = 9,
        PalB1 = 10,
        PalG = 11,
        PalH = 12,
        PalI = 13,
        PalD = 14,
        PalN = 15,
        PalNc = 16,
        SecamB = 17,
        SecamD = 18,
        SecamG = 19,
        SecamH = 20,
        SecamK = 21,
        SecamK1 = 22,
        SecamL = 23,
        SecamL1 = 24,
        Eia861 = 25,
        Eia861A = 26,
        Eia861B = 27,
        PalK = 28,
        PalK1 = 29,
        PalL = 30,
        PalM = 31,
        Other = 255
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigVideoSignalInfo
    {
        public long PixelRate;
        public DisplayConfigRational HSyncFreq;
        public DisplayConfigRational VSyncFreq;
        public DisplayConfig2DRegion ActiveSize;
        public DisplayConfig2DRegion TotalSize;

        public uint videoStandard;
        public uint scanLineOrdering;

        public D3DmdtVideoSignalStandard VideoStandard => (D3DmdtVideoSignalStandard)videoStandard;
        public DisplayConfigScanLineOrdering ScanLineOrdering => (DisplayConfigScanLineOrdering)scanLineOrdering;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"PixelRate: {PixelRate}{nl}" +
                $"HSyncFreq: {HSyncFreq.numerator}/{HSyncFreq.denominator}{nl}" +
                $"VSyncFreq: {VSyncFreq.numerator}/{VSyncFreq.denominator}{nl}" +
                $"ActiveSize: {ActiveSize.X}, {ActiveSize.Y}{nl}" +
                $"TotalSize: {TotalSize.X}, {TotalSize.Y}{nl}" +
                $"VideoStandard: {VideoStandard}{nl}" +
                $"ScanLineOrdering: {ScanLineOrdering}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigTargetMode
    {
        public DisplayConfigVideoSignalInfo TargetVideoSignalInfo;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"TargetVideoSignalInfo:{nl}{Indent(TargetVideoSignalInfo, 0, isArrayItem: false)}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PointL
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigSourceMode
    {
        public uint Width;
        public uint Height;
        public uint pixelFormat;
        public PointL Position;

        public DisplayConfigPixelFormat PixelFormat => (DisplayConfigPixelFormat)pixelFormat;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"Size: {Width}, {Height}{nl}" +
                $"PixelFormat: {PixelFormat}{nl}" +
                $"Position: {Position.X}, {Position.Y}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigPathSourceInfo
    {
        public LUID AdapterId;
        public uint Id;
        public uint ModeInfoIdx;
        public int statusFlags;

        public DisplayConfigSourceStatus StatusFlags => (DisplayConfigSourceStatus)statusFlags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"AdapterId: {AdapterId}{nl}" +
                $"Id: {Id}{nl}" +
                $"ModeInfoIdx: {ModeInfoIdx}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigPathTargetInfo
    {
        public LUID AdapterId;
        public uint Id;
        public uint ModeInfoIdx;
        public uint outputTechnology;
        public uint rotation;
        public uint scaling;
        public DisplayConfigRational RefreshRate;
        public uint scanLineOrdering;

        public bool TargetAvailable;
        public uint statusFlags;

        public DisplayConfigVideoOutputTechnology OutputTechnology => (DisplayConfigVideoOutputTechnology)outputTechnology;
        public DisplayConfigRotation Rotation => (DisplayConfigRotation)rotation;
        public DisplayConfigScaling Scaling => (DisplayConfigScaling)scaling;
        public DisplayConfigScanLineOrdering ScanLineOrdering => (DisplayConfigScanLineOrdering)scanLineOrdering;
        public DisplayConfigTargetStatus StatusFlags => (DisplayConfigTargetStatus)statusFlags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"AdapterId: {AdapterId}{nl}" +
                $"Id: {Id}{nl}" +
                $"ModeInfoIdx: {ModeInfoIdx}{nl}" +
                $"OutputTechnology: {OutputTechnology}{nl}" +
                $"Rotation: {Rotation}{nl}" +
                $"Scaling: {Scaling}{nl}" +
                $"RefreshRate: {RefreshRate.numerator}/{RefreshRate.denominator}{nl}" +
                $"ScanLineOrdering: {ScanLineOrdering}{nl}" +
                $"TargetAvailable: {TargetAvailable}{nl}" +
                $"StatusFlags: {StatusFlags}";
        }
    }

    [Flags]
    public enum QueryDisplayFlags : uint
    {
        Zero = 0x0,

        AllPaths = 0x00000001,
        OnlyActivePaths = 0x00000002,
        DatabaseCurrent = 0x00000004
    }

    [Flags]
    public enum DisplayConfigTopologyId : uint
    {
        Zero = 0x0,

        Internal = 0x00000001,
        Clone = 0x00000002,
        Extend = 0x00000004,
        External = 0x00000008,
        ForceUint32 = 0xFFFFFFFF
    }
}
