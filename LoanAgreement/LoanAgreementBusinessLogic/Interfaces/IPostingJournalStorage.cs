using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface IPostingJournalStorage
    {
        List<PostingJournalViewModel> GetFullList();

        List<PostingJournalViewModel> GetFilteredList(PostingJournalBindingModel model);

        List<PostingJournalViewModel> GetFilteredListByWarehouses(PostingJournalBindingModel model);

        List<PostingJournalViewModel> GetFilteredListByOperationCode(PostingJournalBindingModel model);

        List<PostingJournalViewModel> GetFilteredListByNumberOfCheck(PostingJournalBindingModel model);

        List<PostingJournalViewModel> GetFilteredListByDate(PostingJournalBindingModel model);       

        PostingJournalViewModel GetElement(PostingJournalBindingModel model);

        void Insert(PostingJournalBindingModel model);

        void Update(PostingJournalBindingModel model);

        void Delete(PostingJournalBindingModel model);
    }
}
