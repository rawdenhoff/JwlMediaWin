using GalaSoft.MvvmLight.Messaging;
using JwlMediaWin.Core;
using JwlMediaWin.Core.Models;
using JwlMediaWin.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZoomSwitcher
{
    public partial class Form1 : Form
    {
        private readonly StatusMessageGenerator _statusMessageGenerator = new StatusMessageGenerator();
        private readonly FixerRunner _fixerRunnerJWL = new FixerRunner();
        private readonly FixerRunner _fixerRunnerZoom = new FixerRunner();
        private bool _isFixed;

        private IntPtr jwlHandle;
        private IntPtr zoomHandle;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            _fixerRunnerJWL.AppType = JwLibAppTypes.JwLibrary;
            _fixerRunnerJWL.StatusEvent += HandleFixerRunnerStatusEvent;
            Task.Run(() => { _fixerRunnerJWL.Start(); });

            _fixerRunnerZoom.AppType = JwLibAppTypes.Zoom;
            _fixerRunnerZoom.StatusEvent += HandleFixerRunnerStatusEventZoom;
            Task.Run(() => { _fixerRunnerZoom.Start(); });

        }

        private void Form1_Unload(object sender, FormClosingEventArgs e)
        {
            if (chkJWL.Checked ==false || chkZoom.Checked == false )
            {
                MessageBox.Show($"Cannot quite while windows are hidden.{Environment.NewLine}{Environment.NewLine}Tick both windows first and then try again.", "Media Windows Hidden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private string GetAppName(JwLibAppTypes appType)
        {
            switch (appType)
            {
                case JwLibAppTypes.JwLibrary:
                    return "JW Library";

                case JwLibAppTypes.JwLibrarySignLanguage:
                    return "JW Library Sign Language";

                case JwLibAppTypes.Zoom:
                    return "Zoom";

                default:
                    return "Unknown";
            }
        }

        delegate void SetCheckEnabledCallback(CheckBox ctrl, bool enabled );

        private void SetCheckEnabled(CheckBox ctrl,bool enabled )
        {
            if (ctrl.InvokeRequired)
            {
                SetCheckEnabledCallback d = new SetCheckEnabledCallback(SetCheckEnabled);
                this.Invoke(d, new object[] { ctrl, enabled });
            }

            ctrl.Enabled = enabled;
            if(ctrl.Enabled == false && !ctrl.Checked) //stops the app closing while zoom or jwl media windows are hidden as they won't get found again.
            {
                ctrl.Checked = true;
            }
        }

        private void SetHandle(JwLibAppTypes AppType, IntPtr handle)
        {
            if (AppType == JwLibAppTypes.Zoom)
            {
                zoomHandle = handle;
            }
            else
            {
                jwlHandle = handle;
            }
        }

        private void ResetHandle(JwLibAppTypes AppType)
        {
            if (AppType == JwLibAppTypes.Zoom)
            {
                zoomHandle = IntPtr.Zero;
            }
            else
            {
                jwlHandle = IntPtr.Zero;
            }
        }

        private void ActionResult(FixerStatusEventArgs e, CheckBox check, IntPtr handle, JwLibAppTypes AppType)
        {
            bool booEnabled;

            booEnabled = e.Status.FindWindowResult != null && e.Status.FindWindowResult.AppIsRunning && e.Status.FindWindowResult.FoundMediaWindow;

            if (booEnabled && handle == IntPtr.Zero)
            {
                handle = new IntPtr(e.Status.FindWindowResult.MainMediaWindow.Current.NativeWindowHandle);
                SetHandle(AppType, handle);
            }
            else if (!booEnabled && handle != IntPtr.Zero)
            {
                handle = IntPtr.Zero;
                ResetHandle(AppType);
                e.Status.Reset = true;
            }
            this.SetCheckEnabled(check, booEnabled);
        }

        private void HandleFixerRunnerStatusEvent(object sender, FixerStatusEventArgs e)
        {
            _isFixed = e.Status.IsFixed || (e.Status.FindWindowResult != null && e.Status.FindWindowResult.IsAlreadyFixed);

            ActionResult(e, chkJWL, jwlHandle, _fixerRunnerJWL.AppType);

            var appName = GetAppName(_fixerRunnerJWL.AppType);
            var msg = _statusMessageGenerator.Generate(e.Status, appName);

            if (msg != null)
            {

                Log.Logger.Information(msg);

                if (e.Status.IsFixed)
                {
                    jwlHandle = IntPtr.Zero;
                    ShowBalloonMsg(SystemIcons.Information, msg);

                    //if (_isFixed && jwlHandle == IntPtr.Zero && e.Status.FindWindowResult.FoundMediaWindow)
                    //{
                    //    jwlHandle = new IntPtr(e.Status.FindWindowResult.MainMediaWindow.Current.NativeWindowHandle);
                    //}

                }
            }
        }

        private void HandleFixerRunnerStatusEventZoom(object sender, FixerStatusEventArgs e)
        {
            
            ActionResult(e, chkZoom, zoomHandle, _fixerRunnerZoom.AppType);

        }

        private void ShowBalloonMsg(Icon icon, string msg)
        {

            notifyIcon.Visible = true;
            notifyIcon.Icon = icon;
            notifyIcon.BalloonTipTitle = Application.ProductName;
            notifyIcon.BalloonTipText = msg;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(10000);

        }

        private void chkJWL_CheckedChanged(object sender, EventArgs e)
        {
            CheckboxClicked(jwlHandle,chkJWL);
        }

        private void chkZoom_CheckedChanged(object sender, EventArgs e)
        {
            CheckboxClicked(zoomHandle, chkZoom);
        }

        void CheckboxClicked(IntPtr handle, CheckBox check)
        {
            if (handle != IntPtr.Zero)
            {
                int intVal;

                if (check.Checked)
                {
                    intVal = NativeMethods.SW_SHOW;
                }
                else
                {
                    intVal = NativeMethods.SW_HIDE;
                }

                try
                {
                    bool result;
                    result = NativeMethods.ShowWindow(handle, intVal);
                }
                catch (Exception ex)
                {

                    //throw;
                }

            }
            else
            {
                SetCheckEnabled(check,false);
            }
        }

    }

}
