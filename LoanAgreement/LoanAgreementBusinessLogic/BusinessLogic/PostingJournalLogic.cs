using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class PostingJournalLogic
    {
        private readonly IPostingJournalStorage _postingJournalStorage;

        public PostingJournalLogic(IPostingJournalStorage postingJournalStorage)
        {
            _postingJournalStorage = postingJournalStorage;
        }

        public List<PostingJournalViewModel> Read(PostingJournalBindingModel model)
        {
            if (model == null)
            {
                return _postingJournalStorage.GetFullList();
            }
            if (model.Warehousesendercode != null)
            {
                return _postingJournalStorage.GetFilteredListByWarehouses(model);
            }
            if (model.Tablepartcode != 0 && model.Date != null)
            {
                return _postingJournalStorage.GetFilteredList(model);
            }
            if (model.Operationcode != 0)
            {
                return _postingJournalStorage.GetFilteredListByOperationCode(model);
            }
            if (model.Tablepartcode != 0)
            {
                return new List<PostingJournalViewModel> { _postingJournalStorage.GetElement(model) };
            }

            return _postingJournalStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PostingJournalBindingModel model)
        {
            var element = _postingJournalStorage.GetElement(new PostingJournalBindingModel { Code = model.Code });

            if (element != null && element.Code != model.Code)
            {
                throw new Exception("Не найден такой журнал проводок");
            }

            if (model.Code.HasValue)
            {
                _postingJournalStorage.Update(model);
            }

            else
            {
                _postingJournalStorage.Insert(model);
            }
        }

        public void Delete(PostingJournalBindingModel model)
        {
            var element = _postingJournalStorage.GetElement(new PostingJournalBindingModel { Tablepartcode = model.Tablepartcode });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _postingJournalStorage.Delete(model);
        }
    }
}
