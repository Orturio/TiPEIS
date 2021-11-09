using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class TablePart
    {
        public TablePart()
        {
            PostingJournal = new HashSet<PostingJournal>();
        }

        public int Code { get; set; }
        public int Count { get; set; }
        public int Operationcode { get; set; }
        public int Materialcode { get; set; }
        public decimal? Price { get; set; }

        public virtual Material MaterialcodeNavigation { get; set; }
        public virtual Operation OperationcodeNavigation { get; set; }
        public virtual ICollection<PostingJournal> PostingJournal { get; set; }
    }
}
