using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class ProviderLogic
    {
        private readonly IProviderStorage _providerStorage;

        public ProviderLogic(IProviderStorage providerStorage)
        {
            _providerStorage = providerStorage;
        }

        public List<ProviderViewModel> Read(ProviderBindingModels model)
        {
            if (model == null)
            {
                return _providerStorage.GetFullList();
            }

            if (model.Code.HasValue)
            {
                return new List<ProviderViewModel> { _providerStorage.GetElement(model) };
            }

            return _providerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ProviderBindingModels model)
        {
            var element = _providerStorage.GetElement(new ProviderBindingModels { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найден такой поставщик");
            }

            if (model.Code.HasValue)
            {
                _providerStorage.Update(model);
            }

            else
            {
                _providerStorage.Insert(model);
            }
        }

        public void Delete(ProviderBindingModels model)
        {
            var element = _providerStorage.GetElement(new ProviderBindingModels { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _providerStorage.Delete(model);
        }
    }
}
