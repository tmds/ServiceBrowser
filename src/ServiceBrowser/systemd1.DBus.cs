using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace systemd1.DBus
{
    static class Systemd
    {
        public const string Service = "org.freedesktop.systemd1";
        public const string RootPath = "/org/freedesktop/systemd1";
    }

    static class LoadState
    {
        public const string Stub = "stub";
        public const string Loaded = "loaded";
        public const string NotFound = "not-found";
        public const string Error = "error";
        public const string Merged = "merged";
        public const string Masked = "masked";
    }

    static class ActiveState
    {
        public const string Active = "active";
        public const string Reloading = "reloading";
        public const string Inactive = "inactive";
        public const string Failed = "failed";
        public const string Activating = "activating";
        public const string Deactivating = "deactivating";
    }

    static class UnitFileState
    {
        public const string Enabled = "enabled";
        public const string EnabledRuntime = "enabled-runtime";
        public const string Masked = "masked";
        public const string MaskedRuntime = "masked-runtime";
        public const string Static = "static";
        public const string Disabled = "disabled";
        public const string Invalid = "invalid";
    }

    [StructLayout(LayoutKind.Sequential)]
    class Unit
    {
        private string _unitName;
        private string _description;
        private string _loadState;
        private string _activeState;
        private string _subState;
        private string _followUnit;
        private ObjectPath _unitObjectPath;
        private uint _jobId;
        private string _jobType;
        private ObjectPath _jobObjectPath;

        public string Name => _unitName;
        public ObjectPath Path => _unitObjectPath;
        public string ActiveState => _activeState;
        public string Description => _description;

        public override string ToString() => Name;
    }

    [DBusInterface("org.freedesktop.systemd1.Manager")]
    interface IManager : IDBusObject
    {
        Task<ObjectPath> GetUnitAsync(string arg0);
        Task<ObjectPath> GetUnitByPIDAsync(uint arg0);
        Task<ObjectPath> LoadUnitAsync(string arg0);
        Task<ObjectPath> StartUnitAsync(string arg0, string arg1);
        Task<ObjectPath> StartUnitReplaceAsync(string arg0, string arg1, string arg2);
        Task<ObjectPath> StopUnitAsync(string arg0, string arg1);
        Task<ObjectPath> ReloadUnitAsync(string arg0, string arg1);
        Task<ObjectPath> RestartUnitAsync(string arg0, string arg1);
        Task<ObjectPath> TryRestartUnitAsync(string arg0, string arg1);
        Task<ObjectPath> ReloadOrRestartUnitAsync(string arg0, string arg1);
        Task<ObjectPath> ReloadOrTryRestartUnitAsync(string arg0, string arg1);
        Task KillUnitAsync(string arg0, string arg1, int arg2);
        Task ResetFailedUnitAsync(string arg0);
        Task SetUnitPropertiesAsync(string arg0, bool arg1, (string, object)[] arg2);
        Task<ObjectPath> StartTransientUnitAsync(string arg0, string arg1, (string, object)[] arg2, (string, (string, object)[])[] arg3);
        Task<(string, uint, string)[]> GetUnitProcessesAsync(string arg0);
        Task<ObjectPath> GetJobAsync(uint arg0);
        Task CancelJobAsync(uint arg0);
        Task ClearJobsAsync();
        Task ResetFailedAsync();
        Task<Unit[]> ListUnitsAsync();
        Task<(string, string, string, string, string, string, ObjectPath, uint, string, ObjectPath)[]> ListUnitsFilteredAsync(string[] arg0);
        Task<(string, string, string, string, string, string, ObjectPath, uint, string, ObjectPath)[]> ListUnitsByPatternsAsync(string[] arg0, string[] arg1);
        Task<(string, string, string, string, string, string, ObjectPath, uint, string, ObjectPath)[]> ListUnitsByNamesAsync(string[] arg0);
        Task<(uint, string, string, string, ObjectPath, ObjectPath)[]> ListJobsAsync();
        Task SubscribeAsync();
        Task UnsubscribeAsync();
        Task<string> DumpAsync();
        Task<ObjectPath> CreateSnapshotAsync(string arg0, bool arg1);
        Task RemoveSnapshotAsync(string arg0);
        Task ReloadAsync();
        Task ReexecuteAsync();
        Task ExitAsync();
        Task RebootAsync();
        Task PowerOffAsync();
        Task HaltAsync();
        Task KExecAsync();
        Task SwitchRootAsync(string arg0, string arg1);
        Task SetEnvironmentAsync(string[] arg0);
        Task UnsetEnvironmentAsync(string[] arg0);
        Task UnsetAndSetEnvironmentAsync(string[] arg0, string[] arg1);
        Task<(string path, string state)[]> ListUnitFilesAsync();
        Task<(string, string)[]> ListUnitFilesByPatternsAsync(string[] arg0, string[] arg1);
        Task<string> GetUnitFileStateAsync(string arg0);
        Task<(bool, (string, string, string)[])> EnableUnitFilesAsync(string[] arg0, bool arg1, bool arg2);
        Task<(string, string, string)[]> DisableUnitFilesAsync(string[] arg0, bool arg1);
        Task<(bool, (string, string, string)[])> ReenableUnitFilesAsync(string[] arg0, bool arg1, bool arg2);
        Task<(string, string, string)[]> LinkUnitFilesAsync(string[] arg0, bool arg1, bool arg2);
        Task<(bool, (string, string, string)[])> PresetUnitFilesAsync(string[] arg0, bool arg1, bool arg2);
        Task<(bool, (string, string, string)[])> PresetUnitFilesWithModeAsync(string[] arg0, string arg1, bool arg2, bool arg3);
        Task<(string, string, string)[]> MaskUnitFilesAsync(string[] arg0, bool arg1, bool arg2);
        Task<(string, string, string)[]> UnmaskUnitFilesAsync(string[] arg0, bool arg1);
        Task<(string, string, string)[]> RevertUnitFilesAsync(string[] arg0);
        Task<(string, string, string)[]> SetDefaultTargetAsync(string arg0, bool arg1);
        Task<string> GetDefaultTargetAsync();
        Task<(string, string, string)[]> PresetAllUnitFilesAsync(string arg0, bool arg1, bool arg2);
        Task<(string, string, string)[]> AddDependencyUnitFilesAsync(string[] arg0, string arg1, string arg2, bool arg3, bool arg4);
        Task SetExitCodeAsync(byte arg0);
        Task<IDisposable> WatchUnitNewAsync(Action<(string, ObjectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUnitRemovedAsync(Action<(string, ObjectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchJobNewAsync(Action<(uint, ObjectPath, string)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchJobRemovedAsync(Action<(uint, ObjectPath, string, string)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchStartupFinishedAsync(Action<(ulong, ulong, ulong, ulong, ulong, ulong)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUnitFilesChangedAsync(Action handler, Action<Exception> onError = null);
        Task<IDisposable> WatchReloadingAsync(Action<bool> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ManagerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ManagerProperties
    {
        private string _Version = default (string);
        public string Version
        {
            get
            {
                return _Version;
            }

            set
            {
                _Version = (value);
            }
        }

        private string _Features = default (string);
        public string Features
        {
            get
            {
                return _Features;
            }

            set
            {
                _Features = (value);
            }
        }

        private string _Virtualization = default (string);
        public string Virtualization
        {
            get
            {
                return _Virtualization;
            }

            set
            {
                _Virtualization = (value);
            }
        }

        private string _Architecture = default (string);
        public string Architecture
        {
            get
            {
                return _Architecture;
            }

            set
            {
                _Architecture = (value);
            }
        }

        private string _Tainted = default (string);
        public string Tainted
        {
            get
            {
                return _Tainted;
            }

            set
            {
                _Tainted = (value);
            }
        }

        private ulong _FirmwareTimestamp = default (ulong);
        public ulong FirmwareTimestamp
        {
            get
            {
                return _FirmwareTimestamp;
            }

            set
            {
                _FirmwareTimestamp = (value);
            }
        }

        private ulong _FirmwareTimestampMonotonic = default (ulong);
        public ulong FirmwareTimestampMonotonic
        {
            get
            {
                return _FirmwareTimestampMonotonic;
            }

            set
            {
                _FirmwareTimestampMonotonic = (value);
            }
        }

        private ulong _LoaderTimestamp = default (ulong);
        public ulong LoaderTimestamp
        {
            get
            {
                return _LoaderTimestamp;
            }

            set
            {
                _LoaderTimestamp = (value);
            }
        }

        private ulong _LoaderTimestampMonotonic = default (ulong);
        public ulong LoaderTimestampMonotonic
        {
            get
            {
                return _LoaderTimestampMonotonic;
            }

            set
            {
                _LoaderTimestampMonotonic = (value);
            }
        }

        private ulong _KernelTimestamp = default (ulong);
        public ulong KernelTimestamp
        {
            get
            {
                return _KernelTimestamp;
            }

            set
            {
                _KernelTimestamp = (value);
            }
        }

        private ulong _KernelTimestampMonotonic = default (ulong);
        public ulong KernelTimestampMonotonic
        {
            get
            {
                return _KernelTimestampMonotonic;
            }

            set
            {
                _KernelTimestampMonotonic = (value);
            }
        }

        private ulong _InitRDTimestamp = default (ulong);
        public ulong InitRDTimestamp
        {
            get
            {
                return _InitRDTimestamp;
            }

            set
            {
                _InitRDTimestamp = (value);
            }
        }

        private ulong _InitRDTimestampMonotonic = default (ulong);
        public ulong InitRDTimestampMonotonic
        {
            get
            {
                return _InitRDTimestampMonotonic;
            }

            set
            {
                _InitRDTimestampMonotonic = (value);
            }
        }

        private ulong _UserspaceTimestamp = default (ulong);
        public ulong UserspaceTimestamp
        {
            get
            {
                return _UserspaceTimestamp;
            }

            set
            {
                _UserspaceTimestamp = (value);
            }
        }

        private ulong _UserspaceTimestampMonotonic = default (ulong);
        public ulong UserspaceTimestampMonotonic
        {
            get
            {
                return _UserspaceTimestampMonotonic;
            }

            set
            {
                _UserspaceTimestampMonotonic = (value);
            }
        }

        private ulong _FinishTimestamp = default (ulong);
        public ulong FinishTimestamp
        {
            get
            {
                return _FinishTimestamp;
            }

            set
            {
                _FinishTimestamp = (value);
            }
        }

        private ulong _FinishTimestampMonotonic = default (ulong);
        public ulong FinishTimestampMonotonic
        {
            get
            {
                return _FinishTimestampMonotonic;
            }

            set
            {
                _FinishTimestampMonotonic = (value);
            }
        }

        private ulong _SecurityStartTimestamp = default (ulong);
        public ulong SecurityStartTimestamp
        {
            get
            {
                return _SecurityStartTimestamp;
            }

            set
            {
                _SecurityStartTimestamp = (value);
            }
        }

        private ulong _SecurityStartTimestampMonotonic = default (ulong);
        public ulong SecurityStartTimestampMonotonic
        {
            get
            {
                return _SecurityStartTimestampMonotonic;
            }

            set
            {
                _SecurityStartTimestampMonotonic = (value);
            }
        }

        private ulong _SecurityFinishTimestamp = default (ulong);
        public ulong SecurityFinishTimestamp
        {
            get
            {
                return _SecurityFinishTimestamp;
            }

            set
            {
                _SecurityFinishTimestamp = (value);
            }
        }

        private ulong _SecurityFinishTimestampMonotonic = default (ulong);
        public ulong SecurityFinishTimestampMonotonic
        {
            get
            {
                return _SecurityFinishTimestampMonotonic;
            }

            set
            {
                _SecurityFinishTimestampMonotonic = (value);
            }
        }

        private ulong _GeneratorsStartTimestamp = default (ulong);
        public ulong GeneratorsStartTimestamp
        {
            get
            {
                return _GeneratorsStartTimestamp;
            }

            set
            {
                _GeneratorsStartTimestamp = (value);
            }
        }

        private ulong _GeneratorsStartTimestampMonotonic = default (ulong);
        public ulong GeneratorsStartTimestampMonotonic
        {
            get
            {
                return _GeneratorsStartTimestampMonotonic;
            }

            set
            {
                _GeneratorsStartTimestampMonotonic = (value);
            }
        }

        private ulong _GeneratorsFinishTimestamp = default (ulong);
        public ulong GeneratorsFinishTimestamp
        {
            get
            {
                return _GeneratorsFinishTimestamp;
            }

            set
            {
                _GeneratorsFinishTimestamp = (value);
            }
        }

        private ulong _GeneratorsFinishTimestampMonotonic = default (ulong);
        public ulong GeneratorsFinishTimestampMonotonic
        {
            get
            {
                return _GeneratorsFinishTimestampMonotonic;
            }

            set
            {
                _GeneratorsFinishTimestampMonotonic = (value);
            }
        }

        private ulong _UnitsLoadStartTimestamp = default (ulong);
        public ulong UnitsLoadStartTimestamp
        {
            get
            {
                return _UnitsLoadStartTimestamp;
            }

            set
            {
                _UnitsLoadStartTimestamp = (value);
            }
        }

        private ulong _UnitsLoadStartTimestampMonotonic = default (ulong);
        public ulong UnitsLoadStartTimestampMonotonic
        {
            get
            {
                return _UnitsLoadStartTimestampMonotonic;
            }

            set
            {
                _UnitsLoadStartTimestampMonotonic = (value);
            }
        }

        private ulong _UnitsLoadFinishTimestamp = default (ulong);
        public ulong UnitsLoadFinishTimestamp
        {
            get
            {
                return _UnitsLoadFinishTimestamp;
            }

            set
            {
                _UnitsLoadFinishTimestamp = (value);
            }
        }

        private ulong _UnitsLoadFinishTimestampMonotonic = default (ulong);
        public ulong UnitsLoadFinishTimestampMonotonic
        {
            get
            {
                return _UnitsLoadFinishTimestampMonotonic;
            }

            set
            {
                _UnitsLoadFinishTimestampMonotonic = (value);
            }
        }

        private string _LogLevel = default (string);
        public string LogLevel
        {
            get
            {
                return _LogLevel;
            }

            set
            {
                _LogLevel = (value);
            }
        }

        private string _LogTarget = default (string);
        public string LogTarget
        {
            get
            {
                return _LogTarget;
            }

            set
            {
                _LogTarget = (value);
            }
        }

        private uint _NNames = default (uint);
        public uint NNames
        {
            get
            {
                return _NNames;
            }

            set
            {
                _NNames = (value);
            }
        }

        private uint _NFailedUnits = default (uint);
        public uint NFailedUnits
        {
            get
            {
                return _NFailedUnits;
            }

            set
            {
                _NFailedUnits = (value);
            }
        }

        private uint _NJobs = default (uint);
        public uint NJobs
        {
            get
            {
                return _NJobs;
            }

            set
            {
                _NJobs = (value);
            }
        }

        private uint _NInstalledJobs = default (uint);
        public uint NInstalledJobs
        {
            get
            {
                return _NInstalledJobs;
            }

            set
            {
                _NInstalledJobs = (value);
            }
        }

        private uint _NFailedJobs = default (uint);
        public uint NFailedJobs
        {
            get
            {
                return _NFailedJobs;
            }

            set
            {
                _NFailedJobs = (value);
            }
        }

        private double _Progress = default (double);
        public double Progress
        {
            get
            {
                return _Progress;
            }

            set
            {
                _Progress = (value);
            }
        }

        private string[] _Environment = default (string[]);
        public string[] Environment
        {
            get
            {
                return _Environment;
            }

            set
            {
                _Environment = (value);
            }
        }

        private bool _ConfirmSpawn = default (bool);
        public bool ConfirmSpawn
        {
            get
            {
                return _ConfirmSpawn;
            }

            set
            {
                _ConfirmSpawn = (value);
            }
        }

        private bool _ShowStatus = default (bool);
        public bool ShowStatus
        {
            get
            {
                return _ShowStatus;
            }

            set
            {
                _ShowStatus = (value);
            }
        }

        private string[] _UnitPath = default (string[]);
        public string[] UnitPath
        {
            get
            {
                return _UnitPath;
            }

            set
            {
                _UnitPath = (value);
            }
        }

        private string _DefaultStandardOutput = default (string);
        public string DefaultStandardOutput
        {
            get
            {
                return _DefaultStandardOutput;
            }

            set
            {
                _DefaultStandardOutput = (value);
            }
        }

        private string _DefaultStandardError = default (string);
        public string DefaultStandardError
        {
            get
            {
                return _DefaultStandardError;
            }

            set
            {
                _DefaultStandardError = (value);
            }
        }

        private ulong _RuntimeWatchdogUSec = default (ulong);
        public ulong RuntimeWatchdogUSec
        {
            get
            {
                return _RuntimeWatchdogUSec;
            }

            set
            {
                _RuntimeWatchdogUSec = (value);
            }
        }

        private ulong _ShutdownWatchdogUSec = default (ulong);
        public ulong ShutdownWatchdogUSec
        {
            get
            {
                return _ShutdownWatchdogUSec;
            }

            set
            {
                _ShutdownWatchdogUSec = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private string _SystemState = default (string);
        public string SystemState
        {
            get
            {
                return _SystemState;
            }

            set
            {
                _SystemState = (value);
            }
        }

        private byte _ExitCode = default (byte);
        public byte ExitCode
        {
            get
            {
                return _ExitCode;
            }

            set
            {
                _ExitCode = (value);
            }
        }

        private ulong _DefaultTimerAccuracyUSec = default (ulong);
        public ulong DefaultTimerAccuracyUSec
        {
            get
            {
                return _DefaultTimerAccuracyUSec;
            }

            set
            {
                _DefaultTimerAccuracyUSec = (value);
            }
        }

        private ulong _DefaultTimeoutStartUSec = default (ulong);
        public ulong DefaultTimeoutStartUSec
        {
            get
            {
                return _DefaultTimeoutStartUSec;
            }

            set
            {
                _DefaultTimeoutStartUSec = (value);
            }
        }

        private ulong _DefaultTimeoutStopUSec = default (ulong);
        public ulong DefaultTimeoutStopUSec
        {
            get
            {
                return _DefaultTimeoutStopUSec;
            }

            set
            {
                _DefaultTimeoutStopUSec = (value);
            }
        }

        private ulong _DefaultRestartUSec = default (ulong);
        public ulong DefaultRestartUSec
        {
            get
            {
                return _DefaultRestartUSec;
            }

            set
            {
                _DefaultRestartUSec = (value);
            }
        }

        private ulong _DefaultStartLimitIntervalSec = default (ulong);
        public ulong DefaultStartLimitIntervalSec
        {
            get
            {
                return _DefaultStartLimitIntervalSec;
            }

            set
            {
                _DefaultStartLimitIntervalSec = (value);
            }
        }

        private uint _DefaultStartLimitBurst = default (uint);
        public uint DefaultStartLimitBurst
        {
            get
            {
                return _DefaultStartLimitBurst;
            }

            set
            {
                _DefaultStartLimitBurst = (value);
            }
        }

        private bool _DefaultCPUAccounting = default (bool);
        public bool DefaultCPUAccounting
        {
            get
            {
                return _DefaultCPUAccounting;
            }

            set
            {
                _DefaultCPUAccounting = (value);
            }
        }

        private bool _DefaultBlockIOAccounting = default (bool);
        public bool DefaultBlockIOAccounting
        {
            get
            {
                return _DefaultBlockIOAccounting;
            }

            set
            {
                _DefaultBlockIOAccounting = (value);
            }
        }

        private bool _DefaultMemoryAccounting = default (bool);
        public bool DefaultMemoryAccounting
        {
            get
            {
                return _DefaultMemoryAccounting;
            }

            set
            {
                _DefaultMemoryAccounting = (value);
            }
        }

        private bool _DefaultTasksAccounting = default (bool);
        public bool DefaultTasksAccounting
        {
            get
            {
                return _DefaultTasksAccounting;
            }

            set
            {
                _DefaultTasksAccounting = (value);
            }
        }

        private ulong _DefaultLimitCPU = default (ulong);
        public ulong DefaultLimitCPU
        {
            get
            {
                return _DefaultLimitCPU;
            }

            set
            {
                _DefaultLimitCPU = (value);
            }
        }

        private ulong _DefaultLimitCPUSoft = default (ulong);
        public ulong DefaultLimitCPUSoft
        {
            get
            {
                return _DefaultLimitCPUSoft;
            }

            set
            {
                _DefaultLimitCPUSoft = (value);
            }
        }

        private ulong _DefaultLimitFSIZE = default (ulong);
        public ulong DefaultLimitFSIZE
        {
            get
            {
                return _DefaultLimitFSIZE;
            }

            set
            {
                _DefaultLimitFSIZE = (value);
            }
        }

        private ulong _DefaultLimitFSIZESoft = default (ulong);
        public ulong DefaultLimitFSIZESoft
        {
            get
            {
                return _DefaultLimitFSIZESoft;
            }

            set
            {
                _DefaultLimitFSIZESoft = (value);
            }
        }

        private ulong _DefaultLimitDATA = default (ulong);
        public ulong DefaultLimitDATA
        {
            get
            {
                return _DefaultLimitDATA;
            }

            set
            {
                _DefaultLimitDATA = (value);
            }
        }

        private ulong _DefaultLimitDATASoft = default (ulong);
        public ulong DefaultLimitDATASoft
        {
            get
            {
                return _DefaultLimitDATASoft;
            }

            set
            {
                _DefaultLimitDATASoft = (value);
            }
        }

        private ulong _DefaultLimitSTACK = default (ulong);
        public ulong DefaultLimitSTACK
        {
            get
            {
                return _DefaultLimitSTACK;
            }

            set
            {
                _DefaultLimitSTACK = (value);
            }
        }

        private ulong _DefaultLimitSTACKSoft = default (ulong);
        public ulong DefaultLimitSTACKSoft
        {
            get
            {
                return _DefaultLimitSTACKSoft;
            }

            set
            {
                _DefaultLimitSTACKSoft = (value);
            }
        }

        private ulong _DefaultLimitCORE = default (ulong);
        public ulong DefaultLimitCORE
        {
            get
            {
                return _DefaultLimitCORE;
            }

            set
            {
                _DefaultLimitCORE = (value);
            }
        }

        private ulong _DefaultLimitCORESoft = default (ulong);
        public ulong DefaultLimitCORESoft
        {
            get
            {
                return _DefaultLimitCORESoft;
            }

            set
            {
                _DefaultLimitCORESoft = (value);
            }
        }

        private ulong _DefaultLimitRSS = default (ulong);
        public ulong DefaultLimitRSS
        {
            get
            {
                return _DefaultLimitRSS;
            }

            set
            {
                _DefaultLimitRSS = (value);
            }
        }

        private ulong _DefaultLimitRSSSoft = default (ulong);
        public ulong DefaultLimitRSSSoft
        {
            get
            {
                return _DefaultLimitRSSSoft;
            }

            set
            {
                _DefaultLimitRSSSoft = (value);
            }
        }

        private ulong _DefaultLimitNOFILE = default (ulong);
        public ulong DefaultLimitNOFILE
        {
            get
            {
                return _DefaultLimitNOFILE;
            }

            set
            {
                _DefaultLimitNOFILE = (value);
            }
        }

        private ulong _DefaultLimitNOFILESoft = default (ulong);
        public ulong DefaultLimitNOFILESoft
        {
            get
            {
                return _DefaultLimitNOFILESoft;
            }

            set
            {
                _DefaultLimitNOFILESoft = (value);
            }
        }

        private ulong _DefaultLimitAS = default (ulong);
        public ulong DefaultLimitAS
        {
            get
            {
                return _DefaultLimitAS;
            }

            set
            {
                _DefaultLimitAS = (value);
            }
        }

        private ulong _DefaultLimitASSoft = default (ulong);
        public ulong DefaultLimitASSoft
        {
            get
            {
                return _DefaultLimitASSoft;
            }

            set
            {
                _DefaultLimitASSoft = (value);
            }
        }

        private ulong _DefaultLimitNPROC = default (ulong);
        public ulong DefaultLimitNPROC
        {
            get
            {
                return _DefaultLimitNPROC;
            }

            set
            {
                _DefaultLimitNPROC = (value);
            }
        }

        private ulong _DefaultLimitNPROCSoft = default (ulong);
        public ulong DefaultLimitNPROCSoft
        {
            get
            {
                return _DefaultLimitNPROCSoft;
            }

            set
            {
                _DefaultLimitNPROCSoft = (value);
            }
        }

        private ulong _DefaultLimitMEMLOCK = default (ulong);
        public ulong DefaultLimitMEMLOCK
        {
            get
            {
                return _DefaultLimitMEMLOCK;
            }

            set
            {
                _DefaultLimitMEMLOCK = (value);
            }
        }

        private ulong _DefaultLimitMEMLOCKSoft = default (ulong);
        public ulong DefaultLimitMEMLOCKSoft
        {
            get
            {
                return _DefaultLimitMEMLOCKSoft;
            }

            set
            {
                _DefaultLimitMEMLOCKSoft = (value);
            }
        }

        private ulong _DefaultLimitLOCKS = default (ulong);
        public ulong DefaultLimitLOCKS
        {
            get
            {
                return _DefaultLimitLOCKS;
            }

            set
            {
                _DefaultLimitLOCKS = (value);
            }
        }

        private ulong _DefaultLimitLOCKSSoft = default (ulong);
        public ulong DefaultLimitLOCKSSoft
        {
            get
            {
                return _DefaultLimitLOCKSSoft;
            }

            set
            {
                _DefaultLimitLOCKSSoft = (value);
            }
        }

        private ulong _DefaultLimitSIGPENDING = default (ulong);
        public ulong DefaultLimitSIGPENDING
        {
            get
            {
                return _DefaultLimitSIGPENDING;
            }

            set
            {
                _DefaultLimitSIGPENDING = (value);
            }
        }

        private ulong _DefaultLimitSIGPENDINGSoft = default (ulong);
        public ulong DefaultLimitSIGPENDINGSoft
        {
            get
            {
                return _DefaultLimitSIGPENDINGSoft;
            }

            set
            {
                _DefaultLimitSIGPENDINGSoft = (value);
            }
        }

        private ulong _DefaultLimitMSGQUEUE = default (ulong);
        public ulong DefaultLimitMSGQUEUE
        {
            get
            {
                return _DefaultLimitMSGQUEUE;
            }

            set
            {
                _DefaultLimitMSGQUEUE = (value);
            }
        }

        private ulong _DefaultLimitMSGQUEUESoft = default (ulong);
        public ulong DefaultLimitMSGQUEUESoft
        {
            get
            {
                return _DefaultLimitMSGQUEUESoft;
            }

            set
            {
                _DefaultLimitMSGQUEUESoft = (value);
            }
        }

        private ulong _DefaultLimitNICE = default (ulong);
        public ulong DefaultLimitNICE
        {
            get
            {
                return _DefaultLimitNICE;
            }

            set
            {
                _DefaultLimitNICE = (value);
            }
        }

        private ulong _DefaultLimitNICESoft = default (ulong);
        public ulong DefaultLimitNICESoft
        {
            get
            {
                return _DefaultLimitNICESoft;
            }

            set
            {
                _DefaultLimitNICESoft = (value);
            }
        }

        private ulong _DefaultLimitRTPRIO = default (ulong);
        public ulong DefaultLimitRTPRIO
        {
            get
            {
                return _DefaultLimitRTPRIO;
            }

            set
            {
                _DefaultLimitRTPRIO = (value);
            }
        }

        private ulong _DefaultLimitRTPRIOSoft = default (ulong);
        public ulong DefaultLimitRTPRIOSoft
        {
            get
            {
                return _DefaultLimitRTPRIOSoft;
            }

            set
            {
                _DefaultLimitRTPRIOSoft = (value);
            }
        }

        private ulong _DefaultLimitRTTIME = default (ulong);
        public ulong DefaultLimitRTTIME
        {
            get
            {
                return _DefaultLimitRTTIME;
            }

            set
            {
                _DefaultLimitRTTIME = (value);
            }
        }

        private ulong _DefaultLimitRTTIMESoft = default (ulong);
        public ulong DefaultLimitRTTIMESoft
        {
            get
            {
                return _DefaultLimitRTTIMESoft;
            }

            set
            {
                _DefaultLimitRTTIMESoft = (value);
            }
        }

        private ulong _DefaultTasksMax = default (ulong);
        public ulong DefaultTasksMax
        {
            get
            {
                return _DefaultTasksMax;
            }

            set
            {
                _DefaultTasksMax = (value);
            }
        }

        private ulong _TimerSlackNSec = default (ulong);
        public ulong TimerSlackNSec
        {
            get
            {
                return _TimerSlackNSec;
            }

            set
            {
                _TimerSlackNSec = (value);
            }
        }
    }

    static class ManagerExtensions
    {
        public static Task<string> GetVersionAsync(this IManager o) => o.GetAsync<string>("Version");
        public static Task<string> GetFeaturesAsync(this IManager o) => o.GetAsync<string>("Features");
        public static Task<string> GetVirtualizationAsync(this IManager o) => o.GetAsync<string>("Virtualization");
        public static Task<string> GetArchitectureAsync(this IManager o) => o.GetAsync<string>("Architecture");
        public static Task<string> GetTaintedAsync(this IManager o) => o.GetAsync<string>("Tainted");
        public static Task<ulong> GetFirmwareTimestampAsync(this IManager o) => o.GetAsync<ulong>("FirmwareTimestamp");
        public static Task<ulong> GetFirmwareTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("FirmwareTimestampMonotonic");
        public static Task<ulong> GetLoaderTimestampAsync(this IManager o) => o.GetAsync<ulong>("LoaderTimestamp");
        public static Task<ulong> GetLoaderTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("LoaderTimestampMonotonic");
        public static Task<ulong> GetKernelTimestampAsync(this IManager o) => o.GetAsync<ulong>("KernelTimestamp");
        public static Task<ulong> GetKernelTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("KernelTimestampMonotonic");
        public static Task<ulong> GetInitRDTimestampAsync(this IManager o) => o.GetAsync<ulong>("InitRDTimestamp");
        public static Task<ulong> GetInitRDTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("InitRDTimestampMonotonic");
        public static Task<ulong> GetUserspaceTimestampAsync(this IManager o) => o.GetAsync<ulong>("UserspaceTimestamp");
        public static Task<ulong> GetUserspaceTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("UserspaceTimestampMonotonic");
        public static Task<ulong> GetFinishTimestampAsync(this IManager o) => o.GetAsync<ulong>("FinishTimestamp");
        public static Task<ulong> GetFinishTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("FinishTimestampMonotonic");
        public static Task<ulong> GetSecurityStartTimestampAsync(this IManager o) => o.GetAsync<ulong>("SecurityStartTimestamp");
        public static Task<ulong> GetSecurityStartTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("SecurityStartTimestampMonotonic");
        public static Task<ulong> GetSecurityFinishTimestampAsync(this IManager o) => o.GetAsync<ulong>("SecurityFinishTimestamp");
        public static Task<ulong> GetSecurityFinishTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("SecurityFinishTimestampMonotonic");
        public static Task<ulong> GetGeneratorsStartTimestampAsync(this IManager o) => o.GetAsync<ulong>("GeneratorsStartTimestamp");
        public static Task<ulong> GetGeneratorsStartTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("GeneratorsStartTimestampMonotonic");
        public static Task<ulong> GetGeneratorsFinishTimestampAsync(this IManager o) => o.GetAsync<ulong>("GeneratorsFinishTimestamp");
        public static Task<ulong> GetGeneratorsFinishTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("GeneratorsFinishTimestampMonotonic");
        public static Task<ulong> GetUnitsLoadStartTimestampAsync(this IManager o) => o.GetAsync<ulong>("UnitsLoadStartTimestamp");
        public static Task<ulong> GetUnitsLoadStartTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("UnitsLoadStartTimestampMonotonic");
        public static Task<ulong> GetUnitsLoadFinishTimestampAsync(this IManager o) => o.GetAsync<ulong>("UnitsLoadFinishTimestamp");
        public static Task<ulong> GetUnitsLoadFinishTimestampMonotonicAsync(this IManager o) => o.GetAsync<ulong>("UnitsLoadFinishTimestampMonotonic");
        public static Task<string> GetLogLevelAsync(this IManager o) => o.GetAsync<string>("LogLevel");
        public static Task<string> GetLogTargetAsync(this IManager o) => o.GetAsync<string>("LogTarget");
        public static Task<uint> GetNNamesAsync(this IManager o) => o.GetAsync<uint>("NNames");
        public static Task<uint> GetNFailedUnitsAsync(this IManager o) => o.GetAsync<uint>("NFailedUnits");
        public static Task<uint> GetNJobsAsync(this IManager o) => o.GetAsync<uint>("NJobs");
        public static Task<uint> GetNInstalledJobsAsync(this IManager o) => o.GetAsync<uint>("NInstalledJobs");
        public static Task<uint> GetNFailedJobsAsync(this IManager o) => o.GetAsync<uint>("NFailedJobs");
        public static Task<double> GetProgressAsync(this IManager o) => o.GetAsync<double>("Progress");
        public static Task<string[]> GetEnvironmentAsync(this IManager o) => o.GetAsync<string[]>("Environment");
        public static Task<bool> GetConfirmSpawnAsync(this IManager o) => o.GetAsync<bool>("ConfirmSpawn");
        public static Task<bool> GetShowStatusAsync(this IManager o) => o.GetAsync<bool>("ShowStatus");
        public static Task<string[]> GetUnitPathAsync(this IManager o) => o.GetAsync<string[]>("UnitPath");
        public static Task<string> GetDefaultStandardOutputAsync(this IManager o) => o.GetAsync<string>("DefaultStandardOutput");
        public static Task<string> GetDefaultStandardErrorAsync(this IManager o) => o.GetAsync<string>("DefaultStandardError");
        public static Task<ulong> GetRuntimeWatchdogUSecAsync(this IManager o) => o.GetAsync<ulong>("RuntimeWatchdogUSec");
        public static Task<ulong> GetShutdownWatchdogUSecAsync(this IManager o) => o.GetAsync<ulong>("ShutdownWatchdogUSec");
        public static Task<string> GetControlGroupAsync(this IManager o) => o.GetAsync<string>("ControlGroup");
        public static Task<string> GetSystemStateAsync(this IManager o) => o.GetAsync<string>("SystemState");
        public static Task<byte> GetExitCodeAsync(this IManager o) => o.GetAsync<byte>("ExitCode");
        public static Task<ulong> GetDefaultTimerAccuracyUSecAsync(this IManager o) => o.GetAsync<ulong>("DefaultTimerAccuracyUSec");
        public static Task<ulong> GetDefaultTimeoutStartUSecAsync(this IManager o) => o.GetAsync<ulong>("DefaultTimeoutStartUSec");
        public static Task<ulong> GetDefaultTimeoutStopUSecAsync(this IManager o) => o.GetAsync<ulong>("DefaultTimeoutStopUSec");
        public static Task<ulong> GetDefaultRestartUSecAsync(this IManager o) => o.GetAsync<ulong>("DefaultRestartUSec");
        public static Task<ulong> GetDefaultStartLimitIntervalSecAsync(this IManager o) => o.GetAsync<ulong>("DefaultStartLimitIntervalSec");
        public static Task<uint> GetDefaultStartLimitBurstAsync(this IManager o) => o.GetAsync<uint>("DefaultStartLimitBurst");
        public static Task<bool> GetDefaultCPUAccountingAsync(this IManager o) => o.GetAsync<bool>("DefaultCPUAccounting");
        public static Task<bool> GetDefaultBlockIOAccountingAsync(this IManager o) => o.GetAsync<bool>("DefaultBlockIOAccounting");
        public static Task<bool> GetDefaultMemoryAccountingAsync(this IManager o) => o.GetAsync<bool>("DefaultMemoryAccounting");
        public static Task<bool> GetDefaultTasksAccountingAsync(this IManager o) => o.GetAsync<bool>("DefaultTasksAccounting");
        public static Task<ulong> GetDefaultLimitCPUAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitCPU");
        public static Task<ulong> GetDefaultLimitCPUSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitCPUSoft");
        public static Task<ulong> GetDefaultLimitFSIZEAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitFSIZE");
        public static Task<ulong> GetDefaultLimitFSIZESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitFSIZESoft");
        public static Task<ulong> GetDefaultLimitDATAAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitDATA");
        public static Task<ulong> GetDefaultLimitDATASoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitDATASoft");
        public static Task<ulong> GetDefaultLimitSTACKAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitSTACK");
        public static Task<ulong> GetDefaultLimitSTACKSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitSTACKSoft");
        public static Task<ulong> GetDefaultLimitCOREAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitCORE");
        public static Task<ulong> GetDefaultLimitCORESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitCORESoft");
        public static Task<ulong> GetDefaultLimitRSSAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRSS");
        public static Task<ulong> GetDefaultLimitRSSSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRSSSoft");
        public static Task<ulong> GetDefaultLimitNOFILEAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNOFILE");
        public static Task<ulong> GetDefaultLimitNOFILESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNOFILESoft");
        public static Task<ulong> GetDefaultLimitASAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitAS");
        public static Task<ulong> GetDefaultLimitASSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitASSoft");
        public static Task<ulong> GetDefaultLimitNPROCAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNPROC");
        public static Task<ulong> GetDefaultLimitNPROCSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNPROCSoft");
        public static Task<ulong> GetDefaultLimitMEMLOCKAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitMEMLOCK");
        public static Task<ulong> GetDefaultLimitMEMLOCKSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitMEMLOCKSoft");
        public static Task<ulong> GetDefaultLimitLOCKSAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitLOCKS");
        public static Task<ulong> GetDefaultLimitLOCKSSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitLOCKSSoft");
        public static Task<ulong> GetDefaultLimitSIGPENDINGAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitSIGPENDING");
        public static Task<ulong> GetDefaultLimitSIGPENDINGSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitSIGPENDINGSoft");
        public static Task<ulong> GetDefaultLimitMSGQUEUEAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitMSGQUEUE");
        public static Task<ulong> GetDefaultLimitMSGQUEUESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitMSGQUEUESoft");
        public static Task<ulong> GetDefaultLimitNICEAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNICE");
        public static Task<ulong> GetDefaultLimitNICESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitNICESoft");
        public static Task<ulong> GetDefaultLimitRTPRIOAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRTPRIO");
        public static Task<ulong> GetDefaultLimitRTPRIOSoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRTPRIOSoft");
        public static Task<ulong> GetDefaultLimitRTTIMEAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRTTIME");
        public static Task<ulong> GetDefaultLimitRTTIMESoftAsync(this IManager o) => o.GetAsync<ulong>("DefaultLimitRTTIMESoft");
        public static Task<ulong> GetDefaultTasksMaxAsync(this IManager o) => o.GetAsync<ulong>("DefaultTasksMax");
        public static Task<ulong> GetTimerSlackNSecAsync(this IManager o) => o.GetAsync<ulong>("TimerSlackNSec");
        public static Task SetLogLevelAsync(this IManager o, string val) => o.SetAsync("LogLevel", val);
        public static Task SetLogTargetAsync(this IManager o, string val) => o.SetAsync("LogTarget", val);
        public static Task SetRuntimeWatchdogUSecAsync(this IManager o, ulong val) => o.SetAsync("RuntimeWatchdogUSec", val);
        public static Task SetShutdownWatchdogUSecAsync(this IManager o, ulong val) => o.SetAsync("ShutdownWatchdogUSec", val);
    }

    [DBusInterface("org.freedesktop.systemd1.Service")]
    interface IService : IDBusObject
    {
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<ServiceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ServiceProperties
    {
        private string _Type = default (string);
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = (value);
            }
        }

        private string _Restart = default (string);
        public string Restart
        {
            get
            {
                return _Restart;
            }

            set
            {
                _Restart = (value);
            }
        }

        private string _PIDFile = default (string);
        public string PIDFile
        {
            get
            {
                return _PIDFile;
            }

            set
            {
                _PIDFile = (value);
            }
        }

        private string _NotifyAccess = default (string);
        public string NotifyAccess
        {
            get
            {
                return _NotifyAccess;
            }

            set
            {
                _NotifyAccess = (value);
            }
        }

        private ulong _RestartUSec = default (ulong);
        public ulong RestartUSec
        {
            get
            {
                return _RestartUSec;
            }

            set
            {
                _RestartUSec = (value);
            }
        }

        private ulong _TimeoutStartUSec = default (ulong);
        public ulong TimeoutStartUSec
        {
            get
            {
                return _TimeoutStartUSec;
            }

            set
            {
                _TimeoutStartUSec = (value);
            }
        }

        private ulong _TimeoutStopUSec = default (ulong);
        public ulong TimeoutStopUSec
        {
            get
            {
                return _TimeoutStopUSec;
            }

            set
            {
                _TimeoutStopUSec = (value);
            }
        }

        private ulong _RuntimeMaxUSec = default (ulong);
        public ulong RuntimeMaxUSec
        {
            get
            {
                return _RuntimeMaxUSec;
            }

            set
            {
                _RuntimeMaxUSec = (value);
            }
        }

        private ulong _WatchdogUSec = default (ulong);
        public ulong WatchdogUSec
        {
            get
            {
                return _WatchdogUSec;
            }

            set
            {
                _WatchdogUSec = (value);
            }
        }

        private ulong _WatchdogTimestamp = default (ulong);
        public ulong WatchdogTimestamp
        {
            get
            {
                return _WatchdogTimestamp;
            }

            set
            {
                _WatchdogTimestamp = (value);
            }
        }

        private ulong _WatchdogTimestampMonotonic = default (ulong);
        public ulong WatchdogTimestampMonotonic
        {
            get
            {
                return _WatchdogTimestampMonotonic;
            }

            set
            {
                _WatchdogTimestampMonotonic = (value);
            }
        }

        private string _FailureAction = default (string);
        public string FailureAction
        {
            get
            {
                return _FailureAction;
            }

            set
            {
                _FailureAction = (value);
            }
        }

        private bool _PermissionsStartOnly = default (bool);
        public bool PermissionsStartOnly
        {
            get
            {
                return _PermissionsStartOnly;
            }

            set
            {
                _PermissionsStartOnly = (value);
            }
        }

        private bool _RootDirectoryStartOnly = default (bool);
        public bool RootDirectoryStartOnly
        {
            get
            {
                return _RootDirectoryStartOnly;
            }

            set
            {
                _RootDirectoryStartOnly = (value);
            }
        }

        private bool _RemainAfterExit = default (bool);
        public bool RemainAfterExit
        {
            get
            {
                return _RemainAfterExit;
            }

            set
            {
                _RemainAfterExit = (value);
            }
        }

        private bool _GuessMainPID = default (bool);
        public bool GuessMainPID
        {
            get
            {
                return _GuessMainPID;
            }

            set
            {
                _GuessMainPID = (value);
            }
        }

        private uint _MainPID = default (uint);
        public uint MainPID
        {
            get
            {
                return _MainPID;
            }

            set
            {
                _MainPID = (value);
            }
        }

        private uint _ControlPID = default (uint);
        public uint ControlPID
        {
            get
            {
                return _ControlPID;
            }

            set
            {
                _ControlPID = (value);
            }
        }

        private string _BusName = default (string);
        public string BusName
        {
            get
            {
                return _BusName;
            }

            set
            {
                _BusName = (value);
            }
        }

        private uint _FileDescriptorStoreMax = default (uint);
        public uint FileDescriptorStoreMax
        {
            get
            {
                return _FileDescriptorStoreMax;
            }

            set
            {
                _FileDescriptorStoreMax = (value);
            }
        }

        private uint _NFileDescriptorStore = default (uint);
        public uint NFileDescriptorStore
        {
            get
            {
                return _NFileDescriptorStore;
            }

            set
            {
                _NFileDescriptorStore = (value);
            }
        }

        private string _StatusText = default (string);
        public string StatusText
        {
            get
            {
                return _StatusText;
            }

            set
            {
                _StatusText = (value);
            }
        }

        private int _StatusErrno = default (int);
        public int StatusErrno
        {
            get
            {
                return _StatusErrno;
            }

            set
            {
                _StatusErrno = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private string _USBFunctionDescriptors = default (string);
        public string USBFunctionDescriptors
        {
            get
            {
                return _USBFunctionDescriptors;
            }

            set
            {
                _USBFunctionDescriptors = (value);
            }
        }

        private string _USBFunctionStrings = default (string);
        public string USBFunctionStrings
        {
            get
            {
                return _USBFunctionStrings;
            }

            set
            {
                _USBFunctionStrings = (value);
            }
        }

        private ulong _ExecMainStartTimestamp = default (ulong);
        public ulong ExecMainStartTimestamp
        {
            get
            {
                return _ExecMainStartTimestamp;
            }

            set
            {
                _ExecMainStartTimestamp = (value);
            }
        }

        private ulong _ExecMainStartTimestampMonotonic = default (ulong);
        public ulong ExecMainStartTimestampMonotonic
        {
            get
            {
                return _ExecMainStartTimestampMonotonic;
            }

            set
            {
                _ExecMainStartTimestampMonotonic = (value);
            }
        }

        private ulong _ExecMainExitTimestamp = default (ulong);
        public ulong ExecMainExitTimestamp
        {
            get
            {
                return _ExecMainExitTimestamp;
            }

            set
            {
                _ExecMainExitTimestamp = (value);
            }
        }

        private ulong _ExecMainExitTimestampMonotonic = default (ulong);
        public ulong ExecMainExitTimestampMonotonic
        {
            get
            {
                return _ExecMainExitTimestampMonotonic;
            }

            set
            {
                _ExecMainExitTimestampMonotonic = (value);
            }
        }

        private uint _ExecMainPID = default (uint);
        public uint ExecMainPID
        {
            get
            {
                return _ExecMainPID;
            }

            set
            {
                _ExecMainPID = (value);
            }
        }

        private int _ExecMainCode = default (int);
        public int ExecMainCode
        {
            get
            {
                return _ExecMainCode;
            }

            set
            {
                _ExecMainCode = (value);
            }
        }

        private int _ExecMainStatus = default (int);
        public int ExecMainStatus
        {
            get
            {
                return _ExecMainStatus;
            }

            set
            {
                _ExecMainStatus = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStartPre = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStartPre
        {
            get
            {
                return _ExecStartPre;
            }

            set
            {
                _ExecStartPre = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStart = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStart
        {
            get
            {
                return _ExecStart;
            }

            set
            {
                _ExecStart = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStartPost = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStartPost
        {
            get
            {
                return _ExecStartPost;
            }

            set
            {
                _ExecStartPost = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecReload = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecReload
        {
            get
            {
                return _ExecReload;
            }

            set
            {
                _ExecReload = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStop = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStop
        {
            get
            {
                return _ExecStop;
            }

            set
            {
                _ExecStop = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStopPost = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStopPost
        {
            get
            {
                return _ExecStopPost;
            }

            set
            {
                _ExecStopPost = (value);
            }
        }

        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }

        private string[] _Environment = default (string[]);
        public string[] Environment
        {
            get
            {
                return _Environment;
            }

            set
            {
                _Environment = (value);
            }
        }

        private (string, bool)[] _EnvironmentFiles = default ((string, bool)[]);
        public (string, bool)[] EnvironmentFiles
        {
            get
            {
                return _EnvironmentFiles;
            }

            set
            {
                _EnvironmentFiles = (value);
            }
        }

        private string[] _PassEnvironment = default (string[]);
        public string[] PassEnvironment
        {
            get
            {
                return _PassEnvironment;
            }

            set
            {
                _PassEnvironment = (value);
            }
        }

        private uint _UMask = default (uint);
        public uint UMask
        {
            get
            {
                return _UMask;
            }

            set
            {
                _UMask = (value);
            }
        }

        private ulong _LimitCPU = default (ulong);
        public ulong LimitCPU
        {
            get
            {
                return _LimitCPU;
            }

            set
            {
                _LimitCPU = (value);
            }
        }

        private ulong _LimitCPUSoft = default (ulong);
        public ulong LimitCPUSoft
        {
            get
            {
                return _LimitCPUSoft;
            }

            set
            {
                _LimitCPUSoft = (value);
            }
        }

        private ulong _LimitFSIZE = default (ulong);
        public ulong LimitFSIZE
        {
            get
            {
                return _LimitFSIZE;
            }

            set
            {
                _LimitFSIZE = (value);
            }
        }

        private ulong _LimitFSIZESoft = default (ulong);
        public ulong LimitFSIZESoft
        {
            get
            {
                return _LimitFSIZESoft;
            }

            set
            {
                _LimitFSIZESoft = (value);
            }
        }

        private ulong _LimitDATA = default (ulong);
        public ulong LimitDATA
        {
            get
            {
                return _LimitDATA;
            }

            set
            {
                _LimitDATA = (value);
            }
        }

        private ulong _LimitDATASoft = default (ulong);
        public ulong LimitDATASoft
        {
            get
            {
                return _LimitDATASoft;
            }

            set
            {
                _LimitDATASoft = (value);
            }
        }

        private ulong _LimitSTACK = default (ulong);
        public ulong LimitSTACK
        {
            get
            {
                return _LimitSTACK;
            }

            set
            {
                _LimitSTACK = (value);
            }
        }

        private ulong _LimitSTACKSoft = default (ulong);
        public ulong LimitSTACKSoft
        {
            get
            {
                return _LimitSTACKSoft;
            }

            set
            {
                _LimitSTACKSoft = (value);
            }
        }

        private ulong _LimitCORE = default (ulong);
        public ulong LimitCORE
        {
            get
            {
                return _LimitCORE;
            }

            set
            {
                _LimitCORE = (value);
            }
        }

        private ulong _LimitCORESoft = default (ulong);
        public ulong LimitCORESoft
        {
            get
            {
                return _LimitCORESoft;
            }

            set
            {
                _LimitCORESoft = (value);
            }
        }

        private ulong _LimitRSS = default (ulong);
        public ulong LimitRSS
        {
            get
            {
                return _LimitRSS;
            }

            set
            {
                _LimitRSS = (value);
            }
        }

        private ulong _LimitRSSSoft = default (ulong);
        public ulong LimitRSSSoft
        {
            get
            {
                return _LimitRSSSoft;
            }

            set
            {
                _LimitRSSSoft = (value);
            }
        }

        private ulong _LimitNOFILE = default (ulong);
        public ulong LimitNOFILE
        {
            get
            {
                return _LimitNOFILE;
            }

            set
            {
                _LimitNOFILE = (value);
            }
        }

        private ulong _LimitNOFILESoft = default (ulong);
        public ulong LimitNOFILESoft
        {
            get
            {
                return _LimitNOFILESoft;
            }

            set
            {
                _LimitNOFILESoft = (value);
            }
        }

        private ulong _LimitAS = default (ulong);
        public ulong LimitAS
        {
            get
            {
                return _LimitAS;
            }

            set
            {
                _LimitAS = (value);
            }
        }

        private ulong _LimitASSoft = default (ulong);
        public ulong LimitASSoft
        {
            get
            {
                return _LimitASSoft;
            }

            set
            {
                _LimitASSoft = (value);
            }
        }

        private ulong _LimitNPROC = default (ulong);
        public ulong LimitNPROC
        {
            get
            {
                return _LimitNPROC;
            }

            set
            {
                _LimitNPROC = (value);
            }
        }

        private ulong _LimitNPROCSoft = default (ulong);
        public ulong LimitNPROCSoft
        {
            get
            {
                return _LimitNPROCSoft;
            }

            set
            {
                _LimitNPROCSoft = (value);
            }
        }

        private ulong _LimitMEMLOCK = default (ulong);
        public ulong LimitMEMLOCK
        {
            get
            {
                return _LimitMEMLOCK;
            }

            set
            {
                _LimitMEMLOCK = (value);
            }
        }

        private ulong _LimitMEMLOCKSoft = default (ulong);
        public ulong LimitMEMLOCKSoft
        {
            get
            {
                return _LimitMEMLOCKSoft;
            }

            set
            {
                _LimitMEMLOCKSoft = (value);
            }
        }

        private ulong _LimitLOCKS = default (ulong);
        public ulong LimitLOCKS
        {
            get
            {
                return _LimitLOCKS;
            }

            set
            {
                _LimitLOCKS = (value);
            }
        }

        private ulong _LimitLOCKSSoft = default (ulong);
        public ulong LimitLOCKSSoft
        {
            get
            {
                return _LimitLOCKSSoft;
            }

            set
            {
                _LimitLOCKSSoft = (value);
            }
        }

        private ulong _LimitSIGPENDING = default (ulong);
        public ulong LimitSIGPENDING
        {
            get
            {
                return _LimitSIGPENDING;
            }

            set
            {
                _LimitSIGPENDING = (value);
            }
        }

        private ulong _LimitSIGPENDINGSoft = default (ulong);
        public ulong LimitSIGPENDINGSoft
        {
            get
            {
                return _LimitSIGPENDINGSoft;
            }

            set
            {
                _LimitSIGPENDINGSoft = (value);
            }
        }

        private ulong _LimitMSGQUEUE = default (ulong);
        public ulong LimitMSGQUEUE
        {
            get
            {
                return _LimitMSGQUEUE;
            }

            set
            {
                _LimitMSGQUEUE = (value);
            }
        }

        private ulong _LimitMSGQUEUESoft = default (ulong);
        public ulong LimitMSGQUEUESoft
        {
            get
            {
                return _LimitMSGQUEUESoft;
            }

            set
            {
                _LimitMSGQUEUESoft = (value);
            }
        }

        private ulong _LimitNICE = default (ulong);
        public ulong LimitNICE
        {
            get
            {
                return _LimitNICE;
            }

            set
            {
                _LimitNICE = (value);
            }
        }

        private ulong _LimitNICESoft = default (ulong);
        public ulong LimitNICESoft
        {
            get
            {
                return _LimitNICESoft;
            }

            set
            {
                _LimitNICESoft = (value);
            }
        }

        private ulong _LimitRTPRIO = default (ulong);
        public ulong LimitRTPRIO
        {
            get
            {
                return _LimitRTPRIO;
            }

            set
            {
                _LimitRTPRIO = (value);
            }
        }

        private ulong _LimitRTPRIOSoft = default (ulong);
        public ulong LimitRTPRIOSoft
        {
            get
            {
                return _LimitRTPRIOSoft;
            }

            set
            {
                _LimitRTPRIOSoft = (value);
            }
        }

        private ulong _LimitRTTIME = default (ulong);
        public ulong LimitRTTIME
        {
            get
            {
                return _LimitRTTIME;
            }

            set
            {
                _LimitRTTIME = (value);
            }
        }

        private ulong _LimitRTTIMESoft = default (ulong);
        public ulong LimitRTTIMESoft
        {
            get
            {
                return _LimitRTTIMESoft;
            }

            set
            {
                _LimitRTTIMESoft = (value);
            }
        }

        private string _WorkingDirectory = default (string);
        public string WorkingDirectory
        {
            get
            {
                return _WorkingDirectory;
            }

            set
            {
                _WorkingDirectory = (value);
            }
        }

        private string _RootDirectory = default (string);
        public string RootDirectory
        {
            get
            {
                return _RootDirectory;
            }

            set
            {
                _RootDirectory = (value);
            }
        }

        private int _OOMScoreAdjust = default (int);
        public int OOMScoreAdjust
        {
            get
            {
                return _OOMScoreAdjust;
            }

            set
            {
                _OOMScoreAdjust = (value);
            }
        }

        private int _Nice = default (int);
        public int Nice
        {
            get
            {
                return _Nice;
            }

            set
            {
                _Nice = (value);
            }
        }

        private int _IOScheduling = default (int);
        public int IOScheduling
        {
            get
            {
                return _IOScheduling;
            }

            set
            {
                _IOScheduling = (value);
            }
        }

        private int _CPUSchedulingPolicy = default (int);
        public int CPUSchedulingPolicy
        {
            get
            {
                return _CPUSchedulingPolicy;
            }

            set
            {
                _CPUSchedulingPolicy = (value);
            }
        }

        private int _CPUSchedulingPriority = default (int);
        public int CPUSchedulingPriority
        {
            get
            {
                return _CPUSchedulingPriority;
            }

            set
            {
                _CPUSchedulingPriority = (value);
            }
        }

        private byte[] _CPUAffinity = default (byte[]);
        public byte[] CPUAffinity
        {
            get
            {
                return _CPUAffinity;
            }

            set
            {
                _CPUAffinity = (value);
            }
        }

        private ulong _TimerSlackNSec = default (ulong);
        public ulong TimerSlackNSec
        {
            get
            {
                return _TimerSlackNSec;
            }

            set
            {
                _TimerSlackNSec = (value);
            }
        }

        private bool _CPUSchedulingResetOnFork = default (bool);
        public bool CPUSchedulingResetOnFork
        {
            get
            {
                return _CPUSchedulingResetOnFork;
            }

            set
            {
                _CPUSchedulingResetOnFork = (value);
            }
        }

        private bool _NonBlocking = default (bool);
        public bool NonBlocking
        {
            get
            {
                return _NonBlocking;
            }

            set
            {
                _NonBlocking = (value);
            }
        }

        private string _StandardInput = default (string);
        public string StandardInput
        {
            get
            {
                return _StandardInput;
            }

            set
            {
                _StandardInput = (value);
            }
        }

        private string _StandardOutput = default (string);
        public string StandardOutput
        {
            get
            {
                return _StandardOutput;
            }

            set
            {
                _StandardOutput = (value);
            }
        }

        private string _StandardError = default (string);
        public string StandardError
        {
            get
            {
                return _StandardError;
            }

            set
            {
                _StandardError = (value);
            }
        }

        private string _TTYPath = default (string);
        public string TTYPath
        {
            get
            {
                return _TTYPath;
            }

            set
            {
                _TTYPath = (value);
            }
        }

        private bool _TTYReset = default (bool);
        public bool TTYReset
        {
            get
            {
                return _TTYReset;
            }

            set
            {
                _TTYReset = (value);
            }
        }

        private bool _TTYVHangup = default (bool);
        public bool TTYVHangup
        {
            get
            {
                return _TTYVHangup;
            }

            set
            {
                _TTYVHangup = (value);
            }
        }

        private bool _TTYVTDisallocate = default (bool);
        public bool TTYVTDisallocate
        {
            get
            {
                return _TTYVTDisallocate;
            }

            set
            {
                _TTYVTDisallocate = (value);
            }
        }

        private int _SyslogPriority = default (int);
        public int SyslogPriority
        {
            get
            {
                return _SyslogPriority;
            }

            set
            {
                _SyslogPriority = (value);
            }
        }

        private string _SyslogIdentifier = default (string);
        public string SyslogIdentifier
        {
            get
            {
                return _SyslogIdentifier;
            }

            set
            {
                _SyslogIdentifier = (value);
            }
        }

        private bool _SyslogLevelPrefix = default (bool);
        public bool SyslogLevelPrefix
        {
            get
            {
                return _SyslogLevelPrefix;
            }

            set
            {
                _SyslogLevelPrefix = (value);
            }
        }

        private int _SyslogLevel = default (int);
        public int SyslogLevel
        {
            get
            {
                return _SyslogLevel;
            }

            set
            {
                _SyslogLevel = (value);
            }
        }

        private int _SyslogFacility = default (int);
        public int SyslogFacility
        {
            get
            {
                return _SyslogFacility;
            }

            set
            {
                _SyslogFacility = (value);
            }
        }

        private int _SecureBits = default (int);
        public int SecureBits
        {
            get
            {
                return _SecureBits;
            }

            set
            {
                _SecureBits = (value);
            }
        }

        private ulong _CapabilityBoundingSet = default (ulong);
        public ulong CapabilityBoundingSet
        {
            get
            {
                return _CapabilityBoundingSet;
            }

            set
            {
                _CapabilityBoundingSet = (value);
            }
        }

        private ulong _AmbientCapabilities = default (ulong);
        public ulong AmbientCapabilities
        {
            get
            {
                return _AmbientCapabilities;
            }

            set
            {
                _AmbientCapabilities = (value);
            }
        }

        private string _User = default (string);
        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = (value);
            }
        }

        private string _Group = default (string);
        public string Group
        {
            get
            {
                return _Group;
            }

            set
            {
                _Group = (value);
            }
        }

        private string[] _SupplementaryGroups = default (string[]);
        public string[] SupplementaryGroups
        {
            get
            {
                return _SupplementaryGroups;
            }

            set
            {
                _SupplementaryGroups = (value);
            }
        }

        private string _PAMName = default (string);
        public string PAMName
        {
            get
            {
                return _PAMName;
            }

            set
            {
                _PAMName = (value);
            }
        }

        private string[] _ReadWritePaths = default (string[]);
        public string[] ReadWritePaths
        {
            get
            {
                return _ReadWritePaths;
            }

            set
            {
                _ReadWritePaths = (value);
            }
        }

        private string[] _ReadOnlyPaths = default (string[]);
        public string[] ReadOnlyPaths
        {
            get
            {
                return _ReadOnlyPaths;
            }

            set
            {
                _ReadOnlyPaths = (value);
            }
        }

        private string[] _InaccessiblePaths = default (string[]);
        public string[] InaccessiblePaths
        {
            get
            {
                return _InaccessiblePaths;
            }

            set
            {
                _InaccessiblePaths = (value);
            }
        }

        private ulong _MountFlags = default (ulong);
        public ulong MountFlags
        {
            get
            {
                return _MountFlags;
            }

            set
            {
                _MountFlags = (value);
            }
        }

        private bool _PrivateTmp = default (bool);
        public bool PrivateTmp
        {
            get
            {
                return _PrivateTmp;
            }

            set
            {
                _PrivateTmp = (value);
            }
        }

        private bool _PrivateNetwork = default (bool);
        public bool PrivateNetwork
        {
            get
            {
                return _PrivateNetwork;
            }

            set
            {
                _PrivateNetwork = (value);
            }
        }

        private bool _PrivateDevices = default (bool);
        public bool PrivateDevices
        {
            get
            {
                return _PrivateDevices;
            }

            set
            {
                _PrivateDevices = (value);
            }
        }

        private string _ProtectHome = default (string);
        public string ProtectHome
        {
            get
            {
                return _ProtectHome;
            }

            set
            {
                _ProtectHome = (value);
            }
        }

        private string _ProtectSystem = default (string);
        public string ProtectSystem
        {
            get
            {
                return _ProtectSystem;
            }

            set
            {
                _ProtectSystem = (value);
            }
        }

        private bool _SameProcessGroup = default (bool);
        public bool SameProcessGroup
        {
            get
            {
                return _SameProcessGroup;
            }

            set
            {
                _SameProcessGroup = (value);
            }
        }

        private string _UtmpIdentifier = default (string);
        public string UtmpIdentifier
        {
            get
            {
                return _UtmpIdentifier;
            }

            set
            {
                _UtmpIdentifier = (value);
            }
        }

        private string _UtmpMode = default (string);
        public string UtmpMode
        {
            get
            {
                return _UtmpMode;
            }

            set
            {
                _UtmpMode = (value);
            }
        }

        private (bool, string)_SELinuxContext = default ((bool, string));
        public (bool, string)SELinuxContext
        {
            get
            {
                return _SELinuxContext;
            }

            set
            {
                _SELinuxContext = (value);
            }
        }

        private (bool, string)_AppArmorProfile = default ((bool, string));
        public (bool, string)AppArmorProfile
        {
            get
            {
                return _AppArmorProfile;
            }

            set
            {
                _AppArmorProfile = (value);
            }
        }

        private (bool, string)_SmackProcessLabel = default ((bool, string));
        public (bool, string)SmackProcessLabel
        {
            get
            {
                return _SmackProcessLabel;
            }

            set
            {
                _SmackProcessLabel = (value);
            }
        }

        private bool _IgnoreSIGPIPE = default (bool);
        public bool IgnoreSIGPIPE
        {
            get
            {
                return _IgnoreSIGPIPE;
            }

            set
            {
                _IgnoreSIGPIPE = (value);
            }
        }

        private bool _NoNewPrivileges = default (bool);
        public bool NoNewPrivileges
        {
            get
            {
                return _NoNewPrivileges;
            }

            set
            {
                _NoNewPrivileges = (value);
            }
        }

        private (bool, string[])_SystemCallFilter = default ((bool, string[]));
        public (bool, string[])SystemCallFilter
        {
            get
            {
                return _SystemCallFilter;
            }

            set
            {
                _SystemCallFilter = (value);
            }
        }

        private string[] _SystemCallArchitectures = default (string[]);
        public string[] SystemCallArchitectures
        {
            get
            {
                return _SystemCallArchitectures;
            }

            set
            {
                _SystemCallArchitectures = (value);
            }
        }

        private int _SystemCallErrorNumber = default (int);
        public int SystemCallErrorNumber
        {
            get
            {
                return _SystemCallErrorNumber;
            }

            set
            {
                _SystemCallErrorNumber = (value);
            }
        }

        private string _Personality = default (string);
        public string Personality
        {
            get
            {
                return _Personality;
            }

            set
            {
                _Personality = (value);
            }
        }

        private (bool, string[])_RestrictAddressFamilies = default ((bool, string[]));
        public (bool, string[])RestrictAddressFamilies
        {
            get
            {
                return _RestrictAddressFamilies;
            }

            set
            {
                _RestrictAddressFamilies = (value);
            }
        }

        private uint _RuntimeDirectoryMode = default (uint);
        public uint RuntimeDirectoryMode
        {
            get
            {
                return _RuntimeDirectoryMode;
            }

            set
            {
                _RuntimeDirectoryMode = (value);
            }
        }

        private string[] _RuntimeDirectory = default (string[]);
        public string[] RuntimeDirectory
        {
            get
            {
                return _RuntimeDirectory;
            }

            set
            {
                _RuntimeDirectory = (value);
            }
        }

        private bool _MemoryDenyWriteExecute = default (bool);
        public bool MemoryDenyWriteExecute
        {
            get
            {
                return _MemoryDenyWriteExecute;
            }

            set
            {
                _MemoryDenyWriteExecute = (value);
            }
        }

        private bool _RestrictRealtime = default (bool);
        public bool RestrictRealtime
        {
            get
            {
                return _RestrictRealtime;
            }

            set
            {
                _RestrictRealtime = (value);
            }
        }

        private string _KillMode = default (string);
        public string KillMode
        {
            get
            {
                return _KillMode;
            }

            set
            {
                _KillMode = (value);
            }
        }

        private int _KillSignal = default (int);
        public int KillSignal
        {
            get
            {
                return _KillSignal;
            }

            set
            {
                _KillSignal = (value);
            }
        }

        private bool _SendSIGKILL = default (bool);
        public bool SendSIGKILL
        {
            get
            {
                return _SendSIGKILL;
            }

            set
            {
                _SendSIGKILL = (value);
            }
        }

        private bool _SendSIGHUP = default (bool);
        public bool SendSIGHUP
        {
            get
            {
                return _SendSIGHUP;
            }

            set
            {
                _SendSIGHUP = (value);
            }
        }
    }

    static class ServiceExtensions
    {
        public static Task<string> GetTypeAsync(this IService o) => o.GetAsync<string>("Type");
        public static Task<string> GetRestartAsync(this IService o) => o.GetAsync<string>("Restart");
        public static Task<string> GetPIDFileAsync(this IService o) => o.GetAsync<string>("PIDFile");
        public static Task<string> GetNotifyAccessAsync(this IService o) => o.GetAsync<string>("NotifyAccess");
        public static Task<ulong> GetRestartUSecAsync(this IService o) => o.GetAsync<ulong>("RestartUSec");
        public static Task<ulong> GetTimeoutStartUSecAsync(this IService o) => o.GetAsync<ulong>("TimeoutStartUSec");
        public static Task<ulong> GetTimeoutStopUSecAsync(this IService o) => o.GetAsync<ulong>("TimeoutStopUSec");
        public static Task<ulong> GetRuntimeMaxUSecAsync(this IService o) => o.GetAsync<ulong>("RuntimeMaxUSec");
        public static Task<ulong> GetWatchdogUSecAsync(this IService o) => o.GetAsync<ulong>("WatchdogUSec");
        public static Task<ulong> GetWatchdogTimestampAsync(this IService o) => o.GetAsync<ulong>("WatchdogTimestamp");
        public static Task<ulong> GetWatchdogTimestampMonotonicAsync(this IService o) => o.GetAsync<ulong>("WatchdogTimestampMonotonic");
        public static Task<string> GetFailureActionAsync(this IService o) => o.GetAsync<string>("FailureAction");
        public static Task<bool> GetPermissionsStartOnlyAsync(this IService o) => o.GetAsync<bool>("PermissionsStartOnly");
        public static Task<bool> GetRootDirectoryStartOnlyAsync(this IService o) => o.GetAsync<bool>("RootDirectoryStartOnly");
        public static Task<bool> GetRemainAfterExitAsync(this IService o) => o.GetAsync<bool>("RemainAfterExit");
        public static Task<bool> GetGuessMainPIDAsync(this IService o) => o.GetAsync<bool>("GuessMainPID");
        public static Task<uint> GetMainPIDAsync(this IService o) => o.GetAsync<uint>("MainPID");
        public static Task<uint> GetControlPIDAsync(this IService o) => o.GetAsync<uint>("ControlPID");
        public static Task<string> GetBusNameAsync(this IService o) => o.GetAsync<string>("BusName");
        public static Task<uint> GetFileDescriptorStoreMaxAsync(this IService o) => o.GetAsync<uint>("FileDescriptorStoreMax");
        public static Task<uint> GetNFileDescriptorStoreAsync(this IService o) => o.GetAsync<uint>("NFileDescriptorStore");
        public static Task<string> GetStatusTextAsync(this IService o) => o.GetAsync<string>("StatusText");
        public static Task<int> GetStatusErrnoAsync(this IService o) => o.GetAsync<int>("StatusErrno");
        public static Task<string> GetResultAsync(this IService o) => o.GetAsync<string>("Result");
        public static Task<string> GetUSBFunctionDescriptorsAsync(this IService o) => o.GetAsync<string>("USBFunctionDescriptors");
        public static Task<string> GetUSBFunctionStringsAsync(this IService o) => o.GetAsync<string>("USBFunctionStrings");
        public static Task<ulong> GetExecMainStartTimestampAsync(this IService o) => o.GetAsync<ulong>("ExecMainStartTimestamp");
        public static Task<ulong> GetExecMainStartTimestampMonotonicAsync(this IService o) => o.GetAsync<ulong>("ExecMainStartTimestampMonotonic");
        public static Task<ulong> GetExecMainExitTimestampAsync(this IService o) => o.GetAsync<ulong>("ExecMainExitTimestamp");
        public static Task<ulong> GetExecMainExitTimestampMonotonicAsync(this IService o) => o.GetAsync<ulong>("ExecMainExitTimestampMonotonic");
        public static Task<uint> GetExecMainPIDAsync(this IService o) => o.GetAsync<uint>("ExecMainPID");
        public static Task<int> GetExecMainCodeAsync(this IService o) => o.GetAsync<int>("ExecMainCode");
        public static Task<int> GetExecMainStatusAsync(this IService o) => o.GetAsync<int>("ExecMainStatus");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStartPreAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStartPre");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStartAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStart");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStartPostAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStartPost");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecReloadAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecReload");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStopAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStop");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStopPostAsync(this IService o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStopPost");
        public static Task<string> GetSliceAsync(this IService o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this IService o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this IService o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this IService o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this IService o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this IService o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this IService o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this IService o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this IService o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this IService o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this IService o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this IService o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this IService o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this IService o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this IService o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this IService o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this IService o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this IService o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this IService o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this IService o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this IService o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this IService o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this IService o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this IService o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this IService o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this IService o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this IService o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this IService o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this IService o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this IService o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this IService o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this IService o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this IService o) => o.GetAsync<ulong>("TasksMax");
        public static Task<string[]> GetEnvironmentAsync(this IService o) => o.GetAsync<string[]>("Environment");
        public static Task<(string, bool)[]> GetEnvironmentFilesAsync(this IService o) => o.GetAsync<(string, bool)[]>("EnvironmentFiles");
        public static Task<string[]> GetPassEnvironmentAsync(this IService o) => o.GetAsync<string[]>("PassEnvironment");
        public static Task<uint> GetUMaskAsync(this IService o) => o.GetAsync<uint>("UMask");
        public static Task<ulong> GetLimitCPUAsync(this IService o) => o.GetAsync<ulong>("LimitCPU");
        public static Task<ulong> GetLimitCPUSoftAsync(this IService o) => o.GetAsync<ulong>("LimitCPUSoft");
        public static Task<ulong> GetLimitFSIZEAsync(this IService o) => o.GetAsync<ulong>("LimitFSIZE");
        public static Task<ulong> GetLimitFSIZESoftAsync(this IService o) => o.GetAsync<ulong>("LimitFSIZESoft");
        public static Task<ulong> GetLimitDATAAsync(this IService o) => o.GetAsync<ulong>("LimitDATA");
        public static Task<ulong> GetLimitDATASoftAsync(this IService o) => o.GetAsync<ulong>("LimitDATASoft");
        public static Task<ulong> GetLimitSTACKAsync(this IService o) => o.GetAsync<ulong>("LimitSTACK");
        public static Task<ulong> GetLimitSTACKSoftAsync(this IService o) => o.GetAsync<ulong>("LimitSTACKSoft");
        public static Task<ulong> GetLimitCOREAsync(this IService o) => o.GetAsync<ulong>("LimitCORE");
        public static Task<ulong> GetLimitCORESoftAsync(this IService o) => o.GetAsync<ulong>("LimitCORESoft");
        public static Task<ulong> GetLimitRSSAsync(this IService o) => o.GetAsync<ulong>("LimitRSS");
        public static Task<ulong> GetLimitRSSSoftAsync(this IService o) => o.GetAsync<ulong>("LimitRSSSoft");
        public static Task<ulong> GetLimitNOFILEAsync(this IService o) => o.GetAsync<ulong>("LimitNOFILE");
        public static Task<ulong> GetLimitNOFILESoftAsync(this IService o) => o.GetAsync<ulong>("LimitNOFILESoft");
        public static Task<ulong> GetLimitASAsync(this IService o) => o.GetAsync<ulong>("LimitAS");
        public static Task<ulong> GetLimitASSoftAsync(this IService o) => o.GetAsync<ulong>("LimitASSoft");
        public static Task<ulong> GetLimitNPROCAsync(this IService o) => o.GetAsync<ulong>("LimitNPROC");
        public static Task<ulong> GetLimitNPROCSoftAsync(this IService o) => o.GetAsync<ulong>("LimitNPROCSoft");
        public static Task<ulong> GetLimitMEMLOCKAsync(this IService o) => o.GetAsync<ulong>("LimitMEMLOCK");
        public static Task<ulong> GetLimitMEMLOCKSoftAsync(this IService o) => o.GetAsync<ulong>("LimitMEMLOCKSoft");
        public static Task<ulong> GetLimitLOCKSAsync(this IService o) => o.GetAsync<ulong>("LimitLOCKS");
        public static Task<ulong> GetLimitLOCKSSoftAsync(this IService o) => o.GetAsync<ulong>("LimitLOCKSSoft");
        public static Task<ulong> GetLimitSIGPENDINGAsync(this IService o) => o.GetAsync<ulong>("LimitSIGPENDING");
        public static Task<ulong> GetLimitSIGPENDINGSoftAsync(this IService o) => o.GetAsync<ulong>("LimitSIGPENDINGSoft");
        public static Task<ulong> GetLimitMSGQUEUEAsync(this IService o) => o.GetAsync<ulong>("LimitMSGQUEUE");
        public static Task<ulong> GetLimitMSGQUEUESoftAsync(this IService o) => o.GetAsync<ulong>("LimitMSGQUEUESoft");
        public static Task<ulong> GetLimitNICEAsync(this IService o) => o.GetAsync<ulong>("LimitNICE");
        public static Task<ulong> GetLimitNICESoftAsync(this IService o) => o.GetAsync<ulong>("LimitNICESoft");
        public static Task<ulong> GetLimitRTPRIOAsync(this IService o) => o.GetAsync<ulong>("LimitRTPRIO");
        public static Task<ulong> GetLimitRTPRIOSoftAsync(this IService o) => o.GetAsync<ulong>("LimitRTPRIOSoft");
        public static Task<ulong> GetLimitRTTIMEAsync(this IService o) => o.GetAsync<ulong>("LimitRTTIME");
        public static Task<ulong> GetLimitRTTIMESoftAsync(this IService o) => o.GetAsync<ulong>("LimitRTTIMESoft");
        public static Task<string> GetWorkingDirectoryAsync(this IService o) => o.GetAsync<string>("WorkingDirectory");
        public static Task<string> GetRootDirectoryAsync(this IService o) => o.GetAsync<string>("RootDirectory");
        public static Task<int> GetOOMScoreAdjustAsync(this IService o) => o.GetAsync<int>("OOMScoreAdjust");
        public static Task<int> GetNiceAsync(this IService o) => o.GetAsync<int>("Nice");
        public static Task<int> GetIOSchedulingAsync(this IService o) => o.GetAsync<int>("IOScheduling");
        public static Task<int> GetCPUSchedulingPolicyAsync(this IService o) => o.GetAsync<int>("CPUSchedulingPolicy");
        public static Task<int> GetCPUSchedulingPriorityAsync(this IService o) => o.GetAsync<int>("CPUSchedulingPriority");
        public static Task<byte[]> GetCPUAffinityAsync(this IService o) => o.GetAsync<byte[]>("CPUAffinity");
        public static Task<ulong> GetTimerSlackNSecAsync(this IService o) => o.GetAsync<ulong>("TimerSlackNSec");
        public static Task<bool> GetCPUSchedulingResetOnForkAsync(this IService o) => o.GetAsync<bool>("CPUSchedulingResetOnFork");
        public static Task<bool> GetNonBlockingAsync(this IService o) => o.GetAsync<bool>("NonBlocking");
        public static Task<string> GetStandardInputAsync(this IService o) => o.GetAsync<string>("StandardInput");
        public static Task<string> GetStandardOutputAsync(this IService o) => o.GetAsync<string>("StandardOutput");
        public static Task<string> GetStandardErrorAsync(this IService o) => o.GetAsync<string>("StandardError");
        public static Task<string> GetTTYPathAsync(this IService o) => o.GetAsync<string>("TTYPath");
        public static Task<bool> GetTTYResetAsync(this IService o) => o.GetAsync<bool>("TTYReset");
        public static Task<bool> GetTTYVHangupAsync(this IService o) => o.GetAsync<bool>("TTYVHangup");
        public static Task<bool> GetTTYVTDisallocateAsync(this IService o) => o.GetAsync<bool>("TTYVTDisallocate");
        public static Task<int> GetSyslogPriorityAsync(this IService o) => o.GetAsync<int>("SyslogPriority");
        public static Task<string> GetSyslogIdentifierAsync(this IService o) => o.GetAsync<string>("SyslogIdentifier");
        public static Task<bool> GetSyslogLevelPrefixAsync(this IService o) => o.GetAsync<bool>("SyslogLevelPrefix");
        public static Task<int> GetSyslogLevelAsync(this IService o) => o.GetAsync<int>("SyslogLevel");
        public static Task<int> GetSyslogFacilityAsync(this IService o) => o.GetAsync<int>("SyslogFacility");
        public static Task<int> GetSecureBitsAsync(this IService o) => o.GetAsync<int>("SecureBits");
        public static Task<ulong> GetCapabilityBoundingSetAsync(this IService o) => o.GetAsync<ulong>("CapabilityBoundingSet");
        public static Task<ulong> GetAmbientCapabilitiesAsync(this IService o) => o.GetAsync<ulong>("AmbientCapabilities");
        public static Task<string> GetUserAsync(this IService o) => o.GetAsync<string>("User");
        public static Task<string> GetGroupAsync(this IService o) => o.GetAsync<string>("Group");
        public static Task<string[]> GetSupplementaryGroupsAsync(this IService o) => o.GetAsync<string[]>("SupplementaryGroups");
        public static Task<string> GetPAMNameAsync(this IService o) => o.GetAsync<string>("PAMName");
        public static Task<string[]> GetReadWritePathsAsync(this IService o) => o.GetAsync<string[]>("ReadWritePaths");
        public static Task<string[]> GetReadOnlyPathsAsync(this IService o) => o.GetAsync<string[]>("ReadOnlyPaths");
        public static Task<string[]> GetInaccessiblePathsAsync(this IService o) => o.GetAsync<string[]>("InaccessiblePaths");
        public static Task<ulong> GetMountFlagsAsync(this IService o) => o.GetAsync<ulong>("MountFlags");
        public static Task<bool> GetPrivateTmpAsync(this IService o) => o.GetAsync<bool>("PrivateTmp");
        public static Task<bool> GetPrivateNetworkAsync(this IService o) => o.GetAsync<bool>("PrivateNetwork");
        public static Task<bool> GetPrivateDevicesAsync(this IService o) => o.GetAsync<bool>("PrivateDevices");
        public static Task<string> GetProtectHomeAsync(this IService o) => o.GetAsync<string>("ProtectHome");
        public static Task<string> GetProtectSystemAsync(this IService o) => o.GetAsync<string>("ProtectSystem");
        public static Task<bool> GetSameProcessGroupAsync(this IService o) => o.GetAsync<bool>("SameProcessGroup");
        public static Task<string> GetUtmpIdentifierAsync(this IService o) => o.GetAsync<string>("UtmpIdentifier");
        public static Task<string> GetUtmpModeAsync(this IService o) => o.GetAsync<string>("UtmpMode");
        public static Task<(bool, string)> GetSELinuxContextAsync(this IService o) => o.GetAsync<(bool, string)>("SELinuxContext");
        public static Task<(bool, string)> GetAppArmorProfileAsync(this IService o) => o.GetAsync<(bool, string)>("AppArmorProfile");
        public static Task<(bool, string)> GetSmackProcessLabelAsync(this IService o) => o.GetAsync<(bool, string)>("SmackProcessLabel");
        public static Task<bool> GetIgnoreSIGPIPEAsync(this IService o) => o.GetAsync<bool>("IgnoreSIGPIPE");
        public static Task<bool> GetNoNewPrivilegesAsync(this IService o) => o.GetAsync<bool>("NoNewPrivileges");
        public static Task<(bool, string[])> GetSystemCallFilterAsync(this IService o) => o.GetAsync<(bool, string[])>("SystemCallFilter");
        public static Task<string[]> GetSystemCallArchitecturesAsync(this IService o) => o.GetAsync<string[]>("SystemCallArchitectures");
        public static Task<int> GetSystemCallErrorNumberAsync(this IService o) => o.GetAsync<int>("SystemCallErrorNumber");
        public static Task<string> GetPersonalityAsync(this IService o) => o.GetAsync<string>("Personality");
        public static Task<(bool, string[])> GetRestrictAddressFamiliesAsync(this IService o) => o.GetAsync<(bool, string[])>("RestrictAddressFamilies");
        public static Task<uint> GetRuntimeDirectoryModeAsync(this IService o) => o.GetAsync<uint>("RuntimeDirectoryMode");
        public static Task<string[]> GetRuntimeDirectoryAsync(this IService o) => o.GetAsync<string[]>("RuntimeDirectory");
        public static Task<bool> GetMemoryDenyWriteExecuteAsync(this IService o) => o.GetAsync<bool>("MemoryDenyWriteExecute");
        public static Task<bool> GetRestrictRealtimeAsync(this IService o) => o.GetAsync<bool>("RestrictRealtime");
        public static Task<string> GetKillModeAsync(this IService o) => o.GetAsync<string>("KillMode");
        public static Task<int> GetKillSignalAsync(this IService o) => o.GetAsync<int>("KillSignal");
        public static Task<bool> GetSendSIGKILLAsync(this IService o) => o.GetAsync<bool>("SendSIGKILL");
        public static Task<bool> GetSendSIGHUPAsync(this IService o) => o.GetAsync<bool>("SendSIGHUP");
    }

    [DBusInterface("org.freedesktop.systemd1.Unit")]
    interface IUnit : IDBusObject
    {
        Task<ObjectPath> StartAsync(string arg0);
        Task<ObjectPath> StopAsync(string arg0);
        Task<ObjectPath> ReloadAsync(string arg0);
        Task<ObjectPath> RestartAsync(string arg0);
        Task<ObjectPath> TryRestartAsync(string arg0);
        Task<ObjectPath> ReloadOrRestartAsync(string arg0);
        Task<ObjectPath> ReloadOrTryRestartAsync(string arg0);
        Task KillAsync(string arg0, int arg1);
        Task ResetFailedAsync();
        Task SetPropertiesAsync(bool arg0, (string, object)[] arg1);
        Task<T> GetAsync<T>(string prop);
        Task<UnitProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class UnitProperties
    {
        private string _Id = default (string);
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = (value);
            }
        }

        private string[] _Names = default (string[]);
        public string[] Names
        {
            get
            {
                return _Names;
            }

            set
            {
                _Names = (value);
            }
        }

        private string _Following = default (string);
        public string Following
        {
            get
            {
                return _Following;
            }

            set
            {
                _Following = (value);
            }
        }

        private string[] _Requires = default (string[]);
        public string[] Requires
        {
            get
            {
                return _Requires;
            }

            set
            {
                _Requires = (value);
            }
        }

        private string[] _Requisite = default (string[]);
        public string[] Requisite
        {
            get
            {
                return _Requisite;
            }

            set
            {
                _Requisite = (value);
            }
        }

        private string[] _Wants = default (string[]);
        public string[] Wants
        {
            get
            {
                return _Wants;
            }

            set
            {
                _Wants = (value);
            }
        }

        private string[] _BindsTo = default (string[]);
        public string[] BindsTo
        {
            get
            {
                return _BindsTo;
            }

            set
            {
                _BindsTo = (value);
            }
        }

        private string[] _PartOf = default (string[]);
        public string[] PartOf
        {
            get
            {
                return _PartOf;
            }

            set
            {
                _PartOf = (value);
            }
        }

        private string[] _RequiredBy = default (string[]);
        public string[] RequiredBy
        {
            get
            {
                return _RequiredBy;
            }

            set
            {
                _RequiredBy = (value);
            }
        }

        private string[] _RequisiteOf = default (string[]);
        public string[] RequisiteOf
        {
            get
            {
                return _RequisiteOf;
            }

            set
            {
                _RequisiteOf = (value);
            }
        }

        private string[] _WantedBy = default (string[]);
        public string[] WantedBy
        {
            get
            {
                return _WantedBy;
            }

            set
            {
                _WantedBy = (value);
            }
        }

        private string[] _BoundBy = default (string[]);
        public string[] BoundBy
        {
            get
            {
                return _BoundBy;
            }

            set
            {
                _BoundBy = (value);
            }
        }

        private string[] _ConsistsOf = default (string[]);
        public string[] ConsistsOf
        {
            get
            {
                return _ConsistsOf;
            }

            set
            {
                _ConsistsOf = (value);
            }
        }

        private string[] _Conflicts = default (string[]);
        public string[] Conflicts
        {
            get
            {
                return _Conflicts;
            }

            set
            {
                _Conflicts = (value);
            }
        }

        private string[] _ConflictedBy = default (string[]);
        public string[] ConflictedBy
        {
            get
            {
                return _ConflictedBy;
            }

            set
            {
                _ConflictedBy = (value);
            }
        }

        private string[] _Before = default (string[]);
        public string[] Before
        {
            get
            {
                return _Before;
            }

            set
            {
                _Before = (value);
            }
        }

        private string[] _After = default (string[]);
        public string[] After
        {
            get
            {
                return _After;
            }

            set
            {
                _After = (value);
            }
        }

        private string[] _OnFailure = default (string[]);
        public string[] OnFailure
        {
            get
            {
                return _OnFailure;
            }

            set
            {
                _OnFailure = (value);
            }
        }

        private string[] _Triggers = default (string[]);
        public string[] Triggers
        {
            get
            {
                return _Triggers;
            }

            set
            {
                _Triggers = (value);
            }
        }

        private string[] _TriggeredBy = default (string[]);
        public string[] TriggeredBy
        {
            get
            {
                return _TriggeredBy;
            }

            set
            {
                _TriggeredBy = (value);
            }
        }

        private string[] _PropagatesReloadTo = default (string[]);
        public string[] PropagatesReloadTo
        {
            get
            {
                return _PropagatesReloadTo;
            }

            set
            {
                _PropagatesReloadTo = (value);
            }
        }

        private string[] _ReloadPropagatedFrom = default (string[]);
        public string[] ReloadPropagatedFrom
        {
            get
            {
                return _ReloadPropagatedFrom;
            }

            set
            {
                _ReloadPropagatedFrom = (value);
            }
        }

        private string[] _JoinsNamespaceOf = default (string[]);
        public string[] JoinsNamespaceOf
        {
            get
            {
                return _JoinsNamespaceOf;
            }

            set
            {
                _JoinsNamespaceOf = (value);
            }
        }

        private string[] _RequiresMountsFor = default (string[]);
        public string[] RequiresMountsFor
        {
            get
            {
                return _RequiresMountsFor;
            }

            set
            {
                _RequiresMountsFor = (value);
            }
        }

        private string[] _Documentation = default (string[]);
        public string[] Documentation
        {
            get
            {
                return _Documentation;
            }

            set
            {
                _Documentation = (value);
            }
        }

        private string _Description = default (string);
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = (value);
            }
        }

        private string _LoadState = default (string);
        public string LoadState
        {
            get
            {
                return _LoadState;
            }

            set
            {
                _LoadState = (value);
            }
        }

        private string _ActiveState = default (string);
        public string ActiveState
        {
            get
            {
                return _ActiveState;
            }

            set
            {
                _ActiveState = (value);
            }
        }

        private string _SubState = default (string);
        public string SubState
        {
            get
            {
                return _SubState;
            }

            set
            {
                _SubState = (value);
            }
        }

        private string _FragmentPath = default (string);
        public string FragmentPath
        {
            get
            {
                return _FragmentPath;
            }

            set
            {
                _FragmentPath = (value);
            }
        }

        private string _SourcePath = default (string);
        public string SourcePath
        {
            get
            {
                return _SourcePath;
            }

            set
            {
                _SourcePath = (value);
            }
        }

        private string[] _DropInPaths = default (string[]);
        public string[] DropInPaths
        {
            get
            {
                return _DropInPaths;
            }

            set
            {
                _DropInPaths = (value);
            }
        }

        private string _UnitFileState = default (string);
        public string UnitFileState
        {
            get
            {
                return _UnitFileState;
            }

            set
            {
                _UnitFileState = (value);
            }
        }

        private string _UnitFilePreset = default (string);
        public string UnitFilePreset
        {
            get
            {
                return _UnitFilePreset;
            }

            set
            {
                _UnitFilePreset = (value);
            }
        }

        private ulong _StateChangeTimestamp = default (ulong);
        public ulong StateChangeTimestamp
        {
            get
            {
                return _StateChangeTimestamp;
            }

            set
            {
                _StateChangeTimestamp = (value);
            }
        }

        private ulong _StateChangeTimestampMonotonic = default (ulong);
        public ulong StateChangeTimestampMonotonic
        {
            get
            {
                return _StateChangeTimestampMonotonic;
            }

            set
            {
                _StateChangeTimestampMonotonic = (value);
            }
        }

        private ulong _InactiveExitTimestamp = default (ulong);
        public ulong InactiveExitTimestamp
        {
            get
            {
                return _InactiveExitTimestamp;
            }

            set
            {
                _InactiveExitTimestamp = (value);
            }
        }

        private ulong _InactiveExitTimestampMonotonic = default (ulong);
        public ulong InactiveExitTimestampMonotonic
        {
            get
            {
                return _InactiveExitTimestampMonotonic;
            }

            set
            {
                _InactiveExitTimestampMonotonic = (value);
            }
        }

        private ulong _ActiveEnterTimestamp = default (ulong);
        public ulong ActiveEnterTimestamp
        {
            get
            {
                return _ActiveEnterTimestamp;
            }

            set
            {
                _ActiveEnterTimestamp = (value);
            }
        }

        private ulong _ActiveEnterTimestampMonotonic = default (ulong);
        public ulong ActiveEnterTimestampMonotonic
        {
            get
            {
                return _ActiveEnterTimestampMonotonic;
            }

            set
            {
                _ActiveEnterTimestampMonotonic = (value);
            }
        }

        private ulong _ActiveExitTimestamp = default (ulong);
        public ulong ActiveExitTimestamp
        {
            get
            {
                return _ActiveExitTimestamp;
            }

            set
            {
                _ActiveExitTimestamp = (value);
            }
        }

        private ulong _ActiveExitTimestampMonotonic = default (ulong);
        public ulong ActiveExitTimestampMonotonic
        {
            get
            {
                return _ActiveExitTimestampMonotonic;
            }

            set
            {
                _ActiveExitTimestampMonotonic = (value);
            }
        }

        private ulong _InactiveEnterTimestamp = default (ulong);
        public ulong InactiveEnterTimestamp
        {
            get
            {
                return _InactiveEnterTimestamp;
            }

            set
            {
                _InactiveEnterTimestamp = (value);
            }
        }

        private ulong _InactiveEnterTimestampMonotonic = default (ulong);
        public ulong InactiveEnterTimestampMonotonic
        {
            get
            {
                return _InactiveEnterTimestampMonotonic;
            }

            set
            {
                _InactiveEnterTimestampMonotonic = (value);
            }
        }

        private bool _CanStart = default (bool);
        public bool CanStart
        {
            get
            {
                return _CanStart;
            }

            set
            {
                _CanStart = (value);
            }
        }

        private bool _CanStop = default (bool);
        public bool CanStop
        {
            get
            {
                return _CanStop;
            }

            set
            {
                _CanStop = (value);
            }
        }

        private bool _CanReload = default (bool);
        public bool CanReload
        {
            get
            {
                return _CanReload;
            }

            set
            {
                _CanReload = (value);
            }
        }

        private bool _CanIsolate = default (bool);
        public bool CanIsolate
        {
            get
            {
                return _CanIsolate;
            }

            set
            {
                _CanIsolate = (value);
            }
        }

        private (uint, ObjectPath)_Job = default ((uint, ObjectPath));
        public (uint, ObjectPath)Job
        {
            get
            {
                return _Job;
            }

            set
            {
                _Job = (value);
            }
        }

        private bool _StopWhenUnneeded = default (bool);
        public bool StopWhenUnneeded
        {
            get
            {
                return _StopWhenUnneeded;
            }

            set
            {
                _StopWhenUnneeded = (value);
            }
        }

        private bool _RefuseManualStart = default (bool);
        public bool RefuseManualStart
        {
            get
            {
                return _RefuseManualStart;
            }

            set
            {
                _RefuseManualStart = (value);
            }
        }

        private bool _RefuseManualStop = default (bool);
        public bool RefuseManualStop
        {
            get
            {
                return _RefuseManualStop;
            }

            set
            {
                _RefuseManualStop = (value);
            }
        }

        private bool _AllowIsolate = default (bool);
        public bool AllowIsolate
        {
            get
            {
                return _AllowIsolate;
            }

            set
            {
                _AllowIsolate = (value);
            }
        }

        private bool _DefaultDependencies = default (bool);
        public bool DefaultDependencies
        {
            get
            {
                return _DefaultDependencies;
            }

            set
            {
                _DefaultDependencies = (value);
            }
        }

        private string _OnFailureJobMode = default (string);
        public string OnFailureJobMode
        {
            get
            {
                return _OnFailureJobMode;
            }

            set
            {
                _OnFailureJobMode = (value);
            }
        }

        private bool _IgnoreOnIsolate = default (bool);
        public bool IgnoreOnIsolate
        {
            get
            {
                return _IgnoreOnIsolate;
            }

            set
            {
                _IgnoreOnIsolate = (value);
            }
        }

        private bool _NeedDaemonReload = default (bool);
        public bool NeedDaemonReload
        {
            get
            {
                return _NeedDaemonReload;
            }

            set
            {
                _NeedDaemonReload = (value);
            }
        }

        private ulong _JobTimeoutUSec = default (ulong);
        public ulong JobTimeoutUSec
        {
            get
            {
                return _JobTimeoutUSec;
            }

            set
            {
                _JobTimeoutUSec = (value);
            }
        }

        private string _JobTimeoutAction = default (string);
        public string JobTimeoutAction
        {
            get
            {
                return _JobTimeoutAction;
            }

            set
            {
                _JobTimeoutAction = (value);
            }
        }

        private string _JobTimeoutRebootArgument = default (string);
        public string JobTimeoutRebootArgument
        {
            get
            {
                return _JobTimeoutRebootArgument;
            }

            set
            {
                _JobTimeoutRebootArgument = (value);
            }
        }

        private bool _ConditionResult = default (bool);
        public bool ConditionResult
        {
            get
            {
                return _ConditionResult;
            }

            set
            {
                _ConditionResult = (value);
            }
        }

        private bool _AssertResult = default (bool);
        public bool AssertResult
        {
            get
            {
                return _AssertResult;
            }

            set
            {
                _AssertResult = (value);
            }
        }

        private ulong _ConditionTimestamp = default (ulong);
        public ulong ConditionTimestamp
        {
            get
            {
                return _ConditionTimestamp;
            }

            set
            {
                _ConditionTimestamp = (value);
            }
        }

        private ulong _ConditionTimestampMonotonic = default (ulong);
        public ulong ConditionTimestampMonotonic
        {
            get
            {
                return _ConditionTimestampMonotonic;
            }

            set
            {
                _ConditionTimestampMonotonic = (value);
            }
        }

        private ulong _AssertTimestamp = default (ulong);
        public ulong AssertTimestamp
        {
            get
            {
                return _AssertTimestamp;
            }

            set
            {
                _AssertTimestamp = (value);
            }
        }

        private ulong _AssertTimestampMonotonic = default (ulong);
        public ulong AssertTimestampMonotonic
        {
            get
            {
                return _AssertTimestampMonotonic;
            }

            set
            {
                _AssertTimestampMonotonic = (value);
            }
        }

        private (string, bool, bool, string, int)[] _Conditions = default ((string, bool, bool, string, int)[]);
        public (string, bool, bool, string, int)[] Conditions
        {
            get
            {
                return _Conditions;
            }

            set
            {
                _Conditions = (value);
            }
        }

        private (string, bool, bool, string, int)[] _Asserts = default ((string, bool, bool, string, int)[]);
        public (string, bool, bool, string, int)[] Asserts
        {
            get
            {
                return _Asserts;
            }

            set
            {
                _Asserts = (value);
            }
        }

        private (string, string)_LoadError = default ((string, string));
        public (string, string)LoadError
        {
            get
            {
                return _LoadError;
            }

            set
            {
                _LoadError = (value);
            }
        }

        private bool _Transient = default (bool);
        public bool Transient
        {
            get
            {
                return _Transient;
            }

            set
            {
                _Transient = (value);
            }
        }

        private ulong _StartLimitIntervalSec = default (ulong);
        public ulong StartLimitIntervalSec
        {
            get
            {
                return _StartLimitIntervalSec;
            }

            set
            {
                _StartLimitIntervalSec = (value);
            }
        }

        private uint _StartLimitBurst = default (uint);
        public uint StartLimitBurst
        {
            get
            {
                return _StartLimitBurst;
            }

            set
            {
                _StartLimitBurst = (value);
            }
        }

        private string _StartLimitAction = default (string);
        public string StartLimitAction
        {
            get
            {
                return _StartLimitAction;
            }

            set
            {
                _StartLimitAction = (value);
            }
        }

        private string _RebootArgument = default (string);
        public string RebootArgument
        {
            get
            {
                return _RebootArgument;
            }

            set
            {
                _RebootArgument = (value);
            }
        }
    }

    static class UnitExtensions
    {
        public static Task<string> GetIdAsync(this IUnit o) => o.GetAsync<string>("Id");
        public static Task<string[]> GetNamesAsync(this IUnit o) => o.GetAsync<string[]>("Names");
        public static Task<string> GetFollowingAsync(this IUnit o) => o.GetAsync<string>("Following");
        public static Task<string[]> GetRequiresAsync(this IUnit o) => o.GetAsync<string[]>("Requires");
        public static Task<string[]> GetRequisiteAsync(this IUnit o) => o.GetAsync<string[]>("Requisite");
        public static Task<string[]> GetWantsAsync(this IUnit o) => o.GetAsync<string[]>("Wants");
        public static Task<string[]> GetBindsToAsync(this IUnit o) => o.GetAsync<string[]>("BindsTo");
        public static Task<string[]> GetPartOfAsync(this IUnit o) => o.GetAsync<string[]>("PartOf");
        public static Task<string[]> GetRequiredByAsync(this IUnit o) => o.GetAsync<string[]>("RequiredBy");
        public static Task<string[]> GetRequisiteOfAsync(this IUnit o) => o.GetAsync<string[]>("RequisiteOf");
        public static Task<string[]> GetWantedByAsync(this IUnit o) => o.GetAsync<string[]>("WantedBy");
        public static Task<string[]> GetBoundByAsync(this IUnit o) => o.GetAsync<string[]>("BoundBy");
        public static Task<string[]> GetConsistsOfAsync(this IUnit o) => o.GetAsync<string[]>("ConsistsOf");
        public static Task<string[]> GetConflictsAsync(this IUnit o) => o.GetAsync<string[]>("Conflicts");
        public static Task<string[]> GetConflictedByAsync(this IUnit o) => o.GetAsync<string[]>("ConflictedBy");
        public static Task<string[]> GetBeforeAsync(this IUnit o) => o.GetAsync<string[]>("Before");
        public static Task<string[]> GetAfterAsync(this IUnit o) => o.GetAsync<string[]>("After");
        public static Task<string[]> GetOnFailureAsync(this IUnit o) => o.GetAsync<string[]>("OnFailure");
        public static Task<string[]> GetTriggersAsync(this IUnit o) => o.GetAsync<string[]>("Triggers");
        public static Task<string[]> GetTriggeredByAsync(this IUnit o) => o.GetAsync<string[]>("TriggeredBy");
        public static Task<string[]> GetPropagatesReloadToAsync(this IUnit o) => o.GetAsync<string[]>("PropagatesReloadTo");
        public static Task<string[]> GetReloadPropagatedFromAsync(this IUnit o) => o.GetAsync<string[]>("ReloadPropagatedFrom");
        public static Task<string[]> GetJoinsNamespaceOfAsync(this IUnit o) => o.GetAsync<string[]>("JoinsNamespaceOf");
        public static Task<string[]> GetRequiresMountsForAsync(this IUnit o) => o.GetAsync<string[]>("RequiresMountsFor");
        public static Task<string[]> GetDocumentationAsync(this IUnit o) => o.GetAsync<string[]>("Documentation");
        public static Task<string> GetDescriptionAsync(this IUnit o) => o.GetAsync<string>("Description");
        public static Task<string> GetLoadStateAsync(this IUnit o) => o.GetAsync<string>("LoadState");
        public static Task<string> GetActiveStateAsync(this IUnit o) => o.GetAsync<string>("ActiveState");
        public static Task<string> GetSubStateAsync(this IUnit o) => o.GetAsync<string>("SubState");
        public static Task<string> GetFragmentPathAsync(this IUnit o) => o.GetAsync<string>("FragmentPath");
        public static Task<string> GetSourcePathAsync(this IUnit o) => o.GetAsync<string>("SourcePath");
        public static Task<string[]> GetDropInPathsAsync(this IUnit o) => o.GetAsync<string[]>("DropInPaths");
        public static Task<string> GetUnitFileStateAsync(this IUnit o) => o.GetAsync<string>("UnitFileState");
        public static Task<string> GetUnitFilePresetAsync(this IUnit o) => o.GetAsync<string>("UnitFilePreset");
        public static Task<ulong> GetStateChangeTimestampAsync(this IUnit o) => o.GetAsync<ulong>("StateChangeTimestamp");
        public static Task<ulong> GetStateChangeTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("StateChangeTimestampMonotonic");
        public static Task<ulong> GetInactiveExitTimestampAsync(this IUnit o) => o.GetAsync<ulong>("InactiveExitTimestamp");
        public static Task<ulong> GetInactiveExitTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("InactiveExitTimestampMonotonic");
        public static Task<ulong> GetActiveEnterTimestampAsync(this IUnit o) => o.GetAsync<ulong>("ActiveEnterTimestamp");
        public static Task<ulong> GetActiveEnterTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("ActiveEnterTimestampMonotonic");
        public static Task<ulong> GetActiveExitTimestampAsync(this IUnit o) => o.GetAsync<ulong>("ActiveExitTimestamp");
        public static Task<ulong> GetActiveExitTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("ActiveExitTimestampMonotonic");
        public static Task<ulong> GetInactiveEnterTimestampAsync(this IUnit o) => o.GetAsync<ulong>("InactiveEnterTimestamp");
        public static Task<ulong> GetInactiveEnterTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("InactiveEnterTimestampMonotonic");
        public static Task<bool> GetCanStartAsync(this IUnit o) => o.GetAsync<bool>("CanStart");
        public static Task<bool> GetCanStopAsync(this IUnit o) => o.GetAsync<bool>("CanStop");
        public static Task<bool> GetCanReloadAsync(this IUnit o) => o.GetAsync<bool>("CanReload");
        public static Task<bool> GetCanIsolateAsync(this IUnit o) => o.GetAsync<bool>("CanIsolate");
        public static Task<(uint, ObjectPath)> GetJobAsync(this IUnit o) => o.GetAsync<(uint, ObjectPath)>("Job");
        public static Task<bool> GetStopWhenUnneededAsync(this IUnit o) => o.GetAsync<bool>("StopWhenUnneeded");
        public static Task<bool> GetRefuseManualStartAsync(this IUnit o) => o.GetAsync<bool>("RefuseManualStart");
        public static Task<bool> GetRefuseManualStopAsync(this IUnit o) => o.GetAsync<bool>("RefuseManualStop");
        public static Task<bool> GetAllowIsolateAsync(this IUnit o) => o.GetAsync<bool>("AllowIsolate");
        public static Task<bool> GetDefaultDependenciesAsync(this IUnit o) => o.GetAsync<bool>("DefaultDependencies");
        public static Task<string> GetOnFailureJobModeAsync(this IUnit o) => o.GetAsync<string>("OnFailureJobMode");
        public static Task<bool> GetIgnoreOnIsolateAsync(this IUnit o) => o.GetAsync<bool>("IgnoreOnIsolate");
        public static Task<bool> GetNeedDaemonReloadAsync(this IUnit o) => o.GetAsync<bool>("NeedDaemonReload");
        public static Task<ulong> GetJobTimeoutUSecAsync(this IUnit o) => o.GetAsync<ulong>("JobTimeoutUSec");
        public static Task<string> GetJobTimeoutActionAsync(this IUnit o) => o.GetAsync<string>("JobTimeoutAction");
        public static Task<string> GetJobTimeoutRebootArgumentAsync(this IUnit o) => o.GetAsync<string>("JobTimeoutRebootArgument");
        public static Task<bool> GetConditionResultAsync(this IUnit o) => o.GetAsync<bool>("ConditionResult");
        public static Task<bool> GetAssertResultAsync(this IUnit o) => o.GetAsync<bool>("AssertResult");
        public static Task<ulong> GetConditionTimestampAsync(this IUnit o) => o.GetAsync<ulong>("ConditionTimestamp");
        public static Task<ulong> GetConditionTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("ConditionTimestampMonotonic");
        public static Task<ulong> GetAssertTimestampAsync(this IUnit o) => o.GetAsync<ulong>("AssertTimestamp");
        public static Task<ulong> GetAssertTimestampMonotonicAsync(this IUnit o) => o.GetAsync<ulong>("AssertTimestampMonotonic");
        public static Task<(string, bool, bool, string, int)[]> GetConditionsAsync(this IUnit o) => o.GetAsync<(string, bool, bool, string, int)[]>("Conditions");
        public static Task<(string, bool, bool, string, int)[]> GetAssertsAsync(this IUnit o) => o.GetAsync<(string, bool, bool, string, int)[]>("Asserts");
        public static Task<(string, string)> GetLoadErrorAsync(this IUnit o) => o.GetAsync<(string, string)>("LoadError");
        public static Task<bool> GetTransientAsync(this IUnit o) => o.GetAsync<bool>("Transient");
        public static Task<ulong> GetStartLimitIntervalSecAsync(this IUnit o) => o.GetAsync<ulong>("StartLimitIntervalSec");
        public static Task<uint> GetStartLimitBurstAsync(this IUnit o) => o.GetAsync<uint>("StartLimitBurst");
        public static Task<string> GetStartLimitActionAsync(this IUnit o) => o.GetAsync<string>("StartLimitAction");
        public static Task<string> GetRebootArgumentAsync(this IUnit o) => o.GetAsync<string>("RebootArgument");
    }

    [DBusInterface("org.freedesktop.systemd1.Device")]
    interface IDevice : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<DeviceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class DeviceProperties
    {
        private string _SysFSPath = default (string);
        public string SysFSPath
        {
            get
            {
                return _SysFSPath;
            }

            set
            {
                _SysFSPath = (value);
            }
        }
    }

    static class DeviceExtensions
    {
        public static Task<string> GetSysFSPathAsync(this IDevice o) => o.GetAsync<string>("SysFSPath");
    }

    [DBusInterface("org.freedesktop.systemd1.Swap")]
    interface ISwap : IDBusObject
    {
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<SwapProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SwapProperties
    {
        private string _What = default (string);
        public string What
        {
            get
            {
                return _What;
            }

            set
            {
                _What = (value);
            }
        }

        private int _Priority = default (int);
        public int Priority
        {
            get
            {
                return _Priority;
            }

            set
            {
                _Priority = (value);
            }
        }

        private string _Options = default (string);
        public string Options
        {
            get
            {
                return _Options;
            }

            set
            {
                _Options = (value);
            }
        }

        private ulong _TimeoutUSec = default (ulong);
        public ulong TimeoutUSec
        {
            get
            {
                return _TimeoutUSec;
            }

            set
            {
                _TimeoutUSec = (value);
            }
        }

        private uint _ControlPID = default (uint);
        public uint ControlPID
        {
            get
            {
                return _ControlPID;
            }

            set
            {
                _ControlPID = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecActivate = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecActivate
        {
            get
            {
                return _ExecActivate;
            }

            set
            {
                _ExecActivate = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecDeactivate = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecDeactivate
        {
            get
            {
                return _ExecDeactivate;
            }

            set
            {
                _ExecDeactivate = (value);
            }
        }

        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }

        private string[] _Environment = default (string[]);
        public string[] Environment
        {
            get
            {
                return _Environment;
            }

            set
            {
                _Environment = (value);
            }
        }

        private (string, bool)[] _EnvironmentFiles = default ((string, bool)[]);
        public (string, bool)[] EnvironmentFiles
        {
            get
            {
                return _EnvironmentFiles;
            }

            set
            {
                _EnvironmentFiles = (value);
            }
        }

        private string[] _PassEnvironment = default (string[]);
        public string[] PassEnvironment
        {
            get
            {
                return _PassEnvironment;
            }

            set
            {
                _PassEnvironment = (value);
            }
        }

        private uint _UMask = default (uint);
        public uint UMask
        {
            get
            {
                return _UMask;
            }

            set
            {
                _UMask = (value);
            }
        }

        private ulong _LimitCPU = default (ulong);
        public ulong LimitCPU
        {
            get
            {
                return _LimitCPU;
            }

            set
            {
                _LimitCPU = (value);
            }
        }

        private ulong _LimitCPUSoft = default (ulong);
        public ulong LimitCPUSoft
        {
            get
            {
                return _LimitCPUSoft;
            }

            set
            {
                _LimitCPUSoft = (value);
            }
        }

        private ulong _LimitFSIZE = default (ulong);
        public ulong LimitFSIZE
        {
            get
            {
                return _LimitFSIZE;
            }

            set
            {
                _LimitFSIZE = (value);
            }
        }

        private ulong _LimitFSIZESoft = default (ulong);
        public ulong LimitFSIZESoft
        {
            get
            {
                return _LimitFSIZESoft;
            }

            set
            {
                _LimitFSIZESoft = (value);
            }
        }

        private ulong _LimitDATA = default (ulong);
        public ulong LimitDATA
        {
            get
            {
                return _LimitDATA;
            }

            set
            {
                _LimitDATA = (value);
            }
        }

        private ulong _LimitDATASoft = default (ulong);
        public ulong LimitDATASoft
        {
            get
            {
                return _LimitDATASoft;
            }

            set
            {
                _LimitDATASoft = (value);
            }
        }

        private ulong _LimitSTACK = default (ulong);
        public ulong LimitSTACK
        {
            get
            {
                return _LimitSTACK;
            }

            set
            {
                _LimitSTACK = (value);
            }
        }

        private ulong _LimitSTACKSoft = default (ulong);
        public ulong LimitSTACKSoft
        {
            get
            {
                return _LimitSTACKSoft;
            }

            set
            {
                _LimitSTACKSoft = (value);
            }
        }

        private ulong _LimitCORE = default (ulong);
        public ulong LimitCORE
        {
            get
            {
                return _LimitCORE;
            }

            set
            {
                _LimitCORE = (value);
            }
        }

        private ulong _LimitCORESoft = default (ulong);
        public ulong LimitCORESoft
        {
            get
            {
                return _LimitCORESoft;
            }

            set
            {
                _LimitCORESoft = (value);
            }
        }

        private ulong _LimitRSS = default (ulong);
        public ulong LimitRSS
        {
            get
            {
                return _LimitRSS;
            }

            set
            {
                _LimitRSS = (value);
            }
        }

        private ulong _LimitRSSSoft = default (ulong);
        public ulong LimitRSSSoft
        {
            get
            {
                return _LimitRSSSoft;
            }

            set
            {
                _LimitRSSSoft = (value);
            }
        }

        private ulong _LimitNOFILE = default (ulong);
        public ulong LimitNOFILE
        {
            get
            {
                return _LimitNOFILE;
            }

            set
            {
                _LimitNOFILE = (value);
            }
        }

        private ulong _LimitNOFILESoft = default (ulong);
        public ulong LimitNOFILESoft
        {
            get
            {
                return _LimitNOFILESoft;
            }

            set
            {
                _LimitNOFILESoft = (value);
            }
        }

        private ulong _LimitAS = default (ulong);
        public ulong LimitAS
        {
            get
            {
                return _LimitAS;
            }

            set
            {
                _LimitAS = (value);
            }
        }

        private ulong _LimitASSoft = default (ulong);
        public ulong LimitASSoft
        {
            get
            {
                return _LimitASSoft;
            }

            set
            {
                _LimitASSoft = (value);
            }
        }

        private ulong _LimitNPROC = default (ulong);
        public ulong LimitNPROC
        {
            get
            {
                return _LimitNPROC;
            }

            set
            {
                _LimitNPROC = (value);
            }
        }

        private ulong _LimitNPROCSoft = default (ulong);
        public ulong LimitNPROCSoft
        {
            get
            {
                return _LimitNPROCSoft;
            }

            set
            {
                _LimitNPROCSoft = (value);
            }
        }

        private ulong _LimitMEMLOCK = default (ulong);
        public ulong LimitMEMLOCK
        {
            get
            {
                return _LimitMEMLOCK;
            }

            set
            {
                _LimitMEMLOCK = (value);
            }
        }

        private ulong _LimitMEMLOCKSoft = default (ulong);
        public ulong LimitMEMLOCKSoft
        {
            get
            {
                return _LimitMEMLOCKSoft;
            }

            set
            {
                _LimitMEMLOCKSoft = (value);
            }
        }

        private ulong _LimitLOCKS = default (ulong);
        public ulong LimitLOCKS
        {
            get
            {
                return _LimitLOCKS;
            }

            set
            {
                _LimitLOCKS = (value);
            }
        }

        private ulong _LimitLOCKSSoft = default (ulong);
        public ulong LimitLOCKSSoft
        {
            get
            {
                return _LimitLOCKSSoft;
            }

            set
            {
                _LimitLOCKSSoft = (value);
            }
        }

        private ulong _LimitSIGPENDING = default (ulong);
        public ulong LimitSIGPENDING
        {
            get
            {
                return _LimitSIGPENDING;
            }

            set
            {
                _LimitSIGPENDING = (value);
            }
        }

        private ulong _LimitSIGPENDINGSoft = default (ulong);
        public ulong LimitSIGPENDINGSoft
        {
            get
            {
                return _LimitSIGPENDINGSoft;
            }

            set
            {
                _LimitSIGPENDINGSoft = (value);
            }
        }

        private ulong _LimitMSGQUEUE = default (ulong);
        public ulong LimitMSGQUEUE
        {
            get
            {
                return _LimitMSGQUEUE;
            }

            set
            {
                _LimitMSGQUEUE = (value);
            }
        }

        private ulong _LimitMSGQUEUESoft = default (ulong);
        public ulong LimitMSGQUEUESoft
        {
            get
            {
                return _LimitMSGQUEUESoft;
            }

            set
            {
                _LimitMSGQUEUESoft = (value);
            }
        }

        private ulong _LimitNICE = default (ulong);
        public ulong LimitNICE
        {
            get
            {
                return _LimitNICE;
            }

            set
            {
                _LimitNICE = (value);
            }
        }

        private ulong _LimitNICESoft = default (ulong);
        public ulong LimitNICESoft
        {
            get
            {
                return _LimitNICESoft;
            }

            set
            {
                _LimitNICESoft = (value);
            }
        }

        private ulong _LimitRTPRIO = default (ulong);
        public ulong LimitRTPRIO
        {
            get
            {
                return _LimitRTPRIO;
            }

            set
            {
                _LimitRTPRIO = (value);
            }
        }

        private ulong _LimitRTPRIOSoft = default (ulong);
        public ulong LimitRTPRIOSoft
        {
            get
            {
                return _LimitRTPRIOSoft;
            }

            set
            {
                _LimitRTPRIOSoft = (value);
            }
        }

        private ulong _LimitRTTIME = default (ulong);
        public ulong LimitRTTIME
        {
            get
            {
                return _LimitRTTIME;
            }

            set
            {
                _LimitRTTIME = (value);
            }
        }

        private ulong _LimitRTTIMESoft = default (ulong);
        public ulong LimitRTTIMESoft
        {
            get
            {
                return _LimitRTTIMESoft;
            }

            set
            {
                _LimitRTTIMESoft = (value);
            }
        }

        private string _WorkingDirectory = default (string);
        public string WorkingDirectory
        {
            get
            {
                return _WorkingDirectory;
            }

            set
            {
                _WorkingDirectory = (value);
            }
        }

        private string _RootDirectory = default (string);
        public string RootDirectory
        {
            get
            {
                return _RootDirectory;
            }

            set
            {
                _RootDirectory = (value);
            }
        }

        private int _OOMScoreAdjust = default (int);
        public int OOMScoreAdjust
        {
            get
            {
                return _OOMScoreAdjust;
            }

            set
            {
                _OOMScoreAdjust = (value);
            }
        }

        private int _Nice = default (int);
        public int Nice
        {
            get
            {
                return _Nice;
            }

            set
            {
                _Nice = (value);
            }
        }

        private int _IOScheduling = default (int);
        public int IOScheduling
        {
            get
            {
                return _IOScheduling;
            }

            set
            {
                _IOScheduling = (value);
            }
        }

        private int _CPUSchedulingPolicy = default (int);
        public int CPUSchedulingPolicy
        {
            get
            {
                return _CPUSchedulingPolicy;
            }

            set
            {
                _CPUSchedulingPolicy = (value);
            }
        }

        private int _CPUSchedulingPriority = default (int);
        public int CPUSchedulingPriority
        {
            get
            {
                return _CPUSchedulingPriority;
            }

            set
            {
                _CPUSchedulingPriority = (value);
            }
        }

        private byte[] _CPUAffinity = default (byte[]);
        public byte[] CPUAffinity
        {
            get
            {
                return _CPUAffinity;
            }

            set
            {
                _CPUAffinity = (value);
            }
        }

        private ulong _TimerSlackNSec = default (ulong);
        public ulong TimerSlackNSec
        {
            get
            {
                return _TimerSlackNSec;
            }

            set
            {
                _TimerSlackNSec = (value);
            }
        }

        private bool _CPUSchedulingResetOnFork = default (bool);
        public bool CPUSchedulingResetOnFork
        {
            get
            {
                return _CPUSchedulingResetOnFork;
            }

            set
            {
                _CPUSchedulingResetOnFork = (value);
            }
        }

        private bool _NonBlocking = default (bool);
        public bool NonBlocking
        {
            get
            {
                return _NonBlocking;
            }

            set
            {
                _NonBlocking = (value);
            }
        }

        private string _StandardInput = default (string);
        public string StandardInput
        {
            get
            {
                return _StandardInput;
            }

            set
            {
                _StandardInput = (value);
            }
        }

        private string _StandardOutput = default (string);
        public string StandardOutput
        {
            get
            {
                return _StandardOutput;
            }

            set
            {
                _StandardOutput = (value);
            }
        }

        private string _StandardError = default (string);
        public string StandardError
        {
            get
            {
                return _StandardError;
            }

            set
            {
                _StandardError = (value);
            }
        }

        private string _TTYPath = default (string);
        public string TTYPath
        {
            get
            {
                return _TTYPath;
            }

            set
            {
                _TTYPath = (value);
            }
        }

        private bool _TTYReset = default (bool);
        public bool TTYReset
        {
            get
            {
                return _TTYReset;
            }

            set
            {
                _TTYReset = (value);
            }
        }

        private bool _TTYVHangup = default (bool);
        public bool TTYVHangup
        {
            get
            {
                return _TTYVHangup;
            }

            set
            {
                _TTYVHangup = (value);
            }
        }

        private bool _TTYVTDisallocate = default (bool);
        public bool TTYVTDisallocate
        {
            get
            {
                return _TTYVTDisallocate;
            }

            set
            {
                _TTYVTDisallocate = (value);
            }
        }

        private int _SyslogPriority = default (int);
        public int SyslogPriority
        {
            get
            {
                return _SyslogPriority;
            }

            set
            {
                _SyslogPriority = (value);
            }
        }

        private string _SyslogIdentifier = default (string);
        public string SyslogIdentifier
        {
            get
            {
                return _SyslogIdentifier;
            }

            set
            {
                _SyslogIdentifier = (value);
            }
        }

        private bool _SyslogLevelPrefix = default (bool);
        public bool SyslogLevelPrefix
        {
            get
            {
                return _SyslogLevelPrefix;
            }

            set
            {
                _SyslogLevelPrefix = (value);
            }
        }

        private int _SyslogLevel = default (int);
        public int SyslogLevel
        {
            get
            {
                return _SyslogLevel;
            }

            set
            {
                _SyslogLevel = (value);
            }
        }

        private int _SyslogFacility = default (int);
        public int SyslogFacility
        {
            get
            {
                return _SyslogFacility;
            }

            set
            {
                _SyslogFacility = (value);
            }
        }

        private int _SecureBits = default (int);
        public int SecureBits
        {
            get
            {
                return _SecureBits;
            }

            set
            {
                _SecureBits = (value);
            }
        }

        private ulong _CapabilityBoundingSet = default (ulong);
        public ulong CapabilityBoundingSet
        {
            get
            {
                return _CapabilityBoundingSet;
            }

            set
            {
                _CapabilityBoundingSet = (value);
            }
        }

        private ulong _AmbientCapabilities = default (ulong);
        public ulong AmbientCapabilities
        {
            get
            {
                return _AmbientCapabilities;
            }

            set
            {
                _AmbientCapabilities = (value);
            }
        }

        private string _User = default (string);
        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = (value);
            }
        }

        private string _Group = default (string);
        public string Group
        {
            get
            {
                return _Group;
            }

            set
            {
                _Group = (value);
            }
        }

        private string[] _SupplementaryGroups = default (string[]);
        public string[] SupplementaryGroups
        {
            get
            {
                return _SupplementaryGroups;
            }

            set
            {
                _SupplementaryGroups = (value);
            }
        }

        private string _PAMName = default (string);
        public string PAMName
        {
            get
            {
                return _PAMName;
            }

            set
            {
                _PAMName = (value);
            }
        }

        private string[] _ReadWritePaths = default (string[]);
        public string[] ReadWritePaths
        {
            get
            {
                return _ReadWritePaths;
            }

            set
            {
                _ReadWritePaths = (value);
            }
        }

        private string[] _ReadOnlyPaths = default (string[]);
        public string[] ReadOnlyPaths
        {
            get
            {
                return _ReadOnlyPaths;
            }

            set
            {
                _ReadOnlyPaths = (value);
            }
        }

        private string[] _InaccessiblePaths = default (string[]);
        public string[] InaccessiblePaths
        {
            get
            {
                return _InaccessiblePaths;
            }

            set
            {
                _InaccessiblePaths = (value);
            }
        }

        private ulong _MountFlags = default (ulong);
        public ulong MountFlags
        {
            get
            {
                return _MountFlags;
            }

            set
            {
                _MountFlags = (value);
            }
        }

        private bool _PrivateTmp = default (bool);
        public bool PrivateTmp
        {
            get
            {
                return _PrivateTmp;
            }

            set
            {
                _PrivateTmp = (value);
            }
        }

        private bool _PrivateNetwork = default (bool);
        public bool PrivateNetwork
        {
            get
            {
                return _PrivateNetwork;
            }

            set
            {
                _PrivateNetwork = (value);
            }
        }

        private bool _PrivateDevices = default (bool);
        public bool PrivateDevices
        {
            get
            {
                return _PrivateDevices;
            }

            set
            {
                _PrivateDevices = (value);
            }
        }

        private string _ProtectHome = default (string);
        public string ProtectHome
        {
            get
            {
                return _ProtectHome;
            }

            set
            {
                _ProtectHome = (value);
            }
        }

        private string _ProtectSystem = default (string);
        public string ProtectSystem
        {
            get
            {
                return _ProtectSystem;
            }

            set
            {
                _ProtectSystem = (value);
            }
        }

        private bool _SameProcessGroup = default (bool);
        public bool SameProcessGroup
        {
            get
            {
                return _SameProcessGroup;
            }

            set
            {
                _SameProcessGroup = (value);
            }
        }

        private string _UtmpIdentifier = default (string);
        public string UtmpIdentifier
        {
            get
            {
                return _UtmpIdentifier;
            }

            set
            {
                _UtmpIdentifier = (value);
            }
        }

        private string _UtmpMode = default (string);
        public string UtmpMode
        {
            get
            {
                return _UtmpMode;
            }

            set
            {
                _UtmpMode = (value);
            }
        }

        private (bool, string)_SELinuxContext = default ((bool, string));
        public (bool, string)SELinuxContext
        {
            get
            {
                return _SELinuxContext;
            }

            set
            {
                _SELinuxContext = (value);
            }
        }

        private (bool, string)_AppArmorProfile = default ((bool, string));
        public (bool, string)AppArmorProfile
        {
            get
            {
                return _AppArmorProfile;
            }

            set
            {
                _AppArmorProfile = (value);
            }
        }

        private (bool, string)_SmackProcessLabel = default ((bool, string));
        public (bool, string)SmackProcessLabel
        {
            get
            {
                return _SmackProcessLabel;
            }

            set
            {
                _SmackProcessLabel = (value);
            }
        }

        private bool _IgnoreSIGPIPE = default (bool);
        public bool IgnoreSIGPIPE
        {
            get
            {
                return _IgnoreSIGPIPE;
            }

            set
            {
                _IgnoreSIGPIPE = (value);
            }
        }

        private bool _NoNewPrivileges = default (bool);
        public bool NoNewPrivileges
        {
            get
            {
                return _NoNewPrivileges;
            }

            set
            {
                _NoNewPrivileges = (value);
            }
        }

        private (bool, string[])_SystemCallFilter = default ((bool, string[]));
        public (bool, string[])SystemCallFilter
        {
            get
            {
                return _SystemCallFilter;
            }

            set
            {
                _SystemCallFilter = (value);
            }
        }

        private string[] _SystemCallArchitectures = default (string[]);
        public string[] SystemCallArchitectures
        {
            get
            {
                return _SystemCallArchitectures;
            }

            set
            {
                _SystemCallArchitectures = (value);
            }
        }

        private int _SystemCallErrorNumber = default (int);
        public int SystemCallErrorNumber
        {
            get
            {
                return _SystemCallErrorNumber;
            }

            set
            {
                _SystemCallErrorNumber = (value);
            }
        }

        private string _Personality = default (string);
        public string Personality
        {
            get
            {
                return _Personality;
            }

            set
            {
                _Personality = (value);
            }
        }

        private (bool, string[])_RestrictAddressFamilies = default ((bool, string[]));
        public (bool, string[])RestrictAddressFamilies
        {
            get
            {
                return _RestrictAddressFamilies;
            }

            set
            {
                _RestrictAddressFamilies = (value);
            }
        }

        private uint _RuntimeDirectoryMode = default (uint);
        public uint RuntimeDirectoryMode
        {
            get
            {
                return _RuntimeDirectoryMode;
            }

            set
            {
                _RuntimeDirectoryMode = (value);
            }
        }

        private string[] _RuntimeDirectory = default (string[]);
        public string[] RuntimeDirectory
        {
            get
            {
                return _RuntimeDirectory;
            }

            set
            {
                _RuntimeDirectory = (value);
            }
        }

        private bool _MemoryDenyWriteExecute = default (bool);
        public bool MemoryDenyWriteExecute
        {
            get
            {
                return _MemoryDenyWriteExecute;
            }

            set
            {
                _MemoryDenyWriteExecute = (value);
            }
        }

        private bool _RestrictRealtime = default (bool);
        public bool RestrictRealtime
        {
            get
            {
                return _RestrictRealtime;
            }

            set
            {
                _RestrictRealtime = (value);
            }
        }

        private string _KillMode = default (string);
        public string KillMode
        {
            get
            {
                return _KillMode;
            }

            set
            {
                _KillMode = (value);
            }
        }

        private int _KillSignal = default (int);
        public int KillSignal
        {
            get
            {
                return _KillSignal;
            }

            set
            {
                _KillSignal = (value);
            }
        }

        private bool _SendSIGKILL = default (bool);
        public bool SendSIGKILL
        {
            get
            {
                return _SendSIGKILL;
            }

            set
            {
                _SendSIGKILL = (value);
            }
        }

        private bool _SendSIGHUP = default (bool);
        public bool SendSIGHUP
        {
            get
            {
                return _SendSIGHUP;
            }

            set
            {
                _SendSIGHUP = (value);
            }
        }
    }

    static class SwapExtensions
    {
        public static Task<string> GetWhatAsync(this ISwap o) => o.GetAsync<string>("What");
        public static Task<int> GetPriorityAsync(this ISwap o) => o.GetAsync<int>("Priority");
        public static Task<string> GetOptionsAsync(this ISwap o) => o.GetAsync<string>("Options");
        public static Task<ulong> GetTimeoutUSecAsync(this ISwap o) => o.GetAsync<ulong>("TimeoutUSec");
        public static Task<uint> GetControlPIDAsync(this ISwap o) => o.GetAsync<uint>("ControlPID");
        public static Task<string> GetResultAsync(this ISwap o) => o.GetAsync<string>("Result");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecActivateAsync(this ISwap o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecActivate");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecDeactivateAsync(this ISwap o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecDeactivate");
        public static Task<string> GetSliceAsync(this ISwap o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this ISwap o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this ISwap o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this ISwap o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this ISwap o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this ISwap o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this ISwap o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this ISwap o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this ISwap o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this ISwap o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this ISwap o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this ISwap o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this ISwap o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this ISwap o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this ISwap o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this ISwap o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this ISwap o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this ISwap o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this ISwap o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this ISwap o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this ISwap o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this ISwap o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this ISwap o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this ISwap o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this ISwap o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this ISwap o) => o.GetAsync<ulong>("TasksMax");
        public static Task<string[]> GetEnvironmentAsync(this ISwap o) => o.GetAsync<string[]>("Environment");
        public static Task<(string, bool)[]> GetEnvironmentFilesAsync(this ISwap o) => o.GetAsync<(string, bool)[]>("EnvironmentFiles");
        public static Task<string[]> GetPassEnvironmentAsync(this ISwap o) => o.GetAsync<string[]>("PassEnvironment");
        public static Task<uint> GetUMaskAsync(this ISwap o) => o.GetAsync<uint>("UMask");
        public static Task<ulong> GetLimitCPUAsync(this ISwap o) => o.GetAsync<ulong>("LimitCPU");
        public static Task<ulong> GetLimitCPUSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitCPUSoft");
        public static Task<ulong> GetLimitFSIZEAsync(this ISwap o) => o.GetAsync<ulong>("LimitFSIZE");
        public static Task<ulong> GetLimitFSIZESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitFSIZESoft");
        public static Task<ulong> GetLimitDATAAsync(this ISwap o) => o.GetAsync<ulong>("LimitDATA");
        public static Task<ulong> GetLimitDATASoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitDATASoft");
        public static Task<ulong> GetLimitSTACKAsync(this ISwap o) => o.GetAsync<ulong>("LimitSTACK");
        public static Task<ulong> GetLimitSTACKSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitSTACKSoft");
        public static Task<ulong> GetLimitCOREAsync(this ISwap o) => o.GetAsync<ulong>("LimitCORE");
        public static Task<ulong> GetLimitCORESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitCORESoft");
        public static Task<ulong> GetLimitRSSAsync(this ISwap o) => o.GetAsync<ulong>("LimitRSS");
        public static Task<ulong> GetLimitRSSSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitRSSSoft");
        public static Task<ulong> GetLimitNOFILEAsync(this ISwap o) => o.GetAsync<ulong>("LimitNOFILE");
        public static Task<ulong> GetLimitNOFILESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitNOFILESoft");
        public static Task<ulong> GetLimitASAsync(this ISwap o) => o.GetAsync<ulong>("LimitAS");
        public static Task<ulong> GetLimitASSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitASSoft");
        public static Task<ulong> GetLimitNPROCAsync(this ISwap o) => o.GetAsync<ulong>("LimitNPROC");
        public static Task<ulong> GetLimitNPROCSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitNPROCSoft");
        public static Task<ulong> GetLimitMEMLOCKAsync(this ISwap o) => o.GetAsync<ulong>("LimitMEMLOCK");
        public static Task<ulong> GetLimitMEMLOCKSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitMEMLOCKSoft");
        public static Task<ulong> GetLimitLOCKSAsync(this ISwap o) => o.GetAsync<ulong>("LimitLOCKS");
        public static Task<ulong> GetLimitLOCKSSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitLOCKSSoft");
        public static Task<ulong> GetLimitSIGPENDINGAsync(this ISwap o) => o.GetAsync<ulong>("LimitSIGPENDING");
        public static Task<ulong> GetLimitSIGPENDINGSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitSIGPENDINGSoft");
        public static Task<ulong> GetLimitMSGQUEUEAsync(this ISwap o) => o.GetAsync<ulong>("LimitMSGQUEUE");
        public static Task<ulong> GetLimitMSGQUEUESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitMSGQUEUESoft");
        public static Task<ulong> GetLimitNICEAsync(this ISwap o) => o.GetAsync<ulong>("LimitNICE");
        public static Task<ulong> GetLimitNICESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitNICESoft");
        public static Task<ulong> GetLimitRTPRIOAsync(this ISwap o) => o.GetAsync<ulong>("LimitRTPRIO");
        public static Task<ulong> GetLimitRTPRIOSoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitRTPRIOSoft");
        public static Task<ulong> GetLimitRTTIMEAsync(this ISwap o) => o.GetAsync<ulong>("LimitRTTIME");
        public static Task<ulong> GetLimitRTTIMESoftAsync(this ISwap o) => o.GetAsync<ulong>("LimitRTTIMESoft");
        public static Task<string> GetWorkingDirectoryAsync(this ISwap o) => o.GetAsync<string>("WorkingDirectory");
        public static Task<string> GetRootDirectoryAsync(this ISwap o) => o.GetAsync<string>("RootDirectory");
        public static Task<int> GetOOMScoreAdjustAsync(this ISwap o) => o.GetAsync<int>("OOMScoreAdjust");
        public static Task<int> GetNiceAsync(this ISwap o) => o.GetAsync<int>("Nice");
        public static Task<int> GetIOSchedulingAsync(this ISwap o) => o.GetAsync<int>("IOScheduling");
        public static Task<int> GetCPUSchedulingPolicyAsync(this ISwap o) => o.GetAsync<int>("CPUSchedulingPolicy");
        public static Task<int> GetCPUSchedulingPriorityAsync(this ISwap o) => o.GetAsync<int>("CPUSchedulingPriority");
        public static Task<byte[]> GetCPUAffinityAsync(this ISwap o) => o.GetAsync<byte[]>("CPUAffinity");
        public static Task<ulong> GetTimerSlackNSecAsync(this ISwap o) => o.GetAsync<ulong>("TimerSlackNSec");
        public static Task<bool> GetCPUSchedulingResetOnForkAsync(this ISwap o) => o.GetAsync<bool>("CPUSchedulingResetOnFork");
        public static Task<bool> GetNonBlockingAsync(this ISwap o) => o.GetAsync<bool>("NonBlocking");
        public static Task<string> GetStandardInputAsync(this ISwap o) => o.GetAsync<string>("StandardInput");
        public static Task<string> GetStandardOutputAsync(this ISwap o) => o.GetAsync<string>("StandardOutput");
        public static Task<string> GetStandardErrorAsync(this ISwap o) => o.GetAsync<string>("StandardError");
        public static Task<string> GetTTYPathAsync(this ISwap o) => o.GetAsync<string>("TTYPath");
        public static Task<bool> GetTTYResetAsync(this ISwap o) => o.GetAsync<bool>("TTYReset");
        public static Task<bool> GetTTYVHangupAsync(this ISwap o) => o.GetAsync<bool>("TTYVHangup");
        public static Task<bool> GetTTYVTDisallocateAsync(this ISwap o) => o.GetAsync<bool>("TTYVTDisallocate");
        public static Task<int> GetSyslogPriorityAsync(this ISwap o) => o.GetAsync<int>("SyslogPriority");
        public static Task<string> GetSyslogIdentifierAsync(this ISwap o) => o.GetAsync<string>("SyslogIdentifier");
        public static Task<bool> GetSyslogLevelPrefixAsync(this ISwap o) => o.GetAsync<bool>("SyslogLevelPrefix");
        public static Task<int> GetSyslogLevelAsync(this ISwap o) => o.GetAsync<int>("SyslogLevel");
        public static Task<int> GetSyslogFacilityAsync(this ISwap o) => o.GetAsync<int>("SyslogFacility");
        public static Task<int> GetSecureBitsAsync(this ISwap o) => o.GetAsync<int>("SecureBits");
        public static Task<ulong> GetCapabilityBoundingSetAsync(this ISwap o) => o.GetAsync<ulong>("CapabilityBoundingSet");
        public static Task<ulong> GetAmbientCapabilitiesAsync(this ISwap o) => o.GetAsync<ulong>("AmbientCapabilities");
        public static Task<string> GetUserAsync(this ISwap o) => o.GetAsync<string>("User");
        public static Task<string> GetGroupAsync(this ISwap o) => o.GetAsync<string>("Group");
        public static Task<string[]> GetSupplementaryGroupsAsync(this ISwap o) => o.GetAsync<string[]>("SupplementaryGroups");
        public static Task<string> GetPAMNameAsync(this ISwap o) => o.GetAsync<string>("PAMName");
        public static Task<string[]> GetReadWritePathsAsync(this ISwap o) => o.GetAsync<string[]>("ReadWritePaths");
        public static Task<string[]> GetReadOnlyPathsAsync(this ISwap o) => o.GetAsync<string[]>("ReadOnlyPaths");
        public static Task<string[]> GetInaccessiblePathsAsync(this ISwap o) => o.GetAsync<string[]>("InaccessiblePaths");
        public static Task<ulong> GetMountFlagsAsync(this ISwap o) => o.GetAsync<ulong>("MountFlags");
        public static Task<bool> GetPrivateTmpAsync(this ISwap o) => o.GetAsync<bool>("PrivateTmp");
        public static Task<bool> GetPrivateNetworkAsync(this ISwap o) => o.GetAsync<bool>("PrivateNetwork");
        public static Task<bool> GetPrivateDevicesAsync(this ISwap o) => o.GetAsync<bool>("PrivateDevices");
        public static Task<string> GetProtectHomeAsync(this ISwap o) => o.GetAsync<string>("ProtectHome");
        public static Task<string> GetProtectSystemAsync(this ISwap o) => o.GetAsync<string>("ProtectSystem");
        public static Task<bool> GetSameProcessGroupAsync(this ISwap o) => o.GetAsync<bool>("SameProcessGroup");
        public static Task<string> GetUtmpIdentifierAsync(this ISwap o) => o.GetAsync<string>("UtmpIdentifier");
        public static Task<string> GetUtmpModeAsync(this ISwap o) => o.GetAsync<string>("UtmpMode");
        public static Task<(bool, string)> GetSELinuxContextAsync(this ISwap o) => o.GetAsync<(bool, string)>("SELinuxContext");
        public static Task<(bool, string)> GetAppArmorProfileAsync(this ISwap o) => o.GetAsync<(bool, string)>("AppArmorProfile");
        public static Task<(bool, string)> GetSmackProcessLabelAsync(this ISwap o) => o.GetAsync<(bool, string)>("SmackProcessLabel");
        public static Task<bool> GetIgnoreSIGPIPEAsync(this ISwap o) => o.GetAsync<bool>("IgnoreSIGPIPE");
        public static Task<bool> GetNoNewPrivilegesAsync(this ISwap o) => o.GetAsync<bool>("NoNewPrivileges");
        public static Task<(bool, string[])> GetSystemCallFilterAsync(this ISwap o) => o.GetAsync<(bool, string[])>("SystemCallFilter");
        public static Task<string[]> GetSystemCallArchitecturesAsync(this ISwap o) => o.GetAsync<string[]>("SystemCallArchitectures");
        public static Task<int> GetSystemCallErrorNumberAsync(this ISwap o) => o.GetAsync<int>("SystemCallErrorNumber");
        public static Task<string> GetPersonalityAsync(this ISwap o) => o.GetAsync<string>("Personality");
        public static Task<(bool, string[])> GetRestrictAddressFamiliesAsync(this ISwap o) => o.GetAsync<(bool, string[])>("RestrictAddressFamilies");
        public static Task<uint> GetRuntimeDirectoryModeAsync(this ISwap o) => o.GetAsync<uint>("RuntimeDirectoryMode");
        public static Task<string[]> GetRuntimeDirectoryAsync(this ISwap o) => o.GetAsync<string[]>("RuntimeDirectory");
        public static Task<bool> GetMemoryDenyWriteExecuteAsync(this ISwap o) => o.GetAsync<bool>("MemoryDenyWriteExecute");
        public static Task<bool> GetRestrictRealtimeAsync(this ISwap o) => o.GetAsync<bool>("RestrictRealtime");
        public static Task<string> GetKillModeAsync(this ISwap o) => o.GetAsync<string>("KillMode");
        public static Task<int> GetKillSignalAsync(this ISwap o) => o.GetAsync<int>("KillSignal");
        public static Task<bool> GetSendSIGKILLAsync(this ISwap o) => o.GetAsync<bool>("SendSIGKILL");
        public static Task<bool> GetSendSIGHUPAsync(this ISwap o) => o.GetAsync<bool>("SendSIGHUP");
    }

    [DBusInterface("org.freedesktop.systemd1.Target")]
    interface ITarget : IDBusObject
    {
    }

    [DBusInterface("org.freedesktop.systemd1.Mount")]
    interface IMount : IDBusObject
    {
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<MountProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class MountProperties
    {
        private string _Where = default (string);
        public string Where
        {
            get
            {
                return _Where;
            }

            set
            {
                _Where = (value);
            }
        }

        private string _What = default (string);
        public string What
        {
            get
            {
                return _What;
            }

            set
            {
                _What = (value);
            }
        }

        private string _Options = default (string);
        public string Options
        {
            get
            {
                return _Options;
            }

            set
            {
                _Options = (value);
            }
        }

        private string _Type = default (string);
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = (value);
            }
        }

        private ulong _TimeoutUSec = default (ulong);
        public ulong TimeoutUSec
        {
            get
            {
                return _TimeoutUSec;
            }

            set
            {
                _TimeoutUSec = (value);
            }
        }

        private uint _ControlPID = default (uint);
        public uint ControlPID
        {
            get
            {
                return _ControlPID;
            }

            set
            {
                _ControlPID = (value);
            }
        }

        private uint _DirectoryMode = default (uint);
        public uint DirectoryMode
        {
            get
            {
                return _DirectoryMode;
            }

            set
            {
                _DirectoryMode = (value);
            }
        }

        private bool _SloppyOptions = default (bool);
        public bool SloppyOptions
        {
            get
            {
                return _SloppyOptions;
            }

            set
            {
                _SloppyOptions = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecMount = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecMount
        {
            get
            {
                return _ExecMount;
            }

            set
            {
                _ExecMount = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecUnmount = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecUnmount
        {
            get
            {
                return _ExecUnmount;
            }

            set
            {
                _ExecUnmount = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecRemount = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecRemount
        {
            get
            {
                return _ExecRemount;
            }

            set
            {
                _ExecRemount = (value);
            }
        }

        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }

        private string[] _Environment = default (string[]);
        public string[] Environment
        {
            get
            {
                return _Environment;
            }

            set
            {
                _Environment = (value);
            }
        }

        private (string, bool)[] _EnvironmentFiles = default ((string, bool)[]);
        public (string, bool)[] EnvironmentFiles
        {
            get
            {
                return _EnvironmentFiles;
            }

            set
            {
                _EnvironmentFiles = (value);
            }
        }

        private string[] _PassEnvironment = default (string[]);
        public string[] PassEnvironment
        {
            get
            {
                return _PassEnvironment;
            }

            set
            {
                _PassEnvironment = (value);
            }
        }

        private uint _UMask = default (uint);
        public uint UMask
        {
            get
            {
                return _UMask;
            }

            set
            {
                _UMask = (value);
            }
        }

        private ulong _LimitCPU = default (ulong);
        public ulong LimitCPU
        {
            get
            {
                return _LimitCPU;
            }

            set
            {
                _LimitCPU = (value);
            }
        }

        private ulong _LimitCPUSoft = default (ulong);
        public ulong LimitCPUSoft
        {
            get
            {
                return _LimitCPUSoft;
            }

            set
            {
                _LimitCPUSoft = (value);
            }
        }

        private ulong _LimitFSIZE = default (ulong);
        public ulong LimitFSIZE
        {
            get
            {
                return _LimitFSIZE;
            }

            set
            {
                _LimitFSIZE = (value);
            }
        }

        private ulong _LimitFSIZESoft = default (ulong);
        public ulong LimitFSIZESoft
        {
            get
            {
                return _LimitFSIZESoft;
            }

            set
            {
                _LimitFSIZESoft = (value);
            }
        }

        private ulong _LimitDATA = default (ulong);
        public ulong LimitDATA
        {
            get
            {
                return _LimitDATA;
            }

            set
            {
                _LimitDATA = (value);
            }
        }

        private ulong _LimitDATASoft = default (ulong);
        public ulong LimitDATASoft
        {
            get
            {
                return _LimitDATASoft;
            }

            set
            {
                _LimitDATASoft = (value);
            }
        }

        private ulong _LimitSTACK = default (ulong);
        public ulong LimitSTACK
        {
            get
            {
                return _LimitSTACK;
            }

            set
            {
                _LimitSTACK = (value);
            }
        }

        private ulong _LimitSTACKSoft = default (ulong);
        public ulong LimitSTACKSoft
        {
            get
            {
                return _LimitSTACKSoft;
            }

            set
            {
                _LimitSTACKSoft = (value);
            }
        }

        private ulong _LimitCORE = default (ulong);
        public ulong LimitCORE
        {
            get
            {
                return _LimitCORE;
            }

            set
            {
                _LimitCORE = (value);
            }
        }

        private ulong _LimitCORESoft = default (ulong);
        public ulong LimitCORESoft
        {
            get
            {
                return _LimitCORESoft;
            }

            set
            {
                _LimitCORESoft = (value);
            }
        }

        private ulong _LimitRSS = default (ulong);
        public ulong LimitRSS
        {
            get
            {
                return _LimitRSS;
            }

            set
            {
                _LimitRSS = (value);
            }
        }

        private ulong _LimitRSSSoft = default (ulong);
        public ulong LimitRSSSoft
        {
            get
            {
                return _LimitRSSSoft;
            }

            set
            {
                _LimitRSSSoft = (value);
            }
        }

        private ulong _LimitNOFILE = default (ulong);
        public ulong LimitNOFILE
        {
            get
            {
                return _LimitNOFILE;
            }

            set
            {
                _LimitNOFILE = (value);
            }
        }

        private ulong _LimitNOFILESoft = default (ulong);
        public ulong LimitNOFILESoft
        {
            get
            {
                return _LimitNOFILESoft;
            }

            set
            {
                _LimitNOFILESoft = (value);
            }
        }

        private ulong _LimitAS = default (ulong);
        public ulong LimitAS
        {
            get
            {
                return _LimitAS;
            }

            set
            {
                _LimitAS = (value);
            }
        }

        private ulong _LimitASSoft = default (ulong);
        public ulong LimitASSoft
        {
            get
            {
                return _LimitASSoft;
            }

            set
            {
                _LimitASSoft = (value);
            }
        }

        private ulong _LimitNPROC = default (ulong);
        public ulong LimitNPROC
        {
            get
            {
                return _LimitNPROC;
            }

            set
            {
                _LimitNPROC = (value);
            }
        }

        private ulong _LimitNPROCSoft = default (ulong);
        public ulong LimitNPROCSoft
        {
            get
            {
                return _LimitNPROCSoft;
            }

            set
            {
                _LimitNPROCSoft = (value);
            }
        }

        private ulong _LimitMEMLOCK = default (ulong);
        public ulong LimitMEMLOCK
        {
            get
            {
                return _LimitMEMLOCK;
            }

            set
            {
                _LimitMEMLOCK = (value);
            }
        }

        private ulong _LimitMEMLOCKSoft = default (ulong);
        public ulong LimitMEMLOCKSoft
        {
            get
            {
                return _LimitMEMLOCKSoft;
            }

            set
            {
                _LimitMEMLOCKSoft = (value);
            }
        }

        private ulong _LimitLOCKS = default (ulong);
        public ulong LimitLOCKS
        {
            get
            {
                return _LimitLOCKS;
            }

            set
            {
                _LimitLOCKS = (value);
            }
        }

        private ulong _LimitLOCKSSoft = default (ulong);
        public ulong LimitLOCKSSoft
        {
            get
            {
                return _LimitLOCKSSoft;
            }

            set
            {
                _LimitLOCKSSoft = (value);
            }
        }

        private ulong _LimitSIGPENDING = default (ulong);
        public ulong LimitSIGPENDING
        {
            get
            {
                return _LimitSIGPENDING;
            }

            set
            {
                _LimitSIGPENDING = (value);
            }
        }

        private ulong _LimitSIGPENDINGSoft = default (ulong);
        public ulong LimitSIGPENDINGSoft
        {
            get
            {
                return _LimitSIGPENDINGSoft;
            }

            set
            {
                _LimitSIGPENDINGSoft = (value);
            }
        }

        private ulong _LimitMSGQUEUE = default (ulong);
        public ulong LimitMSGQUEUE
        {
            get
            {
                return _LimitMSGQUEUE;
            }

            set
            {
                _LimitMSGQUEUE = (value);
            }
        }

        private ulong _LimitMSGQUEUESoft = default (ulong);
        public ulong LimitMSGQUEUESoft
        {
            get
            {
                return _LimitMSGQUEUESoft;
            }

            set
            {
                _LimitMSGQUEUESoft = (value);
            }
        }

        private ulong _LimitNICE = default (ulong);
        public ulong LimitNICE
        {
            get
            {
                return _LimitNICE;
            }

            set
            {
                _LimitNICE = (value);
            }
        }

        private ulong _LimitNICESoft = default (ulong);
        public ulong LimitNICESoft
        {
            get
            {
                return _LimitNICESoft;
            }

            set
            {
                _LimitNICESoft = (value);
            }
        }

        private ulong _LimitRTPRIO = default (ulong);
        public ulong LimitRTPRIO
        {
            get
            {
                return _LimitRTPRIO;
            }

            set
            {
                _LimitRTPRIO = (value);
            }
        }

        private ulong _LimitRTPRIOSoft = default (ulong);
        public ulong LimitRTPRIOSoft
        {
            get
            {
                return _LimitRTPRIOSoft;
            }

            set
            {
                _LimitRTPRIOSoft = (value);
            }
        }

        private ulong _LimitRTTIME = default (ulong);
        public ulong LimitRTTIME
        {
            get
            {
                return _LimitRTTIME;
            }

            set
            {
                _LimitRTTIME = (value);
            }
        }

        private ulong _LimitRTTIMESoft = default (ulong);
        public ulong LimitRTTIMESoft
        {
            get
            {
                return _LimitRTTIMESoft;
            }

            set
            {
                _LimitRTTIMESoft = (value);
            }
        }

        private string _WorkingDirectory = default (string);
        public string WorkingDirectory
        {
            get
            {
                return _WorkingDirectory;
            }

            set
            {
                _WorkingDirectory = (value);
            }
        }

        private string _RootDirectory = default (string);
        public string RootDirectory
        {
            get
            {
                return _RootDirectory;
            }

            set
            {
                _RootDirectory = (value);
            }
        }

        private int _OOMScoreAdjust = default (int);
        public int OOMScoreAdjust
        {
            get
            {
                return _OOMScoreAdjust;
            }

            set
            {
                _OOMScoreAdjust = (value);
            }
        }

        private int _Nice = default (int);
        public int Nice
        {
            get
            {
                return _Nice;
            }

            set
            {
                _Nice = (value);
            }
        }

        private int _IOScheduling = default (int);
        public int IOScheduling
        {
            get
            {
                return _IOScheduling;
            }

            set
            {
                _IOScheduling = (value);
            }
        }

        private int _CPUSchedulingPolicy = default (int);
        public int CPUSchedulingPolicy
        {
            get
            {
                return _CPUSchedulingPolicy;
            }

            set
            {
                _CPUSchedulingPolicy = (value);
            }
        }

        private int _CPUSchedulingPriority = default (int);
        public int CPUSchedulingPriority
        {
            get
            {
                return _CPUSchedulingPriority;
            }

            set
            {
                _CPUSchedulingPriority = (value);
            }
        }

        private byte[] _CPUAffinity = default (byte[]);
        public byte[] CPUAffinity
        {
            get
            {
                return _CPUAffinity;
            }

            set
            {
                _CPUAffinity = (value);
            }
        }

        private ulong _TimerSlackNSec = default (ulong);
        public ulong TimerSlackNSec
        {
            get
            {
                return _TimerSlackNSec;
            }

            set
            {
                _TimerSlackNSec = (value);
            }
        }

        private bool _CPUSchedulingResetOnFork = default (bool);
        public bool CPUSchedulingResetOnFork
        {
            get
            {
                return _CPUSchedulingResetOnFork;
            }

            set
            {
                _CPUSchedulingResetOnFork = (value);
            }
        }

        private bool _NonBlocking = default (bool);
        public bool NonBlocking
        {
            get
            {
                return _NonBlocking;
            }

            set
            {
                _NonBlocking = (value);
            }
        }

        private string _StandardInput = default (string);
        public string StandardInput
        {
            get
            {
                return _StandardInput;
            }

            set
            {
                _StandardInput = (value);
            }
        }

        private string _StandardOutput = default (string);
        public string StandardOutput
        {
            get
            {
                return _StandardOutput;
            }

            set
            {
                _StandardOutput = (value);
            }
        }

        private string _StandardError = default (string);
        public string StandardError
        {
            get
            {
                return _StandardError;
            }

            set
            {
                _StandardError = (value);
            }
        }

        private string _TTYPath = default (string);
        public string TTYPath
        {
            get
            {
                return _TTYPath;
            }

            set
            {
                _TTYPath = (value);
            }
        }

        private bool _TTYReset = default (bool);
        public bool TTYReset
        {
            get
            {
                return _TTYReset;
            }

            set
            {
                _TTYReset = (value);
            }
        }

        private bool _TTYVHangup = default (bool);
        public bool TTYVHangup
        {
            get
            {
                return _TTYVHangup;
            }

            set
            {
                _TTYVHangup = (value);
            }
        }

        private bool _TTYVTDisallocate = default (bool);
        public bool TTYVTDisallocate
        {
            get
            {
                return _TTYVTDisallocate;
            }

            set
            {
                _TTYVTDisallocate = (value);
            }
        }

        private int _SyslogPriority = default (int);
        public int SyslogPriority
        {
            get
            {
                return _SyslogPriority;
            }

            set
            {
                _SyslogPriority = (value);
            }
        }

        private string _SyslogIdentifier = default (string);
        public string SyslogIdentifier
        {
            get
            {
                return _SyslogIdentifier;
            }

            set
            {
                _SyslogIdentifier = (value);
            }
        }

        private bool _SyslogLevelPrefix = default (bool);
        public bool SyslogLevelPrefix
        {
            get
            {
                return _SyslogLevelPrefix;
            }

            set
            {
                _SyslogLevelPrefix = (value);
            }
        }

        private int _SyslogLevel = default (int);
        public int SyslogLevel
        {
            get
            {
                return _SyslogLevel;
            }

            set
            {
                _SyslogLevel = (value);
            }
        }

        private int _SyslogFacility = default (int);
        public int SyslogFacility
        {
            get
            {
                return _SyslogFacility;
            }

            set
            {
                _SyslogFacility = (value);
            }
        }

        private int _SecureBits = default (int);
        public int SecureBits
        {
            get
            {
                return _SecureBits;
            }

            set
            {
                _SecureBits = (value);
            }
        }

        private ulong _CapabilityBoundingSet = default (ulong);
        public ulong CapabilityBoundingSet
        {
            get
            {
                return _CapabilityBoundingSet;
            }

            set
            {
                _CapabilityBoundingSet = (value);
            }
        }

        private ulong _AmbientCapabilities = default (ulong);
        public ulong AmbientCapabilities
        {
            get
            {
                return _AmbientCapabilities;
            }

            set
            {
                _AmbientCapabilities = (value);
            }
        }

        private string _User = default (string);
        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = (value);
            }
        }

        private string _Group = default (string);
        public string Group
        {
            get
            {
                return _Group;
            }

            set
            {
                _Group = (value);
            }
        }

        private string[] _SupplementaryGroups = default (string[]);
        public string[] SupplementaryGroups
        {
            get
            {
                return _SupplementaryGroups;
            }

            set
            {
                _SupplementaryGroups = (value);
            }
        }

        private string _PAMName = default (string);
        public string PAMName
        {
            get
            {
                return _PAMName;
            }

            set
            {
                _PAMName = (value);
            }
        }

        private string[] _ReadWritePaths = default (string[]);
        public string[] ReadWritePaths
        {
            get
            {
                return _ReadWritePaths;
            }

            set
            {
                _ReadWritePaths = (value);
            }
        }

        private string[] _ReadOnlyPaths = default (string[]);
        public string[] ReadOnlyPaths
        {
            get
            {
                return _ReadOnlyPaths;
            }

            set
            {
                _ReadOnlyPaths = (value);
            }
        }

        private string[] _InaccessiblePaths = default (string[]);
        public string[] InaccessiblePaths
        {
            get
            {
                return _InaccessiblePaths;
            }

            set
            {
                _InaccessiblePaths = (value);
            }
        }

        private ulong _MountFlags = default (ulong);
        public ulong MountFlags
        {
            get
            {
                return _MountFlags;
            }

            set
            {
                _MountFlags = (value);
            }
        }

        private bool _PrivateTmp = default (bool);
        public bool PrivateTmp
        {
            get
            {
                return _PrivateTmp;
            }

            set
            {
                _PrivateTmp = (value);
            }
        }

        private bool _PrivateNetwork = default (bool);
        public bool PrivateNetwork
        {
            get
            {
                return _PrivateNetwork;
            }

            set
            {
                _PrivateNetwork = (value);
            }
        }

        private bool _PrivateDevices = default (bool);
        public bool PrivateDevices
        {
            get
            {
                return _PrivateDevices;
            }

            set
            {
                _PrivateDevices = (value);
            }
        }

        private string _ProtectHome = default (string);
        public string ProtectHome
        {
            get
            {
                return _ProtectHome;
            }

            set
            {
                _ProtectHome = (value);
            }
        }

        private string _ProtectSystem = default (string);
        public string ProtectSystem
        {
            get
            {
                return _ProtectSystem;
            }

            set
            {
                _ProtectSystem = (value);
            }
        }

        private bool _SameProcessGroup = default (bool);
        public bool SameProcessGroup
        {
            get
            {
                return _SameProcessGroup;
            }

            set
            {
                _SameProcessGroup = (value);
            }
        }

        private string _UtmpIdentifier = default (string);
        public string UtmpIdentifier
        {
            get
            {
                return _UtmpIdentifier;
            }

            set
            {
                _UtmpIdentifier = (value);
            }
        }

        private string _UtmpMode = default (string);
        public string UtmpMode
        {
            get
            {
                return _UtmpMode;
            }

            set
            {
                _UtmpMode = (value);
            }
        }

        private (bool, string)_SELinuxContext = default ((bool, string));
        public (bool, string)SELinuxContext
        {
            get
            {
                return _SELinuxContext;
            }

            set
            {
                _SELinuxContext = (value);
            }
        }

        private (bool, string)_AppArmorProfile = default ((bool, string));
        public (bool, string)AppArmorProfile
        {
            get
            {
                return _AppArmorProfile;
            }

            set
            {
                _AppArmorProfile = (value);
            }
        }

        private (bool, string)_SmackProcessLabel = default ((bool, string));
        public (bool, string)SmackProcessLabel
        {
            get
            {
                return _SmackProcessLabel;
            }

            set
            {
                _SmackProcessLabel = (value);
            }
        }

        private bool _IgnoreSIGPIPE = default (bool);
        public bool IgnoreSIGPIPE
        {
            get
            {
                return _IgnoreSIGPIPE;
            }

            set
            {
                _IgnoreSIGPIPE = (value);
            }
        }

        private bool _NoNewPrivileges = default (bool);
        public bool NoNewPrivileges
        {
            get
            {
                return _NoNewPrivileges;
            }

            set
            {
                _NoNewPrivileges = (value);
            }
        }

        private (bool, string[])_SystemCallFilter = default ((bool, string[]));
        public (bool, string[])SystemCallFilter
        {
            get
            {
                return _SystemCallFilter;
            }

            set
            {
                _SystemCallFilter = (value);
            }
        }

        private string[] _SystemCallArchitectures = default (string[]);
        public string[] SystemCallArchitectures
        {
            get
            {
                return _SystemCallArchitectures;
            }

            set
            {
                _SystemCallArchitectures = (value);
            }
        }

        private int _SystemCallErrorNumber = default (int);
        public int SystemCallErrorNumber
        {
            get
            {
                return _SystemCallErrorNumber;
            }

            set
            {
                _SystemCallErrorNumber = (value);
            }
        }

        private string _Personality = default (string);
        public string Personality
        {
            get
            {
                return _Personality;
            }

            set
            {
                _Personality = (value);
            }
        }

        private (bool, string[])_RestrictAddressFamilies = default ((bool, string[]));
        public (bool, string[])RestrictAddressFamilies
        {
            get
            {
                return _RestrictAddressFamilies;
            }

            set
            {
                _RestrictAddressFamilies = (value);
            }
        }

        private uint _RuntimeDirectoryMode = default (uint);
        public uint RuntimeDirectoryMode
        {
            get
            {
                return _RuntimeDirectoryMode;
            }

            set
            {
                _RuntimeDirectoryMode = (value);
            }
        }

        private string[] _RuntimeDirectory = default (string[]);
        public string[] RuntimeDirectory
        {
            get
            {
                return _RuntimeDirectory;
            }

            set
            {
                _RuntimeDirectory = (value);
            }
        }

        private bool _MemoryDenyWriteExecute = default (bool);
        public bool MemoryDenyWriteExecute
        {
            get
            {
                return _MemoryDenyWriteExecute;
            }

            set
            {
                _MemoryDenyWriteExecute = (value);
            }
        }

        private bool _RestrictRealtime = default (bool);
        public bool RestrictRealtime
        {
            get
            {
                return _RestrictRealtime;
            }

            set
            {
                _RestrictRealtime = (value);
            }
        }

        private string _KillMode = default (string);
        public string KillMode
        {
            get
            {
                return _KillMode;
            }

            set
            {
                _KillMode = (value);
            }
        }

        private int _KillSignal = default (int);
        public int KillSignal
        {
            get
            {
                return _KillSignal;
            }

            set
            {
                _KillSignal = (value);
            }
        }

        private bool _SendSIGKILL = default (bool);
        public bool SendSIGKILL
        {
            get
            {
                return _SendSIGKILL;
            }

            set
            {
                _SendSIGKILL = (value);
            }
        }

        private bool _SendSIGHUP = default (bool);
        public bool SendSIGHUP
        {
            get
            {
                return _SendSIGHUP;
            }

            set
            {
                _SendSIGHUP = (value);
            }
        }
    }

    static class MountExtensions
    {
        public static Task<string> GetWhereAsync(this IMount o) => o.GetAsync<string>("Where");
        public static Task<string> GetWhatAsync(this IMount o) => o.GetAsync<string>("What");
        public static Task<string> GetOptionsAsync(this IMount o) => o.GetAsync<string>("Options");
        public static Task<string> GetTypeAsync(this IMount o) => o.GetAsync<string>("Type");
        public static Task<ulong> GetTimeoutUSecAsync(this IMount o) => o.GetAsync<ulong>("TimeoutUSec");
        public static Task<uint> GetControlPIDAsync(this IMount o) => o.GetAsync<uint>("ControlPID");
        public static Task<uint> GetDirectoryModeAsync(this IMount o) => o.GetAsync<uint>("DirectoryMode");
        public static Task<bool> GetSloppyOptionsAsync(this IMount o) => o.GetAsync<bool>("SloppyOptions");
        public static Task<string> GetResultAsync(this IMount o) => o.GetAsync<string>("Result");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecMountAsync(this IMount o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecMount");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecUnmountAsync(this IMount o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecUnmount");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecRemountAsync(this IMount o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecRemount");
        public static Task<string> GetSliceAsync(this IMount o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this IMount o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this IMount o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this IMount o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this IMount o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this IMount o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this IMount o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this IMount o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this IMount o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this IMount o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this IMount o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this IMount o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this IMount o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this IMount o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this IMount o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this IMount o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this IMount o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this IMount o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this IMount o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this IMount o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this IMount o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this IMount o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this IMount o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this IMount o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this IMount o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this IMount o) => o.GetAsync<ulong>("TasksMax");
        public static Task<string[]> GetEnvironmentAsync(this IMount o) => o.GetAsync<string[]>("Environment");
        public static Task<(string, bool)[]> GetEnvironmentFilesAsync(this IMount o) => o.GetAsync<(string, bool)[]>("EnvironmentFiles");
        public static Task<string[]> GetPassEnvironmentAsync(this IMount o) => o.GetAsync<string[]>("PassEnvironment");
        public static Task<uint> GetUMaskAsync(this IMount o) => o.GetAsync<uint>("UMask");
        public static Task<ulong> GetLimitCPUAsync(this IMount o) => o.GetAsync<ulong>("LimitCPU");
        public static Task<ulong> GetLimitCPUSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitCPUSoft");
        public static Task<ulong> GetLimitFSIZEAsync(this IMount o) => o.GetAsync<ulong>("LimitFSIZE");
        public static Task<ulong> GetLimitFSIZESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitFSIZESoft");
        public static Task<ulong> GetLimitDATAAsync(this IMount o) => o.GetAsync<ulong>("LimitDATA");
        public static Task<ulong> GetLimitDATASoftAsync(this IMount o) => o.GetAsync<ulong>("LimitDATASoft");
        public static Task<ulong> GetLimitSTACKAsync(this IMount o) => o.GetAsync<ulong>("LimitSTACK");
        public static Task<ulong> GetLimitSTACKSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitSTACKSoft");
        public static Task<ulong> GetLimitCOREAsync(this IMount o) => o.GetAsync<ulong>("LimitCORE");
        public static Task<ulong> GetLimitCORESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitCORESoft");
        public static Task<ulong> GetLimitRSSAsync(this IMount o) => o.GetAsync<ulong>("LimitRSS");
        public static Task<ulong> GetLimitRSSSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitRSSSoft");
        public static Task<ulong> GetLimitNOFILEAsync(this IMount o) => o.GetAsync<ulong>("LimitNOFILE");
        public static Task<ulong> GetLimitNOFILESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitNOFILESoft");
        public static Task<ulong> GetLimitASAsync(this IMount o) => o.GetAsync<ulong>("LimitAS");
        public static Task<ulong> GetLimitASSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitASSoft");
        public static Task<ulong> GetLimitNPROCAsync(this IMount o) => o.GetAsync<ulong>("LimitNPROC");
        public static Task<ulong> GetLimitNPROCSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitNPROCSoft");
        public static Task<ulong> GetLimitMEMLOCKAsync(this IMount o) => o.GetAsync<ulong>("LimitMEMLOCK");
        public static Task<ulong> GetLimitMEMLOCKSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitMEMLOCKSoft");
        public static Task<ulong> GetLimitLOCKSAsync(this IMount o) => o.GetAsync<ulong>("LimitLOCKS");
        public static Task<ulong> GetLimitLOCKSSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitLOCKSSoft");
        public static Task<ulong> GetLimitSIGPENDINGAsync(this IMount o) => o.GetAsync<ulong>("LimitSIGPENDING");
        public static Task<ulong> GetLimitSIGPENDINGSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitSIGPENDINGSoft");
        public static Task<ulong> GetLimitMSGQUEUEAsync(this IMount o) => o.GetAsync<ulong>("LimitMSGQUEUE");
        public static Task<ulong> GetLimitMSGQUEUESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitMSGQUEUESoft");
        public static Task<ulong> GetLimitNICEAsync(this IMount o) => o.GetAsync<ulong>("LimitNICE");
        public static Task<ulong> GetLimitNICESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitNICESoft");
        public static Task<ulong> GetLimitRTPRIOAsync(this IMount o) => o.GetAsync<ulong>("LimitRTPRIO");
        public static Task<ulong> GetLimitRTPRIOSoftAsync(this IMount o) => o.GetAsync<ulong>("LimitRTPRIOSoft");
        public static Task<ulong> GetLimitRTTIMEAsync(this IMount o) => o.GetAsync<ulong>("LimitRTTIME");
        public static Task<ulong> GetLimitRTTIMESoftAsync(this IMount o) => o.GetAsync<ulong>("LimitRTTIMESoft");
        public static Task<string> GetWorkingDirectoryAsync(this IMount o) => o.GetAsync<string>("WorkingDirectory");
        public static Task<string> GetRootDirectoryAsync(this IMount o) => o.GetAsync<string>("RootDirectory");
        public static Task<int> GetOOMScoreAdjustAsync(this IMount o) => o.GetAsync<int>("OOMScoreAdjust");
        public static Task<int> GetNiceAsync(this IMount o) => o.GetAsync<int>("Nice");
        public static Task<int> GetIOSchedulingAsync(this IMount o) => o.GetAsync<int>("IOScheduling");
        public static Task<int> GetCPUSchedulingPolicyAsync(this IMount o) => o.GetAsync<int>("CPUSchedulingPolicy");
        public static Task<int> GetCPUSchedulingPriorityAsync(this IMount o) => o.GetAsync<int>("CPUSchedulingPriority");
        public static Task<byte[]> GetCPUAffinityAsync(this IMount o) => o.GetAsync<byte[]>("CPUAffinity");
        public static Task<ulong> GetTimerSlackNSecAsync(this IMount o) => o.GetAsync<ulong>("TimerSlackNSec");
        public static Task<bool> GetCPUSchedulingResetOnForkAsync(this IMount o) => o.GetAsync<bool>("CPUSchedulingResetOnFork");
        public static Task<bool> GetNonBlockingAsync(this IMount o) => o.GetAsync<bool>("NonBlocking");
        public static Task<string> GetStandardInputAsync(this IMount o) => o.GetAsync<string>("StandardInput");
        public static Task<string> GetStandardOutputAsync(this IMount o) => o.GetAsync<string>("StandardOutput");
        public static Task<string> GetStandardErrorAsync(this IMount o) => o.GetAsync<string>("StandardError");
        public static Task<string> GetTTYPathAsync(this IMount o) => o.GetAsync<string>("TTYPath");
        public static Task<bool> GetTTYResetAsync(this IMount o) => o.GetAsync<bool>("TTYReset");
        public static Task<bool> GetTTYVHangupAsync(this IMount o) => o.GetAsync<bool>("TTYVHangup");
        public static Task<bool> GetTTYVTDisallocateAsync(this IMount o) => o.GetAsync<bool>("TTYVTDisallocate");
        public static Task<int> GetSyslogPriorityAsync(this IMount o) => o.GetAsync<int>("SyslogPriority");
        public static Task<string> GetSyslogIdentifierAsync(this IMount o) => o.GetAsync<string>("SyslogIdentifier");
        public static Task<bool> GetSyslogLevelPrefixAsync(this IMount o) => o.GetAsync<bool>("SyslogLevelPrefix");
        public static Task<int> GetSyslogLevelAsync(this IMount o) => o.GetAsync<int>("SyslogLevel");
        public static Task<int> GetSyslogFacilityAsync(this IMount o) => o.GetAsync<int>("SyslogFacility");
        public static Task<int> GetSecureBitsAsync(this IMount o) => o.GetAsync<int>("SecureBits");
        public static Task<ulong> GetCapabilityBoundingSetAsync(this IMount o) => o.GetAsync<ulong>("CapabilityBoundingSet");
        public static Task<ulong> GetAmbientCapabilitiesAsync(this IMount o) => o.GetAsync<ulong>("AmbientCapabilities");
        public static Task<string> GetUserAsync(this IMount o) => o.GetAsync<string>("User");
        public static Task<string> GetGroupAsync(this IMount o) => o.GetAsync<string>("Group");
        public static Task<string[]> GetSupplementaryGroupsAsync(this IMount o) => o.GetAsync<string[]>("SupplementaryGroups");
        public static Task<string> GetPAMNameAsync(this IMount o) => o.GetAsync<string>("PAMName");
        public static Task<string[]> GetReadWritePathsAsync(this IMount o) => o.GetAsync<string[]>("ReadWritePaths");
        public static Task<string[]> GetReadOnlyPathsAsync(this IMount o) => o.GetAsync<string[]>("ReadOnlyPaths");
        public static Task<string[]> GetInaccessiblePathsAsync(this IMount o) => o.GetAsync<string[]>("InaccessiblePaths");
        public static Task<ulong> GetMountFlagsAsync(this IMount o) => o.GetAsync<ulong>("MountFlags");
        public static Task<bool> GetPrivateTmpAsync(this IMount o) => o.GetAsync<bool>("PrivateTmp");
        public static Task<bool> GetPrivateNetworkAsync(this IMount o) => o.GetAsync<bool>("PrivateNetwork");
        public static Task<bool> GetPrivateDevicesAsync(this IMount o) => o.GetAsync<bool>("PrivateDevices");
        public static Task<string> GetProtectHomeAsync(this IMount o) => o.GetAsync<string>("ProtectHome");
        public static Task<string> GetProtectSystemAsync(this IMount o) => o.GetAsync<string>("ProtectSystem");
        public static Task<bool> GetSameProcessGroupAsync(this IMount o) => o.GetAsync<bool>("SameProcessGroup");
        public static Task<string> GetUtmpIdentifierAsync(this IMount o) => o.GetAsync<string>("UtmpIdentifier");
        public static Task<string> GetUtmpModeAsync(this IMount o) => o.GetAsync<string>("UtmpMode");
        public static Task<(bool, string)> GetSELinuxContextAsync(this IMount o) => o.GetAsync<(bool, string)>("SELinuxContext");
        public static Task<(bool, string)> GetAppArmorProfileAsync(this IMount o) => o.GetAsync<(bool, string)>("AppArmorProfile");
        public static Task<(bool, string)> GetSmackProcessLabelAsync(this IMount o) => o.GetAsync<(bool, string)>("SmackProcessLabel");
        public static Task<bool> GetIgnoreSIGPIPEAsync(this IMount o) => o.GetAsync<bool>("IgnoreSIGPIPE");
        public static Task<bool> GetNoNewPrivilegesAsync(this IMount o) => o.GetAsync<bool>("NoNewPrivileges");
        public static Task<(bool, string[])> GetSystemCallFilterAsync(this IMount o) => o.GetAsync<(bool, string[])>("SystemCallFilter");
        public static Task<string[]> GetSystemCallArchitecturesAsync(this IMount o) => o.GetAsync<string[]>("SystemCallArchitectures");
        public static Task<int> GetSystemCallErrorNumberAsync(this IMount o) => o.GetAsync<int>("SystemCallErrorNumber");
        public static Task<string> GetPersonalityAsync(this IMount o) => o.GetAsync<string>("Personality");
        public static Task<(bool, string[])> GetRestrictAddressFamiliesAsync(this IMount o) => o.GetAsync<(bool, string[])>("RestrictAddressFamilies");
        public static Task<uint> GetRuntimeDirectoryModeAsync(this IMount o) => o.GetAsync<uint>("RuntimeDirectoryMode");
        public static Task<string[]> GetRuntimeDirectoryAsync(this IMount o) => o.GetAsync<string[]>("RuntimeDirectory");
        public static Task<bool> GetMemoryDenyWriteExecuteAsync(this IMount o) => o.GetAsync<bool>("MemoryDenyWriteExecute");
        public static Task<bool> GetRestrictRealtimeAsync(this IMount o) => o.GetAsync<bool>("RestrictRealtime");
        public static Task<string> GetKillModeAsync(this IMount o) => o.GetAsync<string>("KillMode");
        public static Task<int> GetKillSignalAsync(this IMount o) => o.GetAsync<int>("KillSignal");
        public static Task<bool> GetSendSIGKILLAsync(this IMount o) => o.GetAsync<bool>("SendSIGKILL");
        public static Task<bool> GetSendSIGHUPAsync(this IMount o) => o.GetAsync<bool>("SendSIGHUP");
    }

    [DBusInterface("org.freedesktop.systemd1.Socket")]
    interface ISocket : IDBusObject
    {
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<SocketProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SocketProperties
    {
        private string _BindIPv6Only = default (string);
        public string BindIPv6Only
        {
            get
            {
                return _BindIPv6Only;
            }

            set
            {
                _BindIPv6Only = (value);
            }
        }

        private uint _Backlog = default (uint);
        public uint Backlog
        {
            get
            {
                return _Backlog;
            }

            set
            {
                _Backlog = (value);
            }
        }

        private ulong _TimeoutUSec = default (ulong);
        public ulong TimeoutUSec
        {
            get
            {
                return _TimeoutUSec;
            }

            set
            {
                _TimeoutUSec = (value);
            }
        }

        private string _BindToDevice = default (string);
        public string BindToDevice
        {
            get
            {
                return _BindToDevice;
            }

            set
            {
                _BindToDevice = (value);
            }
        }

        private string _SocketUser = default (string);
        public string SocketUser
        {
            get
            {
                return _SocketUser;
            }

            set
            {
                _SocketUser = (value);
            }
        }

        private string _SocketGroup = default (string);
        public string SocketGroup
        {
            get
            {
                return _SocketGroup;
            }

            set
            {
                _SocketGroup = (value);
            }
        }

        private uint _SocketMode = default (uint);
        public uint SocketMode
        {
            get
            {
                return _SocketMode;
            }

            set
            {
                _SocketMode = (value);
            }
        }

        private uint _DirectoryMode = default (uint);
        public uint DirectoryMode
        {
            get
            {
                return _DirectoryMode;
            }

            set
            {
                _DirectoryMode = (value);
            }
        }

        private bool _Accept = default (bool);
        public bool Accept
        {
            get
            {
                return _Accept;
            }

            set
            {
                _Accept = (value);
            }
        }

        private bool _Writable = default (bool);
        public bool Writable
        {
            get
            {
                return _Writable;
            }

            set
            {
                _Writable = (value);
            }
        }

        private bool _KeepAlive = default (bool);
        public bool KeepAlive
        {
            get
            {
                return _KeepAlive;
            }

            set
            {
                _KeepAlive = (value);
            }
        }

        private ulong _KeepAliveTimeUSec = default (ulong);
        public ulong KeepAliveTimeUSec
        {
            get
            {
                return _KeepAliveTimeUSec;
            }

            set
            {
                _KeepAliveTimeUSec = (value);
            }
        }

        private ulong _KeepAliveIntervalUSec = default (ulong);
        public ulong KeepAliveIntervalUSec
        {
            get
            {
                return _KeepAliveIntervalUSec;
            }

            set
            {
                _KeepAliveIntervalUSec = (value);
            }
        }

        private uint _KeepAliveProbes = default (uint);
        public uint KeepAliveProbes
        {
            get
            {
                return _KeepAliveProbes;
            }

            set
            {
                _KeepAliveProbes = (value);
            }
        }

        private ulong _DeferAcceptUSec = default (ulong);
        public ulong DeferAcceptUSec
        {
            get
            {
                return _DeferAcceptUSec;
            }

            set
            {
                _DeferAcceptUSec = (value);
            }
        }

        private bool _NoDelay = default (bool);
        public bool NoDelay
        {
            get
            {
                return _NoDelay;
            }

            set
            {
                _NoDelay = (value);
            }
        }

        private int _Priority = default (int);
        public int Priority
        {
            get
            {
                return _Priority;
            }

            set
            {
                _Priority = (value);
            }
        }

        private ulong _ReceiveBuffer = default (ulong);
        public ulong ReceiveBuffer
        {
            get
            {
                return _ReceiveBuffer;
            }

            set
            {
                _ReceiveBuffer = (value);
            }
        }

        private ulong _SendBuffer = default (ulong);
        public ulong SendBuffer
        {
            get
            {
                return _SendBuffer;
            }

            set
            {
                _SendBuffer = (value);
            }
        }

        private int _IPTOS = default (int);
        public int IPTOS
        {
            get
            {
                return _IPTOS;
            }

            set
            {
                _IPTOS = (value);
            }
        }

        private int _IPTTL = default (int);
        public int IPTTL
        {
            get
            {
                return _IPTTL;
            }

            set
            {
                _IPTTL = (value);
            }
        }

        private ulong _PipeSize = default (ulong);
        public ulong PipeSize
        {
            get
            {
                return _PipeSize;
            }

            set
            {
                _PipeSize = (value);
            }
        }

        private bool _FreeBind = default (bool);
        public bool FreeBind
        {
            get
            {
                return _FreeBind;
            }

            set
            {
                _FreeBind = (value);
            }
        }

        private bool _Transparent = default (bool);
        public bool Transparent
        {
            get
            {
                return _Transparent;
            }

            set
            {
                _Transparent = (value);
            }
        }

        private bool _Broadcast = default (bool);
        public bool Broadcast
        {
            get
            {
                return _Broadcast;
            }

            set
            {
                _Broadcast = (value);
            }
        }

        private bool _PassCredentials = default (bool);
        public bool PassCredentials
        {
            get
            {
                return _PassCredentials;
            }

            set
            {
                _PassCredentials = (value);
            }
        }

        private bool _PassSecurity = default (bool);
        public bool PassSecurity
        {
            get
            {
                return _PassSecurity;
            }

            set
            {
                _PassSecurity = (value);
            }
        }

        private bool _RemoveOnStop = default (bool);
        public bool RemoveOnStop
        {
            get
            {
                return _RemoveOnStop;
            }

            set
            {
                _RemoveOnStop = (value);
            }
        }

        private (string, string)[] _Listen = default ((string, string)[]);
        public (string, string)[] Listen
        {
            get
            {
                return _Listen;
            }

            set
            {
                _Listen = (value);
            }
        }

        private string[] _Symlinks = default (string[]);
        public string[] Symlinks
        {
            get
            {
                return _Symlinks;
            }

            set
            {
                _Symlinks = (value);
            }
        }

        private int _Mark = default (int);
        public int Mark
        {
            get
            {
                return _Mark;
            }

            set
            {
                _Mark = (value);
            }
        }

        private uint _MaxConnections = default (uint);
        public uint MaxConnections
        {
            get
            {
                return _MaxConnections;
            }

            set
            {
                _MaxConnections = (value);
            }
        }

        private long _MessageQueueMaxMessages = default (long);
        public long MessageQueueMaxMessages
        {
            get
            {
                return _MessageQueueMaxMessages;
            }

            set
            {
                _MessageQueueMaxMessages = (value);
            }
        }

        private long _MessageQueueMessageSize = default (long);
        public long MessageQueueMessageSize
        {
            get
            {
                return _MessageQueueMessageSize;
            }

            set
            {
                _MessageQueueMessageSize = (value);
            }
        }

        private bool _ReusePort = default (bool);
        public bool ReusePort
        {
            get
            {
                return _ReusePort;
            }

            set
            {
                _ReusePort = (value);
            }
        }

        private string _SmackLabel = default (string);
        public string SmackLabel
        {
            get
            {
                return _SmackLabel;
            }

            set
            {
                _SmackLabel = (value);
            }
        }

        private string _SmackLabelIPIn = default (string);
        public string SmackLabelIPIn
        {
            get
            {
                return _SmackLabelIPIn;
            }

            set
            {
                _SmackLabelIPIn = (value);
            }
        }

        private string _SmackLabelIPOut = default (string);
        public string SmackLabelIPOut
        {
            get
            {
                return _SmackLabelIPOut;
            }

            set
            {
                _SmackLabelIPOut = (value);
            }
        }

        private uint _ControlPID = default (uint);
        public uint ControlPID
        {
            get
            {
                return _ControlPID;
            }

            set
            {
                _ControlPID = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private uint _NConnections = default (uint);
        public uint NConnections
        {
            get
            {
                return _NConnections;
            }

            set
            {
                _NConnections = (value);
            }
        }

        private uint _NAccepted = default (uint);
        public uint NAccepted
        {
            get
            {
                return _NAccepted;
            }

            set
            {
                _NAccepted = (value);
            }
        }

        private string _FileDescriptorName = default (string);
        public string FileDescriptorName
        {
            get
            {
                return _FileDescriptorName;
            }

            set
            {
                _FileDescriptorName = (value);
            }
        }

        private int _SocketProtocol = default (int);
        public int SocketProtocol
        {
            get
            {
                return _SocketProtocol;
            }

            set
            {
                _SocketProtocol = (value);
            }
        }

        private ulong _TriggerLimitIntervalUSec = default (ulong);
        public ulong TriggerLimitIntervalUSec
        {
            get
            {
                return _TriggerLimitIntervalUSec;
            }

            set
            {
                _TriggerLimitIntervalUSec = (value);
            }
        }

        private uint _TriggerLimitBurst = default (uint);
        public uint TriggerLimitBurst
        {
            get
            {
                return _TriggerLimitBurst;
            }

            set
            {
                _TriggerLimitBurst = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStartPre = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStartPre
        {
            get
            {
                return _ExecStartPre;
            }

            set
            {
                _ExecStartPre = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStartPost = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStartPost
        {
            get
            {
                return _ExecStartPost;
            }

            set
            {
                _ExecStartPost = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStopPre = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStopPre
        {
            get
            {
                return _ExecStopPre;
            }

            set
            {
                _ExecStopPre = (value);
            }
        }

        private (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] _ExecStopPost = default ((string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]);
        public (string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[] ExecStopPost
        {
            get
            {
                return _ExecStopPost;
            }

            set
            {
                _ExecStopPost = (value);
            }
        }

        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }

        private string[] _Environment = default (string[]);
        public string[] Environment
        {
            get
            {
                return _Environment;
            }

            set
            {
                _Environment = (value);
            }
        }

        private (string, bool)[] _EnvironmentFiles = default ((string, bool)[]);
        public (string, bool)[] EnvironmentFiles
        {
            get
            {
                return _EnvironmentFiles;
            }

            set
            {
                _EnvironmentFiles = (value);
            }
        }

        private string[] _PassEnvironment = default (string[]);
        public string[] PassEnvironment
        {
            get
            {
                return _PassEnvironment;
            }

            set
            {
                _PassEnvironment = (value);
            }
        }

        private uint _UMask = default (uint);
        public uint UMask
        {
            get
            {
                return _UMask;
            }

            set
            {
                _UMask = (value);
            }
        }

        private ulong _LimitCPU = default (ulong);
        public ulong LimitCPU
        {
            get
            {
                return _LimitCPU;
            }

            set
            {
                _LimitCPU = (value);
            }
        }

        private ulong _LimitCPUSoft = default (ulong);
        public ulong LimitCPUSoft
        {
            get
            {
                return _LimitCPUSoft;
            }

            set
            {
                _LimitCPUSoft = (value);
            }
        }

        private ulong _LimitFSIZE = default (ulong);
        public ulong LimitFSIZE
        {
            get
            {
                return _LimitFSIZE;
            }

            set
            {
                _LimitFSIZE = (value);
            }
        }

        private ulong _LimitFSIZESoft = default (ulong);
        public ulong LimitFSIZESoft
        {
            get
            {
                return _LimitFSIZESoft;
            }

            set
            {
                _LimitFSIZESoft = (value);
            }
        }

        private ulong _LimitDATA = default (ulong);
        public ulong LimitDATA
        {
            get
            {
                return _LimitDATA;
            }

            set
            {
                _LimitDATA = (value);
            }
        }

        private ulong _LimitDATASoft = default (ulong);
        public ulong LimitDATASoft
        {
            get
            {
                return _LimitDATASoft;
            }

            set
            {
                _LimitDATASoft = (value);
            }
        }

        private ulong _LimitSTACK = default (ulong);
        public ulong LimitSTACK
        {
            get
            {
                return _LimitSTACK;
            }

            set
            {
                _LimitSTACK = (value);
            }
        }

        private ulong _LimitSTACKSoft = default (ulong);
        public ulong LimitSTACKSoft
        {
            get
            {
                return _LimitSTACKSoft;
            }

            set
            {
                _LimitSTACKSoft = (value);
            }
        }

        private ulong _LimitCORE = default (ulong);
        public ulong LimitCORE
        {
            get
            {
                return _LimitCORE;
            }

            set
            {
                _LimitCORE = (value);
            }
        }

        private ulong _LimitCORESoft = default (ulong);
        public ulong LimitCORESoft
        {
            get
            {
                return _LimitCORESoft;
            }

            set
            {
                _LimitCORESoft = (value);
            }
        }

        private ulong _LimitRSS = default (ulong);
        public ulong LimitRSS
        {
            get
            {
                return _LimitRSS;
            }

            set
            {
                _LimitRSS = (value);
            }
        }

        private ulong _LimitRSSSoft = default (ulong);
        public ulong LimitRSSSoft
        {
            get
            {
                return _LimitRSSSoft;
            }

            set
            {
                _LimitRSSSoft = (value);
            }
        }

        private ulong _LimitNOFILE = default (ulong);
        public ulong LimitNOFILE
        {
            get
            {
                return _LimitNOFILE;
            }

            set
            {
                _LimitNOFILE = (value);
            }
        }

        private ulong _LimitNOFILESoft = default (ulong);
        public ulong LimitNOFILESoft
        {
            get
            {
                return _LimitNOFILESoft;
            }

            set
            {
                _LimitNOFILESoft = (value);
            }
        }

        private ulong _LimitAS = default (ulong);
        public ulong LimitAS
        {
            get
            {
                return _LimitAS;
            }

            set
            {
                _LimitAS = (value);
            }
        }

        private ulong _LimitASSoft = default (ulong);
        public ulong LimitASSoft
        {
            get
            {
                return _LimitASSoft;
            }

            set
            {
                _LimitASSoft = (value);
            }
        }

        private ulong _LimitNPROC = default (ulong);
        public ulong LimitNPROC
        {
            get
            {
                return _LimitNPROC;
            }

            set
            {
                _LimitNPROC = (value);
            }
        }

        private ulong _LimitNPROCSoft = default (ulong);
        public ulong LimitNPROCSoft
        {
            get
            {
                return _LimitNPROCSoft;
            }

            set
            {
                _LimitNPROCSoft = (value);
            }
        }

        private ulong _LimitMEMLOCK = default (ulong);
        public ulong LimitMEMLOCK
        {
            get
            {
                return _LimitMEMLOCK;
            }

            set
            {
                _LimitMEMLOCK = (value);
            }
        }

        private ulong _LimitMEMLOCKSoft = default (ulong);
        public ulong LimitMEMLOCKSoft
        {
            get
            {
                return _LimitMEMLOCKSoft;
            }

            set
            {
                _LimitMEMLOCKSoft = (value);
            }
        }

        private ulong _LimitLOCKS = default (ulong);
        public ulong LimitLOCKS
        {
            get
            {
                return _LimitLOCKS;
            }

            set
            {
                _LimitLOCKS = (value);
            }
        }

        private ulong _LimitLOCKSSoft = default (ulong);
        public ulong LimitLOCKSSoft
        {
            get
            {
                return _LimitLOCKSSoft;
            }

            set
            {
                _LimitLOCKSSoft = (value);
            }
        }

        private ulong _LimitSIGPENDING = default (ulong);
        public ulong LimitSIGPENDING
        {
            get
            {
                return _LimitSIGPENDING;
            }

            set
            {
                _LimitSIGPENDING = (value);
            }
        }

        private ulong _LimitSIGPENDINGSoft = default (ulong);
        public ulong LimitSIGPENDINGSoft
        {
            get
            {
                return _LimitSIGPENDINGSoft;
            }

            set
            {
                _LimitSIGPENDINGSoft = (value);
            }
        }

        private ulong _LimitMSGQUEUE = default (ulong);
        public ulong LimitMSGQUEUE
        {
            get
            {
                return _LimitMSGQUEUE;
            }

            set
            {
                _LimitMSGQUEUE = (value);
            }
        }

        private ulong _LimitMSGQUEUESoft = default (ulong);
        public ulong LimitMSGQUEUESoft
        {
            get
            {
                return _LimitMSGQUEUESoft;
            }

            set
            {
                _LimitMSGQUEUESoft = (value);
            }
        }

        private ulong _LimitNICE = default (ulong);
        public ulong LimitNICE
        {
            get
            {
                return _LimitNICE;
            }

            set
            {
                _LimitNICE = (value);
            }
        }

        private ulong _LimitNICESoft = default (ulong);
        public ulong LimitNICESoft
        {
            get
            {
                return _LimitNICESoft;
            }

            set
            {
                _LimitNICESoft = (value);
            }
        }

        private ulong _LimitRTPRIO = default (ulong);
        public ulong LimitRTPRIO
        {
            get
            {
                return _LimitRTPRIO;
            }

            set
            {
                _LimitRTPRIO = (value);
            }
        }

        private ulong _LimitRTPRIOSoft = default (ulong);
        public ulong LimitRTPRIOSoft
        {
            get
            {
                return _LimitRTPRIOSoft;
            }

            set
            {
                _LimitRTPRIOSoft = (value);
            }
        }

        private ulong _LimitRTTIME = default (ulong);
        public ulong LimitRTTIME
        {
            get
            {
                return _LimitRTTIME;
            }

            set
            {
                _LimitRTTIME = (value);
            }
        }

        private ulong _LimitRTTIMESoft = default (ulong);
        public ulong LimitRTTIMESoft
        {
            get
            {
                return _LimitRTTIMESoft;
            }

            set
            {
                _LimitRTTIMESoft = (value);
            }
        }

        private string _WorkingDirectory = default (string);
        public string WorkingDirectory
        {
            get
            {
                return _WorkingDirectory;
            }

            set
            {
                _WorkingDirectory = (value);
            }
        }

        private string _RootDirectory = default (string);
        public string RootDirectory
        {
            get
            {
                return _RootDirectory;
            }

            set
            {
                _RootDirectory = (value);
            }
        }

        private int _OOMScoreAdjust = default (int);
        public int OOMScoreAdjust
        {
            get
            {
                return _OOMScoreAdjust;
            }

            set
            {
                _OOMScoreAdjust = (value);
            }
        }

        private int _Nice = default (int);
        public int Nice
        {
            get
            {
                return _Nice;
            }

            set
            {
                _Nice = (value);
            }
        }

        private int _IOScheduling = default (int);
        public int IOScheduling
        {
            get
            {
                return _IOScheduling;
            }

            set
            {
                _IOScheduling = (value);
            }
        }

        private int _CPUSchedulingPolicy = default (int);
        public int CPUSchedulingPolicy
        {
            get
            {
                return _CPUSchedulingPolicy;
            }

            set
            {
                _CPUSchedulingPolicy = (value);
            }
        }

        private int _CPUSchedulingPriority = default (int);
        public int CPUSchedulingPriority
        {
            get
            {
                return _CPUSchedulingPriority;
            }

            set
            {
                _CPUSchedulingPriority = (value);
            }
        }

        private byte[] _CPUAffinity = default (byte[]);
        public byte[] CPUAffinity
        {
            get
            {
                return _CPUAffinity;
            }

            set
            {
                _CPUAffinity = (value);
            }
        }

        private ulong _TimerSlackNSec = default (ulong);
        public ulong TimerSlackNSec
        {
            get
            {
                return _TimerSlackNSec;
            }

            set
            {
                _TimerSlackNSec = (value);
            }
        }

        private bool _CPUSchedulingResetOnFork = default (bool);
        public bool CPUSchedulingResetOnFork
        {
            get
            {
                return _CPUSchedulingResetOnFork;
            }

            set
            {
                _CPUSchedulingResetOnFork = (value);
            }
        }

        private bool _NonBlocking = default (bool);
        public bool NonBlocking
        {
            get
            {
                return _NonBlocking;
            }

            set
            {
                _NonBlocking = (value);
            }
        }

        private string _StandardInput = default (string);
        public string StandardInput
        {
            get
            {
                return _StandardInput;
            }

            set
            {
                _StandardInput = (value);
            }
        }

        private string _StandardOutput = default (string);
        public string StandardOutput
        {
            get
            {
                return _StandardOutput;
            }

            set
            {
                _StandardOutput = (value);
            }
        }

        private string _StandardError = default (string);
        public string StandardError
        {
            get
            {
                return _StandardError;
            }

            set
            {
                _StandardError = (value);
            }
        }

        private string _TTYPath = default (string);
        public string TTYPath
        {
            get
            {
                return _TTYPath;
            }

            set
            {
                _TTYPath = (value);
            }
        }

        private bool _TTYReset = default (bool);
        public bool TTYReset
        {
            get
            {
                return _TTYReset;
            }

            set
            {
                _TTYReset = (value);
            }
        }

        private bool _TTYVHangup = default (bool);
        public bool TTYVHangup
        {
            get
            {
                return _TTYVHangup;
            }

            set
            {
                _TTYVHangup = (value);
            }
        }

        private bool _TTYVTDisallocate = default (bool);
        public bool TTYVTDisallocate
        {
            get
            {
                return _TTYVTDisallocate;
            }

            set
            {
                _TTYVTDisallocate = (value);
            }
        }

        private int _SyslogPriority = default (int);
        public int SyslogPriority
        {
            get
            {
                return _SyslogPriority;
            }

            set
            {
                _SyslogPriority = (value);
            }
        }

        private string _SyslogIdentifier = default (string);
        public string SyslogIdentifier
        {
            get
            {
                return _SyslogIdentifier;
            }

            set
            {
                _SyslogIdentifier = (value);
            }
        }

        private bool _SyslogLevelPrefix = default (bool);
        public bool SyslogLevelPrefix
        {
            get
            {
                return _SyslogLevelPrefix;
            }

            set
            {
                _SyslogLevelPrefix = (value);
            }
        }

        private int _SyslogLevel = default (int);
        public int SyslogLevel
        {
            get
            {
                return _SyslogLevel;
            }

            set
            {
                _SyslogLevel = (value);
            }
        }

        private int _SyslogFacility = default (int);
        public int SyslogFacility
        {
            get
            {
                return _SyslogFacility;
            }

            set
            {
                _SyslogFacility = (value);
            }
        }

        private int _SecureBits = default (int);
        public int SecureBits
        {
            get
            {
                return _SecureBits;
            }

            set
            {
                _SecureBits = (value);
            }
        }

        private ulong _CapabilityBoundingSet = default (ulong);
        public ulong CapabilityBoundingSet
        {
            get
            {
                return _CapabilityBoundingSet;
            }

            set
            {
                _CapabilityBoundingSet = (value);
            }
        }

        private ulong _AmbientCapabilities = default (ulong);
        public ulong AmbientCapabilities
        {
            get
            {
                return _AmbientCapabilities;
            }

            set
            {
                _AmbientCapabilities = (value);
            }
        }

        private string _User = default (string);
        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = (value);
            }
        }

        private string _Group = default (string);
        public string Group
        {
            get
            {
                return _Group;
            }

            set
            {
                _Group = (value);
            }
        }

        private string[] _SupplementaryGroups = default (string[]);
        public string[] SupplementaryGroups
        {
            get
            {
                return _SupplementaryGroups;
            }

            set
            {
                _SupplementaryGroups = (value);
            }
        }

        private string _PAMName = default (string);
        public string PAMName
        {
            get
            {
                return _PAMName;
            }

            set
            {
                _PAMName = (value);
            }
        }

        private string[] _ReadWritePaths = default (string[]);
        public string[] ReadWritePaths
        {
            get
            {
                return _ReadWritePaths;
            }

            set
            {
                _ReadWritePaths = (value);
            }
        }

        private string[] _ReadOnlyPaths = default (string[]);
        public string[] ReadOnlyPaths
        {
            get
            {
                return _ReadOnlyPaths;
            }

            set
            {
                _ReadOnlyPaths = (value);
            }
        }

        private string[] _InaccessiblePaths = default (string[]);
        public string[] InaccessiblePaths
        {
            get
            {
                return _InaccessiblePaths;
            }

            set
            {
                _InaccessiblePaths = (value);
            }
        }

        private ulong _MountFlags = default (ulong);
        public ulong MountFlags
        {
            get
            {
                return _MountFlags;
            }

            set
            {
                _MountFlags = (value);
            }
        }

        private bool _PrivateTmp = default (bool);
        public bool PrivateTmp
        {
            get
            {
                return _PrivateTmp;
            }

            set
            {
                _PrivateTmp = (value);
            }
        }

        private bool _PrivateNetwork = default (bool);
        public bool PrivateNetwork
        {
            get
            {
                return _PrivateNetwork;
            }

            set
            {
                _PrivateNetwork = (value);
            }
        }

        private bool _PrivateDevices = default (bool);
        public bool PrivateDevices
        {
            get
            {
                return _PrivateDevices;
            }

            set
            {
                _PrivateDevices = (value);
            }
        }

        private string _ProtectHome = default (string);
        public string ProtectHome
        {
            get
            {
                return _ProtectHome;
            }

            set
            {
                _ProtectHome = (value);
            }
        }

        private string _ProtectSystem = default (string);
        public string ProtectSystem
        {
            get
            {
                return _ProtectSystem;
            }

            set
            {
                _ProtectSystem = (value);
            }
        }

        private bool _SameProcessGroup = default (bool);
        public bool SameProcessGroup
        {
            get
            {
                return _SameProcessGroup;
            }

            set
            {
                _SameProcessGroup = (value);
            }
        }

        private string _UtmpIdentifier = default (string);
        public string UtmpIdentifier
        {
            get
            {
                return _UtmpIdentifier;
            }

            set
            {
                _UtmpIdentifier = (value);
            }
        }

        private string _UtmpMode = default (string);
        public string UtmpMode
        {
            get
            {
                return _UtmpMode;
            }

            set
            {
                _UtmpMode = (value);
            }
        }

        private (bool, string)_SELinuxContext = default ((bool, string));
        public (bool, string)SELinuxContext
        {
            get
            {
                return _SELinuxContext;
            }

            set
            {
                _SELinuxContext = (value);
            }
        }

        private (bool, string)_AppArmorProfile = default ((bool, string));
        public (bool, string)AppArmorProfile
        {
            get
            {
                return _AppArmorProfile;
            }

            set
            {
                _AppArmorProfile = (value);
            }
        }

        private (bool, string)_SmackProcessLabel = default ((bool, string));
        public (bool, string)SmackProcessLabel
        {
            get
            {
                return _SmackProcessLabel;
            }

            set
            {
                _SmackProcessLabel = (value);
            }
        }

        private bool _IgnoreSIGPIPE = default (bool);
        public bool IgnoreSIGPIPE
        {
            get
            {
                return _IgnoreSIGPIPE;
            }

            set
            {
                _IgnoreSIGPIPE = (value);
            }
        }

        private bool _NoNewPrivileges = default (bool);
        public bool NoNewPrivileges
        {
            get
            {
                return _NoNewPrivileges;
            }

            set
            {
                _NoNewPrivileges = (value);
            }
        }

        private (bool, string[])_SystemCallFilter = default ((bool, string[]));
        public (bool, string[])SystemCallFilter
        {
            get
            {
                return _SystemCallFilter;
            }

            set
            {
                _SystemCallFilter = (value);
            }
        }

        private string[] _SystemCallArchitectures = default (string[]);
        public string[] SystemCallArchitectures
        {
            get
            {
                return _SystemCallArchitectures;
            }

            set
            {
                _SystemCallArchitectures = (value);
            }
        }

        private int _SystemCallErrorNumber = default (int);
        public int SystemCallErrorNumber
        {
            get
            {
                return _SystemCallErrorNumber;
            }

            set
            {
                _SystemCallErrorNumber = (value);
            }
        }

        private string _Personality = default (string);
        public string Personality
        {
            get
            {
                return _Personality;
            }

            set
            {
                _Personality = (value);
            }
        }

        private (bool, string[])_RestrictAddressFamilies = default ((bool, string[]));
        public (bool, string[])RestrictAddressFamilies
        {
            get
            {
                return _RestrictAddressFamilies;
            }

            set
            {
                _RestrictAddressFamilies = (value);
            }
        }

        private uint _RuntimeDirectoryMode = default (uint);
        public uint RuntimeDirectoryMode
        {
            get
            {
                return _RuntimeDirectoryMode;
            }

            set
            {
                _RuntimeDirectoryMode = (value);
            }
        }

        private string[] _RuntimeDirectory = default (string[]);
        public string[] RuntimeDirectory
        {
            get
            {
                return _RuntimeDirectory;
            }

            set
            {
                _RuntimeDirectory = (value);
            }
        }

        private bool _MemoryDenyWriteExecute = default (bool);
        public bool MemoryDenyWriteExecute
        {
            get
            {
                return _MemoryDenyWriteExecute;
            }

            set
            {
                _MemoryDenyWriteExecute = (value);
            }
        }

        private bool _RestrictRealtime = default (bool);
        public bool RestrictRealtime
        {
            get
            {
                return _RestrictRealtime;
            }

            set
            {
                _RestrictRealtime = (value);
            }
        }

        private string _KillMode = default (string);
        public string KillMode
        {
            get
            {
                return _KillMode;
            }

            set
            {
                _KillMode = (value);
            }
        }

        private int _KillSignal = default (int);
        public int KillSignal
        {
            get
            {
                return _KillSignal;
            }

            set
            {
                _KillSignal = (value);
            }
        }

        private bool _SendSIGKILL = default (bool);
        public bool SendSIGKILL
        {
            get
            {
                return _SendSIGKILL;
            }

            set
            {
                _SendSIGKILL = (value);
            }
        }

        private bool _SendSIGHUP = default (bool);
        public bool SendSIGHUP
        {
            get
            {
                return _SendSIGHUP;
            }

            set
            {
                _SendSIGHUP = (value);
            }
        }
    }

    static class SocketExtensions
    {
        public static Task<string> GetBindIPv6OnlyAsync(this ISocket o) => o.GetAsync<string>("BindIPv6Only");
        public static Task<uint> GetBacklogAsync(this ISocket o) => o.GetAsync<uint>("Backlog");
        public static Task<ulong> GetTimeoutUSecAsync(this ISocket o) => o.GetAsync<ulong>("TimeoutUSec");
        public static Task<string> GetBindToDeviceAsync(this ISocket o) => o.GetAsync<string>("BindToDevice");
        public static Task<string> GetSocketUserAsync(this ISocket o) => o.GetAsync<string>("SocketUser");
        public static Task<string> GetSocketGroupAsync(this ISocket o) => o.GetAsync<string>("SocketGroup");
        public static Task<uint> GetSocketModeAsync(this ISocket o) => o.GetAsync<uint>("SocketMode");
        public static Task<uint> GetDirectoryModeAsync(this ISocket o) => o.GetAsync<uint>("DirectoryMode");
        public static Task<bool> GetAcceptAsync(this ISocket o) => o.GetAsync<bool>("Accept");
        public static Task<bool> GetWritableAsync(this ISocket o) => o.GetAsync<bool>("Writable");
        public static Task<bool> GetKeepAliveAsync(this ISocket o) => o.GetAsync<bool>("KeepAlive");
        public static Task<ulong> GetKeepAliveTimeUSecAsync(this ISocket o) => o.GetAsync<ulong>("KeepAliveTimeUSec");
        public static Task<ulong> GetKeepAliveIntervalUSecAsync(this ISocket o) => o.GetAsync<ulong>("KeepAliveIntervalUSec");
        public static Task<uint> GetKeepAliveProbesAsync(this ISocket o) => o.GetAsync<uint>("KeepAliveProbes");
        public static Task<ulong> GetDeferAcceptUSecAsync(this ISocket o) => o.GetAsync<ulong>("DeferAcceptUSec");
        public static Task<bool> GetNoDelayAsync(this ISocket o) => o.GetAsync<bool>("NoDelay");
        public static Task<int> GetPriorityAsync(this ISocket o) => o.GetAsync<int>("Priority");
        public static Task<ulong> GetReceiveBufferAsync(this ISocket o) => o.GetAsync<ulong>("ReceiveBuffer");
        public static Task<ulong> GetSendBufferAsync(this ISocket o) => o.GetAsync<ulong>("SendBuffer");
        public static Task<int> GetIPTOSAsync(this ISocket o) => o.GetAsync<int>("IPTOS");
        public static Task<int> GetIPTTLAsync(this ISocket o) => o.GetAsync<int>("IPTTL");
        public static Task<ulong> GetPipeSizeAsync(this ISocket o) => o.GetAsync<ulong>("PipeSize");
        public static Task<bool> GetFreeBindAsync(this ISocket o) => o.GetAsync<bool>("FreeBind");
        public static Task<bool> GetTransparentAsync(this ISocket o) => o.GetAsync<bool>("Transparent");
        public static Task<bool> GetBroadcastAsync(this ISocket o) => o.GetAsync<bool>("Broadcast");
        public static Task<bool> GetPassCredentialsAsync(this ISocket o) => o.GetAsync<bool>("PassCredentials");
        public static Task<bool> GetPassSecurityAsync(this ISocket o) => o.GetAsync<bool>("PassSecurity");
        public static Task<bool> GetRemoveOnStopAsync(this ISocket o) => o.GetAsync<bool>("RemoveOnStop");
        public static Task<(string, string)[]> GetListenAsync(this ISocket o) => o.GetAsync<(string, string)[]>("Listen");
        public static Task<string[]> GetSymlinksAsync(this ISocket o) => o.GetAsync<string[]>("Symlinks");
        public static Task<int> GetMarkAsync(this ISocket o) => o.GetAsync<int>("Mark");
        public static Task<uint> GetMaxConnectionsAsync(this ISocket o) => o.GetAsync<uint>("MaxConnections");
        public static Task<long> GetMessageQueueMaxMessagesAsync(this ISocket o) => o.GetAsync<long>("MessageQueueMaxMessages");
        public static Task<long> GetMessageQueueMessageSizeAsync(this ISocket o) => o.GetAsync<long>("MessageQueueMessageSize");
        public static Task<bool> GetReusePortAsync(this ISocket o) => o.GetAsync<bool>("ReusePort");
        public static Task<string> GetSmackLabelAsync(this ISocket o) => o.GetAsync<string>("SmackLabel");
        public static Task<string> GetSmackLabelIPInAsync(this ISocket o) => o.GetAsync<string>("SmackLabelIPIn");
        public static Task<string> GetSmackLabelIPOutAsync(this ISocket o) => o.GetAsync<string>("SmackLabelIPOut");
        public static Task<uint> GetControlPIDAsync(this ISocket o) => o.GetAsync<uint>("ControlPID");
        public static Task<string> GetResultAsync(this ISocket o) => o.GetAsync<string>("Result");
        public static Task<uint> GetNConnectionsAsync(this ISocket o) => o.GetAsync<uint>("NConnections");
        public static Task<uint> GetNAcceptedAsync(this ISocket o) => o.GetAsync<uint>("NAccepted");
        public static Task<string> GetFileDescriptorNameAsync(this ISocket o) => o.GetAsync<string>("FileDescriptorName");
        public static Task<int> GetSocketProtocolAsync(this ISocket o) => o.GetAsync<int>("SocketProtocol");
        public static Task<ulong> GetTriggerLimitIntervalUSecAsync(this ISocket o) => o.GetAsync<ulong>("TriggerLimitIntervalUSec");
        public static Task<uint> GetTriggerLimitBurstAsync(this ISocket o) => o.GetAsync<uint>("TriggerLimitBurst");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStartPreAsync(this ISocket o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStartPre");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStartPostAsync(this ISocket o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStartPost");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStopPreAsync(this ISocket o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStopPre");
        public static Task<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]> GetExecStopPostAsync(this ISocket o) => o.GetAsync<(string, string[], bool, ulong, ulong, ulong, ulong, uint, int, int)[]>("ExecStopPost");
        public static Task<string> GetSliceAsync(this ISocket o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this ISocket o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this ISocket o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this ISocket o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this ISocket o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this ISocket o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this ISocket o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this ISocket o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this ISocket o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this ISocket o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this ISocket o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this ISocket o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this ISocket o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this ISocket o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this ISocket o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this ISocket o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this ISocket o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this ISocket o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this ISocket o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this ISocket o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this ISocket o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this ISocket o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this ISocket o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this ISocket o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this ISocket o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this ISocket o) => o.GetAsync<ulong>("TasksMax");
        public static Task<string[]> GetEnvironmentAsync(this ISocket o) => o.GetAsync<string[]>("Environment");
        public static Task<(string, bool)[]> GetEnvironmentFilesAsync(this ISocket o) => o.GetAsync<(string, bool)[]>("EnvironmentFiles");
        public static Task<string[]> GetPassEnvironmentAsync(this ISocket o) => o.GetAsync<string[]>("PassEnvironment");
        public static Task<uint> GetUMaskAsync(this ISocket o) => o.GetAsync<uint>("UMask");
        public static Task<ulong> GetLimitCPUAsync(this ISocket o) => o.GetAsync<ulong>("LimitCPU");
        public static Task<ulong> GetLimitCPUSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitCPUSoft");
        public static Task<ulong> GetLimitFSIZEAsync(this ISocket o) => o.GetAsync<ulong>("LimitFSIZE");
        public static Task<ulong> GetLimitFSIZESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitFSIZESoft");
        public static Task<ulong> GetLimitDATAAsync(this ISocket o) => o.GetAsync<ulong>("LimitDATA");
        public static Task<ulong> GetLimitDATASoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitDATASoft");
        public static Task<ulong> GetLimitSTACKAsync(this ISocket o) => o.GetAsync<ulong>("LimitSTACK");
        public static Task<ulong> GetLimitSTACKSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitSTACKSoft");
        public static Task<ulong> GetLimitCOREAsync(this ISocket o) => o.GetAsync<ulong>("LimitCORE");
        public static Task<ulong> GetLimitCORESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitCORESoft");
        public static Task<ulong> GetLimitRSSAsync(this ISocket o) => o.GetAsync<ulong>("LimitRSS");
        public static Task<ulong> GetLimitRSSSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitRSSSoft");
        public static Task<ulong> GetLimitNOFILEAsync(this ISocket o) => o.GetAsync<ulong>("LimitNOFILE");
        public static Task<ulong> GetLimitNOFILESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitNOFILESoft");
        public static Task<ulong> GetLimitASAsync(this ISocket o) => o.GetAsync<ulong>("LimitAS");
        public static Task<ulong> GetLimitASSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitASSoft");
        public static Task<ulong> GetLimitNPROCAsync(this ISocket o) => o.GetAsync<ulong>("LimitNPROC");
        public static Task<ulong> GetLimitNPROCSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitNPROCSoft");
        public static Task<ulong> GetLimitMEMLOCKAsync(this ISocket o) => o.GetAsync<ulong>("LimitMEMLOCK");
        public static Task<ulong> GetLimitMEMLOCKSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitMEMLOCKSoft");
        public static Task<ulong> GetLimitLOCKSAsync(this ISocket o) => o.GetAsync<ulong>("LimitLOCKS");
        public static Task<ulong> GetLimitLOCKSSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitLOCKSSoft");
        public static Task<ulong> GetLimitSIGPENDINGAsync(this ISocket o) => o.GetAsync<ulong>("LimitSIGPENDING");
        public static Task<ulong> GetLimitSIGPENDINGSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitSIGPENDINGSoft");
        public static Task<ulong> GetLimitMSGQUEUEAsync(this ISocket o) => o.GetAsync<ulong>("LimitMSGQUEUE");
        public static Task<ulong> GetLimitMSGQUEUESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitMSGQUEUESoft");
        public static Task<ulong> GetLimitNICEAsync(this ISocket o) => o.GetAsync<ulong>("LimitNICE");
        public static Task<ulong> GetLimitNICESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitNICESoft");
        public static Task<ulong> GetLimitRTPRIOAsync(this ISocket o) => o.GetAsync<ulong>("LimitRTPRIO");
        public static Task<ulong> GetLimitRTPRIOSoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitRTPRIOSoft");
        public static Task<ulong> GetLimitRTTIMEAsync(this ISocket o) => o.GetAsync<ulong>("LimitRTTIME");
        public static Task<ulong> GetLimitRTTIMESoftAsync(this ISocket o) => o.GetAsync<ulong>("LimitRTTIMESoft");
        public static Task<string> GetWorkingDirectoryAsync(this ISocket o) => o.GetAsync<string>("WorkingDirectory");
        public static Task<string> GetRootDirectoryAsync(this ISocket o) => o.GetAsync<string>("RootDirectory");
        public static Task<int> GetOOMScoreAdjustAsync(this ISocket o) => o.GetAsync<int>("OOMScoreAdjust");
        public static Task<int> GetNiceAsync(this ISocket o) => o.GetAsync<int>("Nice");
        public static Task<int> GetIOSchedulingAsync(this ISocket o) => o.GetAsync<int>("IOScheduling");
        public static Task<int> GetCPUSchedulingPolicyAsync(this ISocket o) => o.GetAsync<int>("CPUSchedulingPolicy");
        public static Task<int> GetCPUSchedulingPriorityAsync(this ISocket o) => o.GetAsync<int>("CPUSchedulingPriority");
        public static Task<byte[]> GetCPUAffinityAsync(this ISocket o) => o.GetAsync<byte[]>("CPUAffinity");
        public static Task<ulong> GetTimerSlackNSecAsync(this ISocket o) => o.GetAsync<ulong>("TimerSlackNSec");
        public static Task<bool> GetCPUSchedulingResetOnForkAsync(this ISocket o) => o.GetAsync<bool>("CPUSchedulingResetOnFork");
        public static Task<bool> GetNonBlockingAsync(this ISocket o) => o.GetAsync<bool>("NonBlocking");
        public static Task<string> GetStandardInputAsync(this ISocket o) => o.GetAsync<string>("StandardInput");
        public static Task<string> GetStandardOutputAsync(this ISocket o) => o.GetAsync<string>("StandardOutput");
        public static Task<string> GetStandardErrorAsync(this ISocket o) => o.GetAsync<string>("StandardError");
        public static Task<string> GetTTYPathAsync(this ISocket o) => o.GetAsync<string>("TTYPath");
        public static Task<bool> GetTTYResetAsync(this ISocket o) => o.GetAsync<bool>("TTYReset");
        public static Task<bool> GetTTYVHangupAsync(this ISocket o) => o.GetAsync<bool>("TTYVHangup");
        public static Task<bool> GetTTYVTDisallocateAsync(this ISocket o) => o.GetAsync<bool>("TTYVTDisallocate");
        public static Task<int> GetSyslogPriorityAsync(this ISocket o) => o.GetAsync<int>("SyslogPriority");
        public static Task<string> GetSyslogIdentifierAsync(this ISocket o) => o.GetAsync<string>("SyslogIdentifier");
        public static Task<bool> GetSyslogLevelPrefixAsync(this ISocket o) => o.GetAsync<bool>("SyslogLevelPrefix");
        public static Task<int> GetSyslogLevelAsync(this ISocket o) => o.GetAsync<int>("SyslogLevel");
        public static Task<int> GetSyslogFacilityAsync(this ISocket o) => o.GetAsync<int>("SyslogFacility");
        public static Task<int> GetSecureBitsAsync(this ISocket o) => o.GetAsync<int>("SecureBits");
        public static Task<ulong> GetCapabilityBoundingSetAsync(this ISocket o) => o.GetAsync<ulong>("CapabilityBoundingSet");
        public static Task<ulong> GetAmbientCapabilitiesAsync(this ISocket o) => o.GetAsync<ulong>("AmbientCapabilities");
        public static Task<string> GetUserAsync(this ISocket o) => o.GetAsync<string>("User");
        public static Task<string> GetGroupAsync(this ISocket o) => o.GetAsync<string>("Group");
        public static Task<string[]> GetSupplementaryGroupsAsync(this ISocket o) => o.GetAsync<string[]>("SupplementaryGroups");
        public static Task<string> GetPAMNameAsync(this ISocket o) => o.GetAsync<string>("PAMName");
        public static Task<string[]> GetReadWritePathsAsync(this ISocket o) => o.GetAsync<string[]>("ReadWritePaths");
        public static Task<string[]> GetReadOnlyPathsAsync(this ISocket o) => o.GetAsync<string[]>("ReadOnlyPaths");
        public static Task<string[]> GetInaccessiblePathsAsync(this ISocket o) => o.GetAsync<string[]>("InaccessiblePaths");
        public static Task<ulong> GetMountFlagsAsync(this ISocket o) => o.GetAsync<ulong>("MountFlags");
        public static Task<bool> GetPrivateTmpAsync(this ISocket o) => o.GetAsync<bool>("PrivateTmp");
        public static Task<bool> GetPrivateNetworkAsync(this ISocket o) => o.GetAsync<bool>("PrivateNetwork");
        public static Task<bool> GetPrivateDevicesAsync(this ISocket o) => o.GetAsync<bool>("PrivateDevices");
        public static Task<string> GetProtectHomeAsync(this ISocket o) => o.GetAsync<string>("ProtectHome");
        public static Task<string> GetProtectSystemAsync(this ISocket o) => o.GetAsync<string>("ProtectSystem");
        public static Task<bool> GetSameProcessGroupAsync(this ISocket o) => o.GetAsync<bool>("SameProcessGroup");
        public static Task<string> GetUtmpIdentifierAsync(this ISocket o) => o.GetAsync<string>("UtmpIdentifier");
        public static Task<string> GetUtmpModeAsync(this ISocket o) => o.GetAsync<string>("UtmpMode");
        public static Task<(bool, string)> GetSELinuxContextAsync(this ISocket o) => o.GetAsync<(bool, string)>("SELinuxContext");
        public static Task<(bool, string)> GetAppArmorProfileAsync(this ISocket o) => o.GetAsync<(bool, string)>("AppArmorProfile");
        public static Task<(bool, string)> GetSmackProcessLabelAsync(this ISocket o) => o.GetAsync<(bool, string)>("SmackProcessLabel");
        public static Task<bool> GetIgnoreSIGPIPEAsync(this ISocket o) => o.GetAsync<bool>("IgnoreSIGPIPE");
        public static Task<bool> GetNoNewPrivilegesAsync(this ISocket o) => o.GetAsync<bool>("NoNewPrivileges");
        public static Task<(bool, string[])> GetSystemCallFilterAsync(this ISocket o) => o.GetAsync<(bool, string[])>("SystemCallFilter");
        public static Task<string[]> GetSystemCallArchitecturesAsync(this ISocket o) => o.GetAsync<string[]>("SystemCallArchitectures");
        public static Task<int> GetSystemCallErrorNumberAsync(this ISocket o) => o.GetAsync<int>("SystemCallErrorNumber");
        public static Task<string> GetPersonalityAsync(this ISocket o) => o.GetAsync<string>("Personality");
        public static Task<(bool, string[])> GetRestrictAddressFamiliesAsync(this ISocket o) => o.GetAsync<(bool, string[])>("RestrictAddressFamilies");
        public static Task<uint> GetRuntimeDirectoryModeAsync(this ISocket o) => o.GetAsync<uint>("RuntimeDirectoryMode");
        public static Task<string[]> GetRuntimeDirectoryAsync(this ISocket o) => o.GetAsync<string[]>("RuntimeDirectory");
        public static Task<bool> GetMemoryDenyWriteExecuteAsync(this ISocket o) => o.GetAsync<bool>("MemoryDenyWriteExecute");
        public static Task<bool> GetRestrictRealtimeAsync(this ISocket o) => o.GetAsync<bool>("RestrictRealtime");
        public static Task<string> GetKillModeAsync(this ISocket o) => o.GetAsync<string>("KillMode");
        public static Task<int> GetKillSignalAsync(this ISocket o) => o.GetAsync<int>("KillSignal");
        public static Task<bool> GetSendSIGKILLAsync(this ISocket o) => o.GetAsync<bool>("SendSIGKILL");
        public static Task<bool> GetSendSIGHUPAsync(this ISocket o) => o.GetAsync<bool>("SendSIGHUP");
    }

    [DBusInterface("org.freedesktop.systemd1.Slice")]
    interface ISlice : IDBusObject
    {
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<SliceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SliceProperties
    {
        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }
    }

    static class SliceExtensions
    {
        public static Task<string> GetSliceAsync(this ISlice o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this ISlice o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this ISlice o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this ISlice o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this ISlice o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this ISlice o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this ISlice o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this ISlice o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this ISlice o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this ISlice o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this ISlice o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this ISlice o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this ISlice o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this ISlice o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this ISlice o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this ISlice o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this ISlice o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this ISlice o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this ISlice o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this ISlice o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this ISlice o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this ISlice o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this ISlice o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this ISlice o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this ISlice o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this ISlice o) => o.GetAsync<ulong>("TasksMax");
    }

    [DBusInterface("org.freedesktop.systemd1.Automount")]
    interface IAutomount : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<AutomountProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class AutomountProperties
    {
        private string _Where = default (string);
        public string Where
        {
            get
            {
                return _Where;
            }

            set
            {
                _Where = (value);
            }
        }

        private uint _DirectoryMode = default (uint);
        public uint DirectoryMode
        {
            get
            {
                return _DirectoryMode;
            }

            set
            {
                _DirectoryMode = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private ulong _TimeoutIdleUSec = default (ulong);
        public ulong TimeoutIdleUSec
        {
            get
            {
                return _TimeoutIdleUSec;
            }

            set
            {
                _TimeoutIdleUSec = (value);
            }
        }
    }

    static class AutomountExtensions
    {
        public static Task<string> GetWhereAsync(this IAutomount o) => o.GetAsync<string>("Where");
        public static Task<uint> GetDirectoryModeAsync(this IAutomount o) => o.GetAsync<uint>("DirectoryMode");
        public static Task<string> GetResultAsync(this IAutomount o) => o.GetAsync<string>("Result");
        public static Task<ulong> GetTimeoutIdleUSecAsync(this IAutomount o) => o.GetAsync<ulong>("TimeoutIdleUSec");
    }

    [DBusInterface("org.freedesktop.systemd1.Scope")]
    interface IScope : IDBusObject
    {
        Task AbandonAsync();
        Task<(string, uint, string)[]> GetProcessesAsync();
        Task<IDisposable> WatchRequestStopAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ScopeProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ScopeProperties
    {
        private string _Controller = default (string);
        public string Controller
        {
            get
            {
                return _Controller;
            }

            set
            {
                _Controller = (value);
            }
        }

        private ulong _TimeoutStopUSec = default (ulong);
        public ulong TimeoutStopUSec
        {
            get
            {
                return _TimeoutStopUSec;
            }

            set
            {
                _TimeoutStopUSec = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private string _Slice = default (string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private string _ControlGroup = default (string);
        public string ControlGroup
        {
            get
            {
                return _ControlGroup;
            }

            set
            {
                _ControlGroup = (value);
            }
        }

        private ulong _MemoryCurrent = default (ulong);
        public ulong MemoryCurrent
        {
            get
            {
                return _MemoryCurrent;
            }

            set
            {
                _MemoryCurrent = (value);
            }
        }

        private ulong _CPUUsageNSec = default (ulong);
        public ulong CPUUsageNSec
        {
            get
            {
                return _CPUUsageNSec;
            }

            set
            {
                _CPUUsageNSec = (value);
            }
        }

        private ulong _TasksCurrent = default (ulong);
        public ulong TasksCurrent
        {
            get
            {
                return _TasksCurrent;
            }

            set
            {
                _TasksCurrent = (value);
            }
        }

        private bool _Delegate = default (bool);
        public bool Delegate
        {
            get
            {
                return _Delegate;
            }

            set
            {
                _Delegate = (value);
            }
        }

        private bool _CPUAccounting = default (bool);
        public bool CPUAccounting
        {
            get
            {
                return _CPUAccounting;
            }

            set
            {
                _CPUAccounting = (value);
            }
        }

        private ulong _CPUShares = default (ulong);
        public ulong CPUShares
        {
            get
            {
                return _CPUShares;
            }

            set
            {
                _CPUShares = (value);
            }
        }

        private ulong _StartupCPUShares = default (ulong);
        public ulong StartupCPUShares
        {
            get
            {
                return _StartupCPUShares;
            }

            set
            {
                _StartupCPUShares = (value);
            }
        }

        private ulong _CPUQuotaPerSecUSec = default (ulong);
        public ulong CPUQuotaPerSecUSec
        {
            get
            {
                return _CPUQuotaPerSecUSec;
            }

            set
            {
                _CPUQuotaPerSecUSec = (value);
            }
        }

        private bool _IOAccounting = default (bool);
        public bool IOAccounting
        {
            get
            {
                return _IOAccounting;
            }

            set
            {
                _IOAccounting = (value);
            }
        }

        private ulong _IOWeight = default (ulong);
        public ulong IOWeight
        {
            get
            {
                return _IOWeight;
            }

            set
            {
                _IOWeight = (value);
            }
        }

        private ulong _StartupIOWeight = default (ulong);
        public ulong StartupIOWeight
        {
            get
            {
                return _StartupIOWeight;
            }

            set
            {
                _StartupIOWeight = (value);
            }
        }

        private (string, ulong)[] _IODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] IODeviceWeight
        {
            get
            {
                return _IODeviceWeight;
            }

            set
            {
                _IODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _IOReadBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadBandwidthMax
        {
            get
            {
                return _IOReadBandwidthMax;
            }

            set
            {
                _IOReadBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteBandwidthMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteBandwidthMax
        {
            get
            {
                return _IOWriteBandwidthMax;
            }

            set
            {
                _IOWriteBandwidthMax = (value);
            }
        }

        private (string, ulong)[] _IOReadIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOReadIOPSMax
        {
            get
            {
                return _IOReadIOPSMax;
            }

            set
            {
                _IOReadIOPSMax = (value);
            }
        }

        private (string, ulong)[] _IOWriteIOPSMax = default ((string, ulong)[]);
        public (string, ulong)[] IOWriteIOPSMax
        {
            get
            {
                return _IOWriteIOPSMax;
            }

            set
            {
                _IOWriteIOPSMax = (value);
            }
        }

        private bool _BlockIOAccounting = default (bool);
        public bool BlockIOAccounting
        {
            get
            {
                return _BlockIOAccounting;
            }

            set
            {
                _BlockIOAccounting = (value);
            }
        }

        private ulong _BlockIOWeight = default (ulong);
        public ulong BlockIOWeight
        {
            get
            {
                return _BlockIOWeight;
            }

            set
            {
                _BlockIOWeight = (value);
            }
        }

        private ulong _StartupBlockIOWeight = default (ulong);
        public ulong StartupBlockIOWeight
        {
            get
            {
                return _StartupBlockIOWeight;
            }

            set
            {
                _StartupBlockIOWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIODeviceWeight = default ((string, ulong)[]);
        public (string, ulong)[] BlockIODeviceWeight
        {
            get
            {
                return _BlockIODeviceWeight;
            }

            set
            {
                _BlockIODeviceWeight = (value);
            }
        }

        private (string, ulong)[] _BlockIOReadBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOReadBandwidth
        {
            get
            {
                return _BlockIOReadBandwidth;
            }

            set
            {
                _BlockIOReadBandwidth = (value);
            }
        }

        private (string, ulong)[] _BlockIOWriteBandwidth = default ((string, ulong)[]);
        public (string, ulong)[] BlockIOWriteBandwidth
        {
            get
            {
                return _BlockIOWriteBandwidth;
            }

            set
            {
                _BlockIOWriteBandwidth = (value);
            }
        }

        private bool _MemoryAccounting = default (bool);
        public bool MemoryAccounting
        {
            get
            {
                return _MemoryAccounting;
            }

            set
            {
                _MemoryAccounting = (value);
            }
        }

        private ulong _MemoryLow = default (ulong);
        public ulong MemoryLow
        {
            get
            {
                return _MemoryLow;
            }

            set
            {
                _MemoryLow = (value);
            }
        }

        private ulong _MemoryHigh = default (ulong);
        public ulong MemoryHigh
        {
            get
            {
                return _MemoryHigh;
            }

            set
            {
                _MemoryHigh = (value);
            }
        }

        private ulong _MemoryMax = default (ulong);
        public ulong MemoryMax
        {
            get
            {
                return _MemoryMax;
            }

            set
            {
                _MemoryMax = (value);
            }
        }

        private ulong _MemoryLimit = default (ulong);
        public ulong MemoryLimit
        {
            get
            {
                return _MemoryLimit;
            }

            set
            {
                _MemoryLimit = (value);
            }
        }

        private string _DevicePolicy = default (string);
        public string DevicePolicy
        {
            get
            {
                return _DevicePolicy;
            }

            set
            {
                _DevicePolicy = (value);
            }
        }

        private (string, string)[] _DeviceAllow = default ((string, string)[]);
        public (string, string)[] DeviceAllow
        {
            get
            {
                return _DeviceAllow;
            }

            set
            {
                _DeviceAllow = (value);
            }
        }

        private bool _TasksAccounting = default (bool);
        public bool TasksAccounting
        {
            get
            {
                return _TasksAccounting;
            }

            set
            {
                _TasksAccounting = (value);
            }
        }

        private ulong _TasksMax = default (ulong);
        public ulong TasksMax
        {
            get
            {
                return _TasksMax;
            }

            set
            {
                _TasksMax = (value);
            }
        }

        private string _KillMode = default (string);
        public string KillMode
        {
            get
            {
                return _KillMode;
            }

            set
            {
                _KillMode = (value);
            }
        }

        private int _KillSignal = default (int);
        public int KillSignal
        {
            get
            {
                return _KillSignal;
            }

            set
            {
                _KillSignal = (value);
            }
        }

        private bool _SendSIGKILL = default (bool);
        public bool SendSIGKILL
        {
            get
            {
                return _SendSIGKILL;
            }

            set
            {
                _SendSIGKILL = (value);
            }
        }

        private bool _SendSIGHUP = default (bool);
        public bool SendSIGHUP
        {
            get
            {
                return _SendSIGHUP;
            }

            set
            {
                _SendSIGHUP = (value);
            }
        }
    }

    static class ScopeExtensions
    {
        public static Task<string> GetControllerAsync(this IScope o) => o.GetAsync<string>("Controller");
        public static Task<ulong> GetTimeoutStopUSecAsync(this IScope o) => o.GetAsync<ulong>("TimeoutStopUSec");
        public static Task<string> GetResultAsync(this IScope o) => o.GetAsync<string>("Result");
        public static Task<string> GetSliceAsync(this IScope o) => o.GetAsync<string>("Slice");
        public static Task<string> GetControlGroupAsync(this IScope o) => o.GetAsync<string>("ControlGroup");
        public static Task<ulong> GetMemoryCurrentAsync(this IScope o) => o.GetAsync<ulong>("MemoryCurrent");
        public static Task<ulong> GetCPUUsageNSecAsync(this IScope o) => o.GetAsync<ulong>("CPUUsageNSec");
        public static Task<ulong> GetTasksCurrentAsync(this IScope o) => o.GetAsync<ulong>("TasksCurrent");
        public static Task<bool> GetDelegateAsync(this IScope o) => o.GetAsync<bool>("Delegate");
        public static Task<bool> GetCPUAccountingAsync(this IScope o) => o.GetAsync<bool>("CPUAccounting");
        public static Task<ulong> GetCPUSharesAsync(this IScope o) => o.GetAsync<ulong>("CPUShares");
        public static Task<ulong> GetStartupCPUSharesAsync(this IScope o) => o.GetAsync<ulong>("StartupCPUShares");
        public static Task<ulong> GetCPUQuotaPerSecUSecAsync(this IScope o) => o.GetAsync<ulong>("CPUQuotaPerSecUSec");
        public static Task<bool> GetIOAccountingAsync(this IScope o) => o.GetAsync<bool>("IOAccounting");
        public static Task<ulong> GetIOWeightAsync(this IScope o) => o.GetAsync<ulong>("IOWeight");
        public static Task<ulong> GetStartupIOWeightAsync(this IScope o) => o.GetAsync<ulong>("StartupIOWeight");
        public static Task<(string, ulong)[]> GetIODeviceWeightAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("IODeviceWeight");
        public static Task<(string, ulong)[]> GetIOReadBandwidthMaxAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("IOReadBandwidthMax");
        public static Task<(string, ulong)[]> GetIOWriteBandwidthMaxAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("IOWriteBandwidthMax");
        public static Task<(string, ulong)[]> GetIOReadIOPSMaxAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("IOReadIOPSMax");
        public static Task<(string, ulong)[]> GetIOWriteIOPSMaxAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("IOWriteIOPSMax");
        public static Task<bool> GetBlockIOAccountingAsync(this IScope o) => o.GetAsync<bool>("BlockIOAccounting");
        public static Task<ulong> GetBlockIOWeightAsync(this IScope o) => o.GetAsync<ulong>("BlockIOWeight");
        public static Task<ulong> GetStartupBlockIOWeightAsync(this IScope o) => o.GetAsync<ulong>("StartupBlockIOWeight");
        public static Task<(string, ulong)[]> GetBlockIODeviceWeightAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("BlockIODeviceWeight");
        public static Task<(string, ulong)[]> GetBlockIOReadBandwidthAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("BlockIOReadBandwidth");
        public static Task<(string, ulong)[]> GetBlockIOWriteBandwidthAsync(this IScope o) => o.GetAsync<(string, ulong)[]>("BlockIOWriteBandwidth");
        public static Task<bool> GetMemoryAccountingAsync(this IScope o) => o.GetAsync<bool>("MemoryAccounting");
        public static Task<ulong> GetMemoryLowAsync(this IScope o) => o.GetAsync<ulong>("MemoryLow");
        public static Task<ulong> GetMemoryHighAsync(this IScope o) => o.GetAsync<ulong>("MemoryHigh");
        public static Task<ulong> GetMemoryMaxAsync(this IScope o) => o.GetAsync<ulong>("MemoryMax");
        public static Task<ulong> GetMemoryLimitAsync(this IScope o) => o.GetAsync<ulong>("MemoryLimit");
        public static Task<string> GetDevicePolicyAsync(this IScope o) => o.GetAsync<string>("DevicePolicy");
        public static Task<(string, string)[]> GetDeviceAllowAsync(this IScope o) => o.GetAsync<(string, string)[]>("DeviceAllow");
        public static Task<bool> GetTasksAccountingAsync(this IScope o) => o.GetAsync<bool>("TasksAccounting");
        public static Task<ulong> GetTasksMaxAsync(this IScope o) => o.GetAsync<ulong>("TasksMax");
        public static Task<string> GetKillModeAsync(this IScope o) => o.GetAsync<string>("KillMode");
        public static Task<int> GetKillSignalAsync(this IScope o) => o.GetAsync<int>("KillSignal");
        public static Task<bool> GetSendSIGKILLAsync(this IScope o) => o.GetAsync<bool>("SendSIGKILL");
        public static Task<bool> GetSendSIGHUPAsync(this IScope o) => o.GetAsync<bool>("SendSIGHUP");
    }

    [DBusInterface("org.freedesktop.systemd1.Timer")]
    interface ITimer : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<TimerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class TimerProperties
    {
        private string _Unit = default (string);
        public string Unit
        {
            get
            {
                return _Unit;
            }

            set
            {
                _Unit = (value);
            }
        }

        private (string, ulong, ulong)[] _TimersMonotonic = default ((string, ulong, ulong)[]);
        public (string, ulong, ulong)[] TimersMonotonic
        {
            get
            {
                return _TimersMonotonic;
            }

            set
            {
                _TimersMonotonic = (value);
            }
        }

        private (string, string, ulong)[] _TimersCalendar = default ((string, string, ulong)[]);
        public (string, string, ulong)[] TimersCalendar
        {
            get
            {
                return _TimersCalendar;
            }

            set
            {
                _TimersCalendar = (value);
            }
        }

        private ulong _NextElapseUSecRealtime = default (ulong);
        public ulong NextElapseUSecRealtime
        {
            get
            {
                return _NextElapseUSecRealtime;
            }

            set
            {
                _NextElapseUSecRealtime = (value);
            }
        }

        private ulong _NextElapseUSecMonotonic = default (ulong);
        public ulong NextElapseUSecMonotonic
        {
            get
            {
                return _NextElapseUSecMonotonic;
            }

            set
            {
                _NextElapseUSecMonotonic = (value);
            }
        }

        private ulong _LastTriggerUSec = default (ulong);
        public ulong LastTriggerUSec
        {
            get
            {
                return _LastTriggerUSec;
            }

            set
            {
                _LastTriggerUSec = (value);
            }
        }

        private ulong _LastTriggerUSecMonotonic = default (ulong);
        public ulong LastTriggerUSecMonotonic
        {
            get
            {
                return _LastTriggerUSecMonotonic;
            }

            set
            {
                _LastTriggerUSecMonotonic = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }

        private ulong _AccuracyUSec = default (ulong);
        public ulong AccuracyUSec
        {
            get
            {
                return _AccuracyUSec;
            }

            set
            {
                _AccuracyUSec = (value);
            }
        }

        private ulong _RandomizedDelayUSec = default (ulong);
        public ulong RandomizedDelayUSec
        {
            get
            {
                return _RandomizedDelayUSec;
            }

            set
            {
                _RandomizedDelayUSec = (value);
            }
        }

        private bool _Persistent = default (bool);
        public bool Persistent
        {
            get
            {
                return _Persistent;
            }

            set
            {
                _Persistent = (value);
            }
        }

        private bool _WakeSystem = default (bool);
        public bool WakeSystem
        {
            get
            {
                return _WakeSystem;
            }

            set
            {
                _WakeSystem = (value);
            }
        }

        private bool _RemainAfterElapse = default (bool);
        public bool RemainAfterElapse
        {
            get
            {
                return _RemainAfterElapse;
            }

            set
            {
                _RemainAfterElapse = (value);
            }
        }
    }

    static class TimerExtensions
    {
        public static Task<string> GetUnitAsync(this ITimer o) => o.GetAsync<string>("Unit");
        public static Task<(string, ulong, ulong)[]> GetTimersMonotonicAsync(this ITimer o) => o.GetAsync<(string, ulong, ulong)[]>("TimersMonotonic");
        public static Task<(string, string, ulong)[]> GetTimersCalendarAsync(this ITimer o) => o.GetAsync<(string, string, ulong)[]>("TimersCalendar");
        public static Task<ulong> GetNextElapseUSecRealtimeAsync(this ITimer o) => o.GetAsync<ulong>("NextElapseUSecRealtime");
        public static Task<ulong> GetNextElapseUSecMonotonicAsync(this ITimer o) => o.GetAsync<ulong>("NextElapseUSecMonotonic");
        public static Task<ulong> GetLastTriggerUSecAsync(this ITimer o) => o.GetAsync<ulong>("LastTriggerUSec");
        public static Task<ulong> GetLastTriggerUSecMonotonicAsync(this ITimer o) => o.GetAsync<ulong>("LastTriggerUSecMonotonic");
        public static Task<string> GetResultAsync(this ITimer o) => o.GetAsync<string>("Result");
        public static Task<ulong> GetAccuracyUSecAsync(this ITimer o) => o.GetAsync<ulong>("AccuracyUSec");
        public static Task<ulong> GetRandomizedDelayUSecAsync(this ITimer o) => o.GetAsync<ulong>("RandomizedDelayUSec");
        public static Task<bool> GetPersistentAsync(this ITimer o) => o.GetAsync<bool>("Persistent");
        public static Task<bool> GetWakeSystemAsync(this ITimer o) => o.GetAsync<bool>("WakeSystem");
        public static Task<bool> GetRemainAfterElapseAsync(this ITimer o) => o.GetAsync<bool>("RemainAfterElapse");
    }

    [DBusInterface("org.freedesktop.systemd1.Path")]
    interface IPath : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<PathProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class PathProperties
    {
        private string _Unit = default (string);
        public string Unit
        {
            get
            {
                return _Unit;
            }

            set
            {
                _Unit = (value);
            }
        }

        private (string, string)[] _Paths = default ((string, string)[]);
        public (string, string)[] Paths
        {
            get
            {
                return _Paths;
            }

            set
            {
                _Paths = (value);
            }
        }

        private bool _MakeDirectory = default (bool);
        public bool MakeDirectory
        {
            get
            {
                return _MakeDirectory;
            }

            set
            {
                _MakeDirectory = (value);
            }
        }

        private uint _DirectoryMode = default (uint);
        public uint DirectoryMode
        {
            get
            {
                return _DirectoryMode;
            }

            set
            {
                _DirectoryMode = (value);
            }
        }

        private string _Result = default (string);
        public string Result
        {
            get
            {
                return _Result;
            }

            set
            {
                _Result = (value);
            }
        }
    }

    static class PathExtensions
    {
        public static Task<string> GetUnitAsync(this IPath o) => o.GetAsync<string>("Unit");
        public static Task<(string, string)[]> GetPathsAsync(this IPath o) => o.GetAsync<(string, string)[]>("Paths");
        public static Task<bool> GetMakeDirectoryAsync(this IPath o) => o.GetAsync<bool>("MakeDirectory");
        public static Task<uint> GetDirectoryModeAsync(this IPath o) => o.GetAsync<uint>("DirectoryMode");
        public static Task<string> GetResultAsync(this IPath o) => o.GetAsync<string>("Result");
    }
}