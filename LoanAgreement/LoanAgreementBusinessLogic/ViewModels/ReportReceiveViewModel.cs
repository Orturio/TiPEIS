using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class ReportReceiveViewModel
    {
        public int Code { set; get; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
