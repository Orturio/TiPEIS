using System;
using System.Windows.Forms;
using MaterialAccountingBusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using Unity;


namespace LoanAgreement
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ChartOfAccountsLogic logic;

        public FormMain(ChartOfAccountsLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormMain_Load(object sender, EventArgs e)
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
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMaterials>();
            form.ShowDialog();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormProviders>();
            form.ShowDialog();
        }

        private void договорыЗаймаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouses>();
            form.ShowDialog();
        }

        private void мОЛToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormResponsePersons>();
            form.ShowDialog();
        }

        private void журналОперацийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOperations>();
            form.ShowDialog();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPostingJournal>();
            form.ShowDialog();
        }

        private void ведомостьРасходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportReceive>();
            form.ShowDialog();
        }

        private void ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportRemains>();
            form.ShowDialog();
        }

        private void ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportRelease>();
            form.ShowDialog();
        }

        private void оборотносальдоваяВедомостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportTurnoverBalance>();
            form.ShowDialog();
        }
    }
}
