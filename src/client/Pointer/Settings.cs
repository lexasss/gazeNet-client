using System;

namespace GazeNetClient.Pointer
{
    [Serializable]
    public class Settings
    {
        public Style Appearance { get; set; }
        public double Opacity { get; set; }
        public int Size { get; set; }
        public long FadingInterval { get; set; }
        public long NoDataVisibilityInterval { get; set; }

        public Settings()
        {
            // All default values must be set here explicitely
            Appearance = Style.FrameRoundedDashed;
            Opacity = 0.3;
            Size = 100;
            FadingInterval = 300;
            NoDataVisibilityInterval = 1000;
        }

        public Settings(Pointer aPointer)
        {
            loadFrom(aPointer);
        }

        public Settings copy()
        {
            Settings result = new Settings();
            result.Appearance = Appearance;
            result.Opacity = Opacity;
            result.Size = Size;
            result.FadingInterval = FadingInterval;
            result.NoDataVisibilityInterval = NoDataVisibilityInterval;
            return result;
        }

        public void loadFrom(Pointer aPointer)
        {
            Appearance = aPointer.Appearance;
            Opacity = aPointer.Opacity;
            Size = aPointer.Size;
            FadingInterval = aPointer.FadingInterval;
            NoDataVisibilityInterval = aPointer.NoDataVisibilityInterval;
        }

        public void saveTo(Pointer aPointer)
        {
            aPointer.Appearance = Appearance;
            aPointer.Opacity = Opacity;
            aPointer.Size = Size;
            aPointer.FadingInterval = FadingInterval;
            aPointer.NoDataVisibilityInterval = NoDataVisibilityInterval;
        }
    }
}
