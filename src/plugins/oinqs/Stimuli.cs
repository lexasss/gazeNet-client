using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Stimuli : UserControl
    {
        public string Image
        {
            get { return lblText.ImageKey; }
            set { lblText.ImageKey = value; }
        }

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
    }
}
