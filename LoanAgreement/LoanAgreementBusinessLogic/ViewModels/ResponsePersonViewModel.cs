using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MaterialAccountingBusinessLogic.ViewModels
{
    public class ResponsePersonViewModel
    {
        public int Code { get; set; }

        [DisplayName("Имя МОЛ")]
        public string Name { get; set; }

        [DisplayName("Фамилия МОЛ")]
        public string Surname { get; set; }

        [DisplayName("Отчество МОЛ")]
        public string Middlename { get; set; }
    }
}
