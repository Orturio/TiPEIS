using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class TablePartLogic
    {
        private readonly ITablePartStorage _tablePartStorage;

        public TablePartLogic(ITablePartStorage materialStorage)
        {
            _tablePartStorage = materialStorage;
        }

        public List<TablePartViewModel> Read(TablePartBindingModel model)
        {
            if (model == null)
            {
                throw new Exception("Элемент не найден");
            }

            return _tablePartStorage.GetFilteredList(model);
        }
    }
}
