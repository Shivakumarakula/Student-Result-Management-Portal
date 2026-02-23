using project_RYS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace project_RYS
{
    public partial class adminlogin : Form
    {
        string msg = "";
        private const string PasswordFilePath = "password.txt";
        public adminlogin()
        {
            InitializeComponent();
            password.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            username.Text = string.Empty;
            password.Text = string.Empty;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            password.PasswordChar = '*';
        }

        private void eye_Click(object sender, EventArgs e)
        {
            if (password.PasswordChar == '*')
            {
                //password.PasswordChar = 
                password.PasswordChar = '\0';

            }
            else
            {
                password.PasswordChar = '*';
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (password.PasswordChar == '*')
            {
                //password.PasswordChar = 
                password.PasswordChar = '\0';

            }
            else
            {
                password.PasswordChar = '*';
            }
        }

        // Read the password from the file (if needed for validation)
        private string GetPasswordFromFile()
        {
            if (File.Exists(PasswordFilePath))
            {
                return File.ReadAllText(PasswordFilePath);
            }
            else
            {
                return string.Empty; // No password file exists
            }
        }


        private void submit_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "")
            {
             msg = "Please enter all the details...!!";
                msgbox mb = new msgbox(msg);
                mb.ShowDialog();
                //MessageBox.Show("please enter all the details");
            }
            else
            {
                //try
                //{
                //    string storepass = GetPasswordFromFile();
                //    if (username.Text.Equals("admin", StringComparison.OrdinalIgnoreCase) && password.Text == storepass)
                //    {
                //        //uploadform uf = new uploadform();
                //        //uf.Show();
                //        //this.Hide();
                //        admindashboard ad = new admindashboard();
                //        ad.Show();
                //        this.Hide();

                //    }
                //    else
                //    {
                //        //MessageBox.Show("please enter correct details..");

                //        msg = "Please enter Correct  details...!!";

                //        msgbox mb = new msgbox(msg);
                //        mb.ShowDialog();
                //    }

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}

                try
                {
                    string storepass = GetPasswordFromFile();

                    if ((username.Text == "admin" || username.Text == "ADMIN") && password.Text == storepass)
                    {
                        admindashboard ad = new admindashboard();
                        ad.Show();
                        this.Hide();
                    }
                    else
                    {
                        msg = "Please enter correct details...!!";
                        msgbox mb = new msgbox(msg);
                        mb.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            homepage homepage = new homepage(); 
            homepage.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            homepage homepage = new homepage();
            homepage.Show();
            this.Hide();

        }

        private void adminlogin_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Fpassword fp=new Fpassword();
            fp.Show();
            this.Close();
        }

        private void password_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
