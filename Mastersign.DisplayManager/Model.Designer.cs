using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mastersign.DisplayManager
{
    #region Scaleton Model Designer generated code
    
    public partial class Point : IEquatable<Point>
    {
        public Point()
        {
        }
        
        public Point(Int32 x, Int32 y)
        {
            this._x = x;
            this._y = y;
        }
        
        #region Equatability
        
        public bool Equals(Point o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            return (
                object.Equals(this._x, o._x) && 
                object.Equals(this._y, o._y));
        }
        
        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            if (!(o.GetType() == typeof(Point)))
            {
                return false;
            }
            return this.Equals((Point)o);
        }
        
        public static bool operator ==(Point a, Point b)
        {
            if ((ReferenceEquals(a, null) && ReferenceEquals(b, null)))
            {
                return true;
            }
            if ((ReferenceEquals(a, null) || ReferenceEquals(b, null)))
            {
                return false;
            }
            return a.Equals(b);
        }
        
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        
        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() ^ 
                (!ReferenceEquals(this._x, null) ? this._x.GetHashCode() : 0) ^ 
                (!ReferenceEquals(this._y, null) ? this._y.GetHashCode() : 0));
        }
        
        #endregion
        
        #region Property X
        
        private Int32 _x;
        
        public virtual Int32 X
        {
            get { return _x; }
        }
        
        #endregion
        
        #region Property Y
        
        private Int32 _y;
        
        public virtual Int32 Y
        {
            get { return _y; }
        }
        
        #endregion
    }
    
    public partial class DisplayDevice : IEquatable<DisplayDevice>, INotifyPropertyChanged
    {
        public DisplayDevice()
        {
        }
        
        #region Equatability
        
        public bool Equals(DisplayDevice o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            return object.ReferenceEquals(this, o);
        }
        
        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            if (!(o.GetType() == typeof(DisplayDevice)))
            {
                return false;
            }
            return this.Equals((DisplayDevice)o);
        }
        
        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
        
        #endregion
        
        #region Change Tracking
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        #endregion
        
        #region Property Id
        
        private UInt32 _id;
        
        public event EventHandler IdChanged;
        
        protected virtual void OnIdChanged()
        {
            EventHandler handler = IdChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Id");
        }
        
        public virtual UInt32 Id
        {
            get { return _id; }
            set
            {
                if ((value == _id))
                {
                    return;
                }
                _id = value;
                this.OnIdChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceName
        
        private string _deviceName;
        
        public event EventHandler DeviceNameChanged;
        
        protected virtual void OnDeviceNameChanged()
        {
            EventHandler handler = DeviceNameChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceName");
        }
        
        public virtual string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (string.Equals(value, _deviceName))
                {
                    return;
                }
                _deviceName = value;
                this.OnDeviceNameChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceString
        
        private string _deviceString;
        
        public event EventHandler DeviceStringChanged;
        
        protected virtual void OnDeviceStringChanged()
        {
            EventHandler handler = DeviceStringChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceString");
        }
        
        public virtual string DeviceString
        {
            get { return _deviceString; }
            set
            {
                if (string.Equals(value, _deviceString))
                {
                    return;
                }
                _deviceString = value;
                this.OnDeviceStringChanged();
            }
        }
        
        #endregion
        
        #region Property StateFlags
        
        private global::Mastersign.DisplayManager.WinApi.DisplayDeviceStateFlags _stateFlags;
        
        public event EventHandler StateFlagsChanged;
        
        protected virtual void OnStateFlagsChanged()
        {
            EventHandler handler = StateFlagsChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"StateFlags");
        }
        
        public virtual global::Mastersign.DisplayManager.WinApi.DisplayDeviceStateFlags StateFlags
        {
            get { return _stateFlags; }
            set
            {
                if ((value == _stateFlags))
                {
                    return;
                }
                _stateFlags = value;
                this.OnStateFlagsChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceID
        
        private string _deviceID;
        
        public event EventHandler DeviceIDChanged;
        
        protected virtual void OnDeviceIDChanged()
        {
            EventHandler handler = DeviceIDChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceID");
        }
        
        public virtual string DeviceID
        {
            get { return _deviceID; }
            set
            {
                if (string.Equals(value, _deviceID))
                {
                    return;
                }
                _deviceID = value;
                this.OnDeviceIDChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceKey
        
        private string _deviceKey;
        
        public event EventHandler DeviceKeyChanged;
        
        protected virtual void OnDeviceKeyChanged()
        {
            EventHandler handler = DeviceKeyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceKey");
        }
        
        public virtual string DeviceKey
        {
            get { return _deviceKey; }
            set
            {
                if (string.Equals(value, _deviceKey))
                {
                    return;
                }
                _deviceKey = value;
                this.OnDeviceKeyChanged();
            }
        }
        
        #endregion
    }
    
    public partial class MonitorDevice : IEquatable<MonitorDevice>, INotifyPropertyChanged
    {
        public MonitorDevice()
        {
        }
        
        #region Equatability
        
        public bool Equals(MonitorDevice o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            return object.ReferenceEquals(this, o);
        }
        
        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            if (!(o.GetType() == typeof(MonitorDevice)))
            {
                return false;
            }
            return this.Equals((MonitorDevice)o);
        }
        
        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
        
        #endregion
        
        #region Change Tracking
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        #endregion
        
        #region Property Id
        
        private UInt32 _id;
        
        public event EventHandler IdChanged;
        
        protected virtual void OnIdChanged()
        {
            EventHandler handler = IdChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Id");
        }
        
        public virtual UInt32 Id
        {
            get { return _id; }
            set
            {
                if ((value == _id))
                {
                    return;
                }
                _id = value;
                this.OnIdChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceName
        
        private string _deviceName;
        
        public event EventHandler DeviceNameChanged;
        
        protected virtual void OnDeviceNameChanged()
        {
            EventHandler handler = DeviceNameChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceName");
        }
        
        public virtual string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (string.Equals(value, _deviceName))
                {
                    return;
                }
                _deviceName = value;
                this.OnDeviceNameChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceString
        
        private string _deviceString;
        
        public event EventHandler DeviceStringChanged;
        
        protected virtual void OnDeviceStringChanged()
        {
            EventHandler handler = DeviceStringChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceString");
        }
        
        public virtual string DeviceString
        {
            get { return _deviceString; }
            set
            {
                if (string.Equals(value, _deviceString))
                {
                    return;
                }
                _deviceString = value;
                this.OnDeviceStringChanged();
            }
        }
        
        #endregion
        
        #region Property StateFlags
        
        private global::Mastersign.DisplayManager.WinApi.DisplayDeviceStateFlags _stateFlags;
        
        public event EventHandler StateFlagsChanged;
        
        protected virtual void OnStateFlagsChanged()
        {
            EventHandler handler = StateFlagsChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"StateFlags");
        }
        
        public virtual global::Mastersign.DisplayManager.WinApi.DisplayDeviceStateFlags StateFlags
        {
            get { return _stateFlags; }
            set
            {
                if ((value == _stateFlags))
                {
                    return;
                }
                _stateFlags = value;
                this.OnStateFlagsChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceID
        
        private string _deviceID;
        
        public event EventHandler DeviceIDChanged;
        
        protected virtual void OnDeviceIDChanged()
        {
            EventHandler handler = DeviceIDChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceID");
        }
        
        public virtual string DeviceID
        {
            get { return _deviceID; }
            set
            {
                if (string.Equals(value, _deviceID))
                {
                    return;
                }
                _deviceID = value;
                this.OnDeviceIDChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceKey
        
        private string _deviceKey;
        
        public event EventHandler DeviceKeyChanged;
        
        protected virtual void OnDeviceKeyChanged()
        {
            EventHandler handler = DeviceKeyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceKey");
        }
        
        public virtual string DeviceKey
        {
            get { return _deviceKey; }
            set
            {
                if (string.Equals(value, _deviceKey))
                {
                    return;
                }
                _deviceKey = value;
                this.OnDeviceKeyChanged();
            }
        }
        
        #endregion
    }
    
    public partial class DisplaySetting : IEquatable<DisplaySetting>, INotifyPropertyChanged
    {
        public DisplaySetting()
        {
        }
        
        #region Equatability
        
        public bool Equals(DisplaySetting o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            return object.ReferenceEquals(this, o);
        }
        
        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            if (!(o.GetType() == typeof(DisplaySetting)))
            {
                return false;
            }
            return this.Equals((DisplaySetting)o);
        }
        
        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
        
        #endregion
        
        #region Change Tracking
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        #endregion
        
        #region Property Id
        
        private Int32 _id;
        
        public event EventHandler IdChanged;
        
        protected virtual void OnIdChanged()
        {
            EventHandler handler = IdChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Id");
        }
        
        public virtual Int32 Id
        {
            get { return _id; }
            set
            {
                if ((value == _id))
                {
                    return;
                }
                _id = value;
                this.OnIdChanged();
            }
        }
        
        #endregion
        
        #region Property DeviceName
        
        private string _deviceName;
        
        public event EventHandler DeviceNameChanged;
        
        protected virtual void OnDeviceNameChanged()
        {
            EventHandler handler = DeviceNameChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DeviceName");
        }
        
        public virtual string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (string.Equals(value, _deviceName))
                {
                    return;
                }
                _deviceName = value;
                this.OnDeviceNameChanged();
            }
        }
        
        #endregion
        
        #region Property Fields
        
        private global::Mastersign.DisplayManager.WinApi.DM_FIELDS _fields;
        
        public event EventHandler FieldsChanged;
        
        protected virtual void OnFieldsChanged()
        {
            EventHandler handler = FieldsChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Fields");
        }
        
        public virtual global::Mastersign.DisplayManager.WinApi.DM_FIELDS Fields
        {
            get { return _fields; }
            set
            {
                if ((value == _fields))
                {
                    return;
                }
                _fields = value;
                this.OnFieldsChanged();
            }
        }
        
        #endregion
        
        #region Property Position
        
        private Point _position;
        
        public event EventHandler PositionChanged;
        
        protected virtual void OnPositionChanged()
        {
            EventHandler handler = PositionChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Position");
        }
        
        public virtual Point Position
        {
            get { return _position; }
            set
            {
                if ((value == _position))
                {
                    return;
                }
                _position = value;
                this.OnPositionChanged();
            }
        }
        
        #endregion
        
        #region Property Orientation
        
        private global::Mastersign.DisplayManager.WinApi.DMDISPLAYORIENTATION _orientation;
        
        public event EventHandler OrientationChanged;
        
        protected virtual void OnOrientationChanged()
        {
            EventHandler handler = OrientationChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Orientation");
        }
        
        public virtual global::Mastersign.DisplayManager.WinApi.DMDISPLAYORIENTATION Orientation
        {
            get { return _orientation; }
            set
            {
                if ((value == _orientation))
                {
                    return;
                }
                _orientation = value;
                this.OnOrientationChanged();
            }
        }
        
        #endregion
        
        #region Property DisplayFixedOutput
        
        private Int32 _displayFixedOutput;
        
        public event EventHandler DisplayFixedOutputChanged;
        
        protected virtual void OnDisplayFixedOutputChanged()
        {
            EventHandler handler = DisplayFixedOutputChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DisplayFixedOutput");
        }
        
        public virtual Int32 DisplayFixedOutput
        {
            get { return _displayFixedOutput; }
            set
            {
                if ((value == _displayFixedOutput))
                {
                    return;
                }
                _displayFixedOutput = value;
                this.OnDisplayFixedOutputChanged();
            }
        }
        
        #endregion
        
        #region Property LogPixels
        
        private Int16 _logPixels;
        
        public event EventHandler LogPixelsChanged;
        
        protected virtual void OnLogPixelsChanged()
        {
            EventHandler handler = LogPixelsChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"LogPixels");
        }
        
        public virtual Int16 LogPixels
        {
            get { return _logPixels; }
            set
            {
                if ((value == _logPixels))
                {
                    return;
                }
                _logPixels = value;
                this.OnLogPixelsChanged();
            }
        }
        
        #endregion
        
        #region Property BitsPerPel
        
        private Int32 _bitsPerPel;
        
        public event EventHandler BitsPerPelChanged;
        
        protected virtual void OnBitsPerPelChanged()
        {
            EventHandler handler = BitsPerPelChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"BitsPerPel");
        }
        
        public virtual Int32 BitsPerPel
        {
            get { return _bitsPerPel; }
            set
            {
                if ((value == _bitsPerPel))
                {
                    return;
                }
                _bitsPerPel = value;
                this.OnBitsPerPelChanged();
            }
        }
        
        #endregion
        
        #region Property PelsWidth
        
        private Int32 _pelsWidth;
        
        public event EventHandler PelsWidthChanged;
        
        protected virtual void OnPelsWidthChanged()
        {
            EventHandler handler = PelsWidthChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"PelsWidth");
        }
        
        public virtual Int32 PelsWidth
        {
            get { return _pelsWidth; }
            set
            {
                if ((value == _pelsWidth))
                {
                    return;
                }
                _pelsWidth = value;
                this.OnPelsWidthChanged();
            }
        }
        
        #endregion
        
        #region Property PelsHeight
        
        private Int32 _pelsHeight;
        
        public event EventHandler PelsHeightChanged;
        
        protected virtual void OnPelsHeightChanged()
        {
            EventHandler handler = PelsHeightChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"PelsHeight");
        }
        
        public virtual Int32 PelsHeight
        {
            get { return _pelsHeight; }
            set
            {
                if ((value == _pelsHeight))
                {
                    return;
                }
                _pelsHeight = value;
                this.OnPelsHeightChanged();
            }
        }
        
        #endregion
        
        #region Property DisplayFlags
        
        private Int32 _displayFlags;
        
        public event EventHandler DisplayFlagsChanged;
        
        protected virtual void OnDisplayFlagsChanged()
        {
            EventHandler handler = DisplayFlagsChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DisplayFlags");
        }
        
        public virtual Int32 DisplayFlags
        {
            get { return _displayFlags; }
            set
            {
                if ((value == _displayFlags))
                {
                    return;
                }
                _displayFlags = value;
                this.OnDisplayFlagsChanged();
            }
        }
        
        #endregion
        
        #region Property Nup
        
        private Int32 _nup;
        
        public event EventHandler NupChanged;
        
        protected virtual void OnNupChanged()
        {
            EventHandler handler = NupChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Nup");
        }
        
        public virtual Int32 Nup
        {
            get { return _nup; }
            set
            {
                if ((value == _nup))
                {
                    return;
                }
                _nup = value;
                this.OnNupChanged();
            }
        }
        
        #endregion
        
        #region Property DisplayFrequency
        
        private Int32 _displayFrequency;
        
        public event EventHandler DisplayFrequencyChanged;
        
        protected virtual void OnDisplayFrequencyChanged()
        {
            EventHandler handler = DisplayFrequencyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"DisplayFrequency");
        }
        
        public virtual Int32 DisplayFrequency
        {
            get { return _displayFrequency; }
            set
            {
                if ((value == _displayFrequency))
                {
                    return;
                }
                _displayFrequency = value;
                this.OnDisplayFrequencyChanged();
            }
        }
        
        #endregion
    }
    
    public partial class DisplayConfiguration : IEquatable<DisplayConfiguration>, INotifyPropertyChanged
    {
        public DisplayConfiguration()
        {
        }
        
        #region Equatability
        
        public bool Equals(DisplayConfiguration o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            return object.ReferenceEquals(this, o);
        }
        
        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            if (!(o.GetType() == typeof(DisplayConfiguration)))
            {
                return false;
            }
            return this.Equals((DisplayConfiguration)o);
        }
        
        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
        
        #endregion
        
        #region Change Tracking
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        #endregion
        
        #region Property Device
        
        private DisplayDevice _device;
        
        public event EventHandler DeviceChanged;
        
        protected virtual void OnDeviceChanged()
        {
            EventHandler handler = DeviceChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Device");
        }
        
        public virtual DisplayDevice Device
        {
            get { return _device; }
            set
            {
                if ((value == _device))
                {
                    return;
                }
                _device = value;
                this.OnDeviceChanged();
            }
        }
        
        #endregion
        
        #region Property Setting
        
        private DisplaySetting _setting;
        
        public event EventHandler SettingChanged;
        
        protected virtual void OnSettingChanged()
        {
            EventHandler handler = SettingChanged;
            if (!ReferenceEquals(handler, null))
            {
                handler(this, EventArgs.Empty);
            }
            this.OnPropertyChanged(@"Setting");
        }
        
        public virtual DisplaySetting Setting
        {
            get { return _setting; }
            set
            {
                if ((value == _setting))
                {
                    return;
                }
                _setting = value;
                this.OnSettingChanged();
            }
        }
        
        #endregion
    }
    
    #endregion
}