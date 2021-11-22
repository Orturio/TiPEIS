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
    public partial class FormReportReceive : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportReceive(ReportLogic logic, SubdivisonLogic logicSub)
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
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "Ведомость поступления материалов на склад" + " '" + comboBoxWarehouse.Text + "' " + "за период с " + dateTimePickerFrom.Value.ToShortDateString() + " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetTablePart(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value,
                    WarehouseCode = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                });
                ReportDataSource source = new ReportDataSource("DataSetReceive", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}