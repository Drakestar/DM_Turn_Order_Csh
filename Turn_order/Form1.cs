using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// ********** TO-DO **********
// - Add option to just roll initiative once
// - Add remembering between sessions
//   * Fighter rememberance
//   * Checkbox rememberance
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
        Button p_button = new Button();
        

        // Stats class for initiating, rolled won't start as a number so it stays 0
        private class Stats
        {
            // Name - Name of creature/character/player
            // Initiative - Bonus, for players "p" is stored which later on is caught that they are a player
            // Rolled - Used for determining actual play order
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
        // Called everytime the initiative roller finds a player, gives the name and a textbox for initiative entry
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
            player_inits[name_index].KeyPress += new KeyPressEventHandler(player_init_enter);
            this.Controls.Add(player_inits[name_index]);
            name_index++;
        }

        // Factory for "Fighter Buttons"
        // In program when the button is pressed it removes that monster/player from future initiative rolls
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
            p_button.Location = new Point(383, 300);
            p_button.Text = "Finalize Player Initiative.";
            p_button.Click += new EventHandler(player_button);
            this.Controls.Add(p_button);
            p_button.Hide();
        }

        // For creating groups of players and monsters open the pre-requisite forms and show them
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

        private void Open_Helpbox(object sender, EventArgs e)
        {
            Helpbox win2 = new Helpbox();
            win2.Show();
        }

        // AddOpener == AddButtonClick
        // On click, sends data from the text boxes above it to make a new fighter
        private void AddOpener(object sender, EventArgs e)
        {
            // They didn't put anything in the damn boxes
            if (add_name.Text == "" || add_init.Text == "") return;
            if (add_init.Text.CompareTo("p") == 0)
            {
                people.Add(add_name.Text);
            }
            int tmp;
            // Try to parse the initiative to an actual number
            int.TryParse(add_init.Text, out tmp);
            // Create the new player, then add them to the required locations
            Stats newplayer = new Stats(add_name.Text, tmp);
            fighter_stats.Add(newplayer);
            fighter_maker(newplayer.name);
            // Reset Textboxes to prevent reclicking
            
            if (ready_to_play && l_names.Any())
            {
                Label temp = new Label();
                Label init_temp = new Label();
                temp.Text = add_name.Text;
                temp.Location = new Point(l_names[l_names.Count - 1].Location.X, l_names[l_names.Count - 1].Location.Y + 25);

                init_temp.Text = add_init.Text;
                init_temp.Location = new Point(l_inits[l_inits.Count - 1].Location.X, l_inits[l_inits.Count - 1].Location.Y + 25);

                l_names.Add(temp);
                l_inits.Add(init_temp);
                this.Controls.Add(l_names[another_index]);
                this.Controls.Add(l_inits[another_index]);
                another_index++;
            }
            add_name.Text = "";
            add_init.Text = "";

        }

        // Deletes Fighter after their name button is clicked
        private void Delete_Fighter(object sender, EventArgs e)
        {
            // Remove the fighter from required lists
            Button tmp_button = (Button)sender;
            fighter_stats.Remove(fighter_stats.Find(x => x.name == tmp_button.Text));
            this.Controls.Remove((Button)sender);
            fighters.Remove(tmp_button);
            int cur_Y = 70;
            // For each fighter move them so it keeps it noice and toidy.
            foreach (Button fightyboi in fighters)
            {
                fightyboi.Location = new Point(fightyboi.Location.X, cur_Y);
                cur_Y += 25;
            }
            // We removed a fighter so there is one less...
            fighter_index--;
            // If currently rolling for initiative
            // remove the "player" name and initiative and lower name_index
        }

        // Load Players/Monsters into Fighter buttons and lists
        // Opens a dialog box to find the csv they want to use.
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

        // "Roll for Initiative" button
        private void Roll_Initiative(object sender, EventArgs e)
        {
            // If there ain't no players, ain't no one fightin'
            // If they are ready to play, they don't need to roll initiative again
            if (ready_to_play || fighters.Count <= 0 || l_names.Any()) return;
            // Create a random for enemy dice rolls
            Random r = new Random();
            foreach (Stats contestant in fighter_stats)
            {
                // Is a player
                if (people.Contains(contestant.name)) player_init_maker(contestant.name);
                // Is a monster (which checks if they rolled a crit)
                else
                {
                    // If they want to roll for monsters
                    if (MonsterRoll.Checked)
                    {
                        player_init_maker(contestant.name);
                    }
                    else
                    {
                        contestant.rolled = r.Next(1, 21) + contestant.initiative;
                    }
                    
                }
            }
            // If there are players, which if there aren't what are you even doing?
            // Creates a "finalize" button which rolls from initiative phase to actual combat
            if (name_index > 0)
            {
                p_button.Show();
            }
            else
            {
                ready_to_play = true;
                show_turn_order();
            }
            if (player_inits.Any()) player_inits[0].Select();
        }
        // "Finalize Player Initiative" Button was clicked, time to kick things off
        // Removes all player entry things, and finalize button, then shows the turn order
        private void player_button(object sender, EventArgs e)
        {
            // Remove all initiative entry texboxes/labels
            for (int i = name_index - 1; i >= 0; i--)
            {
                int tmp;
                int.TryParse(player_inits[i].Text, out tmp);
                int fighter_index = fighter_stats.FindIndex(x => x.name == player_names[i].Text);
                if (fighter_index != -1) fighter_stats[fighter_index].rolled = tmp;
                this.Controls.Remove(player_names[i]);
                this.Controls.Remove(player_inits[i]);
                player_names.RemoveAt(i);
                player_inits.RemoveAt(i);
            }
            name_index = 0;
            p_button.Hide();
            ready_to_play = true;
            show_turn_order();
            turn_button.Select();
        }

        // Makes all the behind-the-scenes initiative tracking visible
        private void show_turn_order()
        {
            int cur_y = 70;
            // Order fighter stats by rolled initative from low to high then reverse it for "actual" order
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
        // Current iteration only removes the highest on initiative and moves up other entrants
        private void Turn_Handler(object sender, EventArgs e)
        {
            if (ready_to_play)
            {
                var tmp_name = l_names[0];
                var tmp_init = l_inits[0];
                bool only_players = true;
                
                // Remove them from visibility and existence then move all other labels up
                this.Controls.Remove(l_names[0]);
                this.Controls.Remove(l_inits[0]);
                l_names.RemoveAt(0);
                l_inits.RemoveAt(0);
                // DM only wants to roll once.
                if (RollOnce.Checked)
                {
                    tmp_name.Location = new Point(l_names[l_names.Count - 1].Location.X, l_names[l_names.Count - 1].Location.Y + 25);
                    tmp_init.Location = new Point(l_inits[l_inits.Count - 1].Location.X, l_inits[l_inits.Count - 1].Location.Y + 25);
                    bool fighter_exists = false;
                    foreach (Button fighter in fighters)
                    {
                        if (fighter.Text == tmp_name.Text) fighter_exists = true;
                    }
                    if (fighter_exists)
                    {
                        l_names.Add(tmp_name);
                        l_inits.Add(tmp_init);
                        this.Controls.Add(l_names[l_names.Count - 1]);
                        this.Controls.Add(l_inits[l_inits.Count - 1]);
                    }                }
                foreach (Label x in l_names)
                {
                    x.Location = new Point(x.Location.X, x.Location.Y - 25);
                    // Do a check to see if only players are left
                    if (!people.Contains(x.Text)) only_players = false;
                }
                foreach (Label x in l_inits) x.Location = new Point(x.Location.X, x.Location.Y - 25);

                another_index--;
                if (RollOnce.Checked) another_index++;
                if (another_index <= 0 || only_players)
                {
                    ready_to_play = false;
                    initiative_button.Select();
                    if (only_players)
                    {
                        foreach (Label x in l_names) this.Controls.Remove(x);
                        foreach (Label x in l_inits) this.Controls.Remove(x);
                        l_names.Clear();
                        l_inits.Clear();
                        another_index = 0;
                    }
                }
            }
            else
            {
                initiative_button.Select();
            }
        }

        private void name_to_init_add_fighter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add_init.Select();
            }
        }

        private void add_fighter_init_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add_name.Select();
                AddOpener(Add_Fighter_Button, e);
            }
        }

        private void player_init_enter(object sender, KeyPressEventArgs e)
        {
            TextBox init_box = sender as TextBox;
            if (e.KeyChar == (char)Keys.Enter)
            {
                int tmp = player_inits.IndexOf(init_box);
                // If on the last box "press" the finalize button
                if (tmp == (name_index - 1))
                {
                    p_button.PerformClick();
                }
                // else move to next box
                else
                {
                    player_inits[tmp + 1].Select();
                }
            }

        }

        private void form_enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                turn_button.PerformClick();
            }
        }

        
    }
}
