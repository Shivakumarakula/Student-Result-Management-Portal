using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_RYS.Forms
{
    public class StudentR
    {

            public string StudentId { get; set; }
            public string StudentName { get; set; }
            public Dictionary<string, string> SubjectResults { get; set; }
            public string TotalGradePoints { get; set; }
            public string SGPA { get; set; }
        
    }
}

