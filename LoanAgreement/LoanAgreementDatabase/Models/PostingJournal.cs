using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class PostingJournal
    {
        public int Code { get; set; }
        public int Debetcheck { get; set; }
        public string Subcontodebet1 { get; set; }
        public string Subcontodebet2 { get; set; }
        public string Subcontodebet3 { get; set; }
        public int Creditcheck { get; set; }
        public string Subcontocredit1 { get; set; }
        public string Subcontocredit2 { get; set; }
        public string Subcontocredit3 { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public int Operationcode { get; set; }
        public string Comment { get; set; }

        public virtual ChartOfAccounts CreditcheckNavigation { get; set; }
        public virtual ChartOfAccounts DebetcheckNavigation { get; set; }
        public virtual TablePart OperationcodeNavigation { get; set; }
    }
}
