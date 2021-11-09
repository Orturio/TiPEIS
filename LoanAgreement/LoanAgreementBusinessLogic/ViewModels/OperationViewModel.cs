using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class OperationViewModel
    {
        [DisplayName("Код операции")]
        public int? Code { get; set; }

        [DisplayName("Тип операции")]
        public string Typeofoperation { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        public int? Providercode { get; set; }

        public int? Warehousesendercode { get; set; }

        public int? Warehousereceivercode { get; set; }

        public int? Subdivisioncode { get; set; }

        public int? Responsiblesendercode { get; set; }

        public int? Responsiblereceivercode { get; set; }

        [DisplayName("Сумма операции")]
        public decimal? Price { get; set; }

        public Dictionary<int, (string, int, decimal)> TablePart { get; set; }
    }
}
