using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MaterialAccountingBusinessLogic;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;

namespace MaterialAccountingDatabase.Implements
{
    public class PostingJournalStorage : IPostingJournalStorage
    {
        public List<PostingJournalViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.PostingJournal.Select(CreateModel)
                .ToList();
            }
        }

        public List<PostingJournalViewModel> GetFilteredList(PostingJournalBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.PostingJournal.Include(rec => rec.TablepartcodeNavigation).ThenInclude(rec => rec.OperationcodeNavigation)
                .Where(rec => rec.Warehousereceivercode == model.Warehousereceivercode && rec.Date <= model.Date)
                .Select(CreateModel).ToList();
            }
        }

        public List<PostingJournalViewModel> GetFilteredListByWarehouses(PostingJournalBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.PostingJournal.Include(rec => rec.TablepartcodeNavigation).ThenInclude(rec => rec.OperationcodeNavigation)
                .Where(rec => rec.Warehousesendercode == model.Warehousesendercode && rec.Date <= model.Date)
                .Select(CreateModel).ToList();
            }
        }

        public List<PostingJournalViewModel> GetFilteredListByOperationCode(PostingJournalBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.PostingJournal.Include(rec => rec.TablepartcodeNavigation).ThenInclude(rec => rec.OperationcodeNavigation)
                .Where(rec => rec.TablepartcodeNavigation.OperationcodeNavigation.Code == model.Operationcode)
                .Select(CreateModel).ToList();
            }
        }

        public PostingJournalViewModel GetElement(PostingJournalBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var agelimit = context.PostingJournal.FirstOrDefault(rec => rec.Tablepartcode == model.Tablepartcode);
                return agelimit != null ? CreateModel(agelimit) : null;
            }
        }

        public void Insert(PostingJournalBindingModel model)
        {
            using (var context = new postgresContext())
            {
                context.PostingJournal.Add(CreateModel(model, new PostingJournal()));                
                context.SaveChanges();
            }
        }

        public void Update(PostingJournalBindingModel model)
        {
            using (var context = new postgresContext())
            {
                var element = context.PostingJournal.FirstOrDefault(rec => rec.Code == model.Code);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PostingJournalBindingModel model)
        {
            using (var context = new postgresContext())
            {
                PostingJournal element = context.PostingJournal.FirstOrDefault(rec => rec.Tablepartcode == model.Tablepartcode);
                if (element != null)
                {
                    context.PostingJournal.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private PostingJournal CreateModel(PostingJournalBindingModel model, PostingJournal postingJournal)
        {
            postingJournal.Debetcheck = model.Debetcheck;
            postingJournal.Date = model?.Date;
            postingJournal.Numberdebetcheck = model.Numberdebetcheck;
            postingJournal.Subcontodebet1 = model.Subcontodebet1;
            postingJournal.Subcontodebet2 = model.Subcontodebet2;
            postingJournal.Subcontodebet3 = model.Subcontodebet3;
            postingJournal.Creditcheck = model.Creditcheck;
            postingJournal.Numbercreditcheck = model.Numbercreditcheck;
            postingJournal.Subcontocredit1 = model.Subcontocredit1;
            postingJournal.Subcontocredit2 = model.Subcontocredit2;
            postingJournal.Subcontocredit3 = model.Subcontocredit3;
            postingJournal.Nameofmaterial = model.Nameofmaterial;
            postingJournal.Count = model.Count;
            postingJournal.Sum = model.Sum;
            postingJournal.Tablepartcode = model.Tablepartcode;
            postingJournal.Operationcode = model.Operationcode;
            postingJournal.Comment = model.Comment;
            postingJournal.Warehousereceivercode = model.Warehousereceivercode;
            postingJournal.Warehousesendercode = model.Warehousesendercode;
            return postingJournal;
        }

        private PostingJournalViewModel CreateModel(PostingJournal postingJournal)
        {
            PostingJournalViewModel model = new PostingJournalViewModel();
            model.Code = postingJournal.Code;
            model.Date = postingJournal.Date;
            model.Debetcheck = postingJournal.Debetcheck;
            model.Numberdebetcheck = postingJournal.Numberdebetcheck;
            model.Subcontodebet1 = postingJournal.Subcontodebet1;
            model.Subcontodebet2 = postingJournal.Subcontodebet2;
            model.Subcontodebet3 = postingJournal.Subcontodebet3;
            model.Creditcheck = postingJournal.Creditcheck;
            model.Numbercreditcheck = postingJournal.Numbercreditcheck;
            model.Subcontocredit1 = postingJournal.Subcontocredit1;
            model.Subcontocredit2 = postingJournal.Subcontocredit2;
            model.Subcontocredit3 = postingJournal.Subcontocredit3;
            model.Nameofmaterial = postingJournal.Nameofmaterial;
            model.Count = postingJournal.Count;
            model.Sum = postingJournal.Sum;
            model.Tablepartcode = postingJournal.Tablepartcode;
            model.Operationcode = postingJournal.Operationcode;
            model.Comment = postingJournal.Comment;
            model.Warehousereceivercode = postingJournal.Warehousereceivercode;
            model.Warehousesendercode = postingJournal.Warehousesendercode;
            return model;
        }
    }
}
