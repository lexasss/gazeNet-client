using System;
using System.Drawing;
using System.Windows.Forms;

namespace GazeNetClient.Utils
{
    public static class Sizing
    {
        /// <summary>
        /// Screen size in millimeters
        /// </summary>
        public static Size ScreenSize { get; set; } = new Size(277, 155);

        /// <summary>
        /// Screen resolution in pixels
        /// </summary>
        public static Size ScreenResolution { get; set; } = new Size(1366, 768);

        /// <summary>
        /// Disntance from eye to the screen in millimeters
        /// </summary>
        public static int ScreenDistance { get; set; } = 418;

        /// <summary>
        /// Converts degrees to pixels
        /// </summary>
        /// <param name="aDegrees">Size in degrees</param>
        /// <returns>Size in pixels</returns>
        public static int degrees2pixels(double aDegrees)
        {
            double screebHeightInDegrees = toDegrees(2 * Math.Atan((double)ScreenSize.Height / 2 / ScreenDistance));
            double screebHeightInPixels = ScreenResolution.Height;

            return (int)(aDegrees * screebHeightInPixels / screebHeightInDegrees);
        }

        private static double toDegrees(double aRadians)
        {
            return aRadians * 180.0 / Math.PI;
        }

        private static double toRadians(double aDegrees)
        {
            return aDegrees * Math.PI / 180.0;
        }
    }
}
