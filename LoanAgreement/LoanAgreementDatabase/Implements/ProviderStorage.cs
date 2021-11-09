using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Linq;

namespace MaterialAccountingDatabase.Implements
{
    public class ProviderStorage : IProviderStorage
    {
        public List<ProviderViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Provider.Select(CreateModel)
                .ToList();
            }
        }

        public List<ProviderViewModel> GetFilteredList(ProviderBindingModels model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Provider
                .Where(rec => rec.Code == model.Code)
                .Select(CreateModel)
                .ToList();
            }
        }

        public ProviderViewModel GetElement(ProviderBindingModels model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var provider = context.Provider.FirstOrDefault(rec => rec.Code == model.Code);
                return provider != null ?
                new ProviderViewModel
                {
                    Code = provider.Code,
                    Name = provider.Name
                } : null;
            }
        }

        public void Insert(ProviderBindingModels model)
        {
            using (var context = new postgresContext())
            {
                context.Provider.Add(CreateModel(model, new Provider()));
                context.SaveChanges();
            }
        }

        public void Update(ProviderBindingModels model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Provider.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("Поставщик не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ProviderBindingModels model)
        {
            using (var context = new postgresContext())
            {
                Provider element = context.Provider.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.Provider.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Поставщик не найден");
                }
            }
        }

        private Provider CreateModel(ProviderBindingModels model, Provider provider)
        {
            provider.Name = model.Name;
            return provider;
        }

        private ProviderViewModel CreateModel(Provider provider)
        {
            ProviderViewModel model = new ProviderViewModel();
            model.Code = provider.Code;
            model.Name = provider.Name;
            return model;
        }
    }
}
