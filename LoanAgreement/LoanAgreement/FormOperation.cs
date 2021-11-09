using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

        private readonly ResponsePersonLogic logicR;

        private readonly SubdivisonLogic logicS;

        private decimal? sumMaterials = 0; 

        private int? code;

        OperationViewModel view;

        private Dictionary<int, (string, int, decimal)> tablePart;

        public FormOperation(OperationLogic logic, ResponsePersonLogic logicR, SubdivisonLogic logicS)
        {
            InitializeComponent();
            this.logic = logic;
            this.logicR = logicR;
            this.logicS = logicS;

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
        }


        private void FormOperation_Load(object sender, EventArgs e)
        {
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
                            comboBox.Text = comboBox.Items[0].ToString();
                            comboBoxMOL.Text = viewMOLReceiver.Name;
                            comboBoxWarehouse.Text = viewWarehouseReceive.Name;
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
                            SubdivisionViewModel viewWarehouseReceive = logicS.Read(new SubdivisionBindingModel { Code = view.Warehousereceivercode })?[0];
                            SubdivisionViewModel viewWarehouseSubdivision = logicS.Read(new SubdivisionBindingModel { Code = view.Subdivisioncode })?[0];
                            comboBox.Text = comboBox.Items[2].ToString();
                            comboBoxWarehouse.Text = viewWarehouseReceive.Name;
                            comboBoxSubdivision.Text = viewWarehouseSubdivision.Name;
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
                    if (view == null)
                    {
                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousesendercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOL.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });
                    }
                    else if (view != null)
                    {
                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Code = view.Code,
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousesendercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Subdivisioncode = null,
                            Responsiblesendercode = null,
                            Responsiblereceivercode = Convert.ToInt32(comboBoxMOL.SelectedValue),
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });
                    }
                }

                if (comboBox.Text == "Перемещение материалов с одного склада на другой" || comboBox.Text == "Отпуск материала со склада в производство") 
                {
                    // Проверка на кол-во материаллов
                    List<OperationViewModel> viewSenderReceive = logic.Read(new OperationBindingModel { Typeofoperation = "Поступление материала на склад", Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue) });
                    List<OperationViewModel> viewSenderMoving = new List<OperationViewModel>();
                    bool noWarning = true;

                    if  (comboBox.Text == "Перемещение материалов с одного склада на другой")
                    {
                        viewSenderMoving  = logic.Read(new OperationBindingModel { Typeofoperation = "Перемещение материалов с одного склада на другой", Warehousereceivercode = Convert.ToInt32(comboBoxWarehouseSender.SelectedValue) });
                    }
                    else if (comboBox.Text == "Отпуск материала со склада в производство")
                    {
                        viewSenderMoving = logic.Read(new OperationBindingModel { Typeofoperation = "Перемещение материалов с одного склада на другой", Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue) });
                    }

                    Dictionary<int, (string, int, decimal)> tablePartSender = new Dictionary<int, (string, int, decimal)>();
                    foreach (var viewItem in viewSenderReceive)
                    {
                        foreach (var tablePartItem in viewItem.TablePart)
                        {
                            if (tablePartSender.ContainsKey(tablePartItem.Key))
                            {
                                tablePartSender[tablePartItem.Key] = (tablePartSender[tablePartItem.Key].Item1, tablePartSender[tablePartItem.Key].Item2 + tablePartItem.Value.Item2, tablePartSender[tablePartItem.Key].Item3);
                            }
                            else
                            {
                                tablePartSender.Add(tablePartItem.Key, (tablePartItem.Value));
                            }
                        }
                    }

                    //Если надо учитывать операцию Перемещение материалов с одного склада на другой, тогда нужно будет раскомментировать              
                    foreach (var viewItem in viewSenderMoving)
                    {
                        foreach (var tablePartItem in viewItem.TablePart)
                        {
                            if (tablePartSender.ContainsKey(tablePartItem.Key))
                            {
                                tablePartSender[tablePartItem.Key] = (tablePartSender[tablePartItem.Key].Item1, tablePartSender[tablePartItem.Key].Item2 + tablePartItem.Value.Item2, tablePartSender[tablePartItem.Key].Item3);
                            }
                            else
                            {
                                tablePartSender.Add(tablePartItem.Key, (tablePartItem.Value));
                            }
                        }
                    }

                    Dictionary<int, (string, int, decimal)> changedTablePart = new Dictionary<int, (string, int, decimal)>();
                    foreach (var item in tablePart)
                    {
                        if (!changedTablePart.ContainsKey(item.Key))
                        {
                            changedTablePart.Add(item.Key, (item.Value));
                        }
                        if (tablePartSender.ContainsKey(item.Key) && tablePartSender[item.Key].Item2 >= item.Value.Item2)
                        {
                            continue;
                        }
                        else
                        {
                            noWarning = false;
                            MessageBox.Show("Кол-во поступленных на выбранный склад материалов меньше кол-ва, которое хотим отправить на другой склад или такого материала вообще не поступало", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if(noWarning)
                    {
                        Dictionary<int, (string, int, decimal)> changedTablePartView;
                        foreach (var viewItem in viewSenderReceive)
                        {
                            changedTablePartView = new Dictionary<int, (string, int, decimal)>();
                            foreach (var item in viewItem.TablePart)
                            {
                                if (tablePart.ContainsKey(item.Key) && changedTablePart[item.Key].Item2 >= item.Value.Item2)
                                {
                                    changedTablePart[item.Key] = (item.Value.Item1, changedTablePart[item.Key].Item2 - item.Value.Item2, item.Value.Item3);
                                    if (!changedTablePartView.ContainsKey(item.Key))
                                    {
                                        changedTablePartView.Add(item.Key, (item.Value.Item1, 0, item.Value.Item3));
                                    }
                                }
                                else if (tablePart.ContainsKey(item.Key) && changedTablePart[item.Key].Item2 < item.Value.Item2)
                                {
                                    if (!changedTablePartView.ContainsKey(item.Key))
                                    {
                                        changedTablePartView.Add(item.Key, (item.Value.Item1, item.Value.Item2 - changedTablePart[item.Key].Item2, item.Value.Item3));
                                    }
                                    changedTablePart[item.Key] = (item.Value.Item1, 0, item.Value.Item3);
                                }
                                else if (!tablePart.ContainsKey(item.Key) && !changedTablePartView.ContainsKey(item.Key))
                                {
                                    changedTablePartView.Add(item.Key, (item.Value));
                                }
                            }

                            decimal totalPrice = 0;
                            foreach (var item in changedTablePartView)
                            {
                                totalPrice += item.Value.Item2 * item.Value.Item3;
                            }

                            logic.CreateOrUpdate(new OperationBindingModel
                            {
                                Code = viewItem.Code,
                                Typeofoperation = viewItem.Typeofoperation,
                                Date = viewItem.Date,
                                Providercode = null,
                                Warehousesendercode = null,
                                Warehousereceivercode = viewItem.Warehousereceivercode,
                                Subdivisioncode = null,
                                Responsiblesendercode = null,
                                Responsiblereceivercode = viewItem.Responsiblereceivercode,
                                Price = totalPrice,
                                TablePart = changedTablePartView
                            });
                        }

                        //Если надо учитывать операцию Перемещение материалов с одного склада на другой, тогда нужно будет раскомментировать 
                        foreach (var viewItem in viewSenderMoving)
                        {
                            changedTablePartView = new Dictionary<int, (string, int, decimal)>();
                            foreach (var item in viewItem.TablePart)
                            {
                                if (tablePart.ContainsKey(item.Key) && changedTablePart[item.Key].Item2 >= item.Value.Item2)
                                {
                                    changedTablePart[item.Key] = (item.Value.Item1, changedTablePart[item.Key].Item2 - item.Value.Item2, item.Value.Item3);
                                    if (!changedTablePartView.ContainsKey(item.Key))
                                    {
                                        changedTablePartView.Add(item.Key, (item.Value.Item1, 0, item.Value.Item3));
                                    }
                                }
                                else if (tablePart.ContainsKey(item.Key) && changedTablePart[item.Key].Item2 < item.Value.Item2)
                                {
                                    if (!changedTablePartView.ContainsKey(item.Key))
                                    {
                                        changedTablePartView.Add(item.Key, (item.Value.Item1, item.Value.Item2 - changedTablePart[item.Key].Item2, item.Value.Item3));
                                    }
                                    changedTablePart[item.Key] = (item.Value.Item1, 0, item.Value.Item3);
                                }
                                else if (!tablePart.ContainsKey(item.Key) && !changedTablePartView.ContainsKey(item.Key))
                                {
                                    changedTablePartView.Add(item.Key, (item.Value));
                                }
                            }

                            decimal totalPrice = 0;
                            foreach (var item in changedTablePartView)
                            {
                                totalPrice += item.Value.Item2 * item.Value.Item3;
                            }

                            logic.CreateOrUpdate(new OperationBindingModel
                            {
                                Code = viewItem.Code,
                                Typeofoperation = viewItem.Typeofoperation,
                                Date = viewItem.Date,
                                Providercode = null,
                                Warehousereceivercode = viewItem.Warehousereceivercode,
                                Warehousesendercode = viewItem.Warehousesendercode,
                                Subdivisioncode = null,
                                Responsiblereceivercode = viewItem.Responsiblereceivercode,
                                Responsiblesendercode = viewItem.Responsiblesendercode,
                                Price = totalPrice,
                                TablePart = changedTablePartView
                            });
                        }
                    }
                    // проверка на кол-во материалов закончена
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

                    if (view == null)
                    {                    
                        logic.CreateOrUpdate(new OperationBindingModel
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
                    }
                    else if (view != null)
                    {
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

                    if (view == null)
                    {
                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Warehousesendercode = null,
                            Subdivisioncode = Convert.ToInt32(comboBoxSubdivision.SelectedValue),
                            Responsiblesendercode = null,
                            Responsiblereceivercode = null,
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });
                    }
                    else if (view != null)
                    {
                        logic.CreateOrUpdate(new OperationBindingModel
                        {
                            Code = view.Code,
                            Typeofoperation = comboBox.Text,
                            Date = dateTimePicker.Value,
                            Providercode = null,
                            Warehousereceivercode = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                            Warehousesendercode = null,
                            Subdivisioncode = Convert.ToInt32(comboBoxSubdivision.SelectedValue),
                            Responsiblesendercode = null,
                            Responsiblereceivercode = null,
                            Price = Convert.ToDecimal(textBoxPrice.Text),
                            TablePart = tablePart
                        });
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
                label9.Visible = false;
                comboBoxWarehouseSender.Visible = false;
                comboBoxWarehouseReceiver.Visible = false;
                comboBoxMOLSender.Visible = false;
                comboBoxMOLReceiver.Visible = false;
                label10.Visible = true;
                comboBoxSubdivision.Visible = true;
            }
        }
    }
}