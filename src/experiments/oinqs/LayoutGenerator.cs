using System;
using System.Collections.Generic;
using System.Drawing;
using GazeNetClient.Plugins.OinQs;

namespace GazeNetClient.Experiment.OinQs
{
    public static class LayoutGenerator
    {
        // Margins in degrees
        private const double MARGIN_CENTER = 1.5;
        private const double MARGIN_BORDER = 1;
        private const double MARGIN_OTHERS = 1.5;

        private const int MAX_LOCATION_SEARCH_COUNT = 1000;

        private static Random sRand = new Random();

        public static LayoutItem[] create(TrialCondition aTrialCondition)
        {
            List<LayoutItem> result = new List<LayoutItem>();

            Size screen = Utils.Sizing.CALIBRATED_SCREEN_RESOLUTION;
            Size grid = aTrialCondition.Grid;
            int targetIndex = sRand.Next(grid.Width * grid.Height);

            for (int j = 0; j < grid.Height; j++)
            {
                for (int i = 0; i < grid.Width; i++)
                {
                    Rectangle rect = new Rectangle(
                        screen.Width * i / grid.Width, 
                        screen.Height * j / grid.Height,
                        screen.Width / grid.Width,
                        screen.Height / grid.Height);

                    Point location = FindValidLocation(rect, result);
                    string imageName = FindImageName(aTrialCondition, result.Count == targetIndex);

                    result.Add(new LayoutItem(imageName, location.X, location.Y));
                }
            }

            foreach (LayoutItem item in result)
            {
                item.x /= screen.Width;
                item.y /= screen.Height;
            }

            return result.ToArray();
        }

        private static Point FindValidLocation(Rectangle aRect, List<LayoutItem> aOthers)
        {
            int marginCenter = Utils.Sizing.degrees2pixels(MARGIN_CENTER);
            int marginBorder = Utils.Sizing.degrees2pixels(MARGIN_BORDER);
            int marginOthers = Utils.Sizing.degrees2pixels(MARGIN_OTHERS);

            Size screen = Utils.Sizing.CALIBRATED_SCREEN_RESOLUTION;

            int x, y;
            bool isValid;
            int searchCount = 0;
            do
            {
                searchCount++;

                x = aRect.Left + sRand.Next(aRect.Width);
                y = aRect.Top + sRand.Next(aRect.Height);

                isValid = Validate(x, y, screen.Width / 2, screen.Height / 2, marginCenter) &&
                    Validate(x, 0, marginBorder) &&
                    Validate(y, 0, marginBorder) &&
                    Validate(x, screen.Width, marginBorder) &&
                    Validate(y, screen.Height, marginBorder);

                if (!isValid)
                    continue;

                foreach (LayoutItem item in aOthers)
                {
                    isValid = Validate(x, y, item.x, item.y, marginOthers);
                    if (!isValid)
                        break;
                }
            } while (!isValid && searchCount < MAX_LOCATION_SEARCH_COUNT);

            if (searchCount == MAX_LOCATION_SEARCH_COUNT)
                throw new ArgumentException("Cannot find a valid location. Please check the session settings");

            return new Point(x, y);
        }

        private static string FindImageName(TrialCondition aTrialCondition, bool aIsTarget)
        {
            string letter = (aIsTarget && aTrialCondition.TargetPresence) ?
                LayoutItemText.Target :
                LayoutItemText.Distractor;

            int orientation = aTrialCondition.Orientation;
            if (letter == LayoutItemText.Target && orientation > 90)
                orientation -= 180;

            return string.Format("{0}{1}", letter, orientation);
        }

        private static bool Validate(double aX1, double aY1, double aX2, double aY2, int aThreshold)
        {
            return Math.Sqrt(Math.Pow(aX1 - aX2, 2) + Math.Pow(aY1 - aY2, 2)) > aThreshold;
        }

        private static bool Validate(int aValue1, int aValue2, int aThreshold)
        {
            return Math.Abs(aValue1 - aValue2) > aThreshold;
        }
    }
}
