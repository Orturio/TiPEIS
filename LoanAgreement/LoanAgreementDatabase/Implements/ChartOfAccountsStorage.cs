using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MaterialAccountingBusinessLogic;

namespace MaterialAccountingDatabase
{
    public class ChartOfAccountStorage : IChartOfAccountsStorage
    {
        public List<ChartOfAccountViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.ChartOfAccounts.Select(CreateModel)
                .ToList();
            }
        }

        public List<ChartOfAccountViewModel> GetFilteredList(ChartOfAccountsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.ChartOfAccounts
                .Where(rec => rec.Code == model.Code)
                .Select(CreateModel)
                .ToList();
            }
        }

        public ChartOfAccountViewModel GetElement(ChartOfAccountsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var chartOfAccounts = context.ChartOfAccounts.FirstOrDefault(rec => rec.Numberofcheck == model.NumberOfCheck);
                return chartOfAccounts != null ?
                new ChartOfAccountViewModel
                {
                    Code = chartOfAccounts.Code,
                    NumberOfCheck = chartOfAccounts.Numberofcheck,
                    NameOfCheck = chartOfAccounts.Nameofcheck,
                    subconto1 = chartOfAccounts.Subconto1,
                    subconto2 = chartOfAccounts.Subconto2,
                    subconto3 = chartOfAccounts.Subconto3,
            } : null;
            }
        }

        public void Insert(ChartOfAccountsBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.ChartOfAccounts.Add(CreateModel(model, new ChartOfAccounts()));
                context.SaveChanges();
            }
        }

        public void Update(ChartOfAccountsBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.ChartOfAccounts.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("возрастное ограничение не найдено");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ChartOfAccountsBindingModel model)
        {
            using (var context = new postgresContext())
            {
                ChartOfAccounts element = context.ChartOfAccounts.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.ChartOfAccounts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
        }

        private ChartOfAccounts CreateModel(ChartOfAccountsBindingModel model, ChartOfAccounts chartOfAccounts)
        {
            chartOfAccounts.Numberofcheck = model.NumberOfCheck;
            chartOfAccounts.Nameofcheck = model.NameOfCheck;
            chartOfAccounts.Subconto1 = model.subconto1;
            chartOfAccounts.Subconto2 = model.subconto2;
            chartOfAccounts.Subconto3 = model.subconto3;
            return chartOfAccounts;
        }

        private ChartOfAccountViewModel CreateModel(ChartOfAccounts user)
        {
            ChartOfAccountViewModel model = new ChartOfAccountViewModel();
            model.Code = user.Code;
            model.NumberOfCheck = user.Numberofcheck;
            model.NameOfCheck = user.Nameofcheck;
            model.subconto1 = user.Subconto1;
            model.subconto2 = user.Subconto2;
            model.subconto3 = user.Subconto3;
            return model;
        }
    }
}
