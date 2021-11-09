using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class Subdivision
    {
        public Subdivision()
        {
            OperationSubdivisioncodeNavigation = new HashSet<Operation>();
            OperationWarehousereceivercodeNavigation = new HashSet<Operation>();
            OperationWarehousesendercodeNavigation = new HashSet<Operation>();
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public bool Warehouse { get; set; }

        public virtual ICollection<Operation> OperationSubdivisioncodeNavigation { get; set; }
        public virtual ICollection<Operation> OperationWarehousereceivercodeNavigation { get; set; }
        public virtual ICollection<Operation> OperationWarehousesendercodeNavigation { get; set; }
    }
}
