using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class extra_form : Form
    {
        public extra_form()
        {
            InitializeComponent();
        }

        //private void downloadbtn_Click(object sender, EventArgs e)
        //{

        //}
        private void downloadbtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save Sample Excel File",
                FileName = "SampleDataStructure.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    // Worksheet 1: studentnames.json
                    var wsStudentNames = workbook.Worksheets.Add("studentnames");
                    wsStudentNames.Cell(1, 1).Value = "studentid";
                    wsStudentNames.Cell(1, 2).Value = "name";
                    wsStudentNames.Row(1).Style.Font.SetBold();
                    // Sample data
                    wsStudentNames.Cell(2, 1).Value = "23VD5A6612";
                    wsStudentNames.Cell(2, 2).Value = "John Doe";
                    wsStudentNames.Cell(3, 1).Value = "23VD1A0510";
                    wsStudentNames.Cell(3, 2).Value = "Jane Smith";
                    wsStudentNames.Columns().AdjustToContents();

                    // Worksheet 2: student_academicyear.json
                    var wsAcademicYear = workbook.Worksheets.Add("student_academicyear");
                    wsAcademicYear.Cell(1, 1).Value = "studentid";
                    wsAcademicYear.Cell(1, 2).Value = "academic_year";
                    wsAcademicYear.Row(1).Style.Font.SetBold();
                    // Sample data
                    wsAcademicYear.Cell(2, 1).Value = "21VD1A6612";
                    wsAcademicYear.Cell(2, 2).Value = "2021-2025";
                    wsAcademicYear.Cell(3, 1).Value = "23VD1A0510";
                    wsAcademicYear.Cell(3, 2).Value = "2023-2026";
                    wsAcademicYear.Columns().AdjustToContents();

                    // Worksheet 3: studentMarks.json
                    var wsStudentMarks = workbook.Worksheets.Add("studentMarks");
                    wsStudentMarks.Cell(1, 1).Value = "StudentId";
                    wsStudentMarks.Cell(1, 2).Value = "SubjectCode";
                    wsStudentMarks.Cell(1, 3).Value = "SubjectName";
                    wsStudentMarks.Cell(1, 4).Value = "Internal";
                    wsStudentMarks.Cell(1, 5).Value = "External";
                    wsStudentMarks.Cell(1, 6).Value = "Total";
                    wsStudentMarks.Cell(1, 7).Value = "Grade";
                    wsStudentMarks.Cell(1, 8).Value = "GradePoints";
                    wsStudentMarks.Cell(1, 9).Value = "Credits";
                    wsStudentMarks.Row(1).Style.Font.SetBold();
                    // Sample data
                    wsStudentMarks.Cell(2, 1).Value = "21VD1A6612";
                    wsStudentMarks.Cell(2, 2).Value = "18319";
                    wsStudentMarks.Cell(2, 3).Value = "Mathematics";
                    wsStudentMarks.Cell(2, 4).Value = "18";
                    wsStudentMarks.Cell(2, 5).Value = "54";
                    wsStudentMarks.Cell(2, 6).Value = "72";
                    wsStudentMarks.Cell(2, 7).Value = "A";
                    wsStudentMarks.Cell(2, 8).Value = "8";
                    wsStudentMarks.Cell(2, 9).Value = "4";
                    wsStudentMarks.Cell(3, 1).Value = "23VD1A0510";
                    wsStudentMarks.Cell(3, 2).Value = "18320";
                    wsStudentMarks.Cell(3, 3).Value = "Physics";
                    wsStudentMarks.Cell(3, 4).Value = "15";
                    wsStudentMarks.Cell(3, 5).Value = "45";
                    wsStudentMarks.Cell(3, 6).Value = "60";
                    wsStudentMarks.Cell(3, 7).Value = "B";
                    wsStudentMarks.Cell(3, 8).Value = "7";
                    wsStudentMarks.Cell(3, 9).Value = "3";
                    wsStudentMarks.Columns().AdjustToContents();

                    // Worksheet 4: subjectCodes.json
                    var wsSubjectCodes = workbook.Worksheets.Add("subjectCodes");
                    wsSubjectCodes.Cell(1, 1).Value = "SubjectCode";
                    wsSubjectCodes.Cell(1, 2).Value = "Year";
                    wsSubjectCodes.Cell(1, 3).Value = "Semester";
                    wsSubjectCodes.Cell(1, 4).Value = "Credits";
                    wsSubjectCodes.Row(1).Style.Font.SetBold();
                    // Sample data
                    wsSubjectCodes.Cell(2, 1).Value = "18319";
                    wsSubjectCodes.Cell(2, 2).Value = "2";
                    wsSubjectCodes.Cell(2, 3).Value = "1";
                    wsSubjectCodes.Cell(2, 4).Value = "4";
                    wsSubjectCodes.Cell(3, 1).Value = "18320";
                    wsSubjectCodes.Cell(3, 2).Value = "2";
                    wsSubjectCodes.Cell(3, 3).Value = "1";
                    wsSubjectCodes.Cell(3, 4).Value = "3.5";
                    wsSubjectCodes.Columns().AdjustToContents();

                    // Save the file
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Sample Excel file downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.OpenForms["admindashboard"]?.Close();
            admindashboard adb = new admindashboard();
            adb.Show();

            try
            {
                StudentAttemptbackup child = new StudentAttemptbackup();
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adb = new admindashboard();
            Application.OpenForms["admindashboard"]?.Close();
            adb.Show();

            try
            {
                getbtn child = new getbtn();
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

        private void swgpands_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adb = new admindashboard();
            Application.OpenForms["admindashboard"]?.Close();
            adb.Show();

            try
            {
                subject_wise_Gp child = new subject_wise_Gp();
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adb = new admindashboard();
            Application.OpenForms["admindashboard"]?.Close();
            adb.Show();

            try
            {
                Batch_wise_backlog_report child = new Batch_wise_backlog_report();
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
}
