
namespace LoanAgreement
{
    partial class FormOperation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxComponents = new System.Windows.Forms.GroupBox();
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.idComponent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxWarehouse = new System.Windows.Forms.ComboBox();
            this.comboBoxMOL = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxWarehouseSender = new System.Windows.Forms.ComboBox();
            this.comboBoxMOLSender = new System.Windows.Forms.ComboBox();
            this.comboBoxWarehouseReceiver = new System.Windows.Forms.ComboBox();
            this.comboBoxMOLReceiver = new System.Windows.Forms.ComboBox();
            this.comboBoxSubdivision = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Цена = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(132, 59);
            this.textBoxPrice.MaxLength = 10;
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.ReadOnly = true;
            this.textBoxPrice.Size = new System.Drawing.Size(127, 20);
            this.textBoxPrice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Сумма операции:";
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Поступление материала на склад",
            "Перемещение материалов с одного склада на другой",
            "Отпуск материала со склада в производство"});
            this.comboBox.Location = new System.Drawing.Point(132, 21);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(431, 21);
            this.comboBox.TabIndex = 5;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Тип операции:";
            // 
            // groupBoxComponents
            // 
            this.groupBoxComponents.Controls.Add(this.buttonRef);
            this.groupBoxComponents.Controls.Add(this.buttonDel);
            this.groupBoxComponents.Controls.Add(this.buttonUpd);
            this.groupBoxComponents.Controls.Add(this.buttonAdd);
            this.groupBoxComponents.Controls.Add(this.dataGridView);
            this.groupBoxComponents.Location = new System.Drawing.Point(34, 99);
            this.groupBoxComponents.Name = "groupBoxComponents";
            this.groupBoxComponents.Size = new System.Drawing.Size(633, 339);
            this.groupBoxComponents.TabIndex = 7;
            this.groupBoxComponents.TabStop = false;
            this.groupBoxComponents.Text = "Компоненты";
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(492, 159);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(114, 38);
            this.buttonRef.TabIndex = 4;
            this.buttonRef.Text = "Обновить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(493, 118);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(113, 35);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(492, 77);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(114, 35);
            this.buttonUpd.TabIndex = 2;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(492, 37);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(114, 34);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idComponent,
            this.Material,
            this.Count,
            this.Цена});
            this.dataGridView.Location = new System.Drawing.Point(6, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(467, 314);
            this.dataGridView.TabIndex = 0;
            // 
            // idComponent
            // 
            this.idComponent.HeaderText = "idComponent";
            this.idComponent.Name = "idComponent";
            this.idComponent.ReadOnly = true;
            this.idComponent.Visible = false;
            // 
            // Material
            // 
            this.Material.HeaderText = "Материал";
            this.Material.Name = "Material";
            this.Material.ReadOnly = true;
            this.Material.Width = 300;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Width = 150;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(572, 452);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(95, 33);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(478, 452);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(88, 34);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(728, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Склад:";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(728, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "МОЛ:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(727, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Дата:";
            this.label5.Visible = false;
            // 
            // comboBoxWarehouse
            // 
            this.comboBoxWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWarehouse.FormattingEnabled = true;
            this.comboBoxWarehouse.Location = new System.Drawing.Point(792, 69);
            this.comboBoxWarehouse.Name = "comboBoxWarehouse";
            this.comboBoxWarehouse.Size = new System.Drawing.Size(134, 21);
            this.comboBoxWarehouse.TabIndex = 13;
            this.comboBoxWarehouse.Visible = false;
            // 
            // comboBoxMOL
            // 
            this.comboBoxMOL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMOL.FormattingEnabled = true;
            this.comboBoxMOL.Location = new System.Drawing.Point(792, 106);
            this.comboBoxMOL.Name = "comboBoxMOL";
            this.comboBoxMOL.Size = new System.Drawing.Size(134, 21);
            this.comboBoxMOL.TabIndex = 14;
            this.comboBoxMOL.Visible = false;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(792, 141);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(134, 20);
            this.dateTimePicker.TabIndex = 15;
            this.dateTimePicker.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(678, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Склад-отправитель:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(684, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "МОЛ-отправитель:";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(685, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Склад-получатель:";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(691, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "МОЛ-получатель:";
            this.label9.Visible = false;
            // 
            // comboBoxWarehouseSender
            // 
            this.comboBoxWarehouseSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWarehouseSender.FormattingEnabled = true;
            this.comboBoxWarehouseSender.Location = new System.Drawing.Point(792, 69);
            this.comboBoxWarehouseSender.Name = "comboBoxWarehouseSender";
            this.comboBoxWarehouseSender.Size = new System.Drawing.Size(134, 21);
            this.comboBoxWarehouseSender.TabIndex = 20;
            this.comboBoxWarehouseSender.Visible = false;
            // 
            // comboBoxMOLSender
            // 
            this.comboBoxMOLSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMOLSender.FormattingEnabled = true;
            this.comboBoxMOLSender.Location = new System.Drawing.Point(792, 106);
            this.comboBoxMOLSender.Name = "comboBoxMOLSender";
            this.comboBoxMOLSender.Size = new System.Drawing.Size(134, 21);
            this.comboBoxMOLSender.TabIndex = 21;
            this.comboBoxMOLSender.Visible = false;
            // 
            // comboBoxWarehouseReceiver
            // 
            this.comboBoxWarehouseReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWarehouseReceiver.FormattingEnabled = true;
            this.comboBoxWarehouseReceiver.Location = new System.Drawing.Point(792, 173);
            this.comboBoxWarehouseReceiver.Name = "comboBoxWarehouseReceiver";
            this.comboBoxWarehouseReceiver.Size = new System.Drawing.Size(134, 21);
            this.comboBoxWarehouseReceiver.TabIndex = 22;
            this.comboBoxWarehouseReceiver.Visible = false;
            // 
            // comboBoxMOLReceiver
            // 
            this.comboBoxMOLReceiver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMOLReceiver.FormattingEnabled = true;
            this.comboBoxMOLReceiver.Location = new System.Drawing.Point(792, 205);
            this.comboBoxMOLReceiver.Name = "comboBoxMOLReceiver";
            this.comboBoxMOLReceiver.Size = new System.Drawing.Size(134, 21);
            this.comboBoxMOLReceiver.TabIndex = 23;
            this.comboBoxMOLReceiver.Visible = false;
            // 
            // comboBoxSubdivision
            // 
            this.comboBoxSubdivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubdivision.FormattingEnabled = true;
            this.comboBoxSubdivision.Location = new System.Drawing.Point(792, 106);
            this.comboBoxSubdivision.Name = "comboBoxSubdivision";
            this.comboBoxSubdivision.Size = new System.Drawing.Size(134, 21);
            this.comboBoxSubdivision.TabIndex = 24;
            this.comboBoxSubdivision.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(696, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Подразделение:";
            this.label10.Visible = false;
            // 
            // Цена
            // 
            this.Цена.HeaderText = "Price";
            this.Цена.Name = "Цена";
            this.Цена.ReadOnly = true;
            // 
            // FormOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 497);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxSubdivision);
            this.Controls.Add(this.comboBoxMOLReceiver);
            this.Controls.Add(this.comboBoxWarehouseReceiver);
            this.Controls.Add(this.comboBoxMOLSender);
            this.Controls.Add(this.comboBoxWarehouseSender);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.comboBoxMOL);
            this.Controls.Add(this.comboBoxWarehouse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxComponents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPrice);
            this.Name = "FormOperation";
            this.Text = "FormOperation";
            this.Load += new System.EventHandler(this.FormOperation_Load);
            this.groupBoxComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxComponents;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idComponent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxWarehouse;
        private System.Windows.Forms.ComboBox comboBoxMOL;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxWarehouseSender;
        private System.Windows.Forms.ComboBox comboBoxMOLSender;
        private System.Windows.Forms.ComboBox comboBoxWarehouseReceiver;
        private System.Windows.Forms.ComboBox comboBoxMOLReceiver;
        private System.Windows.Forms.ComboBox comboBoxSubdivision;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Цена;
    }
}