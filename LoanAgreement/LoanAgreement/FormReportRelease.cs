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
    public partial class FormReportRelease : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportRelease(ReportLogic logic, SubdivisonLogic logicSub)
        {
            InitializeComponent();
            this.logic = logic;

            List<SubdivisionViewModel> subdivisions = logicSub.Read(new SubdivisionBindingModel { Warehouse = false });

            if (subdivisions != null)
            {
                comboBoxSubdivision.DisplayMember = "Name";
                comboBoxSubdivision.ValueMember = "Code";
                comboBoxSubdivision.DataSource = subdivisions;
                comboBoxSubdivision.SelectedItem = null;
            }
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxSubdivision.Text))
            {
                MessageBox.Show("Не выбрали подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                reportViewer.LocalReport.DataSources.Clear();
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "Ведомость отпуска материалов в производство по подразделению" + " '" + comboBoxSubdivision.Text + "' " + "за период с " + dateTimePickerFrom.Value.ToShortDateString() + " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetTablePartRelease(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value,
                    WarehouseCode = Convert.ToInt32(comboBoxSubdivision.SelectedValue)
                });
                ReportDataSource source = new ReportDataSource("DataSetRelease", dataSource);
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
