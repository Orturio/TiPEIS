using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;

namespace LoanAgreement
{
    public partial class FormPostingJournal : Form
    {
        private readonly PostingJournalLogic logic;

        public int Code { set { code = value; } }
        private int? code;

        public FormPostingJournal(PostingJournalLogic logic)
        {
            this.logic = logic;
            InitializeComponent();           
        }

        private void FormPostingJournal_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (code != null)
                {
                    var list = logic.Read(new PostingJournalBindingModel { Operationcode = Convert.ToInt32(code)});
                    if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[7].Visible = false;
                        dataGridView.Columns[12].Visible = false;
                        dataGridView.Columns[15].Visible = false;
                        dataGridView.Columns[18].Visible = false;
                        dataGridView.Columns[19].Visible = false;
                        dataGridView.Columns[17].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                else
                {
                    var list = logic.Read(null);
                    if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[7].Visible = false;
                        dataGridView.Columns[12].Visible = false;
                        dataGridView.Columns[15].Visible = false;
                        dataGridView.Columns[18].Visible = false;
                        dataGridView.Columns[19].Visible = false;
                        dataGridView.Columns[17].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
