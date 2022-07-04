using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class IntroducePhoto
    {
        public int IntroducePhotosId { get; set; }
        public int? IntroduceId { get; set; }
        public byte[] IntroducePhoto1 { get; set; }

        public virtual Introduce Introduce { get; set; }
    }
}
