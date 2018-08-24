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
    public partial class Adder : Form
    {
        public Adder()
        {
            InitializeComponent();
        }

        private void AddCreatur(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Form1.creature_list.Add(textBox1.Text);
                this.Close();
            }
        }
    }
}
