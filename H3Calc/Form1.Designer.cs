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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.attackerComboBox = new System.Windows.Forms.ComboBox();
            this.defenderComboBox = new System.Windows.Forms.ComboBox();
            this.attackerGroupBox = new System.Windows.Forms.GroupBox();
            this.attackerHeroSecondarySkillSpecialistChbx = new System.Windows.Forms.CheckBox();
            this.attackerHeroSecondarySkillComboBox = new System.Windows.Forms.ComboBox();
            this.attackerHeroSecondarySkillLbl = new System.Windows.Forms.Label();
            this.attackerHasHeroChbx = new System.Windows.Forms.CheckBox();
            this.attackerHeroAttackLbl = new System.Windows.Forms.Label();
            this.defenderHeroDefenseLbl = new System.Windows.Forms.Label();
            this.defenderGroupBox = new System.Windows.Forms.GroupBox();
            this.defenderHeroSecondarySkillComboBox = new System.Windows.Forms.ComboBox();
            this.defenderHeroSecondarySkillSpecialistChbx = new System.Windows.Forms.CheckBox();
            this.defenderHeroSecondarySkillLbl = new System.Windows.Forms.Label();
            this.defenderHasHeroChbx = new System.Windows.Forms.CheckBox();
            this.terrainComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.calculatedDamageLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calculatedKillsLbl = new System.Windows.Forms.Label();
            this.notesLbl = new System.Windows.Forms.Label();
            this.attackerHeroLevelLbl = new System.Windows.Forms.Label();
            this.defenderHeroLevelLbl = new System.Windows.Forms.Label();
            this.defenderHeroLevelUpDn = new H3Calc.ImmediateNumericUpDown();
            this.defenderHeroDefenseUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerHeroLevelUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerHeroAttackUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerCountUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerGroupBox.SuspendLayout();
            this.defenderGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defenderHeroLevelUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defenderHeroDefenseUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHeroLevelUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHeroAttackUpDn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).BeginInit();
            this.SuspendLayout();
            // 
            // attackerComboBox
            // 
            this.attackerComboBox.DropDownHeight = 500;
            this.attackerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerComboBox.FormattingEnabled = true;
            this.attackerComboBox.IntegralHeight = false;
            this.attackerComboBox.Location = new System.Drawing.Point(62, 19);
            this.attackerComboBox.Name = "attackerComboBox";
            this.attackerComboBox.Size = new System.Drawing.Size(195, 21);
            this.attackerComboBox.TabIndex = 1;
            this.attackerComboBox.SelectedIndexChanged += new System.EventHandler(this.attackerComboBox_SelectedIndexChanged);
            // 
            // defenderComboBox
            // 
            this.defenderComboBox.DropDownHeight = 500;
            this.defenderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defenderComboBox.FormattingEnabled = true;
            this.defenderComboBox.IntegralHeight = false;
            this.defenderComboBox.Location = new System.Drawing.Point(6, 20);
            this.defenderComboBox.Name = "defenderComboBox";
            this.defenderComboBox.Size = new System.Drawing.Size(195, 21);
            this.defenderComboBox.TabIndex = 5;
            this.defenderComboBox.SelectedIndexChanged += new System.EventHandler(this.defenderComboBox_SelectedIndexChanged);
            // 
            // attackerGroupBox
            // 
            this.attackerGroupBox.Controls.Add(this.attackerHeroLevelLbl);
            this.attackerGroupBox.Controls.Add(this.attackerHeroLevelUpDn);
            this.attackerGroupBox.Controls.Add(this.attackerHeroSecondarySkillSpecialistChbx);
            this.attackerGroupBox.Controls.Add(this.attackerHeroAttackUpDn);
            this.attackerGroupBox.Controls.Add(this.attackerCountUpDn);
            this.attackerGroupBox.Controls.Add(this.attackerHeroSecondarySkillComboBox);
            this.attackerGroupBox.Controls.Add(this.attackerHeroSecondarySkillLbl);
            this.attackerGroupBox.Controls.Add(this.attackerHasHeroChbx);
            this.attackerGroupBox.Controls.Add(this.attackerHeroAttackLbl);
            this.attackerGroupBox.Controls.Add(this.attackerComboBox);
            this.attackerGroupBox.Location = new System.Drawing.Point(12, 12);
            this.attackerGroupBox.Name = "attackerGroupBox";
            this.attackerGroupBox.Size = new System.Drawing.Size(263, 159);
            this.attackerGroupBox.TabIndex = 5;
            this.attackerGroupBox.TabStop = false;
            this.attackerGroupBox.Text = "Attacker";
            // 
            // attackerHeroSecondarySkillSpecialistChbx
            // 
            this.attackerHeroSecondarySkillSpecialistChbx.AutoSize = true;
            this.attackerHeroSecondarySkillSpecialistChbx.Location = new System.Drawing.Point(56, 130);
            this.attackerHeroSecondarySkillSpecialistChbx.Name = "attackerHeroSecondarySkillSpecialistChbx";
            this.attackerHeroSecondarySkillSpecialistChbx.Size = new System.Drawing.Size(71, 17);
            this.attackerHeroSecondarySkillSpecialistChbx.TabIndex = 8;
            this.attackerHeroSecondarySkillSpecialistChbx.Text = "Specialist";
            this.attackerHeroSecondarySkillSpecialistChbx.UseVisualStyleBackColor = true;
            this.attackerHeroSecondarySkillSpecialistChbx.CheckedChanged += new System.EventHandler(this.attackerHeroSecondarySkillSpecialistChbx_CheckedChanged);
            // 
            // attackerHeroSecondarySkillComboBox
            // 
            this.attackerHeroSecondarySkillComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerHeroSecondarySkillComboBox.FormattingEnabled = true;
            this.attackerHeroSecondarySkillComboBox.Location = new System.Drawing.Point(56, 103);
            this.attackerHeroSecondarySkillComboBox.Name = "attackerHeroSecondarySkillComboBox";
            this.attackerHeroSecondarySkillComboBox.Size = new System.Drawing.Size(145, 21);
            this.attackerHeroSecondarySkillComboBox.TabIndex = 4;
            this.attackerHeroSecondarySkillComboBox.SelectedIndexChanged += new System.EventHandler(this.attackerHeroSecondarySkillComboBox_SelectedIndexChanged);
            // 
            // attackerHeroSecondarySkillLbl
            // 
            this.attackerHeroSecondarySkillLbl.AutoSize = true;
            this.attackerHeroSecondarySkillLbl.Location = new System.Drawing.Point(3, 106);
            this.attackerHeroSecondarySkillLbl.Name = "attackerHeroSecondarySkillLbl";
            this.attackerHeroSecondarySkillLbl.Size = new System.Drawing.Size(44, 13);
            this.attackerHeroSecondarySkillLbl.TabIndex = 7;
            this.attackerHeroSecondarySkillLbl.Text = "Offense";
            // 
            // attackerHasHeroChbx
            // 
            this.attackerHasHeroChbx.AutoSize = true;
            this.attackerHasHeroChbx.Location = new System.Drawing.Point(6, 55);
            this.attackerHasHeroChbx.Name = "attackerHasHeroChbx";
            this.attackerHasHeroChbx.Size = new System.Drawing.Size(69, 17);
            this.attackerHasHeroChbx.TabIndex = 2;
            this.attackerHasHeroChbx.Text = "Has hero";
            this.attackerHasHeroChbx.UseVisualStyleBackColor = true;
            this.attackerHasHeroChbx.CheckedChanged += new System.EventHandler(this.attackerHasHeroChbx_CheckedChanged);
            // 
            // attackerHeroAttackLbl
            // 
            this.attackerHeroAttackLbl.AutoSize = true;
            this.attackerHeroAttackLbl.Location = new System.Drawing.Point(3, 79);
            this.attackerHeroAttackLbl.Name = "attackerHeroAttackLbl";
            this.attackerHeroAttackLbl.Size = new System.Drawing.Size(38, 13);
            this.attackerHeroAttackLbl.TabIndex = 2;
            this.attackerHeroAttackLbl.Text = "Attack";
            // 
            // defenderHeroDefenseLbl
            // 
            this.defenderHeroDefenseLbl.AutoSize = true;
            this.defenderHeroDefenseLbl.Location = new System.Drawing.Point(3, 79);
            this.defenderHeroDefenseLbl.Name = "defenderHeroDefenseLbl";
            this.defenderHeroDefenseLbl.Size = new System.Drawing.Size(47, 13);
            this.defenderHeroDefenseLbl.TabIndex = 5;
            this.defenderHeroDefenseLbl.Text = "Defense";
            // 
            // defenderGroupBox
            // 
            this.defenderGroupBox.Controls.Add(this.defenderHeroLevelLbl);
            this.defenderGroupBox.Controls.Add(this.defenderHeroLevelUpDn);
            this.defenderGroupBox.Controls.Add(this.defenderHeroSecondarySkillComboBox);
            this.defenderGroupBox.Controls.Add(this.defenderHeroSecondarySkillSpecialistChbx);
            this.defenderGroupBox.Controls.Add(this.defenderHeroSecondarySkillLbl);
            this.defenderGroupBox.Controls.Add(this.defenderHasHeroChbx);
            this.defenderGroupBox.Controls.Add(this.defenderComboBox);
            this.defenderGroupBox.Controls.Add(this.defenderHeroDefenseLbl);
            this.defenderGroupBox.Controls.Add(this.defenderHeroDefenseUpDn);
            this.defenderGroupBox.Location = new System.Drawing.Point(281, 12);
            this.defenderGroupBox.Name = "defenderGroupBox";
            this.defenderGroupBox.Size = new System.Drawing.Size(207, 159);
            this.defenderGroupBox.TabIndex = 6;
            this.defenderGroupBox.TabStop = false;
            this.defenderGroupBox.Text = "Defender";
            // 
            // defenderHeroSecondarySkillComboBox
            // 
            this.defenderHeroSecondarySkillComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defenderHeroSecondarySkillComboBox.FormattingEnabled = true;
            this.defenderHeroSecondarySkillComboBox.Location = new System.Drawing.Point(56, 103);
            this.defenderHeroSecondarySkillComboBox.Name = "defenderHeroSecondarySkillComboBox";
            this.defenderHeroSecondarySkillComboBox.Size = new System.Drawing.Size(145, 21);
            this.defenderHeroSecondarySkillComboBox.TabIndex = 8;
            this.defenderHeroSecondarySkillComboBox.SelectedIndexChanged += new System.EventHandler(this.defenderHeroSecondarySkillComboBox_SelectedIndexChanged);
            // 
            // defenderHeroSecondarySkillSpecialistChbx
            // 
            this.defenderHeroSecondarySkillSpecialistChbx.AutoSize = true;
            this.defenderHeroSecondarySkillSpecialistChbx.Location = new System.Drawing.Point(56, 130);
            this.defenderHeroSecondarySkillSpecialistChbx.Name = "defenderHeroSecondarySkillSpecialistChbx";
            this.defenderHeroSecondarySkillSpecialistChbx.Size = new System.Drawing.Size(71, 17);
            this.defenderHeroSecondarySkillSpecialistChbx.TabIndex = 15;
            this.defenderHeroSecondarySkillSpecialistChbx.Text = "Specialist";
            this.defenderHeroSecondarySkillSpecialistChbx.UseVisualStyleBackColor = true;
            this.defenderHeroSecondarySkillSpecialistChbx.CheckedChanged += new System.EventHandler(this.defenderHeroSecondarySkillSpecialistChbx_CheckedChanged);
            // 
            // defenderHeroSecondarySkillLbl
            // 
            this.defenderHeroSecondarySkillLbl.AutoSize = true;
            this.defenderHeroSecondarySkillLbl.Location = new System.Drawing.Point(3, 106);
            this.defenderHeroSecondarySkillLbl.Name = "defenderHeroSecondarySkillLbl";
            this.defenderHeroSecondarySkillLbl.Size = new System.Drawing.Size(43, 13);
            this.defenderHeroSecondarySkillLbl.TabIndex = 9;
            this.defenderHeroSecondarySkillLbl.Text = "Armorer";
            // 
            // defenderHasHeroChbx
            // 
            this.defenderHasHeroChbx.AutoSize = true;
            this.defenderHasHeroChbx.Location = new System.Drawing.Point(6, 55);
            this.defenderHasHeroChbx.Name = "defenderHasHeroChbx";
            this.defenderHasHeroChbx.Size = new System.Drawing.Size(69, 17);
            this.defenderHasHeroChbx.TabIndex = 6;
            this.defenderHasHeroChbx.Text = "Has hero";
            this.defenderHasHeroChbx.UseVisualStyleBackColor = true;
            this.defenderHasHeroChbx.CheckedChanged += new System.EventHandler(this.defenderHasHeroChbx_CheckedChanged);
            // 
            // terrainComboBox
            // 
            this.terrainComboBox.DropDownHeight = 120;
            this.terrainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.terrainComboBox.FormattingEnabled = true;
            this.terrainComboBox.IntegralHeight = false;
            this.terrainComboBox.Location = new System.Drawing.Point(6, 19);
            this.terrainComboBox.Name = "terrainComboBox";
            this.terrainComboBox.Size = new System.Drawing.Size(195, 21);
            this.terrainComboBox.TabIndex = 13;
            this.terrainComboBox.SelectedIndexChanged += new System.EventHandler(this.terrainComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.terrainComboBox);
            this.groupBox1.Location = new System.Drawing.Point(281, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 51);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terrain";
            // 
            // calculatedDamageLbl
            // 
            this.calculatedDamageLbl.AutoSize = true;
            this.calculatedDamageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedDamageLbl.Location = new System.Drawing.Point(15, 210);
            this.calculatedDamageLbl.Name = "calculatedDamageLbl";
            this.calculatedDamageLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedDamageLbl.TabIndex = 8;
            this.calculatedDamageLbl.Text = "123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Damage:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(90, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Kills:";
            // 
            // calculatedKillsLbl
            // 
            this.calculatedKillsLbl.AutoSize = true;
            this.calculatedKillsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedKillsLbl.Location = new System.Drawing.Point(90, 210);
            this.calculatedKillsLbl.Name = "calculatedKillsLbl";
            this.calculatedKillsLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedKillsLbl.TabIndex = 11;
            this.calculatedKillsLbl.Text = "123";
            // 
            // notesLbl
            // 
            this.notesLbl.AutoSize = true;
            this.notesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.notesLbl.Location = new System.Drawing.Point(149, 210);
            this.notesLbl.Name = "notesLbl";
            this.notesLbl.Size = new System.Drawing.Size(35, 13);
            this.notesLbl.TabIndex = 12;
            this.notesLbl.Text = "blabla";
            // 
            // attackerHeroLevelLbl
            // 
            this.attackerHeroLevelLbl.AutoSize = true;
            this.attackerHeroLevelLbl.Location = new System.Drawing.Point(120, 131);
            this.attackerHeroLevelLbl.Name = "attackerHeroLevelLbl";
            this.attackerHeroLevelLbl.Size = new System.Drawing.Size(35, 13);
            this.attackerHeroLevelLbl.TabIndex = 15;
            this.attackerHeroLevelLbl.Text = "- level";
            // 
            // defenderHeroLevelLbl
            // 
            this.defenderHeroLevelLbl.AutoSize = true;
            this.defenderHeroLevelLbl.Location = new System.Drawing.Point(120, 131);
            this.defenderHeroLevelLbl.Name = "defenderHeroLevelLbl";
            this.defenderHeroLevelLbl.Size = new System.Drawing.Size(35, 13);
            this.defenderHeroLevelLbl.TabIndex = 16;
            this.defenderHeroLevelLbl.Text = "- level";
            // 
            // defenderHeroLevelUpDn
            // 
            this.defenderHeroLevelUpDn.Location = new System.Drawing.Point(159, 129);
            this.defenderHeroLevelUpDn.Name = "defenderHeroLevelUpDn";
            this.defenderHeroLevelUpDn.Size = new System.Drawing.Size(42, 20);
            this.defenderHeroLevelUpDn.TabIndex = 16;
            this.defenderHeroLevelUpDn.ValueChanged += new System.EventHandler(this.defenderHeroLevelUpDn_ValueChanged);
            // 
            // defenderHeroDefenseUpDn
            // 
            this.defenderHeroDefenseUpDn.Location = new System.Drawing.Point(56, 77);
            this.defenderHeroDefenseUpDn.Name = "defenderHeroDefenseUpDn";
            this.defenderHeroDefenseUpDn.Size = new System.Drawing.Size(42, 20);
            this.defenderHeroDefenseUpDn.TabIndex = 7;
            this.defenderHeroDefenseUpDn.ValueChanged += new System.EventHandler(this.defenderHeroDefenseUpDn_ValueChanged);
            // 
            // attackerHeroLevelUpDn
            // 
            this.attackerHeroLevelUpDn.Location = new System.Drawing.Point(159, 129);
            this.attackerHeroLevelUpDn.Name = "attackerHeroLevelUpDn";
            this.attackerHeroLevelUpDn.Size = new System.Drawing.Size(42, 20);
            this.attackerHeroLevelUpDn.TabIndex = 14;
            this.attackerHeroLevelUpDn.ValueChanged += new System.EventHandler(this.attackerHeroLevelUpDn_ValueChanged);
            // 
            // attackerHeroAttackUpDn
            // 
            this.attackerHeroAttackUpDn.Location = new System.Drawing.Point(56, 77);
            this.attackerHeroAttackUpDn.Name = "attackerHeroAttackUpDn";
            this.attackerHeroAttackUpDn.Size = new System.Drawing.Size(42, 20);
            this.attackerHeroAttackUpDn.TabIndex = 3;
            this.attackerHeroAttackUpDn.ValueChanged += new System.EventHandler(this.attackerHeroAttackUpDn_ValueChanged);
            // 
            // attackerCountUpDn
            // 
            this.attackerCountUpDn.Location = new System.Drawing.Point(6, 20);
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
            this.attackerCountUpDn.TabIndex = 0;
            this.attackerCountUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attackerCountUpDn.ValueChanged += new System.EventHandler(this.attackerCountUpDn_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 240);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.notesLbl);
            this.Controls.Add(this.calculatedKillsLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calculatedDamageLbl);
            this.Controls.Add(this.defenderGroupBox);
            this.Controls.Add(this.attackerGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Heroes III Damage Calculator";
            this.attackerGroupBox.ResumeLayout(false);
            this.attackerGroupBox.PerformLayout();
            this.defenderGroupBox.ResumeLayout(false);
            this.defenderGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defenderHeroLevelUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defenderHeroDefenseUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHeroLevelUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHeroAttackUpDn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox attackerComboBox;
        private System.Windows.Forms.ComboBox defenderComboBox;
        private System.Windows.Forms.GroupBox attackerGroupBox;
        private System.Windows.Forms.GroupBox defenderGroupBox;
        private System.Windows.Forms.CheckBox attackerHasHeroChbx;
        private System.Windows.Forms.Label attackerHeroAttackLbl;
        private System.Windows.Forms.Label defenderHeroDefenseLbl;
        private System.Windows.Forms.CheckBox defenderHasHeroChbx;
        private System.Windows.Forms.ComboBox attackerHeroSecondarySkillComboBox;
        private System.Windows.Forms.Label attackerHeroSecondarySkillLbl;
        private System.Windows.Forms.ComboBox defenderHeroSecondarySkillComboBox;
        private System.Windows.Forms.Label defenderHeroSecondarySkillLbl;
        private ImmediateNumericUpDown attackerCountUpDn;
        private ImmediateNumericUpDown attackerHeroAttackUpDn;
        private ImmediateNumericUpDown defenderHeroDefenseUpDn;
        private System.Windows.Forms.ComboBox terrainComboBox;
        private System.Windows.Forms.CheckBox attackerHeroSecondarySkillSpecialistChbx;
        private ImmediateNumericUpDown attackerHeroLevelUpDn;
        private ImmediateNumericUpDown defenderHeroLevelUpDn;
        private System.Windows.Forms.CheckBox defenderHeroSecondarySkillSpecialistChbx;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label calculatedDamageLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label calculatedKillsLbl;
        private System.Windows.Forms.Label notesLbl;
        private System.Windows.Forms.Label attackerHeroLevelLbl;
        private System.Windows.Forms.Label defenderHeroLevelLbl;

    }
}

