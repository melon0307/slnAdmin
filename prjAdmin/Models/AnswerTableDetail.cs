using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class AnswerTableDetail
    {
        public int AnswerTableDetailsId { get; set; }
        public int? QuestionTableDetailsId { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Q6 { get; set; }
        public string Q7 { get; set; }

        public virtual QuestionTableDetail QuestionTableDetails { get; set; }
    }
}
