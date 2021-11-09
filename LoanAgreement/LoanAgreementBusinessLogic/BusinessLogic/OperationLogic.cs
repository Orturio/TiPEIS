using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class OperationLogic
    {
        private readonly IOperationStorage _operationStorage;

        public OperationLogic(IOperationStorage operationStorage)
        {
            _operationStorage = operationStorage;
        }

        public List<OperationViewModel> Read(OperationBindingModel model)
        {
            if (model == null)
            {
                return _operationStorage.GetFullList();
            }

            if (model.Code.HasValue)
            {
                return new List<OperationViewModel> { _operationStorage.GetElement(model) };
            }

            if (model.Typeofoperation != null)
            {
                return _operationStorage.GetFilteredListByTypeOfOperation(model);
            }
            return _operationStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(OperationBindingModel model)
        {
            var element = _operationStorage.GetElement(new OperationBindingModel { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найдена такая операция");
            }

            if (model.Code.HasValue)
            {
                _operationStorage.Update(model);
            }

            else
            {
                _operationStorage.Insert(model);
            }
        }

        public void Delete(OperationBindingModel model)
        {
            var element = _operationStorage.GetElement(new OperationBindingModel { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _operationStorage.Delete(model);
        }
    }
}
