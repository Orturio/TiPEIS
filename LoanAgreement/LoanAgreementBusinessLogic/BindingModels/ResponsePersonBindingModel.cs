using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic.BindingModels
{
    public class ResponsePersonBindingModel
    {
        public int? Code { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Middlename { get; set; }
    }
}
