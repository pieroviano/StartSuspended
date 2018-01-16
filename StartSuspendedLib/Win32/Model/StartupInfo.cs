using System;

namespace StartSuspendedLib.Win32.Model
{
    public struct StartupInfo
    {
        public uint Cb;
        public string LpReserved;
        public string LpDesktop;
        public string LpTitle;
        public uint DwX;
        public uint DwY;
        public uint DwXSize;
        public uint DwYSize;
        public uint DwXCountChars;
        public uint DwYCountChars;
        public uint DwFillAttribute;
        public uint DwFlags;
        public short WShowWindow;
        public short CbReserved2;
        public IntPtr LpReserved2;
        public IntPtr HStdInput;
        public IntPtr HStdOutput;
        public IntPtr HStdError;
    }
}