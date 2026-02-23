//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace project_RYS
//{
//    public partial class msgbox : Form
//    {
//        public msgbox(string msg1)
//        {
//            InitializeComponent();
//            msg.Text = msg1;
//            msg.ForeColor=System.Drawing.SystemColors.ControlText;
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//        }

//        private void msgbox_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Windows.Forms;

namespace project_RYS
{
    public partial class msgbox : Form
    {
        public msgbox(string msg1)
        {
            InitializeComponent();
            msg.Text = msg1;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void msgbox_Load(object sender, EventArgs e)
        {
            // Optional styling
        }
    }
}
