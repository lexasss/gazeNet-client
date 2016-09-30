using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace GazeNetClient.Plugins.OinQs
{
    public class OinQs : Plugin.IPlugin
    {
        #region Internal members

        private Display iDisplay = new Display();
        private JavaScriptSerializer iJSON = new JavaScriptSerializer();
        private GazeLogger iGazeLogger = new GazeLogger();

        #endregion

        #region Publica members

        public const string NAME = "oinqs";

        public Dictionary<string, EventHandler> MenuItems { get; } = null;
        public string Name { get; } = NAME;

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<Plugin.RequestArgs> Req = delegate { };

        #endregion

        #region Public methods

        public OinQs()
        {
            iDisplay.Visible = false;

            iDisplay.OnFound += Display_OnFound;
            iDisplay.OnNotFound += Display_OnNotFound;
            iDisplay.OnNext += Display_OnNext;
            iDisplay.OnRequestExit += Display_OnRequestExit;
            iDisplay.OnRequestSave += Display_OnRequestSave;
        }

        public void feed(Processor.GazePoint aSample)
        {
            iGazeLogger.feed(aSample);
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
                iGazeLogger.startTrial(new Trial.Config(""));
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

        #endregion

        #region Internal methods

        private void CreateAndAddStimuli(LayoutItem aItem)
        {
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
            iGazeLogger.endTrial();
        }

        private void TryToSaveData()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text files|*.txt";
            sfd.DefaultExt = "txt";
            if (sfd.ShowDialog() == DialogResult.OK)
                iGazeLogger.save(sfd.FileName);
        }

        #endregion

        #region Event handlers

        private void Display_OnNotFound(object aSender, EventArgs aArgs)
        {
            FinishTrial();
            string payload = iJSON.Serialize(new SearchResult() { found = false });
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(NAME, Command.RESULT, payload)));
        }

        private void Display_OnFound(object aSender, EventArgs aArgs)
        {
            FinishTrial();
            string payload = iJSON.Serialize(new SearchResult() { found = true });
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(NAME, Command.RESULT, payload)));
        }

        private void Display_OnNext(object sender, EventArgs e)
        {
            Req(this, new Plugin.SendCommandRequestArgs(new Plugin.Command(NAME, Command.NEXT, "")));
        }

        private void Display_OnRequestExit(object aSender, EventArgs aArgs)
        {
            bool canExit = true;
            if (iGazeLogger.HasData)
            {
                DialogResult answer = MessageBox.Show("Data not saved. Do you wish to save it?", NAME, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (answer == DialogResult.Cancel)
                    canExit = false;
                else if (answer == DialogResult.Yes)
                    TryToSaveData();
            }

            if (canExit)
                Req(this, new Plugin.RequestArgs(Plugin.RequestType.Stop));
        }

        private void Display_OnRequestSave(object aSender, EventArgs aArgs)
        {
            TryToSaveData();
        }

        #endregion
    }
}
