using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_RYS
{
    public partial class homepage : Form
    {
        public homepage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            al.Show();
            this.Hide();
            //msgbox mb= new msgbox();
            //mb.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //studentpage sp = new studentpage();
            //sp.Show();
            //this.Hide();

            string msg = "Services Error";
            msgbox mm = new msgbox(msg);
            mm.Show();
        }
    }
}
