using Newtonsoft.Json;
using OfficeOpenXml;
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

using static project_RYS.uploadform;

namespace project_RYS.Forms
{
    public partial class FormUploadSubjectCodes : Form
    {

        private string subjectCodesFilePath;

        private const string subjectCodesJson = "subjectCodes.json";




        private List<Subject> existingSubjectCodes = new List<Subject>();
        string msg = "";

        public FormUploadSubjectCodes()
        {
            InitializeComponent();

            lblSubjectFileName.Text = "";
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Set based on your usage
            LoadExistingSubjectCodes(); // Load existing data on form initialization
        }

        // Method to load existing subject codes from the JSON file
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

        }


        //private List<Subject> ReadSubjectCodes(string filePath)
        //{
        //    var subjects = new List<Subject>();

        //    try
        //    {
        //        using (var package = new ExcelPackage(new FileInfo(filePath)))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            int rowCount = worksheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++) // Assuming first row is header
        //            {
        //                var subject = new Subject
        //                {
        //                    SubjectCode = worksheet.Cells[row, 1].Text?.Trim(),
        //                    Year = worksheet.Cells[row, 2].Text?.Trim(),
        //                    Semester = worksheet.Cells[row, 3].Text?.Trim(),
        //                    Credits = worksheet.Cells[row, 4].Text?.Trim() // Read Credits from column 4
        //                };

        //                // Validate required fields
        //                if (!string.IsNullOrEmpty(subject.SubjectCode))
        //                {
        //                    subjects.Add(subject);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error reading subject codes: {ex.Message}  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //    return subjects;
        //}


        private List<Subject> ReadSubjectCodes(string filePath)
        {
            var subjects = new List<Subject>();
            var incompleteRows = new List<int>(); // Tracks rows with missing data

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Skip header row
                    {
                        string subjectCode = worksheet.Cells[row, 1].Text?.Trim();
                        string year = worksheet.Cells[row, 2].Text?.Trim();
                        string semester = worksheet.Cells[row, 3].Text?.Trim();
                        string credits = worksheet.Cells[row, 4].Text?.Trim();

                        // Skip row if all cells are empty
                        if (string.IsNullOrWhiteSpace(subjectCode) &&
                            string.IsNullOrWhiteSpace(year) &&
                            string.IsNullOrWhiteSpace(semester) &&
                            string.IsNullOrWhiteSpace(credits))
                        {
                            continue;
                        }

                        // If at least one cell is empty, track this row
                        if (string.IsNullOrWhiteSpace(subjectCode) ||
                            string.IsNullOrWhiteSpace(year) ||
                            string.IsNullOrWhiteSpace(semester) ||
                            string.IsNullOrWhiteSpace(credits))
                        {
                            incompleteRows.Add(row);
                            continue; // Skip this incomplete row from being added
                        }

                        // All required fields present, add to the list
                        subjects.Add(new Subject
                        {
                            SubjectCode = subjectCode,
                            Year = year,
                            Semester = semester,
                            Credits = credits
                        });
                    }
                }

                // Show message if there are any rows with missing values
                if (incompleteRows.Any())
                {
                    string message = "The following rows have missing data and were skipped:\n" +
                                     string.Join(", ", incompleteRows);
                    MessageBox.Show(message, "Incomplete Rows Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading subject codes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return subjects;
        }

        private void btnUploadSubjects_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(openFileDialog.FileName)?.ToLower();

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        subjectCodesFilePath = openFileDialog.FileName;
                        lblSubjectFileName.Text = Path.GetFileName(subjectCodesFilePath);
                        lblStatus.Text = "Subject codes uploaded successfully.";

                        var newSubjectCodes = ReadSubjectCodes(subjectCodesFilePath);
                        if (newSubjectCodes != null && newSubjectCodes.Any())
                        {
                            foreach (var newSubject in newSubjectCodes)
                            {
                                var existingSubject = existingSubjectCodes
                                    .FirstOrDefault(subj => subj.SubjectCode == newSubject.SubjectCode);

                                if (existingSubject != null)
                                {
                                    // Update existing subject information
                                    existingSubject.Year = newSubject.Year;
                                    existingSubject.Semester = newSubject.Semester;
                                    existingSubject.Credits = newSubject.Credits; // Update Credits
                                }
                                else
                                {
                                    // Add new subject to the list
                                    existingSubjectCodes.Add(newSubject);
                                }
                            }

                            // Save updated subject codes to JSON file
                            try
                            {
                                File.WriteAllText(subjectCodesJson, JsonConvert.SerializeObject(existingSubjectCodes, Formatting.Indented));
                                msg = "Subject codes have been updated successfully.";
                                msgbox mb = new msgbox(msg);
                                mb.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                msg = $"Failed to save subject codes: {ex.Message}";
                                msgbox mb = new msgbox(msg);
                                mb.ShowDialog();
                            }
                        }
                        else
                        {
                            msg = "Failed to read subject codes.";
                            msgbox mb = new msgbox(msg);
                            mb.ShowDialog();
                        }
                    }
                    else
                    {
                        msg = "Invalid file format. Please select an Excel file (*.xls or *.xlsx).";
                        msgbox mb = new msgbox(msg);
                        mb.ShowDialog();
                    }
                }
            }
        }
    

    private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblSubjectFileName_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            //this.Hide();
            admindashboard ab
            = new admindashboard();

            this.Close();
            Application.OpenForms["admindashboard"]?.Close();
            ab.Show();
        }
    }
}
