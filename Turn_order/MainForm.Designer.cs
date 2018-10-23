namespace Turn_order
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createEncounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPlayerListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonsterRoll = new System.Windows.Forms.ToolStripMenuItem();
            this.RollOnce = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.label3 = new System.Windows.Forms.Label();
            this.initiative_button = new System.Windows.Forms.Button();
            this.turn_button = new System.Windows.Forms.Button();
            this.add_name = new System.Windows.Forms.TextBox();
            this.add_init = new System.Windows.Forms.TextBox();
            this.Add_Fighter_Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Initiative";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(471, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuBar;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createEncounterToolStripMenuItem,
            this.createPlayerListToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // createEncounterToolStripMenuItem
            // 
            this.createEncounterToolStripMenuItem.Name = "createEncounterToolStripMenuItem";
            this.createEncounterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createEncounterToolStripMenuItem.Text = "Create Encounter";
            this.createEncounterToolStripMenuItem.Click += new System.EventHandler(this.open_encounter);
            // 
            // createPlayerListToolStripMenuItem
            // 
            this.createPlayerListToolStripMenuItem.Name = "createPlayerListToolStripMenuItem";
            this.createPlayerListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createPlayerListToolStripMenuItem.Text = "Create Player List";
            this.createPlayerListToolStripMenuItem.Click += new System.EventHandler(this.PlayerCreator);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuBar;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.loadToolStripMenuItem.Text = "Load Fighters";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadFightersClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MonsterRoll,
            this.RollOnce});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // MonsterRoll
            // 
            this.MonsterRoll.CheckOnClick = true;
            this.MonsterRoll.Name = "MonsterRoll";
            this.MonsterRoll.Size = new System.Drawing.Size(164, 22);
            this.MonsterRoll.Text = "Roll for Monsters";
            // 
            // RollOnce
            // 
            this.RollOnce.CheckOnClick = true;
            this.RollOnce.Name = "RollOnce";
            this.RollOnce.Size = new System.Drawing.Size(164, 22);
            this.RollOnce.Text = "Roll Once";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.Open_Helpbox);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "In Fight";
            // 
            // initiative_button
            // 
            this.initiative_button.Location = new System.Drawing.Point(350, 192);
            this.initiative_button.Name = "initiative_button";
            this.initiative_button.Size = new System.Drawing.Size(106, 23);
            this.initiative_button.TabIndex = 8;
            this.initiative_button.Text = "Roll for Initiative!";
            this.initiative_button.UseVisualStyleBackColor = true;
            this.initiative_button.Click += new System.EventHandler(this.RollInitiativeButton);
            // 
            // turn_button
            // 
            this.turn_button.Location = new System.Drawing.Point(112, 317);
            this.turn_button.Name = "turn_button";
            this.turn_button.Size = new System.Drawing.Size(255, 62);
            this.turn_button.TabIndex = 9;
            this.turn_button.Text = "Next Turn";
            this.turn_button.UseVisualStyleBackColor = true;
            this.turn_button.Click += new System.EventHandler(this.NextTurnButtonClick);
            // 
            // add_name
            // 
            this.add_name.Location = new System.Drawing.Point(350, 75);
            this.add_name.Name = "add_name";
            this.add_name.Size = new System.Drawing.Size(100, 20);
            this.add_name.TabIndex = 10;
            this.add_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddFighterNameBoxKeyPress);
            // 
            // add_init
            // 
            this.add_init.Location = new System.Drawing.Point(350, 125);
            this.add_init.Name = "add_init";
            this.add_init.Size = new System.Drawing.Size(100, 20);
            this.add_init.TabIndex = 11;
            this.add_init.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddFighterInitBoxKeyPress);
            // 
            // Add_Fighter_Button
            // 
            this.Add_Fighter_Button.Location = new System.Drawing.Point(350, 150);
            this.Add_Fighter_Button.Name = "Add_Fighter_Button";
            this.Add_Fighter_Button.Size = new System.Drawing.Size(75, 23);
            this.Add_Fighter_Button.TabIndex = 12;
            this.Add_Fighter_Button.Text = "Add Fighter";
            this.Add_Fighter_Button.UseVisualStyleBackColor = true;
            this.Add_Fighter_Button.Click += new System.EventHandler(this.AddFighterClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Initiative";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(471, 417);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Add_Fighter_Button);
            this.Controls.Add(this.add_init);
            this.Controls.Add(this.add_name);
            this.Controls.Add(this.turn_button);
            this.Controls.Add(this.initiative_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createEncounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPlayerListToolStripMenuItem;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Button initiative_button;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button turn_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Add_Fighter_Button;
        private System.Windows.Forms.TextBox add_init;
        private System.Windows.Forms.TextBox add_name;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MonsterRoll;
        private System.Windows.Forms.ToolStripMenuItem RollOnce;
    }
}

