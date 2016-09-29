using System;
using System.Collections.Generic;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    [Serializable]
    public class Config
    {
        public static readonly int[] ORIENTATIONS = { 0, 90, 180, 270 };
        public static readonly bool[] TARGET_PRESENCE = { true, false };
        public static readonly Size[] GRIDS = {
            new Size(7, 3),
            new Size(7, 5)
        };

        public int Timeout { get; set; } = 30; // seconds

        public bool IsPointerVisible { get; set; } = true;
        public int Repetitions { get; set; } = 1;

        public Size ScreenSize { get; set; } = new Size(277, 155);
        public Size ScreenResolution { get; set; } = new Size(1366, 768);
        public int Distance { get; set; } = 418;

        public int TrialCount
        {
            get
            {
                return Repetitions
                    * ORIENTATIONS.Length
                    * TARGET_PRESENCE.Length
                    * GRIDS.Length;
            }
        } 
    }
}
