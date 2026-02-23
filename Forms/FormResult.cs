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
    public partial class FormResult : Form
    {
        string msg1;
        private List<StudentInfo> students;
        public FormResult()
        {
            InitializeComponent();
            // Call this method to initialize placeholder for the TextBox
            SetPlaceholder();
            LoadStudentData();
        }


        private void LoadStudentData()
        {
            // Load student data from JSON
            students = LoadJsonData<StudentInfo>("studentMarks.json");
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                //MessageBox.Show($" {filePath}");
                msg1 = "File not found:!";
                msgbox mb = new msgbox(msg1);
                mb.ShowDialog();
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        // Set placeholder text for the TextBox
        private void SetPlaceholder()
        {
            txtStudentId.Text = "Enter Rollno here"; // Placeholder text
            txtStudentId.ForeColor = Color.Gray; // Set placeholder text color
            txtStudentId.GotFocus += RemovePlaceholder;
            txtStudentId.LostFocus += AddPlaceholder;
        }

        // Remove placeholder when the TextBox gets focus
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtStudentId.Text == "Enter Rollno here")
            {
                txtStudentId.Text = ""; // Clear the placeholder text
                txtStudentId.ForeColor = Color.Black; // Set the text color back to black
            }
        }

        // Add placeholder if the TextBox loses focus and is empty
        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                txtStudentId.Text = "Enter your text here"; // Add the placeholder text
                txtStudentId.ForeColor = Color.Gray; // Set placeholder text color
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add any logic here if needed when the text changes
        }

        private void resultbtn_Click(object sender, EventArgs e)
        {
            string enteredId = txtStudentId.Text.ToUpper().Trim();

            if (string.IsNullOrEmpty(enteredId))
            {
                //MessageBox.Show(");
                msg1 = "PPlease enter a valid Student ID.\"";
                msgbox mb = new msgbox(msg1);
                mb.ShowDialog();
                return;
            }

            // Check if student ID exists
            var student = students?.FirstOrDefault(s => s.StudentId == enteredId); // Use null-conditional operator to safely access 'students'

            if (student != null)
            {
                // Open the result form with the found student ID
                string id = student.StudentId;
                mainresult mr = new mainresult(id);
                Application.OpenForms["admindashboard"]?.Close();
                mr.Show();
                //resultsForm.Show();
                // Close MainForm
                

                 //Close ChildForm
                this.Close();

            }
            else
            {
                //MessageBox.Show("");
                msg1 = "No student found with the entered ID.";
                msgbox mb = new msgbox(msg1);
                mb.ShowDialog();
            }
        }


        private void FormResult_Load(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
       
    }
    public class StudentInfo
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
    }
}








// Clear any existing controls before adding new ones
//foreach (var control in this.Controls)
//{
//    if (control is DataGridView || control is Label)
//    {
//        this.Controls.Remove(control as Control);
//    }
//}

// Display each Year-Sem backlogs in a separate DataGridView










//private void DisplayYearSemDataGrids(ref int startY)
//{
//    foreach (var group in groupedBacklogs)
//    {
//        var backlogLabel = new Label
//        {
//            Text = $"{group.Key} Backlogs", // Year-Sem label
//            Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
//            AutoSize = true
//        };
//        backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
//        this.Controls.Add(backlogLabel);
//        startY += backlogLabel.Height + Margin;

//        // Create a DataGridView for this Year-Sem group
//        var backlogDataGridView = new DataGridView
//        {
//            Name = $"dgv_{group.Key}",
//            Width = DataGridViewWidth,
//            Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
//            AutoGenerateColumns = false,
//            ColumnCount = 7,
//            RowHeadersVisible = false,
//            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // Fill the width with columns
//            AllowUserToAddRows = false,
//            ScrollBars = ScrollBars.None, // Remove scrollbars
//            ReadOnly = true,
//            AllowUserToResizeColumns = false, // Prevents resizing columns
//            AllowUserToResizeRows = false, // Prevents resizing rows
//        };

//        backlogDataGridView.RowTemplate.Height = 50;

//        // Set column headers
//        backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkOrchid,
//            ForeColor = Color.White,
//            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//            Alignment = DataGridViewContentAlignment.MiddleCenter
//        };
//        backlogDataGridView.ColumnHeadersHeight = 40;

//        // Set rows styling
//        backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkOrchid,
//            ForeColor = Color.White,
//            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//        };
//        backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkViolet
//        };

//        // Set borders and grid color
//        backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
//        backlogDataGridView.GridColor = Color.White;

//        // Define columns
//        backlogDataGridView.Columns[0].Name = "Subject Code";
//        backlogDataGridView.Columns[1].Name = "Subject Name";
//        backlogDataGridView.Columns[2].Name = "Internal";
//        backlogDataGridView.Columns[3].Name = "External";
//        backlogDataGridView.Columns[4].Name = "Total";
//        backlogDataGridView.Columns[5].Name = "Grade";
//        backlogDataGridView.Columns[6].Name = "Credits";

//        // Add rows for each backlog
//        foreach (var backlog in group)
//        {
//            backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
//        }

//        // Adjust height dynamically based on row count
//        backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());

//        // Add the DataGridView to the form only once (check if not already added)
//        if (!this.Controls.Contains(backlogDataGridView))
//        {
//            this.Controls.Add(backlogDataGridView);
//        }

//        startY += backlogDataGridView.Height + Margin; // Update startY for the next control
//                                                       // Create the label first
//                                                       // Initialize sgpalabel only once
//        if (sgpalabel == null)
//        {
//            sgpalabel = new Label();
//            sgpalabel.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
//            sgpalabel.AutoSize = true;
//            this.Controls.Add(sgpalabel); // Add it to the form here
//        }

//        // Set the location after the label is added (Width and Height are now calculated)
//        sgpalabel.Location = new Point((this.ClientSize.Width - sgpalabel.Width) / 2, startY);

//        // Update the Y position for the next control
//        startY += sgpalabel.Height + Margin;

//    }
//}

//private void DisplayYearSemDataGrids(ref int startY)
//{
//    foreach (var group in groupedBacklogs)
//    {
//        var backlogLabel = new Label
//        {
//            Text = $"{group.Key} Backlogs", // Year-Sem label
//            Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
//            AutoSize = true
//        };
//        backlogLabel.Location = new Point((this.ClientSize.Width - backlogLabel.Width) / 2, startY);
//        this.Controls.Add(backlogLabel);
//        startY += backlogLabel.Height + Margin;

//        // Create a DataGridView for this Year-Sem group
//        var backlogDataGridView = new DataGridView
//        {
//            Name = $"dgv_{group.Key}",
//            Width = DataGridViewWidth,
//            Location = new Point((this.ClientSize.Width - DataGridViewWidth) / 2, startY),
//            AutoGenerateColumns = false,
//            ColumnCount = 7,
//            RowHeadersVisible = false,
//            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//            AllowUserToAddRows = false,
//            ScrollBars = ScrollBars.None,
//            ReadOnly = true,
//            AllowUserToResizeColumns = false,
//            AllowUserToResizeRows = false,
//        };

//        backlogDataGridView.RowTemplate.Height = 50;
//        backlogDataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkOrchid,
//            ForeColor = Color.White,
//            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//            Alignment = DataGridViewContentAlignment.MiddleCenter
//        };
//        backlogDataGridView.ColumnHeadersHeight = 40;

//        backlogDataGridView.RowsDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkOrchid,
//            ForeColor = Color.White,
//            Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
//        };
//        backlogDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
//        {
//            BackColor = Color.DarkViolet
//        };

//        backlogDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
//        backlogDataGridView.GridColor = Color.White;

//        backlogDataGridView.Columns[0].Name = "Subject Code";
//        backlogDataGridView.Columns[1].Name = "Subject Name";
//        backlogDataGridView.Columns[2].Name = "Internal";
//        backlogDataGridView.Columns[3].Name = "External";
//        backlogDataGridView.Columns[4].Name = "Total";
//        backlogDataGridView.Columns[5].Name = "Grade";
//        backlogDataGridView.Columns[6].Name = "Credits";

//        foreach (var backlog in group)
//        {
//            backlogDataGridView.Rows.Add(backlog.SubjectCode, backlog.SubjectName, backlog.Internal, backlog.External, backlog.Total, backlog.Grade, backlog.Credits);
//        }

//        backlogDataGridView.Height = CalculateDataGridViewHeight(group.Count());

//        if (!this.Controls.Contains(backlogDataGridView))
//        {
//            this.Controls.Add(backlogDataGridView);
//        }

//        startY += backlogDataGridView.Height + Margin;

//        // Calculate SGPA for this Year-Sem group
//        double totalCredits = 0;
//        double totalGradePoints = 0;

//        foreach (var result in group)
//        {
//            double gradePoint = GetGradePoint(result.Grade);
//            int credits = int.TryParse(result.Credits, out int parsedCredits) ? parsedCredits : 0;

//            totalGradePoints += gradePoint * credits;
//            totalCredits += credits;
//        }

//        double sgpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;

//        // Create SGPA label
//        var sgpaLabel = new Label
//        {
//            Text = $"SGPA for {group.Key}: {sgpa.ToString("F2")}",
//            Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
//            AutoSize = true
//        };
//        sgpaLabel.Location = new Point((this.ClientSize.Width - sgpaLabel.Width) / 2, startY);
//        this.Controls.Add(sgpaLabel);

//        startY += sgpaLabel.Height + Margin;
//    }
//}
