//using ClosedXML.Excel;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class BatchCgpaDisplay : Form
//    {
//        private Button btnDownload;
//        private Button btnBack;
//        private DataGridView dgvResults;
//        private string branchFilter;
//        private string academicYearFilter;
//        private Button backBtn;
//        double totalCredits = 0;
//        public BatchCgpaDisplay(string branch, string academicYear)
//        {
//            branchFilter = branch;
//            academicYearFilter = academicYear;
//            InitializeComponent();
//            LoadResults();
//            SetupForm();

//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1500, 1000);
//            this.Text = "Batch CGPA Results";
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240); // Light background

//            // Header Label
//            Label lblHeader = new Label
//            {
//                Text = "Batch CGPA Results",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid,
//                Location = new Point((this.ClientSize.Width - 300) / 2, 20)
//            };
//            this.Controls.Add(lblHeader);

//            // Branch and Academic Year Labels
//            Label lblBranch = new Label
//            {
//                Text = $"Branch: {branchFilter}",
//                Font = new Font("Arial", 12, FontStyle.Regular),
//                AutoSize = true,
//                Location = new Point(20, 60)
//            };
//            this.Controls.Add(lblBranch);

//            Label lblAcademicYear = new Label
//            {
//                Text = $"Academic Year: {academicYearFilter}",
//                Font = new Font("Arial", 12, FontStyle.Regular),
//                AutoSize = true,
//                Location = new Point(20, 85)
//            };
//            this.Controls.Add(lblAcademicYear);

//            // DataGridView
//            dgvResults = new DataGridView
//            {
//                Location = new Point(100, 120),
//                Size = new Size(1110, 460), // Full width and height minus margins
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                RowHeadersVisible = false,
//                AllowUserToAddRows = false,
//                ReadOnly = true,
//                ScrollBars = ScrollBars.Vertical, // Scrollbar if overflow
//                EnableHeadersVisualStyles = false,
//                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new Font("Arial", 10, FontStyle.Bold),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                },
//                RowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new Font("Arial", 10, FontStyle.Bold)
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkViolet
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };


//            dgvResults.Columns.Add("StudentId", "Student ID");
//            dgvResults.Columns.Add("StudentName", "Student Name");
//            dgvResults.Columns.Add("AcademicYear", "Academic Year");
//            dgvResults.Columns.Add("CGPA", "CGPA");
//            //dgvResults.Columns.Add("Credits", "Earned Credits");  // ✅ New column
//            dgvResults.Columns.Add("Credits", $"Earned Credits ({totalCredits})");

//            // Set column widths
//            dgvResults.Columns[0].Width = 200; // Student ID
//            dgvResults.Columns[1].Width = 350; // Student Name (wide)
//            dgvResults.Columns[2].Width = 200; // Academic Year
//            dgvResults.Columns[3].Width = 200; // CGPA
//            dgvResults.Columns[4].Width = 150; // Credits

//            // Set row and header height
//            dgvResults.RowTemplate.Height = 40;
//            dgvResults.ColumnHeadersHeight = 50;

//            this.Controls.Add(dgvResults);

//            // Download Button
//            btnDownload = new Button
//            {
//                Text = "Download Excel",
//                Location = new Point(530, 600),
//                Width = 150,
//                Height = 35,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            btnDownload.FlatAppearance.BorderColor = Color.DarkViolet;
//            btnDownload.Click += new EventHandler(BtnDownload_Click);
//            this.Controls.Add(btnDownload);


//            backBtn = new Button
//            {
//                Text = "Back To Home",
//                Location = new Point(860, 20),
//                Width = 200,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkViolet,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//            backBtn.Click += new EventHandler(backbtn_Click);
//            this.Controls.Add(backBtn);
//        }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();

//        }
//        private void BtnDownload_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog
//            {
//                Filter = "Excel Workbook|*.xlsx",
//                Title = "Save Batch CGPA Results as Excel",
//                FileName = "BatchResults_CGPA.xlsx"
//            };

//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                using (var workbook = new XLWorkbook())
//                {
//                    var worksheet = workbook.Worksheets.Add("Batch CGPA Results");

//                    // Add title
//                    worksheet.Cell(1, 1).Value = $"Batch CGPA Results - Branch: {branchFilter}, Academic Year: {academicYearFilter}";
//                    worksheet.Range("A1:D1").Merge();
//                    object value = worksheet.Cell(1, 1).Style.Font.SetBold();
//                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

//                    // Add headers
//                    for (int i = 0; i < dgvResults.Columns.Count; i++)
//                    {
//                        worksheet.Cell(2, i + 1).Value = dgvResults.Columns[i].HeaderText;
//                    }
//                    worksheet.Row(2).Style.Font.SetBold();
//                    worksheet.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkOrchid);
//                    worksheet.Row(2).Style.Font.SetFontColor(XLColor.White);

//                    // Add data
//                    for (int i = 0; i < dgvResults.Rows.Count; i++)
//                    {
//                        for (int j = 0; j < dgvResults.Columns.Count; j++)
//                        {
//                            worksheet.Cell(i + 3, j + 1).Value = dgvResults.Rows[i].Cells[j].Value?.ToString();
//                        }
//                    }

//                    // Set column widths to match DataGridView
//                    worksheet.Column(1).Width = 15; // Student ID
//                    worksheet.Column(2).Width = 30; // Student Name
//                    worksheet.Column(3).Width = 20; // Academic Year
//                    worksheet.Column(4).Width = 10; // CGPA

//                    workbook.SaveAs(saveFileDialog.FileName);
//                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }


//        private void LoadResults()
//        {
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

//            // Join data
//            var results = from mark in studentMarks
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join subject in subjectCodes on mark.SubjectCode equals subject.SubjectCode
//                          select new
//                          {
//                              StudentId = mark.StudentId,
//                              StudentName = name.name,
//                              AcademicYear = ay.academic_year,
//                              GradePoints = mark.GradePoints,
//                              SubjectCredits = subject.Credits,  // original subject credits
//                              EarnedCredits = mark.Credits       // earned credits (student-wise)
//                          };

//            // Apply filters
//            var filteredResults = results.Where(r =>
//                (branchFilter == "All" || GetBranchFromStudentId(r.StudentId) == branchFilter) &&
//                (academicYearFilter == "All" || r.AcademicYear == academicYearFilter)
//            );

//            // Group by student and calculate CGPA + total earned credits
//            var studentResults = filteredResults
//                .GroupBy(r => new { r.StudentId, r.StudentName, r.AcademicYear })
//                .Select(g =>
//                {
//                    double totalGradePoints = 0;
//                    double totalCreditsForCgpa = 0;
//                    double totalEarnedCredits = 0;

//                    foreach (var item in g)
//                    {
//                        double gradePoint = double.TryParse(item.GradePoints, out double gp) ? gp : 0;
//                        double subjectCredits = double.TryParse(item.SubjectCredits, out double cr) ? cr : 0;
//                        double earnedCredits = double.TryParse(item.EarnedCredits, out double ec) ? ec : 0;

//                        // For CGPA use subject credits
//                        totalGradePoints += gradePoint * subjectCredits;
//                        totalCreditsForCgpa += subjectCredits;

//                        // For Credits column use earned credits
//                        totalEarnedCredits += earnedCredits;
//                    }
//                    totalCredits = totalCreditsForCgpa;
//                    double cgpa = totalCreditsForCgpa > 0 ? totalGradePoints / totalCreditsForCgpa : 0;

//                    return new
//                    {
//                        g.Key.StudentId,
//                        g.Key.StudentName,
//                        g.Key.AcademicYear,
//                        CGPA = cgpa,
//                        Credits = totalEarnedCredits
//                    };

//                });

//            // Populate DataGridView
//            dgvResults.Rows.Clear();
//            foreach (var result in studentResults)
//            {
//                dgvResults.Rows.Add(
//                    result.StudentId,
//                    result.StudentName,
//                    result.AcademicYear,
//                    result.CGPA.ToString("F2"),
//                    result.Credits.ToString("F0")  // show as integer
//                );
//            }
//            // 🔹 Update column header with total original credits
//            dgvResults.Columns["Credits"].HeaderText = $"Earned Credits ({totalCredits})";
//            if (dgvResults.Rows.Count == 0)
//            {
//                MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }


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
//            else if (studentId.Contains("1a02") || studentId.Contains("1A02"))
//                return "EEE";
//            else if (studentId.Contains("5A02") || studentId.Contains("5a02"))
//                return "EEE";
//            else if (studentId.Contains("1a03") || studentId.Contains("1A03"))
//                return "MECH";
//            else if (studentId.Contains("5A03") || studentId.Contains("5a03"))
//                return "MECH";

//            else if (studentId.Contains("1a25") || studentId.Contains("1A25"))
//                return "ME";
//            else if (studentId.Contains("5A25") || studentId.Contains("5a25"))
//                return "ME";
//            // Add more conditions here for other patterns as needed
//            else
//                return "Unknown"; // Default case if no pattern matches
//        }

//        private void BatchCgpaDisplay_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}


using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class BatchCgpaDisplay : Form
    {
        private Button btnDownload;
        private Button btnBack;
        private DataGridView dgvResults;
        private string branchFilter;
        private string academicYearFilter; 
        private Button backBtn;
        double totalCredits = 0;

        public BatchCgpaDisplay(string branch, string academicYear)
        {
            branchFilter = branch;
            academicYearFilter = academicYear;
            InitializeComponent();
            SetupForm();     // ✅ Create dgvResults first
            LoadResults();   // ✅ Now it's safe to load data
        }

        private void SetupForm()
        {
            this.Size = new Size(1500, 1000);
            this.Text = "Batch CGPA Results";
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Header Label
            Label lblHeader = new Label
            {
                Text = "Batch CGPA Results",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid,
                Location = new Point((this.ClientSize.Width - 300) / 2, 20)
            };
            this.Controls.Add(lblHeader);

            // Branch and Academic Year Labels
            Label lblBranch = new Label
            {
                Text = $"Branch: {branchFilter}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 60)
            };
            this.Controls.Add(lblBranch);

            Label lblAcademicYear = new Label
            {
                Text = $"Academic Year: {academicYearFilter}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 85)
            };
            this.Controls.Add(lblAcademicYear);

            // DataGridView
            dgvResults = new DataGridView
            {
                Location = new Point(100, 120),
                Size = new Size(1110, 460),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkViolet
                },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White
            };

            dgvResults.Columns.Add("StudentId", "Student ID");
            dgvResults.Columns.Add("StudentName", "Student Name");
            dgvResults.Columns.Add("AcademicYear", "Academic Year");
            dgvResults.Columns.Add("CGPA", "CGPA");
            dgvResults.Columns.Add("Credits", "Earned Credits"); // ✅ Header will be updated later

            // Set column widths
            dgvResults.Columns[0].Width = 200;
            dgvResults.Columns[1].Width = 350;
            dgvResults.Columns[2].Width = 200;
            dgvResults.Columns[3].Width = 200;
            dgvResults.Columns[4].Width = 150;

            dgvResults.RowTemplate.Height = 40;
            dgvResults.ColumnHeadersHeight = 50;

            this.Controls.Add(dgvResults);

            // Download Button
            btnDownload = new Button
            {
                Text = "Download Excel",
                Location = new Point(530, 600),
                Width = 150,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrchid,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnDownload.FlatAppearance.BorderColor = Color.DarkViolet;
            btnDownload.Click += new EventHandler(BtnDownload_Click);
            this.Controls.Add(btnDownload);

            // Back Button
            backBtn = new Button
            {
                Text = "Back To Home",
                Location = new Point(860, 20),
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkViolet,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
            backBtn.Click += new EventHandler(backbtn_Click);
            this.Controls.Add(backBtn);
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save Batch CGPA Results as Excel",
                FileName = "BatchResults_CGPA.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Batch CGPA Results");

                    worksheet.Cell(1, 1).Value = $"Batch CGPA Results - Branch: {branchFilter}, Academic Year: {academicYearFilter}";
                    worksheet.Range("A1:D1").Merge();
                    worksheet.Cell(1, 1).Style.Font.SetBold();
                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    for (int i = 0; i < dgvResults.Columns.Count; i++)
                        worksheet.Cell(2, i + 1).Value = dgvResults.Columns[i].HeaderText;

                    worksheet.Row(2).Style.Font.SetBold();
                    worksheet.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkOrchid);
                    worksheet.Row(2).Style.Font.SetFontColor(XLColor.White);

                    for (int i = 0; i < dgvResults.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvResults.Columns.Count; j++)
                            worksheet.Cell(i + 3, j + 1).Value = dgvResults.Rows[i].Cells[j].Value?.ToString();
                    }

                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 30;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 10;

                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadResults()
        {
            var studentNames = LoadJsonData<Student>("studentnames.json");
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

            var results = from mark in studentMarks
                          join name in studentNames on mark.StudentId equals name.studentid
                          join ay in academicYears on mark.StudentId equals ay.studentid
                          join subject in subjectCodes on mark.SubjectCode equals subject.SubjectCode
                          select new
                          {
                              mark.StudentId,
                              StudentName = name.name,
                              AcademicYear = ay.academic_year,
                              GradePoints = mark.GradePoints,
                              SubjectCredits = subject.Credits,
                              EarnedCredits = mark.Credits
                          };

            var filteredResults = results.Where(r =>
                (branchFilter == "All" || GetBranchFromStudentId(r.StudentId) == branchFilter) &&
                (academicYearFilter == "All" || r.AcademicYear == academicYearFilter)
            );

            var studentResults = filteredResults
                .GroupBy(r => new { r.StudentId, r.StudentName, r.AcademicYear })
                .Select(g =>
                {
                    double totalGradePoints = 0, totalCreditsForCgpa = 0, totalEarnedCredits = 0;

                    foreach (var item in g)
                    {
                        double gradePoint = double.TryParse(item.GradePoints, out double gp) ? gp : 0;
                        double subjectCredits = double.TryParse(item.SubjectCredits, out double cr) ? cr : 0;
                        double earnedCredits = double.TryParse(item.EarnedCredits, out double ec) ? ec : 0;

                        totalGradePoints += gradePoint * subjectCredits;
                        totalCreditsForCgpa += subjectCredits;
                        totalEarnedCredits += earnedCredits;
                    }

                    totalCredits = totalCreditsForCgpa;
                    double cgpa = totalCreditsForCgpa > 0 ? totalGradePoints / totalCreditsForCgpa : 0;

                    return new
                    {
                        g.Key.StudentId,
                        g.Key.StudentName,
                        g.Key.AcademicYear,
                        CGPA = cgpa,
                        Credits = totalEarnedCredits
                    };
                });

            dgvResults.Rows.Clear();
            foreach (var result in studentResults)
            {
                dgvResults.Rows.Add(
                    result.StudentId,
                    result.StudentName,
                    result.AcademicYear,
                    result.CGPA.ToString("F2"),
                    result.Credits.ToString("F0")
                );
            }

            dgvResults.Columns["Credits"].HeaderText = $"Earned Credits ({totalCredits})";

            if (dgvResults.Rows.Count == 0)
                MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private string GetBranchFromStudentId(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
            studentId = studentId.ToUpper().Trim();

            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
            if (studentId.Contains("1A02") || studentId.Contains("5A02")) return "EEE";
            if (studentId.Contains("1A03") || studentId.Contains("5A03")) return "ME";
            if (studentId.Contains("1A25") || studentId.Contains("5A25")) return "MIE";

            return "Unknown";
        }

        private void BatchCgpaDisplay_Load(object sender, EventArgs e) {
        }
    }
}
