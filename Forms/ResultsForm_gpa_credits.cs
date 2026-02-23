//////////using OfficeOpenXml.Style;
//////////using OfficeOpenXml;
//////////using System;
//////////using System.Collections.Generic;
//////////using System.ComponentModel;
//////////using System.Data;
//////////using System.Drawing;
//////////using System.IO;
//////////using System.Linq;
//////////using System.Text;
//////////using System.Threading.Tasks;
//////////using System.Windows.Forms;


//////////namespace project_RYS.Forms
//////////{
//////////    public partial class ResultsForm_gpa_credits : Form
//////////    {
//////////        private DataGridView dataGridView;
//////////        private Button downloadBtn;
//////////        private Button backBtn;
//////////        private Label headingLabel;
//////////        private const int DataGridViewWidth = 1250;
//////////        private const int Margin = 20;
//////////        private readonly List<StudentResult> results;
//////////        private readonly string option;
//////////        private readonly string branch;
//////////        private readonly string acYear;
//////////        private readonly getbtn parentForm;

//////////        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
//////////        {
//////////            InitializeComponent();
//////////            this.results = results;
//////////            this.option = option;
//////////            this.branch = branch;
//////////            this.acYear = acYear;
//////////            this.parentForm = parentForm;
//////////            SetupForm();
//////////            PopulateDataGridView();
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
//////////                Text = "Student Results",
//////////                Font = new Font("Arial", 16, FontStyle.Bold),
//////////                AutoSize = true,
//////////                ForeColor = Color.DarkOrchid
//////////            };
//////////            this.Controls.Add(headingLabel);

//////////            // Back Button
//////////            backBtn = new Button
//////////            {
//////////                Text = "Back",
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
//////////            backBtn.Click += new EventHandler(backBtn_Click);
//////////            this.Controls.Add(backBtn);

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

//////////            // DataGridView
//////////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//////////            startY = dataGridView.Bottom + Margin;

//////////            // Download button
//////////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//////////        }

//////////        private void PopulateDataGridView()
//////////        {
//////////            dataGridView.Columns.Clear();

//////////            // Define columns
//////////            dataGridView.Columns.Add("Student ID", "Student ID");
//////////            dataGridView.Columns.Add("Student Name", "Student Name");
//////////            dataGridView.Columns.Add("1-1", "1-1");
//////////            dataGridView.Columns.Add("1-2", "1-2");
//////////            dataGridView.Columns.Add("2-1", "2-1");
//////////            dataGridView.Columns.Add("2-2", "2-2");
//////////            dataGridView.Columns.Add("3-1", "3-1");
//////////            dataGridView.Columns.Add("3-2", "3-2");
//////////            dataGridView.Columns.Add("4-1", "4-1");
//////////            dataGridView.Columns.Add("4-2", "4-2");
//////////            if (option == "GPA")
//////////            {
//////////                dataGridView.Columns.Add("CGPA", "CGPA");
//////////            }
//////////            else
//////////            {
//////////                dataGridView.Columns.Add("Total Credits", "Total Credits");
//////////            }

//////////            // Set column widths
//////////            dataGridView.Columns[0].Width = 150;
//////////            dataGridView.Columns[1].Width = 200;
//////////            for (int i = 2; i <= 9; i++)
//////////            {
//////////                dataGridView.Columns[i].Width = 100;
//////////            }
//////////            dataGridView.Columns[10].Width = 100;

//////////            // Populate rows
//////////            dataGridView.Rows.Clear();
//////////            foreach (var result in results.OrderBy(r => r.StudentId))
//////////            {
//////////                var row = new List<object> { result.StudentId, result.StudentName };
//////////                row.AddRange(new[] {
//////////                    result.Semesters["1-1"], result.Semesters["1-2"],
//////////                    result.Semesters["2-1"], result.Semesters["2-2"],
//////////                    result.Semesters["3-1"], result.Semesters["3-2"],
//////////                    result.Semesters["4-1"], result.Semesters["4-2"]
//////////                });
//////////                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//////////                dataGridView.Rows.Add(row.ToArray());
//////////            }

//////////            if (dataGridView.Rows.Count == 0)
//////////            {
//////////                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
//////////            }

//////////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//////////        }

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

//////////            using (var package = new ExcelPackage())
//////////            {
//////////                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//////////                // Report Info
//////////                worksheet.Cells[1, 1].Value = "Branch";
//////////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//////////                worksheet.Cells[2, 1].Value = "Academic Year";
//////////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//////////                worksheet.Cells[3, 1].Value = "Report Type";
//////////                worksheet.Cells[3, 2].Value = option ?? "Unknown";

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

//////////        private void backBtn_Click(object sender, EventArgs e)
//////////        {
//////////            this.Close();
//////////            parentForm.Show();
//////////        }
//////////    }
//////////}using Newtonsoft.Json;
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
////////    public partial class ResultsForm_gpa_credits : Form
////////    {
////////        private DataGridView dataGridView;
////////        private Button downloadBtn;
////////        private Button backBtn;
////////        private Label headingLabel;
////////        private const int DataGridViewWidth = 1250;
////////        private const int Margin = 20;
////////        private readonly List<StudentResult> results;
////////        private readonly string option;
////////        private readonly string branch;
////////        private readonly string acYear;
////////        private readonly getbtn parentForm;

////////        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
////////        {
////////            InitializeComponent();
////////            this.results = results;
////////            this.option = option;
////////            this.branch = branch;
////////            this.acYear = acYear;
////////            this.parentForm = parentForm;
////////            SetupForm();
////////            PopulateDataGridView();
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
////////                Text = "Student Results",
////////                Font = new Font("Arial", 16, FontStyle.Bold),
////////                AutoSize = true,
////////                ForeColor = Color.DarkOrchid
////////            };
////////            this.Controls.Add(headingLabel);

////////            // Back Button
////////            backBtn = new Button
////////            {
////////                Text = "Back",
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
////////            backBtn.Click += new EventHandler(backBtn_Click);
////////            this.Controls.Add(backBtn);

////////            // Download Button
////////            downloadBtn = new Button
////////            {
////////                Text = "Download",
////////                Width = 200,
////////                Height = 40,
////////                FlatStyle = FlatStyle.Flat,
////////                BackColor = Color.DarkOrchid,
////////                ForeColor = Color.White,
////////                Font = new Font("Arial", 10, FontStyle.Bold)
////////            };
////////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
////////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
////////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
////////            this.Controls.Add(downloadBtn);

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
////////                    BackColor = Color.White
////////                },
////////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////////                {
////////                    BackColor = Color.DarkViolet
////////                },
////////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////////                GridColor = Color.White
////////            };
////////            dataGridView.RowTemplate.Height = 50;
////////            dataGridView.ColumnHeadersHeight = 40;
////////            this.Controls.Add(dataGridView);

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

////////            // DataGridView
////////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////////            startY = dataGridView.Bottom + Margin;

////////            // Download button
////////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
////////        }

////////        private void PopulateDataGridView()
////////        {
////////            dataGridView.Columns.Clear();

////////            // Define columns
////////            dataGridView.Columns.Add("Student ID", "Student ID");
////////            dataGridView.Columns.Add("Student Name", "Student Name");
////////            dataGridView.Columns.Add("1-1", "1-1");
////////            dataGridView.Columns.Add("1-2", "1-2");
////////            dataGridView.Columns.Add("2-1", "2-1");
////////            dataGridView.Columns.Add("2-2", "2-2");
////////            dataGridView.Columns.Add("3-1", "3-1");
////////            dataGridView.Columns.Add("3-2", "3-2");
////////            dataGridView.Columns.Add("4-1", "4-1");
////////            dataGridView.Columns.Add("4-2", "4-2");
////////            if (option == "GPA")
////////            {
////////                dataGridView.Columns.Add("CGPA", "CGPA");
////////            }
////////            else
////////            {
////////                dataGridView.Columns.Add("Total Credits", "Total Credits");
////////            }

////////            // Set column widths
////////            dataGridView.Columns[0].Width = 150;
////////            dataGridView.Columns[1].Width = 200;
////////            for (int i = 2; i <= 9; i++)
////////            {
////////                dataGridView.Columns[i].Width = 100;
////////            }
////////            dataGridView.Columns[10].Width = 100;

////////            // Populate rows
////////            dataGridView.Rows.Clear();
////////            foreach (var result in results.OrderBy(r => r.StudentId))
////////            {
////////                var row = new List<object> { result.StudentId, result.StudentName };
////////                row.AddRange(new[] {
////////                    result.Semesters["1-1"], result.Semesters["1-2"],
////////                    result.Semesters["2-1"], result.Semesters["2-2"],
////////                    result.Semesters["3-1"], result.Semesters["3-2"],
////////                    result.Semesters["4-1"], result.Semesters["4-2"]
////////                });
////////                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
////////                dataGridView.Rows.Add(row.ToArray());
////////            }

////////            if (dataGridView.Rows.Count == 0)
////////            {
////////                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
////////            }

////////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////////        }

////////        private int CalculateDataGridViewHeight(int rowCount)
////////        {
////////            int rowHeight = 50;
////////            int headerHeight = 40;
////////            int totalHeight = headerHeight + rowCount * rowHeight;
////////            return totalHeight < 90 ? 90 : totalHeight;
////////        }

////////        private void downloadBtn_Click(object sender, EventArgs e)
////////        {
////////            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value.ToString() == "No data available")
////////            {
////////                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////////                return;
////////            }

////////            using (var package = new ExcelPackage())
////////            {
////////                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

////////                // Report Info
////////                worksheet.Cells[1, 1].Value = "Branch";
////////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
////////                worksheet.Cells[2, 1].Value = "Academic Year";
////////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
////////                worksheet.Cells[3, 1].Value = "Report Type";
////////                worksheet.Cells[3, 2].Value = option ?? "Unknown";

////////                // Headers
////////                int startRow = 5;
////////                for (int i = 0; i < dataGridView.Columns.Count; i++)
////////                {
////////                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
////////                }

////////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
////////                {
////////                    headerRange.Style.Font.Bold = true;
////////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////////                    headerRange.Style.Font.Color.SetColor(Color.White);
////////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////////                }

////////                // Data
////////                for (int i = 0; i < dataGridView.Rows.Count; i++)
////////                {
////////                    for (int j = 0; j < dataGridView.Columns.Count; j++)
////////                    {
////////                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
////////                    }
////////                }

////////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
////////                var saveFileDialog = new SaveFileDialog
////////                {
////////                    Filter = "Excel files (*.xlsx)|*.xlsx",
////////                    Title = "Save Results Report"
////////                };

////////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////////                {
////////                    var file = new FileInfo(saveFileDialog.FileName);
////////                    package.SaveAs(file);
////////                    MessageBox.Show("Excel report saved successfully!");
////////                }
////////            }
////////        }

////////        private void backBtn_Click(object sender, EventArgs e)
////////        {
////////            this.Close();
////////            parentForm.Show();
////////        }
////////    }
////////}

//////using Newtonsoft.Json;
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
//////    public partial class ResultsForm_gpa_credits : Form
//////    {
//////        private DataGridView dataGridView;
//////        private Button downloadBtn;
//////        private Button backBtn;
//////        private Label headingLabel;
//////        private const int DataGridViewWidth = 1250;
//////        private const int Margin = 20;
//////        private readonly List<StudentResult> results;
//////        private readonly string option;
//////        private readonly string branch;
//////        private readonly string acYear;
//////        private readonly getbtn parentForm;

//////        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
//////        {
//////            InitializeComponent();
//////            this.results = results;
//////            this.option = option;
//////            this.branch = branch;
//////            this.acYear = acYear;
//////            this.parentForm = parentForm;
//////            SetupForm();
//////            PopulateDataGridView();
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
//////                Text = "Student Results",
//////                Font = new Font("Arial", 16, FontStyle.Bold),
//////                AutoSize = true,
//////                ForeColor = Color.DarkOrchid
//////            };
//////            this.Controls.Add(headingLabel);

//////            // Back Button
//////            backBtn = new Button
//////            {
//////                Text = "Back",
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
//////            backBtn.Click += new EventHandler(backBtn_Click);
//////            this.Controls.Add(backBtn);

//////            // Download Button
//////            downloadBtn = new Button
//////            {
//////                Text = "Download",
//////                Width = 200,
//////                Height = 40,
//////                FlatStyle = FlatStyle.Flat,
//////                BackColor = Color.DarkOrchid,
//////                ForeColor = Color.White,
//////                Font = new Font("Arial", 10, FontStyle.Bold)
//////            };
//////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//////            this.Controls.Add(downloadBtn);

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
//////                    BackColor = Color.White
//////                },
//////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//////                {
//////                    BackColor = Color.DarkViolet
//////                },
//////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//////                GridColor = Color.White
//////            };
//////            dataGridView.RowTemplate.Height = 50;
//////            dataGridView.ColumnHeadersHeight = 40;
//////            this.Controls.Add(dataGridView);

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

//////            // DataGridView
//////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//////            startY = dataGridView.Bottom + Margin;

//////            // Download button
//////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//////        }

//////        private void PopulateDataGridView()
//////        {
//////            dataGridView.Columns.Clear();

//////            // Define columns
//////            dataGridView.Columns.Add("Student ID", "Student ID");
//////            dataGridView.Columns.Add("Student Name", "Student Name");
//////            dataGridView.Columns.Add("1-1", "1-1");
//////            dataGridView.Columns.Add("1-2", "1-2");
//////            dataGridView.Columns.Add("2-1", "2-1");
//////            dataGridView.Columns.Add("2-2", "2-2");
//////            dataGridView.Columns.Add("3-1", "3-1");
//////            dataGridView.Columns.Add("3-2", "3-2");
//////            dataGridView.Columns.Add("4-1", "4-1");
//////            dataGridView.Columns.Add("4-2", "4-2");
//////            if (option == "GPA")
//////            {
//////                dataGridView.Columns.Add("CGPA", "CGPA");
//////            }
//////            else
//////            {
//////                dataGridView.Columns.Add("Total Credits", "Total Credits");
//////            }

//////            // Set column widths
//////            dataGridView.Columns[0].Width = 150;
//////            dataGridView.Columns[1].Width = 200;
//////            for (int i = 2; i <= 9; i++)
//////            {
//////                dataGridView.Columns[i].Width = 100;
//////            }
//////            dataGridView.Columns[10].Width = 100;

//////            // Populate rows
//////            dataGridView.Rows.Clear();
//////            foreach (var result in results.OrderBy(r => r.StudentId))
//////            {
//////                var row = new List<object> { result.StudentId, result.StudentName };
//////                row.AddRange(new[] {
//////                    result.Semesters["1-1"], result.Semesters["1-2"],
//////                    result.Semesters["2-1"], result.Semesters["2-2"],
//////                    result.Semesters["3-1"], result.Semesters["3-2"],
//////                    result.Semesters["4-1"], result.Semesters["4-2"]
//////                });
//////                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//////                dataGridView.Rows.Add(row.ToArray());
//////            }

//////            if (dataGridView.Rows.Count == 0)
//////            {
//////                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
//////            }

//////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//////        }

//////        private int CalculateDataGridViewHeight(int rowCount)
//////        {
//////            int rowHeight = 50;
//////            int headerHeight = 40;
//////            int totalHeight = headerHeight + rowCount * rowHeight;
//////            return totalHeight < 90 ? 90 : totalHeight;
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
//////                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//////                // Report Info
//////                worksheet.Cells[1, 1].Value = "Branch";
//////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//////                worksheet.Cells[2, 1].Value = "Academic Year";
//////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//////                worksheet.Cells[3, 1].Value = "Report Type";
//////                worksheet.Cells[3, 2].Value = option ?? "Unknown";

//////                // Headers
//////                int startRow = 5;
//////                for (int i = 0; i < dataGridView.Columns.Count; i++)
//////                {
//////                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
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
//////                    Title = "Save Results Report"
//////                };

//////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//////                {
//////                    var file = new FileInfo(saveFileDialog.FileName);
//////                    package.SaveAs(file);
//////                    MessageBox.Show("Excel report saved successfully!");
//////                }
//////            }
//////        }

//////        private void backBtn_Click(object sender, EventArgs e)
//////        {
//////            this.Close();
//////            parentForm.Show();
//////        }
//////    }
//////}


////using Newtonsoft.Json;
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
////    public partial class ResultsForm_gpa_credits : Form
////    {
////        private DataGridView dataGridView;
////        private Button downloadBtn;
////        private Button backBtn;
////        private Label headingLabel;
////        private const int DataGridViewWidth = 1250;
////        private const int MaxDataGridViewHeight = 600;
////        private const int Margin = 20;
////        private readonly List<StudentResult> results;
////        private readonly string option;
////        private readonly string branch;
////        private readonly string acYear;
////        private readonly getbtn parentForm;

////        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
////        {
////            InitializeComponent();
////            this.results = results;
////            this.option = option;
////            this.branch = branch;
////            this.acYear = acYear;
////            this.parentForm = parentForm;
////            SetupForm();
////            PopulateDataGridView();
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
////                Text = "Student Results",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backBtn = new Button
////            {
////                Text = "Back",
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
////            backBtn.Click += new EventHandler(backBtn_Click);
////            this.Controls.Add(backBtn);

////            // Download Button
////            downloadBtn = new Button
////            {
////                Text = "Download",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
////            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
////            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
////            downloadBtn.Click += new EventHandler(downloadBtn_Click);
////            this.Controls.Add(downloadBtn);

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
////                    BackColor = Color.White
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.FromArgb(186, 85, 211) // Lighter DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };
////            dataGridView.RowTemplate.Height = 40; // Reduced for more rows
////            dataGridView.ColumnHeadersHeight = 40;
////            this.Controls.Add(dataGridView);

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

////            // DataGridView
////            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////            startY = dataGridView.Bottom + Margin;

////            // Download button
////            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
////        }

////        private void PopulateDataGridView()
////        {
////            dataGridView.Columns.Clear();

////            // Define columns
////            dataGridView.Columns.Add("Student ID", "Student ID");
////            dataGridView.Columns.Add("Student Name", "Student Name");
////            dataGridView.Columns.Add("1-1", "1-1");
////            dataGridView.Columns.Add("1-2", "1-2");
////            dataGridView.Columns.Add("2-1", "2-1");
////            dataGridView.Columns.Add("2-2", "2-2");
////            dataGridView.Columns.Add("3-1", "3-1");
////            dataGridView.Columns.Add("3-2", "3-2");
////            dataGridView.Columns.Add("4-1", "4-1");
////            dataGridView.Columns.Add("4-2", "4-2");
////            if (option == "GPA")
////            {
////                dataGridView.Columns.Add("CGPA", "CGPA");
////            }
////            else
////            {
////                dataGridView.Columns.Add("Total Credits", "Total Credits");
////            }

////            // Set column widths
////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 200;
////            for (int i = 2; i <= 9; i++)
////            {
////                dataGridView.Columns[i].Width = 90; // Slightly narrower
////            }
////            dataGridView.Columns[10].Width = 90;

////            // Populate rows
////            dataGridView.Rows.Clear();
////            foreach (var result in results.OrderBy(r => r.StudentId))
////            {
////                var row = new List<object> { result.StudentId, result.StudentName };
////                row.AddRange(new[] {
////                    result.Semesters["1-1"], result.Semesters["1-2"],
////                    result.Semesters["2-1"], result.Semesters["2-2"],
////                    result.Semesters["3-1"], result.Semesters["3-2"],
////                    result.Semesters["4-1"], result.Semesters["4-2"]
////                });
////                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
////                dataGridView.Rows.Add(row.ToArray());
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
////            }

////            // Set DataGridView height
////            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
////        }

////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 40;
////            int headerHeight = 40;
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
////                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

////                // Report Info
////                worksheet.Cells[1, 1].Value = "Branch";
////                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
////                worksheet.Cells[2, 1].Value = "Academic Year";
////                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
////                worksheet.Cells[3, 1].Value = "Report Type";
////                worksheet.Cells[3, 2].Value = option ?? "Unknown";

////                // Headers
////                int startRow = 5;
////                for (int i = 0; i < dataGridView.Columns.Count; i++)
////                {
////                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
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
////                    Title = "Save Results Report"
////                };

////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////                {
////                    var file = new FileInfo(saveFileDialog.FileName);
////                    package.SaveAs(file);
////                    MessageBox.Show("Excel report saved successfully!");
////                }
////            }
////        }

////        private void backBtn_Click(object sender, EventArgs e)
////        {
////            this.Close();
////            parentForm.Show();
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
//    public partial class ResultsForm_gpa_credits : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private const int DataGridViewWidth = 1250;
//        private const int MaxDataGridViewHeight = 600;
//        private const int Margin = 20;
//        private readonly List<StudentResult> results;
//        private readonly string option;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly getbtn parentForm;

//        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
//        {
//            InitializeComponent();
//            this.results = results;
//            //Console.WriteLine(results);
//            this.option = option;
//            this.branch = branch;
//            this.acYear = acYear;
//            this.parentForm = parentForm;
//            SetupForm();
//            PopulateDataGridView();
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
//                Text = "Student Results",
//                Font = new Font("Arial", 16, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
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
//                    ForeColor = Color.Black // Ensure readability
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(186, 85, 211)
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };
//            dataGridView.RowTemplate.Height = 40;
//            dataGridView.ColumnHeadersHeight = 40;
//            this.Controls.Add(dataGridView);

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 200,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//            this.Controls.Add(downloadBtn);

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

//            // DataGridView
//            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//            startY = dataGridView.Bottom + Margin;

//            // Download button
//            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define columns
//            dataGridView.Columns.Add("Student ID", "Student ID");
//            dataGridView.Columns.Add("Student Name", "Student Name");
//            dataGridView.Columns.Add("1-1", "1-1");
//            dataGridView.Columns.Add("1-2", "1-2");
//            dataGridView.Columns.Add("2-1", "2-1");
//            dataGridView.Columns.Add("2-2", "2-2");
//            dataGridView.Columns.Add("3-1", "3-1");
//            dataGridView.Columns.Add("3-2", "3-2");
//            dataGridView.Columns.Add("4-1", "4-1");
//            dataGridView.Columns.Add("4-2", "4-2");
//            if (option == "GPA")
//            {
//                dataGridView.Columns.Add("CGPA", "CGPA");
//            }
//            else
//            {
//                dataGridView.Columns.Add("Total Credits", "Total Credits");
//            }

//            // Set column widths
//            dataGridView.Columns[0].Width = 150;
//            dataGridView.Columns[1].Width = 200;
//            for (int i = 2; i <= 9; i++)
//            {
//                dataGridView.Columns[i].Width = 90;
//            }
//            dataGridView.Columns[10].Width = 90;

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                var row = new List<object> { result.StudentId, result.StudentName };
//                row.AddRange(new[] {
//                    result.Semesters["1-1"], result.Semesters["1-2"],
//                    result.Semesters["2-1"], result.Semesters["2-2"],
//                    result.Semesters["3-1"], result.Semesters["3-2"],
//                    result.Semesters["4-1"], result.Semesters["4-2"]
//                });
//                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
//            }

//            // Set DataGridView height
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 40;
//            int headerHeight = 40;
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
//                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//                // Report Info
//                worksheet.Cells[1, 1].Value = "Branch";
//                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                worksheet.Cells[2, 1].Value = "Academic Year";
//                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                worksheet.Cells[3, 1].Value = "Report Type";
//                worksheet.Cells[3, 2].Value = option ?? "Unknown";

//                // Headers
//                int startRow = 5;
//                for (int i = 0; i < dataGridView.Columns.Count; i++)
//                {
//                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
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
//                    Title = "Save Results Report"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!");
//                }
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//            parentForm.Show();
//        }
//    }
//}

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
//    public partial class ResultsForm_gpa_credits : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private const int DataGridViewWidth = 1200;
//        private const int MaxDataGridViewHeight = 650;
//        private const int Margin = 30;
//        private readonly List<StudentResult> results;
//        private readonly string option;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly getbtn parentForm;

//        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
//        {
//            InitializeComponent();
//            this.results = results;
//            this.option = option;
//            this.branch = branch;
//            this.acYear = acYear;
//            this.parentForm = parentForm;
//            SetupForm();
//            PopulateDataGridView();
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        private void SetupForm()
//        {
//            // Form settings
//            this.Size = new Size(1400, 900);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Student Results",
//                Font = new Font("Arial", 18, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid,
//                Location = new Point(610, 30) // Centered: (1400 - width) / 2 ≈ 610
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
//                Width = 180,
//                Height = 45,
//                Location = new Point(50, 80),
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

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 180,
//                Height = 45,
//                Location = new Point(50, 100), // Will be set in ArrangeControls
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

//            // DataGridView
//            dataGridView = new DataGridView
//            {
//                Width = DataGridViewWidth,
//                Location = new Point(100, 150), // 50px left margin, below heading + back
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
//                    BackColor = Color.FromArgb(200, 162, 200) // Lighter purple
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };
//            dataGridView.RowTemplate.Height = 35;
//            dataGridView.ColumnHeadersHeight = 45;
//            this.Controls.Add(dataGridView);



//            ArrangeControls();
//        }

//        private void ArrangeControls()
//        {
//            int startY = 30;

//            // Center heading
//            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//            startY = headingLabel.Bottom + 50; // Larger margin below heading

//            // Back button
//            backBtn.Location = new Point(50, startY);
//            startY = backBtn.Bottom + Margin;

//            // Download button
//            downloadBtn.Location = new Point((this.ClientSize.Width - downloadBtn.Width) / 2, startY);
//            startY = downloadBtn.Bottom + Margin;
//            // DataGridView
//            dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//            startY = dataGridView.Bottom + Margin;


//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define columns
//            dataGridView.Columns.Add("Student ID", "Student ID");
//            dataGridView.Columns.Add("Student Name", "Student Name");
//            dataGridView.Columns.Add("1-1", "1-1");
//            dataGridView.Columns.Add("1-2", "1-2");
//            dataGridView.Columns.Add("2-1", "2-1");
//            dataGridView.Columns.Add("2-2", "2-2");
//            dataGridView.Columns.Add("3-1", "3-1");
//            dataGridView.Columns.Add("3-2", "3-2");
//            dataGridView.Columns.Add("4-1", "4-1");
//            dataGridView.Columns.Add("4-2", "4-2");
//            if (option == "GPA")
//            {
//                dataGridView.Columns.Add("CGPA", "CGPA");
//            }
//            else
//            {
//                dataGridView.Columns.Add("Total Credits", "Total Credits");
//            }

//            // Set column widths
//            dataGridView.Columns[0].Width = 180; // Student ID
//            dataGridView.Columns[1].Width = 250; // Student Name
//            for (int i = 2; i <= 9; i++)
//            {
//                dataGridView.Columns[i].Width = 80; // Semesters
//            }
//            dataGridView.Columns[10].Width = 110; // CGPA or Total Credits

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                var row = new List<object> { result.StudentId, result.StudentName };
//                row.AddRange(new[] {
//                    result.Semesters["1-1"], result.Semesters["1-2"],
//                    result.Semesters["2-1"], result.Semesters["2-2"],
//                    result.Semesters["3-1"], result.Semesters["3-2"],
//                    result.Semesters["4-1"], result.Semesters["4-2"]
//                });
//                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "", "");
//            }

//            // Set DataGridView height
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = (Math.Min(calculatedHeight, MaxDataGridViewHeight))-100;
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
//                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//                // Report Info
//                worksheet.Cells[1, 1].Value = "Branch";
//                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                worksheet.Cells[2, 1].Value = "Academic Year";
//                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                worksheet.Cells[3, 1].Value = "Report Type";
//                worksheet.Cells[3, 2].Value = option ?? "Unknown";

//                // Headers
//                int startRow = 5;
//                for (int i = 0; i < dataGridView.Columns.Count; i++)
//                {
//                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].Name;
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
//                    Title = "Save Results Report"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!");
//                }
//            }
//        }

//        private void backBtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            Application.OpenForms["admindashboard"]?.Close();
//            admindashboard adb = new admindashboard();
//            adb.Show();

//            try
//            {
//                getbtn child = new getbtn();
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

//        private void ResultsForm_gpa_credits_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}


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
//    public partial class ResultsForm_gpa_credits : Form
//    {
//        private DataGridView dataGridView;
//        private Button downloadBtn;
//        private Button backBtn;
//        private Label headingLabel;
//        private FlowLayoutPanel mainPanel;
//        private const int Margin = 20;
//        private const int MaxDataGridViewHeight = 650;
//        private const int DataGridViewWidth = 1200;
//        private readonly List<StudentResult> results;
//        private readonly string option;
//        private readonly string branch;
//        private readonly string acYear;
//        private readonly getbtn parentForm;

//        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
//        {
//            InitializeComponent();
//            this.results = results ?? new List<StudentResult>();
//            this.option = option;
//            this.branch = branch;
//            this.acYear = acYear;
//            this.parentForm = parentForm;
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//            SetupForm();
//            PopulateDataGridView();
//        }

//        private void SetupForm()
//        {
//            // Form settings
//            this.Size = new Size(1400, 900);
//            this.MinimumSize = new Size(800, 600);
//            this.AutoScroll = true; // Enable form-level vertical scrollbar
//            this.BackColor = Color.FromArgb(240, 240, 240);
//            this.FormBorderStyle = FormBorderStyle.Sizable;
//            this.MaximizeBox = true;

//            // Main panel for layout
//            mainPanel = new FlowLayoutPanel
//            {
//                Dock = DockStyle.Fill,
//                FlowDirection = FlowDirection.TopDown,
//                AutoSize = false, // Prevent auto-sizing to ensure scrolling
//                AutoScroll = true, // Enable panel scrolling for large content
//                Padding = new Padding(Margin),
//                BackColor = Color.FromArgb(240, 240, 240)
//            };
//            this.Controls.Add(mainPanel);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = $"{option} Report - {branch} ({acYear})",
//                Font = new Font("Arial", 16, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid,
//                TextAlign = ContentAlignment.MiddleCenter,
//                Margin = new Padding(0, 0, 0, Margin)
//            };
//            mainPanel.Controls.Add(headingLabel);

//            // Back Button
//            backBtn = new Button
//            {
//                Text = "Back",
//                Width = 180,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkViolet,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold),
//                Margin = new Padding(0, Margin, 0, Margin)
//            };
//            backBtn.FlatAppearance.BorderColor = Color.DarkOrchid;
//            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
//            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
//            backBtn.Click += new EventHandler(BackBtn_Click);
//            mainPanel.Controls.Add(backBtn);

//            // DataGridView
//            dataGridView = new DataGridView
//            {
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.Both, // Enable internal horizontal and vertical scrollbars
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
//                    ForeColor = Color.Black,
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.FromArgb(230, 190, 230) // Lighter purple
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.Single,
//                GridColor = Color.FromArgb(200, 162, 200),
//                Margin = new Padding(0, Margin, 0, Margin)
//            };
//            dataGridView.RowTemplate.Height = 35;
//            dataGridView.ColumnHeadersHeight = 45;
//            mainPanel.Controls.Add(dataGridView);

//            // Download Button
//            downloadBtn = new Button
//            {
//                Text = "Download",
//                Width = 180,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold),
//                Margin = new Padding(0, Margin, 0, Margin)
//            };
//            downloadBtn.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadBtn.MouseEnter += (s, e) => downloadBtn.BackColor = Color.FromArgb(186, 85, 211);
//            downloadBtn.MouseLeave += (s, e) => downloadBtn.BackColor = Color.DarkOrchid;
//            downloadBtn.Click += new EventHandler(downloadBtn_Click);
//            mainPanel.Controls.Add(downloadBtn);

//            // Center controls on resize
//            mainPanel.Resize += (s, e) => CenterControls();
//        }

//        private void CenterControls()
//        {
//            headingLabel.Left = (mainPanel.ClientSize.Width - headingLabel.Width) / 2;
//            backBtn.Left = (mainPanel.ClientSize.Width - backBtn.Width) / 2;
//            dataGridView.Left = (mainPanel.ClientSize.Width - dataGridView.Width) / 2;
//            downloadBtn.Left = (mainPanel.ClientSize.Width - downloadBtn.Width) / 2;
//        }

//        private void PopulateDataGridView()
//        {
//            dataGridView.Columns.Clear();

//            // Define columns
//            dataGridView.Columns.Add("StudentId", "Student ID");
//            dataGridView.Columns.Add("StudentName", "Student Name");
//            string[] semesters = { "1-1", "1-2", "2-1", "2-2", "3-1", "3-2", "4-1", "4-2" };
//            foreach (var semester in semesters)
//            {
//                dataGridView.Columns.Add($"Sem_{semester}", $"Sem {semester}");
//            }
//            dataGridView.Columns.Add(option == "GPA" ? "CGPA" : "TotalCredits", option == "GPA" ? "CGPA" : "Total Credits");

//            // Set column widths
//            dataGridView.Columns[0].Width = 180; // Student ID
//            dataGridView.Columns[1].Width = 250; // Student Name
//            for (int i = 2; i < dataGridView.Columns.Count - 1; i++)
//            {
//                dataGridView.Columns[i].Width = 100; // Semesters (increased for readability)
//            }
//            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 110; // CGPA or Total Credits

//            // Populate rows
//            dataGridView.Rows.Clear();
//            foreach (var result in results.OrderBy(r => r.StudentId))
//            {
//                var row = new List<object> { result.StudentId, result.StudentName };
//                foreach (var semester in semesters)
//                {
//                    row.Add(result.Semesters.ContainsKey(semester) ? result.Semesters[semester] : "-");
//                }
//                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
//                dataGridView.Rows.Add(row.ToArray());
//            }

//            // Handle empty data
//            if (dataGridView.Rows.Count == 0)
//            {
//                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "");
//            }

//            // Set DataGridView height dynamically
//            int calculatedHeight = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            dataGridView.Height = Math.Min(calculatedHeight, MaxDataGridViewHeight);

//            // Ensure last row is visible
//            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
//            {
//                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
//            }
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 35;
//            int headerHeight = 45;
//            return headerHeight + rowCount * rowHeight;
//        }

//        private void downloadBtn_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value?.ToString() == "No data available")
//            {
//                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

//                // Report Info
//                worksheet.Cells[1, 1].Value = "Branch";
//                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
//                worksheet.Cells[2, 1].Value = "Academic Year";
//                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
//                worksheet.Cells[3, 1].Value = "Report Type";
//                worksheet.Cells[3, 2].Value = option ?? "Unknown";

//                // Headers
//                int startRow = 5;
//                for (int i = 0; i < dataGridView.Columns.Count; i++)
//                {
//                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].HeaderText;
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
//                    Title = "Save Results Report",
//                    FileName = $"{branch}_{acYear}_{option}_Report.xlsx"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//        }

//        private void BackBtn_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            //parentForm?.Show();
//            admindashboard adb = new admindashboard();
//            adb.Show();
//            try
//            {
//                getbtn child = new getbtn();
//                child.TopLevel = false;
//                child.FormBorderStyle = FormBorderStyle.None;
//                child.Dock = DockStyle.Fill;
//                adb.panelDesktopPane.Controls.Clear();
//                adb.panelDesktopPane.Controls.Add(child);
//                child.Show();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void ResultsForm_gpa_credits_Load(object sender, EventArgs e)
//        {
//            // Ensure last row is visible on load
//            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
//            {
//                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
//            }
//        }
//    }
//}



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
    public partial class ResultsForm_gpa_credits : Form
    {
        private DataGridView dataGridView;
        private Button downloadBtn;
        private Button backBtn;
        private Label headingLabel;
        private Label lblBranch;
        private Label lblAcademicYear;
        private Label lblOption;
        private const int Margin = 20;
        private readonly List<StudentResult> results;
        private readonly string option;
        private readonly string branch;
        private readonly string acYear;
        private readonly getbtn parentForm;

        public ResultsForm_gpa_credits(List<StudentResult> results, string option, string branch, string acYear, getbtn parentForm)
        {
            InitializeComponent();
            this.results = results ?? new List<StudentResult>();
            this.option = option;
            this.branch = branch;
            this.acYear = acYear;
            this.parentForm = parentForm;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            SetupForm();
            PopulateDataGridView();
        }

        private void SetupForm()
        {
            // Form settings
            this.Size = new Size(1000, 600);
            this.Text = $"{option} Results";
            this.AutoScroll = true; // Enable form-level vertical scrollbar
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            // Header Label
            headingLabel = new Label
            {
                Text = $"{option} Results",
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
            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(153, 50, 204);
            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.DarkViolet;
            backBtn.Click += new EventHandler(BackBtn_Click);
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

            lblOption = new Label
            {
                Text = $"Report Type: {option}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(20, 110)
            };
            this.Controls.Add(lblOption);

            // DataGridView
            dataGridView = new DataGridView
            {
                Location = new Point(20, 190),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None, // Use fixed widths for consistency
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.Both, // Enable internal horizontal and vertical scrollbars
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
                    BackColor = Color.FromArgb(245, 240, 255) // Lighter purple
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

            // Define columns
            dataGridView.Columns.Add("StudentId", "Student ID");
            dataGridView.Columns.Add("StudentName", "Student Name");
            string[] semesters = { "1-1", "1-2", "2-1", "2-2", "3-1", "3-2", "4-1", "4-2" };
            foreach (var semester in semesters)
            {
                dataGridView.Columns.Add($"Sem_{semester}", $"Sem {semester}");
            }
            dataGridView.Columns.Add(option == "GPA" ? "CGPA" : "TotalCredits", option == "GPA" ? "CGPA" : "Total Credits");

            // Set column widths
            dataGridView.Columns[0].Width = 180; // Student ID
            dataGridView.Columns[1].Width = 250; // Student Name
            for (int i = 2; i < dataGridView.Columns.Count - 1; i++)
            {
                dataGridView.Columns[i].Width = 100; // Semesters
            }
            dataGridView.Columns[dataGridView.Columns.Count - 1].Width = 110; // CGPA or Total Credits

            // Populate rows
            dataGridView.Rows.Clear();
            foreach (var result in results.OrderBy(r => r.StudentId))
            {
                var row = new List<object> { result.StudentId, result.StudentName };
                foreach (var semester in semesters)
                {
                    row.Add(result.Semesters.ContainsKey(semester) ? result.Semesters[semester] : "-");
                }
                row.Add(option == "GPA" ? result.CGPA : result.TotalCredits);
                dataGridView.Rows.Add(row.ToArray());
            }

            // Handle empty data
            if (dataGridView.Rows.Count == 0)
            {
                dataGridView.Rows.Add("No data available", "", "", "", "", "", "", "", "", "");
            }

            // Ensure last row is visible
            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
            {
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
            }
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0 || dataGridView.Rows[0].Cells[0].Value?.ToString() == "No data available")
            {
                MessageBox.Show("No data to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Student Results Report");

                // Report Info
                worksheet.Cells[1, 1].Value = "Branch";
                worksheet.Cells[1, 2].Value = branch ?? "Unknown";
                worksheet.Cells[2, 1].Value = "Academic Year";
                worksheet.Cells[2, 2].Value = acYear ?? "Unknown";
                worksheet.Cells[3, 1].Value = "Report Type";
                worksheet.Cells[3, 2].Value = option ?? "Unknown";

                // Headers
                int startRow = 5;
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[startRow, i + 1].Value = dataGridView.Columns[i].HeaderText;
                }

                using (var headerRange = worksheet.Cells[startRow, 1, startRow, dataGridView.Columns.Count])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
                    headerRange.Style.Font.Color.SetColor(Color.White);
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[startRow + i + 1, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "Save Results Report",
                    FileName = $"{branch}_{acYear}_{option}_Report.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(file);
                        MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            //parentForm?.Show();
            admindashboard adb = new admindashboard();
            adb.Show();
            try
            {
                getbtn child = new getbtn();
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

        private void ResultsForm_gpa_credits_Load(object sender, EventArgs e)
        {
            // Ensure last row is visible on load
            if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value?.ToString() != "No data available")
            {
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
            }
        }
    }
}