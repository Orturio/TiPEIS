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
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;

namespace LoanAgreement
{
    public partial class FormTablePart : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Code
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }
            set { comboBoxComponent.SelectedValue = value; }
        }

        private readonly MaterialLogic logic;

        public decimal Price {get; set;}

        public string MaterialName { get { return comboBoxComponent.Text; } }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        MaterialViewModel view;

        public FormTablePart(MaterialLogic logic)
        {
            InitializeComponent();
            this.logic = logic;

            List<MaterialViewModel> list = logic.Read(null);

            if (list != null)
            {
                comboBoxComponent.DisplayMember = "Name";
                comboBoxComponent.ValueMember = "Code";
                comboBoxComponent.DataSource = list;
                comboBoxComponent.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }
            foreach (char c in textBoxCount.Text)
            {
                if (!char.IsNumber(c))
                {
                    MessageBox.Show("Некорректные данные для количества", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            view = logic.Read(new MaterialBindingModel { Code = Code })?[0];

            Price = view.Price;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
