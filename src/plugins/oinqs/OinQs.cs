using System;
using System.Collections.Generic;
using System.Drawing;
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
            iDisplay.OnNotFound += Display_OnNotFound;
            iDisplay.OnNext += Display_OnNext;
            iDisplay.OnRequestExit += Display_OnRequestExit;
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
            iDisplay.clear();
            iDisplay.Show();
        }

        public void command(string aCommand, string aValue)
        {
            if (aCommand == Command.CONFIG)
            {
                Plugin.ContainerConfig containerConfig = iJSON.Deserialize<Plugin.ContainerConfig>(aValue);
                PointF scale = Utils.Sizing.getScale(containerConfig.ScreenSize, containerConfig.Distance);
                iDisplay.StimuliScale = scale;
                Req(this, new Plugin.SetContainerConfigRequestArgs(containerConfig));
            }
            else if (aCommand == Command.ADD)
            {
                LayoutItem item = iJSON.Deserialize<LayoutItem>(aValue);
                CreateAndAddStimuli(item);
            }
            else if (aCommand == Command.ADD_RANGE)
            {
                LayoutItem[] items = iJSON.Deserialize<LayoutItem[]>(aValue);
                foreach (LayoutItem item in items)
                    CreateAndAddStimuli(item);
            }
            else if (aCommand == Command.INSTRUCTION)
            {
                Instruction instruction = iJSON.Deserialize<Instruction>(aValue);
                iDisplay.showInstruction(instruction);
            }
            else if (aCommand == Command.DISPLAY)
            {
                iDisplay.showItems();
            }
            else if (aCommand == Command.RESULT)
            {
                if (iDisplay.IsDisplayingItems)
                {
                    //SearchResult sr = iJSON.Deserialize<SearchResult>(aValue);
                    //System.Diagnostics.Debug.WriteLine("Someone's result: {0}", sr.found ? "found" : "not found");
                    FinishTrial();
                }
            }
        }

        private void CreateAndAddStimuli(LayoutItem aItem)
        {
            Console.WriteLine("{0},{1}    {2}", aItem.x, aItem.y, iDisplay.StimuliScale);
            Size screenSize = Screen.FromControl(iDisplay).Bounds.Size;
            Stimuli stimuli = new Stimuli();

            stimuli.Size = new Size(
                (int)(stimuli.Size.Width * iDisplay.StimuliScale.Y), // "Y" is not a mistake: scaling must be equal in both dimension, vertical scale suits better
                (int)(stimuli.Size.Height * iDisplay.StimuliScale.Y)
            );
            stimuli.setImage(aItem.text);
            stimuli.X = (int)(aItem.x * screenSize.Width);
            stimuli.Y = (int)(aItem.y * screenSize.Height);
            stimuli.Visible = false;
            iDisplay.addItem(stimuli);
        }

        private void FinishTrial()
        {
            iDisplay.clear();
            iDisplay.showInstruction(new Instruction("...", 0));    // just to prevent keypress handling
        }

        private void Display_OnNotFound(object aSender, EventArgs aArgs)
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

        private void Display_OnNext(object sender, EventArgs e)
        {
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(Name, Command.NEXT, "")));
        }

        private void Display_OnRequestExit(object aSender, EventArgs aArgs)
        {
            Req(this, new Plugin.RequestArgs(Plugin.RequestType.Stop));
        }
    }
}
