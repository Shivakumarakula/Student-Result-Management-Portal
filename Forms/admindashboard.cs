using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_RYS
{
    public partial class admindashboard : Form
    {
        //Fields
        private Button currentButton;
        private readonly Random random;
        private int tempIndex;
        private Form activeForm;
        //Constructor
        public admindashboard()
        {
            InitializeComponent();
            random = new Random();
            this.AutoScroll = true;
        }


        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }


        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                }
            }
        }




        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }



        public void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void admindashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonuploadsubcodes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormUploadSubjectCodes(), sender);

        }
        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.BackupMarks(), sender);
        }

        private void uploadnamesbtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.uploadnamesForm(), sender);
        }

        private void buttonbacklockcount_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormBacklockCount(), sender);
        }

        private void buttonuploadmarks_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormUploadMarks(), sender);
        }

        private void buttonresult_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormResult(), sender);
        }

        private void jsonupload_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.BackupForm(), sender);
        
    }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.BackupMarks(), sender);
        }

        private void buttonLogout_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender);
            homepage hp = new homepage();
            hp.Show();
            this.Hide();
        }

        private void panelDesktopPane_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenChildForm(new Forms.BackupNames(), sender);
        }

        private void brbtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.batch_result(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.BatchResult(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.BatchResultCgpa(), sender);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            adminlogin al = new adminlogin();
            
            al.Show();
            this.Hide();
        }

        private void acyrbtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.acyrjsonform(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        { 
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = "Save Sample Excel File",
                    FileName = "SampleDataStructure.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        // Worksheet 1: studentnames.json
                        var wsStudentNames = workbook.Worksheets.Add("studentnames");
                        wsStudentNames.Cell(1, 1).Value = "studentid";
                        wsStudentNames.Cell(1, 2).Value = "name";
                        wsStudentNames.Row(1).Style.Font.SetBold();
                        // Sample data
                        wsStudentNames.Cell(2, 1).Value = "23VD5A6612";
                        wsStudentNames.Cell(2, 2).Value = "John Doe";
                        wsStudentNames.Cell(3, 1).Value = "23VD1A0510";
                        wsStudentNames.Cell(3, 2).Value = "Jane Smith";
                        wsStudentNames.Columns().AdjustToContents();

                        // Worksheet 2: student_academicyear.json
                        var wsAcademicYear = workbook.Worksheets.Add("student_academicyear");
                        wsAcademicYear.Cell(1, 1).Value = "studentid";
                        wsAcademicYear.Cell(1, 2).Value = "academic_year";
                        wsAcademicYear.Row(1).Style.Font.SetBold();
                        // Sample data
                        wsAcademicYear.Cell(2, 1).Value = "21VD1A6612";
                        wsAcademicYear.Cell(2, 2).Value = "2021-2025";
                        wsAcademicYear.Cell(3, 1).Value = "23VD1A0510";
                        wsAcademicYear.Cell(3, 2).Value = "2023-2026";
                        wsAcademicYear.Columns().AdjustToContents();

                        // Worksheet 3: studentMarks.json
                        var wsStudentMarks = workbook.Worksheets.Add("studentMarks");
                        wsStudentMarks.Cell(1, 1).Value = "StudentId";
                        wsStudentMarks.Cell(1, 2).Value = "SubjectCode";
                        wsStudentMarks.Cell(1, 3).Value = "SubjectName";
                        wsStudentMarks.Cell(1, 4).Value = "Internal";
                        wsStudentMarks.Cell(1, 5).Value = "External";
                        wsStudentMarks.Cell(1, 6).Value = "Total";
                        wsStudentMarks.Cell(1, 7).Value = "Grade";
                        wsStudentMarks.Cell(1, 8).Value = "GradePoints";
                        wsStudentMarks.Cell(1, 9).Value = "Credits";
                        wsStudentMarks.Row(1).Style.Font.SetBold();
                        // Sample data
                        wsStudentMarks.Cell(2, 1).Value = "21VD1A6612";
                        wsStudentMarks.Cell(2, 2).Value = "18319";
                        wsStudentMarks.Cell(2, 3).Value = "Mathematics";
                        wsStudentMarks.Cell(2, 4).Value = "18";
                        wsStudentMarks.Cell(2, 5).Value = "54";
                        wsStudentMarks.Cell(2, 6).Value = "72";
                        wsStudentMarks.Cell(2, 7).Value = "A";
                        wsStudentMarks.Cell(2, 8).Value = "8";
                        wsStudentMarks.Cell(2, 9).Value = "4";
                        wsStudentMarks.Cell(3, 1).Value = "23VD1A0510";
                        wsStudentMarks.Cell(3, 2).Value = "18320";
                        wsStudentMarks.Cell(3, 3).Value = "Physics";
                        wsStudentMarks.Cell(3, 4).Value = "15";
                        wsStudentMarks.Cell(3, 5).Value = "45";
                        wsStudentMarks.Cell(3, 6).Value = "60";
                        wsStudentMarks.Cell(3, 7).Value = "B";
                        wsStudentMarks.Cell(3, 8).Value = "7";
                        wsStudentMarks.Cell(3, 9).Value = "3";
                        wsStudentMarks.Columns().AdjustToContents();

                        // Worksheet 4: subjectCodes.json
                        var wsSubjectCodes = workbook.Worksheets.Add("subjectCodes");
                        wsSubjectCodes.Cell(1, 1).Value = "SubjectCode";
                        wsSubjectCodes.Cell(1, 2).Value = "Year";
                        wsSubjectCodes.Cell(1, 3).Value = "Semester";
                    wsSubjectCodes.Cell(1, 4).Value = "Credits";
                    wsSubjectCodes.Row(1).Style.Font.SetBold();
                        // Sample data
                        wsSubjectCodes.Cell(2, 1).Value = "18319";
                        wsSubjectCodes.Cell(2, 2).Value = "2";
                        wsSubjectCodes.Cell(2, 3).Value = "1";
                    wsSubjectCodes.Cell(2, 4).Value = "4";
                        wsSubjectCodes.Cell(3, 1).Value = "18320";
                        wsSubjectCodes.Cell(3, 2).Value = "2";
                        wsSubjectCodes.Cell(3, 3).Value = "1";
                    wsSubjectCodes.Cell(3, 4).Value = "3.5";
                    wsSubjectCodes.Columns().AdjustToContents();

                        // Save the file
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Sample Excel file downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void gpbtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.studentwisegps(), sender);

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.getbtn(), sender);
        }

        private void swgpands_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.subject_wise_Gp(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.StudentAttemptbackup(), sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Batch_wise_backlog_report(), sender);
        }
    }
}
