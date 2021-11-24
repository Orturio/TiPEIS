using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class ReportTurnoverBalanceViewModel
    {
        public string NumberOfCheck { get; set; }

        public decimal DebetBefore { get; set; }

        public decimal CreditBefore { get; set; }

        public decimal TurnoverDebet { get; set; }

        public decimal TurnoverCredit { get; set; }

        public decimal DebetEnd { get; set; }

        public decimal CreditEnd { get; set; }
    }
}
