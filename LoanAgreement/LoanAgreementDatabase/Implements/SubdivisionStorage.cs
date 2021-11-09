using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Linq;

namespace MaterialAccountingDatabase.Implements
{
    public class SubdivisionStorage : ISubdivisionStorage
    {
        public List<SubdivisionViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Subdivision.Select(CreateModel)
                .ToList();
            }
        }

        public List<SubdivisionViewModel> GetFilteredList(SubdivisionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Subdivision
                .Where(rec => rec.Warehouse == model.Warehouse)
                .Select(CreateModel)
                .ToList();
            }
        }

        public SubdivisionViewModel GetElement(SubdivisionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var subdivision = context.Subdivision.FirstOrDefault(rec => rec.Code == model.Code);
                return subdivision != null ?
                new SubdivisionViewModel
                {
                    Code = subdivision.Code,
                    Name = subdivision.Name,
                    Warehouse = subdivision.Warehouse
                } : null;
            }
        }

        public void Insert(SubdivisionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Subdivision.Add(CreateModel(model, new Subdivision()));
                context.SaveChanges();
            }
        }

        public void Update(SubdivisionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Subdivision.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("Склад не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(SubdivisionBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Subdivision element = context.Subdivision.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.Subdivision.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Склад не найден");
                }
            }
        }

        private Subdivision CreateModel(SubdivisionBindingModel model, Subdivision subdivision)
        {
            subdivision.Name = model.Name;
            subdivision.Warehouse = model.Warehouse;
            return subdivision;
        }

        private SubdivisionViewModel CreateModel(Subdivision subdivision)
        {
            SubdivisionViewModel model = new SubdivisionViewModel();
            model.Code = subdivision.Code;
            model.Name = subdivision.Name;
            model.Warehouse = subdivision.Warehouse;
            return model;
        }
    }
}
