namespace H3Calc
{
    partial class CombatDamagePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.swapBtn = new System.Windows.Forms.Button();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.retKillsLbl = new System.Windows.Forms.Label();
            this.calculatedRetDamageLbl = new System.Windows.Forms.Label();
            this.retNotesLbl = new System.Windows.Forms.Label();
            this.retDamageLbl = new System.Windows.Forms.Label();
            this.calculatedRetKillsLbl = new System.Windows.Forms.Label();
            this.killsLbl = new System.Windows.Forms.Label();
            this.calculatedDamageLbl = new System.Windows.Forms.Label();
            this.notesLbl = new System.Windows.Forms.Label();
            this.damageLbl = new System.Windows.Forms.Label();
            this.calculatedKillsLbl = new System.Windows.Forms.Label();
            this.terrainGroupBox = new System.Windows.Forms.GroupBox();
            this.terrainComboBox = new System.Windows.Forms.ComboBox();
            this.defenderGroupBox = new System.Windows.Forms.GroupBox();
            this.defenderPickPanel = new H3Calc.PickPanel();
            this.attackerGroupBox = new System.Windows.Forms.GroupBox();
            this.attackerPickPanel = new H3Calc.PickPanel();
            this.defenderCountUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerCountUpDn = new H3Calc.ImmediateNumericUpDown();
            this.resultPanel.SuspendLayout();
            this.terrainGroupBox.SuspendLayout();
            this.defenderGroupBox.SuspendLayout();
            this.attackerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defenderCountUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).BeginInit();
            this.SuspendLayout();
            // 
            // swapBtn
            // 
            this.swapBtn.Image = global::H3Calc.Properties.Resources.swap_icon;
            this.swapBtn.Location = new System.Drawing.Point(372, 21);
            this.swapBtn.Name = "swapBtn";
            this.swapBtn.Size = new System.Drawing.Size(34, 32);
            this.swapBtn.TabIndex = 24;
            this.swapBtn.UseVisualStyleBackColor = true;
            this.swapBtn.Click += new System.EventHandler(this.swapBtn_Click);
            // 
            // resultPanel
            // 
            this.resultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resultPanel.Controls.Add(this.retKillsLbl);
            this.resultPanel.Controls.Add(this.calculatedRetDamageLbl);
            this.resultPanel.Controls.Add(this.retNotesLbl);
            this.resultPanel.Controls.Add(this.retDamageLbl);
            this.resultPanel.Controls.Add(this.calculatedRetKillsLbl);
            this.resultPanel.Controls.Add(this.killsLbl);
            this.resultPanel.Controls.Add(this.calculatedDamageLbl);
            this.resultPanel.Controls.Add(this.notesLbl);
            this.resultPanel.Controls.Add(this.damageLbl);
            this.resultPanel.Controls.Add(this.calculatedKillsLbl);
            this.resultPanel.Location = new System.Drawing.Point(59, 424);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(507, 55);
            this.resultPanel.TabIndex = 22;
            // 
            // retKillsLbl
            // 
            this.retKillsLbl.AutoSize = true;
            this.retKillsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.retKillsLbl.Location = new System.Drawing.Point(191, 33);
            this.retKillsLbl.Name = "retKillsLbl";
            this.retKillsLbl.Size = new System.Drawing.Size(61, 13);
            this.retKillsLbl.TabIndex = 15;
            this.retKillsLbl.Text = "Ret. kills:";
            // 
            // calculatedRetDamageLbl
            // 
            this.calculatedRetDamageLbl.AutoSize = true;
            this.calculatedRetDamageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedRetDamageLbl.Location = new System.Drawing.Point(87, 33);
            this.calculatedRetDamageLbl.Name = "calculatedRetDamageLbl";
            this.calculatedRetDamageLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedRetDamageLbl.TabIndex = 13;
            this.calculatedRetDamageLbl.Text = "123";
            // 
            // retNotesLbl
            // 
            this.retNotesLbl.AutoSize = true;
            this.retNotesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.retNotesLbl.Location = new System.Drawing.Point(351, 33);
            this.retNotesLbl.Name = "retNotesLbl";
            this.retNotesLbl.Size = new System.Drawing.Size(35, 13);
            this.retNotesLbl.TabIndex = 17;
            this.retNotesLbl.Text = "blabla";
            // 
            // retDamageLbl
            // 
            this.retDamageLbl.AutoSize = true;
            this.retDamageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.retDamageLbl.Location = new System.Drawing.Point(7, 33);
            this.retDamageLbl.Name = "retDamageLbl";
            this.retDamageLbl.Size = new System.Drawing.Size(83, 13);
            this.retDamageLbl.TabIndex = 14;
            this.retDamageLbl.Text = "Ret. damage:";
            // 
            // calculatedRetKillsLbl
            // 
            this.calculatedRetKillsLbl.AutoSize = true;
            this.calculatedRetKillsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedRetKillsLbl.Location = new System.Drawing.Point(249, 33);
            this.calculatedRetKillsLbl.Name = "calculatedRetKillsLbl";
            this.calculatedRetKillsLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedRetKillsLbl.TabIndex = 16;
            this.calculatedRetKillsLbl.Text = "123";
            // 
            // killsLbl
            // 
            this.killsLbl.AutoSize = true;
            this.killsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.killsLbl.Location = new System.Drawing.Point(191, 9);
            this.killsLbl.Name = "killsLbl";
            this.killsLbl.Size = new System.Drawing.Size(34, 13);
            this.killsLbl.TabIndex = 10;
            this.killsLbl.Text = "Kills:";
            // 
            // calculatedDamageLbl
            // 
            this.calculatedDamageLbl.AutoSize = true;
            this.calculatedDamageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedDamageLbl.Location = new System.Drawing.Point(61, 9);
            this.calculatedDamageLbl.Name = "calculatedDamageLbl";
            this.calculatedDamageLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedDamageLbl.TabIndex = 8;
            this.calculatedDamageLbl.Text = "123";
            // 
            // notesLbl
            // 
            this.notesLbl.AutoSize = true;
            this.notesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.notesLbl.Location = new System.Drawing.Point(351, 9);
            this.notesLbl.Name = "notesLbl";
            this.notesLbl.Size = new System.Drawing.Size(35, 13);
            this.notesLbl.TabIndex = 12;
            this.notesLbl.Text = "blabla";
            // 
            // damageLbl
            // 
            this.damageLbl.AutoSize = true;
            this.damageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.damageLbl.Location = new System.Drawing.Point(7, 9);
            this.damageLbl.Name = "damageLbl";
            this.damageLbl.Size = new System.Drawing.Size(57, 13);
            this.damageLbl.TabIndex = 9;
            this.damageLbl.Text = "Damage:";
            // 
            // calculatedKillsLbl
            // 
            this.calculatedKillsLbl.AutoSize = true;
            this.calculatedKillsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedKillsLbl.Location = new System.Drawing.Point(222, 9);
            this.calculatedKillsLbl.Name = "calculatedKillsLbl";
            this.calculatedKillsLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedKillsLbl.TabIndex = 11;
            this.calculatedKillsLbl.Text = "123";
            // 
            // terrainGroupBox
            // 
            this.terrainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.terrainGroupBox.Controls.Add(this.terrainComboBox);
            this.terrainGroupBox.Location = new System.Drawing.Point(577, 424);
            this.terrainGroupBox.Name = "terrainGroupBox";
            this.terrainGroupBox.Size = new System.Drawing.Size(142, 51);
            this.terrainGroupBox.TabIndex = 21;
            this.terrainGroupBox.TabStop = false;
            this.terrainGroupBox.Text = "Terrain";
            // 
            // terrainComboBox
            // 
            this.terrainComboBox.DropDownHeight = 120;
            this.terrainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.terrainComboBox.FormattingEnabled = true;
            this.terrainComboBox.IntegralHeight = false;
            this.terrainComboBox.Location = new System.Drawing.Point(6, 19);
            this.terrainComboBox.Name = "terrainComboBox";
            this.terrainComboBox.Size = new System.Drawing.Size(130, 21);
            this.terrainComboBox.TabIndex = 36;
            // 
            // defenderGroupBox
            // 
            this.defenderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenderGroupBox.Controls.Add(this.defenderPickPanel);
            this.defenderGroupBox.Location = new System.Drawing.Point(412, 3);
            this.defenderGroupBox.Name = "defenderGroupBox";
            this.defenderGroupBox.Size = new System.Drawing.Size(307, 415);
            this.defenderGroupBox.TabIndex = 20;
            this.defenderGroupBox.TabStop = false;
            this.defenderGroupBox.Text = "Defender";
            // 
            // defenderPickPanel
            // 
            this.defenderPickPanel.Heroes = null;
            this.defenderPickPanel.Location = new System.Drawing.Point(6, 19);
            this.defenderPickPanel.Mode = H3Calc.ApplicationMode.Scientific;
            this.defenderPickPanel.Name = "defenderPickPanel";
            this.defenderPickPanel.OpponentHeroStats = null;
            this.defenderPickPanel.Size = new System.Drawing.Size(295, 387);
            this.defenderPickPanel.TabIndex = 2;
            // 
            // attackerGroupBox
            // 
            this.attackerGroupBox.Controls.Add(this.attackerPickPanel);
            this.attackerGroupBox.Location = new System.Drawing.Point(59, 3);
            this.attackerGroupBox.Name = "attackerGroupBox";
            this.attackerGroupBox.Size = new System.Drawing.Size(307, 415);
            this.attackerGroupBox.TabIndex = 19;
            this.attackerGroupBox.TabStop = false;
            this.attackerGroupBox.Text = "Attacker";
            // 
            // attackerPickPanel
            // 
            this.attackerPickPanel.Heroes = null;
            this.attackerPickPanel.Location = new System.Drawing.Point(6, 19);
            this.attackerPickPanel.Mode = H3Calc.ApplicationMode.Scientific;
            this.attackerPickPanel.Name = "attackerPickPanel";
            this.attackerPickPanel.OpponentHeroStats = null;
            this.attackerPickPanel.Size = new System.Drawing.Size(295, 387);
            this.attackerPickPanel.TabIndex = 0;
            // 
            // defenderCountUpDn
            // 
            this.defenderCountUpDn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenderCountUpDn.Location = new System.Drawing.Point(725, 27);
            this.defenderCountUpDn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.defenderCountUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.defenderCountUpDn.Name = "defenderCountUpDn";
            this.defenderCountUpDn.Size = new System.Drawing.Size(50, 20);
            this.defenderCountUpDn.TabIndex = 23;
            this.defenderCountUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // attackerCountUpDn
            // 
            this.attackerCountUpDn.Location = new System.Drawing.Point(3, 27);
            this.attackerCountUpDn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.attackerCountUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attackerCountUpDn.Name = "attackerCountUpDn";
            this.attackerCountUpDn.Size = new System.Drawing.Size(50, 20);
            this.attackerCountUpDn.TabIndex = 18;
            this.attackerCountUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CombatDamagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.swapBtn);
            this.Controls.Add(this.defenderCountUpDn);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.terrainGroupBox);
            this.Controls.Add(this.defenderGroupBox);
            this.Controls.Add(this.attackerGroupBox);
            this.Controls.Add(this.attackerCountUpDn);
            this.Name = "CombatDamagePanel";
            this.Size = new System.Drawing.Size(778, 482);
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.terrainGroupBox.ResumeLayout(false);
            this.defenderGroupBox.ResumeLayout(false);
            this.attackerGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defenderCountUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button swapBtn;
        private ImmediateNumericUpDown defenderCountUpDn;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Label retKillsLbl;
        private System.Windows.Forms.Label calculatedRetDamageLbl;
        private System.Windows.Forms.Label retNotesLbl;
        private System.Windows.Forms.Label retDamageLbl;
        private System.Windows.Forms.Label calculatedRetKillsLbl;
        private System.Windows.Forms.Label killsLbl;
        private System.Windows.Forms.Label calculatedDamageLbl;
        private System.Windows.Forms.Label notesLbl;
        private System.Windows.Forms.Label damageLbl;
        private System.Windows.Forms.Label calculatedKillsLbl;
        private System.Windows.Forms.GroupBox terrainGroupBox;
        private System.Windows.Forms.ComboBox terrainComboBox;
        private System.Windows.Forms.GroupBox defenderGroupBox;
        private PickPanel defenderPickPanel;
        private System.Windows.Forms.GroupBox attackerGroupBox;
        private PickPanel attackerPickPanel;
        private ImmediateNumericUpDown attackerCountUpDn;

    }
}
