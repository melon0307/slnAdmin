using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class QuestionTableDetail
    {
        public QuestionTableDetail()
        {
            AnswerTableDetails = new HashSet<AnswerTableDetail>();
            QuestionTables = new HashSet<QuestionTable>();
        }

        public int QuestionTableDetailsId { get; set; }
        public string Q1 { get; set; }
        public string A11 { get; set; }
        public string A12 { get; set; }
        public string A13 { get; set; }
        public string A14 { get; set; }
        public string A15 { get; set; }
        public string Q2 { get; set; }
        public string A21 { get; set; }
        public string A22 { get; set; }
        public string A23 { get; set; }
        public string A24 { get; set; }
        public string A25 { get; set; }
        public string Q3 { get; set; }
        public string A31 { get; set; }
        public string A32 { get; set; }
        public string A33 { get; set; }
        public string A34 { get; set; }
        public string A35 { get; set; }
        public string A36 { get; set; }
        public string A37 { get; set; }
        public string Q4 { get; set; }
        public string A41 { get; set; }
        public string A42 { get; set; }
        public string A43 { get; set; }
        public string A44 { get; set; }
        public string Q5 { get; set; }
        public string A51 { get; set; }
        public string A52 { get; set; }
        public string A53 { get; set; }
        public string A54 { get; set; }
        public string Q6 { get; set; }
        public string A61 { get; set; }
        public string A62 { get; set; }
        public string A63 { get; set; }
        public string A64 { get; set; }
        public string A65 { get; set; }
        public string Q7 { get; set; }
        public string A71 { get; set; }
        public string A72 { get; set; }
        public string A73 { get; set; }
        public string A74 { get; set; }
        public string A75 { get; set; }

        public virtual ICollection<AnswerTableDetail> AnswerTableDetails { get; set; }
        public virtual ICollection<QuestionTable> QuestionTables { get; set; }
    }
}
