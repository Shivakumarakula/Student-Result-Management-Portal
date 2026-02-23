

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class acyrjsonform : Form
    {
        private const string subjectCodesJson = "student_academicyear.json";
        private List<ac> existingSubjectCodes = new List<ac>();
        string msg = "";

        public acyrjsonform()
        {
            InitializeComponent();
            LoadExistingSubjectCodes();
        }

        private void LoadExistingSubjectCodes()
        {
            if (File.Exists(subjectCodesJson))
            {
                var jsonData = File.ReadAllText(subjectCodesJson);
                existingSubjectCodes = JsonConvert.DeserializeObject<List<ac>>(jsonData) ?? new List<ac>();
            }
        }

        private void acyrjsonform_Load(object sender, EventArgs e)
        {
            // Optional on load logic
        }

        private void btnUploadacyr_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    try
                    {
                        string jsonData = File.ReadAllText(selectedFilePath);
                        var uploadedData = JsonConvert.DeserializeObject<List<ac>>(jsonData);

                        if (uploadedData == null || uploadedData.Count == 0)
                        {
                            ShowStatus("The uploaded JSON file does not contain valid data.");
                            return;
                        }

                        int added = 0, updated = 0, skipped = 0;

                        foreach (var newEntry in uploadedData)
                        {
                            if (string.IsNullOrWhiteSpace(newEntry.studentid) && string.IsNullOrWhiteSpace(newEntry.academic_year))
                            {
                                // Skip completely empty row
                                continue;
                            }
                            else if (string.IsNullOrWhiteSpace(newEntry.studentid) || string.IsNullOrWhiteSpace(newEntry.academic_year))
                            {
                                skipped++;
                                continue;
                            }

                            var existingEntry = existingSubjectCodes.FirstOrDefault(x => x.studentid == newEntry.studentid);
                            if (existingEntry != null)
                            {
                                existingEntry.academic_year = newEntry.academic_year;
                                updated++;
                            }
                            else
                            {
                                existingSubjectCodes.Add(newEntry);
                                added++;
                            }
                        }

                        // Save the merged data
                        File.WriteAllText(subjectCodesJson, JsonConvert.SerializeObject(existingSubjectCodes, Formatting.Indented));

                        msg = $"Updated Academic Year Data:\nAdded: {added}, Updated: {updated}, Skipped (incomplete rows): {skipped}";
                        ShowStatus(msg);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus($"An error occurred while processing the file: {ex.Message}");
                    }
                }
            }
        }

        private void ShowStatus(string message)
        {
            lblStatus.Text = message;
            msgbox mb = new msgbox(message);
            mb.ShowDialog();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

    public class ac
    {
        public string studentid { get; set; }
        public string academic_year { get; set; }
    }
}
