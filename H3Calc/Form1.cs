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
        protected UnitsList units;
        protected TerrainsList terrains;
        protected List<Hero> heroes;

        protected DamageCalculator calculator;

        public Form1()
        {
            InitializeComponent();

            ReadUnitData();
            ReadTerrainData();
            ReadHeroData();
            
            calculator = new DamageCalculator();

            attackerComboBox.DataSource = units;
            attackerComboBox.DisplayMember = "Name";

            defenderComboBox.DataSource = units;
            defenderComboBox.DisplayMember = "Name";
            defenderComboBox.BindingContext = new BindingContext();

            var heroComboBoxItems = new List<KeyValuePair<string, Hero>>();
            heroComboBoxItems.Add(new KeyValuePair<string, Hero>("No hero", null));
            heroComboBoxItems.Add(new KeyValuePair<string, Hero>("Generic hero", new Hero()));
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
            attackerComboBox.SelectedValueChanged += ControlValueChanged;
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

            defenderComboBox.SelectedValueChanged += ControlValueChanged;
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

            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void ReadUnitData()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(UnitsList));
            TextReader reader = new StreamReader("units.xml");
            units = (UnitsList)deserializer.Deserialize(reader);
            reader.Close();
        }

        private void ReadTerrainData()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(TerrainsList));
            TextReader reader = new StreamReader("terrains.xml");
            terrains = (TerrainsList)deserializer.Deserialize(reader);
            reader.Close();

            Terrain emptyTerrain = new Terrain { Id = -1, Name = "Don't care" };
            terrains.Insert(0, emptyTerrain);
        }

        private void ReadHeroData()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(HeroesList));
            TextReader reader = new StreamReader("heroes.xml");
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

        private void UpdateControls()
        {            
            bool enabled;
            
            enabled = (attackerHeroComboBox.SelectedValue != null);
            attackerHeroLevelLbl.Enabled = enabled;
            attackerHeroLevelUpDn.Enabled = enabled;
            attackerHeroAttackLbl.Enabled = enabled;
            attackerHeroAttackUpDn.Enabled = enabled;
            attackerHeroOffenseLbl.Enabled = enabled;
            attackerHeroOffenseComboBox.Enabled = enabled;
            attackerHeroArcheryLbl.Enabled = enabled;
            attackerHeroArcheryComboBox.Enabled = enabled;
            attackerHeroAirLbl.Enabled = enabled;
            attackerHeroAirComboBox.Enabled = enabled;
            attackerHeroFireLbl.Enabled = enabled;
            attackerHeroFireComboBox.Enabled = enabled;
            attackerHeroEarthLbl.Enabled = enabled;
            attackerHeroEarthComboBox.Enabled = enabled;
            attackerHeroWaterLbl.Enabled = enabled;
            attackerHeroWaterComboBox.Enabled = enabled;
            attackerBlessChbx.Enabled = enabled;
            attackerBloodlustChbx.Enabled = enabled;
            attackerPrecisionChbx.Enabled = enabled;
            attackerPrayerChbx.Enabled = enabled;
            attackerFrenzyChbx.Enabled = enabled;
            attackerSlayerChbx.Enabled = enabled;
            defenderDisruptingRayChbx.Enabled = enabled;

            enabled = (defenderHeroComboBox.SelectedValue != null);
            defenderHeroLevelLbl.Enabled = enabled;
            defenderHeroLevelUpDn.Enabled = enabled;
            defenderHeroDefenseLbl.Enabled = enabled;
            defenderHeroDefenseUpDn.Enabled = enabled;
            defenderHeroArmorerLbl.Enabled = enabled;
            defenderHeroArmorerComboBox.Enabled = enabled;
            defenderHeroAirLbl.Enabled = enabled;
            defenderHeroAirComboBox.Enabled = enabled;
            defenderHeroFireLbl.Enabled = enabled;
            defenderHeroFireComboBox.Enabled = enabled;
            defenderHeroEarthLbl.Enabled = enabled;
            defenderHeroEarthComboBox.Enabled = enabled;
            defenderHeroWaterLbl.Enabled = enabled;
            defenderHeroWaterComboBox.Enabled = enabled;
            defenderShieldChbx.Enabled = enabled;
            defenderStoneSkinChbx.Enabled = enabled;
            defenderPrayerChbx.Enabled = enabled;
            defenderAirShieldChbx.Enabled = enabled;
            defenderFrenzyChbx.Enabled = enabled;
            attackerCurseChbx.Enabled = enabled;
            attackerWeaknessChbx.Enabled = enabled;
        }

        private void UpdateCalculatedDamage()
        {
            DamageCalculatorInputData inputData = new DamageCalculatorInputData();
            
            inputData.Attacker = (Unit)attackerComboBox.SelectedValue;
            inputData.Defender = (Unit)defenderComboBox.SelectedValue;

            if ((inputData.Attacker == null) || (inputData.Defender == null))
            {
                return;
            }

            inputData.AttackerCount = (int)attackerCountUpDn.Value;

            inputData.Terrain = (Terrain)terrainComboBox.SelectedValue;
            if (inputData.Terrain.Id == -1)
            {
                inputData.Terrain = null;
            }

            inputData.AttackerHero = (Hero)attackerHeroComboBox.SelectedValue;
            if (inputData.AttackerHero != null)
            {
                HeroStats stats = new HeroStats();
                inputData.AttackerHero.Stats = stats;

                stats.Level = (int)attackerHeroLevelUpDn.Value;
                stats.Attack = (int)attackerHeroAttackUpDn.Value;

                CheckSecondarySkillComboBox(attackerHeroOffenseComboBox, typeof(Offense), stats.SecondarySkills, inputData.AttackerHero);
                CheckSecondarySkillComboBox(attackerHeroArcheryComboBox, typeof(Archery), stats.SecondarySkills, inputData.AttackerHero);
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

            inputData.DefenderHero = (Hero)defenderHeroComboBox.SelectedValue;
            if (inputData.DefenderHero != null)
            {
                HeroStats stats = new HeroStats();
                inputData.DefenderHero.Stats = stats;

                stats.Level = (int)defenderHeroLevelUpDn.Value;
                stats.Defense = (int)defenderHeroDefenseUpDn.Value;

                CheckSecondarySkillComboBox(defenderHeroArmorerComboBox, typeof(Armorer), stats.SecondarySkills, inputData.DefenderHero);
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
            UpdateControls();
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {            
            UpdateCalculatedDamage();
        }
    }
}
