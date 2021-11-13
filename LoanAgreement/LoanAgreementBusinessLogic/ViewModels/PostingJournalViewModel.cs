using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class PostingJournalViewModel
    {
        [DisplayName("Код проводки")]
        public int? Code { get; set; }

        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        public int Debetcheck { get; set; }

        [DisplayName("Номер счета по дебету")]
        public string Numberdebetcheck { get; set; }

        [DisplayName("Субконто дебет 1")]
        public string Subcontodebet1 { get; set; }

        [DisplayName("Субконто дебет 2")]
        public string Subcontodebet2 { get; set; }

        [DisplayName("Субконто дебет 3")]
        public string Subcontodebet3 { get; set; }

        public int Creditcheck { get; set; }

        [DisplayName("Номер счета по кредиту")]
        public string Numbercreditcheck { get; set; }

        [DisplayName("Субконто кредит 1")]
        public string Subcontocredit1 { get; set; }

        [DisplayName("Субконто кредит 2")]
        public string Subcontocredit2 { get; set; }

        [DisplayName("Субконто кредит 3")]
        public string Subcontocredit3 { get; set; }

        public string Nameofmaterial { get; set; }

        [DisplayName("Количество материала")]
        public int Count { get; set; }

        [DisplayName("Сумма материалов")]
        public decimal Sum { get; set; }
  
        public int Tablepartcode { get; set; }

        [DisplayName("Код операции")]
        public int Operationcode { get; set; }

        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        public int? Warehousereceivercode { get; set; }

        public int? Warehousesendercode { get; set; }
    }
}
