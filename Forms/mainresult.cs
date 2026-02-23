////using Newtonsoft.Json;
////using System;
////using System.Collections.Generic;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using static project_RYS.uploadform;
////using System.Windows.Input;
////using System.IO;
////using project_RYS.Forms;
////using ClosedXML.Excel;
////using DocumentFormat.OpenXml.Vml;

////using System.Drawing.Printing; // Required for printing

////namespace project_RYS
////{
////    public partial class mainresult : Form
////    {
////        private PrintDocument printDocument = new PrintDocument();
////        private string printContent = ""; // Store the content to be printed
////        private string studentId;

////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private const int DataGridViewWidth = 1250;
////        private const int DataGridViewHeight = 784;
////        private const int SmallDataGridViewHeight = 80;
////        private new const int Margin = 20;
////        int baklogstotal = 0;
////        double totalcgpa = 0.0;
////        string name = "";
////        private string stuName = "";

////        //public Label sgpalabel;

////        private List<IGrouping<string, sturesults>> groupedBacklogs; // Grouped by Year-Sem as a single string


////        public mainresult(string studentId)
////        {
////            InitializeComponent();
////            this.studentId = studentId;

////            //LoadBacklogData();
////            this.Load += new EventHandler(Form1_Load);

////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();

////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;

////            //sgpalabel = new Label();  // Initialize sgpalabel only once
////            LoadResult();

////        }
////        // Function to load student names from JSON file and return a dictionary
////        private Dictionary<string, string> LoadStudentNames(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return null;
////            }

////            try
////            {
////                var jsonData = File.ReadAllText(filePath);

////                // Deserialize the JSON array into a list of dynamic objects
////                var studentsList = JsonConvert.DeserializeObject<List<Student>>(jsonData);

////                // Convert the list to a dictionary for easy lookup
////                return studentsList
////                    .Where(student => !string.IsNullOrEmpty(student.studentid)) // Filter valid student IDs
////                    .ToDictionary(student => student.studentid, student => student.name);
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return null;
////            }
////        }

////        // Function to get the student's name based on their ID
////        private string GetStudentName(string studentId)
////        {
////            var studentNames = LoadStudentNames("studentnames.json");

////            if (studentNames != null && studentNames.ContainsKey(studentId))
////            {
////                //Console.WriteLine("bdvuabfiubdjvuyc====" + studentNames[studentId]);
////                return studentNames[studentId]; // Return the student's name
////            }
////            else
////            {
////                return "Unknown"; // Default value if student ID not found
////            }
////        }

////        // Class to represent each student in the JSON file
////        private class Student
////        {
////            public string studentid { get; set; }
////            public string name { get; set; }
////        }



////        private string GetBranchFromStudentId(string studentId)
////        {
////            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
////            studentId = studentId.ToUpper().Trim();
////            if (studentId.Contains("5a66") || studentId.Contains("5A66"))
////                return "CSM";
////            else if (studentId.Contains("1a05") || studentId.Contains("1A05"))
////                return "CSE";
////            else if (studentId.Contains("5a05") || studentId.Contains("5A05"))
////                return "CSE";

////            else if (studentId.Contains("1a66") || studentId.Contains("1A66"))
////                return "CSM";
////            else if (studentId.Contains("1a01") || studentId.Contains("1A01"))
////                return "CE";
////            else if (studentId.Contains("5A01") || studentId.Contains("5a01"))
////                return "CE";
////            else if (studentId.Contains("1a02") || studentId.Contains("1A02"))
////                return "EEE";
////            else if (studentId.Contains("5A02") || studentId.Contains("5a02"))
////                return "EEE";
////            else if (studentId.Contains("1a03") || studentId.Contains("1A03"))
////                return "MECH";
////            else if (studentId.Contains("5A03") || studentId.Contains("5a03"))
////                return "MECH";

////            else if (studentId.Contains("1a25") || studentId.Contains("1A25"))
////                return "ME";
////            else if (studentId.Contains("5A25") || studentId.Contains("5a25"))
////                return "ME";
////            // Add more conditions here for other patterns as needed
////            else
////                return "Unknown"; // Default case if no pattern matches
////        }
////        private st GetStudentById(string studentId)
////        {
////            var students = LoadJsonData<st>("studentMarks.json"); // Assuming you have a 'students.json' file
////            return students.FirstOrDefault(s => s.StudentId == studentId);
////        }

////        private void Form1_Load(object sender, EventArgs e)
////        {
////            // Set the label for total backlogs
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
////            //heading.Text = "result";

////            // Create small DataGridView and add student data
////            var smallDataGridView = CreateSmallDataGridView();
////            if (smallDataGridView != null)
////            {
////                // Fetch student details based on studentId
////                var student = GetStudentById(studentId);
////                //stuName=student.Name;
////                stuName = "";

////                if (student != null)
////                {
////                    // Determine the branch based on studentId
////                    string branch = GetBranchFromStudentId(student.StudentId);

////                    Console.WriteLine("branch ===" + branch);
////                    dataGridViews.Add(smallDataGridView);
////                    this.Controls.Add(smallDataGridView);
////                    smallDataGridView.Rows.Add(GetStudentName(studentId), student.StudentId, branch);
////                }
////                else
////                {
////                    MessageBox.Show("Student not found.");
////                }
////            }

////            ArrangeControls();

////        }
////        private void btnBackToHome_Click(object sender, EventArgs e)
////        { 
////                this.Hide();

////            admindashboard adb = new admindashboard();
////                adb.Show();

////            try
////            {
////                FormResult child = new FormResult();
////                child.TopLevel = false;
////                child.FormBorderStyle = FormBorderStyle.None;
////                child.Dock = DockStyle.Fill;
////                adb.panelDesktopPane.Controls.Clear(); // Clear existing content
////                adb.panelDesktopPane.Controls.Add(child); // Add ChildForm to panel
////                child.Show();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        int w;
////        private void ArrangeControls()
////        {
////            int startY = 100;
////            if (heading == null)
////            {
////                heading = new Label();
////            }
////            Button backbtn = new Button
////            {
////                Text = "Back To Home",
////                Width = 200,
////                Height = 40,
////                Margin = new Padding(10)
////            };
////            backbtn.Location = new Point(10, startY);
////            backbtn.Click += new EventHandler(btnBackToHome_Click);
////            this.Controls.Add(backbtn);
////            int bottomMargin = 50;
////            startY = backbtn.Bottom + Margin;

////            heading.Text = "Result";
////            heading.Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
////            heading.AutoSize = true;
////            heading.Location = new Point((this.ClientSize.Width - heading.Width) / 2, 90);
////            this.Controls.Add(heading);

////            startY = heading.Bottom + Margin;

////            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
////            {
////                var smallDataGridView = dataGridViews[0];
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                w = smallDataGridView.Location.X;
////                startY += smallDataGridView.Height + Margin;
////            }

////            DisplayYearSemDataGrids(ref startY);

////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            this.Controls.Add(lblTotalBacklogs);

////            Button downloadButton = new Button
////            {
////                Text = "Download EXCEL",
////                Width = 200,
////                Height = 40
////            };
////            downloadButton.Location = new Point(lblTotalBacklogs.Right, startY);
////            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
////            this.Controls.Add(downloadButton);
////        }

////        private void btnDownloadPdf_Click(object sender, EventArgs e)
////        {
////            SaveFileDialog saveFileDialog = new SaveFileDialog
////            {
////                Filter = "Excel Workbook|*.xlsx",
////                Title = "Save the Result as Excel File",
////                FileName = "StudentResults.xlsx"
////            };

////            if (saveFileDialog.ShowDialog() == DialogResult.OK)
////            {
////                using (var workbook = new XLWorkbook())
////                {
////                    var worksheet = workbook.Worksheets.Add("Student Results");

////                    // Add student details
////                    worksheet.Cell(1, 1).Value = "Student Details";
////                    worksheet.Cell(2, 1).Value = "Name:";
////                    worksheet.Cell(2, 2).Value = GetStudentName(studentId);  // Replace with actual student name
////                    worksheet.Cell(3, 1).Value = "Roll Number:";
////                    worksheet.Cell(3, 2).Value = studentId;  // Replace with actual roll number
////                    worksheet.Cell(4, 1).Value = "CGPA:";
////                    worksheet.Cell(4, 2).Value = totalcgpa;  // Replace with actual CGPA value
////                    worksheet.Cell(5, 1).Value = "Total Backlogs:";
////                    worksheet.Cell(5, 2).Value = baklogstotal;  // Replace with total backlogs count
////                    Console.WriteLine(name);
////                    // Formatting for student details section (optional)
////                    worksheet.Range("A1:B1").Merge().Style.Font.SetBold().Font.FontSize = 14;
////                    worksheet.Range("A2:A5").Style.Font.SetBold();

////                    // Add space between sections
////                    int currentRow = 7;

////                    // Header row for backlogs data
////                    worksheet.Cell(currentRow, 1).Value = "Subject Code";
////                    worksheet.Cell(currentRow, 2).Value = "Subject Name";
////                    worksheet.Cell(currentRow, 3).Value = "Internal";
////                    worksheet.Cell(currentRow, 4).Value = "External";
////                    worksheet.Cell(currentRow, 5).Value = "Total";
////                    worksheet.Cell(currentRow, 6).Value = "Grade";
////                    worksheet.Cell(currentRow, 7).Value = "GradePoints";
////                    worksheet.Cell(currentRow, 8).Value = "Credits";

////                    worksheet.Row(currentRow).Style.Font.SetBold();
////                    currentRow++;

////                    // Populate data (using 'groupedBacklogs' as an example)
////                    foreach (var group in groupedBacklogs)
////                    {
////                        foreach (var backlog in group)
////                        {
////                            worksheet.Cell(currentRow, 1).Value = backlog.SubjectCode;
////                            worksheet.Cell(currentRow, 2).Value = backlog.SubjectName;
////                            worksheet.Cell(currentRow, 3).Value = backlog.Internal;
////                            worksheet.Cell(currentRow, 4).Value = backlog.External;
////                            worksheet.Cell(currentRow, 5).Value = backlog.Total;
////                            worksheet.Cell(currentRow, 6).Value = backlog.Grade;
////                            worksheet.Cell(currentRow, 7).Value=backlog.GradePoints;
////                            worksheet.Cell(currentRow, 8).Value = backlog.Credits;
////                            currentRow++;
////                        }
////                    }

////                    // Optional: Adjust column widths for better visibility
////                    worksheet.Columns().AdjustToContents();

////                    // Save the file
////                    workbook.SaveAs(saveFileDialog.FileName);
////                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                }
////            }
////        }
////        private void DisplayYearSemDataGrids(ref int startY)
////        {
////            double totalSgpaSum = 0;
////            int sgpaCount = 0;

////            foreach (var group in groupedBacklogs)
////            {
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Result",
////                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                var backlogDataGridView = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = DataGridViewWidth,
////                    Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
////                    AutoGenerateColumns = false,
////                    ColumnCount = 8,
////                    RowHeadersVisible = false,
////                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None, // Manual width control
////                    AllowUserToAddRows = false,
////                    ScrollBars = ScrollBars.None,
////                    ReadOnly = true,
////                    AllowUserToResizeColumns = false,
////                    AllowUserToResizeRows = false,
////                };

////                backlogDataGridView.RowTemplate.Height = 50;
////                backlogDataGridView.ColumnHeadersHeight = 40;

////                backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                };
////                backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                };
////                backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkViolet
////                };
////                backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
////                backlogDataGridView.GridColor = Color.White;

////                backlogDataGridView.Columns[0].Name = "Subject Code";
////                backlogDataGridView.Columns[1].Name = "Subject Name";
////                backlogDataGridView.Columns[2].Name = "Internal";
////                backlogDataGridView.Columns[3].Name = "External";
////                backlogDataGridView.Columns[4].Name = "Total";
////                backlogDataGridView.Columns[5].Name = "Grade";
////                backlogDataGridView.Columns[6].Name = "GradePoints";
////                backlogDataGridView.Columns[7].Name = "Credits";

////                // Set column widths
////                backlogDataGridView.Columns[0].Width = 150; // Subject Code
////                backlogDataGridView.Columns[1].Width = 500; // Subject Name (wide)
////                backlogDataGridView.Columns[2].Width = 90;  // Internal
////                backlogDataGridView.Columns[3].Width = 90;  // External
////                backlogDataGridView.Columns[4].Width = 90;  // Total
////                backlogDataGridView.Columns[5].Width = 90;  // Grade
////                backlogDataGridView.Columns[6].Width = 120;  // GradePoints
////                backlogDataGridView.Columns[7].Width = 120;  // Credits

////                double totalGradePoints = 0;
////                double totalCredits = 0;

////                foreach (var backlog in group)
////                {
////                    backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.GradePoints, backlog.Credits);
////                    double subjectCredits = double.TryParse(backlog.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
////                    double gradePoint = double.TryParse(backlog.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;

////                    totalGradePoints += gradePoint * subjectCredits;
////                    totalCredits += subjectCredits;
////                    name = backlog.SubjectName;
////                    Console.WriteLine(name);
////                }

////                backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());
////                if (!this.Controls.Contains(backlogDataGridView))
////                {
////                    this.Controls.Add(backlogDataGridView);
////                }
////                startY += backlogDataGridView.Height + Margin;

////                double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
////                Console.Write("sgpa===",sgpa);
////                totalSgpaSum += sgpa;
////                sgpaCount++;

////                var sgpaLabel = new Label
////                {
////                    Text = $"SGPA: {sgpa:F2}",
////                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true
////                };
////                sgpaLabel.Location = new Point((this.ClientSize.Width - sgpaLabel.Width) / 2, startY);
////                this.Controls.Add(sgpaLabel);
////                startY += sgpaLabel.Height + Margin;
////            }

////            double cgpa = sgpaCount > 0 ? totalSgpaSum / sgpaCount : 0;
////            totalcgpa = cgpa;
////            var cgpaLabel = new Label
////            {
////                Text = $"CGPA: {cgpa:F2}",
////                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
////                AutoSize = true
////            };
////            cgpaLabel.Location = new Point((this.ClientSize.Width - cgpaLabel.Width) / 2, startY);
////            this.Controls.Add(cgpaLabel);
////            startY += cgpaLabel.Height + Margin;
////        }


////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 50; // Set the row height, adjust this if needed
////            int headerHeight = 40; // Set header height, adjust if needed
////            int totalHeight = headerHeight + rowCount * rowHeight;
////            return totalHeight;
////        }

////        private DataGridView CreateSmallDataGridView()
////        {
////            var dataGridView = new DataGridView
////            {
////                EnableHeadersVisualStyles = false,
////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
////                ReadOnly = true,
////                AllowUserToResizeColumns = false, // Prevents resizing columns
////                AllowUserToResizeRows = false,     // Prevents resizing rows
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";

////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }



////        private void LoadResult()
////        {
////            // Load JSON data
////            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
////            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");

////            // Join data for all records of the specified student ID
////            var backlogs = studentMarks
////                .Where(m => m.StudentId == studentId) // No grade filtering, fetch all records for the student
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new sturesults
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,  // Storing as string
////                        External = mark.External,  // Storing as string
////                        Total = mark.Total,  // Storing as string
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,  // Storing as string
////                        YearSem = $"{subject.Year}-{subject.Semester}",  // Create Year-Sem string
////                                                                         //GradePoint = GetGradePoint(mark.Grade).ToString()  // Grade Point as string for display
////                        GradePoints = mark.GradePoints,
////                        SubjectCredits = subject.Credits  // Add Credits from subjectCodes for calculations
////                    })

////                .ToList();

////            baklogstotal = 0;
////            double totalCredits = 0;
////            double totalGradePoints = 0;
////            foreach (var result in backlogs)
////            {
////                if (result.Grade == "F" || result.Grade == "Ab")
////                {
////                    baklogstotal++;
////                }
////                double gradePoint = double.TryParse(result.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;
////                double subjectCredits = double.TryParse(result.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
////                totalGradePoints += gradePoint * subjectCredits;
////                totalCredits += subjectCredits;
////            }

////            Console.WriteLine("jb osdvsc isuih nspiuhcn");
////            Console.WriteLine("total-Credits=" + totalCredits);
////            Console.WriteLine("total-gradepoints=" + totalGradePoints);
////            Console.WriteLine("jb osdvsc isuih nspiuhcn");

////            lblTotalBacklogs.Text = baklogstotal.ToString();
////            double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
////            Console.WriteLine(sgpa);
////            string s = sgpa.ToString("F2");
////            Console.WriteLine(s);

////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key)
////                .ToList();
////        }

////        // Method to load JSON data from file
////        private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}");
////                return new List<T>();
////            }

////            var json = File.ReadAllText(filePath);
////            return JsonConvert.DeserializeObject<List<T>>(json);
////        }

////        private void print_Click(object sender, EventArgs e)
////        {

////        }

////    }


////    public class sturesults
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }  // Storing as string
////        public string External { get; set; }  // Storing as string
////        public string Total { get; set; }  // Storing as string
////        public string Grade { get; set; }
////        public string Credits { get; set; }  // Storing as string
////        public string YearSem { get; set; }
////        public string GradePoints { get; set; }  // Grade Point as string for display
////        public string SubjectCredits { get; set; }  // From subjectCodes (attempted)
////    }

////    // Student marks data structure
////    public class StudentMark
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string GradePoints { get; set; }
////        public string Credits { get; set; }
////    }

////    // Subject data structure
////    public class subj
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }

////        public string Credits { get; set; }
////    }
////    public class st
////    {
////        public string StudentId { get; set; }
////        public string Name { get; set; }

////    }

////}

//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using static project_RYS.uploadform;
//using System.Windows.Input;
//using System.IO;
//using project_RYS.Forms;
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Vml;
//using System.Drawing.Printing; // Required for printing

//namespace project_RYS
//{
//    public partial class mainresult : Form
//    {
//        private PrintDocument printDocument = new PrintDocument();
//        private string printContent = ""; // Store the content to be printed
//        private string studentId;

//        private List<DataGridView> dataGridViews;
//        private List<Label> labels;
//        private const int DataGridViewWidth = 1250;
//        private const int DataGridViewHeight = 784;
//        private const int SmallDataGridViewHeight = 80;
//        private new const int Margin = 20;
//        int baklogstotal = 0;
//        double totalcgpa = 0.0;
//        string name = "";
//        private string stuName = "";

//        private List<IGrouping<string, sturesults>> groupedBacklogs; // Grouped by Year-Sem as a single string

//        public mainresult(string studentId)
//        {
//            Console.WriteLine($"[Constructor] Initializing mainresult form with studentId: {studentId}");
//            InitializeComponent();
//            this.studentId = studentId;
//            Console.WriteLine($"[Constructor] Assigned studentId: {this.studentId}");

//            this.Load += new EventHandler(Form1_Load);
//            //Console.WriteLine("[Constructor] Added Form1_Load event handler");

//            dataGridViews = new List<DataGridView>();
//            labels = new List<Label>();
//            //Console.WriteLine("[Constructor] Initialized dataGridViews and labels lists");

//            this.Size = new Size(1300, 800);
//            this.AutoScroll = true;
//            //Console.WriteLine("[Constructor] Set form size to 1300x800 and enabled AutoScroll");

//            LoadResult();
//            //Console.WriteLine("[Constructor] Called LoadResult");
//        }

//        private Dictionary<string, string> LoadStudentNames(string filePath)
//        {
//            Console.WriteLine($"[LoadStudentNames] Loading student names from file: {filePath}");
//            if (!File.Exists(filePath))
//            {
//                Console.WriteLine($"[LoadStudentNames] File not found: {filePath}");
//                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return null;
//            }

//            try
//            {
//                //Console.WriteLine("[LoadStudentNames] Reading file content");
//                var jsonData = File.ReadAllText(filePath);
//                //Console.WriteLine($"[LoadStudentNames] File content read, length: {jsonData.Length}");

//                //Console.WriteLine("[LoadStudentNames] Deserializing JSON data");
//                var studentsList = JsonConvert.DeserializeObject<List<Student>>(jsonData);
//                //Console.WriteLine($"[LoadStudentNames] Deserialized {studentsList.Count} students");

//                //Console.WriteLine("[LoadStudentNames] Converting to dictionary");
//                var result = studentsList
//                    .Where(student => !string.IsNullOrEmpty(student.studentid))
//                    .ToDictionary(student => student.studentid, student => student.name);
//                //Console.WriteLine($"[LoadStudentNames] Created dictionary with {result.Count} entries");
//                return result;
//            }
//            catch (Exception ex)
//            {
//                //Console.WriteLine($"[LoadStudentNames] Error: {ex.Message}");
//                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return null;
//            }
//        }

//        private string GetStudentName(string studentId)
//        {
//            //Console.WriteLine($"[GetStudentName] Getting name for studentId: {studentId}");
//            var studentNames = LoadStudentNames("studentnames.json");
//            //Console.WriteLine($"[GetStudentName] Loaded student names dictionary, count: {studentNames?.Count ?? 0}");

//            if (studentNames != null && studentNames.ContainsKey(studentId))
//            {
//                var name = studentNames[studentId];
//                //Console.WriteLine($"[GetStudentName] Found name: {name} for studentId: {studentId}");
//                return name;
//            }
//            else
//            {
//                //Console.WriteLine($"[GetStudentName] StudentId {studentId} not found or dictionary is null");
//                return "Unknown";
//            }
//        }

//        private class Student
//        {
//            public string studentid { get; set; }
//            public string name { get; set; }
//        }

//        private string GetBranchFromStudentId(string studentId)
//        {
//            //Console.WriteLine($"[GetBranchFromStudentId] Processing studentId: {studentId}");
//            if (string.IsNullOrWhiteSpace(studentId))
//            {
//                //Console.WriteLine("[GetBranchFromStudentId] StudentId is empty or whitespace");
//                return "Unknown";
//            }

//            studentId = studentId.ToUpper().Trim();
//            //Console.WriteLine($"[GetBranchFromStudentId] Normalized studentId: {studentId}");

//            if (studentId.Contains("5A66") || studentId.Contains("1A66"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: CSM");
//                return "CSM";
//            }
//            else if (studentId.Contains("1A05") || studentId.Contains("5A05"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: CSE");
//                return "CSE";
//            }
//            else if (studentId.Contains("1A01") || studentId.Contains("5A01"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: CE");
//                return "CE";
//            }
//            else if (studentId.Contains("1A02") || studentId.Contains("5A02"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: EEE");
//                return "EEE";
//            }
//            else if (studentId.Contains("1A03") || studentId.Contains("5A03"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: MECH");
//                return "MECH";
//            }
//            else if (studentId.Contains("1A25") || studentId.Contains("5A25"))
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: ME");
//                return "ME";
//            }
//            else
//            {
//                Console.WriteLine("[GetBranchFromStudentId] Branch: Unknown");
//                return "Unknown";
//            }
//        }

//        private st GetStudentById(string studentId)
//        {
//            //Console.WriteLine($"[GetStudentById] Fetching student with ID: {studentId}");
//            var students = LoadJsonData<st>("studentMarks.json");
//            Console.WriteLine($"[GetStudentById] Loaded {students.Count} students");
//            var student = students.FirstOrDefault(s => s.StudentId == studentId);
//            //Console.WriteLine($"[GetStudentById] Found student: {(student != null ? student.StudentId : "null")}");
//            return student;
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {
//            //Console.WriteLine("[Form1_Load] Form loading started");
//            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
//            Console.WriteLine($"[Form1_Load] Set lblTotalBacklogs to: Total Backlogs: {baklogstotal}");

//            var smallDataGridView = CreateSmallDataGridView();
//            //Console.WriteLine($"[Form1_Load] Created smallDataGridView: {(smallDataGridView != null ? "Success" : "Null")}");

//            if (smallDataGridView != null)
//            {
//                var student = GetStudentById(studentId);
//                //Console.WriteLine($"[Form1_Load] Retrieved student: {(student != null ? student.StudentId : "null")}");

//                if (student != null)
//                {
//                    string branch = GetBranchFromStudentId(student.StudentId);
//                    //Console.WriteLine($"[Form1_Load] Determined branch: {branch}");

//                    dataGridViews.Add(smallDataGridView);
//                    //Console.WriteLine("[Form1_Load] Added smallDataGridView to dataGridViews list");

//                    this.Controls.Add(smallDataGridView);
//                    //Console.WriteLine("[Form1_Load] Added smallDataGridView to form controls");

//                    var studentName = GetStudentName(studentId);
//                    smallDataGridView.Rows.Add(studentName, student.StudentId, branch);
//                    //Console.WriteLine($"[Form1_Load] Added row to smallDataGridView: Name={studentName}, ID={student.StudentId}, Branch={branch}");
//                }
//                else
//                {
//                    //Console.WriteLine("[Form1_Load] Student not found");
//                    MessageBox.Show("Student not found.");
//                }
//            }

//            ArrangeControls();
//            //Console.WriteLine("[Form1_Load] Called ArrangeControls");
//        }

//        private void btnBackToHome_Click(object sender, EventArgs e)
//        {
//            //Console.WriteLine("[btnBackToHome_Click] Back to home button clicked");
//            this.Hide();
//            //Console.WriteLine("[btnBackToHome_Click] Hid current form");

//            admindashboard adb = new admindashboard();
//            //Console.WriteLine("[btnBackToHome_Click] Created new admindashboard instance");
//            adb.Show();
//            //Console.WriteLine("[btnBackToHome_Click] Showed admindashboard");

//            try
//            {
//                //Console.WriteLine("[btnBackToHome_Click] Creating new FormResult");
//                FormResult child = new FormResult();
//                child.TopLevel = false;
//                child.FormBorderStyle = FormBorderStyle.None;
//                child.Dock = DockStyle.Fill;
//                //Console.WriteLine("[btnBackToHome_Click] Configured FormResult properties");

//                adb.panelDesktopPane.Controls.Clear();
//                //Console.WriteLine("[btnBackToHome_Click] Cleared panelDesktopPane controls");

//                adb.panelDesktopPane.Controls.Add(child);
//                //Console.WriteLine("[btnBackToHome_Click] Added FormResult to panelDesktopPane");

//                child.Show();
//                //Console.WriteLine("[btnBackToHome_Click] Showed FormResult");
//            }
//            catch (Exception ex)
//            {
//                //Console.WriteLine($"[btnBackToHome_Click] Error: {ex.Message}");
//                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        int w;
//        private void ArrangeControls()
//        {
//            //Console.WriteLine("[ArrangeControls] Starting control arrangement");
//            int startY = 100;
//            //Console.WriteLine($"[ArrangeControls] Initial startY: {startY}");

//            if (heading == null)
//            {
//                heading = new Label();
//                //Console.WriteLine("[ArrangeControls] Created new heading label");
//            }

//            Button backbtn = new Button
//            {
//                Text = "Back To Home",
//                Width = 200,
//                Height = 40,
//                Margin = new Padding(10)
//            };
//            backbtn.Location = new Point(10, startY);
//            backbtn.Click += new EventHandler(btnBackToHome_Click);
//            this.Controls.Add(backbtn);
//            //Console.WriteLine($"[ArrangeControls] Added back button at location: ({backbtn.Location.X}, {backbtn.Location.Y})");

//            int bottomMargin = 50;
//            startY = backbtn.Bottom + Margin;
//            //Console.WriteLine($"[ArrangeControls] Updated startY after back button: {startY}");

//            heading.Text = "Result";
//            heading.Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
//            heading.AutoSize = true;
//            heading.Location = new Point((this.ClientSize.Width - heading.Width) / 2, 90);
//            this.Controls.Add(heading);
//            //Console.WriteLine($"[ArrangeControls] Added heading at location: ({heading.Location.X}, {heading.Location.Y})");

//            startY = heading.Bottom + Margin;
//            //Console.WriteLine($"[ArrangeControls] Updated startY after heading: {startY}");

//            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
//            {
//                var smallDataGridView = dataGridViews[0];
//                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//                w = smallDataGridView.Location.X;
//                startY += smallDataGridView.Height + Margin;
//                //Console.WriteLine($"[ArrangeControls] Positioned smallDataGridView at: ({smallDataGridView.Location.X}, {smallDataGridView.Location.Y}), Updated startY: {startY}");
//            }

//            DisplayYearSemDataGrids(ref startY);
//            Console.WriteLine("[ArrangeControls] Called DisplayYearSemDataGrids");

//            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
//            this.Controls.Add(lblTotalBacklogs);
//            Console.WriteLine($"[ArrangeControls] Added lblTotalBacklogs at: ({lblTotalBacklogs.Location.X}, {lblTotalBacklogs.Location.Y})");

//            Button downloadButton = new Button
//            {
//                Text = "Download EXCEL",
//                Width = 200,
//                Height = 40
//            };
//            downloadButton.Location = new Point(lblTotalBacklogs.Right, startY);
//            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
//            this.Controls.Add(downloadButton);
//            Console.WriteLine($"[ArrangeControls] Added downloadButton at: ({downloadButton.Location.X}, {downloadButton.Location.Y})");
//        }

//        private void btnDownloadPdf_Click(object sender, EventArgs e)
//        {
//            Console.WriteLine("[btnDownloadPdf_Click] Download Excel button clicked");
//            SaveFileDialog saveFileDialog = new SaveFileDialog
//            {
//                Filter = "Excel Workbook|*.xlsx",
//                Title = "Save the Result as Excel File",
//                FileName = "StudentResults.xlsx"
//            };
//            Console.WriteLine("[btnDownloadPdf_Click] Initialized SaveFileDialog");

//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                Console.WriteLine($"[btnDownloadPdf_Click] Save dialog confirmed, saving to: {saveFileDialog.FileName}");
//                using (var workbook = new XLWorkbook())
//                {
//                    Console.WriteLine("[btnDownloadPdf_Click] Created new XLWorkbook");
//                    var worksheet = workbook.Worksheets.Add("Student Results");
//                    Console.WriteLine("[btnDownloadPdf_Click] Added worksheet: Student Results");

//                    worksheet.Cell(1, 1).Value = "Student Details";
//                    worksheet.Cell(2, 1).Value = "Name:";
//                    worksheet.Cell(2, 2).Value = GetStudentName(studentId);
//                    worksheet.Cell(3, 1).Value = "Roll Number:";
//                    worksheet.Cell(3, 2).Value = studentId;
//                    worksheet.Cell(4, 1).Value = "CGPA:";
//                    worksheet.Cell(4, 2).Value = totalcgpa;
//                    worksheet.Cell(5, 1).Value = "Total Backlogs:";
//                    worksheet.Cell(5, 2).Value = baklogstotal;
//                    Console.WriteLine($"[btnDownloadPdf_Click] Added student details: Name={GetStudentName(studentId)}, ID={studentId}, CGPA={totalcgpa}, Backlogs={baklogstotal}");

//                    worksheet.Range("A1:B1").Merge().Style.Font.SetBold().Font.FontSize = 14;
//                    worksheet.Range("A2:A5").Style.Font.SetBold();
//                    Console.WriteLine("[btnDownloadPdf_Click] Applied formatting to student details");

//                    int currentRow = 7;
//                    Console.WriteLine($"[btnDownloadPdf_Click] Set initial currentRow: {currentRow}");

//                    worksheet.Cell(currentRow, 1).Value = "Subject Code";
//                    worksheet.Cell(currentRow, 2).Value = "Subject Name";
//                    worksheet.Cell(currentRow, 3).Value = "Internal";
//                    worksheet.Cell(currentRow, 4).Value = "External";
//                    worksheet.Cell(currentRow, 5).Value = "Total";
//                    worksheet.Cell(currentRow, 6).Value = "Grade";
//                    worksheet.Cell(currentRow, 7).Value = "GradePoints";
//                    worksheet.Cell(currentRow, 8).Value = "Credits";
//                    worksheet.Row(currentRow).Style.Font.SetBold();
//                    Console.WriteLine("[btnDownloadPdf_Click] Added header row");
//                    currentRow++;
//                    Console.WriteLine($"[btnDownloadPdf_Click] Incremented currentRow to: {currentRow}");

//                    foreach (var group in groupedBacklogs)
//                    {
//                        Console.WriteLine($"[btnDownloadPdf_Click] Processing group: {group.Key}");
//                        foreach (var backlog in group)
//                        {
//                            worksheet.Cell(currentRow, 1).Value = backlog.SubjectCode;
//                            worksheet.Cell(currentRow, 2).Value = backlog.SubjectName;
//                            worksheet.Cell(currentRow, 3).Value = backlog.Internal;
//                            worksheet.Cell(currentRow, 4).Value = backlog.External;
//                            worksheet.Cell(currentRow, 5).Value = backlog.Total;
//                            worksheet.Cell(currentRow, 6).Value = backlog.Grade;
//                            worksheet.Cell(currentRow, 7).Value = backlog.GradePoints;
//                            worksheet.Cell(currentRow, 8).Value = backlog.Credits;
//                            Console.WriteLine($"[btnDownloadPdf_Click] Added row: SubjectCode={backlog.SubjectCode}, SubjectName={backlog.SubjectName}");
//                            currentRow++;
//                            Console.WriteLine($"[btnDownloadPdf_Click] Incremented currentRow to: {currentRow}");
//                        }
//                    }

//                    worksheet.Columns().AdjustToContents();
//                    Console.WriteLine("[btnDownloadPdf_Click] Adjusted column widths");

//                    workbook.SaveAs(saveFileDialog.FileName);
//                    Console.WriteLine($"[btnDownloadPdf_Click] Saved workbook to: {saveFileDialog.FileName}");
//                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            else
//            {
//                Console.WriteLine("[btnDownloadPdf_Click] Save dialog cancelled");
//            }
//        }

//        private void DisplayYearSemDataGrids(ref int startY)
//        {
//            Console.WriteLine("[DisplayYearSemDataGrids] Starting display of year-semester data grids");
//            double totalSgpaSum = 0;
//            int sgpaCount = 0;
//            Console.WriteLine($"[DisplayYearSemDataGrids] Initialized totalSgpaSum={totalSgpaSum}, sgpaCount={sgpaCount}");

//            foreach (var group in groupedBacklogs)
//            {
//                Console.WriteLine($"[DisplayYearSemDataGrids] Processing group: {group.Key}");
//                var backlogLabel = new Label
//                {
//                    Text = $"{group.Key} Result",
//                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
//                    AutoSize = true
//                };
//                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
//                this.Controls.Add(backlogLabel);
//                Console.WriteLine($"[DisplayYearSemDataGrids] Added backlogLabel for {group.Key} at: ({backlogLabel.Location.X}, {backlogLabel.Location.Y})");
//                startY += backlogLabel.Height + Margin;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Updated startY: {startY}");

//                var backlogDataGridView = new DataGridView
//                {
//                    Name = $"dgv_{group.Key}",
//                    Width = DataGridViewWidth,
//                    Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
//                    AutoGenerateColumns = false,
//                    ColumnCount = 8,
//                    RowHeadersVisible = false,
//                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                    AllowUserToAddRows = false,
//                    ScrollBars = ScrollBars.None,
//                    ReadOnly = true,
//                    AllowUserToResizeColumns = false,
//                    AllowUserToResizeRows = false,
//                };
//                Console.WriteLine($"[DisplayYearSemDataGrids] Created DataGridView for {group.Key}");

//                backlogDataGridView.RowTemplate.Height = 50;
//                backlogDataGridView.ColumnHeadersHeight = 40;
//                backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                };
//                backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//                };
//                backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkViolet
//                };
//                backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
//                backlogDataGridView.GridColor = Color.White;
//                Console.WriteLine("[DisplayYearSemDataGrids] Configured DataGridView styles");

//                backlogDataGridView.Columns[0].Name = "Subject Code";
//                backlogDataGridView.Columns[1].Name = "Subject Name";
//                backlogDataGridView.Columns[2].Name = "Internal";
//                backlogDataGridView.Columns[3].Name = "External";
//                backlogDataGridView.Columns[4].Name = "Total";
//                backlogDataGridView.Columns[5].Name = "Grade";
//                backlogDataGridView.Columns[6].Name = "GradePoints";
//                backlogDataGridView.Columns[7].Name = "Credits";

//                backlogDataGridView.Columns[0].Width = 150;
//                backlogDataGridView.Columns[1].Width = 500;
//                backlogDataGridView.Columns[2].Width = 90;
//                backlogDataGridView.Columns[3].Width = 90;
//                backlogDataGridView.Columns[4].Width = 90;
//                backlogDataGridView.Columns[5].Width = 90;
//                backlogDataGridView.Columns[6].Width = 120;
//                backlogDataGridView.Columns[7].Width = 120;
//                Console.WriteLine("[DisplayYearSemDataGrids] Set column widths");

//                double totalGradePoints = 0;
//                double totalCredits = 0;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Initialized totalGradePoints={totalGradePoints}, totalCredits={totalCredits} for {group.Key}");

//                foreach (var backlog in group)
//                {
//                    backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.GradePoints, backlog.Credits);
//                    Console.WriteLine($"[DisplayYearSemDataGrids] Added row: SubjectCode={backlog.SubjectCode}, SubjectName={backlog.SubjectName}");

//                    double subjectCredits = double.TryParse(backlog.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
//                    double gradePoint = double.TryParse(backlog.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;
//                    Console.WriteLine($"[DisplayYearSemDataGrids] Parsed: SubjectCredits={subjectCredits}, GradePoint={gradePoint}");

//                    totalGradePoints += gradePoint * subjectCredits;
//                    totalCredits += subjectCredits;
//                    Console.WriteLine($"[DisplayYearSemDataGrids] Updated: totalGradePoints={totalGradePoints}, totalCredits={totalCredits}");

//                    name = backlog.SubjectName;
//                    Console.WriteLine($"[DisplayYearSemDataGrids] Set name: {name}");
//                }

//                backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());
//                Console.WriteLine($"[DisplayYearSemDataGrids] Set DataGridView height: {backlogDataGridView.Height}");

//                if (!this.Controls.Contains(backlogDataGridView))
//                {
//                    this.Controls.Add(backlogDataGridView);
//                    Console.WriteLine($"[DisplayYearSemDataGrids] Added DataGridView to form controls at: ({backlogDataGridView.Location.X}, {backlogDataGridView.Location.Y})");
//                }
//                startY += backlogDataGridView.Height + Margin;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Updated startY: {startY}");

//                double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Calculated SGPA: {sgpa:F2}");
//                totalSgpaSum += sgpa;
//                sgpaCount++;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Updated: totalSgpaSum={totalSgpaSum}, sgpaCount={sgpaCount}");

//                var sgpaLabel = new Label
//                {
//                    Text = $"SGPA: {sgpa:F2}",
//                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
//                    AutoSize = true
//                };
//                sgpaLabel.Location = new Point((this.ClientSize.Width - sgpaLabel.Width) / 2, startY);
//                this.Controls.Add(sgpaLabel);
//                Console.WriteLine($"[DisplayYearSemDataGrids] Added SGPA label: {sgpaLabel.Text} at: ({sgpaLabel.Location.X}, {sgpaLabel.Location.Y})");
//                startY += sgpaLabel.Height + Margin;
//                Console.WriteLine($"[DisplayYearSemDataGrids] Updated startY: {startY}");
//            }

//            double cgpa = sgpaCount > 0 ? totalSgpaSum / sgpaCount : 0;
//            Console.WriteLine($"[DisplayYearSemDataGrids] Calculated CGPA: {cgpa:F2}");
//            totalcgpa = cgpa;
//            Console.WriteLine($"[DisplayYearSemDataGrids] Set totalcgpa: {totalcgpa}");

//            var cgpaLabel = new Label
//            {
//                Text = $"CGPA: {cgpa:F2}",
//                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
//                AutoSize = true
//            };
//            cgpaLabel.Location = new Point((this.ClientSize.Width - cgpaLabel.Width) / 2, startY);
//            this.Controls.Add(cgpaLabel);
//            Console.WriteLine($"[DisplayYearSemDataGrids] Added CGPA label: {cgpaLabel.Text} at: ({cgpaLabel.Location.X}, {cgpaLabel.Location.Y})");
//            startY += cgpaLabel.Height + Margin;
//            Console.WriteLine($"[DisplayYearSemDataGrids] Final startY: {startY}");
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            Console.WriteLine($"[CalculateDataGridViewHeight] Calculating height for {rowCount} rows");
//            int rowHeight = 50;
//            int headerHeight = 40;
//            int totalHeight = headerHeight + rowCount * rowHeight;
//            Console.WriteLine($"[CalculateDataGridViewHeight] rowHeight={rowHeight}, headerHeight={headerHeight}, totalHeight={totalHeight}");
//            return totalHeight;
//        }

//        private DataGridView CreateSmallDataGridView()
//        {
//            Console.WriteLine("[CreateSmallDataGridView] Creating small DataGridView");
//            var dataGridView = new DataGridView
//            {
//                EnableHeadersVisualStyles = false,
//                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                },
//                ColumnHeadersHeight = 40,
//                RowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkViolet
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.None,
//                ReadOnly = true,
//                AllowUserToResizeColumns = false,
//                AllowUserToResizeRows = false,
//            };
//            Console.WriteLine("[CreateSmallDataGridView] Configured DataGridView properties");

//            dataGridView.RowTemplate.Height = 50;
//            dataGridView.ColumnCount = 3;
//            dataGridView.Columns[0].Name = "STUDENT NAME";
//            dataGridView.Columns[1].Name = "ROLL NO";
//            dataGridView.Columns[2].Name = "BRANCH";
//            Console.WriteLine("[CreateSmallDataGridView] Set column names and row height");

//            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
//            Console.WriteLine($"[CreateSmallDataGridView] Set size: ({DataGridViewWidth}, {SmallDataGridViewHeight})");
//            return dataGridView;
//        }

//        private void LoadResult()
//        {
//            Console.WriteLine("[LoadResult] Starting to load results");
//            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
//            Console.WriteLine($"[LoadResult] Loaded {studentMarks.Count} student marks");
//            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");
//            Console.WriteLine($"[LoadResult] Loaded {subjectCodes.Count} subject codes");

//            var backlogs = studentMarks
//                .Where(m => m.StudentId == studentId)
//                .Join(subjectCodes,
//                    mark => mark.SubjectCode,
//                    subject => subject.SubjectCode,
//                    (mark, subject) => new sturesults
//                    {
//                        SubjectCode = mark.SubjectCode,
//                        SubjectName = mark.SubjectName,
//                        Internal = mark.Internal,
//                        External = mark.External,
//                        Total = mark.Total,
//                        Grade = mark.Grade,
//                        Credits = mark.Credits,
//                        YearSem = $"{subject.Year}-{subject.Semester}",
//                        GradePoints = mark.GradePoints,
//                        SubjectCredits = subject.Credits
//                    })
//                .ToList();
//            Console.WriteLine($"[LoadResult] Created {backlogs.Count} backlog records for studentId: {studentId}");

//            baklogstotal = 0;
//            double totalCredits = 0;
//            double totalGradePoints = 0;
//            Console.WriteLine($"[LoadResult] Initialized: baklogstotal={baklogstotal}, totalCredits={totalCredits}, totalGradePoints={totalGradePoints}");

//            foreach (var result in backlogs)
//            {
//                Console.WriteLine($"[LoadResult] Processing result: SubjectCode={result.SubjectCode}, Grade={result.Grade}");
//                if (result.Grade == "F" || result.Grade == "Ab")
//                {
//                    baklogstotal++;
//                    Console.WriteLine($"[LoadResult] Incremented baklogstotal to: {baklogstotal} (Grade: {result.Grade})");
//                }
//                double gradePoint = double.TryParse(result.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;
//                double subjectCredits = double.TryParse(result.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
//                Console.WriteLine($"[LoadResult] Parsed: GradePoint={gradePoint}, SubjectCredits={subjectCredits}");

//                totalGradePoints += gradePoint * subjectCredits;
//                totalCredits += subjectCredits;
//                Console.WriteLine($"[LoadResult] Updated: totalGradePoints={totalGradePoints}, totalCredits={totalCredits}");
//            }

//            Console.WriteLine("[LoadResult] Backlog calculation complete");
//            Console.WriteLine($"[LoadResult] Final totalCredits={totalCredits}");
//            Console.WriteLine($"[LoadResult] Final totalGradePoints={totalGradePoints}");

//            lblTotalBacklogs.Text = baklogstotal.ToString();
//            Console.WriteLine($"[LoadResult] Set lblTotalBacklogs to: {baklogstotal}");

//            double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
//            Console.WriteLine($"[LoadResult] Calculated SGPA: {sgpa}");
//            string s = sgpa.ToString("F2");
//            Console.WriteLine($"[LoadResult] Formatted SGPA: {s}");

//            groupedBacklogs = backlogs
//                .GroupBy(b => b.YearSem)
//                .OrderBy(g => g.Key)
//                .ToList();
//            Console.WriteLine($"[LoadResult] Grouped backlogs into {groupedBacklogs.Count} groups");
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            Console.WriteLine($"[LoadJsonData] Loading JSON from file: {filePath}");
//            if (!File.Exists(filePath))
//            {
//                Console.WriteLine($"[LoadJsonData] File not found: {filePath}");
//                MessageBox.Show($"File not found: {filePath}");
//                return new List<T>();
//            }

//            var json = File.ReadAllText(filePath);
//            Console.WriteLine($"[LoadJsonData] Read JSON content, length: {json.Length}");
//            var data = JsonConvert.DeserializeObject<List<T>>(json);
//            Console.WriteLine($"[LoadJsonData] Deserialized {data.Count} items");
//            return data;
//        }

//        private void print_Click(object sender, EventArgs e)
//        {
//            Console.WriteLine("[print_Click] Print button clicked");
//        }
//    }

//    public class sturesults
//    {
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string Credits { get; set; }
//        public string YearSem { get; set; }
//        public string GradePoints { get; set; }
//        public string SubjectCredits { get; set; }
//    }

//    public class StudentMark
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string GradePoints { get; set; }
//        public string Credits { get; set; }
//    }

//    public class subj
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//        public string Credits { get; set; }
//    }

//    public class st
//    {
//        public string StudentId { get; set; }
//        public string Name { get; set; }
//    }
//}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static project_RYS.uploadform;
using System.Windows.Input;
using System.IO;
using project_RYS.Forms;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using System.Drawing.Printing; // Required for printing

namespace project_RYS
{
    public partial class mainresult : Form
    {
        private PrintDocument printDocument = new PrintDocument();
        private string printContent = ""; // Store the content to be printed
        private string studentId;

        private List<DataGridView> dataGridViews;
        private List<Label> labels;
        private const int DataGridViewWidth = 1250;
        private const int DataGridViewHeight = 784;
        private const int SmallDataGridViewHeight = 80;
        private new const int Margin = 20;
        int baklogstotal = 0;
        double totalcgpa = 0.0;
        string name = "";
        private string stuName = "";

        private List<IGrouping<string, sturesults>> groupedBacklogs; // Grouped by Year-Sem as a single string

        public mainresult(string studentId)
        {
            InitializeComponent();
            this.studentId = studentId;

            this.Load += new EventHandler(Form1_Load);

            dataGridViews = new List<DataGridView>();
            labels = new List<Label>();

            this.Size = new Size(1300, 800);
            this.AutoScroll = true;

            LoadResult();
        }

        private Dictionary<string, string> LoadStudentNames(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                var jsonData = File.ReadAllText(filePath);
                var studentsList = JsonConvert.DeserializeObject<List<Student>>(jsonData);
                return studentsList
                    .Where(student => !string.IsNullOrEmpty(student.studentid))
                    .ToDictionary(student => student.studentid, student => student.name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string GetStudentName(string studentId)
        {
            var studentNames = LoadStudentNames("studentnames.json");
            if (studentNames != null && studentNames.ContainsKey(studentId))
            {
                return studentNames[studentId];
            }
            else
            {
                return "Unknown";
            }
        }

        private class Student
        {
            public string studentid { get; set; }
            public string name { get; set; }
        }

        private string GetBranchFromStudentId(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId)) return "Unknown";
            studentId = studentId.ToUpper().Trim();
            if (studentId.Contains("5A66") || studentId.Contains("1A66"))
                return "CSM";
            else if (studentId.Contains("1A05") || studentId.Contains("5A05"))
                return "CSE";
            else if (studentId.Contains("1A01") || studentId.Contains("5A01"))
                return "CE";
            else if (studentId.Contains("1A02") || studentId.Contains("5A02"))
                return "EEE";
            else if (studentId.Contains("1A03") || studentId.Contains("5A03"))
                return "ME";
            else if (studentId.Contains("1A25") || studentId.Contains("5A25"))
                return "MIE";
            else
                return "Unknown";
        }

        private st GetStudentById(string studentId)
        {
            var students = LoadJsonData<st>("studentMarks.json");
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
            var smallDataGridView = CreateSmallDataGridView();
            if (smallDataGridView != null)
            {
                var student = GetStudentById(studentId);
                if (student != null)
                {
                    string branch = GetBranchFromStudentId(student.StudentId);
                    dataGridViews.Add(smallDataGridView);
                    this.Controls.Add(smallDataGridView);
                    smallDataGridView.Rows.Add(GetStudentName(studentId), student.StudentId, branch);
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }

            ArrangeControls();
        }

        private void btnBackToHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adb = new admindashboard();
            adb.Show();
            try
            {
                FormResult child = new FormResult();
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

        int w;
        private void ArrangeControls()
        {
            int startY = 100;
            if (heading == null)
            {
                heading = new Label();
            }
            Button backbtn = new Button
            {
                Text = "Back To Home",
                Width = 200,
                Height = 40,
                Margin = new Padding(10)
            };
            backbtn.Location = new Point(10, startY);
            backbtn.Click += new EventHandler(btnBackToHome_Click);
            this.Controls.Add(backbtn);
            int bottomMargin = 50;
            startY = backbtn.Bottom + Margin;

            heading.Text = "Result";
            heading.Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
            heading.AutoSize = true;
            heading.Location = new Point((this.ClientSize.Width - heading.Width) / 2, 90);
            this.Controls.Add(heading);

            startY = heading.Bottom + Margin;

            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
            {
                var smallDataGridView = dataGridViews[0];
                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
                w = smallDataGridView.Location.X;
                startY += smallDataGridView.Height + Margin;
            }

            DisplayYearSemDataGrids(ref startY);

            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
            this.Controls.Add(lblTotalBacklogs);

            Button downloadButton = new Button
            {
                Text = "Download EXCEL",
                Width = 200,
                Height = 40
            };
            downloadButton.Location = new Point(lblTotalBacklogs.Right, startY);
            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
            this.Controls.Add(downloadButton);
        }

        private void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save the Result as Excel File",
                FileName = "StudentResults.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Student Results");

                    worksheet.Cell(1, 1).Value = "Student Details";
                    worksheet.Cell(2, 1).Value = "Name:";
                    worksheet.Cell(2, 2).Value = GetStudentName(studentId);
                    worksheet.Cell(3, 1).Value = "Roll Number:";
                    worksheet.Cell(3, 2).Value = studentId;
                    worksheet.Cell(4, 1).Value = "CGPA:";
                    worksheet.Cell(4, 2).Value = totalcgpa;
                    worksheet.Cell(5, 1).Value = "Total Backlogs:";
                    worksheet.Cell(5, 2).Value = baklogstotal;

                    worksheet.Range("A1:B1").Merge().Style.Font.SetBold().Font.FontSize = 14;
                    worksheet.Range("A2:A5").Style.Font.SetBold();

                    int currentRow = 7;

                    worksheet.Cell(currentRow, 1).Value = "Subject Code";
                    worksheet.Cell(currentRow, 2).Value = "Subject Name";
                    worksheet.Cell(currentRow, 3).Value = "Internal";
                    worksheet.Cell(currentRow, 4).Value = "External";
                    worksheet.Cell(currentRow, 5).Value = "Total";
                    worksheet.Cell(currentRow, 6).Value = "Grade";
                    worksheet.Cell(currentRow, 7).Value = "GradePoints";
                    worksheet.Cell(currentRow, 8).Value = "Credits";

                    worksheet.Row(currentRow).Style.Font.SetBold();
                    currentRow++;

                    foreach (var group in groupedBacklogs)
                    {
                        foreach (var backlog in group)
                        {
                            worksheet.Cell(currentRow, 1).Value = backlog.SubjectCode;
                            worksheet.Cell(currentRow, 2).Value = backlog.SubjectName;
                            worksheet.Cell(currentRow, 3).Value = backlog.Internal;
                            Console.WriteLine($"[btnDownloadPdf_Click] Internal Marks for {backlog.SubjectCode}: {backlog.Internal}");
                            worksheet.Cell(currentRow, 4).Value = backlog.External;
                            Console.WriteLine($"[btnDownloadPdf_Click] External Marks for {backlog.SubjectCode}: {backlog.External}");
                            worksheet.Cell(currentRow, 5).Value = backlog.Total;
                            Console.WriteLine($"[btnDownloadPdf_Click] Total Marks for {backlog.SubjectCode}: {backlog.Total}");
                            worksheet.Cell(currentRow, 6).Value = backlog.Grade;
                            Console.WriteLine($"[btnDownloadPdf_Click] Grade for {backlog.SubjectCode}: {backlog.Grade}");
                            worksheet.Cell(currentRow, 7).Value = backlog.GradePoints;
                            Console.WriteLine($"[btnDownloadPdf_Click] Grade Points for {backlog.SubjectCode}: {backlog.GradePoints}");
                            worksheet.Cell(currentRow, 8).Value = backlog.Credits;
                            Console.WriteLine($"[btnDownloadPdf_Click] Credits for {backlog.SubjectCode}: {backlog.Credits}");
                            currentRow++;
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DisplayYearSemDataGrids(ref int startY)
        {
            double totalSgpaSum = 0;
            int sgpaCount = 0;

            foreach (var group in groupedBacklogs)
            {
                var backlogLabel = new Label
                {
                    Text = $"{group.Key} Result",
                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true
                };
                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
                this.Controls.Add(backlogLabel);
                startY += backlogLabel.Height + Margin;

                var backlogDataGridView = new DataGridView
                {
                    Name = $"dgv_{group.Key}",
                    Width = DataGridViewWidth,
                    Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
                    AutoGenerateColumns = false,
                    ColumnCount = 8,
                    RowHeadersVisible = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                    AllowUserToAddRows = false,
                    ScrollBars = ScrollBars.None,
                    ReadOnly = true,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                };

                backlogDataGridView.RowTemplate.Height = 50;
                backlogDataGridView.ColumnHeadersHeight = 40;

                backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                };
                backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                };
                backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkViolet
                };
                backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                backlogDataGridView.GridColor = Color.White;

                backlogDataGridView.Columns[0].Name = "Subject Code";
                backlogDataGridView.Columns[1].Name = "Subject Name";
                backlogDataGridView.Columns[2].Name = "Internal";
                backlogDataGridView.Columns[3].Name = "External";
                backlogDataGridView.Columns[4].Name = "Total";
                backlogDataGridView.Columns[5].Name = "Grade";
                backlogDataGridView.Columns[6].Name = "GradePoints";
                backlogDataGridView.Columns[7].Name = "Credits";

                backlogDataGridView.Columns[0].Width = 150;
                backlogDataGridView.Columns[1].Width = 500;
                backlogDataGridView.Columns[2].Width = 90;
                backlogDataGridView.Columns[3].Width = 90;
                backlogDataGridView.Columns[4].Width = 90;
                backlogDataGridView.Columns[5].Width = 90;
                backlogDataGridView.Columns[6].Width = 120;
                backlogDataGridView.Columns[7].Width = 120;

                double totalGradePoints = 0;
                double totalCredits = 0;

                foreach (var backlog in group)
                {
                    backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.GradePoints, backlog.Credits);
                    Console.WriteLine($"[DisplayYearSemDataGrids] Added row for {group.Key}: SubjectCode={backlog.SubjectCode}, Internal={backlog.Internal}, External={backlog.External}, Total={backlog.Total}, Grade={backlog.Grade}, GradePoints={backlog.GradePoints}, Credits={backlog.Credits}");

                    double subjectCredits = double.TryParse(backlog.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
                    double gradePoint = double.TryParse(backlog.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;
                    Console.WriteLine($"[DisplayYearSemDataGrids] Parsed for {backlog.SubjectCode}: SubjectCredits={subjectCredits}, GradePoint={gradePoint}");

                    double gradePointsContribution = gradePoint * subjectCredits;
                    totalGradePoints += gradePointsContribution;
                    totalCredits += subjectCredits;
                    Console.WriteLine($"[DisplayYearSemDataGrids] Updated for {backlog.SubjectCode}: GradePointsContribution={gradePointsContribution}, TotalGradePoints={totalGradePoints}, TotalCredits={totalCredits}");

                    name = backlog.SubjectName;
                }

                backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());
                if (!this.Controls.Contains(backlogDataGridView))
                {
                    this.Controls.Add(backlogDataGridView);
                }
                startY += backlogDataGridView.Height + Margin;

                double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
                Console.WriteLine($"[DisplayYearSemDataGrids] Calculated SGPA for {group.Key}: {sgpa:F2} (TotalGradePoints={totalGradePoints}, TotalCredits={totalCredits})");
                totalSgpaSum += sgpa;
                sgpaCount++;
                Console.WriteLine($"[DisplayYearSemDataGrids] Updated: TotalSgpaSum={totalSgpaSum}, SgpaCount={sgpaCount}");

                var sgpaLabel = new Label
                {
                    Text = $"SGPA: {sgpa:F2}",
                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true
                };
                sgpaLabel.Location = new Point((this.ClientSize.Width - sgpaLabel.Width) / 2, startY);
                this.Controls.Add(sgpaLabel);
                startY += sgpaLabel.Height + Margin;
            }

            double cgpa = sgpaCount > 0 ? totalSgpaSum / sgpaCount : 0;
            Console.WriteLine($"[DisplayYearSemDataGrids] Calculated CGPA: {cgpa:F2} (TotalSgpaSum={totalSgpaSum}, SgpaCount={sgpaCount})");
            totalcgpa = cgpa;
            var cgpaLabel = new Label
            {
                Text = $"CGPA: {cgpa:F2}",
                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                AutoSize = true
            };
            cgpaLabel.Location = new Point((this.ClientSize.Width - cgpaLabel.Width) / 2, startY);
            this.Controls.Add(cgpaLabel);
            startY += cgpaLabel.Height + Margin;
        }

        private int CalculateDataGridViewHeight(int rowCount)
        {
            int rowHeight = 50;
            int headerHeight = 40;
            int totalHeight = headerHeight + rowCount * rowHeight;
            return totalHeight;
        }

        private DataGridView CreateSmallDataGridView()
        {
            var dataGridView = new DataGridView
            {
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                ColumnHeadersHeight = 40,
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkOrchid,
                    ForeColor = Color.White,
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkViolet
                },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.None,
                ReadOnly = true,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
            };

            dataGridView.RowTemplate.Height = 50;
            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "STUDENT NAME";
            dataGridView.Columns[1].Name = "ROLL NO";
            dataGridView.Columns[2].Name = "BRANCH";

            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
            return dataGridView;
        }

        private void LoadResult()
        {
            var studentMarks = LoadJsonData<StudentMark>("studentMarks.json");
            var subjectCodes = LoadJsonData<subj>("subjectCodes.json");

            var backlogs = studentMarks
                .Where(m => m.StudentId == studentId)
                .Join(subjectCodes,
                    mark => mark.SubjectCode,
                    subject => subject.SubjectCode,
                    (mark, subject) => new sturesults
                    {
                        SubjectCode = mark.SubjectCode,
                        SubjectName = mark.SubjectName,
                        Internal = mark.Internal,
                        External = mark.External,
                        Total = mark.Total,
                        Grade = mark.Grade,
                        Credits = mark.Credits,
                        YearSem = $"{subject.Year}-{subject.Semester}",
                        GradePoints = mark.GradePoints,
                        SubjectCredits = subject.Credits
                    })
                .ToList();

            baklogstotal = 0;
            double totalCredits = 0;
            double totalGradePoints = 0;

            foreach (var result in backlogs)
            {
                Console.WriteLine($"[LoadResult] Processing: SubjectCode={result.SubjectCode}, Internal={result.Internal}, External={result.External}, Total={result.Total}, Grade={result.Grade}, GradePoints={result.GradePoints}, Credits={result.Credits}");
                if (result.Grade == "F" || result.Grade == "Ab")
                {
                    baklogstotal++;
                    Console.WriteLine($"[LoadResult] Backlog detected for {result.SubjectCode}, Total Backlogs: {baklogstotal}");
                }
                double gradePoint = double.TryParse(result.GradePoints, out double parsedGradePoints) ? parsedGradePoints : 0;
                double subjectCredits = double.TryParse(result.SubjectCredits, out double parsedSubjectCredits) ? parsedSubjectCredits : 0;
                Console.WriteLine($"[LoadResult] Parsed for {result.SubjectCode}: GradePoint={gradePoint}, SubjectCredits={subjectCredits}");

                double gradePointsContribution = gradePoint * subjectCredits;
                totalGradePoints += gradePointsContribution;
                totalCredits += subjectCredits;
                Console.WriteLine($"[LoadResult] Updated for {result.SubjectCode}: GradePointsContribution={gradePointsContribution}, TotalGradePoints={totalGradePoints}, TotalCredits={totalCredits}");
            }

            lblTotalBacklogs.Text = baklogstotal.ToString();
            double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;
            Console.WriteLine($"[LoadResult] Calculated SGPA: {sgpa:F2} (TotalGradePoints={totalGradePoints}, TotalCredits={totalCredits})");
            string s = sgpa.ToString("F2");

            groupedBacklogs = backlogs
                .GroupBy(b => b.YearSem)
                .OrderBy(g => g.Key)
                .ToList();
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found: {filePath}");
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        private void print_Click(object sender, EventArgs e)
        {
        }
    }

    public class sturesults
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string Credits { get; set; }
        public string YearSem { get; set; }
        public string GradePoints { get; set; }
        public string SubjectCredits { get; set; }
    }

    public class StudentMark
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string GradePoints { get; set; }
        public string Credits { get; set; }
    }

    public class subj
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Credits { get; set; }
    }

    public class st
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
    }
}