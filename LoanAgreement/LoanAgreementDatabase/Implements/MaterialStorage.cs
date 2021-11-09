using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Linq;

namespace MaterialAccountingDatabase.Implements
{
    public class MaterialStorage : IMaterialStorage
    {
        public List<MaterialViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Material.Select(CreateModel)
                .ToList();
            }
        }

        public List<MaterialViewModel> GetFilteredList(MaterialBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Material
                .Where(rec => rec.Code == model.Code)
                .Select(CreateModel)
                .ToList();
            }
        }

        public MaterialViewModel GetElement(MaterialBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var material = context.Material.FirstOrDefault(rec => rec.Code == model.Code);
                return material != null ?
                new MaterialViewModel
                {
                    Code = material.Code,
                    Name = material.Name,
                    Price = material.Price
                } : null;
            }
        }

        public void Insert(MaterialBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.Material.Add(CreateModel(model, new Material()));
                context.SaveChanges();
            }
        }

        public void Update(MaterialBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.Material.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("материал не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(MaterialBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Material element = context.Material.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.Material.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Материал не найден");
                }
            }
        }

        private Material CreateModel(MaterialBindingModel model, Material material)
        {
            material.Name = model.Name;
            material.Price = model.Price;
            return material;
        }

        private MaterialViewModel CreateModel(Material material)
        {
            MaterialViewModel model = new MaterialViewModel();
            model.Code = material.Code;
            model.Name = material.Name;
            model.Price = material.Price;
            return model;
        }
    }
}
