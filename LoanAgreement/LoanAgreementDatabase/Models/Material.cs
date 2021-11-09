using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class Material
    {
        public Material()
        {
            TablePart = new HashSet<TablePart>();
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<TablePart> TablePart { get; set; }
    }
}
