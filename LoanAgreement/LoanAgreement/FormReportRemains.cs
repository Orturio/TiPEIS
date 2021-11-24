using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace LoanAgreement
{
    public partial class FormReportRemains : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportRemains(ReportLogic logic, SubdivisonLogic logicSub)
        {
            InitializeComponent();
            this.logic = logic;

            List<SubdivisionViewModel> warehouses = logicSub.Read(new SubdivisionBindingModel { Warehouse = true });

            if (warehouses != null)
            {
                comboBoxWarehouse.DisplayMember = "Name";
                comboBoxWarehouse.ValueMember = "Code";
                comboBoxWarehouse.DataSource = warehouses;
                comboBoxWarehouse.SelectedItem = null;
            }
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date == null)
            {
                MessageBox.Show("Дата была не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxWarehouse.Text))
            {
                MessageBox.Show("Не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                reportViewer.LocalReport.DataSources.Clear();
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", " Ведомость остатков на складе" + " '" + comboBoxWarehouse.Text + "' " + "на дату " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetTablePartRemains(new ReportBindingModel
                {
                    DateTo = dateTimePickerTo.Value,
                    WarehouseCode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                });
                ReportDataSource source = new ReportDataSource("DataSetRemains", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date == null)
            {
                MessageBox.Show("Дата была не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxWarehouse.Text))
            {
                MessageBox.Show("Не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOperationsToPdfFileRemains(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateTo = dateTimePickerTo.Value,
                            WarehouseCode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                        });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
