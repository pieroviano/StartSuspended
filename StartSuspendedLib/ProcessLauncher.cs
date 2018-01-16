using System;
using System.Threading;
using StartSuspendedLib.Exceptions;
using StartSuspendedLib.Model;
using StartSuspendedLib.Win32;
using StartSuspendedLib.Win32.Model;

namespace StartSuspendedLib
{
    public class ProcessLauncher
    {
        private IntPtr _threadHandle = IntPtr.Zero;

        public int InitialResumeTime { get; set; }

        public void LaunchProcessInteractively(string filename)
        {
            var p = this;
            uint processId;
            var started = p.LaunchProcessSuspended(filename, InitialResumeTime, out processId);

            string okMessage;
            string title;
            if (started)
            {
                okMessage = $"The Process started with PID {processId}.\r\nClose this dialog to resume it.";
                title = "Process started";
            }
            else
            {
                okMessage = $"Error launching file {filename}";
                if (ProcessStarted == null)
                {
                    throw new ProcessLauncherException(okMessage);
                }
                title = "Error launching file";
            }
            var processLaunchedEventArgs = new ProcessLaunchedEventArgs(started, okMessage, title);
            ProcessStarted?.Invoke(this, processLaunchedEventArgs);

            p.ResumeProcess();
        }

        public bool LaunchProcessSuspended(string processpath, int initialResumeTime, out uint PID)
        {
            var si = new StartupInfo();
            var pi = new ProcessInformation();
            var success = NativeMethods.CreateProcess(processpath, null, IntPtr.Zero, IntPtr.Zero, false,
                ProcessCreationFlags.CreateSuspended, IntPtr.Zero, null, ref si, out pi);
            _threadHandle = pi.HThread;
            PID = pi.DwProcessId;

            if (initialResumeTime > 0)
            {
                NativeMethods.ResumeThread(_threadHandle);
                Thread.Sleep(initialResumeTime);
                NativeMethods.SuspendThread(_threadHandle);
            }

            return success;
        }

        public event EventHandler<ProcessLaunchedEventArgs> ProcessStarted;

        public void ResumeProcess()
        {
            NativeMethods.ResumeThread(_threadHandle);
        }
    }
}