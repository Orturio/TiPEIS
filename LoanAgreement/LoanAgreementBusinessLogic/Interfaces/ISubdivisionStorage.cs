using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingBusinessLogic.Interfaces
{
    public interface ISubdivisionStorage
    {
        List<SubdivisionViewModel> GetFullList();

        List<SubdivisionViewModel> GetFilteredList(SubdivisionBindingModel model);

        SubdivisionViewModel GetElement(SubdivisionBindingModel model);

        void Insert(SubdivisionBindingModel model);

        void Update(SubdivisionBindingModel model);

        void Delete(SubdivisionBindingModel model);
    }
}
