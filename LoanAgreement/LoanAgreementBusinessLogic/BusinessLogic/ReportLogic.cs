using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using MaterialAccountingBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialAccountingBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IOperationStorage _operationStorage;
        private readonly IPostingJournalStorage _postingJournalStorage;

        public ReportLogic(IOperationStorage operationStorage, IPostingJournalStorage postingJournal)
        {
            _operationStorage = operationStorage;
            _postingJournalStorage = postingJournal;
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
                        tablePart.Add(item.Key, (item.Value.Item1, item.Value.Item2, item.Value.Item3 * item.Value.Item2));
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
                        materials.Add(rec.Key, (rec.Value.Item1, rec.Value.Item2, rec.Value.Item3 * rec.Value.Item2));
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
                        tablePart.Add(item.Key, (item.Value.Item1, item.Value.Item2, item.Value.Item3 * item.Value.Item2));
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

        public List<ReportTurnoverBalanceViewModel> GetTurnoverBalance(ReportBindingModel model)
        {
            var list = new List<ReportTurnoverBalanceViewModel>();            

            if (model.NumberOfCheck != "")
            {
                var postings = _postingJournalStorage.GetFilteredListByNumberOfCheck(new PostingJournalBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    Numbercreditcheck = model.NumberOfCheck
                });

                var record = new ReportTurnoverBalanceViewModel();
                record.NumberOfCheck = model.NumberOfCheck;
                foreach (var item in postings)
                {
                    if (item.Date <= model.DateFrom && item.Numberdebetcheck == model.NumberOfCheck)
                        record.DebetBefore += item.Sum;
                    else if (item.Date <= model.DateFrom && item.Numbercreditcheck == model.NumberOfCheck)
                        record.CreditBefore += item.Sum;
                    else if (item.Numberdebetcheck == model.NumberOfCheck)
                        record.TurnoverDebet += item.Sum;
                    else if (item.Numbercreditcheck == model.NumberOfCheck)
                        record.TurnoverCredit += item.Sum;
                }

                record.DebetEnd = (record.TurnoverDebet + record.DebetBefore);
                record.CreditEnd = (record.TurnoverCredit + record.CreditBefore);

                list.Add(record);
            }

            else
            {
                var postings = _postingJournalStorage.GetFilteredListByDate(new PostingJournalBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                });

                Dictionary<string, (decimal, decimal, decimal, decimal)> rec = new Dictionary<string, (decimal, decimal, decimal, decimal)>();

                foreach (var item in postings)
                {
                    if (model.DateFrom > item.Date)
                    {
                        if (rec.ContainsKey(item.Numbercreditcheck) && rec.ContainsKey(item.Numberdebetcheck))
                        {
                            rec[item.Numberdebetcheck] = (rec[item.Numberdebetcheck].Item1 + item.Sum, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4);                        
                            rec[item.Numbercreditcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2 + item.Sum, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4);
                        }
                        else if (rec.ContainsKey(item.Numbercreditcheck))
                        {
                            rec.Add(item.Numberdebetcheck, (item.Sum, 0, 0, 0));
                            rec[item.Numbercreditcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2 + item.Sum, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4);
                        }
                        else if (rec.ContainsKey(item.Numberdebetcheck))
                        {
                            rec.Add(item.Numbercreditcheck, (0, item.Sum, 0, 0));
                            rec[item.Numberdebetcheck] = (rec[item.Numberdebetcheck].Item1 + item.Sum, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4);
                        }
                        else
                        {
                            rec.Add(item.Numbercreditcheck, (0, item.Sum, 0, 0));
                            rec.Add(item.Numberdebetcheck, (item.Sum, 0, 0, 0));
                        }
                    }
                    else
                    {
                        if (rec.ContainsKey(item.Numbercreditcheck) && rec.ContainsKey(item.Numberdebetcheck))
                        {
                            rec[item.Numberdebetcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3 + item.Sum, rec[item.Numberdebetcheck].Item4);
                            rec[item.Numbercreditcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4 + item.Sum);
                        }
                        else if (rec.ContainsKey(item.Numbercreditcheck))
                        {
                            rec.Add(item.Numberdebetcheck, (0, 0, item.Sum, 0));
                            rec[item.Numbercreditcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3, rec[item.Numberdebetcheck].Item4 + item.Sum);
                        }
                        else if (rec.ContainsKey(item.Numberdebetcheck))
                        {
                            rec.Add(item.Numbercreditcheck, (0, 0, 0, item.Sum));
                            rec[item.Numberdebetcheck] = (rec[item.Numberdebetcheck].Item1, rec[item.Numberdebetcheck].Item2, rec[item.Numberdebetcheck].Item3 + item.Sum, rec[item.Numberdebetcheck].Item4);
                        }
                        else
                        {
                            rec.Add(item.Numbercreditcheck, (0, 0, 0, item.Sum));
                            rec.Add(item.Numberdebetcheck, (0, 0, item.Sum, 0));
                        }
                    }
                }

                foreach (var item in rec)
                {
                    var record = new ReportTurnoverBalanceViewModel();
                    record.NumberOfCheck = item.Key;
                    record.DebetBefore = item.Value.Item1;
                    record.CreditBefore = item.Value.Item2;
                    record.TurnoverDebet = item.Value.Item3;
                    record.TurnoverCredit = item.Value.Item4;
                    record.DebetEnd = item.Value.Item1 + item.Value.Item3;
                    record.CreditEnd = item.Value.Item2 + item.Value.Item4;
                    list.Add(record);
                }
            }
                        
            return list;
        }


        public void SaveOperationsToPdfFileReceive(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список материалов на складе",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Operations = GetTablePart(model)
            });
        }

        public void SaveOperationsToPdfFileRemains(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = DateTime.MinValue,
                DateTo = model.DateTo.Value,
                Operations = GetTablePartRemains(model)
            });
        }

        public void SaveOperationsToPdfFileRelease(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Operations = GetTablePartRelease(model)
            });
        }
    }
}
