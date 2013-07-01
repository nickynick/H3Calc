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
            this.combatDamagePanel = new H3Calc.CombatDamagePanel();
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
            // combatDamagePanel
            // 
            this.combatDamagePanel.Heroes = null;
            this.combatDamagePanel.Location = new System.Drawing.Point(9, 12);
            this.combatDamagePanel.Mode = H3Calc.ApplicationMode.Scientific;
            this.combatDamagePanel.Name = "combatDamagePanel";
            this.combatDamagePanel.Size = new System.Drawing.Size(778, 475);
            this.combatDamagePanel.TabIndex = 0;
            this.combatDamagePanel.Terrains = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 500);
            this.Controls.Add(this.combatDamagePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heroes III Damage Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
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

    }
}

