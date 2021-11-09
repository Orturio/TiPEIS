using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface IMaterialStorage
    {
        List<MaterialViewModel> GetFullList();

        List<MaterialViewModel> GetFilteredList(MaterialBindingModel model);

        MaterialViewModel GetElement(MaterialBindingModel model);

        void Insert(MaterialBindingModel model);

        void Update(MaterialBindingModel model);

        void Delete(MaterialBindingModel model);
    }
}
