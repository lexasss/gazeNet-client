namespace GazeNetClient.Plugins.Scaler
{
    public class Config : Plugin.Config
    {
        public int Left { get; set; } = 0;
        public int Top { get; set; } = 0;
        public int Right { get; set; } = 640;
        public int Bottom { get; set; } = 480;

        public int Width { get { return Right - Left; } }
        public int Height { get { return Bottom - Top; } }
    }
}
