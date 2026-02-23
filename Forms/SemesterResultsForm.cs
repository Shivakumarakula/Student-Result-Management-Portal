//////////using OfficeOpenXml;
//////////using OfficeOpenXml.Style;
//////////using System;
//////////using System.Collections.Generic;
//////////using System.Drawing;
//////////using System.IO;
//////////using System.Linq;
//////////using System.Windows.Forms;

//////////namespace project_RYS.Forms
//////////{
//////////    public partial class SemesterResultsForm : Form
//////////    {
//////////        private DataGridView dataGridView;
//////////        private Button downloadBtn;
//////////        private Button backBtn;
//////////        private Label headingLabel;
//////////        private subject_wise_Gp subject_wise_Gp;
//////////        private const int DataGridViewWidth = 1200;
//////////        private const int MaxDataGridViewHeight = 650;
//////////        private const int Margin = 30;
//////////        private readonly List<StudentR> results;
//////////        private readonly string branch;
//////////        private readonly string acYear;
//////////        private readonly string year;
//////////        private readonly string semester;
//////////        //private readonly InputForm parentForm;

//////////        public SemesterResultsForm(List<StudentR> results, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
//////////        {
//////////            InitializeComponent();
//////////            this.results = results;
//////////            this.branch = branch;
//////////            this.acYear = acYear;
//////////            this.year = year;
//////////            this.semester = semester;
//////////            //this.parentForm = parentForm;
//////////            SetupForm();
//////////            PopulateDataGridView();
//////////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//////////        }

//////////        private void SetupForm()
//////////        {
//////////            this.Size = new Size(1400, 900);
//////////            this.AutoScroll = true;
//////////            this.BackColor = Color.FromArgb(240, 240, 240);

//////////            // Heading Label
//////////            headingLabel = new Label
//////////            {
//////////                Text = "Semester Results",
//////////                Font = new Font("Arial", 18, FontStyle.Bold),
//////////                AutoSize = true,
//////////                ForeColor = Color.DarkOrchid,
//////////                Location = new Point(610, 30)
//////////            };
//////////            this.Controls.Add(headingLabel);

//////////            // Back Button
//////////            backBtn = new Button
//////////            {
//////////                Text = "Back",
//////////                Width = 180,
//////////                Height = 45,
//////////                Location = new Point(50, 80),
//////////                FlatStyle = FlatStyle.Flat,
//////////                BackColor = Color.DarkViolet,
//////////                ForeColor = Color.White,
//////////                Font = new Font("Arial", 11, FontStyle.Bold)
//////////            };
//////////            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//////////            backBtn.FlatAppearance.BorderSize = 1;
//////////            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//////////            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//////////            backBtn.Click += new EventHandler(backBtn_Click);
//////////            this.Controls.Add(backBtn);

//////////            // DataGridView
//////////            dataGridView = new DataGridView
//////////            {
//////////                Width = DataGridViewWidth,
//////////                Location = new Point(100, 150),
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
//////////                    BackColor = Color.White,
//////////                    ForeColor = Color.Black
//////////                },
//////////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//////////                {
//////////                    BackColor = Color.FromArgb(200, 162, 200)
//////////                },
//////////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//////////                GridColor = Color.White
//////////            };
//////////            dataGridView.RowTemplate.Height = 35;
//////////            dataGridView.ColumnHeadersHeight = 45;
//////////            this.Controls.Add(dataGridView);

//////////            // Download Button
//////////            downloadBtn = new Button
//////////            {
//////////                Text = "Download",
//////////                Width = 180,
//////////                Height = 45,
//////////                FlatStyle = FlatStyle.Flat,
//////////                BackColor = Color.DarkOrchid,
//////////                ForeColor = Color.White,
//////////                Font = new Font("Arial", 11, FontStyle.Bold)
//////////            };
//////////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////////            downloadBtn.FlatAppearance.BorderSize = 1;
//////////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//////////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//////////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//////////            this.Controls.Add(downloadBtn);

//////////            ArrangeControls();
//////////        }

//////////        private void ArrangeControls()
//////////        {
//////////            int startY = 30;

//////////            // Center heading
//////////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//////////            startY = headingLabel.Bottom + 50;

//////////            // Back button
//////////            backBtn.Location = new Point(50, startY);
//////////            startY = backBtn.Bottom + Margin;

//////////            // DataGridView
//////////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//////////            startY = dataGridView.Bottom + Margin;

//////////            // Download button
//////////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//////////        }

//////////        private void PopulateDataGridView()
//////////        {
//////////            dataGridView.Columns.Clear();

//////////            // Define fixed columns
//////////            dataGridView.Columns.Add("Student ID", "Student ID");
//////////            dataGridView.Columns.Add("Student Name", "Student Name");

//////////            // Add subject columns
//////////            var subjectNames = results.SelectMany(r => r.SubjectResults.Keys).Distinct().OrderBy(s => s).ToList();
//////////            foreach (var subjectName in subjectNames)
//////////            {
//////////                dataGridView.Columns.Add(subjectName, subjectName);
//////////            }

//////////            // Add Total GradePoints and SGPA
//////////            dataGridView.Columns.Add("Total GradePoints", "Total GradePoints");
//////////            dataGridView.Columns.Add("SGPA", "SGPA");

//////////            // Set column widths
//////////            dataGridView.Columns[0].Width = 180; // Student ID
//////////            dataGridView.Columns[1].Width = 250; // Student Name
//////////            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
//////////            {
//////////                dataGridView.Columns[i].Width = 120; // Subjects
//////////            }
//////////            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
//////////            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

//////////            // Populate rows
//////////            dataGridView.Rows.Clear();
//////////            foreach (var result in results.OrderBy(r => r.StudentId))
//////////            {
//////////                var row = new List<object> { result.StudentId, result.StudentName };
//////////                foreach (var subjectName in subjectNames)
//////////                {
//////////                    row.Add(result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-");
//////////                }
//////////                row.Add(result.TotalGradePoints);
//////////                row.Add(result.SGPA);
//////////                dataGridView.Rows.Add(row.ToArray());
//////////            }

//////////            if (dataGridView.Rows.Count == 0)
//////////            {
//////////                var row = new List<object> { "No data available" };
//////////                for (int i = 1; i < dataGridView.Columns.Count; i++)
//////////                {
//////////                    row.Add("");
//////////                }
//////////                dataGridView.Rows.Add(row.ToArray());
//////////            }

//////////            // Set DataGridView height
//////////            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//////////            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//////////        }

//////////        private int CalculateDataGridViewHeight(int rowCount)
//////////        {
//////////            int rowHeight = 35;
//////////            int headerHeight = 45;
//////////            return headerHeight + rowCount * rowHeight;
//////////        }

//////////        private void downloadBtn_Click(object sender, EventArgs e)
//////////        {
//////////            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//////////            {
//////////                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////////                return;
//////////            }

//////////            using (var package = new ExcelPackage())
//////////            {
//////////                var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

//////////                // Report Info
//////////                worksheet.Cells[1, 1].Value = "Branch";
//////////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//////////                worksheet.Cells[2, 1].Value = "Academic Year";
//////////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//////////                worksheet.Cells[3, 1].Value = "Year";
//////////                worksheet.Cells[3, 2].Value = year ?? "Unknown";
//////////                worksheet.Cells[4, 1].Value = "Semester";
//////////                worksheet.Cells[4, 2].Value = semester ?? "Unknown";

//////////                // Headers
//////////                int startRow = 6;
//////////                for (int i = 0; i < dataGridView.Columns.Count; i++)
//////////                {
//////////                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].HeaderText;
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
//////////                    Title = "Save Semester Results Report",
//////////                    FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
//////////                };

//////////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//////////                {
//////////                    var file = new FileInfo(saveFileDialog.FileName);
//////////                    package.SaveAs(file);
//////////                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//////////                }
//////////            }
//////////        }

//////////        private void backBtn_Click(object sender, EventArgs e)
//////////        {
//////////            this.Hide();
//////////            //parentForm.Show();
//////////        }
//////////    }
//////////}

////////using OfficeOpenXml;
////////using OfficeOpenXml.Style;
////////using System;
////////using System.Collections.Generic;
////////using System.Drawing;
////////using System.IO;
////////using System.Linq;
////////using System.Windows.Forms;

////////namespace project_RYS.Forms
////////{
////////    public partial class SemesterResultsForm : Form
////////    {
////////        private DataGridView dataGridView;
////////        private Button downloadBtn;
////////        private Button backBtn;
////////        private Label headingLabel;
////////        private readonly subject_wise_Gp parentForm;
////////        private const int DataGridViewWidth = 1200;
////////        private const int MaxDataGridViewHeight = 650;
////////        private const int Margin = 30;
////////        private readonly List<StudentResult> results;
////////        private readonly string branch;
////////        private readonly string acYear;
////////        private readonly string year;
////////        private readonly string semester;

////////        public SemesterResultsForm(List<StudentResult> results, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
////////        {
////////            InitializeComponent();
////////            this.results = results ?? new List<StudentResult>();
////////            this.branch = branch;
////////            this.acYear = acYear;
////////            this.year = year;
////////            this.semester = semester;
////////            this.parentForm = parentForm;
////////            SetupForm();
////////            PopulateDataGridView();
////////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////////        }

////////        private void SetupForm()
////////        {
////////            this.Size = new Size(1400, 900);
////////            this.AutoScroll = true;
////////            this.BackColor = Color.FromArgb(240, 240, 240);

////////            // Heading Label
////////            headingLabel = new Label
////////            {
////////                Text = "Semester Results",
////////                Font = new Font("Arial", 18, FontStyle.Bold),
////////                AutoSize = true,
////////                ForeColor = Color.DarkOrchid
////////            };
////////            this.Controls.Add(headingLabel);

////////            // Back Button
////////            backBtn = new Button
////////            {
////////                Text = "Back",
////////                Width = 180,
////////                Height = 45,
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

////////            // DataGridView
////////            dataGridView = new DataGridView
////////            {
////////                Width = DataGridViewWidth,
////////                AutoGenerateColumns = false,
////////                RowHeadersVisible = false,
////////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////////                AllowUserToAddRows = false,
////////                ScrollBars = ScrollBars.Vertical,
////////                ReadOnly = true,
////////                AllowUserToResizeColumns = false,
////////                AllowUserToResizeRows = false,
////////                EnableHeadersVisualStyles = false,
////////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////////                {
////////                    BackColor = Color.DarkOrchid,
////////                    ForeColor = Color.White,
////////                    Font = new Font("Arial", 10, FontStyle.Bold),
////////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////////                },
////////                RowsDefaultCellStyle = new DataGridViewCellStyle
////////                {
////////                    Font = new Font("Arial", 10),
////////                    BackColor = Color.White,
////////                    ForeColor = Color.Black
////////                },
////////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////////                {
////////                    BackColor = Color.FromArgb(200, 162, 200)
////////                },
////////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////////                GridColor = Color.White
////////            };
////////            dataGridView.RowTemplate.Height = 35;
////////            dataGridView.ColumnHeadersHeight = 45;
////////            this.Controls.Add(dataGridView);

////////            // Download Button
////////            downloadBtn = new Button
////////            {
////////                Text = "Download",
////////                Width = 180,
////////                Height = 45,
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkOrchid,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 11, FontStyle.Bold)
////////            };
////////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////////            downloadBtn.FlatAppearance.BorderSize = 1;
////////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
////////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
////////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
////////            this.Controls.Add(downloadBtn);

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

////////            // DataGridView
////////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////////            startY = dataGridView.Bottom + Margin;

////////            // Download button
////////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
////////        }

////////        private void PopulateDataGridView()
////////        {
////////            dataGridView.Columns.Clear();

////////            // Define fixed columns
////////            dataGridView.Columns.Add("StudentID", "Student ID");
////////            dataGridView.Columns.Add("StudentName", "Student Name");

////////            // Add subject columns
////////            var subjectNames = results
////////                .Where(r => r.SubjectResults != null)
////////                .SelectMany(r => r.SubjectResults.Keys)
////////                .Distinct(StringComparer.OrdinalIgnoreCase)
////////                .OrderBy(s => s)
////////                .ToList();

////////            foreach (var subjectName in subjectNames)
////////            {
////////                // Truncate long subject names for header
////////                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
////////                dataGridView.Columns.Add(subjectName, displayName);
////////            }

////////            // Add Total GradePoints and SGPA
////////            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
////////            dataGridView.Columns.Add("SGPA", "SGPA");

////////            // Set column widths dynamically
////////            dataGridView.Columns[0].Width = 180; // Student ID
////////            dataGridView.Columns[1].Width = 250; // Student Name
////////            int subjectCount = subjectNames.Count;
////////            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150; // Subtract fixed columns
////////            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
////////            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
////////            {
////////                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects, cap at 150px
////////            }
////////            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
////////            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

////////            // Populate rows
////////            dataGridView.Rows.Clear();
////////            foreach (var result in results.OrderBy(r => r.StudentId))
////////            {
////////                if (result.SubjectResults == null)
////////                {
////////                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
////////                }
////////                var row = new List<object> { result.StudentId, result.Student ಶಿಷ್ಟಾಚಾರ: 
////////                    MessageBox.Show($"Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////////            }
////////        }


////////        private void backBtn_Click(object sender, EventArgs e)
////////        {
////////            this.Hide();
////////            //parentForm?.Show();
////////        }
////////    }

////////}




//////using OfficeOpenXml;
//////using OfficeOpenXml.Style;
//////using System;
//////using System.Collections.Generic;
//////using System.Drawing;
//////using System.IO;
//////using System.Linq;
//////using System.Windows.Forms;

//////namespace project_RYS.Forms
//////{
//////    public partial class SemesterResultsForm : Form
//////    {
//////        private DataGridView dataGridView;
//////        private Button downloadBtn;
//////        private Button backBtn;
//////        private Label headingLabel;
//////        private readonly subject_wise_Gp parentForm;
//////        private const int DataGridViewWidth = 1200;
//////        private const int MaxDataGridViewHeight = 650;
//////        private const int Margin = 30;
//////        private readonly List<StudentR> results;
//////        private readonly string branch;
//////        private readonly string acYear;
//////        private readonly string year;
//////        private readonly string semester;

//////        public SemesterResultsForm(List<StudentR> results, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
//////        {
//////            InitializeComponent();
//////            this.results = results ?? new List<StudentR>();
//////            this.branch = branch;
//////            this.acYear = acYear;
//////            this.year = year;
//////            this.semester = semester;
//////            this.parentForm = parentForm;
//////            SetupForm();
//////            PopulateDataGridView();
//////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//////        }

//////        private void SetupForm()
//////        {
//////            this.Size = new Size(1400, 900);
//////            this.AutoScroll = true;
//////            this.BackColor = Color.FromArgb(240, 240, 240);

//////            // Heading Label
//////            headingLabel = new Label
//////            {
//////                Text = "Semester Results",
//////                Font = new Font("Arial", 18, FontStyle.Bold),
//////                AutoSize = true,
//////                ForeColor = Color.DarkOrchid
//////            };
//////            this.Controls.Add(headingLabel);

//////            // Back Button
//////            backBtn = new Button
//////            {
//////                Text = "Back",
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

//////            // DataGridView
//////            dataGridView = new DataGridView
//////            {
//////                Width = DataGridViewWidth,
//////                AutoGenerateColumns = false,
//////                RowHeadersVisible = false,
//////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//////                AllowUserToAddRows = false,
//////                ScrollBars = ScrollBars.Vertical,
//////                ReadOnly = true,
//////                AllowUserToResizeColumns = false,
//////                AllowUserToResizeRows = false,
//////                EnableHeadersVisualStyles = false,
//////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//////                {
//////                    BackColor = Color.DarkOrchid,
//////                    ForeColor = Color.White,
//////                    Font = new Font("Arial", 10, FontStyle.Bold),
//////                    Alignment = DataGridViewContentAlignment.MiddleCenter
//////                },
//////                RowsDefaultCellStyle = new DataGridViewCellStyle
//////                {
//////                    Font = new Font("Arial", 10),
//////                    BackColor = Color.White,
//////                    ForeColor = Color.Black
//////                },
//////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//////                {
//////                    BackColor = Color.FromArgb(200, 162, 200)
//////                },
//////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//////                GridColor = Color.White
//////            };
//////            dataGridView.RowTemplate.Height = 35;
//////            dataGridView.ColumnHeadersHeight = 45;
//////            this.Controls.Add(dataGridView);

//////            // Download Button
//////            downloadBtn = new Button
//////            {
//////                Text = "Download",
//////                Width = 180,
//////                Height = 45,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkOrchid,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 11, FontStyle.Bold)
//////            };
//////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////            downloadBtn.FlatAppearance.BorderSize = 1;
//////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//////            this.Controls.Add(downloadBtn);

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

//////            // DataGridView
//////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//////            startY = dataGridView.Bottom + Margin;

//////            // Download button
//////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//////        }

//////        private void PopulateDataGridView()
//////        {
//////            dataGridView.Columns.Clear();

//////            // Define fixed columns
//////            dataGridView.Columns.Add("StudentID", "Student ID");
//////            dataGridView.Columns.Add("StudentName", "Student Name");

//////            // Add subject columns
//////            var subjectNames = results
//////                .Where(r => r.SubjectResults != null)
//////                .SelectMany(r => r.SubjectResults.Keys)
//////                .Distinct(StringComparer.OrdinalIgnoreCase)
//////                .OrderBy(s => s)
//////                .ToList();

//////            foreach (var subjectName in subjectNames)
//////            {
//////                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
//////                dataGridView.Columns.Add(subjectName, displayName);
//////            }

//////            // Add Total GradePoints and SGPA
//////            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
//////            dataGridView.Columns.Add("SGPA", "SGPA");

//////            // Set column widths dynamically
//////            dataGridView.Columns[0].Width = 180; // Student ID
//////            dataGridView.Columns[1].Width = 250; // Student Name
//////            int subjectCount = subjectNames.Count;
//////            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150; // Subtract fixed columns
//////            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
//////            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
//////            {
//////                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects, cap at 150px
//////            }
//////            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
//////            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

//////            // Populate rows
//////            dataGridView.Rows.Clear();
//////            foreach (var result in results.OrderBy(r => r.StudentId))
//////            {
//////                if (result.SubjectResults == null)
//////                {
//////                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//////                }
//////                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
//////                foreach (var subjectName in subjectNames)
//////                {
//////                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
//////                    if (value != "-" && !double.TryParse(value, out _))
//////                    {
//////                        value = "-"; // Ensure invalid values are displayed as "-"
//////                    }
//////                    row.Add(value);
//////                }
//////                row.Add(result.TotalGradePoints ?? "-");
//////                row.Add(result.SGPA ?? "-");
//////                dataGridView.Rows.Add(row.ToArray());
//////            }

//////            if (dataGridView.Rows.Count == 0)
//////            {
//////                var row = new List<object> { "No data available" };
//////                for (int i = 1; i < dataGridView.Columns.Count; i++)
//////                {
//////                    row.Add("");
//////                }
//////                dataGridView.Rows.Add(row.ToArray());
//////            }

//////            // Set DataGridView height
//////            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//////            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//////        }

//////        private int CalculateDataGridViewHeight(int rowCount)
//////        {
//////            int rowHeight = 35;
//////            int headerHeight = 45;
//////            return headerHeight + rowCount * rowHeight;
//////        }

//////        private void downloadBtn_Click(object sender, EventArgs e)
//////        {
//////            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//////            {
//////                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//////                return;
//////            }

//////            using (var package = new ExcelPackage())
//////            {
//////                var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

//////                // Report Info
//////                worksheet.Cells[1, 1].Value = "Branch";
//////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//////                worksheet.Cells[2, 1].Value = "Academic Year";
//////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//////                worksheet.Cells[3, 1].Value = "Year";
//////                worksheet.Cells[3, 2].Value = year ?? "Unknown";
//////                worksheet.Cells[4, 1].Value = "Semester";
//////                worksheet.Cells[4, 2].Value = semester ?? "Unknown";

//////                // Headers
//////                int startRow = 6;
//////                for (int i = 0; i < dataGridView.Columns.Count; i++)
//////                {
//////                    string headerText = dataGridView.Columns[i].Name;
//////                    if (results.Any(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText)))
//////                    {
//////                        headerText = results
//////                            .Where(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText))
//////                            .Select(r => r.SubjectResults.Keys.First(k => k.Equals(headerText, StringComparison.OrdinalIgnoreCase)))
//////                            .FirstOrDefault() ?? headerText;
//////                    }
//////                    worksheet.Cells[startRow, i + 1].Value = headerText;
//////                }

//////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
//////                {
//////                    headerRange.Style.Font.Bold = true;
//////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//////                    headerRange.Style.Font.Color.SetColor(Color.White);
//////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//////                }

//////                // Data
//////                for (int i = 0; i < dataGridView.Rows.Count; i++)
//////                {
//////                    for (int j = 0; j < dataGridView.Columns.Count; j++)
//////                    {
//////                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
//////                    }
//////                }

//////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//////                var saveFileDialog = new SaveFileDialog
//////                {
//////                    Filter = "Excel files (*.xlsx)|*.xlsx",
//////                    Title = "Save Semester Results Report",
//////                    FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
//////                };

//////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//////                {
//////                    var file = new FileInfo(saveFileDialog.FileName);
//////                    package.SaveAs(file);
//////                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//////                }
//////            }
//////        }

//////        private void backBtn_Click(object sender, EventArgs e)
//////        {
//////            this.Hide();
//////            //parentForm?.Show();
//////        }
//////    }
//////}

////using OfficeOpenXml;
////using OfficeOpenXml.Style;
////using System;
////using System.Collections.Generic;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Windows.Forms;

////namespace project_RYS.Forms
////{
////    public partial class SemesterResultsForm : Form
////    {
////        private DataGridView dataGridView;
////        private Button downloadBtn;
////        private Button backBtn;
////        private Label headingLabel;
////        private readonly subject_wise_Gp parentForm;
////        private const int DataGridViewWidth = 1200;
////        private const int MaxDataGridViewHeight = 650;
////        private const int Margin = 30;
////        private readonly List<StudentR> results;
////        private readonly string branch;
////        private readonly string acYear;
////        private readonly string year;
////        private readonly string semester;

////        public SemesterResultsForm(List<StudentR> results, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
////        {
////            InitializeComponent();
////            this.results = results ?? new List<StudentR>();
////            this.branch = branch;
////            this.acYear = acYear;
////            this.year = year;
////            this.semester = semester;
////            this.parentForm = parentForm;
////            SetupForm();
////            PopulateDataGridView();
////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////        }

////        private void SetupForm()
////        {
////            this.Size = new Size(1400, 900);
////            this.AutoScroll = true;
////            this.BackColor = Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Semester Results",
////                Font = new Font("Arial", 18, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backBtn = new Button
////            {
////                Text = "Back",
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

////            // DataGridView
////            dataGridView = new DataGridView
////            {
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.Vertical,
////                ReadOnly = true,
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false,
////                EnableHeadersVisualStyles = false,
////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new Font("Arial", 10),
////                    BackColor = Color.White,
////                    ForeColor = Color.Black
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.FromArgb(200, 162, 200)
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };
////            dataGridView.RowTemplate.Height = 35;
////            dataGridView.ColumnHeadersHeight = 45;
////            this.Controls.Add(dataGridView);

////            // Download Button
////            downloadBtn = new Button
////            {
////                Text = "Download",
////                Width = 180,
////                Height = 45,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 11, FontStyle.Bold)
////            };
////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////            downloadBtn.FlatAppearance.BorderSize = 1;
////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
////            this.Controls.Add(downloadBtn);

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

////            // DataGridView
////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////            startY = dataGridView.Bottom + Margin;

////            // Download button
////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
////        }

////        private void PopulateDataGridView()
////        {
////            dataGridView.Columns.Clear();

////            // Define fixed columns
////            dataGridView.Columns.Add("StudentID", "Student ID");
////            dataGridView.Columns.Add("StudentName", "Student Name");

////            // Add subject columns (preserve order from results)
////            var subjectNames = results
////                .Where(r => r.SubjectResults != null)
////                .SelectMany(r => r.SubjectResults.Keys)
////                .Distinct(StringComparer.OrdinalIgnoreCase)
////                .ToList(); // No OrderBy to preserve subject_wise_Gp order

////            foreach (var subjectName in subjectNames)
////            {
////                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
////                dataGridView.Columns.Add(subjectName, displayName);
////            }

////            // Add Total GradePoints and SGPA
////            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
////            dataGridView.Columns.Add("SGPA", "SGPA");

////            // Set column widths dynamically
////            dataGridView.Columns[0].Width = 180; // Student ID
////            dataGridView.Columns[1].Width = 250; // Student Name
////            int subjectCount = subjectNames.Count;
////            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150; // Subtract fixed columns
////            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
////            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
////            {
////                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects, cap at 150px
////            }
////            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
////            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

////            // Populate rows
////            dataGridView.Rows.Clear();
////            foreach (var result in results.OrderBy(r => r.StudentId))
////            {
////                if (result.SubjectResults == null)
////                {
////                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
////                }
////                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
////                foreach (var subjectName in subjectNames)
////                {
////                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
////                    if (value != "-" && !double.TryParse(value, out _))
////                    {
////                        value = "-"; // Ensure invalid values are displayed as "-"
////                    }
////                    row.Add(value);
////                }
////                row.Add(result.TotalGradePoints ?? "-");
////                row.Add(result.SGPA ?? "-");
////                dataGridView.Rows.Add(row.ToArray());
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                var row = new List<object> { "No data available" };
////                for (int i = 1; i < dataGridView.Columns.Count; i++)
////                {
////                    row.Add("");
////                }
////                dataGridView.Rows.Add(row.ToArray());
////            }

////            // Set DataGridView height
////            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
////        }

////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 35;
////            int headerHeight = 45;
////            return headerHeight + rowCount * rowHeight;
////        }

////        private void downloadBtn_Click(object sender, EventArgs e)
////        {
////            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
////            {
////                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return;
////            }

////            using (var package = new ExcelPackage())
////            {
////                var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

////                // Report Info
////                worksheet.Cells[1, 1].Value = "Branch";
////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
////                worksheet.Cells[2, 1].Value = "Academic Year";
////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
////                worksheet.Cells[3, 1].Value = "Year";
////                worksheet.Cells[3, 2].Value = year ?? "Unknown";
////                worksheet.Cells[4, 1].Value = "Semester";
////                worksheet.Cells[4, 2].Value = semester ?? "Unknown";

////                // Headers
////                int startRow = 6;
////                for (int i = 0; i < dataGridView.Columns.Count; i++)
////                {
////                    string headerText = dataGridView.Columns[i].Name;
////                    if (results.Any(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText)))
////                    {
////                        headerText = results
////                            .Where(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText))
////                            .Select(r => r.SubjectResults.Keys.First(k => k.Equals(headerText, StringComparison.OrdinalIgnoreCase)))
////                            .FirstOrDefault() ?? headerText;
////                    }
////                    worksheet.Cells[startRow, i + 1].Value = headerText;
////                }

////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Data
////                for (int i = 0; i < dataGridView.Rows.Count; i++)
////                {
////                    for (int j = 0; j < dataGridView.Columns.Count; j++)
////                    {
////                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
////                    }
////                }

////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
////                var saveFileDialog = new SaveFileDialog
////                {
////                    Filter = "Excel files (*.xlsx)|*.xlsx",
////                    Title = "Save Semester Results Report",
////                    FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
////                };

////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////                {
////                    var file = new FileInfo(saveFileDialog.FileName);
////                    package.SaveAs(file);
////                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                }
////            }
////        }

////        private void backBtn_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            //parentForm?.Show();
////        }
////    }
////}

//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class SemesterResultsForm : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private readonly subject_wise_Gp parentForm;
//        private const int DataGridViewWidth = 1200;
//        private const int MaxDataGridViewHeight = 650;
//        private const int Margin = 30;
//        private readonly List<StudentR> results;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly string year;
//        private readonly string semester;

//        public SemesterResultsForm(List<StudentR> results, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
//        {
//            InitializeComponent();
//            this.results = results ?? new List<StudentR>();
//            this.branch = branch;
//            this.acYear = acYear;
//            this.year = year;
//            this.semester = semester;
//            this.parentForm = parentForm;
//            SetupForm();
//            PopulateDataGridView();
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Semester Results",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
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

//            // DataGridView
//            dataGridView = new DataGridView
//            {
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.Vertical,
//                ReadOnly = true,
//                AllowUserToResizeColumns = false,
//                AllowUserToResizeRows = false,
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
//                    Font = new Font("Arial", 10),
//                    BackColor = Color.White,
//                    ForeColor = Color.Black
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(200, 162, 200)
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };
//            dataGridView.RowTemplate.Height = 35;
//            dataGridView.ColumnHeadersHeight = 45;
//            this.Controls.Add(dataGridView);

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadBtn.FlatAppearance.BorderSize = 1;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//            this.Controls.Add(downloadBtn);

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

//            // DataGridView
//            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//            startY = dataGridView.Bottom + Margin;

//            // Download button
//            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define fixed columns
//            dataGridView.Columns.Add("StudentID", "Student ID");
//            dataGridView.Columns.Add("StudentName", "Student Name");

//            // Add subject columns (preserve order from results)
//            var subjectNames = results
//                .Where(r => r.SubjectResults != null)
//                .SelectMany(r => r.SubjectResults.Keys)
//                .Distinct(StringComparer.OrdinalIgnoreCase)
//                .ToList(); // No sorting to preserve subject_wise_Gp order

//            foreach (var subjectName in subjectNames)
//            {
//                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
//                dataGridView.Columns.Add(subjectName, displayName);
//            }

//            // Add Total GradePoints and SGPA
//            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
//            dataGridView.Columns.Add("SGPA", "SGPA");

//            // Set column widths
//            dataGridView.Columns[0].Width = 180; // Student ID
//            dataGridView.Columns[1].Width = 250; // Student Name
//            int subjectCount = subjectNames.Count;
//            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150;
//            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
//            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
//            {
//                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects
//            }
//            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
//            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                if (result.SubjectResults == null)
//                {
//                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//                }
//                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
//                foreach (var subjectName in subjectNames)
//                {
//                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
//                    if (value != "-" && !double.TryParse(value, out _))
//                    {
//                        value = "-";
//                    }
//                    row.Add(value);
//                }
//                row.Add(result.TotalGradePoints ?? "-");
//                row.Add(result.SGPA ?? "-");
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                var row = new List<object> { "No data available" };
//                for (int i = 1; i < dataGridView.Columns.Count; i++)
//                {
//                    row.Add("");
//                }
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            // Set DataGridView height
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 35;
//            int headerHeight = 45;
//            return headerHeight + rowCount * rowHeight;
//        }

//        private void downloadBtn_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//            {
//                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

//                // Report Info
//                worksheet.Cells[1, 1].Value = "Branch";
//                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                worksheet.Cells[2, 1].Value = "Academic Year";
//                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                worksheet.Cells[3, 1].Value = "Year";
//                worksheet.Cells[3, 2].Value = year ?? "Unknown";
//                worksheet.Cells[4, 1].Value = "Semester";
//                worksheet.Cells[4, 2].Value = semester ?? "Unknown";

//                // Headers
//                int startRow = 6;
//                for (int i = 0; i < dataGridView.Columns.Count; i++)
//                {
//                    string headerText = dataGridView.Columns[i].Name;
//                    if (results.Any(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText)))
//                    {
//                        headerText = results
//                            .Where(r => r.SubjectResults != null && r.SubjectResults.ContainsKey(headerText))
//                            .Select(r => r.SubjectResults.Keys.First(k => k.Equals(headerText, StringComparison.OrdinalIgnoreCase)))
//                            .FirstOrDefault() ?? headerText;
//                    }
//                    worksheet.Cells[startRow, i + 1].Value = headerText;
//                }

//                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
//                {
//                    headerRange.Style.Font.Bold = true;
//                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//                    headerRange.Style.Font.Color.SetColor(Color.White);
//                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }

//                // Data
//                for (int i = 0; i < dataGridView.Rows.Count; i++)
//                {
//                    for (int j = 0; j < dataGridView.Columns.Count; j++)
//                    {
//                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
//                    }
//                }

//                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//                var saveFileDialog = new SaveFileDialog
//                {
//                    Filter = "Excel files (*.xlsx)|*.xlsx",
//                    Title = "Save Semester Results Report",
//                    FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//            parentForm?.Show();
//        }
//    }
//}


//using Newtonsoft.Json;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class SemesterResultsForm : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private readonly subject_wise_Gp parentForm;
//        private readonly List<Subjectss> relevantSubjects;
//        private const int DataGridViewWidth = 1200;
//        private const int MaxDataGridViewHeight = 650;
//        private const int Margin = 30;
//        private readonly List<StudentR> results;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly string year;
//        private readonly string semester;

//        public SemesterResultsForm(List<StudentR> results, List<Subjectss> relevantSubjects, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
//        {
//            InitializeComponent();
//            this.results = results ?? new List<StudentR>();
//            this.relevantSubjects = relevantSubjects ?? new List<Subjectss>();
//            this.branch = branch;
//            this.acYear = acYear;
//            this.year = year;
//            this.semester = semester;
//            this.parentForm = parentForm;
//            SetupForm();
//            PopulateDataGridView();
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Semester Results",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
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

//            // DataGridView
//            dataGridView = new DataGridView
//            {
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.Both, // Enable both horizontal and vertical scroll bars
//                ReadOnly = true,
//                AllowUserToResizeColumns = false,
//                AllowUserToResizeRows = false,
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
//                    Font = new Font("Arial", 10),
//                    BackColor = Color.White,
//                    ForeColor = Color.Black
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(200, 162, 200)
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };
//            dataGridView.RowTemplate.Height = 35;
//            dataGridView.ColumnHeadersHeight = 45;
//            this.Controls.Add(dataGridView);

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadBtn.FlatAppearance.BorderSize = 1;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//            this.Controls.Add(downloadBtn);

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

//            // DataGridView
//            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//            startY = dataGridView.Bottom + Margin;

//            // Download button
//            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define fixed columns
//            dataGridView.Columns.Add("StudentID", "Student ID");
//            dataGridView.Columns.Add("StudentName", "Student Name");

//            // Get subject names from marks for relevant subjects
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var subjectNameDict = marks
//                .Where(m => relevantSubjects.Any(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()))
//                .GroupBy(m => m.SubjectCode.ToUpper())
//                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//            // Add subject columns (only for relevant subjects)
//            var subjectNames = relevantSubjects
//                .Select(s => subjectNameDict.ContainsKey(s.SubjectCode.ToUpper()) ? subjectNameDict[s.SubjectCode.ToUpper()] : s.SubjectCode)
//                .ToList();

//            foreach (var subjectName in subjectNames)
//            {
//                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
//                dataGridView.Columns.Add(subjectName, displayName);
//            }

//            // Add Total GradePoints and SGPA
//            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
//            dataGridView.Columns.Add("SGPA", "SGPA");

//            // Set column widths
//            dataGridView.Columns[0].Width = 180; // Student ID
//            dataGridView.Columns[1].Width = 250; // Student Name
//            int subjectCount = subjectNames.Count;
//            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150;
//            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
//            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
//            {
//                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects
//            }
//            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
//            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                if (result.SubjectResults == null)
//                {
//                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//                }
//                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
//                foreach (var subjectName in subjectNames)
//                {
//                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
//                    if (value != "-" && !double.TryParse(value, out _))
//                    {
//                        value = "-";
//                    }
//                    row.Add(value);
//                }
//                row.Add(result.TotalGradePoints ?? "-");
//                row.Add(result.SGPA ?? "-");
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                var row = new List<object> { "No data available" };
//                for (int i = 1; i < dataGridView.Columns.Count; i++)
//                {
//                    row.Add("");
//                }
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            // Set DataGridView height
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 35;
//            int headerHeight = 45;
//            return headerHeight + rowCount * rowHeight;
//        }

//        private void downloadBtn_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//            {
//                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

//                // Report Info
//                worksheet.Cells[1, 1].Value = "Branch";
//                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                worksheet.Cells[2, 1].Value = "Academic Year";
//                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                worksheet.Cells[3, 1].Value = "Year";
//                worksheet.Cells[3, 2].Value = year ?? "Unknown";
//                worksheet.Cells[4, 1].Value = "Semester";
//                worksheet.Cells[4, 2].Value = semester ?? "Unknown";

//                // Headers
//                int startRow = 6;
//                for (int i = 0; i < dataGridView.Columns.Count; i++)
//                {
//                    string headerText = dataGridView.Columns[i].HeaderText; // Use HeaderText for Excel
//                    worksheet.Cells[startRow, i + 1].Value = headerText;
//                }

//                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
//                {
//                    headerRange.Style.Font.Bold = true;
//                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//                    headerRange.Style.Font.Color.SetColor(Color.White);
//                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }

//                // Data
//                for (int i = 0; i < dataGridView.Rows.Count; i++)
//                {
//                    for (int j = 0; j < dataGridView.Columns.Count; j++)
//                    {
//                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
//                    }
//                }

//                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//                var saveFileDialog = new SaveFileDialog
//                {
//                    Filter = "Excel files (*.xlsx)|*.xlsx",
//                    Title = "Save Semester Results Report",
//                    FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                return new List<T>();
//            }

//            try
//            {
//                var json = File.ReadAllText(filePath);
//                var data = JsonConvert.DeserializeObject<List<T>>(json);
//                return data ?? new List<T>();
//            }
//            catch
//            {
//                return new List<T>();
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            //parentForm?.Show();
//        }
//    }
//}

//using Newtonsoft.Json;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class SemesterResultsForm : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private readonly subject_wise_Gp parentForm;
//        private readonly List<Subjectss> relevantSubjects;
//        private const int DataGridViewWidth = 1200;
//        private const int MaxDataGridViewHeight = 650;
//        private const int Margin = 30;
//        private readonly List<StudentR> results;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly string year;
//        private readonly string semester;

//        public SemesterResultsForm(List<StudentR> results, List<Subjectss> relevantSubjects, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
//        {
//            InitializeComponent();
//            this.results = results ?? new List<StudentR>();
//            this.relevantSubjects = relevantSubjects ?? new List<Subjectss>();
//            this.branch = branch;
//            this.acYear = acYear;
//            this.year = year;
//            this.semester = semester;
//            this.parentForm = parentForm;
//            SetupForm();
//            PopulateDataGridView();
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Semester Results",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
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

//            // DataGridView
//            dataGridView = new DataGridView
//            {
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.Both,
//                ReadOnly = true,
//                AllowUserToResizeColumns = false,
//                AllowUserToResizeRows = false,
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
//                    Font = new Font("Arial", 10),
//                    BackColor = Color.White,
//                    ForeColor = Color.Black
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(200, 162, 200)
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };
//            dataGridView.RowTemplate.Height = 35;
//            dataGridView.ColumnHeadersHeight = 45;
//            this.Controls.Add(dataGridView);

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 180,
//                Height = 45,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 11, FontStyle.Bold)
//            };
//            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadBtn.FlatAppearance.BorderSize = 1;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//            this.Controls.Add(downloadBtn);

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

//            // Download button (positioned next to Back button)
//            downloadBtn.Location = new Point(backBtn.Right + Margin, startY);
//            startY = backBtn.Bottom + Margin;

//            // DataGridView
//            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define fixed columns
//            dataGridView.Columns.Add("StudentID", "Student ID");
//            dataGridView.Columns.Add("StudentName", "Student Name");

//            // Get subject names from marks for relevant subjects
//            var marks = LoadJsonData<StudentMa>("studentMarks.json");
//            var subjectNameDict = marks
//                .Where(m => relevantSubjects.Any(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()))
//                .GroupBy(m => m.SubjectCode.ToUpper())
//                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//            // Add subject columns (only for relevant subjects)
//            var subjectNames = relevantSubjects
//                .Select(s => subjectNameDict.ContainsKey(s.SubjectCode.ToUpper()) ? subjectNameDict[s.SubjectCode.ToUpper()] : s.SubjectCode)
//                .ToList();

//            foreach (var subjectName in subjectNames)
//            {
//                // Use full subject name for column Name, truncate for HeaderText if needed
//                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
//                dataGridView.Columns.Add(subjectName, displayName);
//            }

//            // Add Total GradePoints and SGPA
//            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
//            dataGridView.Columns.Add("SGPA", "SGPA");

//            // Set column widths
//            dataGridView.Columns[0].Width = 180; // Student ID
//            dataGridView.Columns[1].Width = 250; // Student Name
//            int subjectCount = subjectNames.Count;
//            int availableWidth = DataGridViewWidth - 180 - 250 - 150 - 150;
//            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
//            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
//            {
//                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150); // Subjects
//            }
//            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
//            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                if (result.SubjectResults == null)
//                {
//                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//                }
//                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
//                foreach (var subjectName in subjectNames)
//                {
//                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
//                    if (value != "-" && !double.TryParse(value, out _))
//                    {
//                        value = "-";
//                    }
//                    row.Add(value);
//                }
//                row.Add(result.TotalGradePoints ?? "-");
//                row.Add(result.SGPA ?? "-");
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                var row = new List<object> { "No data available" };
//                for (int i = 1; i < dataGridView.Columns.Count; i++)
//                {
//                    row.Add("");
//                }
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            // Set DataGridView height
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 35;
//            int headerHeight = 45;
//            return headerHeight + rowCount * rowHeight;
//        }

//        private void downloadBtn_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
//            {
//                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            try
//            {
//                using (var package = new ExcelPackage())
//                {
//                    var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

//                    // Report Info
//                    worksheet.Cells[1, 1].Value = "Branch";
//                    worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                    worksheet.Cells[2, 1].Value = "Academic Year";
//                    worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                    worksheet.Cells[3, 1].Value = "Year";
//                    worksheet.Cells[3, 2].Value = year ?? "Unknown";
//                    worksheet.Cells[4, 1].Value = "Semester";
//                    worksheet.Cells[4, 2].Value = semester ?? "Unknown";

//                    // Style report info
//                    using (var infoRange = worksheet.Cells[1, 1, 4, 2])
//                    {
//                        infoRange.Style.Font.Bold = true;
//                        infoRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
//                    }

//                    // Headers
//                    int startRow = 6;
//                    var marks = LoadJsonData<StudentMa>("studentMarks.json");
//                    var subjectNameDict = marks
//                        .Where(m => relevantSubjects.Any(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()))
//                        .GroupBy(m => m.SubjectCode.ToUpper())
//                        .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

//                    for (int i = 0; i < dataGridView.Columns.Count; i++)
//                    {
//                        string columnName = dataGridView.Columns[i].Name;
//                        string headerText = dataGridView.Columns[i].HeaderText;
//                        // Use full subject name for subjects
//                        if (i >= 2 && i < dataGridView.Columns.Count - 2)
//                        {
//                            var subject = relevantSubjects[i - 2];
//                            headerText = subjectNameDict.ContainsKey(subject.SubjectCode.ToUpper())
//                                ? subjectNameDict[subject.SubjectCode.ToUpper()]
//                                : subject.SubjectCode;
//                        }
//                        worksheet.Cells[startRow, i + 1].Value = headerText;
//                    }

//                    using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
//                    {
//                        headerRange.Style.Font.Bold = true;
//                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//                        headerRange.Style.Font.Color.SetColor(Color.White);
//                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                        headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
//                    }

//                    // Data
//                    for (int i = 0; i < dataGridView.Rows.Count; i++)
//                    {
//                        for (int j = 0; j < dataGridView.Columns.Count; j++)
//                        {
//                            worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
//                            worksheet.Cells[startRow + i + 1, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                            worksheet.Cells[startRow + i + 1, j + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
//                        }
//                    }

//                    // Auto-fit columns
//                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//                    // Set minimum column width for readability
//                    for (int i = 1; i <= worksheet.Dimension.Columns; i++)
//                    {
//                        if (worksheet.Column(i).Width < 10)
//                            worksheet.Column(i).Width = 10;
//                    }

//                    var saveFileDialog = new SaveFileDialog
//                    {
//                        Filter = "Excel files (*.xlsx)|*.xlsx",
//                        Title = "Save Semester Results Report",
//                        FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
//                    };

//                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                    {
//                        var file = new FileInfo(saveFileDialog.FileName);
//                        package.SaveAs(file);
//                        MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                }
//            }
//            catch (IOException ex)
//            {
//                MessageBox.Show($"Error saving Excel file: {ex.Message}\nPlease ensure the file is not open and you have write permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                return new List<T>();
//            }

//            try
//            {
//                var json = File.ReadAllText(filePath);
//                var data = JsonConvert.DeserializeObject<List<T>>(json);
//                return data ?? new List<T>();
//            }
//            catch
//            {
//                return new List<T>();
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            //this.Hide();
//            //parentForm?.Show();
//            this.Hide();
//            Application.OpenForms["admindashboard"]?.Close();
//            admindashboard adb = new admindashboard();
//            adb.Show();

//            try
//            {
//                subject_wise_Gp child = new subject_wise_Gp();
//                child.TopLevel = false;
//                child.FormBorderStyle = FormBorderStyle.None;
//                child.Dock = DockStyle.Fill;
//                adb.panelDesktopPane.Controls.Clear(); // Clear existing content
//                adb.panelDesktopPane.Controls.Add(child); // Add ChildForm to panel
//                child.Show();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}



using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class SemesterResultsForm : Form
    {
        private DataGridView dataGridView;
        private Button downloadBtn;
        private Button backBtn;
        private Label headingLabel;
        private Label lblBranch;
        private Label lblAcademicYear;
        private Label lblYear;
        private Label lblSemester;
        private readonly subject_wise_Gp parentForm;
        private readonly List<Subjectss> relevantSubjects;
        private readonly List<StudentR> results;
        private readonly string branch;
        private readonly string acYear;
        private readonly string year;
        private readonly string semester;

        public SemesterResultsForm(List<StudentR> results, List<Subjectss> relevantSubjects, string branch, string acYear, string year, string semester, subject_wise_Gp parentForm)
        {
            InitializeComponent();
            this.results = results ?? new List<StudentR>();
            this.relevantSubjects = relevantSubjects ?? new List<Subjectss>();
            this.branch = branch;
            this.acYear = acYear;
            this.year = year;
            this.semester = semester;
            this.parentForm = parentForm;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            SetupForm();
            PopulateDataGridView();
        }

        private void SetupForm()
        {
            // Form settings
            this.Size = new Size(1000, 600);
            this.Text = "Semester Results";
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            // Header Label
            headingLabel = new Label
            {
                Text = "Semester Results",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid,
                Location = new Point((this.ClientSize.Width - 300) / 2, 20)
            };
            this.Controls.Add(headingLabel);

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
            backBtn.FlatAppearance.BorderSize = 1;
            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
            backBtn.Click += new EventHandler(backBtn_Click);
            this.Controls.Add(backBtn);

            // Filter Labels
            lblBranch = new Label
            {
                Text = $"Branch: {branch}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 60)
            };
            this.Controls.Add(lblBranch);

            lblAcademicYear = new Label
            {
                Text = $"Academic Year: {acYear}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 85)
            };
            this.Controls.Add(lblAcademicYear);

            lblYear = new Label
            {
                Text = $"Year: {year}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 110)
            };
            this.Controls.Add(lblYear);

            lblSemester = new Label
            {
                Text = $"Semester: {semester}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 135)
            };
            this.Controls.Add(lblSemester);

            // DataGridView
            dataGridView = new DataGridView
            {
                Location = new Point(20, 190),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.Both,
                ReadOnly = true,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
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
                    Font = new Font("Arial", 10),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 240, 255)
                },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.LightGray
            };
            dataGridView.RowTemplate.Height = 35;
            dataGridView.ColumnHeadersHeight = 45;
            this.Controls.Add(dataGridView);

            // Download Button
            downloadBtn = new Button
            {
                Text = "Download Excel",
                Location = new Point((this.ClientSize.Width - 150) / 2, this.ClientSize.Height - 60),
                Width = 150,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrchid,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom
            };
            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
            downloadBtn.FlatAppearance.BorderSize = 1;
            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
            downloadBtn.Click += new EventHandler(downloadBtn_Click);
            this.Controls.Add(downloadBtn);

            // Adjust positions on resize
            this.Resize += (s, e) =>
            {
                headingLabel.Left = (this.ClientSize.Width - 300) / 2;
                backBtn.Left = this.ClientSize.Width - 240;
                downloadBtn.Left = (this.ClientSize.Width - downloadBtn.Width) / 2;
                downloadBtn.Top = this.ClientSize.Height - 60;
                dataGridView.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 250);
            };
        }

        private void PopulateDataGridView()
        {
            dataGridView.Columns.Clear();

            // Define fixed columns
            dataGridView.Columns.Add("StudentID", "Student ID");
            dataGridView.Columns.Add("StudentName", "Student Name");

            // Get subject names from marks for relevant subjects
            var marks = LoadJsonData<StudentMa>("studentMarks.json");
            var subjectNameDict = marks
                .Where(m => relevantSubjects.Any(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()))
                .GroupBy(m => m.SubjectCode.ToUpper())
                .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

            // Add subject columns
            var subjectNames = relevantSubjects
                .Select(s => subjectNameDict.ContainsKey(s.SubjectCode.ToUpper()) ? subjectNameDict[s.SubjectCode.ToUpper()] : s.SubjectCode)
                .ToList();

            foreach (var subjectName in subjectNames)
            {
                string displayName = subjectName.Length > 20 ? subjectName.Substring(0, 17) + "..." : subjectName;
                dataGridView.Columns.Add(subjectName, displayName);
            }

            // Add Total GradePoints and SGPA
            dataGridView.Columns.Add("TotalGradePoints", "Total GradePoints");
            dataGridView.Columns.Add("SGPA", "SGPA");

            // Set column widths
            dataGridView.Columns[0].Width = 180; // Student ID
            dataGridView.Columns[1].Width = 250; // Student Name
            int subjectCount = subjectNames.Count;
            int availableWidth = (this.ClientSize.Width - 40) - 180 - 250 - 150 - 150; // Adjust for form width
            int subjectWidth = subjectCount > 0 ? Math.Max(80, availableWidth / subjectCount) : 120;
            for (int i = 2; i < dataGridView.Columns.Count - 2; i++)
            {
                dataGridView.Columns[i].Width = Math.Min(subjectWidth, 150);
            }
            dataGridView.Columns[dataGridView.Columns.Count - 2].Width = 150; // Total GradePoints
            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 150; // SGPA

            // Populate rows
            dataGridView.Rows.Clear();
            foreach (var result in results.OrderBy(r => r.StudentId))
            {
                if (result.SubjectResults == null)
                {
                    result.SubjectResults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                var row = new List<object> { result.StudentId ?? "Unknown", result.StudentName ?? "Unknown" };
                foreach (var subjectName in subjectNames)
                {
                    string value = result.SubjectResults.ContainsKey(subjectName) ? result.SubjectResults[subjectName] : "-";
                    if (value != "-" && !double.TryParse(value, out _))
                    {
                        value = "-";
                    }
                    row.Add(value);
                }
                row.Add(result.TotalGradePoints ?? "-");
                row.Add(result.SGPA ?? "-");
                dataGridView.Rows.Add(row.ToArray());
            }

            // Handle empty data
            if (dataGridView.Rows.Count == 0)
            {
                var row = new List<object> { "No data available" };
                for (int i = 1; i < dataGridView.Columns.Count; i++)
                {
                    row.Add("");
                }
                dataGridView.Rows.Add(row.ToArray());
            }

            // Ensure last row is visible
            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
            {
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
            }
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
            {
                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Semester Results Report");

                    // Report Info
                    worksheet.Cells[1, 1].Value = "Branch";
                    worksheet.Cells[1, 2].Value = branch ?? "Unknown";
                    worksheet.Cells[2, 1].Value = "Academic Year";
                    worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
                    worksheet.Cells[3, 1].Value = "Year";
                    worksheet.Cells[3, 2].Value = year ?? "Unknown";
                    worksheet.Cells[4, 1].Value = "Semester";
                    worksheet.Cells[4, 2].Value = semester ?? "Unknown";

                    // Style report info
                    using (var infoRange = worksheet.Cells[1, 1, 4, 2])
                    {
                        infoRange.Style.Font.Bold = true;
                        infoRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }

                    // Headers
                    int startRow = 6;
                    var marks = LoadJsonData<StudentMa>("studentMarks.json");
                    var subjectNameDict = marks
                        .Where(m => relevantSubjects.Any(s => s.SubjectCode.ToUpper() == m.SubjectCode.ToUpper()))
                        .GroupBy(m => m.SubjectCode.ToUpper())
                        .ToDictionary(g => g.Key, g => g.First().SubjectName, StringComparer.OrdinalIgnoreCase);

                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        string columnName = dataGridView.Columns[i].Name;
                        string headerText = dataGridView.Columns[i].HeaderText;
                        if (i >= 2 && i < dataGridView.Columns.Count - 2)
                        {
                            var subject = relevantSubjects[i - 2];
                            headerText = subjectNameDict.ContainsKey(subject.SubjectCode.ToUpper())
                                ? subjectNameDict[subject.SubjectCode.ToUpper()]
                                : subject.SubjectCode;
                        }
                        worksheet.Cells[startRow, i + 1].Value = headerText;
                    }

                    using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
                        headerRange.Style.Font.Color.SetColor(Color.White);
                        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    // Data
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            worksheet.Cells[startRow + i + 1, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[startRow + i + 1, j + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                    {
                        if (worksheet.Column(i).Width < 10)
                            worksheet.Column(i).Width = 10;
                    }

                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Semester Results Report",
                        FileName = $"SemesterResults_{branch}_{acYear}_Year{year}_Sem{semester}.xlsx"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(file);
                        MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error saving Excel file: {ex.Message}\nPlease ensure the file is not open and you have write permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<T>>(json);
                return data ?? new List<T>();
            }
            catch
            {
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
                subject_wise_Gp child = new subject_wise_Gp();
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                adb.panelDesktopPane.Controls.Clear();
                adb.panelDesktopPane.Controls.Add(child);
                child.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SemesterResultsForm_Load(object sender, EventArgs e)
        {
            // Ensure last row is visible on load
            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
            {
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
            }
        }
    }
}