using MaterialAccountingBusinessLogic.BindingModels;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Unity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LoanAgreement
{
    public partial class FormProvider : Form
    {
        public int Code { set { code = value; } }
        private int? code;

        private readonly ProviderLogic logic;

        public FormProvider(ProviderLogic logicА)
        {
            logic = logicА;
            InitializeComponent();
        }

        ProviderViewModel view;

        private void FormProvider_Load(object sender, EventArgs e)
        {
            if (code.HasValue)
            {
                try
                {
                    view = logic.Read(new ProviderBindingModels { Code = code })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
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
            foreach (char c in textBoxName.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    MessageBox.Show("Некорректные данные для имени", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }           

            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new ProviderBindingModels
                    {
                        Code = view.Code,
                        Name = textBoxName.Text
                    });
                }

                else
                {
                    logic.CreateOrUpdate(new ProviderBindingModels
                    {
                        Name = textBoxName.Text
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
