
namespace WindowsFormsApp1
{
    partial class StartForm
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
            this.tom_label = new System.Windows.Forms.Label();
            this.size_label = new System.Windows.Forms.Label();
            this.tom_textBox = new System.Windows.Forms.TextBox();
            this.size_textBox = new System.Windows.Forms.TextBox();
            this.kolvo_label = new System.Windows.Forms.Label();
            this.kolvo_textBox = new System.Windows.Forms.TextBox();
            this.newform_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tom_label
            // 
            this.tom_label.AutoSize = true;
            this.tom_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tom_label.Location = new System.Drawing.Point(19, 22);
            this.tom_label.Name = "tom_label";
            this.tom_label.Size = new System.Drawing.Size(129, 20);
            this.tom_label.TabIndex = 0;
            this.tom_label.Text = "Название тома:";
            // 
            // size_label
            // 
            this.size_label.AutoSize = true;
            this.size_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.size_label.Location = new System.Drawing.Point(19, 76);
            this.size_label.Name = "size_label";
            this.size_label.Size = new System.Drawing.Size(144, 20);
            this.size_label.TabIndex = 1;
            this.size_label.Text = "Размер кластера:";
            // 
            // tom_textBox
            // 
            this.tom_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tom_textBox.Location = new System.Drawing.Point(169, 19);
            this.tom_textBox.Name = "tom_textBox";
            this.tom_textBox.Size = new System.Drawing.Size(100, 26);
            this.tom_textBox.TabIndex = 2;
            // 
            // size_textBox
            // 
            this.size_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.size_textBox.Location = new System.Drawing.Point(169, 73);
            this.size_textBox.Name = "size_textBox";
            this.size_textBox.Size = new System.Drawing.Size(100, 26);
            this.size_textBox.TabIndex = 3;
            // 
            // kolvo_label
            // 
            this.kolvo_label.AutoSize = true;
            this.kolvo_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.kolvo_label.Location = new System.Drawing.Point(19, 131);
            this.kolvo_label.Name = "kolvo_label";
            this.kolvo_label.Size = new System.Drawing.Size(149, 20);
            this.kolvo_label.TabIndex = 4;
            this.kolvo_label.Text = "Кол-во кластеров:";
            // 
            // kolvo_textBox
            // 
            this.kolvo_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.kolvo_textBox.Location = new System.Drawing.Point(169, 128);
            this.kolvo_textBox.Name = "kolvo_textBox";
            this.kolvo_textBox.Size = new System.Drawing.Size(100, 26);
            this.kolvo_textBox.TabIndex = 5;
            // 
            // newform_button
            // 
            this.newform_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.newform_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newform_button.Location = new System.Drawing.Point(62, 181);
            this.newform_button.Name = "newform_button";
            this.newform_button.Size = new System.Drawing.Size(163, 40);
            this.newform_button.TabIndex = 6;
            this.newform_button.Text = "Создать том";
            this.newform_button.UseVisualStyleBackColor = false;
            this.newform_button.Click += new System.EventHandler(this.newform_button_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.tom_textBox);
            this.panel1.Controls.Add(this.newform_button);
            this.panel1.Controls.Add(this.tom_label);
            this.panel1.Controls.Add(this.kolvo_textBox);
            this.panel1.Controls.Add(this.size_label);
            this.panel1.Controls.Add(this.kolvo_label);
            this.panel1.Controls.Add(this.size_textBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 237);
            this.panel1.TabIndex = 7;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 261);
            this.Controls.Add(this.panel1);
            this.Name = "StartForm";
            this.Text = "Создание тома";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tom_label;
        private System.Windows.Forms.Label size_label;
        public System.Windows.Forms.TextBox tom_textBox;
        public System.Windows.Forms.TextBox size_textBox;
        private System.Windows.Forms.Label kolvo_label;
        public System.Windows.Forms.TextBox kolvo_textBox;
        private System.Windows.Forms.Button newform_button;
        private System.Windows.Forms.Panel panel1;
    }
}