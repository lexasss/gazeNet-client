using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GazeNetClient.Plugins.VNC
{
    [Serializable]
    public class Viewer
    {
        public string IP { get; set; }
        public string Name { get; set; }
        public Viewer() { }
        public Viewer(string aIP, string aName) { IP = aIP; Name = aName; }
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

        public static bool isVNCInstalledInto(string aFolder)
        {
            return System.IO.File.Exists(aFolder + UVNC_SERVER + ".exe");
        }

        public static bool isVNCProcess(string aProcessName)
        {
            return aProcessName == UVNC_SERVER || aProcessName == UVNC_VIEWER;
        }

        public string ServerFileName { get { return UVNCInstallationFolder + UVNC_SERVER + ".exe"; } }
        public string ViewerFileName { get { return UVNCInstallationFolder + UVNC_VIEWER + ".exe"; } }
        public string SetPasswordFileName { get { return UVNCInstallationFolder + UVNC_SET_PASSWORD + ".exe"; } }

        public string UVNCInstallationFolder { get; set; } = @"C:\Program Files\uvnc bvba\UltraVNC\";

        public bool ServerEnabled { get; set; } = true;

        public bool ViewersEnabled { get; set; } = true;
        public List<Viewer> Viewers { get; set; } = new List<Viewer>();
        public bool ViewOnly { get; set; } = true;
        public bool AutoScaling { get; set; } = true;
        public bool SharedServer { get; set; } = true;
        public bool DisableClipboard { get; set; } = true;
    }
}
