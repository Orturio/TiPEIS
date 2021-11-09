using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface IOperationStorage
    {
        List<OperationViewModel> GetFullList();

        List<OperationViewModel> GetFilteredList(OperationBindingModel model);

        List<OperationViewModel> GetFilteredListByTypeOfOperation(OperationBindingModel model);

        OperationViewModel GetElement(OperationBindingModel model);

        void Insert(OperationBindingModel model);

        void Update(OperationBindingModel model);

        void Delete(OperationBindingModel model);
    }
}
