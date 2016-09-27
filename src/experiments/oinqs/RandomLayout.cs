using System;
using System.Collections.Generic;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    public static class RandomLayout
    {
        private const int MIN_DIST_TO_CENTER = 200;
        private const int MIN_DIST_TO_ITEM = 100;

        public static Plugins.OinQs.LayoutItem[] create(Size aFieldSize, int aCount, bool aTargetPresence)
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
                    isValid = validate(x, y, aFieldSize.Width / 2, aFieldSize.Height / 2);
                    if (!isValid)
                        continue;
                    foreach (Plugins.OinQs.LayoutItem item in result)
                    {
                        isValid = validate(x, y, item.x, item.y);
                        if (!isValid)
                            break;
                    }
                } while (!isValid);

                string text = (i == 0 && aTargetPresence) ? Plugins.OinQs.LayoutItemText.Target : Plugins.OinQs.LayoutItemText.Distractor;
                result.Add(new Plugins.OinQs.LayoutItem(text, x, y));
            }

            return result.ToArray();
        }

        private static bool validate(int aLeft, int aTop, int aAnotherX, int aAnotherY)
        {
            return Math.Sqrt(Math.Pow(aLeft - aAnotherX, 2) + Math.Pow(aTop - aAnotherY / 2, 2)) > MIN_DIST_TO_ITEM;
        }
    }
}
