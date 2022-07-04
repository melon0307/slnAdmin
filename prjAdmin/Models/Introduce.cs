using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Introduce
    {
        public Introduce()
        {
            IntroducePhotos = new HashSet<IntroducePhoto>();
        }

        public int IntroducesId { get; set; }
        public string IntroducesName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IntroducePhoto> IntroducePhotos { get; set; }
    }
}
