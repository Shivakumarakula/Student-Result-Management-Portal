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

namespace project_RYS.Forms
{
    public partial class uploadnamesForm : Form
    {

        private string stunamesFilePath;

        private const string stunamesJson = "studentnames.json";
        private List<stunames> existingSubjectCodes = new List<stunames>();
        string msg = "";
        public uploadnamesForm()
        {
            InitializeComponent();
            lblStudentFileName.Text = "";
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Set based on your usage
            LoadExistingSubjectCodes(); // Load existing data on form initialization
        }
        private void LoadExistingSubjectCodes()
        {
            if (File.Exists(stunamesJson))
            {
                var jsonData = File.ReadAllText(stunamesJson);
                existingSubjectCodes = JsonConvert.DeserializeObject<List<stunames>>(jsonData) ?? new List<stunames>();
            }
        }


        //private List<stunames> ReadSubjectCodes(string filePath)
        //{
        //    var subjects = new List<stunames>();

        //    using (var package = new ExcelPackage(new FileInfo(filePath)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0];
        //        int rowCount = worksheet.Dimension.Rows;

        //        for (int row = 2; row <= rowCount; row++) // Assuming first row is header
        //        {
        //            var subject = new stunames
        //            {
        //                studentid = worksheet.Cells[row, 1].Text.ToUpper(),

        //                name= worksheet.Cells[row, 2].Text,
        //                //Semester = worksheet.Cells[row, 3].Text
        //            };
        //            subjects.Add(subject);
        //        }
        //    }

        //    return subjects;
        //}

        private List<stunames> ReadSubjectCodes(string filePath)
        {
            var students = new List<stunames>();
            var incompleteRows = new List<int>(); // Keep track of partially filled rows

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Skip header
                {
                    string studentid = worksheet.Cells[row, 1].Text?.Trim().ToUpper();
                    string name = worksheet.Cells[row, 2].Text?.Trim();

                    // Skip completely empty rows
                    if (string.IsNullOrWhiteSpace(studentid) && string.IsNullOrWhiteSpace(name))
                        continue;

                    // If one or more fields are missing, track this row and skip
                    if (string.IsNullOrWhiteSpace(studentid) || string.IsNullOrWhiteSpace(name))
                    {
                        incompleteRows.Add(row);
                        continue;
                    }

                    // Valid row
                    students.Add(new stunames
                    {
                        studentid = studentid,
                        name = name
                    });
                }
            }

            // Show message if any incomplete rows were skipped
            if (incompleteRows.Any())
            {
                string message = "The following rows have missing data and were skipped:\n" +
                                 string.Join(", ", incompleteRows);
                MessageBox.Show(message, "Incomplete Rows Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return students;
        }



        private void btnUploadnames_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Filter to allow only Excel files to be selected
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(openFileDialog.FileName)?.ToLower();

                    // Check if the selected file has a valid Excel file extension
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        stunamesFilePath = openFileDialog.FileName;
                        lblStudentFileName.Text = Path.GetFileName(stunamesFilePath);
                        lblStatus.Text = "Student Names uploaded successfully.";

                        // Read the subject codes from the selected file
                        var newSubjectCodes = ReadSubjectCodes(stunamesFilePath);
                        if (newSubjectCodes != null && newSubjectCodes.Any())
                        {
                            foreach (var newSubject in newSubjectCodes)
                            {
                                // Check if the subject already exists in the list
                                var existingSubject = existingSubjectCodes
                                    .FirstOrDefault(subj => subj.studentid == newSubject.studentid);

                                if (existingSubject != null)
                                {
                                    // Update existing subject information
                                    existingSubject.name = newSubject.name;
                                    //existingSubject.Semester = newSubject.Semester;
                                }
                                else
                                {
                                    // Add new subject to the list
                                    existingSubjectCodes.Add(newSubject);
                                }
                            }

                            // Save updated subject codes to JSON file
                            File.WriteAllText(stunamesJson, JsonConvert.SerializeObject(existingSubjectCodes, Formatting.Indented));
                            msg = "Student Names have been updated successfully.";
                            msgbox mb = new msgbox(msg);
                            mb.Show();
                        }
                        else
                        {
                            // Display an error message if reading subject codes fails
                            msg = "Failed to read Student Names.";
                            msgbox mb = new msgbox(msg);
                            mb.ShowDialog();
                        }
                    }
                    else
                    {
                        // Show a message if the selected file is not a valid Excel file
                        msg = "Invalid file format. Please select an Excel file (*.xls or *.xlsx).";
                        msgbox mb = new msgbox(msg);
                        mb.ShowDialog();
                    }
                }
            }

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    public class stunames
    {
        public string studentid { get; set; }
        public string name { get; set; }
       
    }
}
