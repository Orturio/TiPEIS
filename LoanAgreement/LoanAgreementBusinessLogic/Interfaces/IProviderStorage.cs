using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface IProviderStorage
    {
        List<ProviderViewModel> GetFullList();

        List<ProviderViewModel> GetFilteredList(ProviderBindingModels model);

        ProviderViewModel GetElement(ProviderBindingModels model);

        void Insert(ProviderBindingModels model);

        void Update(ProviderBindingModels model);

        void Delete(ProviderBindingModels model);
    }
}
