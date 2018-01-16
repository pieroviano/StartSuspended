using System;
using System.Diagnostics;
using System.Windows.Forms;
using StartSuspendedLib;
using StartSuspendedLib.Model;

namespace StartSuspended
{
    public partial class MainForm : Form
    {
        private readonly ProcessLauncher _processLauncher;

        private int _trackBarValue;

        public int TrackBarValue
        {
            get => _trackBarValue;
            set
            {
                _trackBarValue = value;
                _processLauncher.InitialResumeTime = value * value;
            }
        }


        public MainForm()
        {
            InitializeComponent();
            AllowDrop = true;
            _processLauncher = new ProcessLauncher();
            _processLauncher.ProcessStarted += _processLauncher_ProcessStarted;
            TrackBarValue = 10;
            UpdateGui();
        }

        private void _processLauncher_ProcessStarted(object sender, ProcessLaunchedEventArgs e)
        {
            if (e.Started)
            {
                MessageBox.Show(
                    e.Message,
                    e.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(e.Message,
                    e.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            TrackBarValue = trackBar.Value;
            UpdateGui();
        }

        private void UpdateGui()
        {
            trackBar.Value = TrackBarValue;
            textBox.Text = _processLauncher.InitialResumeTime.ToString();
        }


        #region Drag & Drop

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (GetFileNameFromDragEvents(e).Length > 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var filename = GetFileNameFromDragEvents(e);
            Debug.Assert(filename.Length > 0);
            _processLauncher.LaunchProcessInteractively(filename);
        }

        private string GetFileNameFromDragEvents(DragEventArgs e)
        {
            var o = e.Data.GetData(DataFormats.FileDrop);
            var ss = o as string[];
            if (ss?.Length > 0)
            {
                var filename = ss[0];
                return filename;
            }
            return string.Empty;
        }

        #endregion
    }
}