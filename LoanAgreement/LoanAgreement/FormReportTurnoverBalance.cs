using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace LoanAgreement
{
    public partial class FormReportTurnoverBalance : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportTurnoverBalance(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;           
        }

        private void FormReportTurnoverBalance_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                reportViewer.LocalReport.DataSources.Clear();
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "Оборотно-сальдовая ведомость по счёту" + " '" + comboBoxCheck.Text + "' с " + dateTimePickerFrom.Value.ToShortDateString() + " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetTurnoverBalance(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value,
                    NumberOfCheck = comboBoxCheck.Text
                });
                ReportDataSource source = new ReportDataSource("DataSetTurnoverBalance", dataSource);
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
