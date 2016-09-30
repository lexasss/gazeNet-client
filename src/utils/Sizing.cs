using System;
using System.Drawing;
using System.Windows.Forms;

namespace GazeNetClient.Utils
{
    public static class Sizing
    {
        /// <summary>
        /// Screen angles in degrees
        /// </summary>
        public static SizeF CALIBRATED_SCREEN_ANGLE { get; } = new SizeF(28f, 21f);

        /// <summary>
        /// Calibrated screen resolution in pixels
        /// </summary>
        public static Size CALIBRATED_SCREEN_RESOLUTION { get; } = new Size(1366, 768);

        /// <summary>
        /// Screen size in millimeters
        /// </summary>
        public static Size ScreenSize { get; set; } = new Size(277, 155);

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
            double screebHeightInPixels = CALIBRATED_SCREEN_RESOLUTION.Height;

            return (int)(aDegrees * screebHeightInPixels / screebHeightInDegrees);
        }

        /// <summary>
        /// Calculated the scale for client visible parts
        /// </summary>
        /// <param name="aScreenSize">Actial screen size in millimeters</param>
        /// <param name="aDistance">Participants actual distance to the screen, in millimeters</param>
        /// <returns>Scale</returns>
        public static PointF getScale(Size aScreenSize, int aDistance)
        {
            SizeF actualScreenAngles = new SizeF(
                (float)toAngle(aScreenSize.Width, aDistance),
                (float)toAngle(aScreenSize.Height, aDistance)
            );

            PointF scale = new PointF(
                CALIBRATED_SCREEN_ANGLE.Width / actualScreenAngles.Width,
                CALIBRATED_SCREEN_ANGLE.Height / actualScreenAngles.Height
            );

            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            scale.X *= (float)CALIBRATED_SCREEN_RESOLUTION.Width / resolution.Width;
            scale.Y *= (float)CALIBRATED_SCREEN_RESOLUTION.Height / resolution.Height;

            return scale;
        }

        private static double toAngle(double aWidth, double aDistance)
        {
            return 2 * Math.Atan(aWidth / 2 / aDistance) * 180 / Math.PI;
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
