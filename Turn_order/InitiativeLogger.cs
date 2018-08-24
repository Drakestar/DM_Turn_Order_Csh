using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turn_order
{
    public partial class InitiativeLogger : Form
    {
        private static int i = 0;
        public InitiativeLogger()
        {
            InitializeComponent();
        }

        private void LoadRequiredInits(object sender, EventArgs e)
        {
            i = 0;
            Form1.full_list.Clear();
            label4.Text = Form1.creature_list[i];
        }

        private void Logged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Log the creature and it's initiative
                Doubles temp = new Doubles("", 0);
                temp.Name = label4.Text;
                if (textBox1.Text == "") textBox1.Text = "1";
                temp.Initiative = Int32.Parse(textBox1.Text);
                // If the initiative is 50 (hopefully impossible) remove it
                if (temp.Initiative != 50)
                {
                    Form1.full_list.Add(temp);
                }
                else { Form1.creature_list.RemoveAt(i); i--; }
                // Replace name and reset iniative
                i++;
                if (i == Form1.creature_list.Count)
                {
                    this.Close();
                }
                else
                {
                    label4.Text = Form1.creature_list[i];
                    textBox1.Text = "";
                }
                
                // If at the end of list leave logger
                
            }
        }
    }
}
