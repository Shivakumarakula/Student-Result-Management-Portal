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
//using Newtonsoft.Json;

//namespace project_RYS.Forms
//{
//    public partial class BackupForm : Form
//    {
//        private const string subjectCodesJson = "subjectCodes.json";
//        private List<Subject> existingSubjectCodes = new List<Subject>();
//        string msg = "";

//        public BackupForm()
//        {
//            InitializeComponent();
//            LoadExistingSubjectCodes(); // Load existing data if needed for comparison or initialization
//        }
//        // Method to load existing subject codes from the JSON file
//        private void LoadExistingSubjectCodes()
//        {
//            if (File.Exists(subjectCodesJson))
//            {
//                var jsonData = File.ReadAllText(subjectCodesJson);
//                existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonData) ?? new List<Subject>();
//            }
//        }

//        private void btnUploadSubjects_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "JSON Files|*.json";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    string selectedFilePath = openFileDialog.FileName;
//                    try
//                    {
//                        // Read the JSON data from the selected file
//                        var jsonData = File.ReadAllText(selectedFilePath);
//                        var uploadedSubjects = JsonConvert.DeserializeObject<List<Subject>>(jsonData);

//                        // Validate the contents of the uploaded JSON file
//                        if (uploadedSubjects != null && uploadedSubjects.Any())
//                        {
//                            // Overwrite the existing JSON file with the new data
//                            File.WriteAllText(subjectCodesJson, jsonData);
//                            existingSubjectCodes = uploadedSubjects; // Update in-memory list if needed
//                            msg = "Subject codes have been successfully restored from the backup.";
//                            ShowMessage(msg);
//                        }
//                        else
//                        {
//                            msg = "The uploaded JSON file does not contain valid subject codes.";
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
//        // Method to display messages to the user (assuming a msgbox form or MessageBox can be used)
//        private void ShowMessage(string message)
//        {
//            // Assuming you have a custom msgbox form
//            msgbox mb = new msgbox(message);
//            mb.ShowDialog();

//            // Alternatively, use the standard MessageBox if no custom form is available:
//            // MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        private void BackupForm_Load(object sender, EventArgs e)
//        {

//        }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace project_RYS.Forms
{
    public partial class BackupForm : Form
    {
        private const string subjectCodesJson = "subjectCodes.json";
        private List<Subject> existingSubjectCodes = new List<Subject>();

        public BackupForm()
        {
            InitializeComponent();
            LoadExistingSubjectCodes();
        }

        private void LoadExistingSubjectCodes()
        {
            if (File.Exists(subjectCodesJson))
            {
                var jsonData = File.ReadAllText(subjectCodesJson);
                existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonData) ?? new List<Subject>();
            }
        }

        private void btnUploadSubjects_Click(object sender, EventArgs e)
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
                        var uploadedSubjects = JsonConvert.DeserializeObject<List<Subject>>(jsonData);

                        if (uploadedSubjects != null && uploadedSubjects.Any())
                        {
                            List<string> added = new List<string>();
                            List<string> updatedMessages = new List<string>();

                            foreach (var newSubj in uploadedSubjects)
                            {
                                if (string.IsNullOrWhiteSpace(newSubj.SubjectCode)) continue;

                                var existing = existingSubjectCodes.FirstOrDefault(s =>
                                    s.SubjectCode.Equals(newSubj.SubjectCode, StringComparison.OrdinalIgnoreCase));

                                if (existing != null)
                                {
                                    // Check if any field is different
                                    if (existing.Year != newSubj.Year ||
                                        existing.Semester != newSubj.Semester ||
                                        existing.Credits != newSubj.Credits)
                                    {
                                        string msg = $"SubjectCode: {existing.SubjectCode}\n" +
                                                     $"Old Data: Year-{existing.Year}, Semester-{existing.Semester}, Credits-{existing.Credits}\n" +
                                                     $"Updated Data: Year-{newSubj.Year}, Semester-{newSubj.Semester}, Credits-{newSubj.Credits}";

                                        // Update the existing data
                                        existing.Year = newSubj.Year;
                                        existing.Semester = newSubj.Semester;
                                        existing.Credits = newSubj.Credits;

                                        updatedMessages.Add(msg);
                                    }
                                    // Else, data is the same – skip
                                }
                                else
                                {
                                    existingSubjectCodes.Add(newSubj);
                                    added.Add(newSubj.SubjectCode);
                                }
                            }

                            // Save back to file
                            File.WriteAllText(subjectCodesJson, JsonConvert.SerializeObject(existingSubjectCodes, Formatting.Indented));

                            // Show message
                            string message = "✅ Backup completed.\n";

                            if (added.Count > 0)
                                message += $"\n🆕 Added ({added.Count}): {string.Join(", ", added)}\n";

                            if (updatedMessages.Count > 0)
                            {
                                message += $"\n✏️ Updated ({updatedMessages.Count}):\n\n";
                                message += string.Join("\n\n", updatedMessages);
                            }

                            if (added.Count == 0 && updatedMessages.Count == 0)
                                message += "\nNo changes detected. All data is already up to date.";

                            ShowMessage(message);
                        }
                        else
                        {
                            ShowMessage("The uploaded JSON file does not contain valid subject codes.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"An error occurred while processing the file:\n\n{ex.Message}");
                    }
                }
            }
        }


        private void ShowMessage(string message)
        {
            // If you have a custom msgbox, use it
            msgbox mb = new msgbox(message);
            mb.ShowDialog();

            // Or use default MessageBox:
            // MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BackupForm_Load(object sender, EventArgs e)
        {
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

    //public class Subject
    //{
    //    public string SubjectCode { get; set; }
    //    public string Year { get; set; }
    //    public string Semester { get; set; }
    //    public string Credits { get; set; }
    //}
}
