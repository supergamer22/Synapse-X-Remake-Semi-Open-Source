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
using CefSharp;
using CefSharp.WinForms;
using BetterMonacoNET;

namespace Synapse_X_Remake_UI
{
    public partial class SXRMain : Form
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


        public SXRMain()
        {
            InitializeComponent();
        }

        private void SXRMain_Load(object sender, EventArgs e)
        {
            monaco1.Initialize();

            //=============================================================================================================================
            // SXRMAIN THEMES CODE IS NOT AVAILABLE...
            //=============================================================================================================================

            listBox1.Items.Clear();
            SXRFunctions.PopulateListBox(listBox1, "./Scripts", "*.txt");
            SXRFunctions.PopulateListBox(listBox1, "./Scripts", "*.lua");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SendLimitedLuaScript(monaco1.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            monaco1.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                monaco1.Text = File.ReadAllText(ofd.FileName);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ef = new OpenFileDialog();
            ef.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua";
            if (ef.ShowDialog() == DialogResult.OK)
            {
                SendLimitedLuaScript(File.ReadAllText(ef.FileName));
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                sw.Write(monaco1.Text);
                sw.Close();
                s.Close();
            }
        }

        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                SendLimitedLuaScript(System.IO.File.ReadAllText("Scripts\\" + this.listBox1.SelectedItem.ToString()));
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SXRFunctions.PopulateListBox(listBox1, "./Scripts", "*.txt");
            SXRFunctions.PopulateListBox(listBox1, "./Scripts", "*.lua");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SXROptions Options = new SXROptions();
            button6.Text = "Starting...";
            Task.Delay(1000).Wait();
            button6.Text = "Options";
            Options.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            button7.Text = "Starting...";
            Task.Delay(1000).Wait();
            button7.Text = "Script Hub";
            SXRScriptHub SH = new SXRScriptHub();
            SH.ShowDialog();
        }

        public async void AutoExec()
        {
            foreach (string scripts in Directory.EnumerateFiles("./AutoExec", "*.lua"))
            {
                SendLimitedLuaScript(File.ReadAllText(scripts));
            }

            foreach (string scripts in Directory.EnumerateFiles("./AutoExec", "*.txt"))
            {
                SendLimitedLuaScript(File.ReadAllText(scripts));
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process[] DetectingRoblox = Process.GetProcessesByName("RobloxPlayerBeta");
            if (DetectingRoblox.Length > 0)
            {
                if (api.isAPIAttached())
                {
                    label1.Text = "Synapse X - v1.8.9c (Scanning if API is Attached...)";
                    label1.Location = new Point(295, 6);
                    Task.Delay(1000).Wait();
                    label1.Text = "Synapse X - v1.8.9c (API Already Attached!)";
                    label1.Location = new Point(295, 6);
                    Task.Delay(500).Wait();
                    label1.Text = "Synapse X - v1.8.9c";
                    label1.Location = new Point(357, 6);
                }
                else
                {
                    label1.Text = "Synapse X - v1.8.9c (Scanning if API is Attached...)";
                    label1.Location = new Point(290, 6);
                    Task.Delay(1000).Wait();
                    label1.Text = "Synapse X - v1.8.9c (Scanning Roblox...)";
                    label1.Location = new Point(300, 6);
                    Task.Delay(1000).Wait();
                    label1.Text = "Synapse X - v1.8.9c (Injecting...)";
                    label1.Location = new Point(320, 6);
                    Task.Delay(1000).Wait();
                    label1.Text = "Synapse X - v1.8.9c (Ready!)";
                    label1.Location = new Point(320, 6);
                    LaunchExploit();
                    Task.Delay(500).Wait();
                    label1.Text = "Synapse X - v1.8.9c";
                    label1.Location = new Point(357, 6);
                    this.AutoExec();
                }
            }
            else
            {
                label1.Text = "Synapse X - v1.8.9c (Scanning Roblox...)";
                label1.Location = new Point(300, 6);
                Task.Delay(2000).Wait();
                label1.Text = "Synapse X - v1.8.9c (Roblox Not Found!)";
                label1.Location = new Point(295, 6);
                Task.Delay(1000).Wait();
                label1.Text = "Synapse X - v1.8.9c";
                label1.Location = new Point(357, 6);
            }
        } 

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                monaco1.Text = File.ReadAllText("Scripts\\" + this.listBox1.SelectedItem.ToString());
            }
        }
    }
}
