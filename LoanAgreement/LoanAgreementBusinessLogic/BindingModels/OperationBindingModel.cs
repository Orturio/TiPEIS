using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class OperationBindingModel
    {
        public int? Code { get; set; }

        public string Typeofoperation { get; set; }

        public DateTime Date { get; set; }

        public int? Providercode { get; set; }

        public int? Warehousesendercode { get; set; }

        public int? Warehousereceivercode { get; set; }

        public int? Subdivisioncode { get; set; }

        public int? Responsiblesendercode { get; set; }

        public int? Responsiblereceivercode { get; set; }

        public decimal? Price { get; set; }

        public Dictionary<int, (string, int, decimal)> TablePart { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
