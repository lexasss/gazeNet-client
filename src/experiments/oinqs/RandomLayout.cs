using System;
using System.Collections.Generic;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    public static class RandomLayout
    {
        private const int MARGIN_CENTER = 73;   // 2 deg
        private const int MARGIN_BORDER = 36;   // 1 deg
        private const int MARGIN_OTHERS = 73;   // 2 deg

        public static Plugins.OinQs.LayoutItem[] create(Size aFieldSize, int aCount, TrialConditions aTrialCondition)
        {
            List<Plugins.OinQs.LayoutItem> result = new List<Plugins.OinQs.LayoutItem>();
            Random rand = new Random();

            for (int i = 0; i < aCount; i++)
            {
                int x, y;
                bool isValid;
                do
                {
                    x = rand.Next(aFieldSize.Width);
                    y = rand.Next(aFieldSize.Height);

                    isValid = validate(x, y, aFieldSize.Width / 2, aFieldSize.Height / 2, MARGIN_CENTER) &&
                        validate(x, 0, MARGIN_BORDER) && 
                        validate(y, 0, MARGIN_BORDER) &&
                        validate(x, aFieldSize.Width, MARGIN_BORDER) &&
                        validate(y, aFieldSize.Height, MARGIN_BORDER);
                    if (!isValid)
                        continue;

                    foreach (Plugins.OinQs.LayoutItem item in result)
                    {
                        isValid = validate(x, y, item.x, item.y, MARGIN_OTHERS);
                        if (!isValid)
                            break;
                    }
                } while (!isValid);

                string letter = (i == 0 && aTrialCondition.TargetPresence) ? Plugins.OinQs.LayoutItemText.Target : Plugins.OinQs.LayoutItemText.Distractor;
                int orientation = aTrialCondition.Orientation;
                if (letter == Plugins.OinQs.LayoutItemText.Target && orientation > 90)
                    orientation -= 180;

                string text = string.Format("{0}{1}", letter, orientation);
                result.Add(new Plugins.OinQs.LayoutItem(text, x, y));
            }

            return result.ToArray();
        }

        private static bool validate(int aX1, int aY1, int aX2, int aY2, int aThreshold)
        {
            return Math.Sqrt(Math.Pow(aX1 - aX2, 2) + Math.Pow(aY1 - aY2, 2)) > aThreshold;
        }

        private static bool validate(int aValue1, int aValue2, int aThreshold)
        {
            return Math.Abs(aValue1 - aValue2) > aThreshold;
        }
    }
}
