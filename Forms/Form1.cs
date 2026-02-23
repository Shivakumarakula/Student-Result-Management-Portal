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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loading.Width += 4;
            //if(loading.Width==100)
            //{
            //    loading.Width += 0;
            //}
            //if(loading.Width==100)
            //    {
            //    loading.Width += 1;
            //}


            int percentage = (loading.Width * 100) / 400;


            per.Text = percentage.ToString() + "%";


            if (loading.Width >= 400)
            {
                timer1.Stop();
                per.Text = "100%";

                homepage hp = new homepage();
                //hp.Show();
                this.Hide();
                //this.Close();
                hp.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void per_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
