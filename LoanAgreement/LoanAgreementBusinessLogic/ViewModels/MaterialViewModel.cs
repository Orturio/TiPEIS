using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class MaterialViewModel
    {
        public int? Code { get; set; }

        [DisplayName("Название материала")]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
