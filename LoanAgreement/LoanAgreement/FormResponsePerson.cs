using System;
using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace LoanAgreement
{
    public partial class FormResponsePerson : Form
    {
        public int Code { set { code = value; } }
        private int? code;

        private readonly ResponsePersonLogic logic;

        public FormResponsePerson(ResponsePersonLogic logicА)
        {
            logic = logicА;
            InitializeComponent();
        }

        ResponsePersonViewModel view;

        private void FormResponsePerson_Load(object sender, EventArgs e)
        {
            if (code.HasValue)
            {
                try
                {
                    view = logic.Read(new ResponsePersonBindingModel { Code = code })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxSurname.Text = view.Surname;
                        textBoxMiddleName.Text = view.Middlename;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxSurname.Text))
            {
                MessageBox.Show("Заполните фамилию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMiddleName.Text))
            {
                MessageBox.Show("Заполните отчество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (char c in textBoxName.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !(c == '.'))
                {
                    MessageBox.Show("Некорректные данные для имени", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (char c in textBoxSurname.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !(c == '.'))
                {
                    MessageBox.Show("Некорректные данные для фамилии", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (char c in textBoxMiddleName.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !(c == '.'))
                {
                    MessageBox.Show("Некорректные данные для отчества", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new ResponsePersonBindingModel
                    {
                        Code = view.Code,
                        Name = textBoxName.Text,
                        Surname = textBoxSurname.Text,
                        Middlename = textBoxMiddleName.Text
                    });
                }

                else
                {
                    logic.CreateOrUpdate(new ResponsePersonBindingModel
                    {
                        Name = textBoxName.Text,
                        Surname = textBoxSurname.Text,
                        Middlename = textBoxMiddleName.Text
                    });
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DbUpdateException exe)
            {
                MessageBox.Show(exe?.InnerException?.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
