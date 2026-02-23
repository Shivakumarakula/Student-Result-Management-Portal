using Newtonsoft.Json;
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
using static project_RYS.uploadform;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace project_RYS
{
    public partial class studentpage : Form
    {
        private const string studentMarksJson = "studentMarks.json"; // JSON file path for student marks
        private const string subjectCodesJson = "subjectCodes.json"; // JSON file path for subject codes

        public studentpage()
        {
            InitializeComponent();
        }

        private void btnFetchResults_Click(object sender, EventArgs e)
        {
           
                string studentId = txtStudentId.Text.Trim();

                if (string.IsNullOrEmpty(studentId))
                {
                    MessageBox.Show("Please enter a Student ID.");
                    return;
                }



            // Load existing student marks
            List<uploadform.StudentMark> studentMarks = LoadStudentMarks();
                if (studentMarks == null || !studentMarks.Any())
                {
                    MessageBox.Show("No student marks data found.");
                    return;
                }
            // Filter student marks based on Student ID
            var filteredMarks11 = studentMarks.Where(m => m.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase)).ToList();

            // Check if any marks were found for the given student ID
            if (!filteredMarks11.Any())
            {
                MessageBox.Show($"No records found for the Student ID '{studentId}'. Please check and try again.");
                return;
            }
            // Load subject codes
            List<Subject> subjectCodes = LoadSubjectCodes();
                if (subjectCodes == null || !subjectCodes.Any())
                {
                    MessageBox.Show("No subject codes data found.");
                    return;
                }

                // Filter student marks based on Student ID
                var filteredMarks = studentMarks.Where(m => m.StudentId == studentId).ToList();

                // Prepare results for DataGridView
                var resultList = new List<ResultViewModel>();

                foreach (var mark in filteredMarks)
                {
                    // Find subject information based on the subject code
                    var subject = subjectCodes.FirstOrDefault(s => s.SubjectCode == mark.SubjectCode);
                    if (subject != null)
                    {
                        resultList.Add(new ResultViewModel
                        {
                            //SubjectName= mark.SubjectName,

                            SubjectCode = mark.SubjectCode,
                            SubjectYear = subject.Year,
                            SubjectSemester = subject.Semester,
                            Internal = mark.Internal,
                            External = mark.External,
                            Total = mark.Total,
                            Grade = mark.Grade,
                            Credits = mark.Credits
                        });
                    }
                }

                // Bind results to DataGridView
                dgvResults.DataSource = resultList;
            }

            // Load existing student marks from JSON
            private List<uploadform.StudentMark> LoadStudentMarks()
            {
                if (File.Exists(studentMarksJson))
                {
                    var json = File.ReadAllText(studentMarksJson);
                    return JsonConvert.DeserializeObject<List<uploadform.StudentMark>>(json);
                }
                return null;
            }

            // Load existing subject codes from JSON
            private List<Subject> LoadSubjectCodes()
            {
                if (File.Exists(subjectCodesJson))
                {
                    var json = File.ReadAllText(subjectCodesJson);
                    return JsonConvert.DeserializeObject<List<Subject>>(json);
                }
                return null;
            }

// ViewModel for displaying results in DataGridView
public class ResultViewModel
        {
            public string SubjectName { get; set; }
            public string SubjectCode { get; set; }
            public string SubjectYear { get; set; }
            public string SubjectSemester { get; set; }
            public string Internal { get; set; }
            public string External { get; set; }
            public string Total { get; set; }
            public string Grade { get; set; }
            public string Credits { get; set; }
        }
     
        //CSV.....


        //private void ExportToCsv(string filePath, DataGridView dataGridView)
        //{
        //    var sb = new StringBuilder();

        //    // Get column headers
        //    var headers = dataGridView.Columns.Cast<DataGridViewColumn>();
        //    sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"")));

        //    // Get row data
        //    foreach (DataGridViewRow row in dataGridView.Rows)
        //    {
        //        // Skip the new row placeholder, if exists
        //        if (row.IsNewRow) continue;

        //        var cells = row.Cells.Cast<DataGridViewCell>()
        //            .Select(cell => "\"" + (cell.Value?.ToString() ?? "") + "\"");
        //        sb.AppendLine(string.Join(",", cells));
        //    }

        //    File.WriteAllText(filePath, sb.ToString());
        //    MessageBox.Show("Data exported to CSV successfully.");
        //}



        //PDF.....


        //private void ExportToPdf(string filePath, DataGridView dataGridView)
        //{
        //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        Document pdfDoc = new Document();
        //        PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();

        //        // Add title
        //        pdfDoc.Add(new Paragraph("Student Results"));

        //        // Create a table with the same number of columns as the DataGridView
        //        PdfPTable pdfTable = new PdfPTable(dataGridView.Columns.Count);

        //        // Add column headers
        //        foreach (DataGridViewColumn column in dataGridView.Columns)
        //        {
        //            pdfTable.AddCell(new Phrase(column.HeaderText));
        //        }

        //        // Add row data
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            foreach (DataGridViewCell cell in row.Cells)
        //            {
        //                pdfTable.AddCell(new Phrase(cell.Value?.ToString() ?? ""));
        //            }
        //        }

        //        pdfDoc.Add(pdfTable);
        //        pdfDoc.Close();
        //        MessageBox.Show("Data exported to PDF successfully.");
        //    }
        //}

        private void download_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog
            //{
            //    Filter = "CSV Files (*.csv)|*.csv",
            //    Title = "Save a CSV File"
            //};

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    ExportToCsv(saveFileDialog.FileName, dgvResults);
            //}
            //SaveFileDialog saveFileDialog = new SaveFileDialog
            //{
            //    Filter = "PDF Files (*.pdf)|*.pdf",
            //    Title = "Save a PDF File"
            //};

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    ExportToPdf(saveFileDialog.FileName, dgvResults);
            //}

        }
    }
}
