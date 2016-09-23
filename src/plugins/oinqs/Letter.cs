using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Letter : UserControl
    {
        public override string Text
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
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

        public Letter()
        {
            InitializeComponent();
        }
    }
}
