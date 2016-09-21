using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OinQs
{
    public partial class Letter : UserControl
    {
        public override string Text
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }

        public int X { get { return (int)(Left + Width / 2); } }
        public int Y { get { return (int)(Top + Height / 2); } }

        public Letter()
        {
            InitializeComponent();
        }
    }
}
