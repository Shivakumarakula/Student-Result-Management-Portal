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
//    public partial class BackupNames : Form
//    {
//        private const string studentMarksJson = "studentnames.json"; // The file where student marks will be stored
//        private List<sss> existingMarks = new List<sss>(); // List to hold existing marks data
//        string msg = "";

//        public BackupNames()
//        {
//            InitializeComponent();
//            LoadExistingMarks(); // Load existing data if any
//        }

//        private void LoadExistingMarks()
//        {
//            if (File.Exists(studentMarksJson))
//            {
//                var jsonData = File.ReadAllText(studentMarksJson);
//                existingMarks = JsonConvert.DeserializeObject<List<sss>>(jsonData) ?? new List<sss>();
//            }
//        }

//        // Method to display messages to the user
//        private void ShowMessage(string message)
//        {
//            // Assuming you have a custom msgbox form to show messages
//            msgbox mb = new msgbox(message);
//            mb.ShowDialog();

//            // Alternatively, you can use the standard MessageBox like below:
//            // MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//    private void btnMarksUpload_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "JSON Files|*.json"; // Filter to show only .json files
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    string selectedFilePath = openFileDialog.FileName;
//                    try
//                    {
//                        // Read the JSON data from the selected file
//                        var jsonData = File.ReadAllText(selectedFilePath);
//                        var uploadedMarks = JsonConvert.DeserializeObject<List<sss>>(jsonData); // Deserialize to List of StudentMark objects

//                        // Validate the contents of the uploaded JSON file
//                        if (uploadedMarks != null && uploadedMarks.Any())
//                        {
//                            // Overwrite the existing JSON file with the new data
//                            File.WriteAllText(studentMarksJson, jsonData);
//                            existingMarks = uploadedMarks; // Update in-memory list with uploaded data
//                            msg = "Student names have been successfully restored from the backup.";
//                            ShowMessage(msg);
//                        }
//                        else
//                        {
//                            msg = "The uploaded JSON file does not contain valid student names.";
//                            ShowMessage(msg);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        msg = $"An error occurred while processing the file: {ex.Message}";
//                        ShowMessage(msg);
//                    }
//                }
//            }
//        }

//        private void BackupNames_Load(object sender, EventArgs e)
//        {
//            //this.Close();
//        }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//        }
//    }
//    public class sss
//    {
//        public string StudentId { get; set; }
//        public string Name { get; set; }

//    }
//}


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
    public partial class BackupNames : Form
    {
        private const string studentMarksJson = "studentnames.json";
        private List<sss> existingMarks = new List<sss>();
        string msg = "";

        public BackupNames()
        {
            InitializeComponent();
            LoadExistingMarks(); // Load existing data if any
        }

        private void LoadExistingMarks()
        {
            if (File.Exists(studentMarksJson))
            {
                var jsonData = File.ReadAllText(studentMarksJson);
                existingMarks = JsonConvert.DeserializeObject<List<sss>>(jsonData) ?? new List<sss>();
            }
        }

        private void ShowMessage(string message)
        {
            msgbox mb = new msgbox(message);
            mb.ShowDialog();
        }

        private void btnMarksUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files|*.json";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    try
                    {
                        var jsonData = File.ReadAllText(selectedFilePath);
                        var uploadedMarks = JsonConvert.DeserializeObject<List<sss>>(jsonData);

                        if (uploadedMarks != null && uploadedMarks.Any())
                        {
                            // Create a dictionary for quick update by StudentId
                            Dictionary<string, sss> updatedData = existingMarks.ToDictionary(x => x.StudentId, x => x);

                            foreach (var student in uploadedMarks)
                            {
                                if (!string.IsNullOrWhiteSpace(student.StudentId))
                                {
                                    // Update name if student exists or add new
                                    updatedData[student.StudentId] = student;
                                }
                            }

                            // Save merged data back to file
                            var mergedList = updatedData.Values.ToList();
                            File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(mergedList, Formatting.Indented));

                            existingMarks = mergedList;
                            msg = "Student names merged and updated successfully.";
                            ShowMessage(msg);
                        }
                        else
                        {
                            msg = "The uploaded JSON file does not contain valid student names.";
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

        private void BackupNames_Load(object sender, EventArgs e)
        {
            // Optional: Any logic to execute on load
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

    public class sss
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
    }
}
