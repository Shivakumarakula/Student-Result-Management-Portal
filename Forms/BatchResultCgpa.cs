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
    public partial class BatchResultCgpa : Form
    {

        public BatchResultCgpa()
        {
            InitializeComponent();
            PopulateComboBoxes();
            this.Load += new EventHandler(BatchResultCgpa_Load);
            submitbtn.Click -= new EventHandler(submitbtn_Click);
            submitbtn.Click += new EventHandler(submitbtn_Click);
        }
        private void PopulateComboBoxes()
        {
            // Branches
            var marks = LoadJsonData<StudentMa>("studentMarks.json");
            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
            branchcombo.Items.AddRange(branches.ToArray());
            if (branches.Any()) branchcombo.SelectedIndex = 0;

            // Academic Years
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            acyearcombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
            if (acyearcombo.Items.Count > 0) acyearcombo.SelectedIndex = 0;
        }
        private string GetBranchFromStudentId(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
            studentId = studentId.ToUpper().Trim();
            if (studentId.Contains("5a66") || studentId.Contains("5A66"))
                return "CSM";
            else if (studentId.Contains("1a05") || studentId.Contains("1A05"))
                return "CSE";
            else if (studentId.Contains("5a05") || studentId.Contains("5A05"))
                return "CSE";

            else if (studentId.Contains("1a66") || studentId.Contains("1A66"))
                return "CSM";
            else if (studentId.Contains("1a01") || studentId.Contains("1A01"))
                return "CE";
            else if (studentId.Contains("5A01") || studentId.Contains("5a01"))
                return "CE";
            else if (studentId.Contains("1a02") || studentId.Contains("1A02"))
                return "EEE";
            else if (studentId.Contains("5A02") || studentId.Contains("5a02"))
                return "EEE";
            else if (studentId.Contains("1a03") || studentId.Contains("1A03"))
                return "ME";
            else if (studentId.Contains("5A03") || studentId.Contains("5a03"))
                return "ME";

            else if (studentId.Contains("1a25") || studentId.Contains("1A25"))
                return "MIE";
            else if (studentId.Contains("5A25") || studentId.Contains("5a25"))
                return "MIE";
            // Add more conditions here for other patterns as needed
            else
                return "Unknown"; // Default case if no pattern matches
        }
        private void submitbtn_Click(object sender, EventArgs e)
        {
            if (branchcombo.SelectedIndex == -1 || acyearcombo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select both branch and academic year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedBranch = branchcombo.SelectedItem.ToString();
            string selectedAcademicYear = acyearcombo.SelectedItem.ToString();

            BatchCgpaDisplay resultForm = new BatchCgpaDisplay(selectedBranch, selectedAcademicYear);
            resultForm.Show();
        }

        private void BatchResultCgpa_Load(object sender, EventArgs e)
        {
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {


            // Load academic years
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json")
                .Select(ay => ay.academic_year)
                .Distinct()
                .ToList();

        }
        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<T>();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<T>>(json);
                return data ?? new List<T>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<T>();
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void branchcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
