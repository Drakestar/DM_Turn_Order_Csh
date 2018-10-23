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
    public partial class Encounters : Form
    {
        List<TextBox> enemies = new List<TextBox>();
        List<TextBox> inits = new List<TextBox>();
        private int index = -1;
        private int cur_y = 65;

        public Encounters()
        {
            InitializeComponent();
            textbox_factory();
        }

        private void textbox_factory()
        {
            index++;
            enemies.Add(new TextBox());
            inits.Add(new TextBox());
            enemies[index].Location = new Point(7, cur_y);
            inits[index].Location = new Point(113, cur_y);
            enemies[index].Size = new Size(100, 20);
            inits[index].Size = new Size(100, 20);
            enemies[index].KeyDown += new KeyEventHandler(this.SwitchtoInit);
            inits[index].KeyDown += new KeyEventHandler(this.InitiativeEnter);
            this.Controls.Add(enemies[index]);
            this.Controls.Add(inits[index]);
            cur_y += 25;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ClearClick(object sender, EventArgs e)
        {
            for (int i = index; i >= 1; i--)
            {
                this.Controls.Remove(enemies[i]);
                this.Controls.Remove(inits[i]);
                enemies.RemoveAt(i);
                inits.RemoveAt(i);
            }
            index = 0;
            enemies[0].Text = "";
            inits[0].Text = "";
            enemies[0].Select();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Title = "Save Encounter";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile()))
                {
                    Console.WriteLine(index);
                    for (int i = 0; i <= index; i++)
                    {
                        sw.Write(enemies[i].Text + "," + inits[i].Text + "\r\n");
                    }
                }
            }
        }

        private void InitiativeEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_factory();
                enemies[index].Select();
                e.SuppressKeyPress = true;
            }
            
        }

        private void SwitchtoInit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                inits[index].Select();
                e.SuppressKeyPress = true;
            }
        }

        private void remove_last(object sender, EventArgs e)
        {
            if (index != 0)
            { 
                this.Controls.Remove(enemies[index]);
                this.Controls.Remove(inits[index]);
                enemies.RemoveAt(index);
                inits.RemoveAt(index);
                index--;
            }
        }
    }
}
