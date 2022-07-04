using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class QuestionTable
    {
        public QuestionTable()
        {
            Qquestionnaires = new HashSet<Qquestionnaire>();
        }

        public int QuestionTableId { get; set; }
        public string QuestionTableName { get; set; }
        public int? QuestionTableDetailsId { get; set; }

        public virtual QuestionTableDetail QuestionTableDetails { get; set; }
        public virtual ICollection<Qquestionnaire> Qquestionnaires { get; set; }
    }
}
