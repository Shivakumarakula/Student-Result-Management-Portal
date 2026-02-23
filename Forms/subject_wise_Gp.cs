
////////using Newtonsoft.Json;
////////using System;
////////using System.Collections.Generic;
////////using System.Drawing;
////////using System.IO;
////////using System.Linq;
////////using System.Windows.Forms;

////////namespace project_RYS.Forms
////////{
////////    public partial class subject_wise_Gp : Form
////////    {
////////        private ComboBox branchCombo;
////////        private ComboBox acYearCombo;
////////        private ComboBox yearCombo;
////////        private ComboBox semCombo;
////////        private Button backBtn;
////////        private Button submitBtn;
////////        private Label headingLabel;
////////        private List<StudentResult> resultsall;
////////        private const int Margin = 30;

////////        public subject_wise_Gp()
////////        {
////////            InitializeComponent();
////////            SetupForm();
////////            PopulateComboBoxes();
////////        }

////////        private void SetupForm()
////////        {
////////            this.Size = new Size(1400, 900);
////////            this.AutoScroll = true;
////////            this.BackColor = Color.FromArgb(240, 240, 240);

////////            // Heading Label
////////            headingLabel = new Label
////////            {
////////                Text = "Semester Results Input",
////////                Font = new Font("Arial", 18, FontStyle.Bold),
////////                AutoSize = true,
////////                ForeColor = Color.DarkOrchid,
////////                Location = new Point(610, 30)
////////            };
////////            this.Controls.Add(headingLabel);

////////            // Back Button
////////            backBtn = new Button
////////            {
////////                Text = "Back To Home",
////////                Width = 180,
////////                Height = 45,
////////                Location = new Point(50, 80),
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkViolet,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 11, FontStyle.Bold)
////////            };
////////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
////////            backBtn.FlatAppearance.BorderSize = 1;
////////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
////////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
////////            backBtn.Click += new EventHandler(backBtn_Click);
////////            this.Controls.Add(backBtn);

////////            // Branch ComboBox
////////            branchCombo = new ComboBox
////////            {
////////                Width = 200,
////////                Height = 30,
////////                Font = new Font("Arial", 10),
////////                DropDownStyle = ComboBoxStyle.DropDownList
////////            };
////////            this.Controls.Add(branchCombo);

////////            // Academic Year ComboBox
////////            acYearCombo = new ComboBox
////////            {
////////                Width = 200,
////////                Height = 30,
////////                Font = new Font("Arial", 10),
////////                DropDownStyle = ComboBoxStyle.DropDownList
////////            };
////////            this.Controls.Add(acYearCombo);

////////            // Year ComboBox
////////            yearCombo = new ComboBox
////////            {
////////                Width = 200,
////////                Height = 30,
////////                Font = new Font("Arial", 10),
////////                DropDownStyle = ComboBoxStyle.DropDownList
////////            };
////////            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
////////            yearCombo.SelectedIndex = 0;
////////            this.Controls.Add(yearCombo);

////////            // Semester ComboBox
////////            semCombo = new ComboBox
////////            {
////////                Width = 200,
////////                Height = 30,
////////                Font = new Font("Arial", 10),
////////                DropDownStyle = ComboBoxStyle.DropDownList
////////            };
////////            semCombo.Items.AddRange(new string[] { "1", "2" });
////////            semCombo.SelectedIndex = 0;
////////            this.Controls.Add(semCombo);

////////            // Submit Button
////////            submitBtn = new Button
////////            {
////////                Text = "Submit",
////////                Width = 180,
////////                Height = 45,
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkOrchid,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 11, FontStyle.Bold)
////////            };
////////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////////            submitBtn.FlatAppearance.BorderSize = 1;
////////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
////////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
////////            submitBtn.Click += new EventHandler(submitBtn_Click);
////////            this.Controls.Add(submitBtn);

////////            ArrangeControls();
////////        }

////////        private void ArrangeControls()
////////        {
////////            int startY = 30;

////////            // Center heading
////////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////////            startY = headingLabel.Bottom + 50;

////////            // Back button
////////            backBtn.Location = new Point(50, startY);
////////            startY = backBtn.Bottom + Margin;

////////            // ComboBoxes and Submit button
////////            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
////////            branchCombo.Location = new Point(comboX, startY);
////////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
////////            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
////////            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
////////            submitBtn.Location = new Point(comboX + 4 * (200 + Margin), startY);
////////        }

////////        private void PopulateComboBoxes()
////////        {
////////            // Branches
////////            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
////////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
////////            branchCombo.Items.AddRange(branches.ToArray());
////////            if (branches.Any()) branchCombo.SelectedIndex = 0;

////////            // Academic Years
////////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
////////            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
////////            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
////////        }

////////        private string GetBranchFromStudentId(string studentId)
////////        {
////////            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
////////            studentId = studentId.ToUpper().Trim();
////////            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
////////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
////////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
////////            return "Unknown";
////////        }

////////        private void submitBtn_Click(object sender, EventArgs e)
////////        {
////////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
////////                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
////////            {
////////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////                return;
////////            }

////////            string branch = branchCombo.SelectedItem.ToString();
////////            string acYear = acYearCombo.SelectedItem.ToString();
////////            string year = yearCombo.SelectedItem.ToString();
////////            string semester = semCombo.SelectedItem.ToString();

////////            var results = LoadResults(branch, acYear, year, semester);
////////            SemesterResultsForm resultsForm = new SemesterResultsForm(results, branch, acYear, year, semester, this);
////////            resultsForm.Show();
////////            this.Hide();
////////        }

////////        private List<StudentResult> LoadResults(string branch, string acYear, string year, string semester)
////////        {
////////            var marks = LoadJsonData<StudentMar>("studentMarks.json");
////////            var subjects = LoadJsonData<Subje>("subjectCodes.json");
////////            var names = LoadJsonData<StudentName>("studentnames.json");
////////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

////////            // Map academic years and names
////////            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
////////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

////////            // Filter subjects by year and semester
////////            var relevantSubjects = subjects
////////                .Where(s => s.Year == year && s.Semester == semester)
////////                .ToList();
////////            var subjectCreditsDict = relevantSubjects
////////                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
////////            var subjectNameDict = marks
////////                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
////////                .ToDictionary(m => m.SubjectCode.ToUpper(), m => m.SubjectName, StringComparer.OrdinalIgnoreCase);

////////            // Filter marks by branch, academic year, and subjects
////////            var filteredMarks = marks
////////                .Where(m =>
////////                    GetBranchFromStudentId(m.StudentId) == branch &&
////////                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
////////                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
////////                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
////////                    m.Grade != "F" && m.Grade != "Ab"
////////                )
////////                .Select(m => new
////////                {
////////                    m.StudentId,
////////                    m.SubjectCode,
////////                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
////////                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
////////                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
////////                })
////////                .ToList();

////////            // Group by student
////////            var groupedByStudent = filteredMarks
////////                .GroupBy(d => d.StudentId.ToUpper())
////////                .Select(g => new
////////                {
////////                    StudentId = g.Key,
////////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
////////                    Subjects = g.ToDictionary(
////////                        s => s.SubjectCode.ToUpper(),
////////                        s => new { s.SubjectName, s.GradePoints, s.Credits })
////////                })
////////                .ToList();

////////            // Create results
////////            var results = new List<StudentR>();
////////            foreach (var student in groupedByStudent)
////////            {
////////                var resultall = new StudentR
////////                {
////////                    StudentId = student.StudentId,
////////                    StudentName = student.StudentName,
////////                    SubjectResults = new Dictionary<string, string>(),
////////                    TotalGradePoints = "0",
////////                    SGPA = "-"
////////                };

////////                double totalGradePoints = 0;
////////                double totalCredits = 0;

////////                foreach (var subject in relevantSubjects)
////////                {
////////                    string subjectCode = subject.SubjectCode.ToUpper();
////////                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
////////                    if (student.Subjects.ContainsKey(subjectCode))
////////                    {
////////                        var subj = student.Subjects[subjectCode];
////////                        double gradePoints = subj.GradePoints * subj.Credits;
////////                        resultall.SubjectResults[subjectName] = gradePoints.ToString("F1");
////////                        totalGradePoints += gradePoints;
////////                        totalCredits += subj.Credits;
////////                    }
////////                    else
////////                    {
////////                        resultall.SubjectResults[subjectName] = "-";
////////                    }
////////                }

////////                resultall.TotalGradePoints = totalGradePoints.ToString("F1");
////////                if (totalCredits > 0)
////////                {
////////                    resultall.SGPA = (totalGradePoints / totalCredits).ToString("F2");
////////                }

////////                results.Add(resultall);
////////            }

////////            return resultsall;
////////        }

////////        private List<T> LoadJsonData<T>(string filePath)
////////        {
////////            if (!File.Exists(filePath))
////////            {
////////                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////                return new List<T>();
////////            }

////////            try
////////            {
////////                var json = File.ReadAllText(filePath);
////////                var data = JsonConvert.DeserializeObject<List<T>>(json);
////////                return data ?? new List<T>();
////////            }
////////            catch (Exception ex)
////////            {
////////                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////                return new List<T>();
////////            }
////////        }

////////        private void backBtn_Click(object sender, EventArgs e)
////////        {
////////            this.Hide();
////////            //admindashboard adb = new admindashboard();
////////            //adb.Show();
////////        }
////////    }

////////    // Reused classes from getbtn.cs
////////    public class StudentMar
////////    {
////////        public string StudentId { get; set; }
////////        public string SubjectCode { get; set; }
////////        public string SubjectName { get; set; }
////////        public string Internal { get; set; }
////////        public string External { get; set; }
////////        public string Total { get; set; }
////////        public string Grade { get; set; }
////////        public string Credits { get; set; }
////////        public string GradePoints { get; set; }
////////    }

////////    public class Subje
////////    {
////////        public string SubjectCode { get; set; }
////////        public string Year { get; set; }
////////        public string Semester { get; set; }
////////        public string Credits { get; set; }
////////    }
////////    public class StudentR
////////    {
////////        public string StudentId { get; set; }
////////        public string StudentName { get; set; }
////////        public Dictionary<string, string> SubjectResults { get; set; }
////////        public string TotalGradePoints { get; set; }
////////        public string SGPA { get; set; }
////////    }
////////}



//////using Newtonsoft.Json;
//////using System;
//////using System.Collections.Generic;
//////using System.Drawing;
//////using System.IO;
//////using System.Linq;
//////using System.Windows.Forms;

//////namespace project_RYS.Forms
//////{
//////    public partial class subject_wise_Gp : Form
//////    {
//////        private ComboBox branchCombo;
//////        private ComboBox acYearCombo;
//////        private ComboBox yearCombo;
//////        private ComboBox semCombo;
//////        private Button backBtn;
//////        private Button submitBtn;
//////        private Label headingLabel;
//////        private List<StudentR> result;
//////        private const int Margin = 30;

//////        public subject_wise_Gp()
//////        {
//////            InitializeComponent();
//////            SetupForm();
//////            PopulateComboBoxes();
//////        }

//////        private void SetupForm()
//////        {
//////            this.Size = new Size(1400, 900);
//////            this.AutoScroll = true;
//////            this.BackColor = Color.FromArgb(240, 240, 240);

//////            // Heading Label
//////            headingLabel = new Label
//////            {
//////                Text = "Semester Results Input",
//////                Font = new Font("Arial", 18, FontStyle.Bold),
//////                AutoSize = true,
//////                ForeColor = Color.DarkOrchid
//////            };
//////            this.Controls.Add(headingLabel);

//////            // Back Button
//////            backBtn = new Button
//////            {
//////                Text = "Back To Home",
//////                Width = 180,
//////                Height = 45,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkViolet,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 11, FontStyle.Bold)
//////            };
//////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//////            backBtn.FlatAppearance.BorderSize = 1;
//////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//////            backBtn.Click += new EventHandler(backBtn_Click);
//////            this.Controls.Add(backBtn);

//////            // Branch ComboBox
//////            branchCombo = new ComboBox
//////            {
//////                Width = 200,
//////                Height = 30,
//////                Font = new Font("Arial", 10),
//////                DropDownStyle = ComboBoxStyle.DropDownList
//////            };
//////            this.Controls.Add(branchCombo);

//////            // Academic Year ComboBox
//////            acYearCombo = new ComboBox
//////            {
//////                Width = 200,
//////                Height = 30,
//////                Font = new Font("Arial", 10),
//////                DropDownStyle = ComboBoxStyle.DropDownList
//////            };
//////            this.Controls.Add(acYearCombo);

//////            // Year ComboBox
//////            yearCombo = new ComboBox
//////            {
//////                Width = 200,
//////                Height = 30,
//////                Font = new Font("Arial", 10),
//////                DropDownStyle = ComboBoxStyle.DropDownList
//////            };
//////            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
//////            yearCombo.SelectedIndex = 0;
//////            this.Controls.Add(yearCombo);

//////            // Semester ComboBox
//////            semCombo = new ComboBox
//////            {
//////                Width = 200,
//////                Height = 30,
//////                Font = new Font("Arial", 10),
//////                DropDownStyle = ComboBoxStyle.DropDownList
//////            };
//////            semCombo.Items.AddRange(new string[] { "1", "2" });
//////            semCombo.SelectedIndex = 0;
//////            this.Controls.Add(semCombo);

//////            // Submit Button
//////            submitBtn = new Button
//////            {
//////                Text = "Submit",
//////                Width = 180,
//////                Height = 45,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkOrchid,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 11, FontStyle.Bold)
//////            };
//////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////            submitBtn.FlatAppearance.BorderSize = 1;
//////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//////            submitBtn.Click += new EventHandler(submitBtn_Click);
//////            this.Controls.Add(submitBtn);

//////            ArrangeControls();
//////        }

//////        private void ArrangeControls()
//////        {
//////            int startY = 30;

//////            // Center heading
//////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//////            startY = headingLabel.Bottom + 50;

//////            // Back button
//////            backBtn.Location = new Point(50, startY);
//////            startY = backBtn.Bottom + Margin;

//////            // ComboBoxes and Submit button
//////            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
//////            branchCombo.Location = new Point(comboX, startY);
//////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//////            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//////            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
//////            submitBtn.Location = new Point(comboX + 4 * (200 + Margin), startY);
//////        }

//////        private void PopulateComboBoxes()
//////        {
//////            // Branches
//////            var marks = LoadJsonData<Studentm>("studentMarks.json");
//////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//////            branchCombo.Items.AddRange(branches.ToArray());
//////            if (branches.Any()) branchCombo.SelectedIndex = 0;

//////            // Academic Years
//////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//////            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
//////            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
//////        }

//////        private string GetBranchFromStudentId(string studentId)
//////        {
//////            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//////            studentId = studentId.ToUpper().Trim();
//////            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
//////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
//////            return "Unknown";
//////        }

//////        private void submitBtn_Click(object sender, EventArgs e)
//////        {
//////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
//////                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
//////            {
//////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////                return;
//////            }

//////            string branch = branchCombo.SelectedItem.ToString();
//////            string acYear = acYearCombo.SelectedItem.ToString();
//////            string year = yearCombo.SelectedItem.ToString();
//////            string semester = semCombo.SelectedItem.ToString();

//////            var results = LoadResults(branch, acYear, year, semester, result);
//////            SemesterResultsForm resultsForm = new SemesterResultsForm(results, branch, acYear, year, semester, this);
//////            resultsForm.Show();
//////            this.Hide();
//////        }

//////        private List<StudentR> LoadResults(string branch, string acYear, string year, string semester, List<StudentR> results)
//////        {
//////            var marks = LoadJsonData<Studentm>("studentMarks.json");
//////            var subjects = LoadJsonData<Subj>("subjectCodes.json");
//////            var names = LoadJsonData<StudentName>("studentnames.json");
//////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

//////            // Map academic years and names
//////            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
//////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//////            // Filter subjects by year and semester
//////            var relevantSubjects = subjects
//////                .Where(s => s.Year == year && s.Semester == semester)
//////                .ToList();
//////            var subjectCreditsDict = relevantSubjects
//////                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
//////            var subjectNameDict = marks
//////                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
//////                .GroupBy(m => m.SubjectCode.ToUpper())
//////                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//////            // Filter marks by branch, academic year, and subjects
//////            var filteredMarks = marks
//////                .Where(m =>
//////                    GetBranchFromStudentId(m.StudentId) == branch &&
//////                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
//////                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
//////                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
//////                    m.Grade != "F" && m.Grade != "Ab"
//////                )
//////                .Select(m => new
//////                {
//////                    m.StudentId,
//////                    m.SubjectCode,
//////                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
//////                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
//////                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
//////                })
//////                .ToList();

//////            // Group by student
//////            var groupedByStudent = filteredMarks
//////                .GroupBy(d => d.StudentId.ToUpper())
//////                .Select(g => new
//////                {
//////                    StudentId = g.Key,
//////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//////                    Subjects = g.ToDictionary(
//////                        s => s.SubjectCode.ToUpper(),
//////                        s => new { s.SubjectName, s.GradePoints, s.Credits },
//////                        StringComparer.OrdinalIgnoreCase)
//////                })
//////                .ToList();

//////            // Create results
//////            var resultss = new List<StudentR>();
//////            foreach (var student in groupedByStudent)
//////            {
//////                var result = new StudentR
//////                {
//////                    StudentId = student.StudentId,
//////                    StudentName = student.StudentName,
//////                    SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
//////                    TotalGradePoints = "0",
//////                    SGPA = "-"
//////                };

//////                double totalGradePoints = 0;
//////                double totalCredits = 0;

//////                foreach (var subject in relevantSubjects)
//////                {
//////                    string subjectCode = subject.SubjectCode.ToUpper();
//////                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
//////                    if (student.Subjects.ContainsKey(subjectCode))
//////                    {
//////                        var subj = student.Subjects[subjectCode];
//////                        double gradePoints = subj.GradePoints * subj.Credits;
//////                        result.SubjectResults[subjectName] = gradePoints.ToString("F1");
//////                        totalGradePoints += gradePoints;
//////                        totalCredits += subj.Credits;
//////                    }
//////                    else
//////                    {
//////                        result.SubjectResults[subjectName] = "-";
//////                    }
//////                }

//////                result.TotalGradePoints = totalGradePoints.ToString("F1");
//////                if (totalCredits > 0)
//////                {
//////                    result.SGPA = (totalGradePoints / totalCredits).ToString("F2");
//////                }

//////                resultss.Add(result);
//////            }

//////            return result;
//////        }

//////        private List<T> LoadJsonData<T>(string filePath)
//////        {
//////            if (!File.Exists(filePath))
//////            {
//////                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////                return new List<T>();
//////            }

//////            try
//////            {
//////                var json = File.ReadAllText(filePath);
//////                var data = JsonConvert.DeserializeObject<List<T>>(json);
//////                return data ?? new List<T>();
//////            }
//////            catch (Exception ex)
//////            {
//////                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////                return new List<T>();
//////            }
//////        }

//////        private void backBtn_Click(object sender, EventArgs e)
//////        {
//////            this.Hide();
//////            //admindashboard adb = new admindashboard();
//////            //adb.Show();
//////        }

//////        private void subject_wise_Gp_Load(object sender, EventArgs e)
//////        {

//////        }
//////    }

//////    public class Studentm
//////    {
//////        public string StudentId { get; set; }
//////        public string SubjectCode { get; set; }
//////        public string SubjectName { get; set; }
//////        public string Internal { get; set; }
//////        public string External { get; set; }
//////        public string Total { get; set; }
//////        public string Grade { get; set; }
//////        public string Credits { get; set; }
//////        public string GradePoints { get; set; }
//////    }

//////    public class Subj
//////    {
//////        public string SubjectCode { get; set; }
//////        public string Year { get; set; }
//////        public string Semester { get; set; }
//////        public string Credits { get; set; }
//////    }

//////    //public class StudentName
//////    //{
//////    //    public string studentid { get; set; }
//////    //    public string name { get; set; }
//////    //}

//////    //public class AcademicYear
//////    //{
//////    //    public string studentid { get; set; }
//////    //    public string academic_year { get; set; }
//////    //}

//////    public class StudentR
//////    {
//////        public string StudentId { get; set; }
//////        public string StudentName { get; set; }
//////        public Dictionary<string, string> SubjectResults { get; set; }
//////        public string TotalGradePoints { get; set; }
//////        public string SGPA { get; set; }
//////    }
//////}

////using Newtonsoft.Json;
////using System;
////using System.Collections.Generic;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Windows.Forms;

////namespace project_RYS.Forms
////{
////    public partial class subject_wise_Gp : Form
////    {
////        private ComboBox branchCombo;
////        private ComboBox acYearCombo;
////        private ComboBox yearCombo;
////        private ComboBox semCombo;
////        private Button backBtn;
////        private Button submitBtn;
////        private Label headingLabel;
////        private const int Margin = 30;

////        public subject_wise_Gp()
////        {
////            InitializeComponent();
////            SetupForm();
////            PopulateComboBoxes();
////        }

////        private void SetupForm()
////        {
////            this.Size = new Size(1400, 900);
////            this.AutoScroll = true;
////            this.BackColor = Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Semester Results Input",
////                Font = new Font("Arial", 18, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backBtn = new Button
////            {
////                Text = "Back To Home",
////                Width = 180,
////                Height = 45,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkViolet,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 11, FontStyle.Bold)
////            };
////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
////            backBtn.FlatAppearance.BorderSize = 1;
////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
////            backBtn.Click += new EventHandler(backBtn_Click);
////            this.Controls.Add(backBtn);

////            // Branch ComboBox
////            branchCombo = new ComboBox
////            {
////                Width = 200,
////                Height = 30,
////                Font = new Font("Arial", 10),
////                DropDownStyle = ComboBoxStyle.DropDownList
////            };
////            this.Controls.Add(branchCombo);

////            // Academic Year ComboBox
////            acYearCombo = new ComboBox
////            {
////                Width = 200,
////                Height = 30,
////                Font = new Font("Arial", 10),
////                DropDownStyle = ComboBoxStyle.DropDownList
////            };
////            this.Controls.Add(acYearCombo);

////            // Year ComboBox
////            yearCombo = new ComboBox
////            {
////                Width = 200,
////                Height = 30,
////                Font = new Font("Arial", 10),
////                DropDownStyle = ComboBoxStyle.DropDownList
////            };
////            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
////            yearCombo.SelectedIndex = 0;
////            this.Controls.Add(yearCombo);

////            // Semester ComboBox
////            semCombo = new ComboBox
////            {
////                Width = 200,
////                Height = 30,
////                Font = new Font("Arial", 10),
////                DropDownStyle = ComboBoxStyle.DropDownList
////            };
////            semCombo.Items.AddRange(new string[] { "1", "2" });
////            semCombo.SelectedIndex = 0;
////            this.Controls.Add(semCombo);

////            // Submit Button
////            submitBtn = new Button
////            {
////                Text = "Submit",
////                Width = 180,
////                Height = 45,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 11, FontStyle.Bold)
////            };
////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////            submitBtn.FlatAppearance.BorderSize = 1;
////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
////            submitBtn.Click += new EventHandler(submitBtn_Click);
////            this.Controls.Add(submitBtn);

////            ArrangeControls();
////        }

////        private void ArrangeControls()
////        {
////            int startY = 30;

////            // Center heading
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            startY = headingLabel.Bottom + 50;

////            // Back button
////            backBtn.Location = new Point(50, startY);
////            startY = backBtn.Bottom + Margin;

////            // ComboBoxes and Submit button
////            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
////            branchCombo.Location = new Point(comboX, startY);
////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
////            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
////            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
////            submitBtn.Location = new Point(comboX + 4 * (200 + Margin), startY);
////        }

////        private void PopulateComboBoxes()
////        {
////            // Branches
////            var marks = LoadJsonData<StudentMa>("studentMarks.json");
////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
////            branchCombo.Items.AddRange(branches.ToArray());
////            if (branches.Any()) branchCombo.SelectedIndex = 0;

////            // Academic Years
////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
////            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
////            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
////        }

////        private string GetBranchFromStudentId(string studentId)
////        {
////            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
////            studentId = studentId.ToUpper().Trim();
////            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
////            return "Unknown";
////        }

////        private void submitBtn_Click(object sender, EventArgs e)
////        {
////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
////                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
////            {
////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return;
////            }

////            string branch = branchCombo.SelectedItem.ToString();
////            string acYear = acYearCombo.SelectedItem.ToString();
////            string year = yearCombo.SelectedItem.ToString();
////            string semester = semCombo.SelectedItem.ToString();

////            var results = LoadResults(branch, acYear, year, semester);
////            if (!results.Any())
////            {
////                MessageBox.Show("No results found for the selected criteria.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }
////            SemesterResultsForm resultsForm = new SemesterResultsForm(results, branch, acYear, year, semester, this);
////            resultsForm.Show();
////            this.Hide();
////        }

////        private List<StudentR> LoadResults(string branch, string acYear, string year, string semester)
////        {
////            var marks = LoadJsonData<StudentMa>("studentMarks.json");
////            var subjects = LoadJsonData<Sub>("subjectCodes.json");
////            var names = LoadJsonData<StudentName>("studentnames.json");
////            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

////            // Map academic years and names
////            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

////            // Filter subjects by year and semester
////            var relevantSubjects = subjects
////                .Where(s => s.Year == year && s.Semester == semester)
////                .OrderBy(s => s.SubjectCode) // Order by SubjectCode for consistent column order
////                .ToList();
////            var subjectCreditsDict = relevantSubjects
////                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
////            var subjectNameDict = marks
////                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
////                .GroupBy(m => m.SubjectCode.ToUpper())
////                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

////            // Filter marks by branch, academic year, and subjects
////            var filteredMarks = marks
////                .Where(m =>
////                    GetBranchFromStudentId(m.StudentId) == branch &&
////                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
////                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
////                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
////                    m.Grade != "F" && m.Grade != "Ab"
////                )
////                .Select(m => new
////                {
////                    m.StudentId,
////                    m.SubjectCode,
////                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
////                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
////                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
////                })
////                .ToList();

////            // Group by student
////            var groupedByStudent = filteredMarks
////                .GroupBy(d => d.StudentId.ToUpper())
////                .Select(g => new
////                {
////                    StudentId = g.Key,
////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
////                    Subjects = g.ToDictionary(
////                        s => s.SubjectCode.ToUpper(),
////                        s => new { s.SubjectName, s.GradePoints, s.Credits },
////                        StringComparer.OrdinalIgnoreCase)
////                })
////                .ToList();

////            // Create results
////            var results = new List<StudentR>();
////            foreach (var student in groupedByStudent)
////            {
////                var result = new StudentR
////                {
////                    StudentId = student.StudentId,
////                    StudentName = student.StudentName,
////                    SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
////                    TotalGradePoints = "0",
////                    SGPA = "-"
////                };

////                double totalGradePoints = 0;
////                double totalCredits = 0;

////                foreach (var subject in relevantSubjects)
////                {
////                    string subjectCode = subject.SubjectCode.ToUpper();
////                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
////                    if (student.Subjects.ContainsKey(subjectCode))
////                    {
////                        var subj = student.Subjects[subjectCode];
////                        double gradePoints = subj.GradePoints * subj.Credits;
////                        if (double.IsInfinity(gradePoints) && gradePoints >= 0)
////                        {
////                            result.SubjectResults[subjectName] = gradePoints.ToString("F1");
////                            totalGradePoints += gradePoints;
////                            totalCredits += subj.Credits;
////                        }
////                        else
////                        {
////                            result.SubjectResults[subjectName] = "-";
////                        }
////                    }
////                    else
////                    {
////                        result.SubjectResults[subjectName] = "-";
////                    }
////                }

////                result.TotalGradePoints = totalGradePoints.ToString("F1");
////                if (totalCredits > 0 && double.IsInfinity(totalGradePoints / totalCredits))
////                {
////                    result.SGPA = (totalGradePoints / totalCredits).ToString("F2");
////                }

////                if (result.SubjectResults.Any(kvp => kvp.Value != "-"))
////                {
////                    results.Add(result);
////                }
////            }

////            return results;
////        }

////        private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new List<T>();
////            }

////            try
////            {
////                var json = File.ReadAllText(filePath);
////                var data = JsonConvert.DeserializeObject<List<T>>(json);
////                return data ?? new List<T>();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new List<T>();
////            }
////        }

////        private void backBtn_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            //admindashboard adb = new admindashboard();
////            //adb.Show();
////        }
////    }

////    public class StudentMa
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////        public string GradePoints { get; set; }
////    }

////    public class Sub
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////        public string Credits { get; set; }
////    }

////    //public class StudentName
////    //{
////    //    public string studentid { get; set; }
////    //    public string name { get; set; }
////    //}

////    //public class AcademicYear
////    //{
////    //    public string studentid { get; set; }
////    //    public string academic_year { get; set; }
////    //}
////}

//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class subject_wise_Gp : Form
//    {
//        private ComboBox branchCombo;
//        private ComboBox acYearCombo;
//        private ComboBox yearCombo;
//        private ComboBox semCombo;
//        private Button backBtn;
//        private Button submitBtn;
//        private Label headingLabel;
//        private const int Margin = 30;

//        public subject_wise_Gp()
//        {
//            InitializeComponent();
//            SetupForm();
//            PopulateComboBoxes();
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Semester Results Input",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back To Home",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkViolet,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//            backBtn.FlatAppearance.BorderSize = 1;
//            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//            backBtn.Click += new EventHandler(backBtn_Click);
//            this.Controls.Add(backBtn);

//            // Branch ComboBox
//            branchCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            this.Controls.Add(branchCombo);

//            // Academic Year ComboBox
//            acYearCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            this.Controls.Add(acYearCombo);

//            // Year ComboBox
//            yearCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
//            yearCombo.SelectedIndex = 0;
//            this.Controls.Add(yearCombo);

//            // Semester ComboBox
//            semCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            semCombo.Items.AddRange(new string[] { "1", "2" });
//            semCombo.SelectedIndex = 0;
//            this.Controls.Add(semCombo);

//            // Submit Button
//            submitBtn = new Button
//            {
//                Text = "Submit",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            submitBtn.FlatAppearance.BorderSize = 1;
//            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//            submitBtn.Click += new EventHandler(submitBtn_Click);
//            this.Controls.Add(submitBtn);

//            ArrangeControls();
//        }

//        private void ArrangeControls()
//        {
//            int startY = 30;

//            // Center heading
//            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//            startY = headingLabel.Bottom + 50;

//            // Back button
//            backBtn.Location = new Point(50, startY);
//            startY = backBtn.Bottom + Margin;

//            // ComboBoxes and Submit button
//            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
//            branchCombo.Location = new Point(comboX, startY);
//            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
//            submitBtn.Location = new Point(comboX + 4 * (200 + Margin), startY);
//        }

//        private void PopulateComboBoxes()
//        {
//            // Branches
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//            branchCombo.Items.AddRange(branches.ToArray());
//            if (branches.Any()) branchCombo.SelectedIndex = 0;

//            // Academic Years
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
//            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();
//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
//            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
//            return "Unknown";
//        }

//        private void submitBtn_Click(object sender, EventArgs e)
//        {
//            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
//                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
//            {
//                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            string branch = branchCombo.SelectedItem.ToString();
//            string acYear = acYearCombo.SelectedItem.ToString();
//            string year = yearCombo.SelectedItem.ToString();
//            string semester = semCombo.SelectedItem.ToString();

//            var results = LoadResults(branch, acYear, year, semester);
//            if (!results.Any())
//            {
//                MessageBox.Show("No results found for the selected criteria.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            SemesterResultsForm resultsForm = new SemesterResultsForm(results, branch, acYear, year, semester, this);
//            resultsForm.Show();
//            this.Hide();
//        }

//        private List<StudentR> LoadResults(string branch, string acYear, string year, string semester)
//        {
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var subjects = LoadJsonData<Subjectss>("subjectCodes.json");
//            var names = LoadJsonData<StudentName>("studentnames.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

//            // Map academic years and names
//            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
//            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//            // Filter subjects by year and semester, order by SubjectCode
//            var relevantSubjects = subjects
//                .Where(s => s.Year == year && s.Semester == semester)
//                .OrderBy(s => s.SubjectCode) // Ensures Mathematics (18321) before Physics (18320)
//                .ToList();

//            var subjectCreditsDict = relevantSubjects
//                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
//            var subjectNameDict = marks
//                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
//                .GroupBy(m => m.SubjectCode.ToUpper())
//                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//            // Filter marks by branch, academic year, and subjects
//            var filteredMarks = marks
//                .Where(m =>
//                    GetBranchFromStudentId(m.StudentId) == branch &&
//                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
//                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
//                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
//                    m.Grade != "F" && m.Grade != "Ab"
//                )
//                .Select(m => new
//                {
//                    m.StudentId,
//                    m.SubjectCode,
//                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
//                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
//                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
//                })
//                .ToList();

//            // Group by student
//            var groupedByStudent = filteredMarks
//                .GroupBy(d => d.StudentId.ToUpper())
//                .Select(g => new
//                {
//                    StudentId = g.Key,
//                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//                    Subjects = g.ToDictionary(
//                        s => s.SubjectCode.ToUpper(),
//                        s => new { s.SubjectName, s.GradePoints, s.Credits },
//                        StringComparer.OrdinalIgnoreCase)
//                })
//                .ToList();

//            // Create results
//            var results = new List<StudentR>();
//            foreach (var student in groupedByStudent)
//            {
//                var result = new StudentR
//                {
//                    StudentId = student.StudentId,
//                    StudentName = student.StudentName,
//                    SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
//                    TotalGradePoints = "0",
//                    SGPA = "-"
//                };

//                double totalGradePoints = 0;
//                double totalCredits = 0;

//                foreach (var subject in relevantSubjects)
//                {
//                    string subjectCode = subject.SubjectCode.ToUpper();
//                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
//                    if (student.Subjects.ContainsKey(subjectCode))
//                    {
//                        var subj = student.Subjects[subjectCode];
//                        double gradePoints = subj.GradePoints * subj.Credits;

//                        // Replacing IsFinite with a compatibility check
//                        if (!double.IsNaN(gradePoints) && !double.IsInfinity(gradePoints) && gradePoints >= 0)
//                        {
//                            result.SubjectResults[subjectName] = gradePoints.ToString("F1");
//                            totalGradePoints += gradePoints;
//                            totalCredits += subj.Credits;
//                        }
//                        else
//                        {
//                            result.SubjectResults[subjectName] = "-";
//                        }
//                    }
//                    else
//                    {
//                        result.SubjectResults[subjectName] = "-";
//                    }

//                }
//                result.TotalGradePoints = totalGradePoints.ToString("F1");

//                if (totalCredits > 0)
//                {
//                    double sgpa = totalGradePoints / totalCredits;

//                    if (!double.IsInfinity(sgpa) && !double.IsNaN(sgpa))
//                    {
//                        result.SGPA = sgpa.ToString("F2");
//                    }
//                }


//                if (result.SubjectResults.Any(kvp => kvp.Value != "-"))
//                {
//                    results.Add(result);
//                }
//            }

//            return results;
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<T>();
//            }

//            try
//            {
//                var json = File.ReadAllText(filePath);
//                var data = JsonConvert.DeserializeObject<List<T>>(json);
//                return data ?? new List<T>();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<T>();
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//            admindashboard adb = new admindashboard();
//            adb.Show();
//        }
//    }

//    public class StudentMa
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string Credits { get; set; }
//        public string GradePoints { get; set; }
//    }

//    public class Subjectss
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//        public string Credits { get; set; }
//    }

//

//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class subject_wise_Gp : Form
//    {
//        private ComboBox branchCombo;
//        private ComboBox acYearCombo;
//        private ComboBox yearCombo;
//        private ComboBox semCombo;
//        private Button backBtn;
//        private Button submitBtn;
//        private Label headingLabel;
//        private const int Margin = 30;

//        public subject_wise_Gp()
//        {
//            InitializeComponent();
//            SetupForm();
//            PopulateComboBoxes();
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Semester Results Input",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back To Home",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkViolet,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//            backBtn.FlatAppearance.BorderSize = 1;
//            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//            backBtn.Click += new EventHandler(backBtn_Click);
//            this.Controls.Add(backBtn);

//            // Branch ComboBox
//            branchCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            this.Controls.Add(branchCombo);

//            // Academic Year ComboBox
//            acYearCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            this.Controls.Add(acYearCombo);

//            // Year ComboBox
//            yearCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
//            yearCombo.SelectedIndex = 0;
//            this.Controls.Add(yearCombo);

//            // Semester ComboBox
//            semCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            semCombo.Items.AddRange(new string[] { "1", "2" });
//            semCombo.SelectedIndex = 0;
//            this.Controls.Add(semCombo);

//            // Submit Button
//            submitBtn = new Button
//            {
//                Text = "Submit",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            submitBtn.FlatAppearance.BorderSize = 1;
//            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//            submitBtn.Click += new EventHandler(submitBtn_Click);
//            this.Controls.Add(submitBtn);

//            ArrangeControls();
//        }

//        private void ArrangeControls()
//        {
//            int startY = 30;

//            // Center heading
//            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//            startY = headingLabel.Bottom + 50;

//            // Back button
//            backBtn.Location = new Point(50, startY);
//            startY = backBtn.Bottom + Margin;

//            // ComboBoxes and Submit button
//            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
//            branchCombo.Location = new Point(comboX, startY);
//            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
//            submitBtn.Location = new Point(comboX + 4 * (200 + Margin), startY);
//        }

//        private void PopulateComboBoxes()
//        {
//            // Branches
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//            branchCombo.Items.AddRange(branches.ToArray());
//            if (branches.Any()) branchCombo.SelectedIndex = 0;

//            // Academic Years
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
//            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
//            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
//            studentId = studentId.ToUpper().Trim();
//            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
//            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
//            return "Unknown";
//        }

//        private void submitBtn_Click(object sender, EventArgs e)
//        {
//            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
//                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
//            {
//                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            string branch = branchCombo.SelectedItem.ToString();
//            string acYear = acYearCombo.SelectedItem.ToString();
//            string year = yearCombo.SelectedItem.ToString();
//            string semester = semCombo.SelectedItem.ToString();

//            var (results, relevantSubjects) = LoadResults(branch, acYear, year, semester);
//            if (!results.Any())
//            {
//                MessageBox.Show("No results found for the selected criteria.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            SemesterResultsForm resultsForm = new SemesterResultsForm(results, relevantSubjects, branch, acYear, year, semester, this);
//            resultsForm.Show();
//            this.Hide();
//        }

//        private (List<StudentR>, List<Subjectss>) LoadResults(string branch, string acYear, string year, string semester)
//        {
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var subjects = LoadJsonData<Subjectss>("subjectCodes.json");
//            var names = LoadJsonData<StudentName>("studentnames.json");
//            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

//            // Map academic years and names
//            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
//            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//            // Filter subjects by year and semester, order by SubjectCode
//            var relevantSubjects = subjects
//                .Where(s => s.Year == year && s.Semester == semester)
//                .OrderBy(s => s.SubjectCode) // Ensures Mathematics (18321) before Physics (18320)
//                .ToList();

//            var subjectCreditsDict = relevantSubjects
//                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
//            var subjectNameDict = marks
//                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
//                .GroupBy(m => m.SubjectCode.ToUpper())
//                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//            // Filter marks by branch, academic year, and subjects
//            var filteredMarks = marks
//                .Where(m =>
//                    GetBranchFromStudentId(m.StudentId) == branch &&
//                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
//                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
//                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
//                    m.Grade != "F" && m.Grade != "Ab"
//                )
//                .Select(m => new
//                {
//                    m.StudentId,
//                    m.SubjectCode,
//                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
//                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
//                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
//                })
//                .ToList();

//            // Group by student
//            var groupedByStudent = filteredMarks
//                .GroupBy(d => d.StudentId.ToUpper())
//                .Select(g => new
//                {
//                    StudentId = g.Key,
//                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//                    Subjects = g.ToDictionary(
//                        s => s.SubjectCode.ToUpper(),
//                        s => new { s.SubjectName, s.GradePoints, s.Credits },
//                        StringComparer.OrdinalIgnoreCase)
//                })
//                .ToList();

//            // Create results
//            var results = new List<StudentR>();
//            foreach (var student in groupedByStudent)
//            {
//                var result = new StudentR
//                {
//                    StudentId = student.StudentId,
//                    StudentName = student.StudentName,
//                    SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
//                    TotalGradePoints = "0",
//                    SGPA = "-"
//                };

//                double totalGradePoints = 0;
//                double totalCredits = 0;

//                foreach (var subject in relevantSubjects)
//                {
//                    string subjectCode = subject.SubjectCode.ToUpper();
//                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
//                    if (student.Subjects.ContainsKey(subjectCode))
//                    {
//                        var subj = student.Subjects[subjectCode];
//                        double gradePoints = subj.GradePoints * subj.Credits;
//                        if (!double.IsNaN(gradePoints) && !double.IsInfinity(gradePoints) && gradePoints >= 0)
//                        {
//                            result.SubjectResults[subjectName] = gradePoints.ToString("F1");
//                            totalGradePoints += gradePoints;
//                            totalCredits += subj.Credits;
//                        }
//                        else
//                        {
//                            result.SubjectResults[subjectName] = "-";
//                        }
//                    }
//                    else
//                    {
//                        result.SubjectResults[subjectName] = "-";
//                    }
//                }

//                result.TotalGradePoints = totalGradePoints.ToString("F1");
//                if (totalCredits > 0)
//                {
//                    double sgpa = totalGradePoints / totalCredits;
//                    if (!double.IsNaN(sgpa) && !double.IsInfinity(sgpa))
//                    {
//                        result.SGPA = sgpa.ToString("F2");
//                    }
//                }

//                if (result.SubjectResults.Any(kvp => kvp.Value != "-"))
//                {
//                    results.Add(result);
//                }
//            }

//            return (results, relevantSubjects);
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<T>();
//            }

//            try
//            {
//                var json = File.ReadAllText(filePath);
//                var data = JsonConvert.DeserializeObject<List<T>>(json);
//                return data ?? new List<T>();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<T>();
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//            admindashboard adb = new admindashboard();
//            adb.Show();
//        }
//    }

//    public class StudentMa
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string Credits { get; set; }
//        public string GradePoints { get; set; }
//    }

//    public class Subjectss
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//        public string Credits { get; set; }
//    }
//}




using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class subject_wise_Gp : Form
    {
        private ComboBox branchCombo;
        private ComboBox acYearCombo;
        private ComboBox yearCombo;
        private ComboBox semCombo;
        private Button backBtn;
        private Button submitBtn;
        private Label headingLabel;
        private const int Margin = 30;

        public subject_wise_Gp()
        {
            InitializeComponent();
            SetupForm();
            PopulateComboBoxes();
        }

        private void SetupForm()
        {
            this.Size = new Size(1400, 900);
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Heading Label
            headingLabel = new Label
            {
                Text = "Semester Results Input",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid
            };
            this.Controls.Add(headingLabel);


            // Back Button
            backBtn = new Button
            {
                Text = "Back To Home",
                Width = 180,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkViolet,
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };
            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
            backBtn.FlatAppearance.BorderSize = 1;
            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
            backBtn.Click += new EventHandler(backBtn_Click);
            this.Controls.Add(backBtn);

            // Create a Label
            Label branchLabel = new Label();
            branchLabel.Text = "Branch Name";
            branchLabel.Location = new System.Drawing.Point(150, 200);
            branchLabel.AutoSize = true;
            this.Controls.Add(branchLabel);
            branchLabel.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);



            // Create a Label
            Label acyear = new Label();
            acyear.Text = " Academic Year";
            acyear.Location = new System.Drawing.Point(350, 200);
            acyear.AutoSize = true;
            this.Controls.Add(acyear);
            acyear.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);


            // Create a Label
            Label year = new Label();
            year.Text = "  Year";
            year.Location = new System.Drawing.Point(590, 200);
            year.AutoSize = true;
            this.Controls.Add(year);
            year.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);


            // Create a Label
            Label sem = new Label();
            sem.Text = " semester";
            sem.Location = new System.Drawing.Point(820, 200);
            sem.AutoSize = true;
            this.Controls.Add(sem);
            sem.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);


            // Branch ComboBox
            branchCombo = new ComboBox
            {
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(branchCombo);

            // Academic Year ComboBox
            acYearCombo = new ComboBox
            {
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(acYearCombo);

            // Year ComboBox
            yearCombo = new ComboBox
            {
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            yearCombo.Items.AddRange(new string[] { "1", "2", "3", "4" });
            yearCombo.SelectedIndex = 0;
            this.Controls.Add(yearCombo);

            // Semester ComboBox
            semCombo = new ComboBox
            {
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            semCombo.Items.AddRange(new string[] { "1", "2" });
            semCombo.SelectedIndex = 0;
            this.Controls.Add(semCombo);

            // Submit Button
            submitBtn = new Button
            {
                Text = "Submit",
                Width = 180,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrchid,
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };
            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
            submitBtn.FlatAppearance.BorderSize = 1;
            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
            submitBtn.Click += new EventHandler(submitBtn_Click);
            this.Controls.Add(submitBtn);

            ArrangeControls();
        }

        private void ArrangeControls()
        {
            int startY = 50;

            // Center heading
            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
            startY = headingLabel.Bottom + 50;

            // Back button
            backBtn.Location = new Point(50, startY);
            startY = backBtn.Bottom + Margin;
            Console.WriteLine(startY);

            startY += 20;
            // ComboBoxes and Submit button
            int comboX = (this.ClientSize.Width - (5 * 200 + 4 * Margin)) / 2;
            branchCombo.Location = new Point(comboX, startY);
            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
            yearCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
            semCombo.Location = new Point(comboX + 3 * (200 + Margin), startY);
            startY+= 50;
            submitBtn.Location = new Point(comboX + 4 * (60 + Margin), 270);
        }

        private void PopulateComboBoxes()
        {
            // Branches
            var marks = LoadJsonData<StudentMa>("studentMarks.json");
            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
            branchCombo.Items.AddRange(branches.ToArray());
            if (branches.Any()) branchCombo.SelectedIndex = 0;

            // Academic Years
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");
            acYearCombo.Items.AddRange(academicYears.Select(ay => ay.academic_year).Distinct().OrderBy(ay => ay).ToArray());
            if (acYearCombo.Items.Count > 0) acYearCombo.SelectedIndex = 0;
        }

        //private string GetBranchFromStudentId(string studentId)
        //{
        //    if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
        //    studentId = studentId.ToUpper().Trim();
        //    if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
        //    if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
        //    if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";

        //    return "Unknown";
        //}
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

        private void submitBtn_Click(object sender, EventArgs e)
        {
            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null ||
                yearCombo.SelectedItem == null || semCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string branch = branchCombo.SelectedItem.ToString();
            string acYear = acYearCombo.SelectedItem.ToString();
            string year = yearCombo.SelectedItem.ToString();
            string semester = semCombo.SelectedItem.ToString();

            var (results, relevantSubjects) = LoadResults(branch, acYear, year, semester);
            if (!results.Any())
            {
                MessageBox.Show("No results found for the selected criteria.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SemesterResultsForm resultsForm = new SemesterResultsForm(results, relevantSubjects, branch, acYear, year, semester, this);
            resultsForm.Show();
            this.Hide();
        }

        private (List<StudentR>, List<Subjectss>) LoadResults(string branch, string acYear, string year, string semester)
        {
            var marks = LoadJsonData<StudentMa>("studentMarks.json");
            var subjects = LoadJsonData<Subjectss>("subjectCodes.json");
            var names = LoadJsonData<StudentName>("studentnames.json");
            var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json");

            // Map academic years and names
            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);
            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

            // Filter subjects by year and semester, order by SubjectCode
            var relevantSubjects = subjects
                .Where(s => s.Year == year && s.Semester == semester)
                .OrderBy(s => s.SubjectCode) // Ensures Mathematics (18321) before Physics (18320)
                .ToList();

            var subjectCreditsDict = relevantSubjects
                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
            var subjectNameDict = marks
                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
                .GroupBy(m => m.SubjectCode.ToUpper())
                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

            // Filter marks by branch, academic year, and subjects
            var filteredMarks = marks
                .Where(m =>
                    GetBranchFromStudentId(m.StudentId) == branch &&
                    academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
                    academicYearDict[m.StudentId.ToUpper()] == acYear &&
                    subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()) &&
                    m.Grade != "F" && m.Grade != "Ab"
                )
                .Select(m => new
                {
                    m.StudentId,
                    m.SubjectCode,
                    SubjectName = subjectNameDict.ContainsKey(m.SubjectCode.ToUpper()) ? subjectNameDict[m.SubjectCode.ToUpper()] : m.SubjectCode,
                    Credits = subjectCreditsDict[m.SubjectCode.ToUpper()],
                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0
                })
                .ToList();

            // Filter relevantSubjects to include only those with marks
            var subjectsWithMarks = filteredMarks.Select(m => m.SubjectCode.ToUpper()).Distinct().ToList();
            relevantSubjects = relevantSubjects
                .Where(s => subjectsWithMarks.Contains(s.SubjectCode.ToUpper()))
                .ToList();

            // Update subjectCreditsDict and subjectNameDict for filtered subjects
            subjectCreditsDict = relevantSubjects
                .ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);
            subjectNameDict = marks
                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
                .GroupBy(m => m.SubjectCode.ToUpper())
                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

            // Group by student
            var groupedByStudent = filteredMarks
                .GroupBy(d => d.StudentId.ToUpper())
                .Select(g => new
                {
                    StudentId = g.Key,
                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
                    Subjects = g.ToDictionary(
                        s => s.SubjectCode.ToUpper(),
                        s => new { s.SubjectName, s.GradePoints, s.Credits },
                        StringComparer.OrdinalIgnoreCase)
                })
                .ToList();

            // Create results
            var results = new List<StudentR>();
            foreach (var student in groupedByStudent)
            {
                var result = new StudentR
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                    TotalGradePoints = "0",
                    SGPA = "-"
                };

                double totalGradePoints = 0;
                double totalCredits = relevantSubjects.Sum(s => double.TryParse(s.Credits, out var c) ? c : 0);

                foreach (var subject in relevantSubjects)
                {
                    string subjectCode = subject.SubjectCode.ToUpper();
                    string subjectName = subjectNameDict.ContainsKey(subjectCode) ? subjectNameDict[subjectCode] : subject.SubjectCode;
                    if (student.Subjects.ContainsKey(subjectCode))
                    {
                        var subj = student.Subjects[subjectCode];
                        double gradePoints = subj.GradePoints * subj.Credits;
                        if (!double.IsNaN(gradePoints) && !double.IsInfinity(gradePoints) && gradePoints >= 0)
                        {
                            result.SubjectResults[subjectName] = gradePoints.ToString("F1");
                            totalGradePoints += gradePoints;
                        }
                        else
                        {
                            result.SubjectResults[subjectName] = "-";
                        }
                    }
                    else
                    {
                        result.SubjectResults[subjectName] = "-";
                    }
                }

                result.TotalGradePoints = totalGradePoints.ToString("F1");
                if (totalCredits > 0)
                {
                    double sgpa = totalGradePoints / totalCredits;
                    if (!double.IsNaN(sgpa) && !double.IsInfinity(sgpa))
                    {
                        result.SGPA = sgpa.ToString("F2");
                    }
                }

                if (result.SubjectResults.Any(kvp => kvp.Value != "-"))
                {
                    results.Add(result);
                }
            }

            return (results, relevantSubjects);
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

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.OpenForms["admindashboard"]?.Close();
            admindashboard adb = new admindashboard();
            adb.Show();

            try
            {
                extra_form child = new extra_form();
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
            //this.Hide();
            //admindashboard adb = new admindashboard();
            //adb.Show();
        }

        private void subject_wise_Gp_Load(object sender, EventArgs e)
        {

        }
    }

    public class StudentMa
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string Credits { get; set; }
        public string GradePoints { get; set; }
    }

    public class Subjectss
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Credits { get; set; }
    }

    //public class StudentName
    //{
    //    public string studentid { get; set; }
    //    public string name { get; set; }
    //}

    //public class AcademicYear
    //{
    //    public string studentid { get; set; }
    //    public string academic_year { get; set; }
    //}
}