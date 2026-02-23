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

namespace project_RYS.Forms
{
    public partial class Fpassword : Form
    {
        //string msg = "";
        private const string PasswordFilePath = "password.txt";

        public Fpassword()
        {
            InitializeComponent();

        
        }


        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            string newPassword = pass.Text;
            string confirmPassword = cpass.Text;

            // Validation checks
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                // Check for empty or whitespace input
                ShowMessage("Please enter a new password.");
                ClearFields();
                return;
            }
            else if (newPassword != confirmPassword)
            {
                // Check if passwords match
                ShowMessage("Passwords do not match. Please try again.");
                ClearFields();
                return;
            }
            else
            {

                // Save the new password
                SavePasswordToFile(newPassword);


                ShowMessage("Password changed...!!!");
                ClearFields();
                adminlogin al = new adminlogin();
                al.Show();
                this.Close();
                //msg = "Password changed...!!!";
                //msgbox mm = new msgbox(msg);
            }
        }

        // Save password to a file
        private void SavePasswordToFile(string newPassword)
        {
            try
            {
                File.WriteAllText(PasswordFilePath, newPassword);
            }
            catch (Exception ex)
            {
                ShowMessage($"An error occurred while saving the password: {ex.Message}");
            }
        }

        private void ShowMessage(string message)
        {
            //MessageBox.Show(message, "Password Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            msgbox box = new msgbox(message);
            box.ShowDialog();
        }

        // Helper function to clear fields
        private void ClearFields()
        {
            pass.Text = string.Empty;
            cpass.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            al.Show();
            this.Close();
        }
    }
}

