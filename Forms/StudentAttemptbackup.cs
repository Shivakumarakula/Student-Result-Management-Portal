//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class StudentAttemptbackup : Form
//    {
//        public StudentAttemptbackup()
//        {
//            InitializeComponent();
//        }

//        private void btnstuatmUpload_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class StudentAttemptbackup : Form
    {
        private const string studentAttemptsJson = "studentAttempts.json"; // The file where student attempts will be stored
        private List<StudentAttempt> existingAttempts = new List<StudentAttempt>(); // List to hold existing attempts data
        private string msg = "";

        public StudentAttemptbackup()
        {
            InitializeComponent();
            LoadExistingAttempts(); // Load existing data if any
        }

        // Button Click to upload the JSON file
        private void btnstuatmUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files|*.json"; // Filter to show only .json files
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    try
                    {
                        // Read the JSON data from the selected file
                        var jsonData = File.ReadAllText(selectedFilePath);
                        var uploadedAttempts = JsonConvert.DeserializeObject<List<StudentAttempt>>(jsonData); // Deserialize to List of StudentAttempt objects

                        // Validate the contents of the uploaded JSON file
                        if (uploadedAttempts != null && uploadedAttempts.Any())
                        {
                            // Overwrite the existing JSON file with the new data
                            File.WriteAllText(studentAttemptsJson, jsonData);
                            existingAttempts = uploadedAttempts; // Update in-memory list with uploaded data
                            msg = "Student attempts have been successfully restored from the backup.";
                            ShowMessage(msg);
                        }
                        else
                        {
                            msg = "The uploaded JSON file does not contain valid student attempts.";
                            ShowMessage(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = $"An error occurred while processing the file: {ex.Message}";
                        ShowMessage(msg);
                    }
                }
            }
        }

        // Method to load existing student attempts from the JSON file
        private void LoadExistingAttempts()
        {
            if (File.Exists(studentAttemptsJson))
            {
                try
                {
                    var jsonData = File.ReadAllText(studentAttemptsJson);
                    existingAttempts = JsonConvert.DeserializeObject<List<StudentAttempt>>(jsonData) ?? new List<StudentAttempt>();
                }
                catch (Exception ex)
                {
                    msg = $"Error loading existing attempts: {ex.Message}";
                    ShowMessage(msg);
                    existingAttempts = new List<StudentAttempt>();
                }
            }
        }

        // Method to display messages to the user
        private void ShowMessage(string message)
        {
            // Assuming you have a custom msgbox form to show messages
            msgbox mb = new msgbox(message);
            mb.ShowDialog();
        }

        //private void backbtn_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //}

        private void backbtn_Click_1(object sender, EventArgs e)
        {

            //this.Hide();
            this.Hide();
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

    // Define the StudentAttempt class
    //public class StudentAttempt
    //{
    //    public string StudentId { get; set; }
    //    public string SubjectCode { get; set; }
    //    public List<AttemptDetail> Attempts { get; set; }
    //}

    // Define the AttemptDetail class for nested attempt data
    public class AttemptDetail
    {
        public int AttemptNumber { get; set; }
        public string GradePoints { get; set; }
        public string Credits { get; set; }
        public string Timestamp { get; set; }
    }
}