using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface IResponsePersonStorage
    {
        List<ResponsePersonViewModel> GetFullList();

        List<ResponsePersonViewModel> GetFilteredList(ResponsePersonBindingModel model);

        ResponsePersonViewModel GetElement(ResponsePersonBindingModel model);

        void Insert(ResponsePersonBindingModel model);

        void Update(ResponsePersonBindingModel model);

        void Delete(ResponsePersonBindingModel model);
    }
}
