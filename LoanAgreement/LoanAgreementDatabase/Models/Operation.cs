using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class Operation
    {
        public Operation()
        {
            TablePart = new HashSet<TablePart>();
        }

        public int Code { get; set; }
        public string Typeofoperation { get; set; }
        public DateTime Date { get; set; }
        public int? Providercode { get; set; }
        public int? Warehousesendercode { get; set; }
        public int? Warehousereceivercode { get; set; }
        public int? Subdivisioncode { get; set; }
        public int? Responsiblesendercode { get; set; }
        public int? Responsiblereceivercode { get; set; }
        public decimal? Price { get; set; }

        public virtual Provider ProvidercodeNavigation { get; set; }
        public virtual ResponsiblePerson ResponsiblereceivercodeNavigation { get; set; }
        public virtual ResponsiblePerson ResponsiblesendercodeNavigation { get; set; }
        public virtual Subdivision SubdivisioncodeNavigation { get; set; }
        public virtual Subdivision WarehousereceivercodeNavigation { get; set; }
        public virtual Subdivision WarehousesendercodeNavigation { get; set; }
        public virtual ICollection<TablePart> TablePart { get; set; }
    }
}
