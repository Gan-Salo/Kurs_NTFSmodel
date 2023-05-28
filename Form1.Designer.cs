
namespace WindowsFormsApp1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.filecontent_textbox = new System.Windows.Forms.TextBox();
            this.filename_label = new System.Windows.Forms.Label();
            this.chose_label = new System.Windows.Forms.Label();
            this.type_label = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.filename_textBox = new System.Windows.Forms.TextBox();
            this.okname_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mft_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clusters_label = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tomstr_label = new System.Windows.Forms.Label();
            this.readonly_checkBox = new System.Windows.Forms.CheckBox();
            this.readonly_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(338, 290);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(348, 56);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(418, 341);
            this.dataGridView2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(4, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(338, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Обновить вручную";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(772, 56);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(224, 341);
            this.treeView1.TabIndex = 3;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripTextBox1,
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(181, 95);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // filecontent_textbox
            // 
            this.filecontent_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filecontent_textbox.Location = new System.Drawing.Point(8, 46);
            this.filecontent_textbox.Multiline = true;
            this.filecontent_textbox.Name = "filecontent_textbox";
            this.filecontent_textbox.Size = new System.Drawing.Size(292, 248);
            this.filecontent_textbox.TabIndex = 4;
            // 
            // filename_label
            // 
            this.filename_label.AutoSize = true;
            this.filename_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filename_label.Location = new System.Drawing.Point(41, 411);
            this.filename_label.Name = "filename_label";
            this.filename_label.Size = new System.Drawing.Size(53, 20);
            this.filename_label.TabIndex = 5;
            this.filename_label.Text = "name:";
            // 
            // chose_label
            // 
            this.chose_label.AutoSize = true;
            this.chose_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chose_label.Location = new System.Drawing.Point(4, 13);
            this.chose_label.Name = "chose_label";
            this.chose_label.Size = new System.Drawing.Size(80, 20);
            this.chose_label.TabIndex = 6;
            this.chose_label.Text = "Выбрано:";
            // 
            // type_label
            // 
            this.type_label.AutoSize = true;
            this.type_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.type_label.Location = new System.Drawing.Point(17, 411);
            this.type_label.Name = "type_label";
            this.type_label.Size = new System.Drawing.Size(18, 20);
            this.type_label.TabIndex = 7;
            this.type_label.Text = "d";
            // 
            // save_button
            // 
            this.save_button.Enabled = false;
            this.save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_button.Location = new System.Drawing.Point(1002, 308);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(310, 38);
            this.save_button.TabIndex = 8;
            this.save_button.Text = "Сохранить содержимое";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // filename_textBox
            // 
            this.filename_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filename_textBox.Location = new System.Drawing.Point(83, 11);
            this.filename_textBox.Name = "filename_textBox";
            this.filename_textBox.Size = new System.Drawing.Size(100, 26);
            this.filename_textBox.TabIndex = 9;
            // 
            // okname_button
            // 
            this.okname_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okname_button.Location = new System.Drawing.Point(186, 11);
            this.okname_button.Name = "okname_button";
            this.okname_button.Size = new System.Drawing.Size(114, 26);
            this.okname_button.TabIndex = 10;
            this.okname_button.Text = "Переименовать";
            this.okname_button.UseVisualStyleBackColor = true;
            this.okname_button.Click += new System.EventHandler(this.okname_button_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.filecontent_textbox);
            this.panel1.Controls.Add(this.okname_button);
            this.panel1.Controls.Add(this.filename_textBox);
            this.panel1.Controls.Add(this.chose_label);
            this.panel1.Location = new System.Drawing.Point(1002, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 299);
            this.panel1.TabIndex = 11;
            // 
            // mft_label
            // 
            this.mft_label.AutoSize = true;
            this.mft_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mft_label.Location = new System.Drawing.Point(87, 14);
            this.mft_label.Name = "mft_label";
            this.mft_label.Size = new System.Drawing.Size(147, 20);
            this.mft_label.TabIndex = 12;
            this.mft_label.Text = "Содержимое MFT ";
            this.mft_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.mft_label);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 47);
            this.panel2.TabIndex = 13;
            // 
            // clusters_label
            // 
            this.clusters_label.AutoSize = true;
            this.clusters_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clusters_label.Location = new System.Drawing.Point(102, 13);
            this.clusters_label.Name = "clusters_label";
            this.clusters_label.Size = new System.Drawing.Size(191, 20);
            this.clusters_label.TabIndex = 14;
            this.clusters_label.Text = "Содержимое кластеров";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.clusters_label);
            this.panel3.Location = new System.Drawing.Point(348, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(418, 47);
            this.panel3.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tomstr_label);
            this.panel4.Location = new System.Drawing.Point(772, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(224, 47);
            this.panel4.TabIndex = 16;
            // 
            // tomstr_label
            // 
            this.tomstr_label.AutoSize = true;
            this.tomstr_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tomstr_label.Location = new System.Drawing.Point(46, 13);
            this.tomstr_label.Name = "tomstr_label";
            this.tomstr_label.Size = new System.Drawing.Size(129, 20);
            this.tomstr_label.TabIndex = 14;
            this.tomstr_label.Text = "Структура тома";
            // 
            // readonly_checkBox
            // 
            this.readonly_checkBox.AutoSize = true;
            this.readonly_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readonly_checkBox.Location = new System.Drawing.Point(1205, 364);
            this.readonly_checkBox.Name = "readonly_checkBox";
            this.readonly_checkBox.Size = new System.Drawing.Size(98, 24);
            this.readonly_checkBox.TabIndex = 17;
            this.readonly_checkBox.Text = "ReadOnly";
            this.readonly_checkBox.UseVisualStyleBackColor = true;
            // 
            // readonly_button
            // 
            this.readonly_button.Location = new System.Drawing.Point(1002, 355);
            this.readonly_button.Name = "readonly_button";
            this.readonly_button.Size = new System.Drawing.Size(197, 42);
            this.readonly_button.TabIndex = 18;
            this.readonly_button.Text = "Сохранить состояние ReadOnly";
            this.readonly_button.UseVisualStyleBackColor = true;
            this.readonly_button.Click += new System.EventHandler(this.readonly_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 440);
            this.Controls.Add(this.readonly_button);
            this.Controls.Add(this.readonly_checkBox);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.type_label);
            this.Controls.Add(this.filename_label);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.TextBox filecontent_textbox;
        private System.Windows.Forms.Label filename_label;
        private System.Windows.Forms.Label chose_label;
        private System.Windows.Forms.Label type_label;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TextBox filename_textBox;
        private System.Windows.Forms.Button okname_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mft_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label clusters_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label tomstr_label;
        private System.Windows.Forms.CheckBox readonly_checkBox;
        private System.Windows.Forms.Button readonly_button;
    }
}

