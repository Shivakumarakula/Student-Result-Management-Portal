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
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using System.IO;
//using ClosedXML.Excel;

//namespace project_RYS.Forms
//{
//    public partial class BatchResultDisplaysgpa : Form
//    {
//        private Button btnDownload;
//        private DataGridView dgvResults;
//        private string branchFilter;
//        private string academicYearFilter;
//        private string yearFilter;
//        private string semesterFilter;
//        public BatchResultDisplaysgpa(string branch, string academicYear, string year, string semester)
//        {
//            //InitializeComponent();
//            branchFilter = branch;
//            academicYearFilter = academicYear;
//            yearFilter = year;
//            semesterFilter = semester;

//            InitializeComponent();
//            SetupForm();
//            LoadResults();

//        }
//        private void SetupForm()
//        {
//            this.Size = new Size(1300, 800);
//            this.Text = "Batch Results";
//            this.AutoScroll = true;

//            // DataGridView
//            dgvResults = new DataGridView
//            {
//                Location = new Point(20, 60),
//                Size = new Size(1250, 680),
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                RowHeadersVisible = false,
//                AllowUserToAddRows = false,
//                ReadOnly = true,
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
//                }
//            };

//            dgvResults.Columns.Add("StudentId", "Student ID");
//            dgvResults.Columns.Add("StudentName", "Student Name");
//            dgvResults.Columns.Add("AcademicYear", "Academic Year");
//            dgvResults.Columns.Add("Year", "Year");
//            dgvResults.Columns.Add("Sem", "Semester");
//            dgvResults.Columns.Add("SGPA", "SGPA");

//            this.Controls.Add(dgvResults);

//            // Download Button
//            btnDownload = new Button
//            {
//                Text = "Download Excel",
//                Location = new Point(20, 20),
//                Width = 120,
//                Height = 30
//            };
//            btnDownload.Click += new EventHandler(BtnDownload_Click);
//            this.Controls.Add(btnDownload);
//        }
//        //private void SetupForm()
//        //{
//        //    this.Size = new Size(1300, 800);
//        //    this.Text = "Batch Results";
//        //    this.AutoScroll = true;

//        //    // DataGridView
//        //    dgvResults = new DataGridView
//        //    {
//        //        Location = new Point(20, 20),
//        //        Size = new Size(1250, 740),
//        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//        //        RowHeadersVisible = false,
//        //        AllowUserToAddRows = false,
//        //        ReadOnly = true,
//        //        EnableHeadersVisualStyles = false,
//        //        ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//        //        {
//        //            BackColor = Color.DarkOrchid,
//        //            ForeColor = Color.White,
//        //            Font = new Font("Arial", 10, FontStyle.Bold),
//        //            Alignment = DataGridViewContentAlignment.MiddleCenter
//        //        },
//        //        RowsDefaultCellStyle = new DataGridViewCellStyle
//        //        {
//        //            BackColor = Color.DarkOrchid,
//        //            ForeColor = Color.White,
//        //            Font = new Font("Arial", 10, FontStyle.Bold)
//        //        },
//        //        AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//        //        {
//        //            BackColor = Color.DarkViolet
//        //        }
//        //    };

//        //    // Define columns
//        //    dgvResults.Columns.Add("StudentId", "Student ID");
//        //    dgvResults.Columns.Add("StudentName", "Student Name");
//        //    dgvResults.Columns.Add("AcademicYear", "Academic Year");
//        //    dgvResults.Columns.Add("Year", "Year");
//        //    dgvResults.Columns.Add("Sem", "Semester");
//        //    dgvResults.Columns.Add("SGPA", "SGPA");

//        //    this.Controls.Add(dgvResults);
//        //    // Download Button
//        //    btnDownload = new Button
//        //    {
//        //        Text = "Download Excel",
//        //        Location = new Point(20, 20),
//        //        Width = 120,
//        //        Height = 30
//        //    };
//        //    btnDownload.Click += new EventHandler(BtnDownload_Click);
//        //    this.Controls.Add(btnDownload);
//        //}

//        private void BtnDownload_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog
//            {
//                Filter = "Excel Workbook|*.xlsx",
//                Title = "Save Batch Results as Excel",
//                FileName = "BatchResults_SGPA.xlsx"
//            };

//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                using (var workbook = new XLWorkbook())
//                {
//                    var worksheet = workbook.Worksheets.Add("Batch Results");

//                    // Add headers
//                    for (int i = 0; i < dgvResults.Columns.Count; i++)
//                    {
//                        worksheet.Cell(1, i + 1).Value = dgvResults.Columns[i].HeaderText;
//                    }
//                    worksheet.Row(1).Style.Font.SetBold();

//                    // Add data
//                    for (int i = 0; i < dgvResults.Rows.Count; i++)
//                    {
//                        for (int j = 0; j < dgvResults.Columns.Count; j++)
//                        {
//                            worksheet.Cell(i + 2, j + 1).Value = dgvResults.Rows[i].Cells[j].Value?.ToString();
//                        }
//                    }

//                    // Adjust column widths
//                    worksheet.Columns().AdjustToContents();

//                    // Save the file
//                    workbook.SaveAs(saveFileDialog.FileName);
//                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }
//        private void LoadResults()
//        {
//            //var studentNames = LoadJsonData<Student>("studentnames.json");
//            //var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            //var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            //var subjectCodes = LoadJsonData<subj>("subjectCodes.json");

//            //// Join data
//            //var results = from mark in studentMarks
//            //              join name in studentNames on mark.StudentId equals name.studentid
//            //              join ay in academicYears on mark.StudentId equals ay.studentid
//            //              join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//            //              select new
//            //              {
//            //                  StudentId = mark.StudentId,
//            //                  StudentName = name.name,
//            //                  AcademicYear = ay.academic_year,
//            //                  Year = sc.Year,
//            //                  Semester = sc.Semester,
//            //                  GradePoints = mark.GradePoints,
//            //                  Credits = mark.Credits
//            //              };

//            //// Apply filters
//            //var filteredResults = results.Where(r =>
//            //    (branchFilter == "All" || GetBranchFromStudentId(r.StudentId) == branchFilter) &&
//            //    (academicYearFilter == "All" || r.AcademicYear == academicYearFilter) &&
//            //    (yearFilter == "All" || r.Year == yearFilter) &&
//            //    (semesterFilter == "All" || r.Semester == semesterFilter)
//            //);

//            //// Group by student and calculate SGPA
//            //var studentResults = filteredResults
//            //    .GroupBy(r => new { r.StudentId, r.StudentName, r.AcademicYear, r.Year, r.Semester })
//            //    .Select(g =>
//            //    {
//            //        double totalGradePoints = 0;
//            //        int totalCredits = 0;

//            //        foreach (var item in g)
//            //        {
//            //            int gradePoint = int.TryParse(item.GradePoints, out int gp) ? gp : 0;
//            //            int credits = int.TryParse(item.Credits, out int cr) ? cr : 0;
//            //            totalGradePoints += gradePoint * credits;
//            //            totalCredits += credits;
//            //        }

//            //        double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
//            //        return new
//            //        {
//            //            g.Key.StudentId,
//            //            g.Key.StudentName,
//            //            g.Key.AcademicYear,
//            //            g.Key.Year,
//            //            g.Key.Semester,
//            //            SGPA = sgpa
//            //        };
//            //    });

//            //// Populate DataGridView
//            //foreach (var result in studentResults)
//            //{
//            //    dgvResults.Rows.Add(result.StudentId, result.StudentName, result.AcademicYear, result.Year, result.Semester, result.SGPA.ToString("F2"));
//            //}

//            //if (dgvResults.Rows.Count == 0)
//            //{
//            //    MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            //    return;
//            //}
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
//                (branchFilter == "All" || GetBranchFromStudentId(r.StudentId) == branchFilter) &&
//                (academicYearFilter == "All" || r.AcademicYear == academicYearFilter) &&
//                (yearFilter == "All" || r.Year == yearFilter) &&
//                (semesterFilter == "All" || r.Semester == semesterFilter)
//            );

//            var studentResults = filteredResults
//                .GroupBy(r => new { r.StudentId, r.StudentName, r.AcademicYear, r.Year, r.Semester })
//                .Select(g =>
//                {
//                    double totalGradePoints = 0;
//                    int totalCredits = 0;
//                    foreach (var item in g)
//                    {
//                        int gradePoint = int.TryParse(item.GradePoints, out int gp) ? gp : 0;
//                        int credits = int.TryParse(item.Credits, out int cr) ? cr : 0;
//                        totalGradePoints += gradePoint * credits;
//                        totalCredits += credits;
//                    }
//                    double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
//                    return new { g.Key.StudentId, g.Key.StudentName, g.Key.AcademicYear, g.Key.Year, g.Key.Semester, SGPA = sgpa };
//                });

//            foreach (var result in studentResults)
//            {
//                dgvResults.Rows.Add(result.StudentId, result.StudentName, result.AcademicYear, result.Year, result.Semester, result.SGPA.ToString("F2"));
//            }
//        }

//        // Reused methods
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
//            // Add more conditions here for other patterns as needed
//            else
//                return "Unknown"; // Default case if no pattern matches
//        }
//    }
//    // Data classes (reuse from your project)
//    public class Student
//    {
//        public string studentid { get; set; }
//        public string name { get; set; }
//    }

//    //public class AcademicYear
//    //{
//    //    public string studentid { get; set; }
//    //    public string academic_year { get; set; }
//    //}

//    //public class StudentMark
//    //{
//    //    public string StudentId { get; set; }
//    //    public string SubjectCode { get; set; }
//    //    public string SubjectName { get; set; }
//    //    public string Internal { get; set; }
//    //    public string External { get; set; }
//    //    public string Total { get; set; }
//    //    public string Grade { get; set; }
//    //    public string GradePoints { get; set; }
//    //    public string Credits { get; set; }
//    //}

//    //public class subj
//    //{
//    //    public string SubjectCode { get; set; }
//    //    public string Year { get; set; }
//    //    public string Semester { get; set; }
//    //}
//}


//```csharp
 
using ClosedXML.Excel;
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
    public partial class BatchResultDisplaysgpa : Form
    {
        private Button btnDownload;
        private Button btnBack;
        private DataGridView dgvResults;
        private string branchFilter;
        private string academicYearFilter;
        private string yearFilter;
        private string semesterFilter;
        private Button backBtn;
        double totalCredits = 0;

        public BatchResultDisplaysgpa(string branch, string academicYear, string year, string semester)
        {
            branchFilter = branch;
            academicYearFilter = academicYear;
            yearFilter = year;
            semesterFilter = semester;
            InitializeComponent();
            SetupForm();
            LoadResults();
        }

        private void SetupForm()
        {
            this.Size = new Size(1000, 600);
            this.Text = "Batch SGPA Results";
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240); // Light background

            // Header Label
            Label lblHeader = new Label
            {
                Text = "Batch SGPA Results",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid,
                Location = new Point((this.ClientSize.Width - 300) / 2, 20)
            };
            this.Controls.Add(lblHeader);


            // Filter Labels
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

            Label lblYear = new Label
            {
                Text = $"Year: {yearFilter}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 110)
            };
            this.Controls.Add(lblYear);

            Label lblSemester = new Label
            {
                Text = $"Semester: {semesterFilter}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 135)
            };
            this.Controls.Add(lblSemester);

         
            // DataGridView
            dgvResults = new DataGridView
            {
                Location = new Point(20, 190), // add left margin
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 250), // fit within form
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // fill available space
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both, // allow both scrolls
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
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Font = new Font("Arial", 10, FontStyle.Regular)
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 240, 255)
                },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.LightGray
            };

            // Add Columns
            dgvResults.Columns.Add("StudentId", "Student ID");
            dgvResults.Columns.Add("StudentName", "Student Name");
            dgvResults.Columns.Add("AcademicYear", "Academic Year");
            dgvResults.Columns.Add("Year", "Year");
            dgvResults.Columns.Add("Semester", "Semester");
            dgvResults.Columns.Add("SGPA", "SGPA");
            dgvResults.Columns.Add("Credits", "Credits");

            // Set column fill weights (controls proportional width)
            dgvResults.Columns["StudentId"].FillWeight = 80;   // narrower
            dgvResults.Columns["StudentName"].FillWeight = 180; // wider
            dgvResults.Columns["AcademicYear"].FillWeight = 90;
            dgvResults.Columns["Year"].FillWeight = 60;
            dgvResults.Columns["Semester"].FillWeight = 60;
            dgvResults.Columns["SGPA"].FillWeight = 80;
            dgvResults.Columns["Credits"].FillWeight = 80;

            // Row and header styling
            dgvResults.RowTemplate.Height = 35;
            dgvResults.ColumnHeadersHeight = 45;

            this.Controls.Add(dgvResults);


            // Download Button
            btnDownload = new Button
            {
                Text = "Download Excel",
                Location = new Point(530, 610),
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
                Title = "Save Batch SGPA Results as Excel",
                FileName = "BatchResults_SGPA.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Batch SGPA Results");

                    // Add title
                    worksheet.Cell(1, 1).Value = $"Batch SGPA Results - Branch: {branchFilter}, Academic Year: {academicYearFilter}, Year: {yearFilter}, Semester: {semesterFilter}";
                    //worksheet.Range("A1:F1").Merge();
                    worksheet.Range("A1:G1").Merge();

                    worksheet.Cell(1, 1).Style.Font.SetBold();
                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // Add headers
                    for (int i = 0; i < dgvResults.Columns.Count; i++)
                    {
                        worksheet.Cell(2, i + 1).Value = dgvResults.Columns[i].HeaderText;
                    }
                    worksheet.Row(2).Style.Font.SetBold();
                    worksheet.Row(2).Style.Fill.SetBackgroundColor(XLColor.DarkOrchid);
                    worksheet.Row(2).Style.Font.SetFontColor(XLColor.White);

                    // Add data
                    for (int i = 0; i < dgvResults.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvResults.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 3, j + 1).Value = dgvResults.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    // Set column widths to match DataGridView
                    worksheet.Column(1).Width = 15; // Student ID
                    worksheet.Column(2).Width = 30; // Student Name
                    worksheet.Column(3).Width = 20; // Academic Year
                    worksheet.Column(4).Width = 10; // Year
                    worksheet.Column(5).Width = 10; // Semester
                    //worksheet.Column(6).Width = 10; // SGPA
                    worksheet.Column(6).Width = 10; // SGPA
                    worksheet.Column(7).Width = 15; // Credits


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

            // Join data
            var results = from mark in studentMarks
                          join name in studentNames on mark.StudentId equals name.studentid
                          join ay in academicYears on mark.StudentId equals ay.studentid
                          join subject in subjectCodes on mark.SubjectCode equals subject.SubjectCode
                          select new
                          {
                              StudentId = mark.StudentId,
                              StudentName = name.name,
                              AcademicYear = ay.academic_year,
                              Year = subject.Year,
                              Semester = subject.Semester,
                              GradePoints = mark.GradePoints,
                              SubjectCredits = subject.Credits, // original credits (for SGPA)
                              EarnedCredits = mark.Credits      // student earned credits (for sum)
                          };

            // Apply filters
            var filteredResults = results.Where(r =>
                (branchFilter == "All" || GetBranchFromStudentId(r.StudentId) == branchFilter) &&
                (academicYearFilter == "All" || r.AcademicYear == academicYearFilter) &&
                (yearFilter == "All" || r.Year == yearFilter) &&
                (semesterFilter == "All" || r.Semester == semesterFilter)
            );

            // Group by student and calculate SGPA + total earned credits
            var studentResults = filteredResults
                .GroupBy(r => new { r.StudentId, r.StudentName, r.AcademicYear, r.Year, r.Semester })
                .Select(g =>
                {
                    double totalGradePoints = 0;
                    double totalCreditsForSGPA = 0;
                    double totalEarnedCredits = 0;

                    foreach (var item in g)
                    {
                        double gradePoint = double.TryParse(item.GradePoints, out double gp) ? gp : 0;
                        double subjectCredits = double.TryParse(item.SubjectCredits, out double cr) ? cr : 0;
                        double earnedCredits = double.TryParse(item.EarnedCredits, out double ec) ? ec : 0;

                        // For SGPA calculation use original subject credits
                        totalGradePoints += gradePoint * subjectCredits;
                        totalCreditsForSGPA += subjectCredits;

                        // For Credits column use student's earned credits
                        totalEarnedCredits += earnedCredits;
                    }
                    totalCredits = totalCreditsForSGPA;
                    double sgpa = totalCreditsForSGPA > 0 ? totalGradePoints / totalCreditsForSGPA : 0;

                    return new
                    {
                        g.Key.StudentId,
                        g.Key.StudentName,
                        g.Key.AcademicYear,
                        g.Key.Year,
                        g.Key.Semester,
                        SGPA = sgpa,
                        Credits = totalEarnedCredits // only earned credits go to DataGridView
                    };
                });

            // Populate DataGridView
            dgvResults.Rows.Clear();
            foreach (var result in studentResults)
            {
                dgvResults.Rows.Add(
                    result.StudentId,
                    result.StudentName,
                    result.AcademicYear,
                    result.Year,
                    result.Semester,
                    result.SGPA.ToString("F2"),
                    result.Credits.ToString("F0") // show credits as integer
                );
            }
            dgvResults.Columns["Credits"].HeaderText = $"Earned Credits ({totalCredits})";
            if (dgvResults.Rows.Count == 0)
            {
                MessageBox.Show("No results found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void BatchResultDisplaysgpa_Load(object sender, EventArgs e)
        {

        }
    }

    public class Student
    {
        public string studentid { get; set; }
        public string name { get; set; }
    }

   
}