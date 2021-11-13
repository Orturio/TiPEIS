using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface ITablePartStorage
    {
        List<TablePartViewModel> GetFilteredList(TablePartBindingModel model);
    }
}
