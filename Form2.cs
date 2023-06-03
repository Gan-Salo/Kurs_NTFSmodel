using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            
        }

        private void newform_button_Click(object sender, EventArgs e)
        {
            
            string tom_name = tom_textBox.Text;
            int size_cluster;
            int kolvo_cluster;

            if (string.IsNullOrEmpty(tom_name) || string.IsNullOrEmpty(size_textBox.Text) || string.IsNullOrEmpty(kolvo_textBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return; // Прерываем выполнение кода
            }

            if (!int.TryParse(size_textBox.Text, out size_cluster) || !int.TryParse(kolvo_textBox.Text, out kolvo_cluster) || int.Parse(kolvo_textBox.Text) < 0 || int.Parse(size_textBox.Text) < 0)
            {
                
                MessageBox.Show("Некорректный формат введенных данных");
                return; // Прерываем выполнение кода
            }


            Form1 secondForm = new Form1(tom_name, size_cluster, kolvo_cluster);
            secondForm.ShowDialog();
        }

    }
}
