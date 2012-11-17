using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using H3Calc.Engine;

namespace H3Calc
{
    public partial class Form1 : Form
    {        
        protected TerrainsList terrains;
        protected List<Hero> heroes;
        protected Hero genericHero;

        protected Unit attacker;
        protected Unit defender;
        protected DamageCalculator calculator;

        protected Control[] AttackerHeroControls;
        protected Control[] DefenderHeroControls;
        protected Control[] StandardModeControls;
        protected Control[] ScientificModeControls;        

        protected Unit Attacker
        {
            get
            {
                return attacker;
            }
            set
            {                
                attacker = value;
                attackerBtn.Text = value.Name;
                UpdateCalculatedDamage();
            }
        }

        protected Unit Defender
        {
            get
            {
                return defender;
            }
            set
            {
                defender = value;
                defenderBtn.Text = value.Name;
                UpdateCalculatedDamage();
            }
        }

        protected ApplicationMode Mode
        {
            get
            {
                return (ApplicationMode)Properties.Settings.Default.Mode;
            }
            set
            {
                Properties.Settings.Default.Mode = (int)value;
                Properties.Settings.Default.Save();
            }
        }

        public Form1()
        {
            InitializeComponent();

            // Removed this line from designer file to workaround against weird VS designer bug :<
            this.Menu = this.mainMenu1; 

            BuildControlGroups();
            
            ReadTerrainData();
            ReadHeroData();
            genericHero = new Hero();
            
            calculator = new DamageCalculator();
                        
            var heroComboBoxItems = new List<KeyValuePair<string, Hero>>();
            heroComboBoxItems.Add(new KeyValuePair<string, Hero>("No hero", null));
            heroComboBoxItems.Add(new KeyValuePair<string, Hero>("Generic hero", genericHero));
            foreach (Hero hero in heroes)
            {
                heroComboBoxItems.Add(new KeyValuePair<string, Hero>(hero.Name, hero));
            }

            attackerHeroComboBox.DataSource = heroComboBoxItems;
            attackerHeroComboBox.DisplayMember = "Key";
            attackerHeroComboBox.ValueMember = "Value";

            defenderHeroComboBox.DataSource = heroComboBoxItems;
            defenderHeroComboBox.DisplayMember = "Key";
            defenderHeroComboBox.ValueMember = "Value";
            defenderHeroComboBox.BindingContext = new BindingContext();

            terrainComboBox.DataSource = terrains;
            terrainComboBox.DisplayMember = "Name";

            GenerateSecondarySkillComboBoxItems(attackerHeroOffenseComboBox);
            GenerateSecondarySkillComboBoxItems(attackerHeroArcheryComboBox);
            GenerateSecondarySkillComboBoxItems(attackerHeroAirComboBox);
            GenerateSecondarySkillComboBoxItems(attackerHeroFireComboBox);
            GenerateSecondarySkillComboBoxItems(attackerHeroEarthComboBox);
            GenerateSecondarySkillComboBoxItems(attackerHeroWaterComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroArmorerComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroAirComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroFireComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroEarthComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroWaterComboBox);

            attackerCountUpDn.ValueChanged += ControlValueChanged;            
            attackerHasHeroChbx.CheckedChanged += ControlValueChanged;
            attackerHasHeroChbx.CheckedChanged += HeroComboBoxValueChanged;
            attackerHeroComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroComboBox.SelectedValueChanged += HeroComboBoxValueChanged;
            attackerHeroLevelUpDn.ValueChanged += ControlValueChanged;
            attackerHeroAttackUpDn.ValueChanged += ControlValueChanged;
            attackerHeroOffenseComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroArcheryComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroAirComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroFireComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroEarthComboBox.SelectedValueChanged += ControlValueChanged;
            attackerHeroWaterComboBox.SelectedValueChanged += ControlValueChanged;
            attackerBlessChbx.CheckedChanged += ControlValueChanged;
            attackerBloodlustChbx.CheckedChanged += ControlValueChanged;
            attackerPrecisionChbx.CheckedChanged += ControlValueChanged;
            attackerPrayerChbx.CheckedChanged += ControlValueChanged;
            attackerFrenzyChbx.CheckedChanged += ControlValueChanged;
            attackerSlayerChbx.CheckedChanged += ControlValueChanged;
            attackerCurseChbx.CheckedChanged += ControlValueChanged;
            attackerWeaknessChbx.CheckedChanged += ControlValueChanged;
            
            defenderHasHeroChbx.CheckedChanged += ControlValueChanged;
            defenderHasHeroChbx.CheckedChanged += HeroComboBoxValueChanged;
            defenderHeroComboBox.SelectedValueChanged += ControlValueChanged;
            defenderHeroComboBox.SelectedValueChanged += HeroComboBoxValueChanged;
            defenderHeroLevelUpDn.ValueChanged += ControlValueChanged;
            defenderHeroDefenseUpDn.ValueChanged += ControlValueChanged;
            defenderHeroArmorerComboBox.SelectedValueChanged += ControlValueChanged;
            defenderHeroAirComboBox.SelectedValueChanged += ControlValueChanged;
            defenderHeroFireComboBox.SelectedValueChanged += ControlValueChanged;
            defenderHeroEarthComboBox.SelectedValueChanged += ControlValueChanged;
            defenderHeroWaterComboBox.SelectedValueChanged += ControlValueChanged;
            defenderShieldChbx.CheckedChanged += ControlValueChanged;
            defenderStoneSkinChbx.CheckedChanged += ControlValueChanged;
            defenderPrayerChbx.CheckedChanged += ControlValueChanged;
            defenderAirShieldChbx.CheckedChanged += ControlValueChanged;
            defenderFrenzyChbx.CheckedChanged += ControlValueChanged;
            defenderDisruptingRayChbx.CheckedChanged += ControlValueChanged;

            terrainComboBox.SelectedValueChanged += ControlValueChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControlsOnHeroChange();
            UpdateControlsOnModeChange();            
            UpdateCalculatedDamage();            
        }

        private void BuildControlGroups()
        {
            AttackerHeroControls = new Control[] 
            { 
                attackerHeroLevelUpDn,
                attackerHeroAttackLbl,
                attackerHeroAttackUpDn,
                attackerHeroOffenseLbl,
                attackerHeroOffenseComboBox,
                attackerHeroArcheryLbl,
                attackerHeroArcheryComboBox,
                attackerHeroAirLbl,
                attackerHeroAirComboBox,
                attackerHeroFireLbl,
                attackerHeroFireComboBox,
                attackerHeroEarthLbl,
                attackerHeroEarthComboBox,
                attackerHeroWaterLbl,
                attackerHeroWaterComboBox,
                attackerBlessChbx,
                attackerBloodlustChbx,
                attackerPrecisionChbx,
                attackerPrayerChbx,
                attackerFrenzyChbx,
                attackerSlayerChbx,
                defenderDisruptingRayChbx
            };

            DefenderHeroControls = new Control[]
            {
                defenderHeroLevelLbl,
                defenderHeroLevelUpDn,
                defenderHeroDefenseLbl,
                defenderHeroDefenseUpDn,
                defenderHeroArmorerLbl,
                defenderHeroArmorerComboBox,
                defenderHeroAirLbl,
                defenderHeroAirComboBox,
                defenderHeroFireLbl,
                defenderHeroFireComboBox,
                defenderHeroEarthLbl,
                defenderHeroEarthComboBox,
                defenderHeroWaterLbl,
                defenderHeroWaterComboBox,
                defenderShieldChbx,
                defenderStoneSkinChbx,
                defenderPrayerChbx,
                defenderAirShieldChbx,
                defenderFrenzyChbx,
                attackerCurseChbx,
                attackerWeaknessChbx
            };

            StandardModeControls = new Control[]
            {
                attackerHeroComboBox,
                attackerHeroLevelLbl,
                attackerHeroLevelUpDn,
                attackerHeroOffenseLbl,
                attackerHeroOffenseComboBox,
                attackerHeroArcheryLbl,
                attackerHeroArcheryComboBox,
                
                defenderHeroComboBox,
                defenderHeroLevelLbl,
                defenderHeroLevelUpDn,
                defenderHeroArmorerLbl,
                defenderHeroArmorerComboBox,
                
            };

            ScientificModeControls = new Control[]
            {
                attackerHeroAirLbl,
                attackerHeroAirComboBox,
                attackerHeroFireLbl,
                attackerHeroFireComboBox,
                attackerHeroEarthLbl,
                attackerHeroEarthComboBox,
                attackerHeroWaterLbl,
                attackerHeroWaterComboBox,

                defenderHeroAirLbl,
                defenderHeroAirComboBox,
                defenderHeroFireLbl,
                defenderHeroFireComboBox,
                defenderHeroEarthLbl,
                defenderHeroEarthComboBox,
                defenderHeroWaterLbl,
                defenderHeroWaterComboBox,

                attackerBlessChbx,
                attackerBloodlustChbx,
                attackerPrecisionChbx,
                attackerPrayerChbx,
                attackerFrenzyChbx,
                attackerSlayerChbx,
                defenderDisruptingRayChbx,

                defenderShieldChbx,
                defenderStoneSkinChbx,
                defenderPrayerChbx,
                defenderAirShieldChbx,
                defenderFrenzyChbx,
                attackerCurseChbx,
                attackerWeaknessChbx
            };
        }        

        private void ReadTerrainData()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(TerrainsList));            
            TextReader reader = new StringReader(Properties.Resources.terrains);
            terrains = (TerrainsList)deserializer.Deserialize(reader);
            reader.Close();

            Terrain emptyTerrain = new Terrain { Id = -1, Name = "Don't care" };
            terrains.Insert(0, emptyTerrain);
        }

        private void ReadHeroData()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(HeroesList));
            TextReader reader = new StringReader(Properties.Resources.heroes);
            HeroesList unsortedHeroes = (HeroesList)deserializer.Deserialize(reader);
            reader.Close();

            heroes = unsortedHeroes.OrderBy(x => x.Name).ToList();
        }        

        private void GenerateSecondarySkillComboBoxItems(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var items = new List<KeyValuePair<string, SecondarySkillLevel>>();
            items.Add(new KeyValuePair<string, SecondarySkillLevel>(SecondarySkillLevel.None.Name, SecondarySkillLevel.None));
            items.Add(new KeyValuePair<string, SecondarySkillLevel>(SecondarySkillLevel.Basic.Name, SecondarySkillLevel.Basic));
            items.Add(new KeyValuePair<string, SecondarySkillLevel>(SecondarySkillLevel.Advanced.Name, SecondarySkillLevel.Advanced));
            items.Add(new KeyValuePair<string, SecondarySkillLevel>(SecondarySkillLevel.Expert.Name, SecondarySkillLevel.Expert));

            comboBox.DataSource = items;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            
            comboBox.SelectedIndex = 0;
        }

        private Hero SelectedAttackerHero()
        {            
            if (Mode == ApplicationMode.Simple)
            {
                return (attackerHasHeroChbx.Checked) ? genericHero : null;                
            }
            else
            {
                return (Hero)attackerHeroComboBox.SelectedValue;
            }
        }

        private Hero SelectedDefenderHero()
        {
            if (Mode == ApplicationMode.Simple)
            {
                return (defenderHasHeroChbx.Checked) ? genericHero : null;
            }
            else
            {
                return (Hero)defenderHeroComboBox.SelectedValue;
            }
        }

        private void SynchronizeHeroControls(ApplicationMode sourceMode)
        {
            if (sourceMode != ApplicationMode.Simple)
            {
                attackerHasHeroChbx.Checked = (attackerHeroComboBox.SelectedValue != null);
                defenderHasHeroChbx.Checked = (defenderHeroComboBox.SelectedValue != null);
            }
            else
            {
                if (attackerHasHeroChbx.Checked)
                {
                    if (attackerHeroComboBox.SelectedValue == null)
                    {
                        attackerHeroComboBox.SelectedValue = genericHero;
                    }
                }
                else
                {
                    attackerHeroComboBox.SelectedIndex = 0;
                }

                if (defenderHasHeroChbx.Checked)
                {
                    if (defenderHeroComboBox.SelectedValue == null)
                    {
                        defenderHeroComboBox.SelectedValue = genericHero;
                    }
                }
                else
                {
                    defenderHeroComboBox.SelectedIndex = 0;
                }
            }
        }

        private void UpdateControlsOnHeroChange()
        {
            SynchronizeHeroControls(Mode);

            bool enabled = (SelectedAttackerHero() != null);
            foreach (Control control in AttackerHeroControls)
            {
                control.Enabled = enabled;
            }

            enabled = (SelectedDefenderHero() != null);
            foreach (Control control in DefenderHeroControls)
            {
                control.Enabled = enabled;
            }
        }

        private void UpdateControlsOnModeChange()
        {
            foreach (MenuItem menuItem in menuItemMode.MenuItems)
            {
                menuItem.Checked = false;
            }

            switch (Mode)
            {
                case ApplicationMode.Simple:
                    menuItemMode1.Checked = true;
                    break;
                case ApplicationMode.Standard:
                    menuItemMode2.Checked = true;
                    break;
                case ApplicationMode.Scientific:
                    menuItemMode3.Checked = true;
                    break;
            }

            bool visible = (Mode == ApplicationMode.Simple);
            attackerHasHeroChbx.Visible = visible;
            defenderHasHeroChbx.Visible = visible;

            visible = (Mode == ApplicationMode.Standard || Mode == ApplicationMode.Scientific);
            foreach (Control control in StandardModeControls)
            {
                control.Visible = visible;
            }
 
            visible = (Mode == ApplicationMode.Scientific);
            foreach (Control control in ScientificModeControls)
            {
                control.Visible = visible;
            }            

            switch (Mode)
            {
                case ApplicationMode.Simple:
                    attackerGroupBox.Height = defenderGroupBox.Height = 90;
                    break;
                case ApplicationMode.Standard:
                    attackerGroupBox.Height = defenderGroupBox.Height = 184;
                    break;
                case ApplicationMode.Scientific:
                    attackerGroupBox.Height = defenderGroupBox.Height = 292;
                    break;
            }

            resultPanel.Top = terrainGroupBox.Top = attackerGroupBox.Top + attackerGroupBox.Height + 6;

            this.Height = terrainGroupBox.Top + terrainGroupBox.Height + 64;
        }
        
        private void UpdateCalculatedDamage()
        {
            DamageCalculatorInputData inputData = new DamageCalculatorInputData();

            inputData.Attacker = Attacker;
            inputData.Defender = Defender;

            bool haveInputData = ((inputData.Attacker != null) && (inputData.Defender != null));

            resultPanel.Visible = haveInputData;            

            if (!haveInputData)
            {
                return;
            }

            inputData.AttackerCount = (int)attackerCountUpDn.Value;

            inputData.Terrain = (Terrain)terrainComboBox.SelectedValue;
            if (inputData.Terrain.Id == -1)
            {
                inputData.Terrain = null;
            }

            inputData.AttackerHero = SelectedAttackerHero();
            if (inputData.AttackerHero != null)
            {
                HeroStats stats = new HeroStats();
                inputData.AttackerHero.Stats = stats;
                
                stats.Attack = (int)attackerHeroAttackUpDn.Value;

                if (Mode != ApplicationMode.Simple)
                {
                    stats.Level = (int)attackerHeroLevelUpDn.Value;

                    CheckSecondarySkillComboBox(attackerHeroOffenseComboBox, typeof(Offense), stats.SecondarySkills, inputData.AttackerHero);
                    CheckSecondarySkillComboBox(attackerHeroArcheryComboBox, typeof(Archery), stats.SecondarySkills, inputData.AttackerHero);

                    if (Mode == ApplicationMode.Scientific)
                    {
                        CheckSecondarySkillComboBox(attackerHeroAirComboBox, typeof(AirMagic), stats.SecondarySkills, inputData.AttackerHero);
                        CheckSecondarySkillComboBox(attackerHeroFireComboBox, typeof(FireMagic), stats.SecondarySkills, inputData.AttackerHero);
                        CheckSecondarySkillComboBox(attackerHeroEarthComboBox, typeof(EarthMagic), stats.SecondarySkills, inputData.AttackerHero);
                        CheckSecondarySkillComboBox(attackerHeroWaterComboBox, typeof(WaterMagic), stats.SecondarySkills, inputData.AttackerHero);


                        CheckSpellCheckbox(attackerBlessChbx, typeof(Bless), inputData.AttackerSpells, inputData.AttackerHero);
                        CheckSpellCheckbox(attackerBloodlustChbx, typeof(Bloodlust), inputData.AttackerSpells, inputData.AttackerHero);
                        CheckSpellCheckbox(attackerPrecisionChbx, typeof(Precision), inputData.AttackerSpells, inputData.AttackerHero);
                        CheckSpellCheckbox(attackerPrayerChbx, typeof(Prayer), inputData.AttackerSpells, inputData.AttackerHero);
                        CheckSpellCheckbox(attackerFrenzyChbx, typeof(Frenzy), inputData.AttackerSpells, inputData.AttackerHero);
                        CheckSpellCheckbox(attackerSlayerChbx, typeof(Slayer), inputData.AttackerSpells, inputData.AttackerHero);

                        CheckSpellCheckbox(defenderDisruptingRayChbx, typeof(DisruptingRay), inputData.DefenderSpells, inputData.AttackerHero);
                    }
                }
            }

            inputData.DefenderHero = SelectedDefenderHero();
            if (inputData.DefenderHero != null)
            {
                HeroStats stats = new HeroStats();
                inputData.DefenderHero.Stats = stats;

                stats.Defense = (int)defenderHeroDefenseUpDn.Value;

                if (Mode != ApplicationMode.Simple)
                {
                    stats.Level = (int)defenderHeroLevelUpDn.Value;

                    CheckSecondarySkillComboBox(defenderHeroArmorerComboBox, typeof(Armorer), stats.SecondarySkills, inputData.DefenderHero);

                    if (Mode == ApplicationMode.Scientific)
                    {
                        CheckSecondarySkillComboBox(defenderHeroAirComboBox, typeof(AirMagic), stats.SecondarySkills, inputData.DefenderHero);
                        CheckSecondarySkillComboBox(defenderHeroFireComboBox, typeof(FireMagic), stats.SecondarySkills, inputData.DefenderHero);
                        CheckSecondarySkillComboBox(defenderHeroEarthComboBox, typeof(EarthMagic), stats.SecondarySkills, inputData.DefenderHero);
                        CheckSecondarySkillComboBox(defenderHeroWaterComboBox, typeof(WaterMagic), stats.SecondarySkills, inputData.DefenderHero);


                        CheckSpellCheckbox(defenderShieldChbx, typeof(Shield), inputData.DefenderSpells, inputData.DefenderHero);
                        CheckSpellCheckbox(defenderStoneSkinChbx, typeof(StoneSkin), inputData.DefenderSpells, inputData.DefenderHero);
                        CheckSpellCheckbox(defenderPrayerChbx, typeof(Prayer), inputData.DefenderSpells, inputData.DefenderHero);
                        CheckSpellCheckbox(defenderAirShieldChbx, typeof(AirShield), inputData.DefenderSpells, inputData.DefenderHero);
                        CheckSpellCheckbox(defenderFrenzyChbx, typeof(Frenzy), inputData.DefenderSpells, inputData.DefenderHero);

                        CheckSpellCheckbox(attackerCurseChbx, typeof(Curse), inputData.AttackerSpells, inputData.DefenderHero);
                        CheckSpellCheckbox(attackerWeaknessChbx, typeof(Weakness), inputData.AttackerSpells, inputData.DefenderHero);
                    }
                }
            }

            int minDamage, maxDamage;
            string notes;
            calculator.CalculateDamage(inputData, out minDamage, out maxDamage, out notes);

            int minKills = minDamage / inputData.Defender.InitialStats.Health;
            int maxKills = maxDamage / inputData.Defender.InitialStats.Health;

            calculatedDamageLbl.Text = FormatRange(minDamage, maxDamage);
            calculatedKillsLbl.Text = FormatRange(minKills, maxKills);
            notesLbl.Text = (notes != null) ? "(" + notes + ")" : null;
        }

        private void CheckSpellCheckbox(CheckBox chbx, Type spellType, List<Spell> spells, Hero caster)
        {
            if (chbx.Checked)
            {
                Spell spell = (Spell)Activator.CreateInstance(spellType);
                spell.Caster = caster;

                spells.Add(spell);
            }
        }

        private void CheckSecondarySkillComboBox(ComboBox comboBox, Type skillType, List<SecondarySkill> skills, Hero hero)
        {
            SecondarySkillLevel skillLevel = (SecondarySkillLevel)comboBox.SelectedValue;
            if (skillLevel != SecondarySkillLevel.None)
            {
                SecondarySkill skill = (SecondarySkill)Activator.CreateInstance(skillType);
                skill.SkillLevel = skillLevel;
                skill.Hero = hero;

                skills.Add(skill);
            }
        }

        private string FormatRange(int min, int max)
        {
            if (min != max)
            {
                return min.ToString() + " — " + max.ToString();
            }
            else
            {
                return min.ToString();
            }
        }

        private void HeroComboBoxValueChanged(object sender, EventArgs e)
        {
            UpdateControlsOnHeroChange();
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {            
            UpdateCalculatedDamage();
        }

        private void menuItemMode_Click(object sender, EventArgs e)
        {
            SynchronizeHeroControls(Mode);

            if (sender == menuItemMode1)
            {
                Mode = ApplicationMode.Simple;
            }
            else if (sender == menuItemMode2)
            {
                Mode = ApplicationMode.Standard;
            }
            else if (sender == menuItemMode3)
            {
                Mode = ApplicationMode.Scientific;
            }
                        
            UpdateControlsOnModeChange();
            UpdateCalculatedDamage();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void attackerBtn_Click(object sender, EventArgs e)
        {
            UnitPicker unitPicker = new UnitPicker();
            unitPicker.UnitPicked += unitPicker_AttackerUnitPicked;
            unitPicker.StartPosition = FormStartPosition.CenterParent;            
            unitPicker.ShowDialog();
        }

        private void defenderBtn_Click(object sender, EventArgs e)
        {
            UnitPicker unitPicker = new UnitPicker();
            unitPicker.UnitPicked += unitPicker_DefenderUnitPicked;
            unitPicker.StartPosition = FormStartPosition.CenterParent;
            unitPicker.ShowDialog();
        }

        void unitPicker_AttackerUnitPicked(object sender, UnitEventArgs e)
        {
            Attacker = e.Unit;

            UnitPicker unitPicker = (UnitPicker)sender;
            unitPicker.Close();            
        }

        void unitPicker_DefenderUnitPicked(object sender, UnitEventArgs e)
        {
            Defender = e.Unit;

            UnitPicker unitPicker = (UnitPicker)sender;
            unitPicker.Close();
        }
    }
}
