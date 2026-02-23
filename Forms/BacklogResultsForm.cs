////using Newtonsoft.Json;
////using project_RYS.Forms;
////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;
////using static project_RYS.uploadform;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {

////        private string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouping by YearSem as a single string

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            //this.studentId = studentId;
////            this.studentId = studentId;

////            // Automatically load backlog data when the form is initialized
////            LoadBacklogData();
////            //LoadBacklogData();
////        }
////        private void LoadBacklogData()
////        {
////            // Load JSON files
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

////            // Filter data for the specific student and grade criteria
////            var backlogs = studentMarks
////                .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"  // Create a Year-Sem string
////                    })
////                .ToList();

////            // Check if backlogs are found
////            if (backlogs.Count == 0)
////            {
////                MessageBox.Show("Zero backlogs found for this student.");
////                return;
////            }

////            // Separate backlogs by Year-Sem
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem) // Grouping by the YearSem string
////                .OrderBy(g => g.Key) // Order by the YearSem string
////                .ToList();

////            // Display each YearSem in a separate DataGridView
////            DisplayYearSemDataGrids();
////        }


////        private void DisplayYearSemDataGrids()
////        {
////            // Clear existing controls
////            Controls.Clear();

////            int topPosition = 20; // Initial position

////            Console.WriteLine("Grouped Backlogs Count: " + groupedBacklogs.Count);
////            foreach (var group in groupedBacklogs)
////            {
////                Console.WriteLine("Year-Sem: " + group.Key + ", Backlog Count: " + group.Count());

////                // Create a DataGridView for each YearSem
////                DataGridView dgv = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = 600,
////                    Height = 150,
////                    Location = new System.Drawing.Point(20, topPosition),
////                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
////                    AllowUserToAddRows = false
////                };

////                // Prepare data and log the entries
////                var data = group.Select(b => new
////                {
////                    b.SubjectCode,
////                    b.SubjectName,
////                    b.Internal,
////                    b.External,
////                    b.Total,
////                    b.Grade,
////                    b.Credits
////                }).ToList();

////                Console.WriteLine($"Year-Sem: {group.Key}, Data Count: {data.Count}");
////                foreach (var item in data)
////                {
////                    Console.WriteLine($"SubjectCode: {item.SubjectCode}, SubjectName: {item.SubjectName}, Internal: {item.Internal}, External: {item.External}, Total: {item.Total}, Grade: {item.Grade}, Credits: {item.Credits}");
////                }

////                // Bind data to DataGridView and check for columns
////                dgv.DataSource = data;
////                if (dgv.Columns.Count >= 7)
////                {
////                    dgv.Columns[0].HeaderText = "Subject Code";
////                    dgv.Columns[1].HeaderText = "Subject Name";
////                    dgv.Columns[2].HeaderText = "Internal";
////                    dgv.Columns[3].HeaderText = "External";
////                    dgv.Columns[4].HeaderText = "Total";
////                    dgv.Columns[5].HeaderText = "Grade";
////                    dgv.Columns[6].HeaderText = "Credits";
////                }
////                else
////                {
////                    Console.WriteLine($"DataGridView for {group.Key} does not have the expected number of columns.");
////                    MessageBox.Show($"DataGridView for {group.Key} did not generate columns as expected.");
////                    continue;
////                }

////                // Create label and add controls
////                Label lblYearSem = new Label
////                {
////                    Text = $"Year-Sem: {group.Key}",
////                    Location = new System.Drawing.Point(20, topPosition - 20),
////                    AutoSize = true
////                };

////                Controls.Add(lblYearSem);
////                Controls.Add(dgv);

////                topPosition += 180; // Adjust position for the next group
////            }
////        }

////        private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}");
////                return new List<T>();
////            }

////            var jsonData = File.ReadAllText(filePath);
////            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
////        }


////}
////    // Define your Backlog class here
////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////        public string YearSem { get; set; }  // Combine Year and Semester as a single property
////    }

////    // Define your StudentMarks class here based on your JSON structure
////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    // Define your Subject class here based on your JSON structure
////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }
////}

////using Newtonsoft.Json;
////using project_RYS.Forms;
////using System;
////using System.Collections.Generic;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Reflection.Emit;
////using System.Windows.Forms;
////using Label = System.Windows.Forms.Label;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private const int DataGridViewWidth = 1250;
////        private const int DataGridViewHeight = 392;
////        private const int SmallDataGridViewHeight = 80;
////        private const int Margin = 20;
////        int baklogstotal = 0;
////        private string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouped by Year-Sem as a single string

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.Load += new EventHandler(Form1_Load);

////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();

////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.studentId = studentId;

////            LoadBacklogData();
////        }
////        private void Form1_Load(object sender, EventArgs e)
////        { 
////            //ArrangeControls();
////            // Add label for Total Backlogs
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";

////            // Create small DataGridView and add sample data
////            var smallDataGridView = CreateSmallDataGridView();
////            if (smallDataGridView != null)
////            {
////                dataGridViews.Add(smallDataGridView);
////                this.Controls.Add(smallDataGridView);
////                smallDataGridView.Rows.Add("Student 1", "12345", "Computer Science");
////            }

////            ArrangeControls();
////        }

////        private void ArrangeControls()
////        {
////            int startY = 10;

////            // Position the small DataGridView at the top
////            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
////            {
////                var smallDataGridView = dataGridViews[0];
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                startY += smallDataGridView.Height + Margin;
////            }

////            // Position each label and corresponding DataGridView
////            for (int i = 0; i < labels.Count && i < dataGridViews.Count - 1; i++)
////            {
////                var label = labels[i];
////                var dataGridView = dataGridViews[i + 1];

////                if (label != null && dataGridView != null)
////                {
////                    label.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                    startY += label.Height;

////                    dataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                    startY += dataGridView.Height + Margin;
////                }
////            }

////            // Adjust AutoScrollMinSize based on the total height of all controls
////            this.AutoScrollMinSize = new Size(DataGridViewWidth, startY);
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
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new Font("Arial", 9)
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
////                ScrollBars = ScrollBars.None
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";

////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }

////        private void LoadBacklogData()
////        {
////            // Load JSON data
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

////            // Filter student marks for specified student ID and failing grades (F, Ab)
////            var backlogs = studentMarks
////                .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"  // Create Year-Sem string
////                    })
////                .ToList();
////            baklogstotal = backlogs.Count;
////            lblTotalBacklogs.Text = backlogs.Count.ToString();

////            // Group backlogs by Year-Sem
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key) // Order groups by Year-Sem
////                .ToList();

////            // Display each Year-Sem backlogs in a separate DataGridView
////            DisplayYearSemDataGrids();
////        }

////        private void DisplayYearSemDataGrids()
////        {
////            int topPosition = 20; // Initial position

////            foreach (var group in groupedBacklogs)
////            {
////                DataGridView dgv = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = 600,
////                    Height = 150,
////                    Location = new System.Drawing.Point(20, topPosition),
////                    AutoGenerateColumns = false,
////                    ColumnCount = 6,
////                    RowHeadersVisible = false,
////                };

////                dgv.Columns[0].Name = "Subject Code";
////                dgv.Columns[1].Name = "Subject Name";
////                dgv.Columns[2].Name = "Internal";
////                dgv.Columns[3].Name = "External";
////                dgv.Columns[4].Name = "Total";
////                dgv.Columns[5].Name = "Grade";

////                foreach (var backlog in group)
////                {
////                    dgv.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade);
////                }

////                this.Controls.Add(dgv);
////                topPosition += dgv.Height + 10; // Update top position for next DataGridView
////            }
////        }
////            // Method to load JSON data from file
////            private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}");
////                return new List<T>();
////            }

////            var jsonData = File.ReadAllText(filePath);
////            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
////        }
////    }

////    // Backlog class for storing data
////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////        public string YearSem { get; set; }  // Combine Year and Semester as a single property
////    }

////    // StudentMarks class matching JSON structure
////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    // Subject class matching JSON structure
////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }
//////}
////using Newtonsoft.Json;
////using System.Collections.Generic;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using System;
////using System.IO;


////using System.Reflection.Emit;

////using Label = System.Windows.Forms.Label;
////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private const int DataGridViewWidth = 1250;
////        private const int DataGridViewHeight = 392;
////        private const int SmallDataGridViewHeight = 80;
////        private const int Margin = 20;
////        int baklogstotal = 0;
////        private string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouped by Year-Sem as a single string

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.Load += new EventHandler(Form1_Load);

////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();

////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.studentId = studentId;

////            LoadBacklogData();
////        }

////        private void Form1_Load(object sender, EventArgs e)
////        {
////            // Set the label for total backlogs
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";

////            // Create small DataGridView and add sample data
////            var smallDataGridView = CreateSmallDataGridView();
////            if (smallDataGridView != null)
////            {
////                dataGridViews.Add(smallDataGridView);
////                this.Controls.Add(smallDataGridView);
////                smallDataGridView.Rows.Add("Student 1", "12345", "Computer Science");
////            }

////            ArrangeControls();
////        }
////        private void ArrangeControls()
////        {
////            int startY = 10; // Initial Y position for controls

////            // Add Heading at the top center
////            Label headingLabel = new Label
////            {
////                Text = "Backlog Results",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true
////            };
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            this.Controls.Add(headingLabel);
////            startY += headingLabel.Height + Margin;

////            // Position the small DataGridView below the heading
////            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
////            {
////                var smallDataGridView = dataGridViews[0];
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                startY += smallDataGridView.Height + Margin;
////            }

////            // Now start placing the backlog DataGridViews below the small DataGridView
////            foreach (var group in groupedBacklogs)
////            {
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Backlogs", // Year-Sem label
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                // Create a DataGridView for this Year-Sem group
////                var backlogDataGridView = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = DataGridViewWidth,
////                    Height = 150,
////                    Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
////                    AutoGenerateColumns = false,
////                    ColumnCount = 6,
////                    RowHeadersVisible = false,
////                };

////                backlogDataGridView.Columns[0].Name = "Subject Code";
////                backlogDataGridView.Columns[1].Name = "Subject Name";
////                backlogDataGridView.Columns[2].Name = "Internal";
////                backlogDataGridView.Columns[3].Name = "External";
////                backlogDataGridView.Columns[4].Name = "Total";
////                backlogDataGridView.Columns[5].Name = "Grade";

////                // Add rows for each backlog in this group
////                foreach (var backlog in group)
////                {
////                    backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade);
////                }

////                // Add the DataGridView to the form only once
////                if (!this.Controls.Contains(backlogDataGridView))
////                {
////                    this.Controls.Add(backlogDataGridView);
////                }

////                startY += backlogDataGridView.Height + Margin; // Update startY for the next control
////            }

////            // Position the total backlog label at the bottom
////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            this.Controls.Add(lblTotalBacklogs);
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
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new Font("Arial", 9)
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
////                ScrollBars = ScrollBars.None
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";

////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }

////        private void LoadBacklogData()
////        {
////            // Load JSON data
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

////            // Filter student marks for specified student ID and failing grades (F, Ab)
////            var backlogs = studentMarks
////                .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"  // Create Year-Sem string
////                    })
////                .ToList();
////            baklogstotal = backlogs.Count;
////            lblTotalBacklogs.Text = backlogs.Count.ToString();

////            // Group backlogs by Year-Sem
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key) // Order groups by Year-Sem
////                .ToList();

////            // Display each Year-Sem backlogs in a separate DataGridView
////            DisplayYearSemDataGrids();
////        }

////        private void DisplayYearSemDataGrids()
////        {
////            int topPosition = 20; // Initial position

////            foreach (var group in groupedBacklogs)
////            {
////                DataGridView dgv = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = 600,
////                    Height = 150,
////                    Location = new System.Drawing.Point(20, topPosition),
////                    AutoGenerateColumns = false,
////                    ColumnCount = 6,
////                    RowHeadersVisible = false,
////                };

////                dgv.Columns[0].Name = "Subject Code";
////                dgv.Columns[1].Name = "Subject Name";
////                dgv.Columns[2].Name = "Internal";
////                dgv.Columns[3].Name = "External";
////                dgv.Columns[4].Name = "Total";
////                dgv.Columns[5].Name = "Grade";

////                foreach (var backlog in group)
////                {
////                    dgv.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade);
////                }

////                this.Controls.Add(dgv);
////                topPosition += dgv.Height + 10; // Update top position for next DataGridView
////            }
////        }

////        // Method to load JSON data from file
////        private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}");
////                return new List<T>();
////            }

////            var jsonData = File.ReadAllText(filePath);
////            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
////        }

////        private void BacklogResultsForm_Load(object sender, EventArgs e)
////        {

////        }
////    }

////    // Backlog class for storing data
////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////        public string YearSem { get; set; }  // Combine Year and Semester as a single property
////    }

////    // StudentMarks class matching JSON structure
////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; internal set; }
////    }

////    // Subject class for matching JSON structure
////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }
////}




////using Newtonsoft.Json;
////using System.Collections.Generic;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using System;
////using System.IO;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private const int DataGridViewWidth = 1250;
////        private const int DataGridViewHeight = 392;
////        private const int SmallDataGridViewHeight = 80;
////        private const int Margin = 20;
////        int baklogstotal = 0;
////        private string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouped by Year-Sem as a single string

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.Load += new EventHandler(Form1_Load);

////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();

////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.studentId = studentId;

////            LoadBacklogData();
////        }

////        private void Form1_Load(object sender, EventArgs e)
////        {
////            // Set the label for total backlogs
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";

////            // Create small DataGridView and add sample data
////            var smallDataGridView = CreateSmallDataGridView();
////            if (smallDataGridView != null)
////            {
////                dataGridViews.Add(smallDataGridView);
////                this.Controls.Add(smallDataGridView);
////                smallDataGridView.Rows.Add("Student 1", "12345", "Computer Science");
////            }

////            ArrangeControls();
////        }

////        private void ArrangeControls()
////        {
////            int startY = 70; // Initial Y position for controls

////            // Add Heading at the top center
////            Label headingLabel = new Label
////            {
////                Text = "Backlog Results",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true
////            };
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            this.Controls.Add(headingLabel);
////            startY += headingLabel.Height + Margin;

////            // Position the small DataGridView below the heading
////            if (dataGridViews.Count > 0 && dataGridViews[0] != null)
////            {
////                var smallDataGridView = dataGridViews[0];
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                startY += smallDataGridView.Height + Margin;
////            }

////            // Now start placing the backlog DataGridViews below the small DataGridView
////            DisplayYearSemDataGrids(ref startY);

////            // Position the total backlog label at the bottom
////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            this.Controls.Add(lblTotalBacklogs);
////        }

////        private void DisplayYearSemDataGrids(ref int startY)
////        {
////            // Clear any existing controls before adding new ones
////            foreach (var control in this.Controls)
////            {
////                if (control is DataGridView || control is Label)
////                {
////                    this.Controls.Remove(control as Control);
////                }
////            }

////            // Display each Year-Sem backlogs in a separate DataGridView
////            foreach (var group in groupedBacklogs)
////            {
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Backlogs", // Year-Sem label
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                // Create a DataGridView for this Year-Sem group
////                var backlogDataGridView = new DataGridView
////                {
////                    Name = $"dgv_{group.Key}",
////                    Width = DataGridViewWidth,
////                    Height = 150,
////                    Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
////                    AutoGenerateColumns = false,
////                    ColumnCount = 6,
////                    RowHeadersVisible = false,
////                };

////                backlogDataGridView.Columns[0].Name = "Subject Code";
////                backlogDataGridView.Columns[1].Name = "Subject Name";
////                backlogDataGridView.Columns[2].Name = "Internal";
////                backlogDataGridView.Columns[3].Name = "External";
////                backlogDataGridView.Columns[4].Name = "Total";
////                backlogDataGridView.Columns[5].Name = "Grade";

////                // Add rows for each backlog in this group
////                foreach (var backlog in group)
////                {
////                    backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade);
////                }

////                // Add the DataGridView to the form only once (check if not already added)
////                if (!this.Controls.Contains(backlogDataGridView))
////                {
////                    this.Controls.Add(backlogDataGridView);
////                }

////                startY += backlogDataGridView.Height + Margin; // Update startY for the next control
////            }
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
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = Color.DarkOrchid,
////                    ForeColor = Color.White,
////                    Font = new Font("Arial", 9)
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
////                ScrollBars = ScrollBars.None
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";

////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }

////        private void LoadBacklogData()
////        {
////            // Load JSON data
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

////            // Filter student marks for specified student ID and failing grades (F, Ab)
////            var backlogs = studentMarks
////                .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"  // Create Year-Sem string
////                    })
////                .ToList();
////            baklogstotal = backlogs.Count;
////            lblTotalBacklogs.Text = backlogs.Count.ToString();

////            // Group backlogs by Year-Sem
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key) // Order groups by Year-Sem
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
////    }

////    // Backlog data structure
////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string YearSem { get; set; }
////        public string Credits { get; set; }
////    }

////    // Student marks data structure
////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    // Subject data structure
////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }
////}


////using Newtonsoft.Json;
////using System.Collections.Generic;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using System;
////using System.IO;
////using project_RYS.Forms;
////using iTextSharp.text.pdf;
////using iTextSharp.text;
////using OfficeOpenXml.Style;
////using OfficeOpenXml;
////using DocumentFormat.OpenXml.Wordprocessing;
////using System.Drawing;
////using static System.Windows.Forms.AxHost;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private List<DataGridView> attemptDataGridViews;
////        private List<Label> attemptLabels;
////        private Button backButton;
////        private Button downloadButton;
////        private Label lblTotalBacklogs;
////        private Label headingLabel;
////        private const int DataGridViewWidth = 1250;
////        private const int DataGridViewHeight = 392;
////        private const int SmallDataGridViewHeight = 80;
////        private new const int Margin = 20;
////        int baklogstotal = 0;
////        private readonly string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs; // Grouped by Year-Sem as a single string
////        private Dictionary<string, List<Attempt>> groupedAttempts;

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.studentId = studentId.ToUpper();
////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();
////            attemptDataGridViews = new List<DataGridView>();
////            attemptLabels = new List<Label>();
////            SetupForm();
////            LoadBacklogData();
////            //InitializeComponent();
////            //this.Load += new EventHandler(Form1_Load);

////            //dataGridViews = new List<DataGridView>();
////            //labels = new List<Label>();

////            //this.Size = new Size(1300, 800);
////            //this.AutoScroll = true;
////            //this.studentId = studentId.ToUpper();

////            //LoadBacklogData();

////        }
////        private void SetupForm()
////        {
////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Backlog Report",
////                Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = System.Drawing.Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backButton = new Button
////            {
////                Text = "Back To Home",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = System.Drawing.Color.DarkViolet,
////                ForeColor = System.Drawing.Color.White,
////                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
////            };
////            backButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
////            backButton.Click += new EventHandler(btnBackToHome_Click);
////            this.Controls.Add(backButton);

////            // Download Button
////            downloadButton = new Button
////            {
////                Text = "Download",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = System.Drawing.Color.DarkOrchid,
////                ForeColor = System.Drawing.Color.White,
////                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
////            };
////            downloadButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkViolet;
////            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
////            this.Controls.Add(downloadButton);

////            // Total Backlogs Label
////            lblTotalBacklogs = new Label
////            {
////                Text = $"Total Backlogs: {baklogstotal}",
////                Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = System.Drawing.Color.DarkOrchid
////            };
////            this.Controls.Add(lblTotalBacklogs);
////        }
////        // Function to load student names from JSON file and return a dictionary
////        //private Dictionary<string, string> LoadStudentNames(string filePath)
////        //{
////        //    if (!File.Exists(filePath))
////        //    {
////        //        MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////        //        return null;
////        //    }

////        //    try
////        //    {
////        //        var jsonData = File.ReadAllText(filePath);

////        //        // Deserialize the JSON array into a list of dynamic objects
////        //        var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);

////        //        // Convert the list to a dictionary for easy lookup
////        //        return studentsList
////        //            .Where(student => !string.IsNullOrEmpty(student.studentid)) // Filter valid student IDs
////        //            .ToDictionary(student => student.studentid, student => student.name);
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////        //        return null;
////        //    }
////        //}

////        private Dictionary<string, string> LoadStudentNames(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
////            }

////            try
////            {
////                var jsonData = File.ReadAllText(filePath);
////                var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);
////                return studentsList
////                    .Where(student => !string.IsNullOrEmpty(student.studentid))
////                    .ToDictionary(student => student.studentid, student => student.name);
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
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
////        private class stnames
////        {
////            public string studentid { get; set; }
////            public string name { get; set; }
////        }

////        private string GetBranchFromStudentId(string studentId)
////        {
////            // Check if the student ID contains specific patterns for each branch
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
////            // Add more conditions here for other patterns as needed
////            else
////                return "Unknown"; // Default case if no pattern matches
////        }

////        //private Student GetStudentById(string studentId)
////        //{
////        //    var students = LoadJsonData<Student>("studentMarks.json"); // Assuming you have a 'students.json' file
////        //    return students.FirstOrDefault(s => s.StudentId == studentId);
////        //}
////        private Student GetStudentById(string studentId)
////        {
////            var students = LoadJsonData<StudentMarks>("studentMarks.json");
////            return new Student
////            {
////                StudentId = studentId,
////                Name = GetStudentName(studentId)
////            };
////        }

////        private void Form1_Load(object sender, EventArgs e)
////        {
////            // Set the label for total backlogs
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";

////            // Create small DataGridView and add student data
////            var smallDataGridView = CreateSmallDataGridView();
////            if (smallDataGridView != null)
////            {
////                // Fetch student details based on studentId
////                var student = GetStudentById(studentId);

////                if (student != null)
////                {
////                    // Determine the branch based on studentId
////                    string branch = GetBranchFromStudentId(student.StudentId);

////                    dataGridViews.Add(smallDataGridView);
////                    this.Controls.Add(smallDataGridView);
////                    //student.Name,
////                    smallDataGridView.Rows.Add(GetStudentName(studentId), student.StudentId, branch);
////                }
////                else
////                {
////                    MessageBox.Show("Student not found.");
////                }
////            }

////            ArrangeControls();

////        }

////        int w;
////        //private object headingLabel;

////        //private void ArrangeControls()
////        //{
////        //    int startY = 100; // Initial Y position for controls
////        //                      // Add Heading at the top center


////        //    Button backbtn = new Button
////        //    {
////        //        Text = "Back To Home",
////        //        Width = 200,
////        //        Height = 40
////        //    };
////        //    backbtn.Location = new Point(10, startY);
////        //    backbtn.Click += new EventHandler(btnBackToHome_Click);
////        //    this.Controls.Add(backbtn);
////        //    startY = backbtn.Bottom + Margin;

////        //    Label headingLabel = new Label
////        //    {
////        //        Text = "Backlog Report",
////        //        Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold),
////        //        AutoSize = true,
////        //        // Optionally add a background color or other properties
////        //    };

////        //    // Center the heading label horizontally
////        //    headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////        //    this.Controls.Add(headingLabel);

////        //    // Update startY to position next controls below the heading
////        //    startY = headingLabel.Bottom + Margin;
////        //    // Position the small DataGridView below the heading
////        //    if (dataGridViews.Count > 0 && dataGridViews[0] != null)
////        //    {
////        //        var smallDataGridView = dataGridViews[0];
////        //        smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////        //        w = smallDataGridView.Location.X;
////        //        startY += smallDataGridView.Height + Margin;
////        //    }


////        //    // Now start placing the backlog DataGridViews below the small DataGridView
////        //    DisplayYearSemDataGrids(ref startY);

////        //    // Position the total backlog label at the bottom
////        //    lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////        //    this.Controls.Add(lblTotalBacklogs);


////        //    Button downloadButton = new Button
////        //    {
////        //        Text = "Download",
////        //        Width = 200,
////        //        Height = 40
////        //    };
////        //    downloadButton.Location = new Point(lblTotalBacklogs.Right, startY);
////        //    downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
////        //    this.Controls.Add(downloadButton);
////        //    //startY += lblTotalBacklogs.Height + Margin;
////        //}

////        private void ArrangeControls()
////        {
////            int startY = 20;

////            // Center heading
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            startY = headingLabel.Bottom + Margin;

////            // Back button
////            backButton.Location = new Point(20, startY);
////            startY = backButton.Bottom + Margin;

////            // Small DataGridView
////            if (dataGridViews.Count > 0)
////            {
////                var smallDataGridView = dataGridViews[0];
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                var student = GetStudentById(studentId);
////                if (student != null)
////                {
////                    smallDataGridView.Rows.Add(student.Name, student.StudentId, GetBranchFromStudentId(studentId));
////                }
////                startY = smallDataGridView.Bottom + Margin;
////            }

////            // Backlog and attempt DataGridViews
////            DisplayYearSemDataGrids(ref startY);

////            // Total backlogs and download button
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            downloadButton.Location = new Point(lblTotalBacklogs.Right + Margin, startY);
////        }
////        //private void DisplayYearSemDataGrids(ref int startY)
////        //{
////        //    foreach (var group in groupedBacklogs)
////        //    {
////        //        var backlogLabel = new Label
////        //        {
////        //            Text = $"{group.Key} Backlogs",
////        //            Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////        //            AutoSize = true
////        //        };
////        //        backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////        //        this.Controls.Add(backlogLabel);
////        //        startY += backlogLabel.Height + Margin;

////        //        var backlogDataGridView = new DataGridView
////        //        {
////        //            Name = $"dgv_{group.Key}",
////        //            Width = DataGridViewWidth,
////        //            Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
////        //            AutoGenerateColumns = false,
////        //            ColumnCount = 7,
////        //            RowHeadersVisible = false,
////        //            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None, // Manual width control
////        //            AllowUserToAddRows = false,
////        //            ScrollBars = ScrollBars.None, // No scrollbars
////        //            ReadOnly = true,
////        //            AllowUserToResizeColumns = false,
////        //            AllowUserToResizeRows = false,
////        //        };
////        //        backlogDataGridView.RowTemplate.Height = 50;
////        //        backlogDataGridView.ColumnHeadersHeight = 40;

////        //        backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkOrchid,
////        //            ForeColor = System.Drawing.Color.White,
////        //            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////        //            Alignment = DataGridViewContentAlignment.MiddleCenter
////        //        };
////        //        backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkOrchid,
////        //            ForeColor = System.Drawing.Color.White,
////        //            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////        //        };
////        //        backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkViolet
////        //        };
////        //        backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
////        //        backlogDataGridView.GridColor = System.Drawing.Color.White;

////        //        backlogDataGridView.Columns[0].Name = "Subject Code";
////        //        backlogDataGridView.Columns[1].Name = "Subject Name";
////        //        backlogDataGridView.Columns[2].Name = "Internal";
////        //        backlogDataGridView.Columns[3].Name = "External";
////        //        backlogDataGridView.Columns[4].Name = "Total";
////        //        backlogDataGridView.Columns[5].Name = "Grade";
////        //        backlogDataGridView.Columns[6].Name = "Credits";

////        //        // Set column widths
////        //        backlogDataGridView.Columns[0].Width = 150; // Subject Code
////        //        backlogDataGridView.Columns[1].Width = 500; // Subject Name (wide)
////        //        backlogDataGridView.Columns[2].Width = 120;  // Internal
////        //        backlogDataGridView.Columns[3].Width = 120;  // External
////        //        backlogDataGridView.Columns[4].Width = 100;  // Total
////        //        backlogDataGridView.Columns[5].Width = 120;  // Grade
////        //        backlogDataGridView.Columns[6].Width = 120;  // Credits

////        //        foreach (var backlog in group)
////        //        {
////        //            backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
////        //        }

////        //        // Remove uncommitted row if any
////        //        if (backlogDataGridView.Rows.Count > 0)
////        //        {
////        //            var lastRow = backlogDataGridView.Rows[backlogDataGridView.Rows.Count - 1];
////        //            if (lastRow.Cells.Cast<DataGridViewCell>().All(cell => cell.Value == null))
////        //            {
////        //                backlogDataGridView.Rows.RemoveAt(backlogDataGridView.Rows.Count - 1);
////        //            }
////        //        }

////        //        // Set dynamic height based on row count
////        //        backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());

////        //        if (!this.Controls.Contains(backlogDataGridView))
////        //        {
////        //            this.Controls.Add(backlogDataGridView);
////        //        }

////        //        startY += backlogDataGridView.Height + Margin;
////        //    }
////        //}




////        private void DisplayYearSemDataGrids(ref int startY)
////        {
////            foreach (var group in groupedBacklogs)
////            {
////                // Backlog Label
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Backlogs",
////                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = System.Drawing.Color.DarkOrchid
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                labels.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                // Backlog DataGridView
////                var backlogDataGridView = CreateBacklogDataGridView(group);
////                backlogDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(backlogDataGridView);
////                dataGridViews.Add(backlogDataGridView);
////                startY += backlogDataGridView.Height + Margin;

////                // Attempt Label
////                var attemptLabel = new Label
////                {
////                    Text = $"{group.Key} Attempt History",
////                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = System.Drawing.Color.DarkOrchid
////                };
////                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
////                this.Controls.Add(attemptLabel);
////                attemptLabels.Add(attemptLabel);
////                startY += attemptLabel.Height + Margin;

////                // Attempt DataGridView
////                var attemptDataGridView = CreateAttemptDataGridView(group.Key);
////                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(attemptDataGridView);
////                attemptDataGridViews.Add(attemptDataGridView);
////                startY += attemptDataGridView.Height + Margin;
////            }
////        }

////        private DataGridView CreateBacklogDataGridView(IGrouping<string, Backlog> group)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_{group.Key}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 7,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
////                ReadOnly = true,
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false,
////                EnableHeadersVisualStyles = false,
////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkOrchid,
////                    ForeColor = System.Drawing.Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new System.Drawing.Font("Arial", 10)
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = System.Drawing.Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Subject Name";
////            dataGridView.Columns[2].Name = "Internal";
////            dataGridView.Columns[3].Name = "External";
////            dataGridView.Columns[4].Name = "Total";
////            dataGridView.Columns[5].Name = "Grade";
////            dataGridView.Columns[6].Name = "Credits";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 500;
////            dataGridView.Columns[2].Width = 120;
////            dataGridView.Columns[3].Width = 120;
////            dataGridView.Columns[4].Width = 100;
////            dataGridView.Columns[5].Width = 120;
////            dataGridView.Columns[6].Width = 120;

////            foreach (var backlog in group)
////            {
////                dataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(group.Count());
////            return dataGridView;
////        }

////        private DataGridView CreateAttemptDataGridView(string yearSem)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_attempts_{yearSem}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 5,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
////                ReadOnly = true,
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false,
////                EnableHeadersVisualStyles = false,
////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkOrchid,
////                    ForeColor = System.Drawing.Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new System.Drawing.Font("Arial", 10)
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = System.Drawing.Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Attempt Number";
////            dataGridView.Columns[2].Name = "Grade Points";
////            dataGridView.Columns[3].Name = "Credits";
////            dataGridView.Columns[4].Name = "Timestamp";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 150;
////            dataGridView.Columns[2].Width = 150;
////            dataGridView.Columns[3].Width = 150;
////            dataGridView.Columns[4].Width = 650;

////            if (groupedAttempts.ContainsKey(yearSem))
////            {
////                foreach (var attempt in groupedAttempts[yearSem])
////                {
////                    dataGridView.Rows.Add(attempt.SubjectCode, attempt.AttemptNumber, attempt.GradePoints, attempt.Credits, attempt.Timestamp);
////                }
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            return dataGridView;
////        }



////        //private int CalculateDataGridViewHeight(int rowCount)
////        //{
////        //    int rowHeight = 50;
////        //    int headerHeight = 40;
////        //    int totalHeight = headerHeight + rowCount * rowHeight;
////        //    return totalHeight;
////        //}
////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 50;
////            int headerHeight = 40;
////            int totalHeight = headerHeight + rowCount * rowHeight;
////            return totalHeight < 90 ? 90 : totalHeight; // Minimum height for empty tables
////        }
////        //private DataGridView CreateSmallDataGridView()
////        //{
////        //    var dataGridView = new DataGridView
////        //    {
////        //        EnableHeadersVisualStyles = false,
////        //        ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkOrchid,
////        //            ForeColor = System.Drawing.Color.White,
////        //            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////        //            Alignment = DataGridViewContentAlignment.MiddleCenter
////        //        },
////        //        ColumnHeadersHeight = 40,
////        //        RowsDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkOrchid,
////        //            ForeColor = System.Drawing.Color.White,
////        //            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////        //        },
////        //        AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////        //        {
////        //            BackColor = System.Drawing.Color.DarkViolet
////        //        },
////        //        CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////        //        GridColor = System.Drawing.Color.White,
////        //        RowHeadersVisible = false,
////        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
////        //        AllowUserToAddRows = false,
////        //        ScrollBars = ScrollBars.None,
////        //        ReadOnly = true,
////        //        AllowUserToResizeColumns = false, // Prevents resizing columns
////        //        AllowUserToResizeRows = false,     // Prevents resizing rows
////        //    };

////        //    dataGridView.RowTemplate.Height = 50;
////        //    dataGridView.ColumnCount = 3;
////        //    dataGridView.Columns[0].Name = "STUDENT NAME";
////        //    dataGridView.Columns[1].Name = "ROLL NO";
////        //    dataGridView.Columns[2].Name = "BRANCH";

////        //    dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////        //    return dataGridView;
////        //}



////        private DataGridView CreateSmallDataGridView()
////        {
////            var dataGridView = new DataGridView
////            {
////                EnableHeadersVisualStyles = false,
////                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkOrchid,
////                    ForeColor = System.Drawing.Color.White,
////                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new System.Drawing.Font("Arial", 10)
////                },
////                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    BackColor = System.Drawing.Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = System.Drawing.Color.White,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
////                ReadOnly = true,
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";
////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }
////        //private void LoadBacklogData()
////        //{
////        //    // Load JSON data
////        //    var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////        //    var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");

////        //    // Filter student marks for specified student ID and failing grades (F, Ab)
////        //    var backlogs = studentMarks
////        //        .Where(m => m.StudentId == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////        //        .Join(subjectCodes,
////        //            mark => mark.SubjectCode,
////        //            subject => subject.SubjectCode,
////        //            (mark, subject) => new Backlog
////        //            {
////        //                SubjectCode = mark.SubjectCode,
////        //                SubjectName = mark.SubjectName,
////        //                Internal = mark.Internal,
////        //                External = mark.External,
////        //                Total = mark.Total,
////        //                Grade = mark.Grade,
////        //                Credits = mark.Credits,
////        //                YearSem = $"{subject.Year}-{subject.Semester}"  // Create Year-Sem string
////        //            })
////        //        .ToList();
////        //    baklogstotal = backlogs.Count;
////        //    lblTotalBacklogs.Text = backlogs.Count.ToString();

////        //    // Group backlogs by Year-Sem
////        //    groupedBacklogs = backlogs
////        //        .GroupBy(b => b.YearSem)
////        //        .OrderBy(g => g.Key) // Order groups by Year-Sem
////        //        .ToList();
////        //}
////        private void LoadBacklogData()
////        {
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");
////            var studentAttempts = LoadJsonData<StudentAttempt>("studentAttempts.json");

////            // Filter backlogs
////            var backlogs = studentMarks
////                .Where(m => m.StudentId.ToUpper() == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"
////                    })
////                .ToList();

////            baklogstotal = backlogs.Count;
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key)
////                .ToList();

////            // Filter attempts for backlog subjects
////            var backlogSubjectCodes = backlogs.Select(b => b.SubjectCode).ToList();
////            var attempts = studentAttempts
////                .Where(a => a.StudentId.ToUpper() == studentId && backlogSubjectCodes.Contains(a.SubjectCode))
////                .SelectMany(a => a.Attempts.Select(attempt => new Attempt
////                {
////                    SubjectCode = a.SubjectCode,
////                    AttemptNumber = attempt.AttemptNumber,
////                    GradePoints = attempt.GradePoints,
////                    Credits = attempt.Credits,
////                    Timestamp = attempt.Timestamp
////                }))
////                .Join(subjectCodes,
////                    attempt => attempt.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (attempt, subject) => new { attempt, YearSem = $"{subject.Year}-{subject.Semester}" })
////                .ToList();

////            groupedAttempts = attempts
////                .GroupBy(a => a.YearSem)
////                .ToDictionary(g => g.Key, g => g.Select(a => a.attempt).OrderBy(a => a.SubjectCode).ThenBy(a => a.AttemptNumber).ToList());
////        }

////        // Method to load JSON data from file
////        //private List<T> LoadJsonData<T>(string filePath)
////        //{
////        //    if (!File.Exists(filePath))
////        //    {
////        //        MessageBox.Show($"File not found: {filePath}");
////        //        return new List<T>();
////        //    }

////        //    var json = File.ReadAllText(filePath);
////        //    return JsonConvert.DeserializeObject<List<T>>(json);
////        //}

////        private List<T> LoadJsonData<T>(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show($"File not found: {filePath}");
////                return new List<T>();
////            }

////            var json = File.ReadAllText(filePath);
////            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
////        }
////        //private void GenerateExcel()
////        //{
////        //    // Configure EPPlus to use the Non-Commercial license context
////        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

////        //    using (var package = new ExcelPackage())
////        //    {
////        //        // Create a worksheet in the Excel package
////        //        var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

////        //        // Add headers to the Excel sheet
////        //        worksheet.Cells[1, 1].Value = "Subject Code";
////        //        worksheet.Cells[1, 2].Value = "Subject Name";
////        //        worksheet.Cells[1, 3].Value = "Internal";
////        //        worksheet.Cells[1, 4].Value = "External";
////        //        worksheet.Cells[1, 5].Value = "Total";
////        //        worksheet.Cells[1, 6].Value = "Grade";
////        //        worksheet.Cells[1, 7].Value = "Credits";
////        //        worksheet.Cells[1, 8].Value = "Year-Sem";

////        //        // Set header styles
////        //        using (var headerRange = worksheet.Cells[1, 1, 1, 8])
////        //        {
////        //            headerRange.Style.Font.Bold = true;
////        //            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////        //            headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkOrchid);
////        //            headerRange.Style.Font.Color.SetColor(System.Drawing.Color.White);
////        //            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////        //        }

////        //        // Fill data from grouped backlogs
////        //        int row = 2;
////        //        foreach (var group in groupedBacklogs)
////        //        {
////        //            foreach (var backlog in group)
////        //            {
////        //                worksheet.Cells[row, 1].Value = backlog.SubjectCode;
////        //                worksheet.Cells[row, 2].Value = backlog.SubjectName;
////        //                //worksheet.Cells[row, 2].Value = "";
////        //                worksheet.Cells[row, 3].Value = backlog.Internal;
////        //                worksheet.Cells[row, 4].Value = backlog.External;
////        //                worksheet.Cells[row, 5].Value = backlog.Total;
////        //                worksheet.Cells[row, 6].Value = backlog.Grade;
////        //                worksheet.Cells[row, 7].Value = backlog.Credits;
////        //                worksheet.Cells[row, 8].Value = backlog.YearSem;
////        //                row++;
////        //            }
////        //        }

////        //        // Adjust columns width to fit content
////        //        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

////        //        // Save the Excel file
////        //        var saveFileDialog = new SaveFileDialog
////        //        {
////        //            Filter = "Excel files (*.xlsx)|*.xlsx",
////        //            Title = "Save Backlog Report"
////        //        };

////        //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
////        //        {
////        //            var file = new FileInfo(saveFileDialog.FileName);
////        //            package.SaveAs(file);
////        //            MessageBox.Show("Excel report saved successfully!");
////        //        }
////        //    }
////        //}

////        private void GenerateExcel()
////        {
////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////            using (var package = new ExcelPackage())
////            {
////                var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

////                // Student Info
////                var student = GetStudentById(studentId);
////                worksheet.Cells[1, 1].Value = "Student Name";
////                worksheet.Cells[1, 2].Value = student.Name;
////                worksheet.Cells[2, 1].Value = "Roll No";
////                worksheet.Cells[2, 2].Value = student.StudentId;
////                worksheet.Cells[3, 1].Value = "Branch";
////                worksheet.Cells[3, 2].Value = GetBranchFromStudentId(studentId);
////                worksheet.Cells[4, 1].Value = "Total Backlogs";
////                worksheet.Cells[4, 2].Value = baklogstotal;

////                // Backlog Headers
////                int startRow = 6;
////                worksheet.Cells[startRow, 1].Value = "Subject Code";
////                worksheet.Cells[startRow, 2].Value = "Subject Name";
////                worksheet.Cells[startRow, 3].Value = "Internal";
////                worksheet.Cells[startRow, 4].Value = "External";
////                worksheet.Cells[startRow, 5].Value = "Total";
////                worksheet.Cells[startRow, 6].Value = "Grade";
////                worksheet.Cells[startRow, 7].Value = "Credits";
////                worksheet.Cells[startRow, 8].Value = "Year-Sem";

////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, 8])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(System.Drawing.Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Backlog Data
////                int row = startRow + 1;
////                foreach (var group in groupedBacklogs)
////                {
////                    foreach (var backlog in group)
////                    {
////                        worksheet.Cells[row, 1].Value = backlog.SubjectCode;
////                        worksheet.Cells[row, 2].Value = backlog.SubjectName;
////                        worksheet.Cells[row, 3].Value = backlog.Internal;
////                        worksheet.Cells[row, 4].Value = backlog.External;
////                        worksheet.Cells[row, 5].Value = backlog.Total;
////                        worksheet.Cells[row, 6].Value = backlog.Grade;
////                        worksheet.Cells[row, 7].Value = backlog.Credits;
////                        worksheet.Cells[row, 8].Value = backlog.YearSem;
////                        row++;
////                    }
////                }

////                // Attempt Headers
////                row++;
////                worksheet.Cells[row, 1].Value = "Attempt History";
////                worksheet.Cells[row, 1, row, 5].Merge = true;
////                worksheet.Cells[row, 1].Style.Font.Bold = true;
////                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                row++;

////                worksheet.Cells[row, 1].Value = "Subject Code";
////                worksheet.Cells[row, 2].Value = "Attempt Number";
////                worksheet.Cells[row, 3].Value = "Grade Points";
////                worksheet.Cells[row, 4].Value = "Credits";
////                worksheet.Cells[row, 5].Value = "Timestamp";

////                using (var headerRange = worksheet.Cells[row, 1, row, 5])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(System.Drawing.Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Attempt Data
////                row++;
////                foreach (var yearSem in groupedAttempts.Keys)
////                {
////                    worksheet.Cells[row, 1].Value = $"{yearSem} Attempts";
////                    worksheet.Cells[row, 1, row, 5].Merge = true;
////                    worksheet.Cells[row, 1].Style.Font.Bold = true;
////                    row++;
////                    foreach (var attempt in groupedAttempts[yearSem])
////                    {
////                        worksheet.Cells[row, 1].Value = attempt.SubjectCode;
////                        worksheet.Cells[row, 2].Value = attempt.AttemptNumber;
////                        worksheet.Cells[row, 3].Value = attempt.GradePoints;
////                        worksheet.Cells[row, 4].Value = attempt.Credits;
////                        worksheet.Cells[row, 5].Value = attempt.Timestamp;
////                        row++;
////                    }
////                }

////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
////                var saveFileDialog = new SaveFileDialog
////                {
////                    Filter = "Excel files (*.xlsx)|*.xlsx",
////                    Title = "Save Backlog Report"
////                };

////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////                {
////                    var file = new FileInfo(saveFileDialog.FileName);
////                    package.SaveAs(file);
////                    MessageBox.Show("Excel report saved successfully!");
////                }
////            }
////        }

////        private void btnDownloadPdf_Click(object sender, EventArgs e)
////        {
////            GenerateExcel();
////        }

////        private void btnBackToHome_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            admindashboard adb = new admindashboard();
////            adb.Show();

////            try
////            {
////                FormBacklockCount child = new FormBacklockCount();
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

////    }

////    // Backlog data structure
////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string YearSem { get; set; }
////        public string Credits { get; set; }
////    }

////    // Student marks data structure
////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    // Subject data structure
////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }
////    public class Student
////    {
////        public string StudentId { get; set; }
////        public string Name { get; set; }
////        //public string Branch { get; set; }
////    }
////    public class StudentAttempt
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public List<Attempt> Attempts { get; set; }
////    }

////    public class Attempt
////    {
////        public string SubjectCode { get; set; }
////        public int AttemptNumber { get; set; }
////        public string GradePoints { get; set; }
////        public string Credits { get; set; }
////        public string Timestamp { get; set; }
////    }

////}


//////```csharp
////using Newtonsoft.Json;
////using System.Collections.Generic;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using System;
////using System.IO;
////using project_RYS.Forms;
////using OfficeOpenXml.Style;
////using OfficeOpenXml;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private List<DataGridView> attemptDataGridViews;
////        private List<Label> attemptLabels;
////        private Button backButton;
////        private Button downloadButton;
////        private Label lblTotalBacklogs;
////        private Label headingLabel;
////        private const int DataGridViewWidth = 1250;
////        private const int SmallDataGridViewHeight = 80;
////        private const int Margin = 20;
////        private int baklogstotal = 0;
////        private readonly string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs;
////        private Dictionary<string, List<Attempt>> groupedAttempts;

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.studentId = studentId.ToUpper();
////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();
////            attemptDataGridViews = new List<DataGridView>();
////            attemptLabels = new List<Label>();
////            SetupForm();
////            LoadBacklogData();
////            ArrangeControls();
////        }

////        private void SetupForm()
////        {
////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.BackColor = Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Backlog Report",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backButton = new Button
////            {
////                Text = "Back To Home",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkViolet,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            backButton.FlatAppearance.BorderColor = Color.DarkOrchid;
////            backButton.MouseEnter += (s, e) => backButton.BackColor = Color.FromArgb(153, 50, 204);
////            backButton.MouseLeave += (s, e) => backButton.BackColor = Color.DarkViolet;
////            backButton.Click += new EventHandler(btnBackToHome_Click);
////            this.Controls.Add(backButton);

////            // Download Button
////            downloadButton = new Button
////            {
////                Text = "Download",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            downloadButton.FlatAppearance.BorderColor = Color.DarkViolet;
////            downloadButton.MouseEnter += (s, e) => downloadButton.BackColor = Color.FromArgb(186, 85, 211);
////            downloadButton.MouseLeave += (s, e) => downloadButton.BackColor = Color.DarkOrchid;
////            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
////            this.Controls.Add(downloadButton);

////            // Total Backlogs Label
////            lblTotalBacklogs = new Label
////            {
////                Text = $"Total Backlogs: {baklogstotal}",
////                Font = new Font("Arial", 12, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(lblTotalBacklogs);
////        }

////        private Dictionary<string, string> LoadStudentNames(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
////            }

////            try
////            {
////                var jsonData = File.ReadAllText(filePath);
////                var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);
////                return studentsList
////                    .Where(student => !string.IsNullOrEmpty(student.studentid))
////                    .ToDictionary(student => student.studentid.ToUpper(), student => student.name);
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
////            }
////        }

////        private string GetStudentName(string studentId)
////        {
////            var studentNames = LoadStudentNames("studentnames.json");
////            return studentNames.ContainsKey(studentId.ToUpper()) ? studentNames[studentId.ToUpper()] : "Unknown";
////        }

////        private class stnames
////        {
////            public string studentid { get; set; }
////            public string name { get; set; }
////        }

////        private string GetBranchFromStudentId(string studentId)
////        {
////            studentId = studentId.ToUpper();
////            if (studentId.Contains("5A66")) return "CSM";
////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
////            if (studentId.Contains("1A66")) return "CSM";
////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
////            return "Unknown";
////        }

////        private Student GetStudentById(string studentId)
////        {
////            return new Student
////            {
////                StudentId = studentId.ToUpper(),
////                Name = GetStudentName(studentId)
////            };
////        }

////        private void ArrangeControls()
////        {
////            int startY = 20;

////            // Center heading
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            startY = headingLabel.Bottom + Margin;

////            // Back button
////            backButton.Location = new Point(20, startY);
////            startY = backButton.Bottom + Margin;

////            // Small DataGridView
////            var smallDataGridView = CreateSmallDataGridView();
////            var student = GetStudentById(studentId);
////            if (student != null)
////            {
////                smallDataGridView.Rows.Add(student.Name, student.StudentId, GetBranchFromStudentId(studentId));
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                dataGridViews.Add(smallDataGridView);
////                this.Controls.Add(smallDataGridView);
////                startY = smallDataGridView.Bottom + Margin;
////            }
////            else
////            {
////                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }

////            // Backlog and attempt DataGridViews
////            DisplayYearSemDataGrids(ref startY);

////            // Total backlogs and download button
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            downloadButton.Location = new Point(lblTotalBacklogs.Right + Margin, startY);
////        }

////        private void DisplayYearSemDataGrids(ref int startY)
////        {
////            foreach (var group in groupedBacklogs)
////            {
////                // Backlog Label
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Backlogs",
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = Color.DarkOrchid
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                labels.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                // Backlog DataGridView
////                var backlogDataGridView = CreateBacklogDataGridView(group);
////                backlogDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(backlogDataGridView);
////                dataGridViews.Add(backlogDataGridView);
////                startY += backlogDataGridView.Height + Margin;

////                // Attempt Label
////                var attemptLabel = new Label
////                {
////                    Text = $"{group.Key} Attempt History",
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = Color.DarkOrchid
////                };
////                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
////                this.Controls.Add(attemptLabel);
////                attemptLabels.Add(attemptLabel);
////                startY += attemptLabel.Height + Margin;

////                // Attempt DataGridView
////                var attemptDataGridView = CreateAttemptDataGridView(group.Key);
////                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(attemptDataGridView);
////                attemptDataGridViews.Add(attemptDataGridView);
////                startY += attemptDataGridView.Height + Margin;
////            }
////        }

////        private DataGridView CreateBacklogDataGridView(IGrouping<string, Backlog> group)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_{group.Key}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 7,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
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
////                    BackColor = Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Subject Name";
////            dataGridView.Columns[2].Name = "Internal";
////            dataGridView.Columns[3].Name = "External";
////            dataGridView.Columns[4].Name = "Total";
////            dataGridView.Columns[5].Name = "Grade";
////            dataGridView.Columns[6].Name = "Credits";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 500;
////            dataGridView.Columns[2].Width = 120;
////            dataGridView.Columns[3].Width = 120;
////            dataGridView.Columns[4].Width = 100;
////            dataGridView.Columns[5].Width = 120;
////            dataGridView.Columns[6].Width = 120;

////            foreach (var backlog in group)
////            {
////                dataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                dataGridView.Rows.Add("No backlogs", "", "", "", "", "", "");
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            return dataGridView;
////        }

////        private DataGridView CreateAttemptDataGridView(string yearSem)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_attempts_{yearSem}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 5,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
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
////                    BackColor = Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Attempt Number";
////            dataGridView.Columns[2].Name = "Grade Points";
////            dataGridView.Columns[3].Name = "Credits";
////            dataGridView.Columns[4].Name = "Timestamp";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 150;
////            dataGridView.Columns[2].Width = 150;
////            dataGridView.Columns[3].Width = 150;
////            dataGridView.Columns[4].Width = 650;

////            if (groupedAttempts.ContainsKey(yearSem))
////            {
////                foreach (var attempt in groupedAttempts[yearSem])
////                {
////                    dataGridView.Rows.Add(attempt.SubjectCode, attempt.AttemptNumber, attempt.GradePoints, attempt.Credits, attempt.Timestamp);
////                }
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                dataGridView.Rows.Add("No attempts", "", "", "", "");
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            return dataGridView;
////        }

////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 50;
////            int headerHeight = 40;
////            int totalHeight = headerHeight + rowCount * rowHeight;
////            return totalHeight < 90 ? 90 : totalHeight;
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
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new Font("Arial", 10),
////                    BackColor = Color.White
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
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";
////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }

////        private void LoadBacklogData()
////        {
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");
////            var studentAttempts = LoadJsonData<StudentAttempt>("studentAttempts.json");

////            // Filter backlogs
////            var backlogs = studentMarks
////                .Where(m => m.StudentId.ToUpper() == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"
////                    })
////                .ToList();

////            baklogstotal = backlogs.Count;
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key)
////                .ToList();

////            // Filter attempts for backlog subjects
////            var backlogSubjectCodes = backlogs.Select(b => b.SubjectCode).ToList();
////            var attempts = studentAttempts
////                .Where(a => a.StudentId.ToUpper() == studentId && backlogSubjectCodes.Contains(a.SubjectCode))
////                .SelectMany(a => a.Attempts.Select(attempt => new Attempt
////                {
////                    SubjectCode = a.SubjectCode,
////                    AttemptNumber = attempt.AttemptNumber,
////                    GradePoints = attempt.GradePoints,
////                    Credits = attempt.Credits,
////                    Timestamp = attempt.Timestamp
////                }))
////                .Join(subjectCodes,
////                    attempt => attempt.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (attempt, subject) => new { attempt, YearSem = $"{subject.Year}-{subject.Semester}" })
////                .ToList();

////            groupedAttempts = attempts
////                .GroupBy(a => a.YearSem)
////                .ToDictionary(g => g.Key, g => g.Select(a => a.attempt).OrderBy(a => a.SubjectCode).ThenBy(a => a.AttemptNumber).ToList());

////            // Log for debugging
////            Console.WriteLine($"Backlogs: {backlogs.Count}, Attempts: {attempts.Count}");
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

////        private void GenerateExcel()
////        {
////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////            using (var package = new ExcelPackage())
////            {
////                var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

////                // Student Info
////                var student = GetStudentById(studentId);
////                worksheet.Cells[1, 1].Value = "Student Name";
////                worksheet.Cells[1, 2].Value = student.Name;
////                worksheet.Cells[2, 1].Value = "Roll No";
////                worksheet.Cells[2, 2].Value = student.StudentId;
////                worksheet.Cells[3, 1].Value = "Branch";
////                worksheet.Cells[3, 2].Value = GetBranchFromStudentId(studentId);
////                worksheet.Cells[4, 1].Value = "Total Backlogs";
////                worksheet.Cells[4, 2].Value = baklogstotal;

////                // Backlog Headers
////                int startRow = 6;
////                worksheet.Cells[startRow, 1].Value = "Subject Code";
////                worksheet.Cells[startRow, 2].Value = "Subject Name";
////                worksheet.Cells[startRow, 3].Value = "Internal";
////                worksheet.Cells[startRow, 4].Value = "External";
////                worksheet.Cells[startRow, 5].Value = "Total";
////                worksheet.Cells[startRow, 6].Value = "Grade";
////                worksheet.Cells[startRow, 7].Value = "Credits";
////                worksheet.Cells[startRow, 8].Value = "Year-Sem";

////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, 8])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Backlog Data
////                int row = startRow + 1;
////                foreach (var group in groupedBacklogs)
////                {
////                    foreach (var backlog in group)
////                    {
////                        worksheet.Cells[row, 1].Value = backlog.SubjectCode;
////                        worksheet.Cells[row, 2].Value = backlog.SubjectName;
////                        worksheet.Cells[row, 3].Value = backlog.Internal;
////                        worksheet.Cells[row, 4].Value = backlog.External;
////                        worksheet.Cells[row, 5].Value = backlog.Total;
////                        worksheet.Cells[row, 6].Value = backlog.Grade;
////                        worksheet.Cells[row, 7].Value = backlog.Credits;
////                        worksheet.Cells[row, 8].Value = backlog.YearSem;
////                        row++;
////                    }
////                }

////                // Attempt Headers
////                row++;
////                worksheet.Cells[row, 1].Value = "Attempt History";
////                worksheet.Cells[row, 1, row, 5].Merge = true;
////                worksheet.Cells[row, 1].Style.Font.Bold = true;
////                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                row++;

////                worksheet.Cells[row, 1].Value = "Subject Code";
////                worksheet.Cells[row, 2].Value = "Attempt Number";
////                worksheet.Cells[row, 3].Value = "Grade Points";
////                worksheet.Cells[row, 4].Value = "Credits";
////                worksheet.Cells[row, 5].Value = "Timestamp";

////                using (var headerRange = worksheet.Cells[row, 1, row, 5])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Attempt Data
////                row++;
////                foreach (var yearSem in groupedAttempts.Keys)
////                {
////                    worksheet.Cells[row, 1].Value = $"{yearSem} Attempts";
////                    worksheet.Cells[row, 1, row, 5].Merge = true;
////                    worksheet.Cells[row, 1].Style.Font.Bold = true;
////                    row++;
////                    foreach (var attempt in groupedAttempts[yearSem])
////                    {
////                        worksheet.Cells[row, 1].Value = attempt.SubjectCode;
////                        worksheet.Cells[row, 2].Value = attempt.AttemptNumber;
////                        worksheet.Cells[row, 3].Value = attempt.GradePoints;
////                        worksheet.Cells[row, 4].Value = attempt.Credits;
////                        worksheet.Cells[row, 5].Value = attempt.Timestamp;
////                        row++;
////                    }
////                }

////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
////                var saveFileDialog = new SaveFileDialog
////                {
////                    Filter = "Excel files (*.xlsx)|*.xlsx",
////                    Title = "Save Backlog Report"
////                };

////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////                {
////                    var file = new FileInfo(saveFileDialog.FileName);
////                    package.SaveAs(file);
////                    MessageBox.Show("Excel report saved successfully!");
////                }
////            }
////        }

////        private void btnDownloadPdf_Click(object sender, EventArgs e)
////        {
////            GenerateExcel();
////        }

////        private void btnBackToHome_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            admindashboard adb = new admindashboard();
////            adb.Show();
////            try
////            {
////                FormBacklockCount child = new FormBacklockCount();
////                child.TopLevel = false;
////                child.FormBorderStyle = FormBorderStyle.None;
////                child.Dock = DockStyle.Fill;
////                adb.panelDesktopPane.Controls.Clear();
////                adb.panelDesktopPane.Controls.Add(child);
////                child.Show();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }
////    }

////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string YearSem { get; set; }
////        public string Credits { get; set; }
////    }

////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }

////    public class Student
////    {
////        public string StudentId { get; set; }
////        public string Name { get; set; }
////    }

////    public class StudentAttempt
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public List<Attempt> Attempts { get; set; }
////    }

////    public class Attempt
////    {
////        public string SubjectCode { get; set; }
////        public int AttemptNumber { get; set; }
////        public string GradePoints { get; set; }
////        public string Credits { get; set; }
////        public string Timestamp { get; set; }
////    }
////}

////using Newtonsoft.Json;
////using System.Collections.Generic;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using System;
////using System.IO;
////using project_RYS.Forms;
////using OfficeOpenXml.Style;
////using OfficeOpenXml;

////namespace project_RYS
////{
////    public partial class BacklogResultsForm : Form
////    {
////        private List<DataGridView> dataGridViews;
////        private List<Label> labels;
////        private List<DataGridView> attemptDataGridViews;
////        private List<Label> attemptLabels;
////        private Button backButton;
////        private Button downloadButton;
////        private Label lblTotalBacklogs;
////        private Label headingLabel;
////        private const int DataGridViewWidth = 1250;
////        private const int SmallDataGridViewHeight = 80;
////        private const int Margin = 20;
////        private int baklogstotal = 0;
////        private readonly string studentId;
////        private List<IGrouping<string, Backlog>> groupedBacklogs;
////        private Dictionary<string, List<Attempt>> groupedAttempts;

////        public BacklogResultsForm(string studentId)
////        {
////            InitializeComponent();
////            this.studentId = studentId.ToUpper();
////            dataGridViews = new List<DataGridView>();
////            labels = new List<Label>();
////            attemptDataGridViews = new List<DataGridView>();
////            attemptLabels = new List<Label>();
////            SetupForm();
////            LoadBacklogData();
////            ArrangeControls();
////        }

////        private void SetupForm()
////        {
////            this.Size = new Size(1300, 800);
////            this.AutoScroll = true;
////            this.BackColor = Color.FromArgb(240, 240, 240);

////            // Heading Label
////            headingLabel = new Label
////            {
////                Text = "Backlog Report",
////                Font = new Font("Arial", 16, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(headingLabel);

////            // Back Button
////            backButton = new Button
////            {
////                Text = "Back To Home",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkViolet,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            backButton.FlatAppearance.BorderColor = Color.DarkOrchid;
////            backButton.MouseEnter += (s, e) => backButton.BackColor = Color.FromArgb(153, 50, 204);
////            backButton.MouseLeave += (s, e) => backButton.BackColor = Color.DarkViolet;
////            backButton.Click += new EventHandler(btnBackToHome_Click);
////            this.Controls.Add(backButton);

////            // Download Button
////            downloadButton = new Button
////            {
////                Text = "Download",
////                Width = 200,
////                Height = 40,
////                FlatStyle = FlatStyle.Flat,
////                BackColor = Color.DarkOrchid,
////                ForeColor = Color.White,
////                Font = new Font("Arial", 10, FontStyle.Bold)
////            };
////            downloadButton.FlatAppearance.BorderColor = Color.DarkViolet;
////            downloadButton.MouseEnter += (s, e) => downloadButton.BackColor = Color.FromArgb(186, 85, 211);
////            downloadButton.MouseLeave += (s, e) => downloadButton.BackColor = Color.DarkOrchid;
////            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
////            this.Controls.Add(downloadButton);

////            // Total Backlogs Label
////            lblTotalBacklogs = new Label
////            {
////                Text = $"Total Backlogs: {baklogstotal}",
////                Font = new Font("Arial", 12, FontStyle.Bold),
////                AutoSize = true,
////                ForeColor = Color.DarkOrchid
////            };
////            this.Controls.Add(lblTotalBacklogs);
////        }

////        private Dictionary<string, string> LoadStudentNames(string filePath)
////        {
////            if (!File.Exists(filePath))
////            {
////                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
////            }

////            try
////            {
////                var jsonData = File.ReadAllText(filePath);
////                var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);
////                return studentsList
////                    .Where(student => !string.IsNullOrEmpty(student.studentid))
////                    .ToDictionary(student => student.studentid.ToUpper(), student => student.name);
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                return new Dictionary<string, string>();
////            }
////        }

////        private string GetStudentName(string studentId)
////        {
////            var studentNames = LoadStudentNames("studentnames.json");
////            return studentNames.ContainsKey(studentId.ToUpper()) ? studentNames[studentId.ToUpper()] : "Unknown";
////        }

////        private class stnames
////        {
////            public string studentid { get; set; }
////            public string name { get; set; }
////        }

////        private string GetBranchFromStudentId(string studentId)
////        {
////            studentId = studentId.ToUpper();
////            if (studentId.Contains("5A66")) return "CSM";
////            if (studentId.Contains("1A05") || studentId.Contains("5A05")) return "CSE";
////            if (studentId.Contains("1A66")) return "CSM";
////            if (studentId.Contains("1A01") || studentId.Contains("5A01")) return "CE";
////            return "Unknown";
////        }

////        private Student GetStudentById(string studentId)
////        {
////            return new Student
////            {
////                StudentId = studentId.ToUpper(),
////                Name = GetStudentName(studentId)
////            };
////        }

////        private void ArrangeControls()
////        {
////            int startY = 20;

////            // Center heading
////            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
////            startY = headingLabel.Bottom + Margin;

////            // Back button
////            backButton.Location = new Point(20, startY);
////            startY = backButton.Bottom + Margin;

////            // Small DataGridView
////            var smallDataGridView = CreateSmallDataGridView();
////            var student = GetStudentById(studentId);
////            if (student != null)
////            {
////                smallDataGridView.Rows.Add(student.Name, student.StudentId, GetBranchFromStudentId(studentId));
////                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                dataGridViews.Add(smallDataGridView);
////                this.Controls.Add(smallDataGridView);
////                startY = smallDataGridView.Bottom + Margin;
////            }
////            else
////            {
////                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }

////            // Backlog and attempt DataGridViews
////            DisplayYearSemDataGrids(ref startY);

////            // Total backlogs and download button
////            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
////            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
////            downloadButton.Location = new Point(lblTotalBacklogs.Right + Margin, startY);
////        }

////        private void DisplayYearSemDataGrids(ref int startY)
////        {
////            foreach (var group in groupedBacklogs)
////            {
////                // Backlog Label
////                var backlogLabel = new Label
////                {
////                    Text = $"{group.Key} Backlogs",
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = Color.DarkOrchid
////                };
////                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
////                this.Controls.Add(backlogLabel);
////                labels.Add(backlogLabel);
////                startY += backlogLabel.Height + Margin;

////                // Backlog DataGridView
////                var backlogDataGridView = CreateBacklogDataGridView(group);
////                backlogDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(backlogDataGridView);
////                dataGridViews.Add(backlogDataGridView);
////                startY += backlogDataGridView.Height + Margin;

////                // Attempt Label
////                var attemptLabel = new Label
////                {
////                    Text = $"{group.Key} Attempt History",
////                    Font = new Font("Arial", 12, FontStyle.Bold),
////                    AutoSize = true,
////                    ForeColor = Color.DarkOrchid
////                };
////                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
////                this.Controls.Add(attemptLabel);
////                attemptLabels.Add(attemptLabel);
////                startY += attemptLabel.Height + Margin;

////                // Attempt DataGridView
////                var attemptDataGridView = CreateAttemptDataGridView(group.Key);
////                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
////                this.Controls.Add(attemptDataGridView);
////                attemptDataGridViews.Add(attemptDataGridView);
////                startY += attemptDataGridView.Height + Margin;
////            }
////        }

////        private DataGridView CreateBacklogDataGridView(IGrouping<string, Backlog> group)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_{group.Key}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 7,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
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
////                    BackColor = Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Subject Name";
////            dataGridView.Columns[2].Name = "Internal";
////            dataGridView.Columns[3].Name = "External";
////            dataGridView.Columns[4].Name = "Total";
////            dataGridView.Columns[5].Name = "Grade";
////            dataGridView.Columns[6].Name = "Credits";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 500;
////            dataGridView.Columns[2].Width = 120;
////            dataGridView.Columns[3].Width = 120;
////            dataGridView.Columns[4].Width = 100;
////            dataGridView.Columns[5].Width = 120;
////            dataGridView.Columns[6].Width = 120;

////            foreach (var backlog in group)
////            {
////                dataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                dataGridView.Rows.Add("No backlogs", "", "", "", "", "", "");
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            return dataGridView;
////        }

////        private DataGridView CreateAttemptDataGridView(string yearSem)
////        {
////            var dataGridView = new DataGridView
////            {
////                Name = $"dgv_attempts_{yearSem}",
////                Width = DataGridViewWidth,
////                AutoGenerateColumns = false,
////                ColumnCount = 6,
////                RowHeadersVisible = false,
////                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
////                AllowUserToAddRows = false,
////                ScrollBars = ScrollBars.None,
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
////                    BackColor = Color.DarkViolet
////                },
////                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
////                GridColor = Color.White
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnHeadersHeight = 40;

////            dataGridView.Columns[0].Name = "Subject Code";
////            dataGridView.Columns[1].Name = "Subject Name";
////            dataGridView.Columns[2].Name = "Attempt Number";
////            dataGridView.Columns[3].Name = "Grade Points";
////            dataGridView.Columns[4].Name = "Credits";
////            dataGridView.Columns[5].Name = "Timestamp";

////            dataGridView.Columns[0].Width = 150;
////            dataGridView.Columns[1].Width = 300;
////            dataGridView.Columns[2].Width = 150;
////            dataGridView.Columns[3].Width = 150;
////            dataGridView.Columns[4].Width = 150;
////            dataGridView.Columns[5].Width = 450;

////            if (groupedAttempts.ContainsKey(yearSem))
////            {
////                foreach (var attempt in groupedAttempts[yearSem])
////                {
////                    dataGridView.Rows.Add(attempt.SubjectCode, attempt.SubjectName, attempt.AttemptNumber, attempt.GradePoints, attempt.Credits, attempt.Timestamp);
////                }
////            }

////            if (dataGridView.Rows.Count == 0)
////            {
////                dataGridView.Rows.Add("No attempts", "", "", "", "", "");
////            }

////            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
////            return dataGridView;
////        }

////        private int CalculateDataGridViewHeight(int rowCount)
////        {
////            int rowHeight = 50;
////            int headerHeight = 40;
////            int totalHeight = headerHeight + rowCount * rowHeight;
////            return totalHeight < 90 ? 90 : totalHeight;
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
////                    Font = new Font("Arial", 10, FontStyle.Bold),
////                    Alignment = DataGridViewContentAlignment.MiddleCenter
////                },
////                ColumnHeadersHeight = 40,
////                RowsDefaultCellStyle = new DataGridViewCellStyle
////                {
////                    Font = new Font("Arial", 10),
////                    BackColor = Color.White
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
////                AllowUserToResizeColumns = false,
////                AllowUserToResizeRows = false
////            };

////            dataGridView.RowTemplate.Height = 50;
////            dataGridView.ColumnCount = 3;
////            dataGridView.Columns[0].Name = "STUDENT NAME";
////            dataGridView.Columns[1].Name = "ROLL NO";
////            dataGridView.Columns[2].Name = "BRANCH";
////            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
////            return dataGridView;
////        }

////        private void LoadBacklogData()
////        {
////            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
////            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");
////            var studentAttempts = LoadJsonData<StudentAttempt>("studentAttempts.json");

////            // Filter backlogs
////            var backlogs = studentMarks
////                .Where(m => m.StudentId.ToUpper() == studentId && (m.Grade == "F" || m.Grade == "Ab"))
////                .Join(subjectCodes,
////                    mark => mark.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (mark, subject) => new Backlog
////                    {
////                        SubjectCode = mark.SubjectCode,
////                        SubjectName = mark.SubjectName,
////                        Internal = mark.Internal,
////                        External = mark.External,
////                        Total = mark.Total,
////                        Grade = mark.Grade,
////                        Credits = mark.Credits,
////                        YearSem = $"{subject.Year}-{subject.Semester}"
////                    })
////                .ToList();

////            baklogstotal = backlogs.Count;
////            groupedBacklogs = backlogs
////                .GroupBy(b => b.YearSem)
////                .OrderBy(g => g.Key)
////                .ToList();

////            // Combine first failed attempts from studentMarks and subsequent attempts from studentAttempts
////            var backlogSubjectCodes = backlogs.Select(b => b.SubjectCode).ToList();

////            // First attempts (failed) from studentMarks
////            var firstAttempts = backlogs
////                .Select(b => new
////                {
////                    Attempt = new Attempt
////                    {
////                        SubjectCode = b.SubjectCode,
////                        SubjectName = b.SubjectName,
////                        AttemptNumber = 1,
////                        GradePoints = "0",
////                        Credits = "0",
////                        Timestamp = "Initial Attempt"
////                    },
////                    YearSem = b.YearSem
////                })
////                .ToList();

////            // Subsequent attempts from studentAttempts
////            var subsequentAttempts = studentAttempts
////                .Where(a => a.StudentId.ToUpper() == studentId && backlogSubjectCodes.Contains(a.SubjectCode))
////                .SelectMany(a => a.Attempts.Select(attempt => new
////                {
////                    Attempt = new Attempt
////                    {
////                        SubjectCode = a.SubjectCode,
////                        SubjectName = studentMarks.FirstOrDefault(m => m.SubjectCode == a.SubjectCode)?.SubjectName ?? "Unknown",
////                        AttemptNumber = attempt.AttemptNumber + 1, // Increment to account for first attempt
////                        GradePoints = attempt.GradePoints,
////                        Credits = attempt.Credits,
////                        Timestamp = attempt.Timestamp
////                    },
////                    SubjectCode = a.SubjectCode
////                }))
////                .Join(subjectCodes,
////                    a => a.SubjectCode,
////                    subject => subject.SubjectCode,
////                    (a, subject) => new
////                    {
////                        a.Attempt,
////                        YearSem = $"{subject.Year}-{subject.Semester}"
////                    })
////                .ToList();

////            // Combine all attempts
////            var allAttempts = firstAttempts.Concat(subsequentAttempts).ToList();

////            groupedAttempts = allAttempts
////                .GroupBy(a => a.YearSem)
////                .ToDictionary(g => g.Key, g => g.Select(a => a.Attempt).OrderBy(a => a.SubjectCode).ThenBy(a => a.AttemptNumber).ToList());

////            // Log for debugging
////            Console.WriteLine($"Backlogs: {backlogs.Count}, Attempts: {allAttempts.Count}");
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

////        private void GenerateExcel()
////        {
////            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
////            using (var package = new ExcelPackage())
////            {
////                var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

////                // Student Info
////                var student = GetStudentById(studentId);
////                worksheet.Cells[1, 1].Value = "Student Name";
////                worksheet.Cells[1, 2].Value = student.Name;
////                worksheet.Cells[2, 1].Value = "Roll No";
////                worksheet.Cells[2, 2].Value = student.StudentId;
////                worksheet.Cells[3, 1].Value = "Branch";
////                worksheet.Cells[3, 2].Value = GetBranchFromStudentId(studentId);
////                worksheet.Cells[4, 1].Value = "Total Backlogs";
////                worksheet.Cells[4, 2].Value = baklogstotal;

////                // Backlog Headers
////                int startRow = 6;
////                worksheet.Cells[startRow, 1].Value = "Subject Code";
////                worksheet.Cells[startRow, 2].Value = "Subject Name";
////                worksheet.Cells[startRow, 3].Value = "Internal";
////                worksheet.Cells[startRow, 4].Value = "External";
////                worksheet.Cells[startRow, 5].Value = "Total";
////                worksheet.Cells[startRow, 6].Value = "Grade";
////                worksheet.Cells[startRow, 7].Value = "Credits";
////                worksheet.Cells[startRow, 8].Value = "Year-Sem";

////                using (var headerRange = worksheet.Cells[startRow, 1, startRow, 8])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Backlog Data
////                int row = startRow + 1;
////                foreach (var group in groupedBacklogs)
////                {
////                    foreach (var backlog in group)
////                    {
////                        worksheet.Cells[row, 1].Value = backlog.SubjectCode;
////                        worksheet.Cells[row, 2].Value = backlog.SubjectName;
////                        worksheet.Cells[row, 3].Value = backlog.Internal;
////                        worksheet.Cells[row, 4].Value = backlog.External;
////                        worksheet.Cells[row, 5].Value = backlog.Total;
////                        worksheet.Cells[row, 6].Value = backlog.Grade;
////                        worksheet.Cells[row, 7].Value = backlog.Credits;
////                        worksheet.Cells[row, 8].Value = backlog.YearSem;
////                        row++;
////                    }
////                }

////                // Attempt Headers
////                row++;
////                worksheet.Cells[row, 1].Value = "Attempt History";
////                worksheet.Cells[row, 1, row, 6].Merge = true;
////                worksheet.Cells[row, 1].Style.Font.Bold = true;
////                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                row++;

////                worksheet.Cells[row, 1].Value = "Subject Code";
////                worksheet.Cells[row, 2].Value = "Subject Name";
////                worksheet.Cells[row, 3].Value = "Attempt Number";
////                worksheet.Cells[row, 4].Value = "Grade Points";
////                worksheet.Cells[row, 5].Value = "Credits";
////                worksheet.Cells[row, 6].Value = "Timestamp";

////                using (var headerRange = worksheet.Cells[row, 1, row, 6])
////                {
////                    headerRange.Style.Font.Bold = true;
////                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
////                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
////                    headerRange.Style.Font.Color.SetColor(Color.White);
////                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
////                }

////                // Attempt Data
////                row++;
////                foreach (var yearSem in groupedAttempts.Keys)
////                {
////                    worksheet.Cells[row, 1].Value = $"{yearSem} Attempts";
////                    worksheet.Cells[row, 1, row, 6].Merge = true;
////                    worksheet.Cells[row, 1].Style.Font.Bold = true;
////                    row++;
////                    foreach (var attempt in groupedAttempts[yearSem])
////                    {
////                        worksheet.Cells[row, 1].Value = attempt.SubjectCode;
////                        worksheet.Cells[row, 2].Value = attempt.SubjectName;
////                        worksheet.Cells[row, 3].Value = attempt.AttemptNumber;
////                        worksheet.Cells[row, 4].Value = attempt.GradePoints;
////                        worksheet.Cells[row, 5].Value = attempt.Credits;
////                        worksheet.Cells[row, 6].Value = attempt.Timestamp;
////                        row++;
////                    }
////                }

////                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
////                var saveFileDialog = new SaveFileDialog
////                {
////                    Filter = "Excel files (*.xlsx)|*.xlsx",
////                    Title = "Save Backlog Report"
////                };

////                if (saveFileDialog.ShowDialog() == DialogResult.OK)
////                {
////                    var file = new FileInfo(saveFileDialog.FileName);
////                    package.SaveAs(file);
////                    MessageBox.Show("Excel report saved successfully!");
////                }
////            }
////        }

////        private void btnDownloadPdf_Click(object sender, EventArgs e)
////        {
////            GenerateExcel();
////        }

////        private void btnBackToHome_Click(object sender, EventArgs e)
////        {
////            this.Hide();
////            admindashboard adb = new admindashboard();
////            adb.Show();
////            try
////            {
////                FormBacklockCount child = new FormBacklockCount();
////                child.TopLevel = false;
////                child.FormBorderStyle = FormBorderStyle.None;
////                child.Dock = DockStyle.Fill;
////                adb.panelDesktopPane.Controls.Clear();
////                adb.panelDesktopPane.Controls.Add(child);
////                child.Show();
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Failed to open ChildForm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }
////    }

////    public class Backlog
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string YearSem { get; set; }
////        public string Credits { get; set; }
////    }

////    public class StudentMarks
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public string Internal { get; set; }
////        public string External { get; set; }
////        public string Total { get; set; }
////        public string Grade { get; set; }
////        public string Credits { get; set; }
////    }

////    public class Subject
////    {
////        public string SubjectCode { get; set; }
////        public string Year { get; set; }
////        public string Semester { get; set; }
////    }

////    public class Student
////    {
////        public string StudentId { get; set; }
////        public string Name { get; set; }
////    }

////    public class StudentAttempt
////    {
////        public string StudentId { get; set; }
////        public string SubjectCode { get; set; }
////        public List<Attempt> Attempts { get; set; }
////    }

////    public class Attempt
////    {
////        public string SubjectCode { get; set; }
////        public string SubjectName { get; set; }
////        public int AttemptNumber { get; set; }
////        public string GradePoints { get; set; }
////        public string Credits { get; set; }
////        public string Timestamp { get; set; }
////    }
////}





//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using System;
//using System.IO;
//using project_RYS.Forms;
//using OfficeOpenXml.Style;
//using OfficeOpenXml;

//namespace project_RYS
//{
//    public partial class BacklogResultsForm : Form
//    {
//        private List<DataGridView> dataGridViews;
//        private List<Label> labels;
//        private List<DataGridView> attemptDataGridViews;
//        private List<Label> attemptLabels;
//        private Button backButton;
//        private Button downloadButton;
//        private Label lblTotalBacklogs;
//        private Label headingLabel;
//        private const int DataGridViewWidth = 1250;
//        private const int SmallDataGridViewHeight = 80;
//        private const int Margin = 20;
//        private int baklogstotal = 0;
//        private readonly string studentId;
//        private List<IGrouping<string, Backlog>> groupedBacklogs;
//        private Dictionary<string, List<Attempt>> groupedAttempts;

//        public BacklogResultsForm(string studentId)
//        {
//            InitializeComponent();
//            this.studentId = studentId.ToUpper();
//            dataGridViews = new List<DataGridView>();
//            labels = new List<Label>();
//            attemptDataGridViews = new List<DataGridView>();
//            attemptLabels = new List<Label>();
//            SetupForm();
//            LoadBacklogData();
//            ArrangeControls();
//        }

//        private void SetupForm()
//        {
//            this.Size = new Size(1300, 800);
//            this.AutoScroll = true;
//            this.BackColor = Color.FromArgb(240, 240, 240);

//            // Heading Label
//            headingLabel = new Label
//            {
//                Text = "Backlog Report",
//                Font = new Font("Arial", 16, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(headingLabel);

//            // Back Button
//            backButton = new Button
//            {
//                Text = "Back To Home",
//                Width = 200,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkViolet,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            backButton.FlatAppearance.BorderColor = Color.DarkOrchid;
//            backButton.MouseEnter += (s, e) => backButton.BackColor = Color.FromArgb(153, 50, 204);
//            backButton.MouseLeave += (s, e) => backButton.BackColor = Color.DarkViolet;
//            backButton.Click += new EventHandler(btnBackToHome_Click);
//            this.Controls.Add(backButton);

//            // Download Button
//            downloadButton = new Button
//            {
//                Text = "Download",
//                Width = 200,
//                Height = 40,
//                FlatStyle = FlatStyle.Flat,
//                BackColor = Color.DarkOrchid,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            downloadButton.FlatAppearance.BorderColor = Color.DarkViolet;
//            downloadButton.MouseEnter += (s, e) => downloadButton.BackColor = Color.FromArgb(186, 85, 211);
//            downloadButton.MouseLeave += (s, e) => downloadButton.BackColor = Color.DarkOrchid;
//            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
//            this.Controls.Add(downloadButton);

//            // Total Backlogs Label
//            lblTotalBacklogs = new Label
//            {
//                Text = $"Total Backlogs: {baklogstotal}",
//                Font = new Font("Arial", 12, FontStyle.Bold),
//                AutoSize = true,
//                ForeColor = Color.DarkOrchid
//            };
//            this.Controls.Add(lblTotalBacklogs);
//        }

//        private Dictionary<string, string> LoadStudentNames(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new Dictionary<string, string>();
//            }

//            try
//            {
//                var jsonData = File.ReadAllText(filePath);
//                var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);
//                return studentsList
//                    .Where(student => !string.IsNullOrEmpty(student.studentid))
//                    .ToDictionary(student => student.studentid.ToUpper(), student => student.name);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new Dictionary<string, string>();
//            }
//        }

//        private string GetStudentName(string studentId)
//        {
//            var studentNames = LoadStudentNames("studentnames.json");
//            return studentNames.ContainsKey(studentId.ToUpper()) ? studentNames[studentId.ToUpper()] : "Unknown";
//        }

//        private class stnames
//        {
//            public string studentid { get; set; }
//            public string name { get; set; }
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

//        private Student GetStudentById(string studentId)
//        {
//            return new Student
//            {
//                StudentId = studentId.ToUpper(),
//                Name = GetStudentName(studentId)
//            };
//        }

//        private void ArrangeControls()
//        {
//            int startY = 20;

//            // Center heading
//            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
//            startY = headingLabel.Bottom + Margin;

//            // Back button
//            backButton.Location = new Point(20, startY);
//            startY = backButton.Bottom + Margin;

//            // Small DataGridView
//            var smallDataGridView = CreateSmallDataGridView();
//            var student = GetStudentById(studentId);
//            if (student != null)
//            {
//                smallDataGridView.Rows.Add(student.Name, student.StudentId, GetBranchFromStudentId(studentId));
//                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//                dataGridViews.Add(smallDataGridView);
//                this.Controls.Add(smallDataGridView);
//                startY = smallDataGridView.Bottom + Margin;
//            }
//            else
//            {
//                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//            // Backlog and attempt DataGridViews
//            DisplayYearSemDataGrids(ref startY);

//            // Total backlogs and download button
//            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
//            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
//            downloadButton.Location = new Point(lblTotalBacklogs.Right + Margin, startY);
//        }

//        private void DisplayYearSemDataGrids(ref int startY)
//        {
//            foreach (var group in groupedBacklogs)
//            {
//                // Backlog Label
//                var backlogLabel = new Label
//                {
//                    Text = $"{group.Key} Backlogs",
//                    Font = new Font("Arial", 12, FontStyle.Bold),
//                    AutoSize = true,
//                    ForeColor = Color.DarkOrchid
//                };
//                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
//                this.Controls.Add(backlogLabel);
//                labels.Add(backlogLabel);
//                startY += backlogLabel.Height + Margin;

//                // Backlog DataGridView
//                var backlogDataGridView = CreateBacklogDataGridView(group);
//                backlogDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//                this.Controls.Add(backlogDataGridView);
//                dataGridViews.Add(backlogDataGridView);
//                startY += backlogDataGridView.Height + Margin;

//                // Attempt Label
//                var attemptLabel = new Label
//                {
//                    Text = $"{group.Key} Attempt History",
//                    Font = new Font("Arial", 12, FontStyle.Bold),
//                    AutoSize = true,
//                    ForeColor = Color.DarkOrchid
//                };
//                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
//                this.Controls.Add(attemptLabel);
//                attemptLabels.Add(attemptLabel);
//                startY += attemptLabel.Height + Margin;

//                // Attempt DataGridView
//                var attemptDataGridView = CreateAttemptDataGridView(group.Key);
//                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//                this.Controls.Add(attemptDataGridView);
//                attemptDataGridViews.Add(attemptDataGridView);
//                startY += attemptDataGridView.Height + Margin;
//            }

//            // Display attempt tables for Year-Sem not in backlogs
//            var backlogYearSems = groupedBacklogs.Select(g => g.Key).ToList();
//            var attemptYearSems = groupedAttempts.Keys.Where(ys => !backlogYearSems.Contains(ys)).OrderBy(ys => ys).ToList();
//            foreach (var yearSem in attemptYearSems)
//            {
//                // Attempt Label
//                var attemptLabel = new Label
//                {
//                    Text = $"{yearSem} Attempt History",
//                    Font = new Font("Arial", 12, FontStyle.Bold),
//                    AutoSize = true,
//                    ForeColor = Color.DarkOrchid
//                };
//                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
//                this.Controls.Add(attemptLabel);
//                attemptLabels.Add(attemptLabel);
//                startY += attemptLabel.Height + Margin;

//                // Attempt DataGridView
//                var attemptDataGridView = CreateAttemptDataGridView(yearSem);
//                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
//                this.Controls.Add(attemptDataGridView);
//                attemptDataGridViews.Add(attemptDataGridView);
//                startY += attemptDataGridView.Height + Margin;
//            }
//        }

//        private DataGridView CreateBacklogDataGridView(IGrouping<string, Backlog> group)
//        {
//            var dataGridView = new DataGridView
//            {
//                Name = $"dgv_{group.Key}",
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                ColumnCount = 7,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.None,
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
//                    BackColor = Color.White
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkViolet
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };

//            dataGridView.RowTemplate.Height = 50;
//            dataGridView.ColumnHeadersHeight = 40;

//            dataGridView.Columns[0].Name = "Subject Code";
//            dataGridView.Columns[1].Name = "Subject Name";
//            dataGridView.Columns[2].Name = "Internal";
//            dataGridView.Columns[3].Name = "External";
//            dataGridView.Columns[4].Name = "Total";
//            dataGridView.Columns[5].Name = "Grade";
//            dataGridView.Columns[6].Name = "Credits";

//            dataGridView.Columns[0].Width = 150;
//            dataGridView.Columns[1].Width = 500;
//            dataGridView.Columns[2].Width = 120;
//            dataGridView.Columns[3].Width = 120;
//            dataGridView.Columns[4].Width = 100;
//            dataGridView.Columns[5].Width = 120;
//            dataGridView.Columns[6].Width = 120;

//            foreach (var backlog in group)
//            {
//                dataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                dataGridView.Rows.Add("No backlogs", "", "", "", "", "", "");
//            }

//            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            return dataGridView;
//        }

//        private DataGridView CreateAttemptDataGridView(string yearSem)
//        {
//            var dataGridView = new DataGridView
//            {
//                Name = $"dgv_attempts_{yearSem}",
//                Width = DataGridViewWidth,
//                AutoGenerateColumns = false,
//                ColumnCount = 6,
//                RowHeadersVisible = false,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
//                AllowUserToAddRows = false,
//                ScrollBars = ScrollBars.None,
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
//                    BackColor = Color.White
//                },
//                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkViolet
//                },
//                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
//                GridColor = Color.White
//            };

//            dataGridView.RowTemplate.Height = 50;
//            dataGridView.ColumnHeadersHeight = 40;

//            dataGridView.Columns[0].Name = "Subject Code";
//            dataGridView.Columns[1].Name = "Subject Name";
//            dataGridView.Columns[2].Name = "Attempt Number";
//            dataGridView.Columns[3].Name = "Grade Points";
//            dataGridView.Columns[4].Name = "Credits";
//            dataGridView.Columns[5].Name = "Timestamp";

//            dataGridView.Columns[0].Width = 150;
//            dataGridView.Columns[1].Width = 300;
//            dataGridView.Columns[2].Width = 150;
//            dataGridView.Columns[3].Width = 150;
//            dataGridView.Columns[4].Width = 150;
//            dataGridView.Columns[5].Width = 450;

//            if (groupedAttempts.ContainsKey(yearSem))
//            {
//                foreach (var attempt in groupedAttempts[yearSem])
//                {
//                    dataGridView.Rows.Add(attempt.SubjectCode, attempt.SubjectName, attempt.AttemptNumber, attempt.GradePoints, attempt.Credits, attempt.Timestamp);
//                }
//            }

//            if (dataGridView.Rows.Count == 0)
//            {
//                dataGridView.Rows.Add("No attempts", "", "", "", "", "");
//            }

//            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
//            return dataGridView;
//        }

//        private int CalculateDataGridViewHeight(int rowCount)
//        {
//            int rowHeight = 50;
//            int headerHeight = 40;
//            int totalHeight = headerHeight + rowCount * rowHeight;
//            return totalHeight < 90 ? 90 : totalHeight;
//        }

//        private DataGridView CreateSmallDataGridView()
//        {
//            var dataGridView = new DataGridView
//            {
//                EnableHeadersVisualStyles = false,
//                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    BackColor = Color.DarkOrchid,
//                    ForeColor = Color.White,
//                    Font = new Font("Arial", 10, FontStyle.Bold),
//                    Alignment = DataGridViewContentAlignment.MiddleCenter
//                },
//                ColumnHeadersHeight = 40,
//                RowsDefaultCellStyle = new DataGridViewCellStyle
//                {
//                    Font = new Font("Arial", 10),
//                    BackColor = Color.White
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
//                AllowUserToResizeRows = false
//            };

//            dataGridView.RowTemplate.Height = 50;
//            dataGridView.ColumnCount = 3;
//            dataGridView.Columns[0].Name = "STUDENT NAME";
//            dataGridView.Columns[1].Name = "ROLL NO";
//            dataGridView.Columns[2].Name = "BRANCH";
//            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
//            return dataGridView;
//        }

//        private void LoadBacklogData()
//        {
//            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
//            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");
//            var studentAttempts = LoadJsonData<StudentAttempt>("studentAttempts.json");

//            // Filter backlogs
//            var backlogs = studentMarks
//                .Where(m => m.StudentId.ToUpper() == studentId && (m.Grade == "F" || m.Grade == "Ab"))
//                .Join(subjectCodes,
//                    mark => mark.SubjectCode,
//                    subject => subject.SubjectCode,
//                    (mark, subject) => new Backlog
//                    {
//                        SubjectCode = mark.SubjectCode,
//                        SubjectName = mark.SubjectName,
//                        Internal = mark.Internal,
//                        External = mark.External,
//                        Total = mark.Total,
//                        Grade = mark.Grade,
//                        Credits = mark.Credits,
//                        YearSem = $"{subject.Year}-{subject.Semester}"
//                    })
//                .ToList();

//            baklogstotal = backlogs.Count;
//            groupedBacklogs = backlogs
//                .GroupBy(b => b.YearSem)
//                .OrderBy(g => g.Key)
//                .ToList();

//            // Load all attempts from studentAttempts
//            var attempts = studentAttempts
//                .Where(a => a.StudentId.ToUpper() == studentId)
//                .SelectMany(a => a.Attempts.Select(attempt => new
//                {
//                    Attempt = new Attempt
//                    {
//                        SubjectCode = a.SubjectCode,
//                        SubjectName = studentMarks.FirstOrDefault(m => m.SubjectCode == a.SubjectCode && m.StudentId.ToUpper() == studentId)?.SubjectName ?? "Unknown",
//                        AttemptNumber = attempt.AttemptNumber,
//                        GradePoints = attempt.GradePoints,
//                        Credits = attempt.Credits,
//                        Timestamp = attempt.Timestamp
//                    },
//                    SubjectCode = a.SubjectCode
//                }))
//                .Join(subjectCodes,
//                    a => a.SubjectCode,
//                    subject => subject.SubjectCode,
//                    (a, subject) => new
//                    {
//                        a.Attempt,
//                        YearSem = $"{subject.Year}-{subject.Semester}"
//                    })
//                .ToList();

//            groupedAttempts = attempts
//                .GroupBy(a => a.YearSem)
//                .ToDictionary(g => g.Key, g => g.Select(a => a.Attempt).OrderBy(a => a.SubjectCode).ThenBy(a => a.AttemptNumber).ToList());

//            // Log for debugging
//            Console.WriteLine($"Backlogs: {backlogs.Count}, Attempts: {attempts.Count}");
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

//        private void GenerateExcel()
//        {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

//                // Student Info
//                var student = GetStudentById(studentId);
//                worksheet.Cells[1, 1].Value = "Student Name";
//                worksheet.Cells[1, 2].Value = student.Name;
//                worksheet.Cells[2, 1].Value = "Roll No";
//                worksheet.Cells[2, 2].Value = student.StudentId;
//                worksheet.Cells[3, 1].Value = "Branch";
//                worksheet.Cells[3, 2].Value = GetBranchFromStudentId(studentId);
//                worksheet.Cells[4, 1].Value = "Total Backlogs";
//                worksheet.Cells[4, 2].Value = baklogstotal;

//                // Backlog Headers
//                int startRow = 6;
//                worksheet.Cells[startRow, 1].Value = "Subject Code";
//                worksheet.Cells[startRow, 2].Value = "Subject Name";
//                worksheet.Cells[startRow, 3].Value = "Internal";
//                worksheet.Cells[startRow, 4].Value = "External";
//                worksheet.Cells[startRow, 5].Value = "Total";
//                worksheet.Cells[startRow, 6].Value = "Grade";
//                worksheet.Cells[startRow, 7].Value = "Credits";
//                worksheet.Cells[startRow, 8].Value = "Year-Sem";

//                using (var headerRange = worksheet.Cells[startRow, 1, startRow, 8])
//                {
//                    headerRange.Style.Font.Bold = true;
//                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//                    headerRange.Style.Font.Color.SetColor(Color.White);
//                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }

//                // Backlog Data
//                int row = startRow + 1;
//                foreach (var group in groupedBacklogs)
//                {
//                    foreach (var backlog in group)
//                    {
//                        worksheet.Cells[row, 1].Value = backlog.SubjectCode;
//                        worksheet.Cells[row, 2].Value = backlog.SubjectName;
//                        worksheet.Cells[row, 3].Value = backlog.Internal;
//                        worksheet.Cells[row, 4].Value = backlog.External;
//                        worksheet.Cells[row, 5].Value = backlog.Total;
//                        worksheet.Cells[row, 6].Value = backlog.Grade;
//                        worksheet.Cells[row, 7].Value = backlog.Credits;
//                        worksheet.Cells[row, 8].Value = backlog.YearSem;
//                        row++;
//                    }
//                }

//                // Attempt Headers
//                row++;
//                worksheet.Cells[row, 1].Value = "Attempt History";
//                worksheet.Cells[row, 1, row, 6].Merge = true;
//                worksheet.Cells[row, 1].Style.Font.Bold = true;
//                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                row++;

//                worksheet.Cells[row, 1].Value = "Subject Code";
//                worksheet.Cells[row, 2].Value = "Subject Name";
//                worksheet.Cells[row, 3].Value = "Attempt Number";
//                worksheet.Cells[row, 4].Value = "Grade Points";
//                worksheet.Cells[row, 5].Value = "Credits";
//                worksheet.Cells[row, 6].Value = "Timestamp";

//                using (var headerRange = worksheet.Cells[row, 1, row, 6])
//                {
//                    headerRange.Style.Font.Bold = true;
//                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
//                    headerRange.Style.Font.Color.SetColor(Color.White);
//                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }

//                // Attempt Data
//                row++;
//                foreach (var yearSem in groupedAttempts.Keys.OrderBy(ys => ys))
//                {
//                    worksheet.Cells[row, 1].Value = $"{yearSem} Attempts";
//                    worksheet.Cells[row, 1, row, 6].Merge = true;
//                    worksheet.Cells[row, 1].Style.Font.Bold = true;
//                    row++;
//                    foreach (var attempt in groupedAttempts[yearSem])
//                    {
//                        worksheet.Cells[row, 1].Value = attempt.SubjectCode;
//                        worksheet.Cells[row, 2].Value = attempt.SubjectName;
//                        worksheet.Cells[row, 3].Value = attempt.AttemptNumber;
//                        worksheet.Cells[row, 4].Value = attempt.GradePoints;
//                        worksheet.Cells[row, 5].Value = attempt.Credits;
//                        worksheet.Cells[row, 6].Value = attempt.Timestamp;
//                        row++;
//                    }
//                }

//                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
//                var saveFileDialog = new SaveFileDialog
//                {
//                    Filter = "Excel files (*.xlsx)|*.xlsx",
//                    Title = "Save Backlog Report"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    var file = new FileInfo(saveFileDialog.FileName);
//                    package.SaveAs(file);
//                    MessageBox.Show("Excel report saved successfully!");
//                }
//            }
//        }

//        private void btnDownloadPdf_Click(object sender, EventArgs e)
//        {
//            GenerateExcel();
//        }

//        private void btnBackToHome_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            admindashboard adb = new admindashboard();
//            adb.Show();

//            try
//            {
//                FormBacklockCount child = new FormBacklockCount();
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

//    public class Backlog
//    {
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string YearSem { get; set; }
//        public string Credits { get; set; }
//    }

//    public class StudentMarks
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public string Internal { get; set; }
//        public string External { get; set; }
//        public string Total { get; set; }
//        public string Grade { get; set; }
//        public string Credits { get; set; }
//    }

//    public class Subject
//    {
//        public string SubjectCode { get; set; }
//        public string Year { get; set; }
//        public string Semester { get; set; }
//    }

//    public class Student
//    {
//        public string StudentId { get; set; }
//        public string Name { get; set; }
//    }

//    public class StudentAttempt
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public List<Attempt> Attempts { get; set; }
//    }

//    public class Attempt
//    {
//        public string SubjectCode { get; set; }
//        public string SubjectName { get; set; }
//        public int AttemptNumber { get; set; }
//        public string GradePoints { get; set; }
//        public string Credits { get; set; }
//        public string Timestamp { get; set; }
//    }
//}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using project_RYS.Forms;

namespace project_RYS
{
    public partial class BacklogResultsForm : Form
    {
        private List<DataGridView> dataGridViews;
        private List<Label> labels;
        private List<DataGridView> attemptDataGridViews;
        private List<Label> attemptLabels;
        private Button backButton;
        private Button downloadButton;
        private Label lblTotalBacklogs;
        private Label headingLabel;
        private const int DataGridViewWidth = 1250;
        private const int SmallDataGridViewHeight = 80;
        private const int Margin = 20;
        private int baklogstotal = 0;
        private readonly string studentId;
        private List<IGrouping<string, Backlog>> groupedBacklogs;
        private Dictionary<string, List<Attempt>> groupedAttempts;

        // List of colors for SubjectCode highlighting
        private readonly Color[] subjectColors = new Color[]
        {
            Color.FromArgb(173, 216, 230), // LightBlue
            Color.FromArgb(144, 238, 144), // LightGreen
            Color.FromArgb(255, 182, 193), // LightPink
            Color.FromArgb(240, 230, 140), // Khaki
            Color.FromArgb(216, 191, 216), // Thistle
            Color.FromArgb(255, 228, 196), // Bisque
            Color.FromArgb(175, 238, 238), // PaleTurquoise
            Color.FromArgb(245, 245, 220)  // Beige
        };

        public BacklogResultsForm(string studentId)
        {
            InitializeComponent();
            this.studentId = studentId.ToUpper();
            dataGridViews = new List<DataGridView>();
            labels = new List<Label>();
            attemptDataGridViews = new List<DataGridView>();
            attemptLabels = new List<Label>();
            SetupForm();
            LoadBacklogData();
            ArrangeControls();
        }

        private void SetupForm()
        {
            this.Size = new Size(1300, 800);
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Heading Label
            headingLabel = new Label
            {
                Text = "Backlog Report",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid
            };
            this.Controls.Add(headingLabel);

            // Back Button
            backButton = new Button
            {
                Text = "Back To Home",
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkViolet,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            backButton.FlatAppearance.BorderColor = Color.DarkOrchid;
            backButton.MouseEnter += (s, e) => backButton.BackColor = Color.FromArgb(153, 50, 204);
            backButton.MouseLeave += (s, e) => backButton.BackColor = Color.DarkViolet;
            backButton.Click += new EventHandler(btnBackToHome_Click);
            this.Controls.Add(backButton);

            // Download Button
            downloadButton = new Button
            {
                Text = "Download",
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrchid,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            downloadButton.FlatAppearance.BorderColor = Color.DarkViolet;
            downloadButton.MouseEnter += (s, e) => downloadButton.BackColor = Color.FromArgb(186, 85, 211);
            downloadButton.MouseLeave += (s, e) => downloadButton.BackColor = Color.DarkOrchid;
            downloadButton.Click += new EventHandler(btnDownloadPdf_Click);
            this.Controls.Add(downloadButton);

            // Total Backlogs Label
            lblTotalBacklogs = new Label
            {
                Text = $"Total Backlogs: {baklogstotal}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.DarkOrchid
            };
            this.Controls.Add(lblTotalBacklogs);
        }

        private Dictionary<string, string> LoadStudentNames(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Student names file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<string, string>();
            }

            try
            {
                var jsonData = File.ReadAllText(filePath);
                var studentsList = JsonConvert.DeserializeObject<List<stnames>>(jsonData);
                return studentsList
                    .Where(student => !string.IsNullOrEmpty(student.studentid))
                    .ToDictionary(student => student.studentid.ToUpper(), student => student.name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load student names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Dictionary<string, string>();
            }
        }

        private string GetStudentName(string studentId)
        {
            var studentNames = LoadStudentNames("studentnames.json");
            return studentNames.ContainsKey(studentId.ToUpper()) ? studentNames[studentId.ToUpper()] : "Unknown";
        }

        private class stnames
        {
            public string studentid { get; set; }
            public string name { get; set; }
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

        private Student GetStudentById(string studentId)
        {
            return new Student
            {
                StudentId = studentId.ToUpper(),
                Name = GetStudentName(studentId)
            };
        }

        private void ArrangeControls()
        {
            int startY = 20;

            // Center heading
            headingLabel.Location = new Point((this.ClientSize.Width - headingLabel.Width) / 2, startY);
            startY = headingLabel.Bottom + Margin;

            // Back button
            backButton.Location = new Point(20, startY);
            startY = backButton.Bottom + Margin;

            // Small DataGridView
            var smallDataGridView = CreateSmallDataGridView();
            var student = GetStudentById(studentId);
            if (student != null)
            {
                smallDataGridView.Rows.Add(student.Name, student.StudentId, GetBranchFromStudentId(studentId));
                smallDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
                dataGridViews.Add(smallDataGridView);
                this.Controls.Add(smallDataGridView);
                startY = smallDataGridView.Bottom + Margin;
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Backlog and attempt DataGridViews
            DisplayYearSemDataGrids(ref startY);

            // Total backlogs and download button
            lblTotalBacklogs.Text = $"Total Backlogs: {baklogstotal}";
            lblTotalBacklogs.Location = new Point((this.ClientSize.Width - lblTotalBacklogs.Width) / 2, startY);
            downloadButton.Location = new Point(lblTotalBacklogs.Right + Margin, startY);
        }

        private void DisplayYearSemDataGrids(ref int startY)
        {
            foreach (var group in groupedBacklogs)
            {
                // Backlog Label
                var backlogLabel = new Label
                {
                    Text = $"{group.Key} Backlogs",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.DarkOrchid
                };
                backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
                this.Controls.Add(backlogLabel);
                labels.Add(backlogLabel);
                startY += backlogLabel.Height + Margin;

                // Backlog DataGridView
                var backlogDataGridView = CreateBacklogDataGridView(group);
                backlogDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
                this.Controls.Add(backlogDataGridView);
                dataGridViews.Add(backlogDataGridView);
                startY += backlogDataGridView.Height + Margin;

                // Attempt Label
                var attemptLabel = new Label
                {
                    Text = $"{group.Key} Attempt History",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.DarkOrchid
                };
                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
                this.Controls.Add(attemptLabel);
                attemptLabels.Add(attemptLabel);
                startY += attemptLabel.Height + Margin;

                // Attempt DataGridView
                var attemptDataGridView = CreateAttemptDataGridView(group.Key);
                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
                this.Controls.Add(attemptDataGridView);
                attemptDataGridViews.Add(attemptDataGridView);
                startY += attemptDataGridView.Height + Margin;
            }

            // Display attempt tables for Year-Sem not in backlogs
            var backlogYearSems = groupedBacklogs.Select(g => g.Key).ToList();
            var attemptYearSems = groupedAttempts.Keys.Where(ys => !backlogYearSems.Contains(ys)).OrderBy(ys => ys).ToList();
            foreach (var yearSem in attemptYearSems)
            {
                // Attempt Label
                var attemptLabel = new Label
                {
                    Text = $"{yearSem} Attempt History",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.DarkOrchid
                };
                attemptLabel.Location = new Point((this.ClientSize.Width - attemptLabel.Width) / 2, startY);
                this.Controls.Add(attemptLabel);
                attemptLabels.Add(attemptLabel);
                startY += attemptLabel.Height + Margin;

                // Attempt DataGridView
                var attemptDataGridView = CreateAttemptDataGridView(yearSem);
                attemptDataGridView.Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY);
                this.Controls.Add(attemptDataGridView);
                attemptDataGridViews.Add(attemptDataGridView);
                startY += attemptDataGridView.Height + Margin;
            }
        }

        private DataGridView CreateBacklogDataGridView(IGrouping<string, Backlog> group)
        {
            var dataGridView = new DataGridView
            {
                Name = $"dgv_{group.Key}",
                Width = DataGridViewWidth,
                AutoGenerateColumns = false,
                ColumnCount = 7,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.None,
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
                    BackColor = Color.White
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkViolet
                },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White
            };

            dataGridView.RowTemplate.Height = 50;
            dataGridView.ColumnHeadersHeight = 40;

            dataGridView.Columns[0].Name = "Subject Code";
            dataGridView.Columns[1].Name = "Subject Name";
            dataGridView.Columns[2].Name = "Internal";
            dataGridView.Columns[3].Name = "External";
            dataGridView.Columns[4].Name = "Total";
            dataGridView.Columns[5].Name = "Grade";
            dataGridView.Columns[6].Name = "Credits";

            dataGridView.Columns[0].Width = 150;
            dataGridView.Columns[1].Width = 500;
            dataGridView.Columns[2].Width = 120;
            dataGridView.Columns[3].Width = 120;
            dataGridView.Columns[4].Width = 100;
            dataGridView.Columns[5].Width = 120;
            dataGridView.Columns[6].Width = 120;

            foreach (var backlog in group)
            {
                dataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
            }

            if (dataGridView.Rows.Count == 0)
            {
                dataGridView.Rows.Add("No backlogs", "", "", "", "", "", "");
            }

            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
            return dataGridView;
        }

        private DataGridView CreateAttemptDataGridView(string yearSem)
        {
            var dataGridView = new DataGridView
            {
                Name = $"dgv_attempts_{yearSem}",
                Width = DataGridViewWidth,
                AutoGenerateColumns = false,
                ColumnCount = 6,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.None,
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
                    BackColor = Color.White
                },
                // Remove AlternatingRowsDefaultCellStyle to avoid conflicts with subject colors
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White
            };

            dataGridView.RowTemplate.Height = 50;
            dataGridView.ColumnHeadersHeight = 40;

            dataGridView.Columns[0].Name = "Subject Code";
            dataGridView.Columns[1].Name = "Subject Name";
            dataGridView.Columns[2].Name = "Attempt Number";
            dataGridView.Columns[3].Name = "Grade Points";
            dataGridView.Columns[4].Name = "Credits";
            dataGridView.Columns[5].Name = "Timestamp";

            dataGridView.Columns[0].Width = 150;
            dataGridView.Columns[1].Width = 300;
            dataGridView.Columns[2].Width = 150;
            dataGridView.Columns[3].Width = 150;
            dataGridView.Columns[4].Width = 150;
            dataGridView.Columns[5].Width = 450;

            // Assign colors to SubjectCodes
            Dictionary<string, Color> subjectColorMap = new Dictionary<string, Color>();
            if (groupedAttempts.ContainsKey(yearSem))
            {
                var uniqueSubjectCodes = groupedAttempts[yearSem]
                    .Select(a => a.SubjectCode)
                    .Distinct()
                    .OrderBy(sc => sc)
                    .ToList();
                for (int i = 0; i < uniqueSubjectCodes.Count; i++)
                {
                    subjectColorMap[uniqueSubjectCodes[i]] = subjectColors[i % subjectColors.Length];
                }

                foreach (var attempt in groupedAttempts[yearSem])
                {
                    int rowIndex = dataGridView.Rows.Add(attempt.SubjectCode, attempt.SubjectName, attempt.AttemptNumber, attempt.GradePoints, attempt.Credits, attempt.Timestamp);
                    // Apply color to the entire row
                    dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = subjectColorMap[attempt.SubjectCode];
                }
            }

            if (dataGridView.Rows.Count == 0)
            {
                dataGridView.Rows.Add("No attempts", "", "", "", "", "");
            }

            dataGridView.Height = CalculateDataGridViewHeight(dataGridView.Rows.Count);
            return dataGridView;
        }

        private int CalculateDataGridViewHeight(int rowCount)
        {
            int rowHeight = 50;
            int headerHeight = 40;
            int totalHeight = headerHeight + rowCount * rowHeight;
            return totalHeight < 90 ? 90 : totalHeight;
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
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                ColumnHeadersHeight = 40,
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Arial", 10),
                    BackColor = Color.White
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
                AllowUserToResizeRows = false
            };

            dataGridView.RowTemplate.Height = 50;
            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "STUDENT NAME";
            dataGridView.Columns[1].Name = "ROLL NO";
            dataGridView.Columns[2].Name = "BRANCH";
            dataGridView.Size = new Size(DataGridViewWidth, SmallDataGridViewHeight);
            return dataGridView;
        }

        private void LoadBacklogData()
        {
            var studentMarks = LoadJsonData<StudentMarks>("studentMarks.json");
            var subjectCodes = LoadJsonData<Subject>("subjectCodes.json");
            var studentAttempts = LoadJsonData<StudentAttempt>("studentAttempts.json");

            // Filter backlogs
            var backlogs = studentMarks
                .Where(m => m.StudentId.ToUpper() == studentId && (m.Grade == "F" || m.Grade == "Ab"))
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
                        YearSem = $"{subject.Year}-{subject.Semester}"
                    })
                .ToList();

            baklogstotal = backlogs.Count;
            groupedBacklogs = backlogs
                .GroupBy(b => b.YearSem)
                .OrderBy(g => g.Key)
                .ToList();

            // Load all attempts from studentAttempts
            var attempts = studentAttempts
                .Where(a => a.StudentId.ToUpper() == studentId)
                .SelectMany(a => a.Attempts.Select(attempt => new
                {
                    Attempt = new Attempt
                    {
                        SubjectCode = a.SubjectCode,
                        SubjectName = studentMarks.FirstOrDefault(m => m.SubjectCode == a.SubjectCode && m.StudentId.ToUpper() == studentId)?.SubjectName ?? "Unknown",
                        AttemptNumber = attempt.AttemptNumber,
                        GradePoints = attempt.GradePoints,
                        Credits = attempt.Credits,
                        Timestamp = attempt.Timestamp
                    },
                    SubjectCode = a.SubjectCode
                }))
                .Join(subjectCodes,
                    a => a.SubjectCode,
                    subject => subject.SubjectCode,
                    (a, subject) => new
                    {
                        a.Attempt,
                        YearSem = $"{subject.Year}-{subject.Semester}"
                    })
                .ToList();

            groupedAttempts = attempts
                .GroupBy(a => a.YearSem)
                .ToDictionary(g => g.Key, g => g.Select(a => a.Attempt).OrderBy(a => a.SubjectCode).ThenBy(a => a.AttemptNumber).ToList());
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

        private void GenerateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Backlog Report");

                // Student Info
                var student = GetStudentById(studentId);
                worksheet.Cells[1, 1].Value = "Student Name";
                worksheet.Cells[1, 2].Value = student.Name;
                worksheet.Cells[2, 1].Value = "Roll No";
                worksheet.Cells[2, 2].Value = student.StudentId;
                worksheet.Cells[3, 1].Value = "Branch";
                worksheet.Cells[3, 2].Value = GetBranchFromStudentId(studentId);
                worksheet.Cells[4, 1].Value = "Total Backlogs";
                worksheet.Cells[4, 2].Value = baklogstotal;

                using (var infoRange = worksheet.Cells[1, 1, 4, 2])
                {
                    infoRange.Style.Font.Bold = true;
                    infoRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                }

                // Backlog Headers
                int startRow = 6;
                worksheet.Cells[startRow, 1].Value = "Subject Code";
                worksheet.Cells[startRow, 2].Value = "Subject Name";
                worksheet.Cells[startRow, 3].Value = "Internal";
                worksheet.Cells[startRow, 4].Value = "External";
                worksheet.Cells[startRow, 5].Value = "Total";
                worksheet.Cells[startRow, 6].Value = "Grade";
                worksheet.Cells[startRow, 7].Value = "Credits";
                worksheet.Cells[startRow, 8].Value = "Year-Sem";

                using (var headerRange = worksheet.Cells[startRow, 1, startRow, 8])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
                    headerRange.Style.Font.Color.SetColor(Color.White);
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                // Backlog Data
                int row = startRow + 1;
                foreach (var group in groupedBacklogs)
                {
                    foreach (var backlog in group)
                    {
                        worksheet.Cells[row, 1].Value = backlog.SubjectCode;
                        worksheet.Cells[row, 2].Value = backlog.SubjectName;
                        worksheet.Cells[row, 3].Value = backlog.Internal;
                        worksheet.Cells[row, 4].Value = backlog.External;
                        worksheet.Cells[row, 5].Value = backlog.Total;
                        worksheet.Cells[row, 6].Value = backlog.Grade;
                        worksheet.Cells[row, 7].Value = backlog.Credits;
                        worksheet.Cells[row, 8].Value = backlog.YearSem;
                        worksheet.Cells[row, 1, row, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        row++;
                    }
                }

                // Attempt Headers
                row++;
                worksheet.Cells[row, 1].Value = "Attempt History";
                worksheet.Cells[row, 1, row, 6].Merge = true;
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;

                worksheet.Cells[row, 1].Value = "Subject Code";
                worksheet.Cells[row, 2].Value = "Subject Name";
                worksheet.Cells[row, 3].Value = "Attempt Number";
                worksheet.Cells[row, 4].Value = "Grade Points";
                worksheet.Cells[row, 5].Value = "Credits";
                worksheet.Cells[row, 6].Value = "Timestamp";

                using (var headerRange = worksheet.Cells[row, 1, row, 6])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
                    headerRange.Style.Font.Color.SetColor(Color.White);
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                // Attempt Data with Colors
                row++;
                foreach (var yearSem in groupedAttempts.Keys.OrderBy(ys => ys))
                {
                    worksheet.Cells[row, 1].Value = $"{yearSem} Attempts";
                    worksheet.Cells[row, 1, row, 6].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row++;

                    // Assign colors to SubjectCodes for this Year-Sem
                    Dictionary<string, Color> subjectColorMap = new Dictionary<string, Color>();
                    var uniqueSubjectCodes = groupedAttempts[yearSem]
                        .Select(a => a.SubjectCode)
                        .Distinct()
                        .OrderBy(sc => sc)
                        .ToList();
                    for (int i = 0; i < uniqueSubjectCodes.Count; i++)
                    {
                        subjectColorMap[uniqueSubjectCodes[i]] = subjectColors[i % subjectColors.Length];
                    }

                    foreach (var attempt in groupedAttempts[yearSem])
                    {
                        worksheet.Cells[row, 1].Value = attempt.SubjectCode;
                        worksheet.Cells[row, 2].Value = attempt.SubjectName;
                        worksheet.Cells[row, 3].Value = attempt.AttemptNumber;
                        worksheet.Cells[row, 4].Value = attempt.GradePoints;
                        worksheet.Cells[row, 5].Value = attempt.Credits;
                        worksheet.Cells[row, 6].Value = attempt.Timestamp;
                        // Apply color to the row
                        worksheet.Cells[row, 1, row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1, row, 6].Style.Fill.BackgroundColor.SetColor(subjectColorMap[attempt.SubjectCode]);
                        worksheet.Cells[row, 1, row, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        row++;
                    }
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // Set minimum column width for readability
                for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                {
                    if (worksheet.Column(i).Width < 10)
                        worksheet.Column(i).Width = 10;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "Save Backlog Report",
                    FileName = $"BacklogReport_{studentId}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);
                    MessageBox.Show("Excel report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateExcel();
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

        private void btnBackToHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adb = new admindashboard();
            adb.Show();

            try
            {
                FormBacklockCount child = new FormBacklockCount();
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
    }

    public class Backlog
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Internal { get; set; }
        public string External { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string YearSem { get; set; }
        public string Credits { get; set; }
    }

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

    public class Subject
    {
        public string SubjectCode { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
    }

    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
    }

    public class StudentAttempt
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public List<Attempt> Attempts { get; set; }
    }

    public class Attempt
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int AttemptNumber { get; set; }
        public string GradePoints { get; set; }
        public string Credits { get; set; }
        public string Timestamp { get; set; }
    }
}