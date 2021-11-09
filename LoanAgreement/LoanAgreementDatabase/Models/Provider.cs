using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class Provider
    {
        public Provider()
        {
            Operation = new HashSet<Operation>();
        }

        public int Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Operation> Operation { get; set; }
    }
}
