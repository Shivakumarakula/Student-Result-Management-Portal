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
//    public partial class batch_result : Form

//    {
//        private string excelPath;

//        private const string subjectCodesJson = "student_academicyear.json";

//        public batch_result()
//        {
//            InitializeComponent();
//        }

//        private void btnnext_Click(object sender, EventArgs e)
//        {

//        }
//    }

//    public class StudentAcademic
//    {
//        public string studentid { get; set; }
//        public string academic_year { get; set; }
//    }

//}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace project_RYS.Forms
{
    public partial class batch_result : Form
    {
        private string excelPath;
        private const string studentAcademicJson = "student_academicyear.json";

        public batch_result()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Important for EPPlus
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelPath = openFileDialog.FileName;
                ProcessAcademicYearExcel(excelPath, studentAcademicJson);
            }
        }

        //public void ProcessAcademicYearExcel(string excelPath, string jsonPath)
        //{
        //    var newStudents = new List<StudentAcademic>();

        //    // ✅ Read Excel File
        //    using (var package = new ExcelPackage(new FileInfo(excelPath)))
        //    {
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //        int rowCount = worksheet.Dimension.Rows;

        //        for (int row = 2; row <= rowCount; row++)
        //        {
        //            string studentId = worksheet.Cells[row, 1].Text.Trim().ToUpper();
        //            string academicYear = worksheet.Cells[row, 2].Text.Trim();

        //            if (!string.IsNullOrWhiteSpace(studentId) && !string.IsNullOrWhiteSpace(academicYear))
        //            {
        //                newStudents.Add(new StudentAcademic
        //                {
        //                    studentid = studentId,
        //                    academic_year = academicYear
        //                });
        //            }
        //        }
        //    }

        //    // ✅ Load existing JSON if it exists
        //    List<StudentAcademic> existingStudents;
        //    if (File.Exists(jsonPath))
        //    {
        //        string existingJson = File.ReadAllText(jsonPath);
        //        existingStudents = JsonConvert.DeserializeObject<List<StudentAcademic>>(existingJson) ?? new List<StudentAcademic>();
        //    }
        //    else
        //    {
        //        existingStudents = new List<StudentAcademic>();
        //    }

        //    // 🔄 Update or append
        //    foreach (var newStudent in newStudents)
        //    {
        //        var existing = existingStudents.FirstOrDefault(s => s.studentid == newStudent.studentid);
        //        if (existing != null)
        //        {
        //            // Update academic year if needed
        //            if (existing.academic_year != newStudent.academic_year)
        //            {
        //                existing.academic_year = newStudent.academic_year;
        //            }
        //        }
        //        else
        //        {
        //            existingStudents.Add(newStudent);
        //        }
        //    }

        //    // 💾 Save back to JSON
        //    File.WriteAllText(jsonPath, JsonConvert.SerializeObject(existingStudents, Formatting.Indented));
        //    MessageBox.Show("Academic year data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    status.Text = "Academic year data updated successfully!";
        //}


        public void ProcessAcademicYearExcel(string excelPath, string jsonPath)
        {
            var newStudents = new List<StudentAcademic>();

            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string studentId = worksheet.Cells[row, 1].Text.Trim();
                    string academicYear = worksheet.Cells[row, 2].Text.Trim();

                    // Skip fully empty row
                    if (string.IsNullOrWhiteSpace(studentId) && string.IsNullOrWhiteSpace(academicYear))
                        continue;

                    // If any field is missing but row has at least one non-empty cell
                    if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(academicYear))
                    {
                        MessageBox.Show($"Row {row}: Missing data.\nStudent ID: '{studentId}', Academic Year: '{academicYear}'",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    newStudents.Add(new StudentAcademic
                    {
                        studentid = studentId.ToUpper(),
                        academic_year = academicYear
                    });
                }
            }

            // Load existing data
            List<StudentAcademic> existingStudents = new List<StudentAcademic>();
            if (File.Exists(jsonPath))
            {
                string existingJson = File.ReadAllText(jsonPath);
                existingStudents = JsonConvert.DeserializeObject<List<StudentAcademic>>(existingJson) ?? new List<StudentAcademic>();
            }

            // Update or Add
            foreach (var newStudent in newStudents)
            {
                var existing = existingStudents.FirstOrDefault(s => s.studentid == newStudent.studentid);
                if (existing != null)
                {
                    if (existing.academic_year != newStudent.academic_year)
                    {
                        existing.academic_year = newStudent.academic_year;
                    }
                }
                else
                {
                    existingStudents.Add(newStudent);
                }
            }

            // Save back to JSON
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(existingStudents, Formatting.Indented));
            MessageBox.Show("Academic year data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            status.Text = "Academic year data updated successfully!";
        }

        private void btnstuid_Click(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();   
        }
    }

    public class StudentAcademic
    {
        public string studentid { get; set; }
        public string academic_year { get; set; }
    }
}
