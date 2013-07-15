namespace H3Calc
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemMode = new System.Windows.Forms.MenuItem();
            this.menuItemMode1 = new System.Windows.Forms.MenuItem();
            this.menuItemMode2 = new System.Windows.Forms.MenuItem();
            this.menuItemMode3 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.combatDamagePanel = new H3Calc.CombatDamagePanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.spellDamagePanel = new H3Calc.SpellDamagePanel();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemMode,
            this.aboutMenuItem});
            // 
            // menuItemMode
            // 
            this.menuItemMode.Index = 0;
            this.menuItemMode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemMode1,
            this.menuItemMode2,
            this.menuItemMode3});
            this.menuItemMode.Text = "Mode";
            // 
            // menuItemMode1
            // 
            this.menuItemMode1.Index = 0;
            this.menuItemMode1.RadioCheck = true;
            this.menuItemMode1.Text = "Simple";
            this.menuItemMode1.Click += new System.EventHandler(this.menuItemMode_Click);
            // 
            // menuItemMode2
            // 
            this.menuItemMode2.Index = 1;
            this.menuItemMode2.RadioCheck = true;
            this.menuItemMode2.Text = "Standard";
            this.menuItemMode2.Click += new System.EventHandler(this.menuItemMode_Click);
            // 
            // menuItemMode3
            // 
            this.menuItemMode3.Index = 2;
            this.menuItemMode3.RadioCheck = true;
            this.menuItemMode3.Text = "Scientific";
            this.menuItemMode3.Click += new System.EventHandler(this.menuItemMode_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 1;
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(797, 507);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.combatDamagePanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(789, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Combat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // combatDamagePanel
            // 
            this.combatDamagePanel.Heroes = null;
            this.combatDamagePanel.Location = new System.Drawing.Point(6, 3);
            this.combatDamagePanel.Mode = H3Calc.ApplicationMode.Scientific;
            this.combatDamagePanel.Name = "combatDamagePanel";
            this.combatDamagePanel.Size = new System.Drawing.Size(778, 475);
            this.combatDamagePanel.TabIndex = 0;
            this.combatDamagePanel.Terrains = null;
            this.combatDamagePanel.Units = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.spellDamagePanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(789, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Magic";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // spellDamagePanel
            // 
            this.spellDamagePanel.Heroes = null;
            this.spellDamagePanel.Location = new System.Drawing.Point(6, 6);
            this.spellDamagePanel.Name = "spellDamagePanel";
            this.spellDamagePanel.Size = new System.Drawing.Size(777, 327);
            this.spellDamagePanel.Spells = null;
            this.spellDamagePanel.TabIndex = 0;
            this.spellDamagePanel.Units = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 530);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heroes III Damage Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemMode1;
        private System.Windows.Forms.MenuItem menuItemMode2;
        private System.Windows.Forms.MenuItem menuItemMode3;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.MenuItem menuItemMode;
        private CombatDamagePanel combatDamagePanel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SpellDamagePanel spellDamagePanel;

    }
}

