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
        List<TextBox> players = new List<TextBox>();
        private int index = -1;
        private int cur_y = 65;

        public Players()
        {
            InitializeComponent();
            player_factory();
        }

        private void player_factory()
        {
            index++;
            players.Add(new TextBox());
            players[index].Location = new Point(7, cur_y);
            players[index].Size = new Size(100, 20);
            players[index].KeyDown += new KeyEventHandler(this.textBox_Enter);
            this.Controls.Add(players[index]);
            cur_y += 25;
        }

        private void ClearList(object sender, EventArgs e)
        {
            for (int i = index; i >= 1; i--)
            {
                this.Controls.Remove(players[i]);
                players.RemoveAt(i);
            }
            index = 0;
            players[0].Text = "";
            players[0].Select();
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
                    for (int i = 0; i <= index; i++)
                    {
                        sw.Write(players[i].Text + ",p" + Environment.NewLine);
                    }
                }
            }
        }

        private void textBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                player_factory();
                players[index].Select();
            }
            
        }

        private void Remove_last(object sender, EventArgs e)
        {
            if (index != 0)
            {
                this.Controls.Remove(players[index]);
                players.RemoveAt(index);
                index--;
            }
        }
    }
}
