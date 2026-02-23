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
using static project_RYS.uploadform;

namespace project_RYS.Forms
{

   
    public partial class FormBacklogreport : Form
    {
        private string studentId;
        //private List<IGrouping<dynamic, dynamic>> groupedBacklogs;
        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouping by YearSem as a single string
        //private int currentGroupIndex = 0;
        //int backlogs;
        public FormBacklogreport(string studentId)
        {
            InitializeComponent();
            this.studentId = studentId;

            LoadBacklogData();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //    private void LoadBacklogData()
        //    {
        //        // Load JSON files
        //        var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
        //        var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

        //        // Debugging: Check loaded data
        //        Console.WriteLine("Loaded Student Marks:");
        //        foreach (var mark in studentMarks)
        //        {
        //            Console.WriteLine($"ID: {mark.StudentId}, Subject: {mark.SubjectCode}, Grade: {mark.Grade}");
        //        }

        //        Console.WriteLine("Loaded Subject Codes:");
        //        foreach (var subject in subjectCodes)
        //        {
        //            Console.WriteLine($"Subject Code: {subject.SubjectCode}, Year: {subject.Year}, Semester: {subject.Semester}");
        //        }

        //        // Filter data for the specific student and grade criteria
        //        var backlogs = studentMarks
        //            .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
        //            .Join(subjectCodes,
        //                mark => mark.SubjectCode,
        //                subject => subject.SubjectCode,
        //                (mark, subject) => new Backlog
        //                {
        //                    SubjectCode = mark.SubjectCode,
        //                    SubjectName = mark.SubjectName,
        //                    Internal = mark.Internal,
        //                    External = mark.External,
        //                    Total = mark.Total,
        //                    Grade = mark.Grade,
        //                    Credits = mark.Credits,
        //                    YearSem = $"{subject.Year}-{subject.Semester}"  // Create a Year-Sem string
        //                })
        //            .ToList();

        //        // Check if backlogs are found
        //        Console.WriteLine($"Total Backlogs Found: {backlogs.Count}");

        //        if (backlogs.Count == 0)
        //        {
        //            MessageBox.Show("Zero backlogs found for this student.");
        //            lblYearSem.Text = "Total Backlogs: 0";
        //            dgvBacklogs.DataSource = null;
        //            return;
        //        }

        //        // Separate backlogs by Year-Sem
        //        groupedBacklogs = backlogs
        //            .GroupBy(b => b.YearSem) // Grouping by the YearSem string
        //            .OrderBy(g => g.Key) // Order by the YearSem string
        //            .ToList();

        //        DisplayCurrentGroup();
        //    }

        //    private void DisplayCurrentGroup()
        //    {
        //        if (groupedBacklogs == null || groupedBacklogs.Count == 0)
        //            return;

        //        var currentGroup = groupedBacklogs[currentGroupIndex];
        //        var yearSem = currentGroup.Key;

        //        // Set label to show current Year-Sem
        //        lblYearSem.Text = $"Year-Sem: {yearSem}";

        //        // Bind data to existing DataGridView without Year and Semester columns
        //        var data = currentGroup
        //            .Select(b => new
        //            {
        //                b.SubjectCode,
        //                b.SubjectName,
        //                b.Internal,
        //                b.External,
        //                b.Total,
        //                b.Grade,
        //                b.Credits
        //            })
        //            .ToList();

        //        dgvBacklogs.DataSource = data;

        //        // Set column headers
        //        dgvBacklogs.Columns[0].HeaderText = "Subject Code";
        //        dgvBacklogs.Columns[1].HeaderText = "Subject Name";
        //        dgvBacklogs.Columns[2].HeaderText = "Internal";
        //        dgvBacklogs.Columns[3].HeaderText = "External";
        //        dgvBacklogs.Columns[4].HeaderText = "Total";
        //        dgvBacklogs.Columns[5].HeaderText = "Grade";
        //        dgvBacklogs.Columns[6].HeaderText = "Credits";
        //    }

        //    private List<T> LoadJsonData<T>(string filePath)
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            MessageBox.Show($"File not found: {filePath}");
        //            return new List<T>();
        //        }

        //        var jsonData = File.ReadAllText(filePath);
        //        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        //    }
        //}

        //// Define your Backlog class here
        //public class Backlog
        //{
        //    public string SubjectCode { get; set; }
        //    public string SubjectName { get; set; }
        //    public string Internal { get; set; }
        //    public string External { get; set; }
        //    public string Total { get; set; }
        //    public string Grade { get; set; }
        //    public string Credits { get; set; }
        //    public string YearSem { get; set; }  // Combine Year and Semester as a single property
        //}

        //// Define your StudentMarks class here based on your JSON structure
        //public class StudentMarks
        //{
        //    public string StudentId { get; set; }
        //    public string SubjectCode { get; set; }
        //    public string SubjectName { get; set; }
        //    public string Internal { get; set; }
        //    public string External { get; set; }
        //    public string Total { get; set; }
        //    public string Grade { get; set; }
        //    public string Credits { get; set; }
        //}

        //// Define your Subject class here based on your JSON structure
        //public class Subject
        //{
        //    public string SubjectCode { get; set; }
        //    public string Year { get; set; }
        //    public string Semester { get; set; }
        //}


        // using buttons.........!!!!!!!!!!!!!11111



        //    private void LoadBacklogData()
        //    {
        //        // Load JSON files
        //        var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
        //        var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

        //        // Debugging: Check loaded data
        //        Console.WriteLine("Loaded Student Marks:");
        //        foreach (var mark in studentMarks)
        //        {
        //            Console.WriteLine($"ID: {mark.StudentId}, Subject: {mark.SubjectCode}, Grade: {mark.Grade}");
        //        }

        //        Console.WriteLine("Loaded Subject Codes:");
        //        foreach (var subject in subjectCodes)
        //        {
        //            Console.WriteLine($"Subject Code: {subject.SubjectCode}, Year: {subject.Year}, Semester: {subject.Semester}");
        //        }

        //        // Filter data for the specific student and grade criteria
        //        var backlogs = studentMarks
        //            .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
        //            .Join(subjectCodes,
        //                mark => mark.SubjectCode,
        //                subject => subject.SubjectCode,
        //                (mark, subject) => new Backlog
        //                {
        //                    SubjectCode = mark.SubjectCode,
        //                    SubjectName = mark.SubjectName,
        //                    Internal = mark.Internal,
        //                    External = mark.External,
        //                    Total = mark.Total,
        //                    Grade = mark.Grade,
        //                    Credits = mark.Credits,
        //                    YearSem = $"{subject.Year}-{subject.Semester}"  // Create a Year-Sem string
        //                })
        //            .ToList();

        //        // Check if backlogs are found
        //        Console.WriteLine($"Total Backlogs Found: {backlogs.Count}");

        //        if (backlogs.Count == 0)
        //        {
        //            MessageBox.Show("Zero backlogs found for this student.");
        //            lblYearSem.Text = "Total Backlogs: 0";
        //            dgvBacklogs.DataSource = null;
        //            return;
        //        }

        //        // Separate backlogs by Year-Sem
        //        groupedBacklogs = backlogs
        //            .GroupBy(b => b.YearSem) // Grouping by the YearSem string
        //            .OrderBy(g => g.Key) // Order by the YearSem string
        //            .ToList();

        //        DisplayCurrentGroup();
        //    }

        //    private void DisplayCurrentGroup()
        //    {
        //        if (groupedBacklogs == null || groupedBacklogs.Count == 0)
        //            return;

        //        var currentGroup = groupedBacklogs[currentGroupIndex];
        //        var yearSem = currentGroup.Key;

        //        // Set label to show current Year-Sem
        //        lblYearSem.Text = $"Year-Sem: {yearSem}";

        //        // Bind data to existing DataGridView
        //        var data = currentGroup
        //            .Select(b => new
        //            {
        //                b.SubjectCode,
        //                b.SubjectName,
        //                b.Internal,
        //                b.External,
        //                b.Total,
        //                b.Grade,
        //                b.Credits
        //            })
        //            .ToList();

        //        dgvBacklogs.DataSource = data;

        //        // Set column headers
        //        dgvBacklogs.Columns[0].HeaderText = "Subject Code";
        //        dgvBacklogs.Columns[1].HeaderText = "Subject Name";
        //        dgvBacklogs.Columns[2].HeaderText = "Internal";
        //        dgvBacklogs.Columns[3].HeaderText = "External";
        //        dgvBacklogs.Columns[4].HeaderText = "Total";
        //        dgvBacklogs.Columns[5].HeaderText = "Grade";
        //        dgvBacklogs.Columns[6].HeaderText = "Credits";

        //        // Update navigation buttons visibility
        //        btnPrevious.Enabled = currentGroupIndex > 0;
        //        btnNext.Enabled = currentGroupIndex < groupedBacklogs.Count - 1;
        //    }

        //    private void btnPrevious_Click(object sender, EventArgs e)
        //    {
        //        if (currentGroupIndex > 0)
        //        {
        //            currentGroupIndex--;
        //            DisplayCurrentGroup();
        //        }
        //    }

        //    private void btnNext_Click(object sender, EventArgs e)
        //    {
        //        if (currentGroupIndex < groupedBacklogs.Count - 1)
        //        {
        //            currentGroupIndex++;
        //            DisplayCurrentGroup();
        //        }
        //    }

        //    private List<T> LoadJsonData<T>(string filePath)
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            MessageBox.Show($"File not found: {filePath}");
        //            return new List<T>();
        //        }

        //        var jsonData = File.ReadAllText(filePath);
        //        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        //    }

        //    // Optional: You can implement a method to validate the student ID input
        //    private void ValidateStudentId(string studentId)
        //    {
        //        // Add validation logic here if needed
        //    }
        //}

        //// Define your Backlog class here
        //public class Backlog
        //{
        //    public string SubjectCode { get; set; }
        //    public string SubjectName { get; set; }
        //    public string Internal { get; set; }
        //    public string External { get; set; }
        //    public string Total { get; set; }
        //    public string Grade { get; set; }
        //    public string Credits { get; set; }
        //    public string YearSem { get; set; }  // Combine Year and Semester as a single property
        //}

        //// Define your StudentMarks class here based on your JSON structure
        //public class StudentMarks
        //{
        //    public string StudentId { get; set; }
        //    public string SubjectCode { get; set; }
        //    public string SubjectName { get; set; }
        //    public string Internal { get; set; }
        //    public string External { get; set; }
        //    public string Total { get; set; }
        //    public string Grade { get; set; }
        //    public string Credits { get; set; }
        //}

        //// Define your Subject class here based on your JSON structure
        //public class Subject
        //{
        //    public string SubjectCode { get; set; }
        //    public string Year { get; set; }
        //    public string Semester { get; set; }
        //}


        private void LoadBacklogData()
        {
            // Load JSON files
            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

            // Filter data for the specific student and grade criteria
            var backlogs = studentMarks
                .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
                .Join(subjectCodes,
                    mark => mark.SubjectCode,
                    subject => subject.SubjectCode,
                    (mark, subject) => new Backlog
                    {
                        SubjectCode = mark.SubjectCode,
                        SubjectName = mark.SubjectName,
                        Internal = mark.Internal,
                        External = mark.External,
                        Total = mark.Total,
                        Grade = mark.Grade,
                        Credits = mark.Credits,
                        YearSem = $"{subject.Year}-{subject.Semester}"  // Create a Year-Sem string
                    })
                .ToList();

            // Check if backlogs are found
            if (backlogs.Count == 0)
            {
                MessageBox.Show("Zero backlogs found for this student.");
                return;
            }

            // Separate backlogs by Year-Sem
            groupedBacklogs = backlogs
                .GroupBy(b => b.YearSem) // Grouping by the YearSem string
                .OrderBy(g => g.Key) // Order by the YearSem string
                .ToList();

            // Display each YearSem in a separate DataGridView
            DisplayYearSemDataGrids();
        }

        private void DisplayYearSemDataGrids()
        {
            // Clear existing DataGridViews if any
            Controls.Clear();

            int topPosition = 20; // Initial top position for the first DataGridView

            foreach (var group in groupedBacklogs)
            {
                // Create a new DataGridView for each YearSem
                DataGridView dgv = new DataGridView
                {
                    Name = $"dgv_{group.Key}",
                    Width = 600,
                    Height = 150,
                    Location = new System.Drawing.Point(20, topPosition), // Position it below the last one
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    AllowUserToAddRows = false
                };

                // Bind data to the DataGridView
                var data = group.Select(b => new
                {
                    b.SubjectCode,
                    b.SubjectName,
                    b.Internal,
                    b.External,
                    b.Total,
                    b.Grade,
                    b.Credits
                }).ToList();

                dgv.DataSource = data;

                // Set column headers
                dgv.Columns[0].HeaderText = "Subject Code";
                dgv.Columns[1].HeaderText = "Subject Name";
                dgv.Columns[2].HeaderText = "Internal";
                dgv.Columns[3].HeaderText = "External";
                dgv.Columns[4].HeaderText = "Total";
                dgv.Columns[5].HeaderText = "Grade";
                dgv.Columns[6].HeaderText = "Credits";

                // Create a label for the Year-Sem
                Label lblYearSem = new Label
                {
                    Text = $"Year-Sem: {group.Key}",
                    Location = new System.Drawing.Point(20, topPosition - 20), // Position above the DataGridView
                    AutoSize = true
                };

                // Add the label and DataGridView to the form
                Controls.Add(lblYearSem);
                Controls.Add(dgv);

                // Update the top position for the next DataGridView
                topPosition += 180; // Move down for the next DataGridView and label
            }
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found: {filePath}");
                return new List<T>();
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        }
    }

    // Define your Backlog class here
    public class Backlog
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string Credits { get; set; }
        public string YearSem { get; set; }  // Combine Year and Semester as a single property
    }

    // Define your StudentMarks class here based on your JSON structure
    public class StudentMarks
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string Credits { get; set; }
    }

    // Define your Subject class here based on your JSON structure
    public class Subject
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Credits { get; set; }
    }
}

