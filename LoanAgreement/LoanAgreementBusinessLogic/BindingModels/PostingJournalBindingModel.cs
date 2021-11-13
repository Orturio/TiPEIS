using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class PostingJournalBindingModel
    {
        public int? Code { get; set; }

        public DateTime? Date { get; set; }
      
        public int Debetcheck { get; set; }

        public string Numberdebetcheck { get; set; }

        public string Subcontodebet1 { get; set; }
      
        public string Subcontodebet2 { get; set; }
      
        public string Subcontodebet3 { get; set; }
      
        public int Creditcheck { get; set; }

        public string Numbercreditcheck { get; set; }

        public string Subcontocredit1 { get; set; }
       
        public string Subcontocredit2 { get; set; }
       
        public string Subcontocredit3 { get; set; }

        public string Nameofmaterial { get; set; }

        public int Count { get; set; }
       
        public decimal Sum { get; set; }
        
        public int Tablepartcode { get; set; }

        public int Operationcode { get; set; }

        public string Comment { get; set; }

        public int? Warehousereceivercode { get; set; }

        public int? Warehousesendercode { get; set; }
    }
}
