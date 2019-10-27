using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;
using System.IO.Compression;
using Synapse_X_Remake;
using System.Threading;

namespace Synapse_X_Remake_UI
{

    public partial class SXROptions : Form
    {
        ExploitAPI api = new ExploitAPI();

        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LaunchExploit();

        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendLuaCScript(string script);

        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendLimitedLuaScript(string script);

        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendCommand(string script);

        public SXROptions()
        {
            InitializeComponent();
        }

        private void SXROptions_Load(object sender, EventArgs e)
        {

            //=============================================================================================================================
            // SXROPTIONS THEMES CODE IS NOT AVAILABLE..
            //=============================================================================================================================

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Process[] DetectFSPUnlocker = Process.GetProcessesByName("rbxfpsunlocker");
                if (DetectFSPUnlocker.Length > 0)
                {
                    foreach (var Kill in Process.GetProcessesByName("rbxfpsunlocker"))
                    {
                        Kill.Kill();
                    }
                    using (Process E = new Process())
                    {
                        E.StartInfo.UseShellExecute = false;
                        E.StartInfo.FileName = "./Bin/FpsUnlocker/rbxfpsunlocker.exe";
                        E.StartInfo.CreateNoWindow = true;
                        E.Start();
                    }
                }
                else
                {
                    using (Process E = new Process())
                    {
                        E.StartInfo.UseShellExecute = false;
                        E.StartInfo.FileName = "./Bin/FpsUnlocker/rbxfpsunlocker.exe";
                        E.StartInfo.CreateNoWindow = true;
                        E.Start();
                    }
                }
            }
            else
            {
                foreach (var KillFpsUnlocker in Process.GetProcessesByName("rbxfpsunlocker"))
                {
                    KillFpsUnlocker.Kill();
                }
            }
        }
            
        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                Process[] RobloxDetect = Process.GetProcessesByName("RobloxPlayerBeta");
                if (RobloxDetect.Length > 0)
                {
                    WebClient Client = new WebClient();
                    string ESP = Client.DownloadString("https://pastebin.com/raw/TarPLCzQ");
                    SendLimitedLuaScript(ESP);
                }
                else
                {
                    MessageBox.Show("Roblox Not Found!", "Synapse X Remake Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                Process[] KillRoblox = Process.GetProcessesByName("RobloxPlayerBeta");
                if (KillRoblox.Length > 0)
                {
                    foreach (var RobloxKilled in Process.GetProcessesByName("RobloxPlayerBeta"))
                    {
                        RobloxKilled.Kill();
                    }
                }
                else
                {
                    MessageBox.Show("Roblox Not Found!", "Synapse X Remake Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}