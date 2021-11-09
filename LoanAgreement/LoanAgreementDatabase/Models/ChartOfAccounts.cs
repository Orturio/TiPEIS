using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class ChartOfAccounts
    {
        public ChartOfAccounts()
        {
            PostingJournalCreditcheckNavigation = new HashSet<PostingJournal>();
            PostingJournalDebetcheckNavigation = new HashSet<PostingJournal>();
        }

        public int Code { get; set; }
        public string Numberofcheck { get; set; }
        public string Nameofcheck { get; set; }
        public string Subconto1 { get; set; }
        public string Subconto2 { get; set; }
        public string Subconto3 { get; set; }

        public virtual ICollection<PostingJournal> PostingJournalCreditcheckNavigation { get; set; }
        public virtual ICollection<PostingJournal> PostingJournalDebetcheckNavigation { get; set; }
    }
}
