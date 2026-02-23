//using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.Cmp;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//using OfficeOpenXml;

//using System.IO;

//using ExcelDataReader;
//using static project_RYS.uploadform;
//using System.Net.Configuration;


//namespace project_RYS.Forms
//{
//    public partial class FormUploadMarks : Form
//    {

//        //private string subjectCodesFilePath;
//        private string studentMarksFilePath;
//        private const string subjectCodesJson = "subjectCodes.json";
//        private const string studentMarksJson = "studentMarks.json";

//        string msg = "";

//        //string msg = "";
//        private List<Subject> existingSubjectCodes = new List<Subject>();

//        public FormUploadMarks()
//        {
//            InitializeComponent();
//            lblMarksFileName.Text = "";
//            LoadSubjectCodesFromJson();
//            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Set based on your usage
//        }


//        private void LoadSubjectCodesFromJson()
//        {
//            try
//            {
//                if (File.Exists(subjectCodesJson))
//                {
//                    var jsonContent = File.ReadAllText(subjectCodesJson);
//                    existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonContent) ?? new List<Subject>();

//                }
//                else
//                {
//                    //MessageBox.Show("Subject codes JSON file not found. Please upload the subject codes first.");
//                    msg = "Subject codes JSON file not found. Please upload the subject codes first.";
//                    msgbox mb = new msgbox(msg);
//                    mb.ShowDialog();
//                    existingSubjectCodes = new List<Subject>();
//                }
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show("Error loading subject codes: " + ex.Message);
//                msg = "Error loading subject codes!" + ex;
//                msgbox mb = new msgbox(msg);
//                mb.ShowDialog();
//            }
//        }

//private void lblStatus_Click(object sender, EventArgs e)
//{

//}

//private void label1_Click(object sender, EventArgs e)
//{

//}

//        private void btnUploadMarks_Click(object sender, EventArgs e)
//        {

//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    studentMarksFilePath = openFileDialog.FileName;
//                    lblMarksFileName.Text = Path.GetFileName(studentMarksFilePath);
//                    lblStatus.Text = "Student marks file uploaded successfully.";

//                }
//            }
//        }

//        private void btnNext_Click(object sender, EventArgs e)
//        {

//            if (existingSubjectCodes == null || !existingSubjectCodes.Any())
//            {
//                msg = "No subject codes found. Please upload and save subject codes first.";
//                msgbox m1 = new msgbox(msg);
//                m1.ShowDialog();
//                //MessageBox.Show("No subject codes found. Please upload and save subject codes first.");
//                return;
//            }


//            if (string.IsNullOrEmpty(studentMarksFilePath))
//            {
//                msg = "Please upload the student marks file first.";
//                msgbox m2 = new msgbox(msg);
//                m2.ShowDialog();
//                //MessageBox.Show("Please upload the student marks file first.");
//                return;
//            }

//            var newStudentMarks = ReadStudentMarks(studentMarksFilePath);
//            if (newStudentMarks == null || !newStudentMarks.Any())
//            {
//                msg = "Failed to read student marks.";
//                msgbox m3 = new msgbox(msg);
//                m3.ShowDialog();
//                //MessageBox.Show("Failed to read student marks.");
//                return;
//            }


//            List<uploadform.StudentMark> existingStudentMarks = new List<uploadform.StudentMark>();
//            if (File.Exists(studentMarksJson))
//            {
//                var existingJson = File.ReadAllText(studentMarksJson);
//                existingStudentMarks = JsonConvert.DeserializeObject<List<uploadform.StudentMark>>(existingJson) ?? new List<uploadform.StudentMark>(); // Ensure it's not null
//            }


//            // Check for new subject codes in marks
//            List<string> newSubjectCodes = newStudentMarks
//                .Where(newMark => !existingSubjectCodes.Any(subj => subj.SubjectCode == newMark.SubjectCode))
//                .Select(newMark => newMark.SubjectCode)
//                .Distinct()
//                .ToList();

//            if (newSubjectCodes.Any())
//            {
//                string newSubjectCodesMessage = "New subject codes detected in marks:\n" + string.Join("\n", newSubjectCodes);
//                MessageBox.Show(newSubjectCodesMessage + "\n\nPlease upload these subject codes first.");
//                return;
//            }

//            foreach (var newMark in newStudentMarks)
//            {

//                var existingMark = existingStudentMarks
//                    .FirstOrDefault(mark => mark.StudentId == newMark.StudentId && mark.SubjectCode == newMark.SubjectCode);
//                if (existingMark != null)
//                {
//                    existingMark.Internal = newMark.Internal;
//                    existingMark.External = newMark.External;
//                    existingMark.Total = newMark.Total;
//                    existingMark.Grade = newMark.Grade;
//                    existingMark.GradePoints = newMark.GradePoints;
//                    existingMark.Credits = newMark.Credits;
//                }
//                else
//                {
//                    existingStudentMarks.Add(newMark);
//                }
//            }

//            File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(existingStudentMarks));

//            lblStatus.Text = "Processing completed successfully.";
//            //MessageBox.Show("Data uploaded successfully.");
//            msg = "Marks  uploaded successfully, so back to dashboard";
//            msgbox mb = new msgbox(msg);
//            mb.ShowDialog();



//        }

//        private List<uploadform.StudentMark> ReadStudentMarks(string filePath)
//        {
//            var studentMarks = new List<uploadform.StudentMark>();
//            try
//            {
//                using (var package = new ExcelPackage(new FileInfo(filePath)))
//                {
//                    var worksheet = package.Workbook.Worksheets[0];
//                    int rowCount = worksheet.Dimension.Rows;

//                    for (int row = 2; row <= rowCount; row++)
//                    {
//                        var mark = new uploadform.StudentMark
//                        {
//                            StudentId = worksheet.Cells[row, 1].Text.Trim(),

//                            SubjectCode = worksheet.Cells[row, 2].Text.Trim(),
//                            SubjectName = worksheet.Cells[row, 3].Text.Trim(),
//                            Internal = worksheet.Cells[row, 4].Text.Trim(),
//                            External = worksheet.Cells[row, 5].Text.Trim(),
//                            Total = worksheet.Cells[row, 6].Text.Trim(),
//                            Grade = worksheet.Cells[row, 7].Text.Trim(),
//                            GradePoints = worksheet.Cells[row, 8].Text.Trim(),
//                            Credits = worksheet.Cells[row, 9].Text.Trim()
//                        };

//                        studentMarks.Add(mark);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error reading student marks: " + ex.Message);
//            }

//            return studentMarks;
//        }
//    }
//}





//using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.Cmp;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using OfficeOpenXml;
//using static project_RYS.uploadform;

//namespace project_RYS.Forms
//{
//    public partial class FormUploadMarks : Form
//    {
//        private string studentMarksFilePath;
//        private const string subjectCodesJson = "subjectCodes.json";
//        private const string studentMarksJson = "studentMarks.json";

//        private List<Subject> existingSubjectCodes = new List<Subject>();

//        public FormUploadMarks()
//        {
//            InitializeComponent();
//            lblMarksFileName.Text = "";
//            LoadSubjectCodesFromJson();
//            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Set based on your usage
//        }

//        private void LoadSubjectCodesFromJson()
//        {
//            try
//            {
//                string subjectCodesPath = Path.Combine(ConfigManager.GetJsonFilePath(), subjectCodesJson);
//                if (File.Exists(subjectCodesPath))
//                {
//                    var jsonContent = File.ReadAllText(subjectCodesPath);
//                    existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonContent) ?? new List<Subject>();

//                    Console.WriteLine("Existing Subject Codes:");
//                    foreach (var subject in existingSubjectCodes)
//                    {
//                        Console.WriteLine($"Code: {subject.SubjectCode}, Year: {subject.Year}, Semester: {subject.Semester}");
//                    }
//                }
//                else
//                {
//                    MessageBox.Show("Subject codes JSON file not found. Please upload the subject codes first.");
//                    existingSubjectCodes = new List<Subject>();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error loading subject codes: " + ex.Message);
//            }
//        }

//        private void btnUploadMarks_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    studentMarksFilePath = openFileDialog.FileName;
//                    lblMarksFileName.Text = Path.GetFileName(studentMarksFilePath);
//                    lblStatus.Text = "Student marks file uploaded successfully.";
//                }
//            }
//        }

//        private void btnNext_Click(object sender, EventArgs e)
//        {
//            if (existingSubjectCodes == null || !existingSubjectCodes.Any())
//            {
//                MessageBox.Show("No subject codes found. Please upload and save subject codes first.");
//                return;
//            }

//            if (string.IsNullOrEmpty(studentMarksFilePath))
//            {
//                MessageBox.Show("Please upload the student marks file first.");
//                return;
//            }

//            var newStudentMarks = ReadStudentMarks(studentMarksFilePath);
//            if (newStudentMarks == null || !newStudentMarks.Any())
//            {
//                MessageBox.Show("Failed to read student marks.");
//                return;
//            }

//            List<StudentMark> existingStudentMarks = new List<StudentMark>();

//            // Update to store in the configured JSON path
//            string studentMarksPath = Path.Combine(ConfigManager.GetJsonFilePath(), studentMarksJson);
//            if (File.Exists(studentMarksPath))
//            {
//                var existingJson = File.ReadAllText(studentMarksPath);
//                existingStudentMarks = JsonConvert.DeserializeObject<List<StudentMark>>(existingJson) ?? new List<StudentMark>();
//            }

//            List<string> newSubjectCodes = newStudentMarks
//                .Where(newMark => !existingSubjectCodes.Any(subj => subj.SubjectCode == newMark.SubjectCode))
//                .Select(newMark => newMark.SubjectCode)
//                .Distinct()
//                .ToList();

//            if (newSubjectCodes.Any())
//            {
//                string newSubjectCodesMessage = "New subject codes detected in marks:\n" + string.Join("\n", newSubjectCodes);
//                MessageBox.Show(newSubjectCodesMessage + "\n\nPlease upload these subject codes first.");
//                return;
//            }

//            foreach (var newMark in newStudentMarks)
//            {
//                var existingMark = existingStudentMarks
//                    .FirstOrDefault(mark => mark.StudentId == newMark.StudentId && mark.SubjectCode == newMark.SubjectCode);
//                if (existingMark != null)
//                {
//                    existingMark.Internal = newMark.Internal;
//                    existingMark.External = newMark.External;
//                    existingMark.Total = newMark.Total;
//                    existingMark.Grade = newMark.Grade;
//                    existingMark.GradePoints = newMark.GradePoints;
//                    existingMark.Credits = newMark.Credits;
//                }
//                else
//                {
//                    existingStudentMarks.Add(newMark);
//                }
//            }

//            File.WriteAllText(studentMarksPath, JsonConvert.SerializeObject(existingStudentMarks));
//            lblStatus.Text = "Processing completed successfully.";
//            MessageBox.Show("Marks uploaded successfully.");
//        }

//        private void lblStatus_Click(object sender, EventArgs e)
//        {

//        }

//        private void label1_Click(object sender, EventArgs e)
//        {

//        }

//        private List<StudentMark> ReadStudentMarks(string filePath)
//        {
//            var studentMarks = new List<StudentMark>();
//            try
//            {
//                using (var package = new ExcelPackage(new FileInfo(filePath)))
//                {
//                    var worksheet = package.Workbook.Worksheets[0];
//                    int rowCount = worksheet.Dimension.Rows;

//                    for (int row = 2; row <= rowCount; row++)
//                    {
//                        var mark = new StudentMark
//                        {
//                            StudentId = worksheet.Cells[row, 1].Text.Trim(),
//                            Name = worksheet.Cells[row, 2].Text.Trim(),
//                            SubjectCode = worksheet.Cells[row, 3].Text.Trim(),
//                            SubjectName = worksheet.Cells[row, 4].Text.Trim(),
//                            Internal = worksheet.Cells[row, 5].Text.Trim(),
//                            External = worksheet.Cells[row, 6].Text.Trim(),
//                            Total = worksheet.Cells[row, 7].Text.Trim(),
//                            Grade = worksheet.Cells[row, 8].Text.Trim(),
//                            GradePoints = worksheet.Cells[row, 9].Text.Trim(),
//                            Credits = worksheet.Cells[row, 10].Text.Trim()
//                        };

//                        studentMarks.Add(mark);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error reading student marks: " + ex.Message);
//            }

//            return studentMarks;
//        }
//    }
//}




//using Newtonsoft.Json;
//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace project_RYS.Forms
//{
//    public partial class FormUploadMarks : Form
//    {
//        private string studentMarksFilePath;
//        private const string subjectCodesJson = "subjectCodes.json";
//        private const string studentMarksJson = "studentMarks.json";
//        private const string studentNamesJson = "studentnames.json";
//        private const string studentAttemptsJson = "studentAttempts.json";
//        string msg = "";
//        private List<Subject> existingSubjectCodes = new List<Subject>();
//        private List<Student> existingStudentNames = new List<Student>();

//        public FormUploadMarks()
//        {
//            InitializeComponent();
//            lblMarksFileName.Text = "";
//            LoadSubjectCodesFromJson();
//            LoadStudentNamesFromJson();
//            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
//        }

//        private void lblStatus_Click(object sender, EventArgs e)
//        {

//        }

//        private void label1_Click(object sender, EventArgs e)
//        {

//        }
//        private void LoadSubjectCodesFromJson()
//        {
//            try
//            {
//                if (File.Exists(subjectCodesJson))
//                {
//                    var jsonContent = File.ReadAllText(subjectCodesJson);
//                    existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonContent) ?? new List<Subject>();
//                }
//                else
//                {
//                    msg = "Subject codes JSON file not found. Please upload the subject codes first.";
//                    ShowMessageBox(msg);
//                    existingSubjectCodes = new List<Subject>();
//                }
//            }
//            catch (Exception ex)
//            {
//                msg = "Error loading subject codes! " + ex.Message;
//                ShowMessageBox(msg);
//            }
//        }

//        private void LoadStudentNamesFromJson()
//        {
//            try
//            {
//                if (File.Exists(studentNamesJson))
//                {
//                    var jsonContent = File.ReadAllText(studentNamesJson);
//                    existingStudentNames = JsonConvert.DeserializeObject<List<Student>>(jsonContent) ?? new List<Student>();
//                }
//                else
//                {
//                    msg = "Student names JSON file not found. Please upload the student names first.";
//                    ShowMessageBox(msg);
//                    existingStudentNames = new List<Student>();
//                }
//            }
//            catch (Exception ex)
//            {
//                msg = "Error loading student names! " + ex.Message;
//                ShowMessageBox(msg);
//            }
//        }

//        //private void btnUploadMarks_Click(object sender, EventArgs e)
//        //{
//        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
//        //    {
//        //        openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
//        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
//        //        {
//        //            studentMarksFilePath = openFileDialog.FileName;
//        //            lblMarksFileName.Text = Path.GetFileName(studentMarksFilePath);
//        //            lblStatus.Text = "Student marks file uploaded successfully.";
//        //        }
//        //    }
//        //}
//        private void btnUploadMarks_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    studentMarksFilePath = openFileDialog.FileName;
//                    lblMarksFileName.Text = Path.GetFileName(studentMarksFilePath);
//                    lblStatus.Text = "Student marks file uploaded successfully.";
//                }
//            }
//        }

//        //private void btnNext_Click(object sender, EventArgs e)
//        //{
//        //    if (existingSubjectCodes == null || !existingSubjectCodes.Any())
//        //    {
//        //        msg = "No subject codes found. Please upload and save subject codes first.";
//        //        ShowMessageBox(msg);
//        //        return;
//        //    }

//        //    if (existingStudentNames == null || !existingStudentNames.Any())
//        //    {
//        //        msg = "No student names found. Please upload and save student names first.";
//        //        ShowMessageBox(msg);
//        //        return;
//        //    }

//        //    if (string.IsNullOrEmpty(studentMarksFilePath))
//        //    {
//        //        msg = "Please upload the student marks file first.";
//        //        ShowMessageBox(msg);
//        //        return;
//        //    }

//        //    var newStudentMarks = ReadStudentMarks(studentMarksFilePath);
//        //    if (newStudentMarks == null || !newStudentMarks.Any())
//        //    {
//        //        msg = "Failed to read student marks.";
//        //        ShowMessageBox(msg);
//        //        return;
//        //    }

//        //    // Validate student IDs against studentNamesJson
//        //    var invalidStudentIds = newStudentMarks
//        //        .Where(mark => !existingStudentNames.Any(student => student.StudentId == mark.StudentId))
//        //        .Select(mark => mark.StudentId)
//        //        .Distinct()
//        //        .ToList();

//        //    if (invalidStudentIds.Any())
//        //    {
//        //        string invalidIdsMessage = "The following student IDs are not found in student names JSON:\n" +
//        //                                   string.Join("\n", invalidStudentIds);
//        //        ShowMessageBox(invalidIdsMessage + "\n\nPlease update the student names file.");
//        //        return;
//        //    }

//        //    // Load existing student marks
//        //    List<uploadform.StudentMark> existingStudentMarks = new List<uploadform.StudentMark>();
//        //    if (File.Exists(studentMarksJson))
//        //    {
//        //        var existingJson = File.ReadAllText(studentMarksJson);
//        //        existingStudentMarks = JsonConvert.DeserializeObject<List<uploadform.StudentMark>>(existingJson) ?? new List<uploadform.StudentMark>();
//        //    }

//        //    // Check for new subject codes in marks
//        //    List<string> newSubjectCodes = newStudentMarks
//        //        .Where(newMark => !existingSubjectCodes.Any(subj => subj.SubjectCode == newMark.SubjectCode))
//        //        .Select(newMark => newMark.SubjectCode)
//        //        .Distinct()
//        //        .ToList();

//        //    if (newSubjectCodes.Any())
//        //    {
//        //        string newSubjectCodesMessage = "New subject codes detected in marks:\n" + string.Join("\n", newSubjectCodes);
//        //        ShowMessageBox(newSubjectCodesMessage + "\n\nPlease upload these subject codes first.");
//        //        return;
//        //    }

//        //    // Update or add marks
//        //    foreach (var newMark in newStudentMarks)
//        //    {
//        //        var existingMark = existingStudentMarks
//        //            .FirstOrDefault(mark => mark.StudentId.ToUpper() == newMark.StudentId.ToUpper() && mark.SubjectCode == newMark.SubjectCode);
//        //        if (existingMark != null)
//        //        {
//        //            existingMark.Internal = newMark.Internal;
//        //            existingMark.External = newMark.External;
//        //            existingMark.Total = newMark.Total;
//        //            existingMark.Grade = newMark.Grade;
//        //            existingMark.GradePoints = newMark.GradePoints;
//        //            existingMark.Credits = newMark.Credits;
//        //        }
//        //        else
//        //        {
//        //            existingStudentMarks.Add(newMark);
//        //        }
//        //    }

//        //    File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(existingStudentMarks));

//        //    lblStatus.Text = "Processing completed successfully.";
//        //    msg = "Marks uploaded successfully, so back to dashboard";
//        //    ShowMessageBox(msg);
//        //}

//        private void btnNext_Click(object sender, EventArgs e)
//        {
//            if (existingSubjectCodes == null || !existingSubjectCodes.Any())
//            {
//                msg = "No subject codes found. Please upload and save subject codes first.";
//                ShowMessageBox(msg);
//                return;
//            }

//            if (existingStudentNames == null || !existingStudentNames.Any())
//            {
//                msg = "No student names found. Please upload and save student names first.";
//                ShowMessageBox(msg);
//                return;
//            }

//            if (string.IsNullOrEmpty(studentMarksFilePath))
//            {
//                msg = "Please upload the student marks file first.";
//                ShowMessageBox(msg);
//                return;
//            }

//            var newStudentMarks = ReadStudentMarks(studentMarksFilePath);
//            if (newStudentMarks == null || !newStudentMarks.Any())
//            {
//                msg = "Failed to read student marks.";
//                ShowMessageBox(msg);
//                return;
//            }

//            // Validate student IDs
//            var invalidStudentIds = newStudentMarks
//                .Where(mark => !existingStudentNames.Any(student => student.StudentId == mark.StudentId))
//                .Select(mark => mark.StudentId)
//                .Distinct()
//                .ToList();

//            if (invalidStudentIds.Any())
//            {
//                string invalidIdsMessage = "The following student IDs are not found in student names JSON:\n" +
//                                           string.Join("\n", invalidStudentIds);
//                ShowMessageBox(invalidIdsMessage + "\n\nPlease update the student names file.");
//                return;
//            }

//            // Validate subject codes
//            List<string> newSubjectCodes = newStudentMarks
//                .Where(newMark => !existingSubjectCodes.Any(subj => subj.SubjectCode == newMark.SubjectCode))
//                .Select(newMark => newMark.SubjectCode)
//                .Distinct()
//                .ToList();

//            if (newSubjectCodes.Any())
//            {
//                string newSubjectCodesMessage = "New subject codes detected in marks:\n" + string.Join("\n", newSubjectCodes);
//                ShowMessageBox(newSubjectCodesMessage + "\n\nPlease upload these subject codes first.");
//                return;
//            }

//            // Load existing student marks and attempts
//            List<uploadform.StudentMark> existingStudentMarks = LoadJsonData<uploadform.StudentMark>(studentMarksJson);
//            List<StudentAttempt> studentAttempts = LoadJsonData<StudentAttempt>(studentAttemptsJson);

//            // Update marks and track attempts
//            foreach (var newMark in newStudentMarks)
//            {
//                var existingMark = existingStudentMarks
//                    .FirstOrDefault(mark => mark.StudentId.ToUpper() == newMark.StudentId.ToUpper() && mark.SubjectCode == newMark.SubjectCode);
//                if (existingMark != null)
//                {
//                    // Update existing mark
//                    existingMark.Internal = newMark.Internal;
//                    existingMark.External = newMark.External;
//                    existingMark.Total = newMark.Total;
//                    existingMark.Grade = newMark.Grade;
//                    existingMark.GradePoints = newMark.GradePoints;
//                    existingMark.Credits = newMark.Credits;
//                }
//                else
//                {
//                    // Add new mark
//                    existingStudentMarks.Add(newMark);
//                }

//                // Update studentAttempts.json
//                var attemptEntry = studentAttempts.FirstOrDefault(a =>
//                    a.StudentId.ToUpper() == newMark.StudentId.ToUpper() && a.SubjectCode == newMark.SubjectCode);
//                if (attemptEntry == null)
//                {
//                    attemptEntry = new StudentAttempt
//                    {
//                        StudentId = newMark.StudentId,
//                        SubjectCode = newMark.SubjectCode,
//                        Attempts = new List<Attempt>()
//                    };
//                    studentAttempts.Add(attemptEntry);
//                }

//                int attemptNumber = attemptEntry.Attempts.Count + 1;
//                attemptEntry.Attempts.Add(new Attempt
//                {
//                    AttemptNumber = attemptNumber,
//                    GradePoints = newMark.GradePoints,
//                    Credits = newMark.Credits,
//                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd")
//                });
//            }

//            // Save updated JSON files
//            File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(existingStudentMarks, Formatting.Indented));
//            File.WriteAllText(studentAttemptsJson, JsonConvert.SerializeObject(studentAttempts, Formatting.Indented));

//            lblStatus.Text = "Processing completed successfully.";
//            msg = "Marks uploaded successfully.";
//            ShowMessageBox(msg);
//        }

//        private List<T> LoadJsonData<T>(string filePath)
//        {
//            if (!File.Exists(filePath)) return new List<T>();
//            var json = File.ReadAllText(filePath);
//            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
//        }

//        private List<uploadform.StudentMark> ReadStudentMarks(string filePath)
//        {
//            var studentMarks = new List<uploadform.StudentMark>();
//            try
//            {
//                using (var package = new ExcelPackage(new FileInfo(filePath)))
//                {
//                    var worksheet = package.Workbook.Worksheets[0];
//                    int rowCount = worksheet.Dimension.Rows;

//                    for (int row = 2; row <= rowCount; row++)
//                    {
//                        var mark = new uploadform.StudentMark
//                        {
//                            StudentId = worksheet.Cells[row, 1].Text.ToUpper().Trim(),
//                            SubjectCode = worksheet.Cells[row, 2].Text.Trim(),
//                            SubjectName = worksheet.Cells[row, 3].Text.Trim(),
//                            Internal = worksheet.Cells[row, 4].Text.Trim(),
//                            External = worksheet.Cells[row, 5].Text.Trim(),
//                            Total = worksheet.Cells[row, 6].Text.Trim(),
//                            Grade = worksheet.Cells[row, 7].Text.Trim(),
//                            GradePoints = worksheet.Cells[row, 8].Text.Trim(),
//                            Credits = worksheet.Cells[row, 9].Text.Trim()
//                        };

//                        studentMarks.Add(mark);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error reading student marks: " + ex.Message);
//            }

//            return studentMarks;
//        }

//        private void ShowMessageBox(string message)
//        {
//            msgbox mb = new msgbox(message);
//            mb.ShowDialog();
//        }

//        public class Student
//        {
//            public string StudentId { get; set; }
//            public string Name { get; set; }
//        }

//        private void backbtn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//    }
//    public class StudentAttempt
//    {
//        public string StudentId { get; set; }
//        public string SubjectCode { get; set; }
//        public List<Attempt> Attempts { get; set; }
//    }

//    public class Attempt
//    {
//        public int AttemptNumber { get; set; }
//        public string GradePoints { get; set; }
//        public string Credits { get; set; }
//        public string Timestamp { get; set; }
//    }
//}


//```csharp
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace project_RYS.Forms
{
    public partial class FormUploadMarks : Form
    {
        private string studentMarksFilePath;
        private const string subjectCodesJson = "subjectCodes.json";
        private const string studentMarksJson = "studentMarks.json";
        private const string studentNamesJson = "studentnames.json";
        private const string studentAttemptsJson = "studentAttempts.json";
        string msg = "";
        private List<Subject> existingSubjectCodes = new List<Subject>();
        private List<Student> existingStudentNames = new List<Student>();

        public FormUploadMarks()
        {
            InitializeComponent();
            lblMarksFileName.Text = "";
            LoadSubjectCodesFromJson();
            LoadStudentNamesFromJson();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void LoadSubjectCodesFromJson()
        {
            try
            {
                if (File.Exists(subjectCodesJson))
                {
                    var jsonContent = File.ReadAllText(subjectCodesJson);
                    existingSubjectCodes = JsonConvert.DeserializeObject<List<Subject>>(jsonContent) ?? new List<Subject>();
                }
                else
                {
                    msg = "Subject codes JSON file not found. Please upload the subject codes first.";
                    ShowMessageBox(msg);
                    existingSubjectCodes = new List<Subject>();
                }
            }
            catch (Exception ex)
            {
                msg = "Error loading subject codes! " + ex.Message;
                ShowMessageBox(msg);
            }
        }

        private void LoadStudentNamesFromJson()
        {
            try
            {
                if (File.Exists(studentNamesJson))
                {
                    var jsonContent = File.ReadAllText(studentNamesJson);
                    existingStudentNames = JsonConvert.DeserializeObject<List<Student>>(jsonContent) ?? new List<Student>();
                }
                else
                {
                    msg = "Student names JSON file not found. Please upload the student names first.";
                    ShowMessageBox(msg);
                    existingStudentNames = new List<Student>();
                }
            }
            catch (Exception ex)
            {
                msg = "Error loading student names! " + ex.Message;
                ShowMessageBox(msg);
            }
        }
        private void btnUploadMarks_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    studentMarksFilePath = openFileDialog.FileName;
                    lblMarksFileName.Text = Path.GetFileName(studentMarksFilePath);
                    lblStatus.Text = "Student marks file uploaded successfully.";
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (existingSubjectCodes == null || !existingSubjectCodes.Any())
            {
                msg = "No subject codes found. Please upload and save subject codes first.";
                ShowMessageBox(msg);
                return;
            }

            if (existingStudentNames == null || !existingStudentNames.Any())
            {
                msg = "No student names found. Please upload and save student names first.";
                ShowMessageBox(msg);
                return;
            }

            if (string.IsNullOrEmpty(studentMarksFilePath))
            {
                msg = "Please upload the student marks file first.";
                ShowMessageBox(msg);
                return;
            }

            var newStudentMarks = ReadStudentMarks(studentMarksFilePath);
            if (newStudentMarks == null || !newStudentMarks.Any())
            {
                msg = "Failed to read student marks.";
                ShowMessageBox(msg);
                return;
            }

            // Validate student IDs
            var invalidStudentIds = newStudentMarks
                .Where(mark => !existingStudentNames.Any(student => student.StudentId == mark.StudentId))
                .Select(mark => mark.StudentId)
                .Distinct()
                .ToList();

            if (invalidStudentIds.Any())
            {
                string invalidIdsMessage = "The following student IDs are not found in student names JSON:\r\n" +
                                           string.Join("\r\n", invalidStudentIds);
                ShowMessageBox(invalidIdsMessage + "\r\n\r\nPlease update the student names file.");
                return;
            }


            List<string> newSubjectCodes = newStudentMarks
    .Where(newMark => !existingSubjectCodes
        .Any(subj => subj.SubjectCode.Equals(newMark.SubjectCode, StringComparison.OrdinalIgnoreCase)))
    .Select(newMark => newMark.SubjectCode)
    .Distinct(StringComparer.OrdinalIgnoreCase)
    .ToList();

            if (newSubjectCodes.Any())
            {
                string newSubjectCodesMessage = "New subject codes detected in marks:\n" +
                    string.Join(", ", newSubjectCodes);
                ShowMessageBox(newSubjectCodesMessage + "\n\nPlease upload these subject codes first.");
                return;
            }


            // Load existing student marks and attempts
            List<uploadform.StudentMark> existingStudentMarks = LoadJsonData<uploadform.StudentMark>(studentMarksJson);
            List<StudentAttempt> studentAttempts = LoadJsonData<StudentAttempt>(studentAttemptsJson);

            // Update marks and track attempts
            foreach (var newMark in newStudentMarks)
            {
                var existingMark = existingStudentMarks
                    .FirstOrDefault(mark => mark.StudentId.ToUpper() == newMark.StudentId.ToUpper() && mark.SubjectCode == newMark.SubjectCode);
                if (existingMark != null)
                {
                    // Update existing mark
                    existingMark.Internal = newMark.Internal;
                    existingMark.External = newMark.External;
                    existingMark.Total = newMark.Total;
                    existingMark.Grade = newMark.Grade;
                    existingMark.GradePoints = newMark.GradePoints;
                    existingMark.Credits = newMark.Credits;
                }
                else
                {
                    // Add new mark
                    existingStudentMarks.Add(newMark);
                }

                // Update studentAttempts.json only for failed or previously failed subjects
                var attemptEntry = studentAttempts.FirstOrDefault(a =>
                    a.StudentId.ToUpper() == newMark.StudentId.ToUpper() && a.SubjectCode == newMark.SubjectCode);

                bool isFailed = newMark.GradePoints == "0";
                bool hasPriorFailedAttempt = attemptEntry != null && attemptEntry.Attempts.Any(a => a.GradePoints == "0");
                bool isPassing = !isFailed && double.TryParse(newMark.GradePoints, out double gp) && gp > 0;

                if (isFailed || (isPassing && hasPriorFailedAttempt))
                {
                    if (attemptEntry == null)
                    {
                        attemptEntry = new StudentAttempt
                        {
                            StudentId = newMark.StudentId,
                            SubjectCode = newMark.SubjectCode,
                            Attempts = new List<Attempt>()
                        };
                        studentAttempts.Add(attemptEntry);
                    }

                    int attemptNumber = attemptEntry.Attempts.Count + 1;
                    attemptEntry.Attempts.Add(new Attempt
                    {
                        AttemptNumber = attemptNumber,
                        GradePoints = newMark.GradePoints,
                        Credits = newMark.Credits,
                        Timestamp = DateTime.Now.ToString("yyyy-MM-dd")
                    });
                }
            }

            // Save updated JSON files
            File.WriteAllText(studentMarksJson, JsonConvert.SerializeObject(existingStudentMarks, Formatting.Indented));
            File.WriteAllText(studentAttemptsJson, JsonConvert.SerializeObject(studentAttempts, Formatting.Indented));

            lblStatus.Text = "Processing completed successfully.";
            msg = "Marks uploaded successfully.";
            ShowMessageBox(msg);
        }

        private List<T> LoadJsonData<T>(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

       
        private List<uploadform.StudentMark> ReadStudentMarks(string filePath)
        {
            var studentMarks = new List<uploadform.StudentMark>();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        // Read all cell values into variables
                        string studentId = worksheet.Cells[row, 1].Text?.Trim().ToUpper() ?? "";
                        string subjectCode = worksheet.Cells[row, 2].Text?.Trim() ?? "";
                        string subjectName = worksheet.Cells[row, 3].Text?.Trim() ?? "";
                        string internalMark = worksheet.Cells[row, 4].Text?.Trim();
                        string externalMark = worksheet.Cells[row, 5].Text?.Trim();
                        string total = worksheet.Cells[row, 6].Text?.Trim();
                        string grade = worksheet.Cells[row, 7].Text?.Trim();
                        string gradePoints = worksheet.Cells[row, 8].Text?.Trim();
                        string credits = worksheet.Cells[row, 9].Text?.Trim();

                        // Skip the row if all fields are empty
                        if (string.IsNullOrWhiteSpace(studentId) &&
                            string.IsNullOrWhiteSpace(subjectCode) &&
                            string.IsNullOrWhiteSpace(subjectName) &&
                            string.IsNullOrWhiteSpace(internalMark) &&
                            string.IsNullOrWhiteSpace(externalMark) &&
                            string.IsNullOrWhiteSpace(total) &&
                            string.IsNullOrWhiteSpace(grade) &&
                            string.IsNullOrWhiteSpace(gradePoints) &&
                            string.IsNullOrWhiteSpace(credits))
                        {
                            continue; // Skip empty row
                        }

                        // Fill empty fields with "0"
                        internalMark = string.IsNullOrWhiteSpace(internalMark) ? "0" : internalMark;
                        externalMark = string.IsNullOrWhiteSpace(externalMark) ? "0" : externalMark;
                        total = string.IsNullOrWhiteSpace(total) ? "0" : total;
                        grade = string.IsNullOrWhiteSpace(grade) ? "0" : grade;
                        gradePoints = string.IsNullOrWhiteSpace(gradePoints) ? "0" : gradePoints;
                        credits = string.IsNullOrWhiteSpace(credits) ? "0" : credits;

                        // Optional: you may also want to ensure that subjectCode and studentId are not empty
                        if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(subjectCode))
                        {
                            continue; // essential fields missing, skip row
                        }

                        var mark = new uploadform.StudentMark
                        {
                            StudentId = studentId,
                            SubjectCode = subjectCode,
                            SubjectName = subjectName,
                            Internal = internalMark,
                            External = externalMark,
                            Total = total,
                            Grade = grade,
                            GradePoints = gradePoints,
                            Credits = credits
                        };

                        studentMarks.Add(mark);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading student marks: " + ex.Message);
            }

            return studentMarks;
        }


        private void ShowMessageBox(string message)
        {
            msgbox mb = new msgbox(message);
            mb.ShowDialog();
        }

        public class Student
        {
            public string StudentId { get; set; }
            public string Name { get; set; }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    public class StudentAttempt
    {
        public string StudentId { get; set; }
        public string SubjectCode { get; set; }
        public List<Attempt> Attempts { get; set; }
    }

    public class Attempt
    {
        public int AttemptNumber { get; set; }
        public string GradePoints { get; set; }
        public string Credits { get; set; }
        public string Timestamp { get; set; }
    }
}