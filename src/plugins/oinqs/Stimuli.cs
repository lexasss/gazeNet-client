using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Stimuli : UserControl
    {
        public int X
        {
            get { return (int)(Left + Width / 2); }
            set { Left = value - Width / 2; }
        }
        public int Y
        {
            get { return (int)(Top + Height / 2); }
            set { Top = value - Height / 2; }
        }

        public Stimuli()
        {
            InitializeComponent();
        }

        public void setImage(string aKey)
        {
            BackgroundImage = imlQs.Images[aKey];
        }
    }
}
