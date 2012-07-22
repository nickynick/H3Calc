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
            this.defenderComboBox = new System.Windows.Forms.ComboBox();
            this.attackerGroupBox = new System.Windows.Forms.GroupBox();
            this.attackerCountUpDn = new H3Calc.ImmediateNumericUpDown();
            this.attackerComboBox = new System.Windows.Forms.ComboBox();
            this.defenderHeroDefenseLbl = new System.Windows.Forms.Label();
            this.defenderGroupBox = new System.Windows.Forms.GroupBox();
            this.immediateNumericUpDown3 = new H3Calc.ImmediateNumericUpDown();
            this.immediateNumericUpDown2 = new H3Calc.ImmediateNumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.terrainComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.calculatedDamageLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calculatedKillsLbl = new System.Windows.Forms.Label();
            this.notesLbl = new System.Windows.Forms.Label();
            this.immediateNumericUpDown1 = new H3Calc.ImmediateNumericUpDown();
            this.immediateNumericUpDown4 = new H3Calc.ImmediateNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.attackerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).BeginInit();
            this.defenderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // defenderComboBox
            // 
            this.defenderComboBox.DropDownHeight = 500;
            this.defenderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defenderComboBox.FormattingEnabled = true;
            this.defenderComboBox.IntegralHeight = false;
            this.defenderComboBox.Location = new System.Drawing.Point(6, 19);
            this.defenderComboBox.Name = "defenderComboBox";
            this.defenderComboBox.Size = new System.Drawing.Size(289, 21);
            this.defenderComboBox.TabIndex = 5;
            this.defenderComboBox.SelectedIndexChanged += new System.EventHandler(this.defenderComboBox_SelectedIndexChanged);
            // 
            // attackerGroupBox
            // 
            this.attackerGroupBox.Controls.Add(this.immediateNumericUpDown1);
            this.attackerGroupBox.Controls.Add(this.immediateNumericUpDown4);
            this.attackerGroupBox.Controls.Add(this.attackerComboBox);
            this.attackerGroupBox.Controls.Add(this.label3);
            this.attackerGroupBox.Controls.Add(this.comboBox2);
            this.attackerGroupBox.Controls.Add(this.comboBox1);
            this.attackerGroupBox.Controls.Add(this.label15);
            this.attackerGroupBox.Controls.Add(this.label14);
            this.attackerGroupBox.Controls.Add(this.label4);
            this.attackerGroupBox.Controls.Add(this.comboBox12);
            this.attackerGroupBox.Controls.Add(this.comboBox3);
            this.attackerGroupBox.Controls.Add(this.label13);
            this.attackerGroupBox.Controls.Add(this.label5);
            this.attackerGroupBox.Controls.Add(this.comboBox11);
            this.attackerGroupBox.Controls.Add(this.comboBox10);
            this.attackerGroupBox.Controls.Add(this.label12);
            this.attackerGroupBox.Location = new System.Drawing.Point(68, 12);
            this.attackerGroupBox.Name = "attackerGroupBox";
            this.attackerGroupBox.Size = new System.Drawing.Size(301, 208);
            this.attackerGroupBox.TabIndex = 5;
            this.attackerGroupBox.TabStop = false;
            this.attackerGroupBox.Text = "Attacker";
            // 
            // attackerCountUpDn
            // 
            this.attackerCountUpDn.Location = new System.Drawing.Point(12, 32);
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
            // attackerComboBox
            // 
            this.attackerComboBox.DropDownHeight = 500;
            this.attackerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerComboBox.FormattingEnabled = true;
            this.attackerComboBox.IntegralHeight = false;
            this.attackerComboBox.Location = new System.Drawing.Point(6, 19);
            this.attackerComboBox.Name = "attackerComboBox";
            this.attackerComboBox.Size = new System.Drawing.Size(289, 21);
            this.attackerComboBox.TabIndex = 1;
            this.attackerComboBox.SelectedIndexChanged += new System.EventHandler(this.attackerComboBox_SelectedIndexChanged);
            // 
            // defenderHeroDefenseLbl
            // 
            this.defenderHeroDefenseLbl.AutoSize = true;
            this.defenderHeroDefenseLbl.Location = new System.Drawing.Point(6, 103);
            this.defenderHeroDefenseLbl.Name = "defenderHeroDefenseLbl";
            this.defenderHeroDefenseLbl.Size = new System.Drawing.Size(47, 13);
            this.defenderHeroDefenseLbl.TabIndex = 5;
            this.defenderHeroDefenseLbl.Text = "Defense";
            // 
            // defenderGroupBox
            // 
            this.defenderGroupBox.Controls.Add(this.immediateNumericUpDown3);
            this.defenderGroupBox.Controls.Add(this.immediateNumericUpDown2);
            this.defenderGroupBox.Controls.Add(this.label11);
            this.defenderGroupBox.Controls.Add(this.comboBox8);
            this.defenderGroupBox.Controls.Add(this.comboBox9);
            this.defenderGroupBox.Controls.Add(this.label10);
            this.defenderGroupBox.Controls.Add(this.comboBox7);
            this.defenderGroupBox.Controls.Add(this.label9);
            this.defenderGroupBox.Controls.Add(this.comboBox4);
            this.defenderGroupBox.Controls.Add(this.label6);
            this.defenderGroupBox.Controls.Add(this.defenderComboBox);
            this.defenderGroupBox.Controls.Add(this.comboBox5);
            this.defenderGroupBox.Controls.Add(this.label7);
            this.defenderGroupBox.Controls.Add(this.comboBox6);
            this.defenderGroupBox.Controls.Add(this.label8);
            this.defenderGroupBox.Controls.Add(this.defenderHeroDefenseLbl);
            this.defenderGroupBox.Location = new System.Drawing.Point(375, 12);
            this.defenderGroupBox.Name = "defenderGroupBox";
            this.defenderGroupBox.Size = new System.Drawing.Size(301, 208);
            this.defenderGroupBox.TabIndex = 6;
            this.defenderGroupBox.TabStop = false;
            this.defenderGroupBox.Text = "Defender";
            // 
            // immediateNumericUpDown3
            // 
            this.immediateNumericUpDown3.Location = new System.Drawing.Point(218, 61);
            this.immediateNumericUpDown3.Maximum = new decimal(new int[] {
            108,
            0,
            0,
            0});
            this.immediateNumericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.immediateNumericUpDown3.Name = "immediateNumericUpDown3";
            this.immediateNumericUpDown3.Size = new System.Drawing.Size(42, 20);
            this.immediateNumericUpDown3.TabIndex = 25;
            this.immediateNumericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // immediateNumericUpDown2
            // 
            this.immediateNumericUpDown2.Location = new System.Drawing.Point(59, 101);
            this.immediateNumericUpDown2.Name = "immediateNumericUpDown2";
            this.immediateNumericUpDown2.Size = new System.Drawing.Size(42, 20);
            this.immediateNumericUpDown2.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(176, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Level";
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(218, 181);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(77, 21);
            this.comboBox8.TabIndex = 31;
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(6, 60);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(130, 21);
            this.comboBox9.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Earth";
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(218, 154);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(77, 21);
            this.comboBox7.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(176, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Fire";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(218, 127);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(77, 21);
            this.comboBox4.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Water";
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(218, 100);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(77, 21);
            this.comboBox5.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(176, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Air";
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(59, 127);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(77, 21);
            this.comboBox6.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Armorer";
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
            this.groupBox1.Location = new System.Drawing.Point(278, 467);
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
            this.calculatedDamageLbl.Location = new System.Drawing.Point(12, 500);
            this.calculatedDamageLbl.Name = "calculatedDamageLbl";
            this.calculatedDamageLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedDamageLbl.TabIndex = 8;
            this.calculatedDamageLbl.Text = "123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Damage:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(87, 477);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Kills:";
            // 
            // calculatedKillsLbl
            // 
            this.calculatedKillsLbl.AutoSize = true;
            this.calculatedKillsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.calculatedKillsLbl.Location = new System.Drawing.Point(87, 500);
            this.calculatedKillsLbl.Name = "calculatedKillsLbl";
            this.calculatedKillsLbl.Size = new System.Drawing.Size(25, 13);
            this.calculatedKillsLbl.TabIndex = 11;
            this.calculatedKillsLbl.Text = "123";
            // 
            // notesLbl
            // 
            this.notesLbl.AutoSize = true;
            this.notesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.notesLbl.Location = new System.Drawing.Point(146, 500);
            this.notesLbl.Name = "notesLbl";
            this.notesLbl.Size = new System.Drawing.Size(35, 13);
            this.notesLbl.TabIndex = 12;
            this.notesLbl.Text = "blabla";
            // 
            // immediateNumericUpDown1
            // 
            this.immediateNumericUpDown1.Location = new System.Drawing.Point(218, 61);
            this.immediateNumericUpDown1.Maximum = new decimal(new int[] {
            108,
            0,
            0,
            0});
            this.immediateNumericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.immediateNumericUpDown1.Name = "immediateNumericUpDown1";
            this.immediateNumericUpDown1.Size = new System.Drawing.Size(42, 20);
            this.immediateNumericUpDown1.TabIndex = 40;
            this.immediateNumericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // immediateNumericUpDown4
            // 
            this.immediateNumericUpDown4.Location = new System.Drawing.Point(59, 101);
            this.immediateNumericUpDown4.Name = "immediateNumericUpDown4";
            this.immediateNumericUpDown4.Size = new System.Drawing.Size(42, 20);
            this.immediateNumericUpDown4.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Level";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(218, 181);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 21);
            this.comboBox1.TabIndex = 46;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(6, 60);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(130, 21);
            this.comboBox2.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Earth";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(218, 154);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(77, 21);
            this.comboBox3.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Fire";
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(218, 127);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(77, 21);
            this.comboBox10.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(176, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Water";
            // 
            // comboBox11
            // 
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Location = new System.Drawing.Point(218, 100);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(77, 21);
            this.comboBox11.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(176, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Air";
            // 
            // comboBox12
            // 
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Location = new System.Drawing.Point(59, 127);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(77, 21);
            this.comboBox12.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 130);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "Armorer";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Defense";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 586);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.attackerCountUpDn);
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
            ((System.ComponentModel.ISupportInitialize)(this.attackerCountUpDn)).EndInit();
            this.defenderGroupBox.ResumeLayout(false);
            this.defenderGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.immediateNumericUpDown4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox defenderComboBox;
        private System.Windows.Forms.GroupBox attackerGroupBox;
        private System.Windows.Forms.GroupBox defenderGroupBox;
        private System.Windows.Forms.Label defenderHeroDefenseLbl;
        private ImmediateNumericUpDown attackerCountUpDn;
        private System.Windows.Forms.ComboBox terrainComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label calculatedDamageLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label calculatedKillsLbl;
        private System.Windows.Forms.Label notesLbl;
        private ImmediateNumericUpDown immediateNumericUpDown3;
        private ImmediateNumericUpDown immediateNumericUpDown2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox attackerComboBox;
        private ImmediateNumericUpDown immediateNumericUpDown1;
        private ImmediateNumericUpDown immediateNumericUpDown4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox12;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox11;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.Label label12;

    }
}

