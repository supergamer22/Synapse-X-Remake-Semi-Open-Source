using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Synapse_X_Remake_UI
{
    public partial class SXRScriptHub : Form
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

        public SXRScriptHub()
        {
            InitializeComponent();
        }

        private void SXRScriptHub_Load(object sender, EventArgs e)
        {
            //=============================================================================================================================
            // SXRSCRIPTHUB THEMES AND SCRIPT HUB CODE IS NOT AVAILABLE...
            //=============================================================================================================================

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Script Hub Code is not available...
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            // Script Hub Code is not available...
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
