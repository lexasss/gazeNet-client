using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace GazeNetClient.Plugins.OinQs
{
    /*
    public sealed class NAME
    {
        private const string val = "oinqs";

        public static bool operator ==(NAME aVal1, string aVal2) { return aVal1.Equals(aVal2); }
        public static bool operator !=(NAME aVal1, string aVal2) { return !aVal1.Equals(aVal2); }
        public static implicit operator string(NAME aRef) { return aRef.ToString(); }

        public override string ToString() { return val; }
        public override bool Equals(object obj) { return val.Equals(obj); }
        public override int GetHashCode() { return val.GetHashCode(); }
    }*/

    public class OinQs : Plugin.IPlugin
    {
        public const string NAME = "oinqs";

        private class LetterNetDesc
        {
            public string text { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        private Display iDisplay = new Display();
        JavaScriptSerializer iJSON = new JavaScriptSerializer();

        public Dictionary<string, EventHandler> MenuItems { get; } = null;
        public string Name { get; } = NAME;

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<Plugin.RequestArgs> Req = delegate { };

        public OinQs()
        {
            iDisplay.Visible = false;

            iDisplay.OnFound += Display_OnFound;
            iDisplay.OnStopped += Display_OnStopped;
        }

        public void feed(float aX, float aY)
        {
            
        }

        public void finilize()
        {
            iDisplay.Hide();
        }

        public void start()
        {
            iDisplay.Controls.Clear();
            iDisplay.Enabled = false;
            iDisplay.Show();
        }

        public void command(string aCommand, string aValue)
        {
            if (aCommand == Command.ADD)
            {
                LetterNetDesc letterDesc = iJSON.Deserialize<LetterNetDesc>(aValue);
                CreateAndAddLetter(letterDesc);
            }
            else if (aCommand == Command.ADD_RANGE)
            {
                LetterNetDesc[] letterDescs = iJSON.Deserialize<LetterNetDesc[]>(aValue);
                foreach (LetterNetDesc letterDesc in letterDescs)
                    CreateAndAddLetter(letterDesc);
            }
            else if (aCommand == Command.DISPLAY)
            {
                foreach (Control ctrl in iDisplay.Controls)
                    ctrl.Visible = true;
                iDisplay.Enabled = true;
            }
            else if (aCommand == Command.RESULT)
            {
                if (iDisplay.Enabled)
                {
                    //SearchResult sr = iJSON.Deserialize<SearchResult>(aValue);
                    //Console.WriteLine("Someone's result: {0}", sr.found ? "found" : "not found");
                    FinishTrial();
                }
            }
        }

        private void CreateAndAddLetter(LetterNetDesc aLetterDesc)
        {
            Letter letter = new Letter();
            letter.Text = aLetterDesc.text;
            letter.X = aLetterDesc.x;
            letter.Y = aLetterDesc.y;
            letter.Visible = false;
            iDisplay.Controls.Add(letter);
        }

        private void FinishTrial()
        {
            iDisplay.Enabled = false;
            iDisplay.Controls.Clear();
        }

        private void Display_OnStopped(object aSender, EventArgs aArgs)
        {
            FinishTrial();
            string payload = iJSON.Serialize(new SearchResult() { found = false });
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(Name, Command.RESULT, payload)));
        }

        private void Display_OnFound(object aSender, EventArgs aArgs)
        {
            FinishTrial();
            string payload = iJSON.Serialize(new SearchResult() { found = true });
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(Name, Command.RESULT, payload)));
        }
    }
}
