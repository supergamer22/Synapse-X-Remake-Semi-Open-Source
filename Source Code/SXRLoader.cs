using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeAreDevs_API;
using Synapse_X_Remake_UI;

namespace Synapse_X_Remake_UI
{
    public partial class SXRLoader : Form
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


        public SXRLoader()
        {
            InitializeComponent();
        }
        //=============================================================================================================================

        private void SXRLoader_Load(object sender, EventArgs e)
        {
            SXROptions Options = new SXROptions();

            //=============================================================================================================================
            // SXRLOADER THEMES
            //=============================================================================================================================

            WebClient Client = new WebClient();
            string Themes = File.ReadAllText(Application.StartupPath + "\\Bin\\Themes.json");
            JObject jobject = JObject.Parse(Themes);

            // ===================== SXRLOADER LOGO ===================== //

            this.pictureBox1.ImageLocation = jobject["SXRLogo"]["SXRLoaderLogo"]["ImageLink"].ToString();

            // ===================== SXRLOADER HEADER ===================== //

            this.panel1.BackColor = jobject["SXRLoader"]["Header"]["BackColor"].ToObject<Color>();

            // ===================== SXRLOADER UI ===================== //

            this.BackColor = jobject["SXRLoader"]["SXRLoaderUI"]["BackColor"].ToObject<Color>();

            // ===================== SXRLOADER FONTS ===================== //

            this.label1.ForeColor = jobject["SXRLoader"]["Fonts"]["ForeColor"].ToObject<Color>();
            this.label2.ForeColor = jobject["SXRLoader"]["Fonts"]["ForeColor"].ToObject<Color>();

            //=============================================================================================================================
            // SXRLOADER THEMES END
            //=============================================================================================================================

            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            SXRMain main = new SXRMain();
            main.Show();
        }
    }
}
