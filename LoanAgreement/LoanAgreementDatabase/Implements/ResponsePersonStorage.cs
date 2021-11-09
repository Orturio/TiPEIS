using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialAccountingDatabase.Implements
{
    public class ResponsePersonStorage : IResponsePersonStorage
    {
        public List<ResponsePersonViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.ResponsiblePerson.Select(CreateModel)
                .ToList();
            }
        }

        public List<ResponsePersonViewModel> GetFilteredList(ResponsePersonBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.ResponsiblePerson
                .Where(rec => rec.Code == model.Code)
                .Select(CreateModel)
                .ToList();
            }
        }

        public ResponsePersonViewModel GetElement(ResponsePersonBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var responsePerson = context.ResponsiblePerson.FirstOrDefault(rec => rec.Code == model.Code);
                return responsePerson != null ?
                new ResponsePersonViewModel
                {
                    Code = responsePerson.Code,
                    Name = responsePerson.Name,
                    Surname = responsePerson.Surname,
                    Middlename = responsePerson.Middlename
                } : null;
            }
        }

        public void Insert(ResponsePersonBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.ResponsiblePerson.Add(CreateModel(model, new ResponsiblePerson()));
                context.SaveChanges();
            }
        }

        public void Update(ResponsePersonBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.ResponsiblePerson.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("МОЛ не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ResponsePersonBindingModel model)
        {
            using (var context = new postgresContext())
            {
                ResponsiblePerson element = context.ResponsiblePerson.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.ResponsiblePerson.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("MOЛ не найден");
                }
            }
        }

        private ResponsiblePerson CreateModel(ResponsePersonBindingModel model, ResponsiblePerson responsePerson)
        {
            responsePerson.Name = model.Name;
            responsePerson.Surname = model.Surname;
            responsePerson.Middlename = model.Middlename;
            return responsePerson;
        }

        private ResponsePersonViewModel CreateModel(ResponsiblePerson responsiblePerson)
        {
            ResponsePersonViewModel model = new ResponsePersonViewModel();
            model.Code = responsiblePerson.Code;
            model.Name = responsiblePerson.Name;
            model.Surname = responsiblePerson.Surname;
            model.Middlename = responsiblePerson.Middlename;
            return model;
        }
    }
}
