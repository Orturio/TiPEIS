using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class ProviderViewModel
    {
        public int? Code { get; set; }

        [DisplayName("Название поставщика")]
        public string Name { get; set; }
    }
}
