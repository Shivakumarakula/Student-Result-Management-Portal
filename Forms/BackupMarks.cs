////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;

////namespace project_RYS.Forms
////{
////    public partial class BackupMarks : Form
////    {
////        public BackupMarks()
////        {
////            InitializeComponent();
////        }

////        private void label1_Click(object sender, EventArgs e)
////        {

////        }

////        private void btnMarksUpload_Click(object sender, EventArgs e)
////        {

////        }
////    }
//////}
////using static project_RYS.uploadform;
////using Newtonsoft.Json;
////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Windows.Forms;

////namespace project_RYS.Forms
////{
////    public partial class BackupMarks : Form
////    {
//        //private const string studentMarksJson = "studentMarks.json";
//        ////private const string marksJson = ".json"; // The file where student marks will be stored
//        //private List<StudentMarks> existingMarks = new List<StudentMark>(); // List to hold existing marks data
//        //string msg = "";

//        //public BackupMarks()
//        //{
//        //    InitializeComponent();
//        //    LoadExistingMarks(); // Load existing data if any
//        //}
//        //private void label1_Click(object sender, EventArgs e)
//        //{

//        //}

//        //private void btnMarksUpload_Click(object sender, EventArgs e)
//        //{
//        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
//        //    {
//        //        openFileDialog.Filter = "JSON Files|*.json";
//        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
//        //        {
//        //            string selectedFilePath = openFileDialog.FileName;
//        //            try
//        //            {
//        //                // Read the JSON data from the selected file
//        //                var jsonData = File.ReadAllText(selectedFilePath);
//        //                var uploadedMarks = JsonConvert.DeserializeObject<List<uploadform.StudentMark>>(jsonData);

//        //                // Validate the contents of the uploaded JSON file
//        //                if (uploadedMarks != null && uploadedMarks.Any())
//        //                {
//        //                    // Overwrite the existing JSON file with the new data
//        //                    File.WriteAllText(studentMarksJson, jsonData);
//        //                    existingMarks = uploadedMarks; // Update in-memory list if needed
//        //                    msg = "Student marks have been successfully restored from the backup.";
//        //                    ShowMessage(msg);
//        //                }
//        //                else
//        //                {
//        //                    msg = "The uploaded JSON file does not contain valid student marks.";
//        //                    ShowMessage(msg);
//        //                }
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                msg = $"An error occurred while processing the file: {ex.Message}";
//        //                ShowMessage(msg);
//        //            }
//        //        }
//        //    }
//        //}
//        // Method to load existing student marks from the JSON file
//        //    private void LoadExistingMarks()
//        //    {
//        //        if (File.Exists(studentMarksJson))
//        //        {
//        //            var jsonData = File.ReadAllText(studentMarksJson);
//        //            existingMarks = JsonConvert.DeserializeObject<List<StudentMarks>>(jsonData) ?? new List<StudentMarks>();
//        //        }
//        //    }


//        //    // Method to display messages to the user (using a custom msgbox or MessageBox)
//        //    private void ShowMessage(string message)
//        //    {
//        //        // Assuming you have a custom msgbox form
//        //        msgbox mb = new msgbox(message);
//        //        mb.ShowDialog();

//        //        // Alternatively, you can use the standard MessageBox like below:
//        //        // MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        //    }
//        //}

//        // Assuming StudentMarks class definition

//    //}



//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//    {
//        public partial class BackupMarks : Form
//        {
//            private const string studentMarksJson = "studentMarks.json"; // The file where student marks will be stored
//            private List<StudentMark> existingMarks = new List<StudentMark>(); // List to hold existing marks data
//            string msg = "";

//            public BackupMarks()
//            {
//                InitializeComponent();
//                LoadExistingMarks(); // Load existing data if any
//            }

//        // Button Click to upload the JSON file
//        private void btnMarksUpload_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "JSON Files|*.json";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    string selectedFilePath = openFileDialog.FileName;
//                    try
//                    {
//                        var jsonData = File.ReadAllText(selectedFilePath);
//                        var uploadedMarks = JsonConvert.DeserializeObject<List<StudentMark>>(jsonData);
//                        if (uploadedMarks == null || !uploadedMarks.Any())
//                        {
//                            ShowMessage("The uploaded file is empty or invalid.");
//                            return;
//                        }

//                        LoadExistingMarks(); // Load existing records

//                        List<StudentMark> finalMarks = new List<StudentMark>(existingMarks); // Start with current data
//                        List<string> updatesLog = new List<string>(); // Track updates

//                        foreach (var newMark in uploadedMarks)
//                        {
//                            if (string.IsNullOrWhiteSpace(newMark.StudentId) || string.IsNullOrWhiteSpace(newMark.SubjectCode))
//                                continue; // Skip invalid entries

//                            var existing = finalMarks.FirstOrDefault(m => m.StudentId == newMark.StudentId && m.SubjectCode == newMark.SubjectCode);
//                            if (existing != null)
//                            {
//                                if (
//                                    existing.SubjectName == newMark.SubjectName &&
//                                    existing.Internal == newMark.Internal &&
//                                    existing.External == newMark.External &&
//                                    existing.Total == newMark.Total &&
//                                    existing.Grade == newMark.Grade &&
//                                    existing.GradePoints == newMark.GradePoints &&
//                                    existing.Credits == newMark.Credits
//                                )
//                                {
//                                    // Same data, skip
//                                    continue;
//                                }
//                                else
//                                {
//                                    // Different data, update
//                                    string updateMsg = $"Updated: {existing.StudentId}-{existing.SubjectCode}\n" +
//                                                       $"Old: {existing.Internal}, {existing.External}, {existing.Total}, {existing.Grade}, {existing.GradePoints}, {existing.Credits}\n" +
//                                                       $"New: {newMark.Internal}, {newMark.External}, {newMark.Total}, {newMark.Grade}, {newMark.GradePoints}, {newMark.Credits}\n";
//                                    updatesLog.Add(updateMsg);

//                                    // Update values
//                                    existing.SubjectName = newMark.SubjectName;
//                                    existing.Internal = newMark.Internal;
//                                    existing.External = newMark.External;
//                                    existing.Total = newMark.Total;
//                                    existing.Grade = newMark.Grade;
//                                    existing.GradePoints = newMark.GradePoints;
//                                    existing.Credits = newMark.Credits;
//                                }
//                            }
//                            else
//                            {
//                                // New entry
//                                finalMarks.Add(newMark);
//                            }
//                        }

//                        // Write merged data
//                        File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(finalMarks, Formatting.Indented));
//                        existingMarks = finalMarks;

//                        if (updatesLog.Count == 0)
//                            ShowMessage("Marks uploaded successfully. No changes were needed.");
//                        else
//                            ShowMessage("Marks updated:\n\n" + string.Join("\n\n", updatesLog));
//                    }
//                    catch (Exception ex)
//                    {
//                        ShowMessage($"An error occurred: {ex.Message}");
//                    }
//                }
//            }
//        }

//        private void label1_Click(object sender, EventArgs e)
//        {

//        }
//        // Method to load existing student marks from the JSON file
//        private void LoadExistingMarks()
//            {
//                if (File.Exists(studentMarksJson))
//                {
//                    var jsonData = File.ReadAllText(studentMarksJson);
//                    existingMarks = JsonConvert.DeserializeObject<List<StudentMark>>(jsonData) ?? new List<StudentMark>();
//                }
//            }

//            // Method to display messages to the user
//            private void ShowMessage(string message)
//            {
//                // Assuming you have a custom msgbox form to show messages
//                msgbox mb = new msgbox(message);
//                mb.ShowDialog();

//                    }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//        }
//    }

//    // Define the StudentMark class
//    public class StudentMark
//        {
//            public string StudentId { get; set; }
//            //public string Name { get; set; }
//            public string SubjectCode { get; set; }
//            public string SubjectName { get; set; }
//            public string Internal { get; set; }  // Changed to decimal for proper numeric representation
//            public string External { get; set; }  // Changed to decimal for proper numeric representation
//            public string Total { get; set; }     // Changed to decimal for proper numeric representation
//            public string Grade { get; set; }
//            public string GradePoints { get; set; } // Changed to decimal for grade points
//            public string Credits { get; set; } // Changed to decimal for credits
//        }
//    }



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class BackupMarks : Form
    {
        private const string studentMarksJson = "studentMarks.json";
        private List<StudentMark> existingMarks = new List<StudentMark>();

        public BackupMarks()
        {
            InitializeComponent();
            LoadExistingMarks();
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
                        var uploadedMarks = JsonConvert.DeserializeObject<List<StudentMark>>(jsonData);

                        if (uploadedMarks != null && uploadedMarks.Any())
                        {
                            string messageLog = "";
                            int addedCount = 0;
                            int updatedCount = 0;
                            int skippedCount = 0;

                            foreach (var newMark in uploadedMarks)
                            {
                                // Skip empty StudentId or SubjectCode
                                if (string.IsNullOrWhiteSpace(newMark.StudentId) || string.IsNullOrWhiteSpace(newMark.SubjectCode))
                                    continue;

                                var existing = existingMarks.FirstOrDefault(m =>
                                    m.StudentId == newMark.StudentId && m.SubjectCode == newMark.SubjectCode);

                                if (existing == null)
                                {
                                    existingMarks.Add(newMark);
                                    addedCount++;
                                }
                                else if (!AreMarksEqual(existing, newMark))
                                {
                                    string oldData = $"StudentId: {existing.StudentId}, SubjectCode: {existing.SubjectCode}, Internal: {existing.Internal}, External: {existing.External}, Total: {existing.Total}, Grade: {existing.Grade}, GradePoints: {existing.GradePoints}, Credits: {existing.Credits}";
                                    string newData = $"Internal: {newMark.Internal}, External: {newMark.External}, Total: {newMark.Total}, Grade: {newMark.Grade}, GradePoints: {newMark.GradePoints}, Credits: {newMark.Credits}";

                                    messageLog += $"Updated: {existing.SubjectCode} for {existing.StudentId}\nOld: {oldData}\nNew: {newData}\n\n";

                                    // Update the existing mark
                                    existing.SubjectName = newMark.SubjectName;
                                    existing.Internal = newMark.Internal;
                                    existing.External = newMark.External;
                                    existing.Total = newMark.Total;
                                    existing.Grade = newMark.Grade;
                                    existing.GradePoints = newMark.GradePoints;
                                    existing.Credits = newMark.Credits;

                                    updatedCount++;
                                }
                                else
                                {
                                    skippedCount++;
                                }
                            }

                            // Save to JSON file
                            File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(existingMarks, Formatting.Indented));

                            // Show message summary
                            string resultMessage = $"Marks Uploaded Successfully.\n\n" +
                                                   $"➤ Added: {addedCount}\n" +
                                                   $"➤ Updated: {updatedCount}\n" +
                                                   $"➤ Skipped: {skippedCount} (No changes needed)\n\n";

                            ShowMessage(resultMessage + messageLog);
                        }
                        else
                        {
                            ShowMessage("The uploaded JSON file does not contain valid student marks.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"An error occurred while processing the file: {ex.Message}");
                    }
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool AreMarksEqual(StudentMark a, StudentMark b)
        {
            return a.StudentId == b.StudentId &&
                   a.SubjectCode == b.SubjectCode &&
                   a.SubjectName == b.SubjectName &&
                   a.Internal == b.Internal &&
                   a.External == b.External &&
                   a.Total == b.Total &&
                   a.Grade == b.Grade &&
                   a.GradePoints == b.GradePoints &&
                   a.Credits == b.Credits;
        }

        private void LoadExistingMarks()
        {
            if (File.Exists(studentMarksJson))
            {
                var jsonData = File.ReadAllText(studentMarksJson);
                existingMarks = JsonConvert.DeserializeObject<List<StudentMark>>(jsonData) ?? new List<StudentMark>();
            }
        }

        private void ShowMessage(string message)
        {
            msgbox mb = new msgbox(message); // Assuming you use a custom message box
            mb.ShowDialog();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

    public class StudentMark
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string GradePoints { get; set; }
        public string Credits { get; set; }
    }
}
