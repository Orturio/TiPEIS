using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic
{
    public interface IChartOfAccountsStorage
    {
        List<ChartOfAccountViewModel> GetFullList();

        List<ChartOfAccountViewModel> GetFilteredList(ChartOfAccountsBindingModel model);

        ChartOfAccountViewModel GetElement(ChartOfAccountsBindingModel model);

        void Insert(ChartOfAccountsBindingModel model);

        void Update(ChartOfAccountsBindingModel model);

        void Delete(ChartOfAccountsBindingModel model);
    }
}
