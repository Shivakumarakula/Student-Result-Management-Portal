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
//    public partial class BatchBacklogreport : Form
//    {
//        public BatchBacklogreport()
//        {
//            InitializeComponent();
//        }

//        private void BatchBacklogreport_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}



//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class BatchBacklogreport : Form
//    {
//        private string _branch;
//        private string _academicYear;
//        private DataGridView dgvBacklogs;
//        private Button downloadBtn;

//        public BatchBacklogreport(string branch, string academicYear)
//        {
//            _branch = branch;
//            _academicYear = academicYear;

//            // Initialize UI via code
//            InitializeComponentDynamic();

//            // Load backlog data
//            LoadBacklogData();
//        }

//        private void InitializeComponentDynamic()
//        {
//            this.Text = $"Backlog Report - {_branch} ({_academicYear})";
//            this.Width = 1000;
//            this.Height = 600;
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // DataGridView
//            dgvBacklogs = new DataGridView
//            {
//                Dock = DockStyle.Top,
//                Height = 500,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false,
//                ReadOnly = true,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
//                ScrollBars = ScrollBars.Both
//            };

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download Report",
//                Dock = DockStyle.Bottom,
//                Height = 40
//            };
//            downloadBtn.Click += downloadbtn_Click;

//            // Add controls
//            this.Controls.Add(dgvBacklogs);
//            this.Controls.Add(downloadBtn);
//        }

//        private void LoadBacklogData()
//        {
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

//            var results = from mark in studentMarks
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//                          where ay.academic_year == _academicYear &&
//                                GetBranchFromStudentId(mark.StudentId) == _branch
//                          select new
//                          {
//                              mark.StudentId,
//                              StudentName = name.name,
//                              sc.Year,
//                              sc.Semester,
//                              mark.SubjectCode,
//                              mark.SubjectName,
//                              mark.Grade,
//                              IsBacklog = mark.Grade == "F" || mark.GradePoints == "0"
//                          };

//            // Group by student and year-semester
//            var grouped = results.GroupBy(r => new { r.StudentId, r.StudentName, r.Year, r.Semester })
//                                 .Select(g => new
//                                 {
//                                     g.Key.StudentId,
//                                     g.Key.StudentName,
//                                     g.Key.Year,
//                                     g.Key.Semester,
//                                     Backlogs = g.Count(x => x.IsBacklog)
//                                 }).ToList();

//            // Pivot into DataTable
//            DataTable dt = new DataTable();
//            dt.Columns.Add("Student ID");
//            dt.Columns.Add("Name");

//            var yearSemesters = grouped.Select(g => $"Y{g.Year}-S{g.Semester}")
//                                       .Distinct().OrderBy(x => x).ToList();

//            foreach (var ys in yearSemesters)
//                dt.Columns.Add(ys);

//            dt.Columns.Add("Total Backlogs");

//            var students = grouped.Select(g => new { g.StudentId, g.StudentName })
//                                  .Distinct().ToList();

//            foreach (var student in students)
//            {
//                var row = dt.NewRow();
//                row["Student ID"] = student.StudentId;
//                row["Name"] = student.StudentName;

//                int totalBacklogs = 0;
//                foreach (var ys in yearSemesters)
//                {
//                    var part = grouped.FirstOrDefault(g => g.StudentId == student.StudentId &&
//                                                           $"Y{g.Year}-S{g.Semester}" == ys);
//                    int count = part?.Backlogs ?? 0;
//                    row[ys] = count;
//                    totalBacklogs += count;
//                }
//                row["Total Backlogs"] = totalBacklogs;
//                dt.Rows.Add(row);
//            }

//            dgvBacklogs.DataSource = dt;
//        }

//        private void downloadbtn_Click(object sender, EventArgs e)
//        {
//            if (dgvBacklogs.DataSource is DataTable dt)
//            {
//                SaveFileDialog sfd = new SaveFileDialog
//                {
//                    Filter = "CSV files (*.csv)|*.csv",
//                    FileName = "BatchBacklogReport.csv"
//                };

//                if (sfd.ShowDialog() == DialogResult.OK)
//                {
//                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
//                    {
//                        // Write headers
//                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
//                        sw.WriteLine(string.Join(",", columnNames));

//                        // Write data
//                        foreach (DataRow row in dt.Rows)
//                        {
//                            var fields = row.ItemArray.Select(f => f.ToString());
//                            sw.WriteLine(string.Join(",", fields));
//                        }
//                    }
//                    MessageBox.Show("Report downloaded successfully!");
//                }
//            }
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();

//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("5A05") || studentId.Contains("1A05")) return "CSE";
//            if (studentId.Contains("5A01") || studentId.Contains("1A01")) return "CE";
//            if (studentId.Contains("5A02") || studentId.Contains("1A02")) return "EEE";
//            if (studentId.Contains("5A03") || studentId.Contains("1A03")) return "ME";
//            if (studentId.Contains("5A25") || studentId.Contains("1A25")) return "MIE";

//            return "Unknown";
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//                return new List<T>();
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }
//    }

//}


//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class BatchBacklogreport : Form
//    {
//        private string _branch;
//        private string _academicYear;
//        private DataGridView dgvBacklogs;
//        private Button downloadBtn;
//        private Label titleLabel;
//        private Panel topPanel;

//        public BatchBacklogreport(string branch, string academicYear)
//        {
//            _branch = branch;
//            _academicYear = academicYear;

//            InitializeDynamicUI();
//            LoadBacklogData();
//        }

//        private void InitializeDynamicUI()
//        {
//            // === FORM SETTINGS ===
//            this.Text = $"Backlog Report - {_branch} ({_academicYear})";
//            this.WindowState = FormWindowState.Maximized;
//            this.BackColor = Color.FromArgb(245, 247, 250);
//            this.Font = new Font("Segoe UI", 10);

//            // === TOP PANEL ===
//            topPanel = new Panel
//            {
//                Dock = DockStyle.Top,
//                Height = 70,
//                BackColor = Color.FromArgb(44, 62, 80)
//            };
//            this.Controls.Add(topPanel);

//            // === TITLE LABEL ===
//            titleLabel = new Label
//            {
//                Text = $"Batch-wise Backlog Report - {_branch} ({_academicYear})",
//                Dock = DockStyle.Fill,
//                TextAlign = ContentAlignment.MiddleCenter,
//                ForeColor = Color.White,
//                Font = new Font("Segoe UI", 16, FontStyle.Bold)
//            };
//            topPanel.Controls.Add(titleLabel);

//            // === DATAGRIDVIEW ===
//            dgvBacklogs = new DataGridView
//            {
//                Dock = DockStyle.Fill,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false,
//                ReadOnly = true,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                BackgroundColor = Color.White,
//                BorderStyle = BorderStyle.None,
//                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
//                MultiSelect = false,
//                GridColor = Color.LightGray,
//                EnableHeadersVisualStyles = false
//            };

//            // Header style
//            dgvBacklogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
//            dgvBacklogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
//            dgvBacklogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold);

//            // Alternate row style
//            dgvBacklogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
//            dgvBacklogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(93, 173, 226);
//            dgvBacklogs.DefaultCellStyle.SelectionForeColor = Color.Black;

//            // === DOWNLOAD BUTTON ===
//            downloadBtn = new Button
//            {
//                Text = "⬇ Download Report",
//                Dock = DockStyle.Bottom,
//                Height = 50,
//                FlatStyle = FlatStyle.Flat,
//                Font = new Font("Segoe UI Semibold", 12, FontStyle.Bold),
//                ForeColor = Color.White,
//                BackColor = Color.FromArgb(46, 134, 193),
//                Cursor = Cursors.Hand
//            };
//            downloadBtn.FlatAppearance.BorderSize = 0;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(40, 116, 166);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.FromArgb(46, 134, 193);
//            downloadBtn.Click += downloadbtn_Click;

//            // === ADD TO FORM ===
//            this.Controls.Add(dgvBacklogs);
//            this.Controls.Add(downloadBtn);
//        }

//        private void LoadBacklogData()
//        {
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

//            var results = from mark in studentMarks
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//                          where ay.academic_year == _academicYear &&
//                                GetBranchFromStudentId(mark.StudentId) == _branch
//                          select new
//                          {
//                              mark.StudentId,
//                              StudentName = name.name,
//                              sc.Year,
//                              sc.Semester,
//                              mark.SubjectCode,
//                              mark.SubjectName,
//                              mark.Grade,
//                              IsBacklog = mark.Grade == "F" || mark.GradePoints == "0"
//                          };

//            // Group by student and year-semester
//            var grouped = results.GroupBy(r => new { r.StudentId, r.StudentName, r.Year, r.Semester })
//                                 .Select(g => new
//                                 {
//                                     g.Key.StudentId,
//                                     g.Key.StudentName,
//                                     g.Key.Year,
//                                     g.Key.Semester,
//                                     Backlogs = g.Count(x => x.IsBacklog)
//                                 }).ToList();

//            // Create DataTable
//            DataTable dt = new DataTable();
//            dt.Columns.Add("Student ID");
//            dt.Columns.Add("Name");

//            var yearSemesters = grouped.Select(g => $"Y{g.Year}-S{g.Semester}")
//                                       .Distinct().OrderBy(x => x).ToList();

//            foreach (var ys in yearSemesters)
//                dt.Columns.Add(ys);

//            dt.Columns.Add("Total Backlogs");

//            var students = grouped.Select(g => new { g.StudentId, g.StudentName })
//                                  .Distinct().ToList();

//            foreach (var student in students)
//            {
//                var row = dt.NewRow();
//                row["Student ID"] = student.StudentId;
//                row["Name"] = student.StudentName;

//                int totalBacklogs = 0;
//                foreach (var ys in yearSemesters)
//                {
//                    var part = grouped.FirstOrDefault(g => g.StudentId == student.StudentId &&
//                                                           $"Y{g.Year}-S{g.Semester}" == ys);
//                    int count = part?.Backlogs ?? 0;
//                    row[ys] = count;
//                    totalBacklogs += count;
//                }
//                row["Total Backlogs"] = totalBacklogs;
//                dt.Rows.Add(row);
//            }

//            dgvBacklogs.DataSource = dt;

//            // Color code backlog counts
//            dgvBacklogs.CellFormatting += (s, e) =>
//            {
//                if (e.ColumnIndex >= 2 && e.ColumnIndex < dgvBacklogs.Columns.Count)
//                {
//                    if (int.TryParse(e.Value?.ToString(), out int val))
//                    {
//                        if (val == 0)
//                            e.CellStyle.ForeColor = Color.DarkGreen;
//                        else
//                            e.CellStyle.ForeColor = Color.Red;
//                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
//                    }
//                }
//            };
//        }

//        private void downloadbtn_Click(object sender, EventArgs e)
//        {
//            if (dgvBacklogs.DataSource is DataTable dt)
//            {
//                SaveFileDialog sfd = new SaveFileDialog
//                {
//                    Filter = "CSV files (*.csv)|*.csv",
//                    FileName = $"BacklogReport_{_branch}_{_academicYear}.csv"
//                };

//                if (sfd.ShowDialog() == DialogResult.OK)
//                {
//                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
//                    {
//                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
//                        sw.WriteLine(string.Join(",", columnNames));

//                        foreach (DataRow row in dt.Rows)
//                        {
//                            var fields = row.ItemArray.Select(f => f.ToString());
//                            sw.WriteLine(string.Join(",", fields));
//                        }
//                    }
//                    MessageBox.Show("Report downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();

//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("5A05") || studentId.Contains("1A05")) return "CSE";
//            if (studentId.Contains("5A01") || studentId.Contains("1A01")) return "CE";
//            if (studentId.Contains("5A02") || studentId.Contains("1A02")) return "EEE";
//            if (studentId.Contains("5A03") || studentId.Contains("1A03")) return "ME";
//            if (studentId.Contains("5A25") || studentId.Contains("1A25")) return "MIE";

//            return "Unknown";
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//                return new List<T>();
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }

//        // === DATA CLASSES ===
//        public class Student
//        {
//            public string studentid { get; set; }
//            public string name { get; set; }
//        }

//        public class Subject
//        {
//            public string SubjectCode { get; set; }
//            public string Year { get; set; }
//            public string Semester { get; set; }
//            public string Credits { get; set; }
//        }

//        public class StudentMark
//        {
//            public string StudentId { get; set; }
//            public string SubjectCode { get; set; }
//            public string SubjectName { get; set; }
//            public string Internal { get; set; }
//            public string External { get; set; }
//            public string Total { get; set; }
//            public string Grade { get; set; }
//            public string GradePoints { get; set; }
//            public string Credits { get; set; }
//        }

//        public class AcademicYear
//        {
//            public string studentid { get; set; }
//            public string academic_year { get; set; }
//        }
//    }
//}



//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class BatchBacklogreport : Form
//    {
//        private string _branch;
//        private string _academicYear;
//        private DataGridView dgvBacklogs;
//        private Button downloadBtn;

//        public BatchBacklogreport(string branch, string academicYear)
//        {
//            _branch = branch;
//            _academicYear = academicYear;

//            InitializeComponent();
//            InitializeCustomUI();
//            LoadBacklogData();
//        }

//        private void InitializeCustomUI()
//        {
//            this.Text = $"Backlog Report - {_branch} ({_academicYear})";
//            this.BackColor = Color.WhiteSmoke;
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // --- DataGridView setup ---
//            dgvBacklogs = new DataGridView
//            {
//                Location = new Point(20, 20),
//                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 100),
//                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false,
//                ReadOnly = true,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                BackgroundColor = Color.White,
//                BorderStyle = BorderStyle.Fixed3D,
//                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(35, 90, 160),
//                    ForeColor = Color.White,
//                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                },
//                DefaultCellStyle = new DataGridViewCellStyle
//                {
//                    Font = new Font("Segoe UI", 9),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter,
//                    Padding = new Padding(2)
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.AliceBlue
//                },
//                GridColor = Color.Gray
//            };

//            // --- Download Button ---
//            downloadBtn = new Button
//            {
//                Text = "Download Report",
//                Size = new Size(180, 40),
//                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
//                BackColor = Color.FromArgb(30, 144, 255),
//                ForeColor = Color.White,
//                Font = new Font("Segoe UI", 9, FontStyle.Bold),
//                FlatStyle = FlatStyle.Flat
//            };
//            downloadBtn.FlatAppearance.BorderSize = 0;
//            downloadBtn.Location = new Point(this.ClientSize.Width - downloadBtn.Width - 30, this.ClientSize.Height - downloadBtn.Height - 20);
//            downloadBtn.Click += downloadbtn_Click;

//            // Resize event (maintains button position)
//            this.Resize += (s, e) =>
//            {
//                dgvBacklogs.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 100);
//                downloadBtn.Location = new Point(this.ClientSize.Width - downloadBtn.Width - 30, this.ClientSize.Height - downloadBtn.Height - 20);
//            };

//            // Add controls
//            this.Controls.Add(dgvBacklogs);
//            this.Controls.Add(downloadBtn);
//        }

//        private void LoadBacklogData()
//        {
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

//            var results = from mark in studentMarks
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//                          where ay.academic_year == _academicYear &&
//                                GetBranchFromStudentId(mark.StudentId) == _branch
//                          select new
//                          {
//                              mark.StudentId,
//                              StudentName = name.name,
//                              sc.Year,
//                              sc.Semester,
//                              mark.SubjectCode,
//                              mark.SubjectName,
//                              mark.Grade,
//                              IsBacklog = mark.Grade == "F" || mark.GradePoints == "0"
//                          };

//            var grouped = results.GroupBy(r => new { r.StudentId, r.StudentName, r.Year, r.Semester })
//                                 .Select(g => new
//                                 {
//                                     g.Key.StudentId,
//                                     g.Key.StudentName,
//                                     g.Key.Year,
//                                     g.Key.Semester,
//                                     Backlogs = g.Count(x => x.IsBacklog)
//                                 }).ToList();

//            DataTable dt = new DataTable();
//            dt.Columns.Add("Student ID");
//            dt.Columns.Add("Name");

//            var yearSemesters = grouped.Select(g => $"Y{g.Year}-S{g.Semester}")
//                                       .Distinct().OrderBy(x => x).ToList();

//            foreach (var ys in yearSemesters)
//                dt.Columns.Add(ys);

//            dt.Columns.Add("Total Backlogs");

//            var students = grouped.Select(g => new { g.StudentId, g.StudentName })
//                                  .Distinct().ToList();

//            foreach (var student in students)
//            {
//                var row = dt.NewRow();
//                row["Student ID"] = student.StudentId;
//                row["Name"] = student.StudentName;

//                int totalBacklogs = 0;
//                foreach (var ys in yearSemesters)
//                {
//                    var part = grouped.FirstOrDefault(g => g.StudentId == student.StudentId &&
//                                                           $"Y{g.Year}-S{g.Semester}" == ys);
//                    int count = part?.Backlogs ?? 0;
//                    row[ys] = count;
//                    totalBacklogs += count;
//                }
//                row["Total Backlogs"] = totalBacklogs;
//                dt.Rows.Add(row);
//            }

//            dgvBacklogs.DataSource = dt;
//        }

//        private void downloadbtn_Click(object sender, EventArgs e)
//        {
//            if (dgvBacklogs.DataSource is DataTable dt)
//            {
//                SaveFileDialog sfd = new SaveFileDialog
//                {
//                    Filter = "CSV files (*.csv)|*.csv",
//                    FileName = "BatchBacklogReport.csv"
//                };

//                if (sfd.ShowDialog() == DialogResult.OK)
//                {
//                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
//                    {
//                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
//                        sw.WriteLine(string.Join(",", columnNames));

//                        foreach (DataRow row in dt.Rows)
//                        {
//                            var fields = row.ItemArray.Select(f => f.ToString());
//                            sw.WriteLine(string.Join(",", fields));
//                        }
//                    }
//                    MessageBox.Show("Report downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();

//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("5A05") || studentId.Contains("1A05")) return "CSE";
//            if (studentId.Contains("5A01") || studentId.Contains("1A01")) return "CE";
//            if (studentId.Contains("5A02") || studentId.Contains("1A02")) return "EEE";
//            if (studentId.Contains("5A03") || studentId.Contains("1A03")) return "ME";
//            if (studentId.Contains("5A25") || studentId.Contains("1A25")) return "MIE";

//            return "Unknown";
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//                return new List<T>();
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }
//    }
//}



//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class BatchBacklogreport : Form
//    {
//        private string _branch;
//        private string _academicYear;
//        private DataGridView dgvBacklogs;
//        private Button downloadBtn;

//        public BatchBacklogreport(string branch, string academicYear)
//        {
//            _branch = branch;
//            _academicYear = academicYear;
//            InitializeComponentDynamic();
//        }

//        private void BatchBacklogreport_Load(object sender, EventArgs e)
//        {
//            LoadBacklogData();
//        }

//        private void InitializeComponentDynamic()
//        {
//            this.Text = $"Backlog Report - {_branch} ({_academicYear})";
//            this.Width = 1100;
//            this.Height = 650;
//            this.StartPosition = FormStartPosition.CenterScreen;
//            this.BackColor = System.Drawing.Color.White;

//            // DataGridView setup
//            dgvBacklogs = new DataGridView
//            {
//                Location = new System.Drawing.Point(20, 20),
//                Width = 1200,
//                Height = 520,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false,
//                AllowUserToResizeColumns = false,
//                AllowUserToResizeRows = false,
//                ReadOnly = true,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
//                BackgroundColor = System.Drawing.Color.White,
//                BorderStyle = BorderStyle.Fixed3D,
//                ScrollBars = ScrollBars.Both,
//                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
//            };

//            // Download Button setup (bottom-right)
//            downloadBtn = new Button
//            {
//                Text = "Download Report",
//                Width = 200,
//                Height = 40,
//                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
//                Location = new System.Drawing.Point(this.ClientSize.Width - 220, this.ClientSize.Height - 60),
//            };
//            downloadBtn.Click += downloadbtn_Click;

//            this.Controls.Add(dgvBacklogs);
//            this.Controls.Add(downloadBtn);

//            // Adjust layout dynamically
//            this.Load += BatchBacklogreport_Load;
//        }

//        private void LoadBacklogData()
//        {
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            var studentNames = LoadJsonData<Student>("studentnames.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

//            var results = from mark in studentMarks
//                          join ay in academicYears on mark.StudentId equals ay.studentid
//                          join name in studentNames on mark.StudentId equals name.studentid
//                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
//                          where ay.academic_year == _academicYear &&
//                                GetBranchFromStudentId(mark.StudentId) == _branch
//                          select new
//                          {
//                              mark.StudentId,
//                              StudentName = name.name,
//                              sc.Year,
//                              sc.Semester,
//                              mark.SubjectCode,
//                              mark.SubjectName,
//                              mark.Grade,
//                              IsBacklog = mark.Grade == "F" || mark.GradePoints == "0"
//                          };

//            // Grouping and sorting by Student ID
//            var grouped = results
//                .GroupBy(r => new { r.StudentId, r.StudentName, r.Year, r.Semester })
//                .Select(g => new
//                {
//                    g.Key.StudentId,
//                    g.Key.StudentName,
//                    g.Key.Year,
//                    g.Key.Semester,
//                    Backlogs = g.Count(x => x.IsBacklog)
//                })
//                .OrderBy(g => g.StudentId)
//                .ToList();

//            // Pivot into DataTable
//            DataTable dt = new DataTable();
//            dt.Columns.Add("Student ID");
//            dt.Columns.Add("Name");

//            var yearSemesters = grouped.Select(g => $"Y{g.Year}-S{g.Semester}")
//                                       .Distinct().OrderBy(x => x).ToList();

//            foreach (var ys in yearSemesters)
//                dt.Columns.Add(ys);

//            dt.Columns.Add("Total Backlogs");

//            var students = grouped.Select(g => new { g.StudentId, g.StudentName })
//                                  .Distinct()
//                                  .OrderBy(s => s.StudentId)
//                                  .ToList();

//            foreach (var student in students)
//            {
//                var row = dt.NewRow();
//                row["Student ID"] = student.StudentId;
//                row["Name"] = student.StudentName;

//                int totalBacklogs = 0;
//                foreach (var ys in yearSemesters)
//                {
//                    var part = grouped.FirstOrDefault(g => g.StudentId == student.StudentId &&
//                                                           $"Y{g.Year}-S{g.Semester}" == ys);
//                    int count = part?.Backlogs ?? 0;
//                    row[ys] = count;
//                    totalBacklogs += count;
//                }
//                row["Total Backlogs"] = totalBacklogs;
//                dt.Rows.Add(row);
//            }

//            dgvBacklogs.DataSource = dt;

//            // Make "Name" column wider for clarity
//            dgvBacklogs.Columns["Name"].Width = 200;
//        }

//        private void downloadbtn_Click(object sender, EventArgs e)
//        {
//            if (dgvBacklogs.DataSource is DataTable dt)
//            {
//                SaveFileDialog sfd = new SaveFileDialog
//                {
//                    Filter = "CSV files (*.csv)|*.csv",
//                    FileName = "BatchBacklogReport.csv"
//                };

//                if (sfd.ShowDialog() == DialogResult.OK)
//                {
//                    // Safely overwrite if exists
//                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
//                    using (StreamWriter sw = new StreamWriter(fs))
//                    {
//                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
//                        sw.WriteLine(string.Join(",", columnNames));

//                        foreach (DataRow row in dt.AsEnumerable().OrderBy(r => r["Student ID"].ToString()))
//                        {
//                            var fields = row.ItemArray.Select(f => f.ToString());
//                            sw.WriteLine(string.Join(",", fields));
//                        }
//                    }

//                    MessageBox.Show("Report downloaded successfully!", "Success",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();

//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("5A05") || studentId.Contains("1A05")) return "CSE";
//            if (studentId.Contains("5A01") || studentId.Contains("1A01")) return "CE";
//            if (studentId.Contains("5A02") || studentId.Contains("1A02")) return "EEE";
//            if (studentId.Contains("5A03") || studentId.Contains("1A03")) return "ME";
//            if (studentId.Contains("5A25") || studentId.Contains("1A25")) return "MIE";

//            return "Unknown";
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//                return new List<T>();
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }
//    }
//}



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class BatchBacklogreport : Form
    {
        private string _branch;
        private string _academicYear;
        private DataGridView dgvBacklogs;
        private Button downloadBtn;
        private Label headerLabel;

        public BatchBacklogreport(string branch, string academicYear)
        {
            _branch = branch;
            _academicYear = academicYear;
            InitializeComponentDynamic();
        }

        private void BatchBacklogreport_Load(object sender, EventArgs e)
        {
            LoadBacklogData();
        }

        private void InitializeComponentDynamic()
        {
            this.Text = $"Backlog Report - {_branch} ({_academicYear})";
            this.Width = 1200;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;

            // Header Label
            headerLabel = new Label
            {
                Text = $"Backlog Report - {_branch} ({_academicYear})",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Width = this.Width,
                Height = 50,
                Location = new System.Drawing.Point(0, 10),
                ForeColor = System.Drawing.Color.FromArgb(40, 40, 40)
            };
            this.Controls.Add(headerLabel);

            // DataGridView setup
            dgvBacklogs = new DataGridView
            {
                Location = new System.Drawing.Point(40, 80),   // Manual position
                Width = 1200,                                  // Manual width
                Height = 500,                                  // Manual height
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                BackgroundColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                ScrollBars = ScrollBars.Both,
                ColumnHeadersHeight = 35,                      // Manual header height
                RowTemplate = { Height = 30 },                 // Manual row height
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            this.Controls.Add(dgvBacklogs);

            // Download Button (bottom-right)
            downloadBtn = new Button
            {
                Text = "Download Report",
                Width = 200,
                Height = 40,
                Location = new System.Drawing.Point(this.Width - 260, this.Height - 100),
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            downloadBtn.FlatAppearance.BorderSize = 0;
            downloadBtn.Click += downloadbtn_Click;

            this.Controls.Add(downloadBtn);
            this.Load += BatchBacklogreport_Load;
        }

        private void LoadBacklogData()
        {
            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            var studentNames = LoadJsonData<Student>("studentnames.json");
            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

            var results = from mark in studentMarks
                          join ay in academicYears on mark.StudentId equals ay.studentid
                          join name in studentNames on mark.StudentId equals name.studentid
                          join sc in subjectCodes on mark.SubjectCode equals sc.SubjectCode
                          where ay.academic_year == _academicYear &&
                                GetBranchFromStudentId(mark.StudentId) == _branch
                          select new
                          {
                              mark.StudentId,
                              StudentName = name.name,
                              sc.Year,
                              sc.Semester,
                              mark.SubjectCode,
                              mark.SubjectName,
                              mark.Grade,
                              IsBacklog = mark.Grade == "F" || mark.GradePoints == "0"
                          };

            // Grouping and sorting by Student ID
            var grouped = results
                .GroupBy(r => new { r.StudentId, r.StudentName, r.Year, r.Semester })
                .Select(g => new
                {
                    g.Key.StudentId,
                    g.Key.StudentName,
                    g.Key.Year,
                    g.Key.Semester,
                    Backlogs = g.Count(x => x.IsBacklog)
                })
                .OrderBy(g => g.StudentId)
                .ToList();

            // Create DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Student ID");
            dt.Columns.Add("Name");

            var yearSemesters = grouped.Select(g => $"Y{g.Year}-S{g.Semester}")
                                       .Distinct().OrderBy(x => x).ToList();

            foreach (var ys in yearSemesters)
                dt.Columns.Add(ys);

            dt.Columns.Add("Total Backlogs");

            var students = grouped.Select(g => new { g.StudentId, g.StudentName })
                                  .Distinct()
                                  .OrderBy(s => s.StudentId)
                                  .ToList();

            foreach (var student in students)
            {
                var row = dt.NewRow();
                row["Student ID"] = student.StudentId;
                row["Name"] = student.StudentName;

                int totalBacklogs = 0;
                foreach (var ys in yearSemesters)
                {
                    var part = grouped.FirstOrDefault(g => g.StudentId == student.StudentId &&
                                                           $"Y{g.Year}-S{g.Semester}" == ys);
                    int count = part?.Backlogs ?? 0;
                    row[ys] = count;
                    totalBacklogs += count;
                }
                row["Total Backlogs"] = totalBacklogs;
                dt.Rows.Add(row);
            }

            dgvBacklogs.DataSource = dt;

            // Manually adjust column widths
            if (dgvBacklogs.Columns.Contains("Student ID")) dgvBacklogs.Columns["Student ID"].Width = 150;
            if (dgvBacklogs.Columns.Contains("Name")) dgvBacklogs.Columns["Name"].Width = 300;

            foreach (DataGridViewColumn col in dgvBacklogs.Columns)
            {
                if (col.HeaderText.StartsWith("Y"))
                    col.Width = 100;
            }

            if (dgvBacklogs.Columns.Contains("Total Backlogs"))
                dgvBacklogs.Columns["Total Backlogs"].Width = 150;
        }

        private void downloadbtn_Click(object sender, EventArgs e)
        {
            if (dgvBacklogs.DataSource is DataTable dt)
            {
                string safeBranch = _branch.Replace(" ", "_");
                string safeYear = _academicYear.Replace(" ", "_").Replace("/", "-");
                string defaultFileName = $"BatchBacklogReport_{safeBranch}_{safeYear}.csv";

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = defaultFileName
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Safely overwrite file (allow replacing)
                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            // Write headers
                            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                            sw.WriteLine(string.Join(",", columnNames));

                            // Write data (sorted by Student ID)
                            foreach (DataRow row in dt.AsEnumerable().OrderBy(r => r["Student ID"].ToString()))
                            {
                                var fields = row.ItemArray.Select(f => f.ToString().Replace(",", " ")); // Avoid CSV breaks
                                sw.WriteLine(string.Join(",", fields));
                            }
                        }

                        MessageBox.Show("Report downloaded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("The file is currently open. Please close it and try again.",
                            "File In Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                return new List<T>();
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
    }
}
