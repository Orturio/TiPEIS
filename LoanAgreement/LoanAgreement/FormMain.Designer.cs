
namespace LoanAgreement
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.контрагентызаимодавцыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.договорыЗаймаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мОЛToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поставщикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналПроводокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ведомостьРасходовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelName = new System.Windows.Forms.Label();
            this.оборотносальдоваяВедомостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.журналОперацийToolStripMenuItem,
            this.журналПроводокToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.оборотносальдоваяВедомостьToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(886, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.контрагентызаимодавцыToolStripMenuItem,
            this.договорыЗаймаToolStripMenuItem,
            this.мОЛToolStripMenuItem,
            this.поставщикиToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // контрагентызаимодавцыToolStripMenuItem
            // 
            this.контрагентызаимодавцыToolStripMenuItem.Name = "контрагентызаимодавцыToolStripMenuItem";
            this.контрагентызаимодавцыToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.контрагентызаимодавцыToolStripMenuItem.Text = "Материлы";
            this.контрагентызаимодавцыToolStripMenuItem.Click += new System.EventHandler(this.материалыToolStripMenuItem_Click);
            // 
            // договорыЗаймаToolStripMenuItem
            // 
            this.договорыЗаймаToolStripMenuItem.Name = "договорыЗаймаToolStripMenuItem";
            this.договорыЗаймаToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.договорыЗаймаToolStripMenuItem.Text = "Склады";
            this.договорыЗаймаToolStripMenuItem.Click += new System.EventHandler(this.договорыЗаймаToolStripMenuItem_Click);
            // 
            // мОЛToolStripMenuItem
            // 
            this.мОЛToolStripMenuItem.Name = "мОЛToolStripMenuItem";
            this.мОЛToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.мОЛToolStripMenuItem.Text = "МОЛ";
            this.мОЛToolStripMenuItem.Click += new System.EventHandler(this.мОЛToolStripMenuItem_Click);
            // 
            // поставщикиToolStripMenuItem
            // 
            this.поставщикиToolStripMenuItem.Name = "поставщикиToolStripMenuItem";
            this.поставщикиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.поставщикиToolStripMenuItem.Text = "Поставщики";
            this.поставщикиToolStripMenuItem.Click += new System.EventHandler(this.поставщикиToolStripMenuItem_Click);
            // 
            // журналОперацийToolStripMenuItem
            // 
            this.журналОперацийToolStripMenuItem.Name = "журналОперацийToolStripMenuItem";
            this.журналОперацийToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.журналОперацийToolStripMenuItem.Text = "Журнал операций";
            this.журналОперацийToolStripMenuItem.Click += new System.EventHandler(this.журналОперацийToolStripMenuItem_Click);
            // 
            // журналПроводокToolStripMenuItem
            // 
            this.журналПроводокToolStripMenuItem.Name = "журналПроводокToolStripMenuItem";
            this.журналПроводокToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.журналПроводокToolStripMenuItem.Text = "Журнал проводок";
            this.журналПроводокToolStripMenuItem.Click += new System.EventHandler(this.журналПроводокToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ведомостьРасходовToolStripMenuItem,
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem,
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // ведомостьРасходовToolStripMenuItem
            // 
            this.ведомостьРасходовToolStripMenuItem.Name = "ведомостьРасходовToolStripMenuItem";
            this.ведомостьРасходовToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.ведомостьРасходовToolStripMenuItem.Text = "Ведомость поступления материалов на склад";
            this.ведомостьРасходовToolStripMenuItem.Click += new System.EventHandler(this.ведомостьРасходовToolStripMenuItem_Click);
            // 
            // ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem
            // 
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem.Name = "ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem";
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem.Text = "Ведомость остатков на складе";
            this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem.Click += new System.EventHandler(this.ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem_Click);
            // 
            // ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem
            // 
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem.Name = "ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem";
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem.Text = "Ведомость отпуска материалов в производство";
            this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem.Click += new System.EventHandler(this.ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 53);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(886, 465);
            this.dataGridView.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(379, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(119, 22);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "План счетов";
            // 
            // оборотносальдоваяВедомостьToolStripMenuItem
            // 
            this.оборотносальдоваяВедомостьToolStripMenuItem.Name = "оборотносальдоваяВедомостьToolStripMenuItem";
            this.оборотносальдоваяВедомостьToolStripMenuItem.Size = new System.Drawing.Size(197, 20);
            this.оборотносальдоваяВедомостьToolStripMenuItem.Text = "Оборотно-сальдовая ведомость";
            this.оборотносальдоваяВедомостьToolStripMenuItem.Click += new System.EventHandler(this.оборотносальдоваяВедомостьToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 518);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "Главная форма";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem контрагентызаимодавцыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem договорыЗаймаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналОперацийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ведомостьРасходовToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ToolStripMenuItem ведомостьСуммПолученныхЗаймовЗаПериодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мОЛToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поставщикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ведомостьОтпускаМатериаловВПроизводствоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборотносальдоваяВедомостьToolStripMenuItem;
    }
}

