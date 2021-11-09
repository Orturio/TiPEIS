using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MaterialAccountingBusinessLogic
{
    public class ChartOfAccountViewModel
    {
        public int? Code { get; set; }

        [DisplayName("Номер счета")]
        public string NumberOfCheck { get; set; }

        [DisplayName("Название счета")]
        public string NameOfCheck { get; set; }

        [DisplayName("Субконто 1")]
        public string subconto1 { get; set; }

        [DisplayName("Субконто 2")]
        public string subconto2 { get; set; }

        [DisplayName("Субконто 3")]
        public string subconto3 { get; set; }
    }
}
