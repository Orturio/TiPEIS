using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class SubdivisionViewModel
    {
        public int? Code { get; set; }

        [DisplayName("Название склада")]
        public string Name { get; set; }

        [DisplayName("тип")]
        public bool Warehouse { get; set; }
    }
}
