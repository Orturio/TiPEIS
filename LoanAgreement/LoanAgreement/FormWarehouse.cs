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
    public partial class FormWarehouse : Form
    {
        public int Code { set { code = value; } }
        private int? code;

        private readonly SubdivisonLogic logic;

        public FormWarehouse(SubdivisonLogic logicА)
        {
            logic = logicА;
            InitializeComponent();
        }

        SubdivisionViewModel view;

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (code.HasValue)
            {
                try
                {
                    view = logic.Read(new SubdivisionBindingModel { Code = code })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        if (view.Warehouse)
                        {
                            comboBox1.Text = comboBox1.Items[0].ToString();
                        }
                        else
                        {
                            comboBox1.Text = comboBox1.Items[1].ToString();
                        }
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
            bool warehouse = false; 

            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    warehouse = true;
                    break;
                case 1:
                    warehouse = false;
                    break;
            }

            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            foreach (char c in textBoxName.Text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && !(c == '.'))
                {
                    MessageBox.Show("Некорректные данные для названия склада", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            try
            {
                if (view != null)
                {
                    logic.CreateOrUpdate(new SubdivisionBindingModel
                    {
                        Code = view.Code,
                        Name = textBoxName.Text,
                        Warehouse = warehouse
                    });
                }

                else
                {
                    logic.CreateOrUpdate(new SubdivisionBindingModel
                    {
                        Name = textBoxName.Text,
                        Warehouse = warehouse
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
