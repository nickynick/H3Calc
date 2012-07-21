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

namespace H3Calc
{
    public partial class Form1 : Form
    {
        protected UnitsList units;
        protected TerrainsList terrains;
        protected DamageCalculator calculator;

        public Form1()
        {
            InitializeComponent();

            ReadUnitData();
            ReadTerrainData();
            
            calculator = new DamageCalculator();

            attackerComboBox.DataSource = units;
            attackerComboBox.DisplayMember = "Name";

            defenderComboBox.DataSource = units;
            defenderComboBox.DisplayMember = "Name";
            defenderComboBox.BindingContext = new BindingContext();

            terrainComboBox.DataSource = terrains;
            terrainComboBox.DisplayMember = "Name";

            GenerateSecondarySkillComboBoxItems(attackerHeroSecondarySkillComboBox);
            GenerateSecondarySkillComboBoxItems(defenderHeroSecondarySkillComboBox);

            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void GenerateUnitData()
        {
            XmlSerializer x = new XmlSerializer(typeof(UnitsList));
            TextWriter writer = new StreamWriter("units.xml");
            UnitsList unitsList = new UnitsList();

            StreamReader names = new StreamReader("names.txt");
            StreamReader damage = new StreamReader("damage.txt");
            StreamReader attack = new StreamReader("attack.txt");
            StreamReader defense = new StreamReader("defense.txt");
            StreamReader health = new StreamReader("health.txt");

            while (!names.EndOfStream)
            {
                Unit unit = new Unit();
                unit.Name = names.ReadLine();

                string[] damageStrings = damage.ReadLine().Split(new char[] { '-' });
                unit.MinDamage = int.Parse(damageStrings[0]);
                unit.MaxDamage = (damageStrings.Length > 1) ? int.Parse(damageStrings[1]) : unit.MinDamage;

                unit.Attack = int.Parse(attack.ReadLine());
                unit.Defense = int.Parse(defense.ReadLine());
                unit.Health = int.Parse(health.ReadLine());

                unitsList.Add(unit);
            }

            names.Close();
            damage.Close();
            attack.Close();
            defense.Close();
            health.Close();

            x.Serialize(writer, unitsList);
            writer.Close();
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

        private void GenerateSecondarySkillComboBoxItems(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            comboBox.Items.Add(new SecondarySkillLevel { Level = 0, Title = "None" });
            comboBox.Items.Add(new SecondarySkillLevel { Level = 1, Title = "Basic" });
            comboBox.Items.Add(new SecondarySkillLevel { Level = 2, Title = "Advanced" });
            comboBox.Items.Add(new SecondarySkillLevel { Level = 3, Title = "Expert" });

            comboBox.DisplayMember = "Title";
            comboBox.ValueMember = "Level";

            comboBox.SelectedIndex = 0;
        }

        private void UpdateControls()
        {
            bool enabled;
            
            enabled = attackerHasHeroChbx.Checked;
            attackerHeroAttackLbl.Enabled = enabled;
            attackerHeroAttackUpDn.Enabled = enabled;
            attackerHeroSecondarySkillLbl.Enabled = enabled;
            attackerHeroSecondarySkillComboBox.Enabled = enabled;
            attackerHeroSecondarySkillSpecialistChbx.Enabled = enabled;

            enabled = enabled && attackerHeroSecondarySkillSpecialistChbx.Checked;
            attackerHeroLevelLbl.Enabled = enabled;
            attackerHeroLevelUpDn.Enabled = enabled;

            enabled = defenderHasHeroChbx.Checked;
            defenderHeroDefenseUpDn.Enabled = enabled;
            defenderHeroDefenseLbl.Enabled = enabled;
            defenderHeroSecondarySkillLbl.Enabled = enabled;
            defenderHeroSecondarySkillComboBox.Enabled = enabled;
            defenderHeroSecondarySkillSpecialistChbx.Enabled = enabled;

            enabled = enabled && defenderHeroSecondarySkillSpecialistChbx.Checked;
            defenderHeroLevelLbl.Enabled = enabled;
            defenderHeroLevelUpDn.Enabled = enabled;
        }

        private void UpdateCalculatedDamage()
        {
            Unit attacker = (Unit)attackerComboBox.SelectedValue;
            Unit defender = (Unit)defenderComboBox.SelectedValue;            

            if ((attacker == null) || (defender == null))
            {
                return;
            }

            int attackerCount = (int)attackerCountUpDn.Value;

            Terrain terrain = (Terrain)terrainComboBox.SelectedValue;

            Hero attackerHero = null;
            if (attackerHasHeroChbx.Checked)
            {
                attackerHero = new Hero();
                attackerHero.PrimarySkill = (int)attackerHeroAttackUpDn.Value;
                attackerHero.SecondarySkill = ((SecondarySkillLevel)attackerHeroSecondarySkillComboBox.SelectedItem).Level;

                if (attackerHeroSecondarySkillSpecialistChbx.Checked)
                {
                    attackerHero.IsSecondarySkillSpecialist = true;
                    attackerHero.Level = (int)attackerHeroLevelUpDn.Value;
                }
            }

            Hero defenderHero = null;
            if (defenderHasHeroChbx.Checked)
            {
                defenderHero = new Hero();
                defenderHero.PrimarySkill = (int)defenderHeroDefenseUpDn.Value;
                defenderHero.SecondarySkill = ((SecondarySkillLevel)defenderHeroSecondarySkillComboBox.SelectedItem).Level;

                if (defenderHeroSecondarySkillSpecialistChbx.Checked)
                {
                    defenderHero.IsSecondarySkillSpecialist = true;
                    defenderHero.Level = (int)defenderHeroLevelUpDn.Value;
                }
            }

            int minDamage, maxDamage;
            string notes;
            calculator.CalculateDamage(attacker, attackerCount, defender, attackerHero, defenderHero, terrain, out minDamage, out maxDamage, out notes);

            int minKills = minDamage / defender.Health;
            int maxKills = maxDamage / defender.Health;

            calculatedDamageLbl.Text = FormatRange(minDamage, maxDamage);
            calculatedKillsLbl.Text = FormatRange(minKills, maxKills);
            notesLbl.Text = (notes != null) ? "(" + notes + ")" : null;
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

        private void attackerHasHeroChbx_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void defenderHasHeroChbx_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void attackerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Unit attacker = (Unit)attackerComboBox.SelectedValue;
            attackerHeroSecondarySkillLbl.Text = (attacker.IsRanged) ? "Archery" : "Offense";

            UpdateCalculatedDamage();
        }        

        private void attackerHeroAttackUpDn_ValueChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void attackerHeroSecondarySkillComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void defenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void defenderHeroDefenseUpDn_ValueChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void defenderHeroSecondarySkillComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void attackerCountUpDn_ValueChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void attackerHeroSecondarySkillSpecialistChbx_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void defenderHeroSecondarySkillSpecialistChbx_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
            UpdateCalculatedDamage();
        }

        private void attackerHeroLevelUpDn_ValueChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void defenderHeroLevelUpDn_ValueChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }

        private void terrainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalculatedDamage();
        }
    }
}
