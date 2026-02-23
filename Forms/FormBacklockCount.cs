//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.IO;

//namespace project_RYS.Forms
//{
//    public partial class FormBacklockCount : Form
//    {
//        private const string studentMarksJson = "studentMarks.json";
//        public FormBacklockCount()
//        {
//            InitializeComponent();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            string studentId = stuid.Text;

//            if (string.IsNullOrEmpty(studentId))
//            {
//                MessageBox.Show("Please enter a valid Student ID.");
//                return;
//            }
//            else
//            {
//                // Open the backlog results form
//                BacklogResultsForm resultsForm = new BacklogResultsForm(studentId);
//                //FormBacklogreport formBacklogreport = new FormBacklogreport(studentId);
//                //formBacklogreport.Show();
//                //string studentId = stuid.Text.Trim();
//                resultsForm.Show();
//            }

//            //if (string.IsNullOrEmpty(studentId))
//            //{
//            //    MessageBox.Show("Please enter a Student ID.");
//            //    return;
//            //}

//            //DisplayBacklogs(studentId);
//        }


//    }
//}

//    //public class StudentMark
//    //{
//    //    public string StudentId { get; set; }
//    //    public string Name { get; set; }
//    //    public string SubjectCode { get; set; }
//    //    public string SubjectName { get; set; }
//    //    public string Internal { get; set; }
//    //    public string External { get; set; }
//    //    public string Total { get; set; }
//    //    public string Grade { get; set; }
//    //    public string Credits { get; set; }
//    //    public string Year { get; set; }  // Make sure Year is included in your StudentMark class
//    //    public string Semester { get; set; } // Make sure Semester is included in your StudentMark class
//    //}



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class FormBacklockCount : Form
    {
        string msg = "";
        private const string studentMarksJson = "studentMarks.json";

        public FormBacklockCount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentId = stuid.Text.ToUpper().Trim();

            if (string.IsNullOrEmpty(studentId))
            {
                //MessageBox.Show("Please enter a valid Student ID.");
                msg = "Please enter a vaild student ID...!!";
                msgbox mb = new msgbox(msg);
                mb.ShowDialog();
                return;
            }

            // Check if the student ID exists in the JSON file
            if (!StudentIdExists(studentId))
            {
                //MessageBox.Show("");

                msg = "Student ID not found. Please enter a valid Student ID.!!";
                msgbox mb = new msgbox(msg);
                mb.ShowDialog();
                return;
            }

            // Open the backlog results form if student ID is found
            BacklogResultsForm resultsForm = new BacklogResultsForm(studentId);
           
            resultsForm.Show();
            // Close MainForm
            Application.OpenForms["admindashboard"]?.Close();

            // Close ChildForm
            this.Hide(); 
            //this.Close();


        }

        private bool StudentIdExists(string studentId)
        {
            try
            {
                if (!File.Exists(studentMarksJson))
                {
                    //MessageBox.Show("");
                    msg = "Student data file not found.!";
                    msgbox mb = new msgbox(msg);
                    mb.ShowDialog();
                    return false;
                }

                // Read and deserialize the JSON data
                string jsonData = File.ReadAllText(studentMarksJson);
                var students = JsonConvert.DeserializeObject<List<Stu>>(jsonData);

                // Check if any student matches the given ID
                return students != null && students.Any(s => s.StudentId == studentId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($" {ex.Message}");
                msg = "Error reading student data:"+ex;
                msgbox mb = new msgbox(msg);
                mb.ShowDialog();
                return false;
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            //this.Hide();
            this.Close();
            Application.OpenForms["admindashboard"]?.Close();
            admindashboard adb = new admindashboard();
            adb.Show();

            try
            {
                extra_form child = new extra_form();
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                adb.panelDesktopPane.Controls.Clear(); // Clear existing content
                adb.panelDesktopPane.Controls.Add(child); // Add ChildForm to panel
                child.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Define a class for the student data (update according to your actual data structure)
    public class Stu
    {
        public string StudentId { get; set; }
        // Add other properties as necessary
    }
}
