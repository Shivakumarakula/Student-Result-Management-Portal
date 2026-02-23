//using Newtonsoft.Json;
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
//namespace project_RYS.Forms
//{
//    public partial class BatchResult : Form
//    { 
//        public BatchResult()
//        {
//            InitializeComponent();
//            //LoadStudentMarks(); // Load existing student marks from the JSON file
//            this.Load += new EventHandler(BatchResult_Load);
//            subbtn.Click -= new EventHandler(subbtn_Click); // Hook up the button event


//        }
//        private void label4_Click(object sender, EventArgs e)
//        {

//        }

//        private void BatchResult_Load(object sender, EventArgs e)
//        {

//                LoadDropdowns();

//        }
//        private void LoadDropdowns()
//        {
//            // Load branches (hardcoded for now, could be dynamic)
//            branchcombo.Items.AddRange(new[] { "All", "CSM", "CSE", "CE" });
//            branchcombo.SelectedIndex = 0;

//            // Load academic years
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json")
//                .Select(ay => ay.academic_year)
//                .Distinct()
//                .ToList();
//            acyearcombo.Items.Add("All");
//            acyearcombo.Items.AddRange(academicYears.ToArray());
//            acyearcombo.SelectedIndex = 0;

//            // Load years and semesters from subjectCodes.json
//            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");
//            var years = subjectCodes.Select(sc => sc.Year).Distinct().ToList();
//            var semesters = subjectCodes.Select(sc => sc.Semester).Distinct().ToList();
//            yearcombo.Items.Add("All");
//            yearcombo.Items.AddRange(years.ToArray());
//            yearcombo.SelectedIndex = 0;
//            semcombo.Items.Add("All");
//            semcombo.Items.AddRange(semesters.ToArray());
//            semcombo.SelectedIndex = 0;
//        }

//        private void subbtn_Click(object sender, EventArgs e)
//        {
//            Console.WriteLine("Submitbtn_Click triggered1111");
//            if (branchcombo.SelectedIndex == -1 || acyearcombo.SelectedIndex == -1 ||
//                yearcombo.SelectedIndex == -1 || semcombo.SelectedIndex == -1)
//            {
//                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            string selectedBranch = branchcombo.SelectedItem.ToString();
//            string selectedAcademicYear = acyearcombo.SelectedItem.ToString();
//            string selectedYear = yearcombo.SelectedItem.ToString();
//            string selectedSemester = semcombo.SelectedItem.ToString();

//            // Check if there are results before opening the form
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");

//            var results = from mark in studentMarks
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//                          select new
//                          {
//                              StudentId = mark.StudentId,
//                              StudentName = name.name,
//                              AcademicYear = ay.academic_year,
//                              Year = sc.Year,
//                              Semester = sc.Semester,
//                              GradePoints = mark.GradePoints,
//                              Credits = mark.Credits
//                          };

//            var filteredResults = results.Where(r =>
//                (selectedBranch == "All" || GetBranchFromStudentId(r.StudentId) == selectedBranch) &&
//                (selectedAcademicYear == "All" || r.AcademicYear == selectedAcademicYear) &&
//                (selectedYear == "All" || r.Year == selectedYear) &&
//                (selectedSemester == "All" || r.Semester == selectedSemester)
//            );

//            if (!filteredResults.Any())
//            {
//                MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                Console.WriteLine("Submitbtn_Click triggered");

//                //this.Hide();

//                return; // Stay on the current form
//            }
//            Console.WriteLine("Submitbtn_Click triggered22222");
//            // Open result form only if there are results
//            BatchResultDisplaysgpa resultForm = new BatchResultDisplaysgpa(selectedBranch, selectedAcademicYear, selectedYear, selectedSemester);
//            Console.WriteLine("Submitbtn_Click triggered3333");
//            resultForm.Show();
//            Console.WriteLine("Submitbtn_Click triggered4444");
//        }


//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();
//            if (studentId.Contains("5a66") || studentId.Contains("5A66"))
//                return "CSM";
//            else if (studentId.Contains("1a05") || studentId.Contains("1A05"))
//                return "CSE";
//            else if (studentId.Contains("5a05") || studentId.Contains("5A05"))
//                return "CSE";

//            else if (studentId.Contains("1a66") || studentId.Contains("1A66"))
//                return "CSM";
//            else if (studentId.Contains("1a01") || studentId.Contains("1A01"))
//                return "CE";
//            else if (studentId.Contains("5A01") || studentId.Contains("5a01"))
//                return "CE";
//            // Add more conditions here for other patterns as needed
//            else
//                return "Unknown"; // Default case if no pattern matches
//        }


//        private void branchcombo_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }
//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                MessageBox.Show($"File not found: {filePath}");
//                return new List<T>();
//            }
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }
//    }
//    // Data classes
//    public class AcademicYear
//    {
//        public string studentid { get; set; }
//        public string academic_year { get; set; }
//    }

//    public class subj
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//    }
//}

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

namespace project_RYS.Forms
{
    public partial class BatchResult : Form
    {
        public BatchResult()
        {
            InitializeComponent();
            PopulateComboBoxes();
            this.Load += new EventHandler(BatchResult_Load);

            // Ensure the Click event is only attached once
            subbtn.Click -= new EventHandler(subbtn_Click); // Remove any existing handler
            subbtn.Click += new EventHandler(subbtn_Click); // Add it once
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

    

        private void label4_Click(object sender, EventArgs e)
        {
            // Empty method, can be removed unless needed
        }

        private void BatchResult_Load(object sender, EventArgs e)
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
            // Load years and semesters from subjectCodes.json
            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");
            var years = subjectCodes.Select(sc => sc.Year).Distinct().ToList();
            var semesters = subjectCodes.Select(sc => sc.Semester).Distinct().ToList();
        }

        private void subbtn_Click(object sender, EventArgs e)
        {
             Console.WriteLine("Submitbtn_Click triggered - Start");

            if (branchcombo.SelectedIndex == -1 || acyearcombo.SelectedIndex == -1 ||
                yearcombo.SelectedIndex == -1 || semcombo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Submitbtn_Click triggered - Validation failed");
                return;
            }

            string selectedBranch = branchcombo.SelectedItem.ToString();
            string selectedAcademicYear = acyearcombo.SelectedItem.ToString();
            string selectedYear = yearcombo.SelectedItem.ToString();
            string selectedSemester = semcombo.SelectedItem.ToString();

            // Check if there are results
            var studentNames = LoadJsonData<Student>("studentnames.json");
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");

            var results = from mark in studentMarks
                          join name in studentNames on mark.StudentId equals name.studentid
                          join ay in academicYears on mark.StudentId equals ay.studentid
                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
                          select new
                          {
                              StudentId = mark.StudentId,
                              StudentName = name.name,
                              AcademicYear = ay.academic_year,
                              Year = sc.Year,
                              Semester = sc.Semester,
                              GradePoints = mark.GradePoints,
                              SubjectCredits = sc.Credits // Use Credits from subjectCodes.json
                          };

            var filteredResults = results.Where(r =>
                (selectedBranch == "All" || GetBranchFromStudentId(r.StudentId) == selectedBranch) &&
                (selectedAcademicYear == "All" || r.AcademicYear == selectedAcademicYear) &&
                (selectedYear == "All" || r.Year == selectedYear) &&
                (selectedSemester == "All" || r.Semester == selectedSemester)
            );

            if (!filteredResults.Any())
            {
                MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Submitbtn_Click triggered - No results found");
                return;
            }

            Console.WriteLine("Submitbtn_Click triggered - Results found, opening form");

            // Open result form
            BatchResultDisplaysgpa resultForm = new BatchResultDisplaysgpa(selectedBranch, selectedAcademicYear, selectedYear, selectedSemester);
            resultForm.Show();

            Console.WriteLine("Submitbtn_Click triggered - Form shown, end of method");
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

        private void branchcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Empty method, can be removed unless needed
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

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void yearcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    // Data classes

    public class AcademicYear
    {
        public string studentid { get; set; }
        public string academic_year { get; set; }
    }
  
    public class subj
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Credits { get; set; }
    }

}



