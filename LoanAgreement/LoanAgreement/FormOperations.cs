using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.ViewModels;
using MaterialAccountingBusinessLogic.BusinessLogic;

namespace LoanAgreement
{
    public partial class FormOperations : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly OperationLogic logic;

        public FormOperations(OperationLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormOperations_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[6].Visible = false;
                    dataGridView.Columns[7].Visible = false;
                    dataGridView.Columns[8].Visible = false;
                    dataGridView.Columns[10].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
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
            var form = Container.Resolve<FormOperation>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormOperation>();
                form.Code = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
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
                    int code = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    OperationViewModel viewOperation = logic.Read(new OperationBindingModel { Code = code })?[0];

                    List<OperationViewModel> viewOperationsMoving = new List<OperationViewModel>();
                    List<OperationViewModel> viewOperationsRealise = new List<OperationViewModel>();

                    if (viewOperation.Typeofoperation == "Поступление материала на склад")
                    {
                        viewOperationsMoving = logic.Read(new OperationBindingModel { Typeofoperation = "Перемещение материалов с одного склада на другой", Warehousesendercode = viewOperation.Warehousereceivercode, Date = viewOperation.Date});
                        viewOperationsRealise = logic.Read(new OperationBindingModel { Typeofoperation = "Отпуск материала со склада в производство", Warehousesendercode = viewOperation.Warehousereceivercode, Date = viewOperation.Date });
                    }
                   
                    if (viewOperation.Typeofoperation == "Перемещение материалов с одного склада на другой")
                    {
                        viewOperationsRealise = logic.Read(new OperationBindingModel { Typeofoperation = "Отпуск материала со склада в производство", Warehousesendercode = viewOperation.Warehousereceivercode, Date = viewOperation.Date });
                    }

                    if ((viewOperationsMoving.Count == 0 && viewOperationsRealise.Count == 0))
                    {
                        try
                        {
                            logic.Delete(new OperationBindingModel { Code = code });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Удаление невозможно, так как есть движения по операции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonPostingJournal_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormPostingJournal>();
                form.Code = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
