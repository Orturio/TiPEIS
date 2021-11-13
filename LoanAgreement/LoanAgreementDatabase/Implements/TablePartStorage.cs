using System;
using System.Collections.Generic;
using System.Text;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MaterialAccountingDatabase.Implements
{
    public class TablePartStorage : ITablePartStorage
    {
        public List<TablePartViewModel> GetFilteredList(TablePartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.TablePart
                .Where(rec => rec.Operationcode == model.Operationcode)
                .Select(CreateModel)
                .ToList();
            }
        }
          
        private TablePartViewModel CreateModel(TablePart tablePart)
            {
                TablePartViewModel model = new TablePartViewModel();
                model.Code = tablePart.Code;
                model.Materialcode = tablePart.Materialcode;
                model.Operationcode = tablePart.Operationcode;
                model.Count = tablePart.Count;
                model.Price = tablePart.Price;               
                return model;
            }
    }
}
