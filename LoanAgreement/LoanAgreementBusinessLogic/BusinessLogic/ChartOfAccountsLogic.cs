using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialAccountingBusinessLogic
{
    public class ChartOfAccountsLogic
    {
        private readonly IChartOfAccountsStorage _chartOfAccountsStorage;

        public ChartOfAccountsLogic(IChartOfAccountsStorage chartOfAccountsStorage)
        {
            _chartOfAccountsStorage = chartOfAccountsStorage;
        }

        public List<ChartOfAccountViewModel> Read(ChartOfAccountsBindingModel model)
        {
            if (model == null)
            {
                return _chartOfAccountsStorage.GetFullList();
            }
            if (model.NumberOfCheck != null)
            {
                return new List<ChartOfAccountViewModel> { _chartOfAccountsStorage.GetElement(model) };
            }
            return _chartOfAccountsStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ChartOfAccountsBindingModel model)
        {
            var element = _chartOfAccountsStorage.GetElement(new ChartOfAccountsBindingModel { Code = model.Code });

            if (element == null)
            {
                throw new Exception("Не найдено возрастное ограничение");
            }

            if (model.Code.HasValue)
            {
                _chartOfAccountsStorage.Update(model);
            }

            else
            {
                _chartOfAccountsStorage.Insert(model);
            }
        }

        public void Delete(ChartOfAccountsBindingModel model)
        {
            var element = _chartOfAccountsStorage.GetElement(new ChartOfAccountsBindingModel
            {
                Code = model.Code
            });
            if (element == null)
            {
                throw new Exception("возрастное ограничение не найдено");
            }
            _chartOfAccountsStorage.Delete(model);
        }
    }
}
