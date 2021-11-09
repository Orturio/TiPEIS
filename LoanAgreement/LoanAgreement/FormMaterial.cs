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
    public partial class FormMaterial : Form
    {
        public int Code { set { code = value; } }
        private int? code;

        private readonly MaterialLogic logic;

        public FormMaterial(MaterialLogic logicА)
        {
            logic = logicА;
            InitializeComponent();
        }

        MaterialViewModel view;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (char c in textBoxName.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !(c == '.'))
                {
                    MessageBox.Show("Некорректные данные для названия материала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (char c in textBoxPrice.Text)
            {
                if (!char.IsNumber(c) && !(c == ','))
                {
                    MessageBox.Show("Некорректные данные для цены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new MaterialBindingModel
                    {
                        Code = view.Code,
                        Name = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text)
                    });
                }

                else
                {
                    logic.CreateOrUpdate(new MaterialBindingModel
                    {
                        Name = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text)
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

        private void FormMaterial_Load(object sender, EventArgs e)
        {
            if (code.HasValue)
            {
                try
                {
                    view = logic.Read(new MaterialBindingModel { Code = code })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxPrice.Text = Convert.ToString(view.Price);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
