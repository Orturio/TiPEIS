using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IOperationStorage _operationStorage;
        private readonly IPostingJournalStorage _postingJournal;

        public ReportLogic(IOperationStorage operationStorage, IPostingJournalStorage postingJournal)
        {
            _operationStorage = operationStorage;
            _postingJournal = postingJournal;
        }

        ///
        /// Получение табличной части
        /// 

        public List<ReportReceiveViewModel> GetTablePart(ReportBindingModel model)
        {
            var operations = _operationStorage.GetFilteredListByData(new OperationBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Warehousereceivercode = model.WarehouseCode
            });

            var list = new List<ReportReceiveViewModel>();

            Dictionary<int, (string, int, decimal)> tablePart = new Dictionary<int, (string, int, decimal)>();

            foreach (var record in operations)
            {
                foreach (var item in record.TablePart)
                {
                    if (tablePart.ContainsKey(item.Key))
                    {
                        tablePart[item.Key] = (item.Value.Item1, item.Value.Item2 + tablePart[item.Key].Item2, item.Value.Item3 * (item.Value.Item2 + tablePart[item.Key].Item2)); 
                    }
                    else
                    {
                        tablePart.Add(item.Key, (item.Value.Item1, item.Value.Item2, item.Value.Item3));
                    }
                }        
            }

            foreach (var item in tablePart)
            {
                var rec = new ReportReceiveViewModel
                {
                    Code = item.Key,
                    Name = item.Value.Item1,
                    Count = item.Value.Item2,
                    Sum = item.Value.Item3
                };
                list.Add(rec);
            }
            return list;
        }

        public List<ReportReceiveViewModel> GetTablePartRemains(ReportBindingModel model)
        {
            var warehouseReceive = _operationStorage.GetFilteredListWarehouseReceive(new OperationBindingModel
            {
                DateTo = model.DateTo,
                Warehousereceivercode = model.WarehouseCode
            });

            var warehouseSend = _operationStorage.GetFilteredListWarehouseSender(new OperationBindingModel
            {
                DateTo = model.DateTo,
                Warehousesendercode = model.WarehouseCode
            });

            var list = new List<ReportReceiveViewModel>();

            Dictionary<int, (string, int, decimal)> materials = new Dictionary<int, (string, int, decimal)>();
            foreach (var item in warehouseReceive)
            {
                foreach (var rec in item.TablePart)
                {
                    if (materials.ContainsKey(rec.Key))
                    {
                        materials[rec.Key] = (materials[rec.Key].Item1, materials[rec.Key].Item2 + rec.Value.Item2, rec.Value.Item3 * (materials[rec.Key].Item2 + rec.Value.Item2));
                    }
                    else
                    {
                        materials.Add(rec.Key, (rec.Value.Item1, rec.Value.Item2, rec.Value.Item3));
                    }
                }
            }

            foreach (var item in warehouseSend)
            {    
                foreach (var rec in item.TablePart)
                {
                    if (materials.ContainsKey(rec.Key))
                    {
                        materials[rec.Key] = (materials[rec.Key].Item1, materials[rec.Key].Item2 - rec.Value.Item2, rec.Value.Item3 * (materials[rec.Key].Item2 - rec.Value.Item2));
                    }                   
                }               
            }

            foreach (var item in materials)
            {
                var rec = new ReportReceiveViewModel
                {
                    Code = item.Key,
                    Name = item.Value.Item1,
                    Count = item.Value.Item2,
                    Sum = item.Value.Item3
                };
                list.Add(rec);
            }
            return list;
        }

        public List<ReportReceiveViewModel> GetTablePartRelease(ReportBindingModel model)
        {
            var operations = _operationStorage.GetFilteredListSubdivision(new OperationBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Subdivisioncode = model.WarehouseCode
            });

            var list = new List<ReportReceiveViewModel>();

            Dictionary<int, (string, int, decimal)> tablePart = new Dictionary<int, (string, int, decimal)>();

            foreach (var record in operations)
            {
                foreach (var item in record.TablePart)
                {
                    if (tablePart.ContainsKey(item.Key))
                    {
                        tablePart[item.Key] = (item.Value.Item1, item.Value.Item2 + tablePart[item.Key].Item2, item.Value.Item3 * (item.Value.Item2 + tablePart[item.Key].Item2));
                    }
                    else
                    {
                        tablePart.Add(item.Key, (item.Value.Item1, item.Value.Item2, item.Value.Item3));
                    }
                }
            }

            foreach (var item in tablePart)
            {
                var rec = new ReportReceiveViewModel
                {
                    Code = item.Key,
                    Name = item.Value.Item1,
                    Count = item.Value.Item2,
                    Sum = item.Value.Item3
                };
                list.Add(rec);
            }
            return list;
        }
    }
}
