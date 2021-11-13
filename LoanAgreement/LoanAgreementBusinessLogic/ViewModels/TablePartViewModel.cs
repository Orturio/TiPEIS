using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class TablePartViewModel
    {
        public int Code { get; set; }

        public int Count { get; set; }

        public int Operationcode { get; set; }

        public int Materialcode { get; set; }

        public decimal? Price { get; set; }
    }
}
