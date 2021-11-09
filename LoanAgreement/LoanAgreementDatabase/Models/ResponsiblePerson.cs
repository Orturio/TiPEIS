using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class ResponsiblePerson
    {
        public ResponsiblePerson()
        {
            OperationResponsiblereceivercodeNavigation = new HashSet<Operation>();
            OperationResponsiblesendercodeNavigation = new HashSet<Operation>();
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }

        public virtual ICollection<Operation> OperationResponsiblereceivercodeNavigation { get; set; }
        public virtual ICollection<Operation> OperationResponsiblesendercodeNavigation { get; set; }
    }
}
