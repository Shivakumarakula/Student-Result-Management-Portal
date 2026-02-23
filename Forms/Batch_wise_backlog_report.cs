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
//    public partial class Batch_wise_backlog_report : Form
//    {
//        public Batch_wise_backlog_report()
//        {
//            InitializeComponent();
//        }

//        private void submitbtn_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class Batch_wise_backlog_report : Form
    {
        public Batch_wise_backlog_report()
        {
            InitializeComponent();
            PopulateComboBoxes();
            this.Load += new EventHandler(Batch_wise_backlog_report_Load);

            submitbtn.Click -= new EventHandler(submitbtn_Click);
            submitbtn.Click += new EventHandler(submitbtn_Click);
        }

        private void PopulateComboBoxes()
        {
            // Load student marks
            var marks = LoadJsonData<StudentMark>("studentMarks.json");
            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId))
                                .Distinct().OrderBy(b => b).ToList();
            branchcombo.Items.AddRange(branches.ToArray());
            if (branches.Any()) branchcombo.SelectedIndex = 0;

            // Load academic years
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            acyearcombo.Items.AddRange(academicYears.Select(ay => ay.academic_year)
                                .Distinct().OrderBy(ay => ay).ToArray());
            if (acyearcombo.Items.Count > 0) acyearcombo.SelectedIndex = 0;
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

            BatchBacklogreport reportForm = new BatchBacklogreport(selectedBranch, selectedAcademicYear);
            reportForm.Show();
        }

        private void Batch_wise_backlog_report_Load(object sender, EventArgs e)
        {
            // Can load defaults if needed
        }

        private string GetBranchFromStudentId(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
            studentId = studentId.ToUpper().Trim();

            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
            if (studentId.Contains("5A05") || studentId.Contains("1A05")) return "CSE";
            if (studentId.Contains("5A01") || studentId.Contains("1A01")) return "CE";
            if (studentId.Contains("5A02") || studentId.Contains("1A02")) return "EEE";
            if (studentId.Contains("5A03") || studentId.Contains("1A03")) return "ME";
            if (studentId.Contains("5A25") || studentId.Contains("1A25")) return "MIE";

            return "Unknown";
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found: {filePath}");
                return new List<T>();
            }
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        private void Batch_wise_backlog_report_Load_1(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            admindashboard ab
           = new admindashboard();

            this.Close();
            Application.OpenForms["admindashboard"]?.Close();
            ab.Show();
        }
    }

    // Data classes
    //public class StudentMark
    //{
    //    public string StudentId { get; set; }
    //    public string SubjectCode { get; set; }
    //    public string SubjectName { get; set; }
    //    public string Internal { get; set; }
    //    public string External { get; set; }
    //    public string Total { get; set; }
    //    public string Grade { get; set; }
    //    public string GradePoints { get; set; }
    //    public string Credits { get; set; }
    //}

    //public class AcademicYear
    //{
    //    public string studentid { get; set; }
    //    public string academic_year { get; set; }
    //}
}
