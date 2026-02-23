//////////using Newtonsoft.Json;
//////////using OfficeOpenXml.Style;
//////////using OfficeOpenXml;
//////////using System;
//////////using System.Collections.Generic;
//////////using System.ComponentModel;
//////////using System.Data;
//////////using System.Drawing;
//////////using System.IO;
//////////using System.Linq;
//////////using System.Windows.Forms;

//////////namespace project_RYS.Forms
//////////{
//////////    public partial class getbtn : Form
//////////    {
//////////        private ComboBox branchCombo;
//////////        private ComboBox acYearCombo;
//////////        private ComboBox optionsCombo;
//////////        private Button backBtn;
//////////        private Button submitBtn;
//////////        private Button downloadBtn;
//////////        private DataGridView dataGridView;
//////////        private Label headingLabel;
//////////        private const int DataGridViewWidth = 1250;
//////////        private const int Margin = 20;
//////////        private List<StudentResult> results;
//////////        public getbtn()
//////////        {
//////////            InitializeComponent();
//////////            SetupForm();
//////////            PopulateComboBoxes();
//////////            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
//////////        }

//////////        private void SetupForm()
//////////        {
//////////            this.Size = new Size(1300, 800);
//////////            this.AutoScroll = true;
//////////            this.BackColor = Color.FromArgb(240, 240, 240);

//////////            // Heading Label
//////////            headingLabel = new Label
//////////            {
//////////                Text = "Student Results Report",
//////////                Font = new Font("Arial", 16, FontStyle.Bold),
//////////                AutoSize = true,
//////////                ForeColor = Color.DarkOrchid
//////////            };
//////////            this.Controls.Add(headingLabel);

//////////            // Back Button
//////////            backBtn = new Button
//////////            {
//////////                Text = "Back To Home",
//////////                Width = 200,
//////////                Height = 40,
//////////                FlatStyle = FlatStyle.Flat,
//////////                BackColor = Color.DarkViolet,
//////////                ForeColor = Color.White,
//////////                Font = new Font("Arial", 10, FontStyle.Bold)
//////////            };
//////////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//////////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//////////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//////////            backBtn.Click += new EventHandler(backbtn_Click);
//////////            this.Controls.Add(backBtn);

//////////            // Branch ComboBox
//////////            branchCombo = new ComboBox
//////////            {
//////////                Width = 200,
//////////                Height = 30,
//////////                Font = new Font("Arial", 10),
//////////                DropDownStyle = ComboBoxStyle.DropDownList
//////////            };
//////////            this.Controls.Add(branchCombo);

//////////            // Academic Year ComboBox
//////////            acYearCombo = new ComboBox
//////////            {
//////////                Width = 200,
//////////                Height = 30,
//////////                Font = new Font("Arial", 10),
//////////                DropDownStyle = ComboBoxStyle.DropDownList
//////////            };
//////////            this.Controls.Add(acYearCombo);

//////////            // Options ComboBox
//////////            optionsCombo = new ComboBox
//////////            {
//////////                Width = 200,
//////////                Height = 30,
//////////                Font = new Font("Arial", 10),
//////////                DropDownStyle = ComboBoxStyle.DropDownList
//////////            };
//////////            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
//////////            optionsCombo.SelectedIndex = 0; // Default to GPA
//////////            this.Controls.Add(optionsCombo);

//////////            // Submit Button
//////////            submitBtn = new Button
//////////            {
//////////                Text = "Submit",
//////////                Width = 200,
//////////                Height = 40,
//////////                FlatStyle = FlatStyle.Flat,
//////////                BackColor = Color.DarkOrchid,
//////////                ForeColor = Color.White,
//////////                Font = new Font("Arial", 10, FontStyle.Bold)
//////////            };
//////////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//////////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//////////            submitBtn.Click += new EventHandler(submitbtn_Click);
//////////            this.Controls.Add(submitBtn);

//////////            // Download Button
//////////            downloadBtn = new Button
//////////            {
//////////                Text = "Download",
//////////                Width = 200,
//////////                Height = 40,
//////////                FlatStyle = FlatStyle.Flat,
//////////                BackColor = Color.DarkOrchid,
//////////                ForeColor = Color.White,
//////////                Font = new Font("Arial", 10, FontStyle.Bold)
//////////            };
//////////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//////////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//////////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//////////            this.Controls.Add(downloadBtn);

//////////            // DataGridView
//////////            dataGridView = new DataGridView
//////////            {
//////////                Width = DataGridViewWidth,
//////////                AutoGenerateColumns = false,
//////////                RowHeadersVisible = false,
//////////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//////////                AllowUserToAddRows = false,
//////////                ScrollBars = ScrollBars.Vertical,
//////////                ReadOnly = true,
//////////                AllowUserToResizeColumns = false,
//////////                AllowUserToResizeRows = false,
//////////                EnableHeadersVisualStyles = false,
//////////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//////////                {
//////////                    BackColor = Color.DarkOrchid,
//////////                    ForeColor = Color.White,
//////////                    Font = new Font("Arial", 10, FontStyle.Bold),
//////////                    Alignment = DataGridViewContentAlignment.MiddleCenter
//////////                },
//////////                RowsDefaultCellStyle = new DataGridViewCellStyle
//////////                {
//////////                    Font = new Font("Arial", 10),
//////////                    BackColor = Color.White
//////////                },
//////////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//////////                {
//////////                    BackColor = Color.DarkViolet
//////////                },
//////////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//////////                GridColor = Color.White
//////////            };
//////////            dataGridView.RowTemplate.Height = 50;
//////////            dataGridView.ColumnHeadersHeight = 40;
//////////            this.Controls.Add(dataGridView);

//////////            ArrangeControls();
//////////        }

//////////        private void ArrangeControls()
//////////        {
//////////            int startY = 20;

//////////            // Center heading
//////////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//////////            startY = headingLabel.Bottom + Margin;

//////////            // Back button
//////////            backBtn.Location = new Point(20, startY);
//////////            startY = backBtn.Bottom + Margin;

//////////            // ComboBoxes and Submit button
//////////            int comboX = (this.ClientSize.Width - (3 * 200 + 2 * Margin)) / 2;
//////////            branchCombo.Location = new Point(comboX, startY);
//////////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//////////            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//////////            submitBtn.Location = new Point(comboX + 3 * (200 + Margin), startY);
//////////            startY = submitBtn.Bottom + Margin;

//////////            // DataGridView
//////////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//////////            startY = dataGridView.Bottom + Margin;

//////////            // Download button
//////////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//////////        }

//////////        private void PopulateComboBoxes()
//////////        {
//////////            // Branches
//////////            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
//////////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//////////            branchCombo.Items.AddRange(branches.ToArray());
//////////            if (branches.Any()) branchCombo.SelectedIndex = 0;

//////////            // Academic Years (hardcoded for now, adjust based on JSON if available)
//////////            acYearCombo.Items.AddRange(new string[] { "2023-2024", "2024-2025" });
//////////            acYearCombo.SelectedIndex = 0;
//////////        }
//////////        private string GetBranchFromStudentId(string studentId)
//////////        {
//////////            studentId = studentId.ToUpper();
//////////            if (studentId.Contains("5A66") || studentId.Contains("1A66")) return "CSM";
//////////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
//////////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
//////////            return "Unknown";
//////////        }


//////////        private void backbtn_Click(object sender, EventArgs e)
//////////        {
//////////            this.Hide();
//////////        }

//////////        private void label5_Click(object sender, EventArgs e)
//////////        {

//////////        }

//////////        private void submitbtn_Click(object sender, EventArgs e)
//////////        {
//////////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
//////////            {
//////////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////////                return;
//////////            }

//////////            string branch = branchCombo.SelectedItem.ToString();
//////////            string acYear = acYearCombo.SelectedItem.ToString();
//////////            string option = optionsCombo.SelectedItem.ToString();

//////////            LoadResults(branch, acYear, option);
//////////            PopulateDataGridView(option);
//////////        }

//////////        private void LoadResults(string branch, string acYear, string option)
//////////        {
//////////            var marks = LoadJsonData<StudentM>("studentMarks.json");
//////////            var subjects = LoadJsonData<Subject>("subjectCodes.json");
//////////            var names = LoadJsonData<StudentName>("studentnames.json");

//////////            // Filter by branch
//////////            var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

//////////            // Map student names
//////////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//////////            // Join with subjects to get Year-Sem
//////////            var studentData = filteredMarks
//////////                .Join(subjects,
//////////                    m => m.SubjectCode,
//////////                    s => s.SubjectCode,
//////////                    (m, s) => new
//////////                    {
//////////                        m.StudentId,
//////////                        m.SubjectCode,
//////////                        m.SubjectName,
//////////                        m.Grade,
//////////                        m.Credits,
//////////                        m.GradePoints,
//////////                        YearSem = $"{s.Year}-{s.Semester}"
//////////                    })
//////////                .Where(d => d.Grade != "F" && d.Grade != "Ab") // Only passed subjects
//////////                .ToList();

//////////            // Group by StudentId
//////////            var groupedByStudent = studentData
//////////                .GroupBy(d => d.StudentId.ToUpper())
//////////                .Select(g => new
//////////                {
//////////                    StudentId = g.Key,
//////////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//////////                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
//////////                        s => s.Key,
//////////                        s => s.Select(x => new { x.Credits, x.GradePoints }).ToList())
//////////                })
//////////                .ToList();

//////////            results = new List<StudentResult>();
//////////            foreach (var student in groupedByStudent)
//////////            {
//////////                var result = new StudentResult
//////////                {
//////////                    StudentId = student.StudentId,
//////////                    StudentName = student.StudentName,
//////////                    Semesters = new Dictionary<string, string>
//////////                    {
//////////                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
//////////                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
//////////                    }
//////////                };

//////////                double totalGradePoints = 0;
//////////                double totalCredits = 0;

//////////                foreach (var semester in student.Semesters)
//////////                {
//////////                    double semGradePoints = 0;
//////////                    double semCredits = 0;

//////////                    foreach (var subject in semester.Value)
//////////                    {
//////////                        double credits = double.TryParse(subject.Credits, out var c) ? c : 0;
//////////                        double gradePoints = double.TryParse(subject.GradePoints, out var gp) ? gp : 0;
//////////                        semGradePoints += gradePoints * credits;
//////////                        semCredits += credits;
//////////                    }

//////////                    if (option == "GPA")
//////////                    {
//////////                        if (semCredits > 0)
//////////                        {
//////////                            double sgpa = semGradePoints / semCredits;
//////////                            result.Semesters[semester.Key] = sgpa.ToString("F2");
//////////                            totalGradePoints += semGradePoints;
//////////                            totalCredits += semCredits;
//////////                        }
//////////                    }
//////////                    else // Credits
//////////                    {
//////////                        result.Semesters[semester.Key] = semCredits.ToString("F0");
//////////                        totalCredits += semCredits;
//////////                    }
//////////                }

//////////                if (option == "GPA" && totalCredits > 0)
//////////                {
//////////                    result.CGPA = (totalGradePoints / totalCredits).ToString("F2");
//////////                }
//////////                else if (option == "GPA")
//////////                {
//////////                    result.CGPA = "-";
//////////                }
//////////                else // Credits
//////////                {
//////////                    result.TotalCredits = totalCredits.ToString("F0");
//////////                }

//////////                results.Add(result);
//////////            }
//////////        }



//////////        private void PopulateDataGridView(string option)
//////////{
//////////    dataGridView.Columns.Clear();
//////////    dataGridView.ColumnCount = option == "GPA" ? 10 : 10;

//////////    // Define columns
//////////    dataGridView.Columns[0].Name = "Student ID";
//////////    dataGridView.Columns[1].Name = "Student Name";
//////////    dataGridView.Columns[2].Name = "1-1";
//////////    dataGridView.Columns[3].Name = "1-2";
//////////    dataGridView.Columns[4].Name = "2-1";
//////////    dataGridView.Columns[5].Name = "2-2";
//////////    dataGridView.Columns[6].Name = "3-1";
//////////    dataGridView.Columns[7].Name = "3-2";
//////////    dataGridView.Columns[8].Name = "4-1";
//////////    dataGridView.Columns[9].Name = "4-2";
//////////    if (option == "GPA")
//////////    {
//////////        dataGridView.Columns.Add("CGPA", "CGPA");
//////////    }
//////////    else
//////////    {
//////////        dataGridView.Columns.Add("Total Credits", "Total Credits");
//////////    }

//////////    // Set column widths
//////////    dataGridView.Columns[0].Width = 150;
//////////    dataGridView.Columns[1].Width = 200;
//////////    for (int i = 2; i <= 9; i++)
//////////    {
//////////        dataGridView.Columns[i].Width = 100;
//////////    }
//////////    dataGridView.Columns[10].Width = 100;

//////////    // Populate rows
//////////    dataGridView.Rows.Clear();
//////////    foreach (var result in results.OrderBy(r => r.StudentId))
//////////    {
//////////        var row = new List<object> { result.StudentId, result.StudentName };
//////////        row.AddRange(new[] {
//////////                    result.Semesters["1-1"], result.Semesters["1-2"],
//////////                    result.Semesters["2-1"], result.Semesters["2-2"],
//////////                    result.Semesters["3-1"], result.Semesters["3-2"],
//////////                    result.Semesters["4-1"], result.Semesters["4-2"]
//////////                });
//////////        row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//////////        dataGridView.Rows.Add(row.ToArray());
//////////    }

//////////    if (dataGridView.Rows.Count == 0)
//////////    {
//////////        dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
//////////    }

//////////    dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//////////}



//////////        private int CalculateDataGridViewHeight(int rowCount)
//////////        {
//////////            int rowHeight = 50;
//////////            int headerHeight = 40;
//////////            int totalHeight = headerHeight + rowCount * rowHeight;
//////////            return totalHeight < 90 ? 90 : totalHeight;
//////////        }

//////////        private void downloadBtn_Click(object sender, EventArgs e)
//////////        {
//////////            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//////////            {
//////////                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////////                return;
//////////            }

//////////            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//////////            using (var package = new ExcelPackage())
//////////            {
//////////                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//////////                // Report Info
//////////                worksheet.Cells[1, 1].Value = "Branch";
//////////                worksheet.Cells[1, 2].Value = branchCombo.SelectedItem?.ToString() ?? "Unknown";
//////////                worksheet.Cells[2, 1].Value = "Academic Year";
//////////                worksheet.Cells[2, 2].Value = acYearCombo.SelectedItem?.ToString() ?? "Unknown";
//////////                worksheet.Cells[3, 1].Value = "Report Type";
//////////                worksheet.Cells[3, 2].Value = optionsCombo.SelectedItem?.ToString() ?? "Unknown";

//////////                // Headers
//////////                int startRow = 5;
//////////                for (int i = 0; i < dataGridView.Columns.Count; i++)
//////////                {
//////////                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
//////////                }

//////////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
//////////                {
//////////                    headerRange.Style.Font.Bold = true;
//////////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//////////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//////////                    headerRange.Style.Font.Color.SetColor(Color.White);
//////////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//////////                }

//////////                // Data
//////////                for (int i = 0; i < dataGridView.Rows.Count; i++)
//////////                {
//////////                    for (int j = 0; j < dataGridView.Columns.Count; j++)
//////////                    {
//////////                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
//////////                    }
//////////                }

//////////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//////////                var saveFileDialog = new SaveFileDialog
//////////                {
//////////                    Filter = "Excel files (*.xlsx)|*.xlsx",
//////////                    Title = "Save Results Report"
//////////                };

//////////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//////////                {
//////////                    var file = new FileInfo(saveFileDialog.FileName);
//////////                    package.SaveAs(file);
//////////                    MessageBox.Show("Excel report saved successfully!");
//////////                }
//////////            }
//////////        }

//////////        private List<T> LoadJsonData<T>(string filePath)
//////////        {
//////////            if (!File.Exists(filePath))
//////////            {
//////////                MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////////                return new List<T>();
//////////            }

//////////            try
//////////            {
//////////                var json = File.ReadAllText(filePath);
//////////                var data = JsonConvert.DeserializeObject<List<T>>(json);
//////////                return data ?? new List<T>();
//////////            }
//////////            catch (Exception ex)
//////////            {
//////////                MessageBox.Show($"Error loading {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////////                return new List<T>();
//////////            }
//////////        }




//////////    }


//////////    public class StudentM
//////////    {
//////////        public string StudentId { get; set; }
//////////        public string SubjectCode { get; set; }
//////////        public string SubjectName { get; set; }
//////////        public string Internal { get; set; }
//////////        public string External { get; set; }
//////////        public string Total { get; set; }
//////////        public string Grade { get; set; }
//////////        public string Credits { get; set; }
//////////        public string GradePoints { get; set; }
//////////    }

//////////    public class StudentName
//////////    {
//////////        public string studentid { get; set; }
//////////        public string name { get; set; }
//////////    }

//////////    public class StudentResult
//////////    {
//////////        public string StudentId { get; set; }
//////////        public string StudentName { get; set; }
//////////        public Dictionary<string, string> Semesters { get; set; }
//////////        public string CGPA { get; set; }
//////////        public string TotalCredits { get; set; }
//////////    }
//////////}


////////using Newtonsoft.Json;
////////using OfficeOpenXml;
////////using System;
////////using System.Collections.Generic;
////////using System.Drawing;
////////using System.IO;
////////using System.Linq;
////////using System.Windows.Forms;

////////namespace project_RYS.Forms
////////{
////////    public partial class getbtn : Form
////////    {
////////        private ComboBox branchCombo;
////////        private ComboBox acYearCombo;
////////        private ComboBox optionsCombo;
////////        private Button backBtn;
////////        private Button submitBtn;
////////        private Label headingLabel;
////////        private const int Margin = 20;
////////        private List<StudentResult> results;

////////        public getbtn()
////////        {
////////            InitializeComponent();
////////            SetupForm();
////////            PopulateComboBoxes();
////////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////////        }

////////        private void SetupForm()
////////        {
////////            this.Size = new Size(1300, 800);
////////            this.AutoScroll = true;
////////            this.BackColor = Color.FromArgb(240, 240, 240);

////////            // Heading Label
////////            headingLabel = new Label
////////            {
////////                Text = "Student Results Report",
////////                Font = new Font("Arial", 16, FontStyle.Bold),
////////                AutoSize = true,
////////                ForeColor = Color.DarkOrchid
////////            };
////////            this.Controls.Add(headingLabel);

////////            // Back Button
////////            backBtn = new Button
////////            {
////////                Text = "Back To Home",
////////                Width = 200,
////////                Height = 40,
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkViolet,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 10, FontStyle.Bold)
////////            };
////////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
////////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
////////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
////////            backBtn.Click += new EventHandler(backbtn_Click);
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

////////            // Options ComboBox
////////            optionsCombo = new ComboBox
////////            {
////////                Width = 200,
////////                Height = 30,
////////                Font = new Font("Arial", 10),
////////                DropDownStyle = ComboBoxStyle.DropDownList
////////            };
////////            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
////////            optionsCombo.SelectedIndex = 0; // Default to GPA
////////            this.Controls.Add(optionsCombo);

////////            // Submit Button
////////            submitBtn = new Button
////////            {
////////                Text = "Submit",
////////                Width = 200,
////////                Height = 40,
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkOrchid,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 10, FontStyle.Bold)
////////            };
////////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
////////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
////////            submitBtn.Click += new EventHandler(submitBtn_Click);
////////            this.Controls.Add(submitBtn);

////////            ArrangeControls();
////////        }

////////        private void ArrangeControls()
////////        {
////////            int startY = 20;

////////            // Center heading
////////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////////            startY = headingLabel.Bottom + Margin;

////////            // Back button
////////            backBtn.Location = new Point(20, startY);
////////            startY = backBtn.Bottom + Margin;

////////            // ComboBoxes and Submit button
////////            int comboX = (this.ClientSize.Width - (3 * 200 + 2 * Margin)) / 2;
////////            branchCombo.Location = new Point(comboX, startY);
////////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
////////            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
////////            submitBtn.Location = new Point(comboX + 1 * (200 + Margin), 00);
////////        }

////////        //private void PopulateComboBoxes()
////////        //{
////////        //    // Branches
////////        //    var marks = LoadJsonData<StudentMarks>("studentMarks.json");
////////        //    var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
////////        //    branchCombo.Items.AddRange(branches.ToArray());
////////        //    if (branches.Any()) branchCombo.SelectedIndex = 0;

////////        //    // Academic Years (hardcoded for now, adjust based on JSON if available)
////////        //    acYearCombo.Items.AddRange(new string[] { "2021-2025", "2022-2026" });
////////        //    acYearCombo.SelectedIndex = 0;
////////        //}
////////        private void PopulateComboBoxes()
////////        {
////////            // Branches
////////            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
////////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
////////            branchCombo.Items.AddRange(branches.ToArray());
////////            if (branches.Any()) branchCombo.SelectedIndex = 0;

////////            // Academic Years
////////            acYearCombo.Items.AddRange(new string[] { "2021-2025", "2022-2026" });
////////            acYearCombo.SelectedIndex = 0;
////////        }


////////        private string GetBranchFromStudentId(string studentId)
////////        {
////////            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
////////            studentId = studentId.ToUpper().Trim();
////////            if (studentId.Contains("5a66") || studentId.Contains("5A66"))
////////                return "CSM";
////////            else if (studentId.Contains("1a05") || studentId.Contains("1A05"))
////////                return "CSE";
////////            else if (studentId.Contains("5a05") || studentId.Contains("5A05"))
////////                return "CSE";

////////            else if (studentId.Contains("1a66") || studentId.Contains("1A66"))
////////                return "CSM";
////////            else if (studentId.Contains("1a01") || studentId.Contains("1A01"))
////////                return "CE";
////////            else if (studentId.Contains("5A01") || studentId.Contains("5a01"))
////////                return "CE";
////////            // Add more conditions here for other patterns as needed
////////            else
////////                return "Unknown"; // Default case if no pattern matches
////////        }

////////        //private void submitBtn_Click(object sender, EventArgs e)
////////        //{
////////        //    if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
////////        //    {
////////        //        MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////        //        return;
////////        //    }

////////        //    string branch = branchCombo.SelectedItem.ToString();
////////        //    string acYear = acYearCombo.SelectedItem.ToString();
////////        //    string option = optionsCombo.SelectedItem.ToString();

////////        //    LoadResults(branch, acYear, option);
////////        //    ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
////////        //    resultsForm.Show();
////////        //    this.Hide();
////////        //}
////////        private void submitBtn_Click(object sender, EventArgs e)
////////        {
////////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
////////            {
////////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////                return;
////////            }

////////            string branch = branchCombo.SelectedItem.ToString();
////////            string acYear = acYearCombo.SelectedItem.ToString();
////////            string option = optionsCombo.SelectedItem.ToString();

////////            LoadResults(branch, acYear, option);
////////            ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
////////            resultsForm.Show();
////////            this.Hide();
////////        }

////////        //private void LoadResults(string branch, string acYear, string option)
////////        //{
////////        //    var marks = LoadJsonData<StudentM>("studentMarks.json");
////////        //    var subjects = LoadJsonData<Subject>("subjectCodes.json");
////////        //    var names = LoadJsonData<StudentName>("studentnames.json");

////////        //    // Filter by branch
////////        //    var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

////////        //    // Map student names
////////        //    var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

////////        //    // Join with subjects to get Year-Sem
////////        //    var studentData = filteredMarks
////////        //        .Join(subjects,
////////        //            m => m.SubjectCode,
////////        //            s => s.SubjectCode,
////////        //            (m, s) => new
////////        //            {
////////        //                m.StudentId,
////////        //                m.SubjectCode,
////////        //                m.SubjectName,
////////        //                m.Grade,
////////        //                m.Credits,
////////        //                m.GradePoints,
////////        //                YearSem = $"{s.Year}-{s.Semester}"
////////        //            })
////////        //        .Where(d => d.Grade != "F" && d.Grade != "Ab") // Only passed subjects
////////        //        .ToList();

////////        //    // Group by StudentId
////////        //    var groupedByStudent = studentData
////////        //        .GroupBy(d => d.StudentId.ToUpper())
////////        //        .Select(g => new
////////        //        {
////////        //            StudentId = g.Key,
////////        //            StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
////////        //            Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
////////        //                s => s.Key,
////////        //                s => s.Select(x => new { x.Credits, x.GradePoints }).ToList())
////////        //        })
////////        //        .ToList();

////////        //    results = new List<StudentResult>();
////////        //    foreach (var student in groupedByStudent)
////////        //    {
////////        //        var result = new StudentResult
////////        //        {
////////        //            StudentId = student.StudentId,
////////        //            StudentName = student.StudentName,
////////        //            Semesters = new Dictionary<string, string>
////////        //            {
////////        //                { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
////////        //                { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
////////        //            }
////////        //        };

////////        //        double totalGradePoints = 0;
////////        //        double totalCredits = 0;

////////        //        foreach (var semester in student.Semesters)
////////        //        {
////////        //            double semGradePoints = 0;
////////        //            double semCredits = 0;

////////        //            foreach (var subject in semester.Value)
////////        //            {
////////        //                double credits = double.TryParse(subject.Credits, out var c) ? c : 0;
////////        //                double gradePoints = double.TryParse(subject.GradePoints, out var gp) ? gp : 0;
////////        //                semGradePoints += gradePoints * credits;
////////        //                semCredits += credits;
////////        //            }

////////        //            if (option == "GPA")
////////        //            {
////////        //                if (semCredits > 0)
////////        //                {
////////        //                    double sgpa = semGradePoints / semCredits;
////////        //                    result.Semesters[semester.Key] = sgpa.ToString("F2");
////////        //                    totalGradePoints += semGradePoints;
////////        //                    totalCredits += semCredits;
////////        //                }
////////        //            }
////////        //            else // Credits
////////        //            {
////////        //                result.Semesters[semester.Key] = semCredits.ToString("F0");
////////        //                totalCredits += semCredits;
////////        //            }
////////        //        }

////////        //        if (option == "GPA" && totalCredits > 0)
////////        //        {
////////        //            result.CGPA = (totalGradePoints / totalCredits).ToString("F2");
////////        //        }
////////        //        else if (option == "GPA")
////////        //        {
////////        //            result.CGPA = "-";
////////        //        }
////////        //        else // Credits
////////        //        {
////////        //            result.TotalCredits = totalCredits.ToString("F0");
////////        //        }

////////        //        results.Add(result);
////////        //    }
////////        //}

////////        private void LoadResults(string branch, string acYear, string option)
////////        {
////////            var marks = LoadJsonData<StudentM>("studentMarks.json");
////////            var subjects = LoadJsonData<Subjec>("subjectCodes.json");
////////            var names = LoadJsonData<StudentName>("studentnames.json");

////////            // Filter by branch
////////            var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

////////            // Map student names
////////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

////////            // Join with subjects to get Year-Sem and Credits (from subjectCodes.json)
////////            var studentData = filteredMarks
////////                .Join(subjects,
////////                    m => m.SubjectCode,
////////                    s => s.SubjectCode,
////////                    (m, s) => new
////////                    {
////////                        m.StudentId,
////////                        m.SubjectCode,
////////                        m.SubjectName,
////////                        m.Grade,
////////                        CreditsFromMarks = m.Credits, // For Credits option
////////                        CreditsFromSubjects = s.Credits, // For GPA option
////////                        m.GradePoints,
////////                        YearSem = $"{s.Year}-{s.Semester}"
////////                    })
////////                .Where(d => d.Grade != "F" && d.Grade != "Ab") // Only passed subjects
////////                .ToList();

////////            // Group by StudentId
////////            var groupedByStudent = studentData
////////                .GroupBy(d => d.StudentId.ToUpper())
////////                .Select(g => new
////////                {
////////                    StudentId = g.Key,
////////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
////////                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
////////                        s => s.Key,
////////                        s => s.Select(x => new { x.CreditsFromMarks, x.CreditsFromSubjects, x.GradePoints }).ToList())
////////                })
////////                .ToList();

////////            results = new List<StudentResult>();
////////            foreach (var student in groupedByStudent)
////////            {
////////                var result = new StudentResult
////////                {
////////                    StudentId = student.StudentId,
////////                    StudentName = student.StudentName,
////////                    Semesters = new Dictionary<string, string>
////////                    {
////////                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
////////                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
////////                    }
////////                };

////////                double totalGradePoints = 0;
////////                double totalCredits = 0;

////////                foreach (var semester in student.Semesters)
////////                {
////////                    double semGradePoints = 0;
////////                    double semCredits = 0;

////////                    foreach (var subject in semester.Value)
////////                    {
////////                        double creditsForGPA = double.TryParse(subject.CreditsFromSubjects, out var c1) ? c1 : 0;
////////                        double creditsForCredits = double.TryParse(subject.CreditsFromMarks, out var c2) ? c2 : 0;
////////                        double gradePoints = double.TryParse(subject.GradePoints, out var gp) ? gp : 0;

////////                        if (option == "GPA")
////////                        {
////////                            semGradePoints += gradePoints * creditsForGPA;
////////                            semCredits += creditsForGPA;
////////                        }
////////                        else // Credits
////////                        {
////////                            semCredits += creditsForCredits;
////////                        }
////////                    }

////////                    if (option == "GPA")
////////                    {
////////                        if (semCredits > 0)
////////                        {
////////                            double sgpa = semGradePoints / semCredits;
////////                            result.Semesters[semester.Key] = sgpa.ToString("F2");
////////                            totalGradePoints += semGradePoints;
////////                            totalCredits += semCredits;
////////                        }
////////                    }
////////                    else // Credits
////////                    {
////////                        result.Semesters[semester.Key] = semCredits.ToString("F0");
////////                        totalCredits += semCredits;
////////                    }
////////                }

////////                if (option == "GPA" && totalCredits > 0)
////////                {
////////                    result.CGPA = (totalGradePoints / totalCredits).ToString("F2");
////////                }
////////                else if (option == "GPA")
////////                {
////////                    result.CGPA = "-";
////////                }
////////                else // Credits
////////                {
////////                    result.TotalCredits = totalCredits.ToString("F0");
////////                }

////////                results.Add(result);
////////            }
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

////////        private void backbtn_Click(object sender, EventArgs e)
////////        {
////////            this.Hide();
////////            admindashboard adb = new admindashboard();
////////            adb.Show();
////////        }
////////    }


////////    public class StudentM
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

////////    public class Subjec
////////    {
////////        public string SubjectCode { get; set; }
////////        public string Year { get; set; }
////////        public string Semester { get; set; }
////////        public string Credits { get; set; }
////////    }

////////    public class StudentName
////////    {
////////        public string studentid { get; set; }
////////        public string name { get; set; }
////////    }

////////    public class StudentResult
////////    {
////////        public string StudentId { get; set; }
////////        public string StudentName { get; set; }
////////        public Dictionary<string, string> Semesters { get; set; }
////////        public string CGPA { get; set; }
////////        public string TotalCredits { get; set; }
////////    }
////////}

//////using Newtonsoft.Json;
//////using OfficeOpenXml;
//////using System;
//////using System.Collections.Generic;
//////using System.Drawing;
//////using System.IO;
//////using System.Linq;
//////using System.Windows.Forms;

//////namespace project_RYS.Forms
//////{
//////    public partial class getbtn : Form
//////    {
//////        private ComboBox branchCombo;
//////        private ComboBox acYearCombo;
//////        private ComboBox optionsCombo;
//////        private Button backBtn;
//////        private Button submitBtn;
//////        private Label headingLabel;
//////        private const int Margin = 20;
//////        private List<StudentResult> results;

//////        public getbtn()
//////        {
//////            InitializeComponent();
//////            SetupForm();
//////            PopulateComboBoxes();
//////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//////        }

//////        private void SetupForm()
//////        {
//////            this.Size = new Size(1300, 800);
//////            this.AutoScroll = true;
//////            this.BackColor = Color.FromArgb(240, 240, 240);

//////            // Heading Label
//////            headingLabel = new Label
//////            {
//////                Text = "Student Results Report",
//////                Font = new Font("Arial", 16, FontStyle.Bold),
//////                AutoSize = true,
//////                ForeColor = Color.DarkOrchid
//////            };
//////            this.Controls.Add(headingLabel);

//////            // Back Button
//////            backBtn = new Button
//////            {
//////                Text = "Back To Home",
//////                Width = 200,
//////                Height = 40,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkViolet,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 10, FontStyle.Bold)
//////            };
//////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//////            backBtn.Click += new EventHandler(backbtn_Click);
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

//////            // Options ComboBox
//////            optionsCombo = new ComboBox
//////            {
//////                Width = 200,
//////                Height = 30,
//////                Font = new Font("Arial", 10),
//////                DropDownStyle = ComboBoxStyle.DropDownList
//////            };
//////            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
//////            optionsCombo.SelectedIndex = 0; // Default to GPA
//////            this.Controls.Add(optionsCombo);

//////            // Submit Button
//////            submitBtn = new Button
//////            {
//////                Text = "Submit",
//////                Width = 200,
//////                Height = 40,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkOrchid,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 10, FontStyle.Bold)
//////            };
//////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//////            submitBtn.Click += new EventHandler(submitBtn_Click);
//////            this.Controls.Add(submitBtn);

//////            ArrangeControls();
//////        }

//////        private void ArrangeControls()
//////        {
//////            int startY = 20;

//////            // Center heading
//////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//////            startY = headingLabel.Bottom + Margin;

//////            // Back button
//////            backBtn.Location = new Point(20, startY);
//////            startY = backBtn.Bottom + Margin;

//////            // ComboBoxes and Submit button
//////            int comboX = (this.ClientSize.Width - (3 * 200 + 2 * Margin)) / 2;
//////            branchCombo.Location = new Point(comboX, startY);
//////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//////            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//////            submitBtn.Location = new Point(comboX + 3 * (200 + Margin), startY);
//////        }

//////        private void PopulateComboBoxes()
//////        {
//////            // Branches
//////            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
//////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//////            branchCombo.Items.AddRange(branches.ToArray());
//////            if (branches.Any()) branchCombo.SelectedIndex = 0;

//////            // Academic Years
//////            acYearCombo.Items.AddRange(new string[] { "2021-2025", "2022-2026" });
//////            acYearCombo.SelectedIndex = 0;
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
//////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
//////            {
//////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////                return;
//////            }

//////            string branch = branchCombo.SelectedItem.ToString();
//////            string acYear = acYearCombo.SelectedItem.ToString();
//////            string option = optionsCombo.SelectedItem.ToString();

//////            LoadResults(branch, acYear, option);
//////            ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
//////            resultsForm.Show();
//////            this.Hide();
//////        }

//////        private void LoadResults(string branch, string acYear, string option)
//////        {
//////            var marks = LoadJsonData<StudentM>("studentMarks.json");
//////            var subjects = LoadJsonData<Subjec>("subjectCodes.json");
//////            var names = LoadJsonData<StudentName>("studentnames.json");

//////            // Filter by branch
//////            var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

//////            // Map student names
//////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//////            // Join with subjects to get Year-Sem and Credits
//////            var studentData = filteredMarks
//////                .Join(subjects,
//////                    m => m.SubjectCode,
//////                    s => s.SubjectCode,
//////                    (m, s) => new
//////                    {
//////                        m.StudentId,
//////                        m.SubjectCode,
//////                        m.SubjectName,
//////                        m.Grade,
//////                        CreditsFromMarks = m.Credits, // For Credits option
//////                        CreditsFromSubjects = s.Credits, // For GPA option
//////                        m.GradePoints,
//////                        YearSem = $"{s.Year}-{s.Semester}"
//////                    })
//////                .Where(d => d.Grade != "F" && d.Grade != "Ab") // Only passed subjects
//////                .ToList();

//////            // Group by StudentId
//////            var groupedByStudent = studentData
//////                .GroupBy(d => d.StudentId.ToUpper())
//////                .Select(g => new
//////                {
//////                    StudentId = g.Key,
//////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//////                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
//////                        s => s.Key,
//////                        s => s.Select(x => new { x.CreditsFromMarks, x.CreditsFromSubjects, x.GradePoints }).ToList())
//////                })
//////                .ToList();

//////            results = new List<StudentResult>();
//////            foreach (var student in groupedByStudent)
//////            {
//////                var result = new StudentResult
//////                {
//////                    StudentId = student.StudentId,
//////                    StudentName = student.StudentName,
//////                    Semesters = new Dictionary<string, string>
//////                    {
//////                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
//////                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
//////                    }
//////                };

//////                double totalSgpa = 0;
//////                int semesterCount = 0;
//////                double totalCredits = 0;

//////                foreach (var semester in student.Semesters)
//////                {
//////                    double semGradePoints = 0;
//////                    double semCredits = 0;

//////                    foreach (var subject in semester.Value)
//////                    {
//////                        double creditsForGPA = double.TryParse(subject.CreditsFromSubjects, out var c1) ? c1 : 0;
//////                        double creditsForCredits = double.TryParse(subject.CreditsFromMarks, out var c2) ? c2 : 0;
//////                        double gradePoints = double.TryParse(subject.GradePoints, out var gp) ? gp : 0;

//////                        if (option == "GPA")
//////                        {
//////                            semGradePoints += gradePoints * creditsForGPA;
//////                            semCredits += creditsForGPA;
//////                        }
//////                        else // Credits
//////                        {
//////                            semCredits += creditsForCredits;
//////                        }
//////                    }

//////                    if (option == "GPA")
//////                    {
//////                        if (semCredits > 0)
//////                        {
//////                            double sgpa = semGradePoints / semCredits;
//////                            result.Semesters[semester.Key] = sgpa.ToString("F2");
//////                            totalSgpa += sgpa;
//////                            semesterCount++;
//////                        }
//////                    }
//////                    else // Credits
//////                    {
//////                        result.Semesters[semester.Key] = semCredits.ToString("F1");
//////                        totalCredits += semCredits;
//////                    }
//////                }

//////                if (option == "GPA")
//////                {
//////                    if (semesterCount > 0)
//////                    {
//////                        result.CGPA = (totalSgpa / semesterCount).ToString("F2");
//////                    }
//////                    else
//////                    {
//////                        result.CGPA = "-";
//////                    }
//////                }
//////                else // Credits
//////                {
//////                    result.TotalCredits = totalCredits.ToString("F1");
//////                }

//////                results.Add(result);
//////            }
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

//////        private void backbtn_Click(object sender, EventArgs e)
//////        {
//////            this.Hide();
//////            //admindashboard adb = new admindashboard();
//////            //adb.Show();
//////        }
//////    }

//////    public class StudentM
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

//////    public class Subjec
//////    {
//////        public string SubjectCode { get; set; }
//////        public string Year { get; set; }
//////        public string Semester { get; set; }
//////        public string Credits { get; set; }
//////    }

//////    public class StudentName
//////    {
//////        public string studentid { get; set; }
//////        public string name { get; set; }
//////    }

//////    public class StudentResult
//////    {
//////        public string StudentId { get; set; }
//////        public string StudentName { get; set; }
//////        public Dictionary<string, string> Semesters { get; set; }
//////        public string CGPA { get; set; }
//////        public string TotalCredits { get; set; }
//////    }
//////}


////using Newtonsoft.Json;
////using OfficeOpenXml;
////using System;
////using System.Collections.Generic;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Windows.Forms;

////namespace project_RYS.Forms
////{
////    public partial class getbtn : Form
////    {
////        private ComboBox branchCombo;
////        private ComboBox acYearCombo;
////        private ComboBox optionsCombo;
////        private Button backBtn;
////        private Button submitBtn;
////        private Label headingLabel;
////        private const int Margin = 20;
////        private List<StudentResult> results;

////        public getbtn()
////        {
////            InitializeComponent();
////            SetupForm();
////            PopulateComboBoxes();
////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////        }

////        private void SetupForm()
////        {
////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.BackColor = Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Student Results Report",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backBtn = new Button
////            {
////                Text = "Back To Home",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkViolet,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
////            backBtn.Click += new EventHandler(backbtn_Click);
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

////            // Options ComboBox
////            optionsCombo = new ComboBox
////            {
////                Width = 200,
////                Height = 30,
////                Font = new Font("Arial", 10),
////                DropDownStyle = ComboBoxStyle.DropDownList
////            };
////            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
////            optionsCombo.SelectedIndex = 0; // Default to GPA
////            this.Controls.Add(optionsCombo);

////            // Submit Button
////            submitBtn = new Button
////            {
////                Text = "Submit",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
////            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
////            submitBtn.Click += new EventHandler(submitBtn_Click);
////            this.Controls.Add(submitBtn);

////            ArrangeControls();
////        }

////        private void ArrangeControls()
////        {
////            int startY = 20;

////            // Center heading
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            startY = headingLabel.Bottom + Margin;

////            // Back button
////            backBtn.Location = new Point(20, startY);
////            startY = backBtn.Bottom + Margin;

////            // ComboBoxes and Submit button
////            int comboX = (this.ClientSize.Width - (3 * 200 + 2 * Margin)) / 2;
////            branchCombo.Location = new Point(comboX, startY);
////            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
////            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
////            submitBtn.Location = new Point(comboX + 3 * (200 + Margin), startY);
////        }

////        private void PopulateComboBoxes()
////        {
////            // Branches
////            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
////            branchCombo.Items.AddRange(branches.ToArray());
////            if (branches.Any()) branchCombo.SelectedIndex = 0;

////            // Academic Years
////            acYearCombo.Items.AddRange(new string[] { "2021-2025", "2022-2026" });
////            acYearCombo.SelectedIndex = 0;
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
////            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
////            {
////                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return;
////            }

////            string branch = branchCombo.SelectedItem.ToString();
////            string acYear = acYearCombo.SelectedItem.ToString();
////            string option = optionsCombo.SelectedItem.ToString();

////            LoadResults(branch, acYear, option);
////            ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
////            resultsForm.Show();
////            this.Hide();
////        }

////        private void LoadResults(string branch, string acYear, string option)
////        {
////            var marks = LoadJsonData<StudentM>("studentMarks.json");
////            var subjects = LoadJsonData<Subjec>("subjectCodes.json");
////            var names = LoadJsonData<StudentName>("studentnames.json");

////            // Filter by branch
////            var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

////            // Map student names
////            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

////            // Create subject credits dictionary for GPA
////            var subjectCreditsDict = subjects.ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);

////            // Join with subjects to get Year-Sem and Credits
////            var studentData = filteredMarks
////                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
////                .Select(m => new
////                {
////                    m.StudentId,
////                    m.SubjectCode,
////                    m.SubjectName,
////                    m.Grade,
////                    CreditsFromMarks = double.TryParse(m.Credits, out var c) ? c : 0, // For Credits option
////                    CreditsFromSubjects = subjectCreditsDict[m.SubjectCode.ToUpper()], // For GPA option
////                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0,
////                    YearSem = subjects.FirstOrDefault(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()) != null
////                        ? $"{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Year}-{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Semester}"
////                        : null
////                })
////                .Where(d => d.YearSem != null && d.Grade != "F" && d.Grade != "Ab") // Only passed subjects with valid Year-Sem
////                .ToList();

////            // Group by StudentId
////            var groupedByStudent = studentData
////                .GroupBy(d => d.StudentId.ToUpper())
////                .Select(g => new
////                {
////                    StudentId = g.Key,
////                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
////                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
////                        s => s.Key,
////                        s => s.Select(x => new { x.CreditsFromMarks, x.CreditsFromSubjects, x.GradePoints }).ToList())
////                })
////                .ToList();

////            results = new List<StudentResult>();
////            foreach (var student in groupedByStudent)
////            {
////                var result = new StudentResult
////                {
////                    StudentId = student.StudentId,
////                    StudentName = student.StudentName,
////                    Semesters = new Dictionary<string, string>
////                    {
////                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
////                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
////                    }
////                };

////                double totalSgpa = 0;
////                int semesterCount = 0;
////                double totalCredits = 0;

////                foreach (var semester in student.Semesters)
////                {
////                    double semGradePoints = 0;
////                    double semCredits = 0;

////                    foreach (var subject in semester.Value)
////                    {
////                        if (option == "GPA")
////                        {
////                            semGradePoints += subject.GradePoints * subject.CreditsFromSubjects;
////                            semCredits += subject.CreditsFromSubjects;
////                        }
////                        else // Credits
////                        {
////                            semCredits += subject.CreditsFromMarks;
////                        }
////                    }

////                    if (option == "GPA")
////                    {
////                        if (semCredits > 0)
////                        {
////                            double sgpa = semGradePoints / semCredits;
////                            result.Semesters[semester.Key] = sgpa.ToString("F2");
////                            totalSgpa += sgpa;
////                            semesterCount++;
////                        }
////                    }
////                    else // Credits
////                    {
////                        result.Semesters[semester.Key] = semCredits.ToString("F1");
////                        totalCredits += semCredits;
////                    }
////                }

////                if (option == "GPA")
////                {
////                    if (semesterCount > 0)
////                    {
////                        result.CGPA = (totalSgpa / semesterCount).ToString("F2");
////                    }
////                    else
////                    {
////                        result.CGPA = "-";
////                    }
////                }
////                else // Credits
////                {
////                    result.TotalCredits = totalCredits.ToString("F1");
////                }

////                results.Add(result);
////            }
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

////        private void backbtn_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            admindashboard adb = new admindashboard();
////            adb.Show();
////        }
////    }

////    public class StudentM
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

////    public class Subjec
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////        public string Credits { get; set; }
////    }

////    public class StudentName
////    {
////        public string studentid { get; set; }
////        public string name { get; set; }
////    }

////    public class StudentResult
////    {
////        public string StudentId { get; set; }
////        public string StudentName { get; set; }
////        public Dictionary<string, string> Semesters { get; set; }
////        public string CGPA { get; set; }
////        public string TotalCredits { get; set; }
////    }
////}



//using Newtonsoft.Json;
//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class getbtn : Form
//    {
//        private ComboBox branchCombo;
//        private ComboBox acYearCombo;
//        private ComboBox optionsCombo;
//        private Button backBtn;
//        private Button submitBtn;
//        private Label headingLabel;
//        private const int Margin = 20;
//        private List<StudentResult> results;

//        public getbtn()
//        {
//            InitializeComponent();
//            SetupForm();
//            PopulateComboBoxes();
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1300, 800);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Student Results Report",
//                Font = new Font("Arial", 16, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back To Home",
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

//            // Options ComboBox
//            optionsCombo = new ComboBox
//            {
//                Width = 200,
//                Height = 30,
//                Font = new Font("Arial", 10),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
//            optionsCombo.SelectedIndex = 0; // Default to GPA
//            this.Controls.Add(optionsCombo);

//            // Submit Button
//            submitBtn = new Button
//            {
//                Text = "Submit",
//                Width = 200,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
//            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
//            submitBtn.Click += new EventHandler(submitBtn_Click);
//            this.Controls.Add(submitBtn);

//            ArrangeControls();
//        }

//        private void ArrangeControls()
//        {
//            int startY = 20;

//            // Center heading
//            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//            startY = headingLabel.Bottom + Margin;

//            // Back button
//            backBtn.Location = new Point(20, startY);
//            startY = backBtn.Bottom + Margin;

//            // ComboBoxes and Submit button
//            int comboX = (this.ClientSize.Width - (3 * 200 + 2 * Margin)) / 2;
//            branchCombo.Location = new Point(comboX, startY);
//            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
//            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);
//            submitBtn.Location = new Point(comboX + 3 * (40 + Margin),200);
//        }

//        private void PopulateComboBoxes()
//        {
//            // Branches
//            var marks = LoadJsonData<StudentMarks>("studentMarks.json");
//            var branches = marks.Select(m => GetBranchFromStudentId(m.StudentId)).Distinct().OrderBy(b => b).ToList();
//            branchCombo.Items.AddRange(branches.ToArray());
//            if (branches.Any()) branchCombo.SelectedIndex = 0;

//            // Academic Years
//            acYearCombo.Items.AddRange(new string[] { "2010-2014",
//"2011-2015",
//"2012-2016",
//"2013-2017",
//"2014-2018",
//"2015-2019",
//"2016-2020",
//"2017-2021",
//"2018-2022",
//"2019-2023",
//"2020-2024",
//"2021-2025",
//"2022-2026",
//"2023-2027",
//"2024-2028",
//"2025-2029",
//"2026-2030",
//"2027-2031",
//"2028-2032",
//"2029-2033",
//"2030-2034",
//"2031-2035",
//"2032-2036",
//"2033-2037",
//"2034-2038",
//"2035-2039",
//"2036-2040",
//"2037-2041 "});
//            acYearCombo.SelectedIndex = 0;
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

//        private void submitBtn_Click(object sender, EventArgs e)
//        {
//            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
//            {
//                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            string branch = branchCombo.SelectedItem.ToString();
//            string acYear = acYearCombo.SelectedItem.ToString();
//            string option = optionsCombo.SelectedItem.ToString();

//            LoadResults(branch, acYear, option);
//            ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
//            resultsForm.Show();
//            this.Hide();
//        }

//        private void LoadResults(string branch, string acYear, string option)
//        {
//            var marks = LoadJsonData<StudentM>("studentMarks.json");
//            var subjects = LoadJsonData<Subjec>("subjectCodes.json");
//            var names = LoadJsonData<StudentName>("studentnames.json");

//            // Filter by branch
//            var filteredMarks = marks.Where(m => GetBranchFromStudentId(m.StudentId) == branch).ToList();

//            // Map student names
//            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

//            // Create subject credits dictionary
//            var subjectCreditsDict = subjects.ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);

//            // Join data for SGPA/Credits
//            var studentData = filteredMarks
//                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
//                .Select(m => new
//                {
//                    m.StudentId,
//                    m.SubjectCode,
//                    m.Grade,
//                    CreditsFromMarks = double.TryParse(m.Credits, out var c) ? c : 0,
//                    CreditsFromSubjects = subjectCreditsDict[m.SubjectCode.ToUpper()],
//                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0,
//                    YearSem = subjects.FirstOrDefault(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()) != null
//                        ? $"{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Year}-{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Semester}"
//                        : null
//                })
//                .Where(d => d.YearSem != null)
//                .ToList();

//            // Group by student
//            var groupedByStudent = studentData
//                .GroupBy(d => d.StudentId.ToUpper())
//                .Select(g => new
//                {
//                    StudentId = g.Key,
//                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
//                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
//                        s => s.Key,
//                        s => s.Select(x => new { x.CreditsFromMarks, x.CreditsFromSubjects, x.GradePoints }).ToList())
//                })
//                .ToList();

//            results = new List<StudentResult>();
//            foreach (var student in groupedByStudent)
//            {
//                var result = new StudentResult
//                {
//                    StudentId = student.StudentId,
//                    StudentName = student.StudentName,
//                    Semesters = new Dictionary<string, string>
//                    {
//                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
//                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
//                    }
//                };

//                double totalSgpa = 0;
//                int semesterCount = 0;
//                double totalCredits = 0;

//                foreach (var semester in student.Semesters)
//                {
//                    double semGradePoints = 0;
//                    double semCredits = 0;

//                    foreach (var subject in semester.Value)
//                    {
//                        if (option == "GPA")
//                        {
//                            semGradePoints += subject.GradePoints * subject.CreditsFromSubjects;
//                            semCredits += subject.CreditsFromSubjects;
//                        }
//                        else // Credits
//                        {
//                            semCredits += subject.CreditsFromMarks;
//                        }
//                    }

//                    if (option == "GPA")
//                    {
//                        if (semCredits > 0)
//                        {
//                            double sgpa = semGradePoints / semCredits;
//                            result.Semesters[semester.Key] = sgpa.ToString("F2");
//                            totalSgpa += sgpa;
//                            semesterCount++;
//                        }
//                    }
//                    else // Credits
//                    {
//                        result.Semesters[semester.Key] = semCredits.ToString("F1");
//                        totalCredits += semCredits;
//                    }
//                }

//                if (option == "GPA")
//                {
//                    if (semesterCount > 0)
//                    {
//                        result.CGPA = (totalSgpa / semesterCount).ToString("F2");
//                    }
//                    else
//                    {
//                        result.CGPA = "-";
//                    }
//                }
//                else // Credits
//                {
//                    result.TotalCredits = totalCredits.ToString("F1");
//                }

//                results.Add(result);
//            }
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

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            admindashboard adb = new admindashboard();
//            adb.Show();
//        }
//    }

//    public class StudentM
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

//    public class Subjec
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//        public string Credits { get; set; }
//    }

//    public class StudentName
//    {
//        public string studentid { get; set; }
//        public string name { get; set; }
//    }

//    public class StudentResult
//    {
//        public string StudentId { get; set; }
//        public string StudentName { get; set; }
//        public Dictionary<string, string> Semesters { get; set; }
//        public string CGPA { get; set; }
//        public string TotalCredits { get; set; }
//    }
//}

using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class getbtn : Form
    {
        private ComboBox branchCombo;
        private ComboBox acYearCombo;
        private ComboBox optionsCombo;
        private Button backBtn;
        private Button submitBtn;
        private Label headingLabel;
        private const int Margin = 20;
        private List<StudentResult> results;

        public getbtn()
        {
            InitializeComponent();
            SetupForm();
            PopulateComboBoxes();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void SetupForm()
        {
            this.Size = new Size(1300, 800);
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Heading Label
            headingLabel = new Label
            {
                Text = "Student Results Report",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid
            };
            this.Controls.Add(headingLabel);

            // Back Button
            backBtn = new Button
            {
                Text = "Back To Home",
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


            // Create a Label
            Label branchLabel = new Label();
            branchLabel.Text = "Branch Name";
            branchLabel.Location = new System.Drawing.Point(210, 200);
            branchLabel.AutoSize = true;
            this.Controls.Add(branchLabel);
            branchLabel.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);



            // Create a Label
            Label acyear = new Label();
            acyear.Text = " Academic Year";
            acyear.Location = new System.Drawing.Point(430, 200);
            acyear.AutoSize = true;
            this.Controls.Add(acyear);
            acyear.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);


            // Create a Label
            Label year = new Label();
            year.Text = "  Select your Option";
            year.Location = new System.Drawing.Point(645, 200);
            year.AutoSize = true;
            this.Controls.Add(year);
            year.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);


           
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

            // Options ComboBox
            optionsCombo = new ComboBox
            {
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            optionsCombo.Items.AddRange(new string[] { "GPA", "Credits" });
            optionsCombo.SelectedIndex = 0; // Default to GPA
            this.Controls.Add(optionsCombo);

            // Submit Button
            submitBtn = new Button
            {
                Text = "Submit",
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrchid,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            submitBtn.FlatAppearance.BorderColor = Color.DarkViolet;
            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(186, 85, 211);
            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.DarkOrchid;
            submitBtn.Click += new EventHandler(submitBtn_Click);
            this.Controls.Add(submitBtn);

            ArrangeControls();
        }

        private void ArrangeControls()
        {
            int startY = 40;

            // Center heading
            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
            startY = headingLabel.Bottom + Margin;

            // Back button
            backBtn.Location = new Point(20, startY);
            startY = backBtn.Bottom + Margin;


            startY += 80;
            // ComboBoxes and Submit button
            int comboX = (this.ClientSize.Width - (4 * 200 + 3 * Margin)) / 2;
            branchCombo.Location = new Point(comboX, startY);
            acYearCombo.Location = new Point(comboX + 200 + Margin, startY);
            optionsCombo.Location = new Point(comboX + 2 * (200 + Margin), startY);

            //startY += 300;
            submitBtn.Location = new Point(comboX + 3 * (40 + Margin), 300);
        }

        
        private void PopulateComboBoxes()
        {
            try
            {
                // Branches
                var marks = LoadJsonData<StudentMarks>("studentMarks.json") ?? new List<StudentMarks>();

                var branches = marks
                    .Where(m => !string.IsNullOrWhiteSpace(m.StudentId))
                    .Select(m => GetBranchFromStudentId(m.StudentId))
                    .Distinct()
                    .OrderBy(b => b)
                    .ToList();

                branchCombo.Items.Clear();
                if (branches.Any())
                {
                    branchCombo.Items.AddRange(branches.ToArray());
                    branchCombo.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No branches found in studentMarks.json", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Academic Years
                var academicYears = LoadJsonData<AcademicYear>("student_academicyear.json") ?? new List<AcademicYear>();

                var years = academicYears
                    .Where(ay => !string.IsNullOrWhiteSpace(ay.academic_year))
                    .Select(ay => ay.academic_year)
                    .Distinct()
                    .OrderBy(ay => ay)
                    .ToArray();

                acYearCombo.Items.Clear();
                if (years.Length > 0)
                {
                    acYearCombo.Items.AddRange(years);
                    acYearCombo.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No academic years found in student_academicyear.json", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading dropdowns: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
     

        private void submitBtn_Click(object sender, EventArgs e)
        {
            if (branchCombo.SelectedItem == null || acYearCombo.SelectedItem == null || optionsCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select all options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string branch = branchCombo.SelectedItem.ToString();
            string acYear = acYearCombo.SelectedItem.ToString();
            string option = optionsCombo.SelectedItem.ToString();

            LoadResults(branch, acYear, option);
            ResultsForm_gpa_credits resultsForm = new ResultsForm_gpa_credits(results, option, branch, acYear, this);
            Application.OpenForms["admindashboard"]?.Close();
            resultsForm.Show();
            this.Hide();
        }

        private void LoadResults(string branch, string acYear, string option)
        {
            var marks = LoadJsonData<StudentM>("studentMarks.json");
            var subjects = LoadJsonData<Subjec>("subjectCodes.json");
            var names = LoadJsonData<StudentName>("studentnames.json");
            var academicYears = LoadJsonData<AcademicY>("student_academicyear.json");

            // Map academic years
            var academicYearDict = academicYears.ToDictionary(ay => ay.studentid.ToUpper(), ay => ay.academic_year);

            // Filter by branch and academic year
            var filteredMarks = marks.Where(m =>
                GetBranchFromStudentId(m.StudentId) == branch &&
                academicYearDict.ContainsKey(m.StudentId.ToUpper()) &&
                academicYearDict[m.StudentId.ToUpper()] == acYear
            ).ToList();

            // Map student names
            var nameDict = names.ToDictionary(n => n.studentid.ToUpper(), n => n.name);

            // Create subject credits dictionary
            var subjectCreditsDict = subjects.ToDictionary(s => s.SubjectCode.ToUpper(), s => double.TryParse(s.Credits, out var c) ? c : 0);

            // Join data for SGPA/Credits
            var studentData = filteredMarks
                .Where(m => subjectCreditsDict.ContainsKey(m.SubjectCode.ToUpper()))
                .Select(m => new
                {
                    m.StudentId,
                    m.SubjectCode,
                    m.Grade,
                    CreditsFromMarks = double.TryParse(m.Credits, out var c) ? c : 0,
                    CreditsFromSubjects = subjectCreditsDict[m.SubjectCode.ToUpper()],
                    GradePoints = double.TryParse(m.GradePoints, out var gp) ? gp : 0,
                    YearSem = subjects.FirstOrDefault(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()) != null
                        ? $"{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Year}-{subjects.First(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()).Semester}"
                        : null
                })
                .Where(d => d.YearSem != null)
                .ToList();

            // Group by student
            var groupedByStudent = studentData
                .GroupBy(d => d.StudentId.ToUpper())
                .Select(g => new
                {
                    StudentId = g.Key,
                    StudentName = nameDict.ContainsKey(g.Key) ? nameDict[g.Key] : "Unknown",
                    Semesters = g.GroupBy(s => s.YearSem).ToDictionary(
                        s => s.Key,
                        s => s.Select(x => new { x.CreditsFromMarks, x.CreditsFromSubjects, x.GradePoints }).ToList())
                })
                .ToList();

            results = new List<StudentResult>();
            foreach (var student in groupedByStudent)
            {
                var result = new StudentResult
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Semesters = new Dictionary<string, string>
                    {
                        { "1-1", "-" }, { "1-2", "-" }, { "2-1", "-" }, { "2-2", "-" },
                        { "3-1", "-" }, { "3-2", "-" }, { "4-1", "-" }, { "4-2", "-" }
                    }
                };

                double totalSgpa = 0;
                int semesterCount = 0;
                double totalCredits = 0;

                foreach (var semester in student.Semesters)
                {
                    double semGradePoints = 0;
                    double semCredits = 0;

                    foreach (var subject in semester.Value)
                    {
                        if (option == "GPA")
                        {
                            semGradePoints += subject.GradePoints * subject.CreditsFromSubjects;
                            semCredits += subject.CreditsFromSubjects;
                        }
                        else // Credits
                        {
                            semCredits += subject.CreditsFromMarks;
                        }
                    }

                    if (option == "GPA")
                    {
                        if (semCredits > 0)
                        {
                            double sgpa = semGradePoints / semCredits;
                            result.Semesters[semester.Key] = sgpa.ToString("F2");
                            totalSgpa += sgpa;
                            semesterCount++;
                        }
                    }
                    else // Credits
                    {
                        result.Semesters[semester.Key] = semCredits.ToString("F1");
                        totalCredits += semCredits;
                    }
                }

                if (option == "GPA")
                {
                    if (semesterCount > 0)
                    {
                        result.CGPA = (totalSgpa / semesterCount).ToString("F2");
                    }
                    else
                    {
                        result.CGPA = "-";
                    }
                }
                else // Credits
                {
                    result.TotalCredits = totalCredits.ToString("F1");
                }

                results.Add(result);
            }
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
        }

        private void getbtn_Load(object sender, EventArgs e)
        {

        }
    }

    public class StudentM
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

    public class Subjec
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Credits { get; set; }
    }

    public class StudentName
    {
        public string studentid { get; set; }
        public string name { get; set; }
    }

    public class AcademicY
    {
        public string studentid { get; set; }
        public string academic_year { get; set; }
    }

    public class StudentResult
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public Dictionary<string, string> Semesters { get; set; }
        public string CGPA { get; set; }
        public string TotalCredits { get; set; }
    }
}