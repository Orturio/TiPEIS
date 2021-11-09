using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class MaterialBindingModel
    {
        public int? Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
