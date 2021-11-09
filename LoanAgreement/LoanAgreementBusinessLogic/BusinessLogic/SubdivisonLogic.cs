using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class SubdivisonLogic
    {
        private readonly ISubdivisionStorage _subdivisionStorage;

        public SubdivisonLogic(ISubdivisionStorage subdivisionStorage)
        {
            _subdivisionStorage = subdivisionStorage;
        }

        public List<SubdivisionViewModel> Read(SubdivisionBindingModel model)
        {
            if (model == null)
            {
                return _subdivisionStorage.GetFullList();
            }

            if (model.Code.HasValue)
            {
                return new List<SubdivisionViewModel> { _subdivisionStorage.GetElement(model) };
            }

            return _subdivisionStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SubdivisionBindingModel model)
        {
            var element = _subdivisionStorage.GetElement(new SubdivisionBindingModel { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найден такой склад или подразделение");
            }

            if (model.Code.HasValue)
            {
                _subdivisionStorage.Update(model);
            }

            else
            {
                _subdivisionStorage.Insert(model);
            }
        }

        public void Delete(SubdivisionBindingModel model)
        {
            var element = _subdivisionStorage.GetElement(new SubdivisionBindingModel { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _subdivisionStorage.Delete(model);
        }
    }
}
