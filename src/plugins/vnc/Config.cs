using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GazeNetClient.Plugins.VNC
{
    [Serializable]
    public class Viewer
    {
        public string IP { get; set; }
        public string Name { get; set; }

        [XmlIgnore]
        public Process Process { get; set; } = null;
        [XmlIgnore]
        public bool Resize { get; set; } = false;

        public Viewer() { }
        public Viewer(string aIP, string aName, bool aResize = false) { IP = aIP; Name = aName; Resize = aResize; }
    }

    [Serializable]
    [XmlType(Namespace = "GazeNetClient.Plugins", TypeName = "VNCConfig")]
    public class Config : Plugin.Config
    {
        [XmlIgnore]
        public static readonly string UVNC_SERVER = "winvnc";
        [XmlIgnore]
        public static readonly string UVNC_VIEWER = "vncviewer";
        [XmlIgnore]
        public static readonly string UVNC_SET_PASSWORD = "setpasswd";
        [XmlIgnore]
        public static readonly string PASSWORD = "gasp";
        [XmlIgnore]
        public static readonly string UVNC_CONFIG = "options.vnc";
        [XmlIgnore]
        public static readonly uint UVNC_RECONNECT_DELAY = 5;

        public static bool isVNCInstalledInto(string aFolder)
        {
            return System.IO.File.Exists(aFolder + UVNC_SERVER + ".exe");
        }

        public string ServerFileName { get { return UVNCInstallationFolder + UVNC_SERVER + ".exe"; } }
        public string ViewerFileName { get { return UVNCInstallationFolder + UVNC_VIEWER + ".exe"; } }
        public string SetPasswordFileName { get { return UVNCInstallationFolder + UVNC_SET_PASSWORD + ".exe"; } }

        public string UVNCInstallationFolder { get; set; } = @"C:\Program Files\uvnc bvba\UltraVNC\";

        public bool ServerEnabled { get; set; } = true;

        public bool ViewersEnabled { get; set; } = true;
        public List<Viewer> Viewers { get; set; } = new List<Viewer>();
        public bool ViewOnly { get; set; } = true;
        public bool LaunchViewersOnStart { get; set; } = true;
    }
}
