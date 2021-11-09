using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class SubdivisionBindingModel
    {
        public int? Code { get; set; }

        public string Name { get; set; }

        public bool Warehouse { get; set; }
    }
}
