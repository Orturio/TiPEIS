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
    public class OperationStorage : IOperationStorage
    {
        public List<OperationViewModel> GetFullList()
        {
            using (var context = new postgresContext())
            {
                return context.Operation.Include(rec => rec.TablePart).ThenInclude(rec => rec.MaterialcodeNavigation)
                .ToList().Select(rec => new OperationViewModel
                {
                    Code = rec.Code,
                    Typeofoperation = rec.Typeofoperation,
                    Date = rec.Date,
                    Providercode = rec.Providercode,
                    Warehousesendercode = rec.Warehousesendercode,
                    Warehousereceivercode = rec.Warehousereceivercode,
                    Subdivisioncode = rec.Subdivisioncode,
                    Responsiblesendercode = rec.Responsiblesendercode,
                    Responsiblereceivercode = rec.Responsiblereceivercode,
                    Price = rec.Price,
                    TablePart = rec.TablePart.ToDictionary(recPC => recPC.Materialcode, recPC => (recPC.MaterialcodeNavigation.Name, recPC.Operationcode, recPC.MaterialcodeNavigation.Price))
                }).ToList();
            }
        }

        public List<OperationViewModel> GetFilteredList(OperationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Operation.Include(rec => rec.TablePart).ThenInclude(rec => rec.MaterialcodeNavigation).Where(rec => rec.Code == model.Code).ToList().Select(rec => new OperationViewModel
                {
                    Code = rec.Code,
                    Typeofoperation = rec.Typeofoperation,
                    Date = rec.Date,
                    Providercode = rec.Providercode,
                    Warehousesendercode = rec.Warehousesendercode,
                    Warehousereceivercode = rec.Warehousereceivercode,
                    Subdivisioncode = rec.Subdivisioncode,
                    Responsiblesendercode = rec.Responsiblesendercode,
                    Responsiblereceivercode = rec.Responsiblereceivercode,
                    Price = rec.Price,
                    TablePart = rec.TablePart.ToDictionary(recPC => recPC.Materialcode, recPC => (recPC.MaterialcodeNavigation.Name, recPC.Count, recPC.MaterialcodeNavigation.Price))
                }).ToList();
            }
        }

        public List<OperationViewModel> GetFilteredListByTypeOfOperation(OperationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                return context.Operation.Include(rec => rec.TablePart).ThenInclude(rec => rec.MaterialcodeNavigation).Where(rec => (rec.Typeofoperation == model.Typeofoperation && rec.Warehousereceivercode == model.Warehousereceivercode) || (rec.Typeofoperation == model.Typeofoperation && rec.Warehousesendercode == model.Warehousesendercode && rec.Date <= model.Date)).ToList().Select(rec => new OperationViewModel
                {
                    Code = rec.Code,
                    Typeofoperation = rec.Typeofoperation,
                    Date = rec.Date,
                    Providercode = rec.Providercode,
                    Warehousesendercode = rec.Warehousesendercode,
                    Warehousereceivercode = rec.Warehousereceivercode,
                    Subdivisioncode = rec.Subdivisioncode,
                    Responsiblesendercode = rec.Responsiblesendercode,
                    Responsiblereceivercode = rec.Responsiblereceivercode,
                    Price = rec.Price,
                    TablePart = rec.TablePart.ToDictionary(recPC => recPC.Materialcode, recPC => (recPC.MaterialcodeNavigation.Name, recPC.Count, recPC.MaterialcodeNavigation.Price))
                }).ToList();
            }
        }

        public OperationViewModel GetElement(OperationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new postgresContext())
            {
                var operation = context.Operation.Include(rec => rec.TablePart).ThenInclude(rec => rec.MaterialcodeNavigation).FirstOrDefault(rec => rec.Code == model.Code);
                return operation != null ?
                new OperationViewModel
                {
                    Code = operation.Code,
                    Typeofoperation = operation.Typeofoperation,
                    Date = operation.Date,
                    Providercode = operation.Providercode,
                    Warehousesendercode = operation.Warehousesendercode,
                    Warehousereceivercode = operation.Warehousereceivercode,
                    Subdivisioncode = operation.Subdivisioncode,
                    Responsiblesendercode = operation.Responsiblesendercode,
                    Responsiblereceivercode = operation.Responsiblereceivercode,
                    Price = operation.Price,
                    TablePart = operation.TablePart.ToDictionary(recPC => recPC.Materialcode, recPC => (recPC.MaterialcodeNavigation.Name, recPC.Count, recPC.MaterialcodeNavigation.Price))
                } : null;
            }
        }

        public int Insert(OperationBindingModel model)
        {
            using (var context = new postgresContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Operation operation = CreateModel(model, new Operation());
                        context.Operation.Add(operation);
                        context.SaveChanges();
                        CreateModel(model, operation, context);
                        transaction.Commit();

                        Operation element = context.Operation.FirstOrDefault(rec => operation.Code == rec.Code);
                        return element.Code;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(OperationBindingModel model)
        {
            using (var context = new postgresContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Operation.FirstOrDefault(rec => rec.Code == model.Code);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(OperationBindingModel model)
        {
            using (var context = new postgresContext())
            {
                Operation element = context.Operation.FirstOrDefault(rec => rec.Code == model.Code);
                if (element != null)
                {
                    context.Operation.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Операция не найдена не найден");
                }
            }
        }

        private Operation CreateModel(OperationBindingModel model, Operation operation)
        {
            operation.Typeofoperation = model.Typeofoperation;
            operation.Date = model.Date;
            operation.Providercode = model.Providercode;
            operation.Warehousesendercode = model.Warehousesendercode;
            operation.Warehousereceivercode = model.Warehousereceivercode;
            operation.Subdivisioncode = model.Subdivisioncode;
            operation.Responsiblesendercode = model.Responsiblesendercode;
            operation.Responsiblereceivercode = model.Responsiblereceivercode;
            operation.Price = model.Price;
            return operation;
        }

        private Operation CreateModel(OperationBindingModel model, Operation operation, postgresContext context)
        {
            operation.Typeofoperation = model.Typeofoperation;
            operation.Date = model.Date;
            operation.Providercode = model.Providercode;
            operation.Warehousesendercode = model.Warehousesendercode;
            operation.Warehousereceivercode = model.Warehousereceivercode;
            operation.Subdivisioncode = model.Subdivisioncode;
            operation.Responsiblesendercode = model.Responsiblesendercode;
            operation.Responsiblereceivercode = model.Responsiblereceivercode;
            operation.Price = model.Price;
            if (model.Code.HasValue)
            {
                
                var tablPart = context.TablePart.Where(rec => rec.Operationcode == model.Code.Value).ToList();
                context.TablePart.RemoveRange(tablPart.Where(rec => !model.TablePart.ContainsKey(rec.Materialcode)).ToList());
                var newTablePart = tablPart.Where(rec => model.TablePart.ContainsKey(rec.Materialcode)).ToList();
                // удалили те, которых нет в модели
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateTablePart in newTablePart)
                {
                    updateTablePart.Count = model.TablePart[updateTablePart.Materialcode].Item2;
                    updateTablePart.Price = model.TablePart[updateTablePart.Materialcode].Item3;
                    model.TablePart.Remove(updateTablePart.Materialcode);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.TablePart)
            {
                context.TablePart.Add(new TablePart
                {
                    Operationcode = operation.Code,
                    Materialcode = pc.Key,
                    Count = pc.Value.Item2,
                    Price = pc.Value.Item3
                });
                context.SaveChanges();
            }
            return operation;
        }
    }
}
