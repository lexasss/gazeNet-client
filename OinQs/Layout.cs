using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OinQs
{
    public class Layout
    {
        public const int MIN_DIST_TO_CENTER = 200;
        public const int MIN_DIST_TO_ITEM = 100;

        public static Letter[] create(Size aFieldSize, int aCount)
        {
            List<Letter> result = new List<Letter>();
            Random r = new Random();

            for (int i = 0; i < aCount; i++)
            {
                Letter letter = new Letter();
                if (i == 0)
                    letter.Text = "O";

                int x, y;
                bool isValid;
                do
                {
                    x = r.Next(aFieldSize.Width);
                    y = r.Next(aFieldSize.Height);
                    isValid = validate(x, y, new Point(aFieldSize.Width / 2, aFieldSize.Height / 2));
                    if (!isValid)
                        continue;
                    foreach (Letter item in result)
                    {
                        isValid = validate(x, y, item.Location);
                        if (!isValid)
                            break;
                    }
                } while (!isValid);

                letter.Left = x;
                letter.Top = y;
                result.Add(letter);
            }

            return result.ToArray();
        }

        private static bool validate(int aLeft, int aTop, Size aFieldSize, Size aLetterSize)
        {
            return aLeft < aFieldSize.Width - aLetterSize.Width && aTop < aFieldSize.Height - aLetterSize.Height;
        }

        private static bool validate(int aLeft, int aTop, Point aAnotherLetter)
        {
            return Math.Sqrt(Math.Pow(aLeft - aAnotherLetter.X, 2) + Math.Pow(aTop - aAnotherLetter.Y / 2, 2)) > MIN_DIST_TO_ITEM;
        }
    }
}
