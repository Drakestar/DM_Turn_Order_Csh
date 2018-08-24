using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Turn_order
{

    public partial class Form1 : Form
    {
        public static List<string> creature_list = new List<string>();
        public static List<Doubles> full_list = new List<Doubles>();
        public static List<Doubles> SortedList2 = new List<Doubles>();
        public static int x = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void open_encounter(object sender, EventArgs e)
        {
            Encounters win2 = new Encounters();
            win2.Show();
        }

        private void Player_Load(object sender, EventArgs e)
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }

            if (fileName != null)
            {
                //Do something with the file, for example read text from it
                label3.Text += File.ReadAllText(fileName);
            }
        }

        private void PlayerCreator(object sender, EventArgs e)
        {
            Players win2 = new Players();
            win2.Show();
        }

        private void RollorTurn(object sender, EventArgs e)
        {
            if (button2.Text == "Roll Initiative")
            {
                if (!creature_list.Any())
                {
                    creature_list.AddRange(new List<string>(Regex.Split(label3.Text, Environment.NewLine)));
                    creature_list.Remove("");
                }
                InitiativeLogger win2 = new InitiativeLogger();
                win2.Show();
                button2.Text = "Click to Start";
            }
            else if (button2.Text == "Click to Start")
            {
                label3.Text = "";
                SortedList2 = full_list.OrderBy(o => o.Initiative).ToList();
                SortedList2.Reverse();
                foreach (var items in SortedList2)
                {
                    label3.Text += items.Name;
                    label3.Text += Environment.NewLine;
                    label4.Text += items.Initiative.ToString();
                    label4.Text += Environment.NewLine;
                }
                x = 0;
                button2.Text = "Next Turn";
            }
            else
            {
                label3.Text = label3.Text.Remove(0, SortedList2[x].Name.Length);
                label3.Text = label3.Text.Trim();
                label4.Text = label4.Text.Remove(0, SortedList2[x].Initiative.ToString().Length);
                label4.Text = label4.Text.Trim();
                x++;
                if (label4.Text == "")
                {
                    button2.Text = "Roll Initiative";
                    label3.Text = "";
                }
            }
        }

        private void ClearLabels(object sender, EventArgs e)
        {
            label3.Text = "";
            label4.Text = "";
        }

        public Boolean isEmptyStringArray(String[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearCaches(object sender, EventArgs e)
        {
            button2.Text = "Roll Initiative";
            creature_list = new List<string>();
            full_list = new List<Doubles>();
            SortedList2 = new List<Doubles>();
            x = 0;
            label3.Text = "";
            label4.Text = "";
    }

        private void AddOpener(object sender, EventArgs e)
        {
            Adder win2 = new Adder();
            win2.Show();
        }
    }
    public class Doubles
    {
        
        public string Name { get; set; }
        public int Initiative { get; set; }
        public Doubles(string nam, int ago)
        {
            Name = nam;
            Initiative = ago;
        }
    }
}
