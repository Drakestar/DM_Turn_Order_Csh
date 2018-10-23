using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

// ********** TO-DO **********
// - Add remembering between sessions
//   * Fighter rememberance
//   * Checkbox rememberance
// - Scroll bar in encounters?
// - Able to hide adding fighter
namespace Turn_order
{

    public partial class MainForm : Form
    {
        // Y value start for programmatic boxes/buttons with y-offset
        private int y_start = 80;
        private int y_offset = 30;


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
        private Color friendly = Color.FromArgb(75, 75, 255);
        private Color enemy = Color.FromArgb(255, 125, 125);
        // String for filename
        private string default_filename = "c:\\";
        Button p_button = new Button();


        public MainForm()
        {
            InitializeComponent();

            p_button.Location = new Point(350, 220);
            p_button.Text = "Finalize Initiative";
            p_button.Size = new Size(100, 50);
            p_button.Click += new EventHandler(player_button);
            p_button.BackColor = Color.LightGreen;
            this.Controls.Add(p_button);
            p_button.Hide();
        }

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

        // ******************* Button/Label/Textbox Factories **********************

        // Factory for creating the labels/textboxes for player initiatives
        // Called everytime the initiative roller finds a player, gives the name and a textbox for initiative entry
        private void Player_init_maker(string name)
        {
            player_names.Add(new Label());
            player_names[name_index].Text = name;
            player_names[name_index].Location = new Point(145, y_start + name_index * y_offset);
            player_names[name_index].AutoSize = true;
            player_names[name_index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            player_names[name_index].BackColor = Color.LightBlue;
            this.Controls.Add(player_names[name_index]);
            player_inits.Add(new TextBox());
            player_inits[name_index].Location = new Point(240, y_start + name_index * y_offset);
            player_inits[name_index].Size = new Size(50, 20);
            player_inits[name_index].KeyDown += new KeyEventHandler(InitTextboxKeyPress);
            this.Controls.Add(player_inits[name_index]);
            name_index++;
        }

        // Factory for "Fighter Buttons"
        // In program when the button is pressed it removes that monster/player from future initiative rolls
        public void fighter_maker(string name)
        {
            fighters.Add(new Button());
            fighters[fighter_index].Text = name;
            fighters[fighter_index].Location = new Point(7, y_start + fighter_index * y_offset);
            fighters[fighter_index].Size = new Size(100, y_offset);
            fighters[fighter_index].Click += new EventHandler(FighterButtonClick);
            if (people.Contains(name))
            {
                fighters[fighter_index].BackColor = friendly;
                fighters[fighter_index].ForeColor = Color.White;
            }
            else fighters[fighter_index].BackColor = enemy;
            this.Controls.Add(fighters[fighter_index]);
            fighter_index++;
        }

        // Creates the labels relevant to the actual turn order, and next turn
        private void Initiative_label_maker(Stats fighter)
        {
            Label temp = new Label();
            Label init_temp = new Label();
            temp.Text = fighter.name;
            init_temp.Text = fighter.rolled.ToString();
            temp.Location = new Point(145, y_start + another_index * y_offset);
            init_temp.Location = new Point(250, y_start + another_index * y_offset);
            temp.BackColor = Color.LightBlue;
            init_temp.BackColor = Color.LightBlue;
            temp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            init_temp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            temp.AutoSize = true;
            init_temp.AutoSize = true;
            l_names.Add(temp);
            l_inits.Add(init_temp);
            this.Controls.Add(l_names[another_index]);
            this.Controls.Add(l_inits[another_index]);
            another_index++;

        }

        // ********************** Window Openers ************************

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



        // Makes all the behind-the-scenes initiative tracking visible
        private void show_turn_order()
        {
            // Order fighter stats by rolled initative from low to high then reverse it for "actual" order
            List<Stats> SortedFighterList = fighter_stats.OrderBy(o => o.rolled).ToList();
            SortedFighterList.Reverse();
            foreach (Stats fighter in SortedFighterList)
            {
                Initiative_label_maker(fighter);
            }
        }

        // On click, sends data from the text boxes above it to make a new fighter
        private void AddFighterClick(object sender, EventArgs e)
        {
            // Try to parse the initiative to an actual number
            int tmp;
            int.TryParse(add_init.Text, out tmp);

            // They didn't put anything in the damn boxes
            if (add_name.Text == "" || add_init.Text == "") return;
            // Added fighter is a player
            if (add_init.Text.CompareTo("p") == 0) people.Add(add_name.Text);
            
            // Create the new player, then add them to the required locations
            Stats newplayer = new Stats(add_name.Text, tmp);
            fighter_stats.Add(newplayer);
            fighter_maker(newplayer.name);
            // Reset Textboxes to prevent reclicking
            add_name.Text = "";
            add_init.Text = "";
            // If play is already underway and there are people still on the board add the fighter to initiative as well
            if (ready_to_play && l_names.Any())  Initiative_label_maker(newplayer);
        }

        // Deletes the fighter after clicking the button
        private void FighterButtonClick(object sender, EventArgs e)
        {
            // Remove the fighter from required lists
            Button tmp_button = (Button)sender;
            fighter_stats.Remove(fighter_stats.Find(x => x.name == tmp_button.Text));
            this.Controls.Remove((Button)sender);
            fighters.Remove(tmp_button);
            // For each fighter move them so it keeps it noice and toidy.
            int i = 0;
            foreach (Button fightyboi in fighters)
            {
                fightyboi.Location = new Point(fightyboi.Location.X, y_start + y_offset *i);
                i++;
            }

            // We removed a fighter so there is one less...
            fighter_index--;
            // If currently rolling for initiative
            // remove the "player" name and initiative and lower name_index
        }

        // Load Players/Monsters into Fighter buttons and lists
        // Opens a dialog box to find the csv they want to use.
        private void LoadFightersClick(object sender, EventArgs e)
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
                openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
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
                    int.TryParse(info[1], out tmp);
                    if (info[1].CompareTo("p") == 0) people.Add(info[0]);
                    Stats newplayer = new Stats(info[0], tmp);
                    fighter_stats.Add(newplayer);
                    fighter_maker(newplayer.name);
                }
            }
        }

        // "Roll for Initiative" button
        private void RollInitiativeButton(object sender, EventArgs e)
        {
            // Create a random for enemy dice rolls
            Random r = new Random();
            // If there ain't no players, ain't no one fightin'
            // If they are ready to play, they don't need to roll initiative again
            if (ready_to_play || fighters.Count <= 0 || player_inits.Any()) return;
            
            foreach (Stats contestant in fighter_stats)
            {
                // Is a player
                if (people.Contains(contestant.name)) Player_init_maker(contestant.name);
                // Is a monster (which checks if they rolled a crit)
                else
                {
                    // If they want to roll for monsters
                    if (MonsterRoll.Checked) Player_init_maker(contestant.name);
                    else contestant.rolled = r.Next(1, 21) + contestant.initiative;
                }
            }
            // If there are players, which if there aren't what are you even doing?
            // Creates a " button which rolls from initiative phase to actual combat
            if (name_index > 0) p_button.Show();
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

        

        // Handles pressing the next turn button
        // Current iteration only removes the highest on initiative and moves up other entrants
        private void NextTurnButtonClick(object sender, EventArgs e)
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
                if (RollOnce.Checked) ReplaceLabel(tmp_name, tmp_init);
                foreach (Label x in l_names)
                {
                    x.Location = new Point(x.Location.X, x.Location.Y - y_offset);
                    // Do a check to see if only players are left
                    if (RollOnce.Checked && !people.Contains(x.Text)) only_players = false;
                }
                if (!RollOnce.Checked) only_players = false;
                foreach (Label x in l_inits) x.Location = new Point(x.Location.X, x.Location.Y - y_offset);

                if (!RollOnce.Checked) another_index--;
                if (another_index <= 0 || only_players)
                {
                    ready_to_play = false;
                    initiative_button.Select();
                }
                if (only_players) ClearFightLabels();
            }
            else initiative_button.Select();
        }

        private void ClearFightLabels()
        {
            foreach (Label x in l_names) this.Controls.Remove(x);
            foreach (Label x in l_inits) this.Controls.Remove(x);
            l_names.Clear();
            l_inits.Clear();
            another_index = 0;
        }

        private void ReplaceLabel(Label tmp_name, Label tmp_init)
        {
            tmp_name.Location = new Point(l_names[l_names.Count - 1].Location.X, l_names[l_names.Count - 1].Location.Y + y_offset);
            tmp_init.Location = new Point(l_inits[l_inits.Count - 1].Location.X, l_inits[l_inits.Count - 1].Location.Y + y_offset);
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
            }
        }
        
        // ********************** Enter Keypress Checks **************************

        
        private void AddFighterNameBoxKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_init.Select();
                e.SuppressKeyPress = true;
            }
        }

        private void AddFighterInitBoxKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_name.Select();
                AddFighterClick(Add_Fighter_Button, e);
                e.SuppressKeyPress = true;
            }
        }

        private void FormKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                turn_button.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        // When the player presses enter on one of the initiative entry boxes
        private void InitTextboxKeyPress(object sender, KeyEventArgs e)
        {
            TextBox init_box = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
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
                e.SuppressKeyPress = true;
            }
        }


    }
}
