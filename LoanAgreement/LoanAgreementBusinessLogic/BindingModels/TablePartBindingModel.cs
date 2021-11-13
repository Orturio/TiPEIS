using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class TablePartBindingModel
    {
        public int Code { get; set; }
        public int Count { get; set; }
        public int Operationcode { get; set; }
        public int Materialcode { get; set; }
        public decimal? Price { get; set; }
    }
}
