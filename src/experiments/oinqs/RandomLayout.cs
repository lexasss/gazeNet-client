using System;
using System.Collections.Generic;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    public static class RandomLayout
    {
        private const int MIN_DIST_TO_CENTER = 200;
        private const int MIN_DIST_TO_ITEM = 100;

        public static LayoutItem[] create(Size aFieldSize, int aCount)
        {
            List<LayoutItem> result = new List<LayoutItem>();
            Random r = new Random();

            for (int i = 0; i < aCount; i++)
            {
                int x, y;
                bool isValid;
                do
                {
                    x = r.Next(aFieldSize.Width);
                    y = r.Next(aFieldSize.Height);
                    isValid = validate(x, y, aFieldSize.Width / 2, aFieldSize.Height / 2);
                    if (!isValid)
                        continue;
                    foreach (LayoutItem item in result)
                    {
                        isValid = validate(x, y, item.x, item.y);
                        if (!isValid)
                            break;
                    }
                } while (!isValid);

                result.Add(new LayoutItem(i == 0 ? "O" : "Q", new Point(x, y)));
            }

            return result.ToArray();
        }

        private static bool validate(int aLeft, int aTop, int aAnotherX, int aAnotherY)
        {
            return Math.Sqrt(Math.Pow(aLeft - aAnotherX, 2) + Math.Pow(aTop - aAnotherY / 2, 2)) > MIN_DIST_TO_ITEM;
        }
    }
}
