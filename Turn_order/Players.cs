using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turn_order
{
    public partial class Players : Form
    {
        public Players()
        {
            InitializeComponent();
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            string temp = textBox1.Text;
            textBox1.Text = "";
            label3.Text += temp;
            label3.Text += Environment.NewLine;
        }

        private void ClearList(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void SavePlayers(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Player Names";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK){
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile()))
                {
                    sw.Write(label3.Text);
                }
            }
        }

        private void textBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string temp = textBox1.Text;
                textBox1.Text = "";
                label3.Text += temp;
                label3.Text += Environment.NewLine;
            }
            
        }
    }
}
