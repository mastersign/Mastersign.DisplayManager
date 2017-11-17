using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using static Mastersign.DisplayManager.ConsoleFormat;

namespace Mastersign.DisplayManager.WinApi
{
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

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int iModeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettingsEx(string deviceName, int iModeNum, ref DEVMODE devMode, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern DISP_CHANGE ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, ChangeDisplaySettingsFlags dwflags, IntPtr lParam);

        [DllImport("user32.dll")]
        // A signature for ChangeDisplaySettingsEx with a DEVMODE struct as the second parameter won't allow you to pass in IntPtr.Zero, so create an overload
        public static extern DISP_CHANGE ChangeDisplaySettingsEx(string lpszDeviceName, IntPtr lpDevMode, IntPtr hwnd, ChangeDisplaySettingsFlags dwflags, IntPtr lParam);

        public const int EDD_GET_DEVICE_INTERFACE_NAME = 1;
        public const int EDS_ENUM_CURRENT_SETTINGS = -1;
        public const int EDS_ENUM_REGISTRY_SETTINGS = -2;
    }

    // https://stackoverflow.com/questions/16082330/communicating-with-windows7-display-api

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
    public enum DisplayConfigSourceStatus
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
        public DisplayConfigPathSourceInfo sourceInfo;
        public DisplayConfigPathTargetInfo targetInfo;
        public uint flags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"SourceInfo:{nl}{Indent(sourceInfo, 0, isArrayItem: false)}{nl}" +
                $"TargetInfo:{nl}{Indent(targetInfo, 0, isArrayItem: false)}";
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
        public DisplayConfigModeInfoType infoType;

        [FieldOffset(4)]
        public uint id;

        [FieldOffset(8)]
        public LUID adapterId;

        [FieldOffset(16)]
        public DisplayConfigTargetMode targetMode;

        [FieldOffset(16)]
        public DisplayConfigSourceMode sourceMode;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"InfoType: {infoType}{nl}" +
                $"Id: {id}{nl}" +
                $"AdapterId: {adapterId}{nl}" +
                $"TargetMode:{nl}{Indent(targetMode, 0, isArrayItem: false)}{nl}" +
                $"SourceMode:{nl}{Indent(sourceMode, 0, isArrayItem: false)}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfig2DRegion
    {
        public uint cx;
        public uint cy;
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
        public long pixelRate;
        public DisplayConfigRational hSyncFreq;
        public DisplayConfigRational vSyncFreq;
        public DisplayConfig2DRegion activeSize;
        public DisplayConfig2DRegion totalSize;

        public D3DmdtVideoSignalStandard videoStandard;
        public DisplayConfigScanLineOrdering ScanLineOrdering;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"PixelRate: {pixelRate}{nl}" +
                $"HSyncFreq: {hSyncFreq.numerator}/{hSyncFreq.denominator}{nl}" +
                $"VSyncFreq: {vSyncFreq.numerator}/{vSyncFreq.denominator}{nl}" +
                $"ActiveSize: {activeSize.cx}, {activeSize.cy}{nl}" +
                $"TotalSize: {totalSize.cx}, {totalSize.cy}{nl}" +
                $"VideoStandard: {videoStandard}{nl}" +
                $"ScanLineOrdering: {ScanLineOrdering}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigTargetMode
    {
        public DisplayConfigVideoSignalInfo targetVideoSignalInfo;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"TargetVideoSignalInfo:{nl}{Indent(targetVideoSignalInfo, 0, isArrayItem: false)}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PointL
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigSourceMode
    {
        public uint width;
        public uint height;
        public uint pixelFormat; // DisplayConfigPixelFormat
        public PointL position;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"Size: {width}, {height}{nl}" +
                $"PixelFormat: {(DisplayConfigPixelFormat)pixelFormat}{nl}" +
                $"Position: {position.x}, {position.y}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigPathSourceInfo
    {
        public LUID adapterId;
        public uint id;
        public uint modeInfoIdx;

        public DisplayConfigSourceStatus statusFlags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"AdapterId: {adapterId}{nl}" +
                $"Id: {id}{nl}" +
                $"ModeInfoIdx: {modeInfoIdx}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayConfigPathTargetInfo
    {
        public LUID adapterId;
        public uint id;
        public uint modeInfoIdx;
        public DisplayConfigVideoOutputTechnology outputTechnology;
        public DisplayConfigRotation rotation;
        public DisplayConfigScaling scaling;
        public DisplayConfigRational refreshRate;
        public DisplayConfigScanLineOrdering scanLineOrdering;

        public bool targetAvailable;
        public DisplayConfigTargetStatus statusFlags;

        public override string ToString()
        {
            var nl = Environment.NewLine;
            return $"AdapterId: {adapterId}{nl}" +
                $"Id: {id}{nl}" +
                $"ModeInfoIdx: {modeInfoIdx}{nl}" +
                $"OutputTechnology: {outputTechnology}{nl}" +
                $"Rotation: {rotation}{nl}" +
                $"Scaling: {scaling}{nl}" +
                $"RefreshRate: {refreshRate.numerator}/{refreshRate.denominator}{nl}" +
                $"ScanLineOrdering: {scanLineOrdering}{nl}" +
                $"TargetAvailable: {targetAvailable}{nl}" +
                $"StatusFlags: {statusFlags}";
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

    // http://www.pinvoke.net/default.aspx/Structures/DEVMODE.html
    // https://msdn.microsoft.com/en-us/library/ms812499.aspx
    // https://stackoverflow.com/questions/19943907
    // https://software.intel.com/en-us/articles/windows-8-desktop-app-desktop-rotation-sample-whitepaper

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        [FieldOffset(0)]
        public string dmDeviceName;

        [FieldOffset(32)]
        public Int16 dmSpecVersion;

        [FieldOffset(34)]
        public Int16 dmDriverVersion;

        [FieldOffset(36)]
        public Int16 dmSize;

        [FieldOffset(38)]
        public Int16 dmDriverExtra;

        [FieldOffset(40)]
        public UInt32 dmFields;

        // Printer Settings

        //[FieldOffset(44)]
        //Int16 dmOrientation;

        //[FieldOffset(46)]
        //Int16 dmPaperSize;

        //[FieldOffset(48)]
        //Int16 dmPaperLength;

        //[FieldOffset(50)]
        //Int16 dmPaperWidth;

        //[FieldOffset(52)]
        //Int16 dmScale;

        //[FieldOffset(54)]
        //Int16 dmCopies;

        //[FieldOffset(56)]
        //Int16 dmDefaultSource;

        //[FieldOffset(58)]
        //Int16 dmPrintQuality;

        // Display Settings

        [FieldOffset(44)]
        public POINTL dmPosition;

        [FieldOffset(52)]
        public DMDISPLAYORIENTATION dmDisplayOrientation;

        [FieldOffset(56)]
        public Int32 dmDisplayFixedOutput;

        // Printer Settings

        //[FieldOffset(60)]
        //public DMCOLOR dmColor;

        //[FieldOffset(62)]
        //public DMDUP dmDuplex;

        //[FieldOffset(64)]
        //public short dmYResolution;

        //[FieldOffset(66)]
        //public short dmTTOption;

        //[FieldOffset(68)]
        //public DMCOLLATE dmCollate;

        //[FieldOffset(70)]
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        //public string dmFormName;

        [FieldOffset(102)]
        public Int16 dmLogPixels;

        [FieldOffset(104)]
        public Int32 dmBitsPerPel;

        [FieldOffset(108)]
        public Int32 dmPelsWidth;

        [FieldOffset(112)]
        public Int32 dmPelsHeight;

        // Display Settings

        [FieldOffset(116)]
        public Int32 dmDisplayFlags;

        [FieldOffset(116)]
        public Int32 dmNup;

        [FieldOffset(120)]
        public Int32 dmDisplayFrequency;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINTL
    {
        public int x;
        public int y;
    }

    [Flags]
    public enum DM_FIELDS : UInt32
    {
        DM_ORIENTATION = 0x00000001,
        DM_PAPERSIZE = 0x00000002,
        DM_PAPERLENGTH = 0x00000004,
        DM_PAPERWIDTH = 0x00000008,
        DM_SCALE = 0x00000010,
        DM_POSITION = 0x00000020,
        DM_NUP = 0x00000040,
        DM_DISPLAYORIENTATION = 0x00000080,
        DM_COPIES = 0x00000100,
        DM_DEFAULTSOURCE = 0x00000200,
        DM_PRINTQUALITY = 0x00000400,
        DM_COLOR = 0x00000800,
        DM_DUPLEX = 0x00001000,
        DM_YRESOLUTION = 0x00002000,
        DM_TTOPTION = 0x00004000,
        DM_COLLATE = 0x00008000,
        DM_FORMNAME = 0x00010000,
        DM_LOGPIXELS = 0x00020000,
        DM_BITSPERPEL = 0x00040000,
        DM_PELSWIDTH = 0x00080000,
        DM_PELSHEIGHT = 0x00100000,
        DM_DISPLAYFLAGS = 0x00200000,
        DM_DISPLAYFREQUENCY = 0x00400000,
        DM_ICMMETHOD = 0x00800000,
        DM_ICMINTENT = 0x01000000,
        DM_MEDIATYPE = 0x02000000,
        DM_DITHERTYPE = 0x04000000,
        DM_PANNINGWIDTH = 0x08000000,
        DM_PANNINGHEIGHT = 0x10000000,
        DM_DISPLAYFIXEDOUTPUT = 0x20000000,
    }

    /// <summary>
    /// Display Orientation
    /// </summary>
    public enum DMDISPLAYORIENTATION : Int32
    {
        DMDO_DEFAULT = 0,
        DMDO_90 = 1,
        DMDO_180 = 2,
        DMDO_270 = 3,
    }

    /// <summary>
    /// Switches between color and monochrome on color printers.
    /// </summary>
    public enum DMCOLOR : Int16
    {
        DMCOLOR_UNKNOWN = 0,
        DMCOLOR_MONOCHROME = 1,
        DMCOLOR_COLOR = 2,
    }

    /// <summary>
    /// Selects duplex or double-sided printing for printers capable of duplex printing.
    /// </summary>
    public enum DMDUP : Int16
    {
        /// <summary>
        /// Unknown setting.
        /// </summary>
        DMDUP_UNKNOWN = 0,

        /// <summary>
        /// Normal (nonduplex) printing.
        /// </summary>
        DMDUP_SIMPLEX = 1,

        /// <summary>
        /// Long-edge binding, that is, the long edge of the page is vertical.
        /// </summary>
        DMDUP_VERTICAL = 2,

        /// <summary>
        /// Short-edge binding, that is, the long edge of the page is horizontal.
        /// </summary>
        DMDUP_HORIZONTAL = 3,
    }

    /// <summary>
    /// Specifies whether collation should be used when printing multiple copies.
    /// </summary>
    public enum DMCOLLATE : Int16
    {
        /// <summary>
        /// Do not collate when printing multiple copies.
        /// </summary>
        DMCOLLATE_FALSE = 0,

        /// <summary>
        /// Collate when printing multiple copies.
        /// </summary>
        DMCOLLATE_TRUE = 1
    }

    public enum DISP_CHANGE : Int32
    {
        Successful = 0,
        Restart = 1,
        Failed = -1,
        BadMode = -2,
        NotUpdated = -3,
        BadFlags = -4,
        BadParam = -5,
        BadDualView = -6
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DISPLAY_DEVICE
    {
        [MarshalAs(UnmanagedType.U4)]
        public int ddSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string ddDeviceName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ddDeviceString;

        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags ddStateFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ddDeviceID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ddDeviceKey;
    }

    [Flags]
    public enum DisplayDeviceStateFlags : Int32
    {
        Deactivated = 0x0,

        /// <summary>The device is part of the desktop.</summary>
        AttachedToDesktop = 0x1,

        MultiDriver = 0x2,

        /// <summary>The device is part of the desktop.</summary>
        PrimaryDevice = 0x4,

        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 0x8,

        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 0x10,

        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 0x20,

        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 0x8000000,

        Remote = 0x4000000,

        Disconnect = 0x2000000,
    }

    [Flags()]
    public enum ChangeDisplaySettingsFlags : UInt32
    {
        None = 0,
        UpdateRegistry = 0x00000001,
        Test = 0x00000002,
        FullScreen = 0x00000004,
        Global = 0x00000008,
        SetPrimary = 0x00000010,
        VideoParameters = 0x00000020,
        EnableUnsafeModes = 0x00000100,
        DisableUnsafeModes = 0x00000200,
        Reset = 0x40000000,
        ResetEx = 0x20000000,
        NoReset = 0x10000000
    }
}
