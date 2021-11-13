using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MaterialAccountingBusinessLogic;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using Unity;

namespace LoanAgreement
{
    public partial class FormOperation : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Code { set { code = value; } }

        private readonly OperationLogic logic;
        private readonly MaterialLogic logicMaterial;
        private readonly PostingJournalLogic logicJournal;
        private readonly ResponsePersonLogic logicR;
        private readonly SubdivisonLogic logicS;
        private readonly ChartOfAccountsLogic logicChart;
        private readonly TablePartLogic logicTablePart;
        private readonly ProviderLogic logicProvider;

        private decimal? sumMaterials = 0; 

        private int? code;

        OperationViewModel view;

        private Dictionary<int, (string, int, decimal)> tablePart;

        public FormOperation(OperationLogic logic, ResponsePersonLogic logicR, SubdivisonLogic logicS, PostingJournalLogic logicJournal, ChartOfAccountsLogic logicChart, TablePartLogic logicTablePart, MaterialLogic logicMaterial, ProviderLogic logicProvider)
        {
            InitializeComponent();
            this.logic = logic;
            this.logicR = logicR;
            this.logicS = logicS;
            this.logicJournal = logicJournal;
            this.logicChart = logicChart;
            this.logicTablePart = logicTablePart;
            this.logicMaterial = logicMaterial;
            this.logicProvider = logicProvider;

            List<ResponsePersonViewModel> list = logicR.Read(null);
            if (list != null)
            {
                comboBoxMOL.DisplayMember = "Name";
                comboBoxMOL.ValueMember = "Code";
                comboBoxMOL.DataSource = list;
                comboBoxMOL.SelectedItem = null;
            }
            
            List<ResponsePersonViewModel> listMolReceiver = logicR.Read(null);
            if(listMolReceiver != null)
            {
                comboBoxMOLReceiver.DisplayMember = "Name";
                comboBoxMOLReceiver.ValueMember = "Code";
                comboBoxMOLReceiver.DataSource = listMolReceiver;
                comboBoxMOLReceiver.SelectedItem = null;
            }


            List<ResponsePersonViewModel> listMolSender = logicR.Read(null);
            if (listMolSender != null)
            {
                comboBoxMOLSender.DisplayMember = "Name";
                comboBoxMOLSender.ValueMember = "Code";
                comboBoxMOLSender.DataSource = listMolSender;
                comboBoxMOLSender.SelectedItem = null;
            }

            List<SubdivisionViewModel> listWarehouses = logicS.Read(new SubdivisionBindingModel {Warehouse = true});
            if (listWarehouses != null)
            {
                comboBoxWarehouse.DisplayMember = "Name";
                comboBoxWarehouse.ValueMember = "Code";
                comboBoxWarehouse.DataSource = listWarehouses;
                comboBoxWarehouse.SelectedItem = null;
            }

            List<SubdivisionViewModel> listWarehouseReceiver = logicS.Read(new SubdivisionBindingModel { Warehouse = true });
            if (listWarehouseReceiver != null)
            {
                comboBoxWarehouseReceiver.DisplayMember = "Name";
                comboBoxWarehouseReceiver.ValueMember = "Code";
                comboBoxWarehouseReceiver.DataSource = listWarehouseReceiver;
                comboBoxWarehouseReceiver.SelectedItem = null;
            }

            List<SubdivisionViewModel> listWarehouseSender = logicS.Read(new SubdivisionBindingModel { Warehouse = true });
            if (listWarehouseSender != null)
            {
                comboBoxWarehouseSender.DisplayMember = "Name";
                comboBoxWarehouseSender.ValueMember = "Code";
                comboBoxWarehouseSender.DataSource = listWarehouseSender;
                comboBoxWarehouseSender.SelectedItem = null;
            }

            List<SubdivisionViewModel> listSubdivisions = logicS.Read(new SubdivisionBindingModel { Warehouse = false });
            if (listSubdivisions != null)
            {
                comboBoxSubdivision.DisplayMember = "Name";
                comboBoxSubdivision.ValueMember = "Code";
                comboBoxSubdivision.DataSource = listSubdivisions;
                comboBoxSubdivision.SelectedItem = null;
            }

            List<ProviderViewModel> listProviders = logicProvider.Read(null);
            if (listProviders != null)
            {
                comboBoxProvider.DisplayMember = "Name";
                comboBoxProvider.ValueMember = "Code";
                comboBoxProvider.DataSource = listProviders;
                comboBoxProvider.SelectedItem = null;
            }
        }


        private void FormOperation_Load(object sender, EventArgs e)
        {
            List<PostingJournalViewModel> viewCool = logicJournal.Read(new PostingJournalBindingModel { Tablepartcode = 121, Date = DateTime.Now });

            if (code.HasValue)
            {
                try
                {
                    view = logic.Read(new OperationBindingModel { Code = code.Value })?[0];

                    if (view != null)
                    {
                        if (view.Typeofoperation == "Поступление материала на склад")
                        {
                            SubdivisionViewModel viewWarehouseReceive = logicS.Read(new SubdivisionBindingModel { Code = view.Warehousereceivercode })?[0];
                            ResponsePersonViewModel viewMOLReceiver = logicR.Read(new ResponsePersonBindingModel { Code = view.Responsiblereceivercode })?[0];
                            ProviderViewModel viewProvider = logicProvider.Read(new ProviderBindingModels { Code = view.Providercode })?[0];
                            comboBox.Text = comboBox.Items[0].ToString();
                            comboBoxMOL.Text = viewMOLReceiver.Name;
                            comboBoxWarehouse.Text = viewWarehouseReceive.Name;
                            comboBoxProvider.Text = viewProvider.Name;
                            dateTimePicker.Value = view.Date; 
                        }
                        else if (view.Typeofoperation == "Перемещение материалов с одного склада на другой")
                        {
                            SubdivisionViewModel viewWarehouseReceive = logicS.Read(new SubdivisionBindingModel { Code = view.Warehousereceivercode })?[0];
                            SubdivisionViewModel viewWarehouseSender = logicS.Read(new SubdivisionBindingModel { Code = view.Warehousesendercode })?[0];
                            ResponsePersonViewModel viewMOLReceiver = logicR.Read(new ResponsePersonBindingModel { Code = view.Responsiblereceivercode })?[0];
                            ResponsePersonViewModel viewMOLSender = logicR.Read(new ResponsePersonBindingModel { Code = view.Responsiblesendercode })?[0];
                            comboBox.Text = comboBox.Items[1].ToString();
                            comboBoxMOLReceiver.Text = viewMOLReceiver.Name;
                            comboBoxMOLSender.Text = viewMOLSender.Name;
                            comboBoxWarehouseReceiver.Text = viewWarehouseReceive.Name;
                            comboBoxWarehouseSender.Text = viewWarehouseSender.Name;
                            dateTimePicker.Value = view.Date;
                        }
                        else if (view.Typeofoperation == "Отпуск материала со склада в производство")
                        {
                            SubdivisionViewModel viewWarehouseReceive = logicS.Read(new SubdivisionBindingModel { Code = view.Warehousesendercode })?[0];
                            SubdivisionViewModel viewWarehouseSubdivision = logicS.Read(new SubdivisionBindingModel { Code = view.Subdivisioncode })?[0];
                            ResponsePersonViewModel viewMOLReceiver = logicR.Read(new ResponsePersonBindingModel { Code = view.Responsiblereceivercode })?[0];
                            comboBox.Text = comboBox.Items[2].ToString();
                            comboBoxWarehouse.Text = viewWarehouseReceive.Name;
                            comboBoxSubdivision.Text = viewWarehouseSubdivision.Name;
                            comboBoxMOLReceiver.Text = viewMOLReceiver.Name;
                            dateTimePicker.Value = view.Date;
                        }

                        textBoxPrice.Text = view.Price.ToString();
                        tablePart = view.TablePart;
                        LoadData();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                }
            }

            else
            {
                tablePart = new Dictionary<int, (string, int, decimal)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (tablePart != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in tablePart)
                    {
                        dataGridView.Rows.Add(new object[] {pc.Key, pc.Value.Item1, pc.Value.Item2, pc.Value.Item3});
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTablePart>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (tablePart.ContainsKey(form.Code))
                {
                    tablePart[form.Code] = (form.MaterialName, form.Count, form.Price);
                }

                else
                {
                    tablePart.Add(form.Code, (form.MaterialName, form.Count, form.Price));
                }
                sumMaterials = 0;
                foreach (var item in tablePart)
                {
                    sumMaterials += item.Value.Item2 * item.Value.Item3;
                }
                textBoxPrice.Text = sumMaterials.ToString();
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormTablePart>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Code = id;
                form.Count = tablePart[id].Item2;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    tablePart.Remove(id);
                    tablePart[form.Code] = (form.MaterialName, form.Count, form.Price);

                    sumMaterials = 0;
                    foreach (var item in tablePart)
                    {
                        sumMaterials += item.Value.Item2 * item.Value.Item3;
                    }
                    textBoxPrice.Text = sumMaterials.ToString();

                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        tablePart.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                        sumMaterials = 0;
                        foreach (var item in tablePart)
                        {
                            sumMaterials += item.Value.Item2 * item.Value.Item3;
                        }
                        textBoxPrice.Text = sumMaterials.ToString();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox.Text))
            {
                MessageBox.Show("Не выбрали тип операции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Неверная цена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tablePart == null || tablePart.Count == 0)
            {
                MessageBox.Show("Выберите материалы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (comboBox.Text == "Поступление материала на склад")
                {
                    if (dateTimePicker == null)
                    {
                        MessageBox.Show("Не выбрали дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxWarehouse.Text))
                    {
                        MessageBox.Show("Не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxMOL.Text))
                    {
                        MessageBox.Show("Не выбрали МОЛ-а", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxCheckMaterial.Text))
                    {
                        MessageBox.Show("Выберите счет материала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxProvider.Text)) 
                    {
                        MessageBox.Show("Выберите Поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MaterialViewModel viewModelMaterial;

                    if (view == null)
                    {
                        ChartOfAccountViewModel viewDebet = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                        ChartOfAccountViewModel viewCredit = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = "60" })?[0];

                        int code = logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = Convert.ToInt32(comboBoxProvider.SelectedValue),
                            Warehousesendercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOL.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousereceivercode })?[0];
                        ResponsePersonViewModel viewMOL = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblereceivercode })?[0];
                        ProviderViewModel viewProvider = logicProvider.Read(new ProviderBindingModels { Code = viewOperation.Providercode })?[0];
                        
                        foreach (var item in viewTablePart)
                        {
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                            {
                                Debetcheck = Convert.ToInt32(viewDebet.Code),
                                Date = viewOperation.Date,
                                Numberdebetcheck = viewDebet.NumberOfCheck,
                                Subcontodebet1 = viewModelMaterial.Name,
                                Subcontodebet2 = viewSubdivision.Name,
                                Subcontodebet3 = viewMOL.Name,
                                Creditcheck = Convert.ToInt32(viewCredit.Code),
                                Numbercreditcheck = viewCredit.NumberOfCheck,
                                Subcontocredit1 = viewProvider.Name,
                                Subcontocredit2 = null,
                                Subcontocredit3 = null,
                                Nameofmaterial = viewModelMaterial.Name,
                                Count = item.Count,
                                Sum = Convert.ToDecimal(item.Count * item.Price),
                                Tablepartcode = item.Code,
                                Operationcode = item.Operationcode,
                                Comment = "Операция поступления материалов",
                                Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                            });
                        }                        
                    }

                    else if (view != null)
                    {                       
                        ChartOfAccountViewModel viewDebet = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                        ChartOfAccountViewModel viewCredit = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = "60" })?[0];

                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Code = view.Code,
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = Convert.ToInt32(comboBoxProvider.SelectedValue),
                            Warehousesendercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOL.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewChangedOperation = logic.Read(new OperationBindingModel { Code = view.Code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(viewChangedOperation.Code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewChangedOperation.Warehousereceivercode })?[0];
                        ResponsePersonViewModel viewMOL = logicR.Read(new ResponsePersonBindingModel { Code = viewChangedOperation.Responsiblereceivercode })?[0];
                        ProviderViewModel viewProvider = logicProvider.Read(new ProviderBindingModels { Code = viewChangedOperation.Providercode })?[0];

                        foreach (var item in viewTablePart)
                        {                         
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            PostingJournalViewModel viewJournal = logicJournal.Read(new PostingJournalBindingModel { Tablepartcode = item.Code })?[0];

                            if (viewJournal != null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Code = viewJournal.Code,
                                    Debetcheck = Convert.ToInt32(viewDebet.Code),
                                    Date = viewChangedOperation.Date,
                                    Numberdebetcheck = viewDebet.NumberOfCheck,
                                    Subcontodebet1 = viewModelMaterial.Name,
                                    Subcontodebet2 = viewSubdivision.Name,
                                    Subcontodebet3 = viewMOL.Name,
                                    Creditcheck = Convert.ToInt32(viewCredit.Code),
                                    Numbercreditcheck = viewCredit.NumberOfCheck,
                                    Subcontocredit1 = viewProvider.Name,
                                    Subcontocredit2 = null,
                                    Subcontocredit3 = null,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = viewJournal.Comment,
                                    Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                                });
                            }
                            else if (viewJournal == null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Debetcheck = Convert.ToInt32(viewDebet.Code),
                                    Date = viewChangedOperation.Date,
                                    Numberdebetcheck = viewDebet.NumberOfCheck,
                                    Subcontodebet1 = viewDebet.subconto1,
                                    Subcontodebet2 = viewDebet.subconto2,
                                    Subcontodebet3 = viewDebet.subconto3,
                                    Creditcheck = Convert.ToInt32(viewCredit.Code),
                                    Numbercreditcheck = viewCredit.NumberOfCheck,
                                    Subcontocredit1 = viewCredit.subconto1,
                                    Subcontocredit2 = viewCredit.subconto2,
                                    Subcontocredit3 = viewCredit.subconto3,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = "Операция поступления материалов",
                                    Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                                });
                            }
                        }                  
                    }
                }

                if (comboBox.Text == "Перемещение материалов с одного склада на другой" || comboBox.Text == "Отпуск материала со склада в производство")
                {
                    ChartOfAccountViewModel viewDebet = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                    ChartOfAccountViewModel viewCredit = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                    List<PostingJournalViewModel> viewPostingJournalReceivers = new List<PostingJournalViewModel>();
                    List<PostingJournalViewModel> viewPostingJournalSenders = new List<PostingJournalViewModel>();
                    if (comboBox.Text == "Перемещение материалов с одного склада на другой")
                    {
                        viewPostingJournalSenders = logicJournal.Read(new PostingJournalBindingModel { Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue), Date = dateTimePicker.Value });
                        viewPostingJournalReceivers = logicJournal.Read(new PostingJournalBindingModel { Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue), Date = dateTimePicker.Value });
                    }
                    if (comboBox.Text == "Отпуск материала со склада в производство")
                    {
                        viewPostingJournalSenders = logicJournal.Read(new PostingJournalBindingModel { Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue), Date = dateTimePicker.Value });
                        viewPostingJournalReceivers = logicJournal.Read(new PostingJournalBindingModel { Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue), Date = dateTimePicker.Value });
                    }

                    Dictionary<string, int> materials = new Dictionary<string, int>();
                    foreach (var item in viewPostingJournalReceivers)
                    {
                        if (materials.ContainsKey(item.Nameofmaterial))
                        {
                            materials[item.Nameofmaterial] = materials[item.Nameofmaterial] + item.Count;                          
                        }                       
                        else
                        {
                            materials.Add(item.Nameofmaterial, item.Count);
                        }
                    }

                    foreach (var item in viewPostingJournalSenders)
                    {
                        if (view != null && item.Operationcode == view.Code)
                        {
                            continue;
                        }
                        if (materials.ContainsKey(item.Nameofmaterial))
                        {
                            materials[item.Nameofmaterial] = materials[item.Nameofmaterial] - item.Count;
                        }
                        else
                        {
                            materials.Add(item.Nameofmaterial, -item.Count);
                        }
                    }

                    foreach (var item in tablePart)
                    {
                        if (materials.ContainsKey(item.Value.Item1) && materials[item.Value.Item1] >= item.Value.Item2)
                        {
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("Кол-во поступленных на выбранный склад материалов меньше кол-ва, которое хотим отправить на другой склад или какого-то материала вообще не поступало", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (comboBox.Text == "Перемещение материалов с одного склада на другой")
                {
                    if (dateTimePicker == null)
                    {
                        MessageBox.Show("Не выбрали дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxWarehouseReceiver.Text))
                    {
                        MessageBox.Show("Не выбрали склад получатель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxWarehouseSender.Text))
                    {
                        MessageBox.Show("Не выбрали склад отправитель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (comboBoxWarehouseSender.SelectedIndex == comboBoxWarehouseReceiver.SelectedIndex)
                    {
                        MessageBox.Show("Нельзя выбрать один и тот же склад отправитель и получатель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxMOLReceiver.Text))
                    {
                        MessageBox.Show("Не выбрали МОЛ-а получателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxMOLSender.Text))
                    {
                        MessageBox.Show("Не выбрали МОЛ-а отправителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (comboBoxMOLSender.SelectedIndex == comboBoxMOLReceiver.SelectedIndex)
                    {
                        MessageBox.Show("Нельзя выбрать одно и того же МОЛ-а отправителя и получателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }                              
                    if (string.IsNullOrEmpty(comboBoxCheckMaterial.Text))
                    {
                        MessageBox.Show("Выберите счет материала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }                   

                    MaterialViewModel viewModelMaterial;

                    if (view == null)
                    {
                        ChartOfAccountViewModel viewDebets = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                        ChartOfAccountViewModel viewCredits = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];

                        int code = logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseReceiver.SelectedValue),
                            Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOLReceiver.SelectedValue),
                            Responsiblesendercode = Convert.ToInt32(comboBoxMOLSender.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousereceivercode })?[0];
                        SubdivisionViewModel viewWarehouseSender = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousesendercode })?[0];
                        ResponsePersonViewModel viewMOLReceiver = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblereceivercode })?[0];
                        ResponsePersonViewModel viewMOLSender = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblesendercode })?[0];

                        foreach (var item in viewTablePart)
                        {
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                            {
                                Debetcheck = Convert.ToInt32(viewDebets.Code),
                                Date = viewOperation.Date,
                                Numberdebetcheck = viewDebets.NumberOfCheck,
                                Subcontodebet1 = viewModelMaterial.Name,
                                Subcontodebet2 = viewSubdivision.Name,
                                Subcontodebet3 = viewMOLReceiver.Name,
                                Creditcheck = Convert.ToInt32(viewCredits.Code),
                                Numbercreditcheck = viewCredits.NumberOfCheck,
                                Subcontocredit1 = viewModelMaterial.Name,
                                Subcontocredit2 = viewWarehouseSender.Name,
                                Subcontocredit3 = viewMOLSender.Name,
                                Nameofmaterial = viewModelMaterial.Name,
                                Count = item.Count,
                                Sum = Convert.ToDecimal(item.Count * item.Price),
                                Tablepartcode = item.Code,
                                Operationcode = item.Operationcode,
                                Comment = "Операция перемещения материалов",
                                Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseReceiver.SelectedValue),
                                Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue),
                            });
                        }
                    }
                    else if (view != null)
                    {
                        ChartOfAccountViewModel viewDebets = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];
                        ChartOfAccountViewModel viewCredits = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = "60" })?[0];

                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Code = view.Code,
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseReceiver.SelectedValue),
                            Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOLReceiver.SelectedValue),
                            Responsiblesendercode = Convert.ToInt32(comboBoxMOLSender.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = view.Code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousereceivercode })?[0];
                        SubdivisionViewModel viewWarehouseSender = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousesendercode })?[0];
                        ResponsePersonViewModel viewMOLReceiver = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblereceivercode })?[0];
                        ResponsePersonViewModel viewMOLSender = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblesendercode })?[0];

                        foreach (var item in viewTablePart)
                        {
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            PostingJournalViewModel viewJournal = logicJournal.Read(new PostingJournalBindingModel { Tablepartcode = item.Code })?[0];

                            if (viewJournal != null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Code = viewJournal.Code,
                                    Debetcheck = Convert.ToInt32(viewDebets.Code),
                                    Date = viewOperation.Date,
                                    Numberdebetcheck = viewDebets.NumberOfCheck,
                                    Subcontodebet1 = viewModelMaterial.Name,
                                    Subcontodebet2 = viewSubdivision.Name,
                                    Subcontodebet3 = viewMOLReceiver.Name,
                                    Creditcheck = Convert.ToInt32(viewCredits.Code),
                                    Numbercreditcheck = viewCredits.NumberOfCheck,
                                    Subcontocredit1 = viewModelMaterial.Name,
                                    Subcontocredit2 = viewWarehouseSender.Name,
                                    Subcontocredit3 = viewMOLSender.Name,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = viewJournal.Comment,
                                    Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseReceiver.SelectedValue),
                                    Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue),
                                });
                            }
                            else if (viewJournal == null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Debetcheck = Convert.ToInt32(viewDebets.Code),
                                    Date = viewOperation.Date,
                                    Numberdebetcheck = viewDebets.NumberOfCheck,
                                    Subcontodebet1 = viewModelMaterial.Name,
                                    Subcontodebet2 = viewSubdivision.Name,
                                    Subcontodebet3 = viewMOLReceiver.Name,
                                    Creditcheck = Convert.ToInt32(viewCredits.Code),
                                    Numbercreditcheck = viewCredits.NumberOfCheck,
                                    Subcontocredit1 = viewModelMaterial.Name,
                                    Subcontocredit2 = viewWarehouseSender.Name,
                                    Subcontocredit3 = viewMOLSender.Name,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = "Операция перемещения материалов",
                                    Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseReceiver.SelectedValue),
                                    Warehousesendercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue)
                                });
                            }
                        }
                    }
                }        

                if (comboBox.Text == "Отпуск материала со склада в производство")
                {
                    if (dateTimePicker == null)
                    {
                        MessageBox.Show("Не выбрали дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxWarehouse.Text))
                    {
                        MessageBox.Show("Не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxSubdivision.Text))
                    {
                        MessageBox.Show("Не выбрали подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxCheckMaterial.Text))
                    {
                        MessageBox.Show("Выберите счет материала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxChecks.Text))
                    {
                        MessageBox.Show("Выберите счет затрат", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(comboBoxMOLReceiver.Text))
                    {
                        MessageBox.Show("Выберите счет затрат", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MaterialViewModel viewModelMaterial;

                    if (view == null)
                    {
                        ChartOfAccountViewModel viewDebet = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxChecks.Text })?[0];
                        ChartOfAccountViewModel viewCredit = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];

                        int code = logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = null,
                            Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = Convert.ToInt32(comboBoxSubdivision.SelectedValue),
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOLReceiver.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Subdivisioncode })?[0];
                        SubdivisionViewModel viewWarehouse = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousesendercode })?[0];
                        ResponsePersonViewModel viewMOL = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblereceivercode })?[0];

                        foreach (var item in viewTablePart)
                        {
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                            {
                                Debetcheck = Convert.ToInt32(viewDebet.Code),
                                Date = viewOperation.Date,
                                Numberdebetcheck = viewDebet.NumberOfCheck,
                                Subcontodebet1 = viewSubdivision.Name,
                                Subcontodebet2 = null,
                                Subcontodebet3 = null,
                                Creditcheck = Convert.ToInt32(viewCredit.Code),
                                Numbercreditcheck = viewCredit.NumberOfCheck,
                                Subcontocredit1 = viewModelMaterial.Name,
                                Subcontocredit2 = viewWarehouse.Name,
                                Subcontocredit3 = viewMOL.Name,
                                Nameofmaterial = viewModelMaterial.Name,
                                Count = item.Count,
                                Sum = Convert.ToDecimal(item.Count * item.Price),
                                Tablepartcode = item.Code,
                                Operationcode = item.Operationcode,
                                Comment = "Операция реализации материалов",
                                Warehousereceivercode = null,
                                Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            });
                        }
                    }
                    else if (view != null)
                    {
                        ChartOfAccountViewModel viewDebet = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxChecks.Text })?[0];
                        ChartOfAccountViewModel viewCredit = logicChart.Read(new ChartOfAccountsBindingModel { NumberOfCheck = comboBoxCheckMaterial.Text })?[0];

                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Code = view.Code,
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = null,
                            Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = Convert.ToInt32(comboBoxSubdivision.SelectedValue),
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOLReceiver.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });

                        OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = code })?[0];
                        List<TablePartViewModel> viewTablePart = logicTablePart.Read(new TablePartBindingModel { Operationcode = Convert.ToInt32(code) });
                        SubdivisionViewModel viewSubdivision = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Subdivisioncode })?[0];
                        SubdivisionViewModel viewWarehouse = logicS.Read(new SubdivisionBindingModel { Code = viewOperation.Warehousesendercode })?[0];
                        ResponsePersonViewModel viewMOL = logicR.Read(new ResponsePersonBindingModel { Code = viewOperation.Responsiblereceivercode })?[0];

                        foreach (var item in viewTablePart)
                        {
                            viewModelMaterial = new MaterialViewModel();
                            viewModelMaterial = logicMaterial.Read(new MaterialBindingModel { Code = item.Materialcode })?[0];

                            PostingJournalViewModel viewJournal = logicJournal.Read(new PostingJournalBindingModel { Tablepartcode = item.Code })?[0];

                            if (viewJournal != null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Code = viewJournal.Code,
                                    Debetcheck = Convert.ToInt32(viewDebet.Code),
                                    Date = viewOperation.Date,
                                    Numberdebetcheck = viewDebet.NumberOfCheck,
                                    Subcontodebet1 = viewSubdivision.Name,
                                    Subcontodebet2 = null,
                                    Subcontodebet3 = null,
                                    Creditcheck = Convert.ToInt32(viewCredit.Code),
                                    Numbercreditcheck = viewCredit.NumberOfCheck,
                                    Subcontocredit1 = viewModelMaterial.Name,
                                    Subcontocredit2 = viewWarehouse.Name,
                                    Subcontocredit3 = viewMOL.Name,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = viewJournal.Comment,
                                    Warehousereceivercode = null,
                                    Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                                });
                            }
                            else if (viewJournal == null)
                            {
                                logicJournal.CreateOrUpdate(new PostingJournalBindingModel
                                {
                                    Debetcheck = Convert.ToInt32(viewDebet.Code),
                                    Date = viewOperation.Date,
                                    Numberdebetcheck = viewDebet.NumberOfCheck,
                                    Subcontodebet1 = viewSubdivision.Name,
                                    Subcontodebet2 = null,
                                    Subcontodebet3 = null,
                                    Creditcheck = Convert.ToInt32(viewCredit.Code),
                                    Numbercreditcheck = viewCredit.NumberOfCheck,
                                    Subcontocredit1 = viewModelMaterial.Name,
                                    Subcontocredit2 = viewWarehouse.Name,
                                    Subcontocredit3 = viewMOL.Name,
                                    Nameofmaterial = viewModelMaterial.Name,
                                    Count = item.Count,
                                    Sum = Convert.ToDecimal(item.Count * item.Price),
                                    Tablepartcode = item.Code,
                                    Operationcode = item.Operationcode,
                                    Comment = "Операция реализации материалов",
                                    Warehousereceivercode = null,
                                    Warehousesendercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                                });
                            }
                        }
                    }
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.Text == "Поступление материала на склад")
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                comboBoxMOL.Visible = true;
                dateTimePicker.Visible = true;
                comboBoxWarehouse.Visible = true;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                comboBoxWarehouseSender.Visible = false;
                comboBoxWarehouseReceiver.Visible = false;
                comboBoxMOLSender.Visible = false;
                comboBoxMOLReceiver.Visible = false;
                label10.Visible = false;               
                comboBoxSubdivision.Visible = false;
                label11.Visible = false;
                comboBoxChecks.Visible = false;
                label12.Visible = true;
                comboBoxCheckMaterial.Visible = true;
                label13.Visible = true;
                comboBoxProvider.Visible = true;
            }

            else if (comboBox.Text == "Перемещение материалов с одного склада на другой")
            {
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = true;
                comboBoxMOL.Visible = false;
                dateTimePicker.Visible = true;
                comboBoxWarehouse.Visible = false;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                comboBoxWarehouseSender.Visible = true;
                comboBoxWarehouseReceiver.Visible = true;
                comboBoxMOLSender.Visible = true;
                comboBoxMOLReceiver.Visible = true;
                label10.Visible = false;
                comboBoxSubdivision.Visible = false;
                label11.Visible = false;
                comboBoxChecks.Visible = false;
                label12.Visible = true;
                comboBoxCheckMaterial.Visible = true;
                label13.Visible = false;
                comboBoxProvider.Visible = false;
            }

            else if (comboBox.Text == "Отпуск материала со склада в производство")
            {
                label3.Visible = true;
                label4.Visible = false;
                label5.Visible = true;
                comboBoxMOL.Visible = false;
                dateTimePicker.Visible = true;
                comboBoxWarehouse.Visible = true;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = true;
                comboBoxWarehouseSender.Visible = false;
                comboBoxWarehouseReceiver.Visible = false;
                comboBoxMOLSender.Visible = false;
                comboBoxMOLReceiver.Visible = true;
                label10.Visible = true;
                comboBoxSubdivision.Visible = true;
                label11.Visible = true;
                comboBoxChecks.Visible = true;
                label12.Visible = true;
                comboBoxCheckMaterial.Visible = true;
                label13.Visible = false;
                comboBoxProvider.Visible = false;
            }
        }
    }
}