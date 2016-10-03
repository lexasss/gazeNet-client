using System.Text;

namespace GazeNetClient.Plugins.OinQs
{
    public class TrialConfig
    {
        public string Name { get; set; }

        public TrialConfig() { }
        public TrialConfig(string aName) { Name = aName; }

        public override string ToString()
        {
            return new StringBuilder().
                Append(Name).
                ToString();
        }
    }
}
