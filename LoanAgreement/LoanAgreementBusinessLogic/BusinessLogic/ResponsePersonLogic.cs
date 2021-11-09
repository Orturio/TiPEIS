using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class ResponsePersonLogic
    {
        private readonly IResponsePersonStorage _responsePersonStorage;

        public ResponsePersonLogic(IResponsePersonStorage responsePersonStorage)
        {
            _responsePersonStorage = responsePersonStorage;
        }

        public List<ResponsePersonViewModel> Read(ResponsePersonBindingModel model)
        {
            if (model == null)
            {
                return _responsePersonStorage.GetFullList();
            }

            if (model.Code.HasValue)
            {
                return new List<ResponsePersonViewModel> { _responsePersonStorage.GetElement(model) };
            }

            return _responsePersonStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ResponsePersonBindingModel model)
        {
            var element = _responsePersonStorage.GetElement(new ResponsePersonBindingModel { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найден такой МОЛ");
            }

            if (model.Code.HasValue)
            {
                _responsePersonStorage.Update(model);
            }

            else
            {
                _responsePersonStorage.Insert(model);
            }
        }

        public void Delete(ResponsePersonBindingModel model)
        {
            var element = _responsePersonStorage.GetElement(new ResponsePersonBindingModel { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _responsePersonStorage.Delete(model);
        }
    }
}
