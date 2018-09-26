using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Turn_order
{

    public partial class Form1 : Form
    {
        // Stats is the basis for most of the ordering and whatnot
        private static List<Stats> fighter_stats = new List<Stats>();
        // Fighters will be derived from the overall list
        private List<Button> fighters = new List<Button>();
        // Other lists
        private List<string> people = new List<string>();
        private List<Label> player_names = new List<Label>();
        private List<TextBox> player_inits = new List<TextBox>();
        // Used for putting the players initiatives into the stats field.
        private List<Label> l_names = new List<Label>();
        private List<Label> l_inits = new List<Label>();
        // There's probably a better way, but 3 different indices anyway
        private int fighter_index = 0;
        private int name_index = 0;
        private int another_index = 0;
        private bool ready_to_play = false;
        // String for filename
        private string default_filename = "c:\\";

        // Stats class for initiating, rolled won't start as a number so it stays 0
        private class Stats
        {
            public string name = string.Empty;
            public int initiative = 0;
            public int rolled = 0;
            public Stats(string in_name, int in_init)
            {
                name = in_name;
                initiative = in_init;
            }
            
        }

        // Factory for creating the labels/textboxes for player initiatives
        private void player_init_maker(string name)
        {
            player_names.Add(new Label());
            player_names[name_index].Text = name;
            player_names[name_index].Location = new Point(145, 70 + name_index * 25);
            player_names[name_index].Size = new Size(100, 20);
            this.Controls.Add(player_names[name_index]);
            player_inits.Add(new TextBox());
            player_inits[name_index].Location = new Point(250, 70 + name_index * 25);
            player_inits[name_index].Size = new Size(100, 20);
            this.Controls.Add(player_inits[name_index]);
            name_index++;
        }

        // Factory for creating the fighter buttons
        public void fighter_maker(string name)
        {
            fighters.Add(new Button());
            fighters[fighter_index].Text = name;
            fighters[fighter_index].Location = new Point(7, 70 + fighter_index * 25);
            fighters[fighter_index].Size = new Size(100, 20);
            fighters[fighter_index].Click += new EventHandler(Delete_Fighter);
            this.Controls.Add(fighters[fighter_index]);
            fighter_index++;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void open_encounter(object sender, EventArgs e)
        {
            Encounters win2 = new Encounters();
            win2.Show();
        }

        private void PlayerCreator(object sender, EventArgs e)
        {
            Players win2 = new Players();
            win2.Show();
        }

        // Still called opener, even though it adds to fighter and stats
        // Not handling a player mid-fight, because there's no goddamn reason to
        private void AddOpener(object sender, EventArgs e)
        {
            if (add_name.Text == "" || add_init.Text == "") return;
            int tmp;
            int.TryParse(add_init.Text, out tmp);
            Stats newplayer = new Stats(add_name.Text, tmp);
            fighter_stats.Add(newplayer);
            fighter_maker(newplayer.name);
            add_name.Text = "";
            add_init.Text = "";
        }

        // Removes a fighter from the buttons and fighter_stats
        private void Delete_Fighter(object sender, EventArgs e)
        {
            Button tmp_button = (Button)sender;
            fighter_stats.Remove(fighter_stats.Find(x => x.name == tmp_button.Text));
            this.Controls.Remove((Button)sender);
            fighters.Remove(tmp_button);
            int cur_Y = 70;
            foreach (Button fightyboi in fighters)
            {
                fightyboi.Location = new Point(fightyboi.Location.X, cur_Y);
                cur_Y += 25;
            }
            fighter_index--;
        }

        // Loads players/monsters into buttons and fighter_stats
        private void Load_Fighters(object sender, EventArgs e)
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
                openFileDialog1.InitialDirectory = System.Reflection.Assembly.GetEntryAssembly().Location;
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
                default_filename = fileName;
                string[] lines = System.IO.File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] info = line.Split(',');
                    int tmp = 0;
                    if (info[1].CompareTo("p") == 0)
                    {
                        people.Add(info[0]);
                    }
                    int.TryParse(info[1], out tmp);
                    Stats newplayer = new Stats(info[0], tmp);
                    fighter_stats.Add(newplayer);
                    fighter_maker(newplayer.name);
                }
            }
        }

        private void Roll_Initiative(object sender, EventArgs e)
        {
            if (ready_to_play || fighters.Count <= 0) return;
            Random r = new Random();
            foreach (Stats contestant in fighter_stats)
            {
                // Is a player
                if (people.Contains(contestant.name)) player_init_maker(contestant.name);
                // Is a monster
                else contestant.rolled = r.Next(0, 20) + contestant.initiative;
            }
            // If there are players, which if there aren't what are you even doing?
            if (name_index > 0)
            {
                Button p_button = new Button();
                p_button.Location = new Point(383, 300);
                p_button.Text = "Finalize Player Initiative.";
                p_button.Click += new EventHandler(player_button);
                this.Controls.Add(p_button);
            }
            else
            {
                ready_to_play = true;
                show_turn_order();
            }
        }

        private void player_button(object sender, EventArgs e)
        {
            for (int i = name_index - 1; i >= 0; i--)
            {
                int tmp;
                int.TryParse(player_inits[i].Text, out tmp);
                fighter_stats[fighter_stats.FindIndex(x => x.name == player_names[i].Text)].rolled = tmp;
                this.Controls.Remove(player_names[i]);
                this.Controls.Remove(player_inits[i]);
                player_names.RemoveAt(i);
                player_inits.RemoveAt(i);
            }
            name_index = 0;
            this.Controls.Remove((Button)sender);
            ready_to_play = true;
            show_turn_order();
        }

        private void show_turn_order()
        {
            int cur_y = 70;
            // Order fighter stats by rolled initative
            List<Stats> SortedList = fighter_stats.OrderBy(o => o.rolled).ToList();
            SortedList.Reverse();
            // For each fighter create 2 labels for their initiative and name
            foreach (Stats fighter in SortedList)
            {
                Label temp = new Label();
                Label init_temp = new Label();
                temp.Text = fighter.name;
                temp.Location = new Point(145, cur_y);

                init_temp.Text = fighter.rolled.ToString();
                init_temp.Location = new Point(250, cur_y);

                l_names.Add(temp);
                l_inits.Add(init_temp);
                this.Controls.Add(l_names[another_index]);
                this.Controls.Add(l_inits[another_index]);
                another_index++;
                cur_y += 25;
            }
        }

        // Handles pressing the next turn button
        private void Turn_Handler(object sender, EventArgs e)
        {
            if (ready_to_play)
            {
                // Remove them from visibility and existence then move all other labels up
                this.Controls.Remove(l_names[0]);
                this.Controls.Remove(l_inits[0]);
                l_names.RemoveAt(0);
                l_inits.RemoveAt(0);
                foreach (Label x in l_names) x.Location = new Point(x.Location.X, x.Location.Y - 25);
                foreach (Label x in l_inits) x.Location = new Point(x.Location.X, x.Location.Y - 25);
                another_index--;
                if (another_index <= 0) ready_to_play = false;
            }
        }
    }
}
