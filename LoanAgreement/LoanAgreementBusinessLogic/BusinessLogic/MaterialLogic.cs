using System;
using System.Collections.Generic;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Text;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class MaterialLogic
    {
        private readonly IMaterialStorage _materialStorage;

        public MaterialLogic(IMaterialStorage materialStorage)
        {
            _materialStorage = materialStorage;
        }

        public List<MaterialViewModel> Read(MaterialBindingModel model)
        {
            if (model == null)
            {
                return _materialStorage.GetFullList();
            }

            if (model.Code.HasValue)
            {
                return new List<MaterialViewModel> { _materialStorage.GetElement(model) };
            }

            return _materialStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MaterialBindingModel model)
        {
            var element = _materialStorage.GetElement(new MaterialBindingModel { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найден такой материал");
            }

            if (model.Code.HasValue)
            {
                _materialStorage.Update(model);
            }

            else
            {
                _materialStorage.Insert(model);
            }
        }

        public void Delete(MaterialBindingModel model)
        {
            var element = _materialStorage.GetElement(new MaterialBindingModel { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _materialStorage.Delete(model);
        }
    }
}
