using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using H3Calc.Engine;

namespace H3Calc
{
    public partial class SpellDamagePanel : UserControl
    {
        public event EventHandler DataChanged;

        private SpellDamageCalculatorData data;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpellDamageCalculatorData Data
        {
            get
            {
                if (data == null)
                {
                    data = new SpellDamageCalculatorData();                    
                }
                return data;
            }
            set
            {
                data = value;
                SetControlsWithData(value);
                UpdateCalculatedDamage();
            }
        }

        private List<DamageSpell> spells;
        public List<DamageSpell> Spells
        {
            get
            {
                return spells;
            }
            set
            {
                spells = value;
                UpdateSpellsComboBox();
            }
        }

        private List<Hero> heroes;
        public List<Hero> Heroes
        {
            get
            {
                return heroes;
            }
            set
            {
                heroes = value;
                UpdateHeroComboBox();
            }
        }

        public List<Unit> Units { get; set; }

        private Unit pickedUnit;
        private Unit PickedUnit
        {
            get
            {
                return pickedUnit;
            }
            set
            {
                pickedUnit = value;
                targetBtn.Text = (value != null) ? value.Name : "Pick";
            }
        }

        private bool dataUpdateSuspended;
        private ComboBox[] secondarySkillComboBoxes;

        private SpellDamageCalculator calculator;

        public SpellDamagePanel()
        {
            InitializeComponent();

            calculator = new SpellDamageCalculator();

            secondarySkillComboBoxes = new ComboBox[] {
                heroAirComboBox, heroFireComboBox, heroEarthComboBox, heroWaterComboBox, heroSorceryComboBox,
                protFromAirComboBox, protFromFireComboBox, protFromEarthComboBox, protFromWaterComboBox
            };

            foreach (ComboBox comboBox in secondarySkillComboBoxes)
            {
                comboBox.DataSource = SecondarySkillLevel.Levels();
                comboBox.DisplayMember = "Name";
            }

            spellComboBox.SelectedValueChanged += ControlValueChanged;
            heroComboBox.SelectedValueChanged += ControlValueChanged;
            heroLevelUpDn.ValueChanged += ControlValueChanged;
            heroSpellPowerUpDn.ValueChanged += ControlValueChanged;
            heroAirComboBox.SelectedValueChanged += ControlValueChanged;
            heroFireComboBox.SelectedValueChanged += ControlValueChanged;
            heroEarthComboBox.SelectedValueChanged += ControlValueChanged;
            heroWaterComboBox.SelectedValueChanged += ControlValueChanged;
            heroSorceryComboBox.SelectedValueChanged += ControlValueChanged;
            airOrbChbx.CheckedChanged += ControlValueChanged;
            fireOrbChbx.CheckedChanged += ControlValueChanged;
            earthOrbChbx.CheckedChanged += ControlValueChanged;
            waterOrbChbx.CheckedChanged += ControlValueChanged;
            protFromAirComboBox.SelectedValueChanged += ControlValueChanged;
            protFromFireComboBox.SelectedValueChanged += ControlValueChanged;
            protFromEarthComboBox.SelectedValueChanged += ControlValueChanged;
            protFromWaterComboBox.SelectedValueChanged += ControlValueChanged;

            UpdateCalculatedDamage();
        }

        private void UpdateSpellsComboBox()
        {
            if (spells == null)
            {
                return;
            }

            dataUpdateSuspended = true;
                        
            spellComboBox.DataSource = spells;
            spellComboBox.DisplayMember = "Name";

            dataUpdateSuspended = false;
        }

        private void UpdateHeroComboBox()
        {
            if (heroes == null)
            {
                return;
            }

            dataUpdateSuspended = true;
                        
            heroComboBox.DataSource = heroes;
            heroComboBox.DisplayMember = "Name";

            dataUpdateSuspended = false;
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            UpdateData();            
        }

        private void UpdateData()
        {
            if (!dataUpdateSuspended)
            {
                UpdateDataFromControls(Data);

                if (DataChanged != null)
                {
                    DataChanged(this, null);
                }

                UpdateCalculatedDamage();
            }
        }

        void UpdateDataFromControls(SpellDamageCalculatorData data)
        {            
            data.Spell = (DamageSpell)spellComboBox.SelectedValue;

            if (data.CasterHeroStats == null)
            {
                data.CasterHeroStats = new HeroStats();
            }

            data.CasterHeroStats.Hero = (Hero)heroComboBox.SelectedValue;
            data.CasterHeroStats.Level = (int)heroLevelUpDn.Value;
            data.CasterHeroStats.SpellPower = (int)heroSpellPowerUpDn.Value;

            data.CasterHeroStats.SetLevelForSecondarySkillType(typeof(AirMagic), (SecondarySkillLevel)heroAirComboBox.SelectedValue);
            data.CasterHeroStats.SetLevelForSecondarySkillType(typeof(FireMagic), (SecondarySkillLevel)heroFireComboBox.SelectedValue);
            data.CasterHeroStats.SetLevelForSecondarySkillType(typeof(EarthMagic), (SecondarySkillLevel)heroEarthComboBox.SelectedValue);
            data.CasterHeroStats.SetLevelForSecondarySkillType(typeof(WaterMagic), (SecondarySkillLevel)heroWaterComboBox.SelectedValue);
            data.CasterHeroStats.SetLevelForSecondarySkillType(typeof(Sorcery), (SecondarySkillLevel)heroSorceryComboBox.SelectedValue);

            data.TargetProtectionSpells = new List<ProtectionSpell>();
            CheckProtectionSpellComboBox(protFromAirComboBox, new ProtectionFromAir(), data.TargetProtectionSpells);
            CheckProtectionSpellComboBox(protFromFireComboBox, new ProtectionFromFire(), data.TargetProtectionSpells);
            CheckProtectionSpellComboBox(protFromEarthComboBox, new ProtectionFromEarth(), data.TargetProtectionSpells);
            CheckProtectionSpellComboBox(protFromWaterComboBox, new ProtectionFromWater(), data.TargetProtectionSpells);

            data.CasterMagicOrbs = new List<MagicOrb>();
            CheckMagicOrbCheckBox(airOrbChbx, typeof(AirMagic), data.CasterMagicOrbs);
            CheckMagicOrbCheckBox(fireOrbChbx, typeof(FireMagic), data.CasterMagicOrbs);
            CheckMagicOrbCheckBox(earthOrbChbx, typeof(EarthMagic), data.CasterMagicOrbs);
            CheckMagicOrbCheckBox(waterOrbChbx, typeof(WaterMagic), data.CasterMagicOrbs);

            data.Spell.CasterStats = data.CasterHeroStats.SpellCasterStatsForSpell(data.Spell);

            data.Target = PickedUnit;            
        }

        void CheckProtectionSpellComboBox(ComboBox comboBox, ProtectionSpell spell, List<ProtectionSpell> spells)
        {
            SecondarySkillLevel skillLevel = (SecondarySkillLevel)comboBox.SelectedValue;
            if (skillLevel == SecondarySkillLevel.None) 
            {
                return;
            }

            spell.CasterStats.SkillLevel = skillLevel;
            spells.Add(spell);            
        }

        void CheckMagicOrbCheckBox(CheckBox checkBox, Type magicType, List<MagicOrb> orbs)
        {
            if (checkBox.Checked)
            {
                MagicOrb orb = new MagicOrb(magicType);
                orbs.Add(orb);
            }
        }

        void SetControlsWithData(SpellDamageCalculatorData data)
        {
            dataUpdateSuspended = true;

            spellComboBox.SelectedItem = data.Spell ?? spellComboBox.Items[0];

            if (data.CasterHeroStats != null && data.CasterHeroStats.Hero != null)
            {
                heroComboBox.SelectedItem = data.CasterHeroStats.Hero;
                heroLevelUpDn.Value = data.CasterHeroStats.Level;
                heroSpellPowerUpDn.Value = data.CasterHeroStats.SpellPower;

                heroAirComboBox.SelectedItem = data.CasterHeroStats.LevelForSecondarySkillType(typeof(AirMagic));
                heroFireComboBox.SelectedItem = data.CasterHeroStats.LevelForSecondarySkillType(typeof(FireMagic));
                heroEarthComboBox.SelectedItem = data.CasterHeroStats.LevelForSecondarySkillType(typeof(EarthMagic));
                heroWaterComboBox.SelectedItem = data.CasterHeroStats.LevelForSecondarySkillType(typeof(WaterMagic));
                heroSorceryComboBox.SelectedItem = data.CasterHeroStats.LevelForSecondarySkillType(typeof(Sorcery));
            }
            else
            {
                heroComboBox.SelectedIndex = 0;
                heroLevelUpDn.Value = 1;
                heroSpellPowerUpDn.Value = 0;

                heroAirComboBox.SelectedItem = SecondarySkillLevel.None;
                heroFireComboBox.SelectedItem = SecondarySkillLevel.None;
                heroEarthComboBox.SelectedItem = SecondarySkillLevel.None;
                heroWaterComboBox.SelectedItem = SecondarySkillLevel.None;
                heroSorceryComboBox.SelectedItem = SecondarySkillLevel.None;
            }

            PickedUnit = data.Target;

            dataUpdateSuspended = false;
        }

        void UpdateCalculatedDamage()
        {
            bool hasInputData = (Data.Spell != null && Data.Target != null);
            resultPanel.Visible = hasInputData;
            if (!hasInputData)
            {
                return;
            }

            int damage;
            calculator.CalculateDamage(Data, out damage);

            int kills = damage / Data.Target.InitialStats.Health;

            calculatedDamageLbl.Text = damage.ToString();
            calculatedKillsLbl.Text = kills.ToString();
        }

        private void targetBtn_Click(object sender, EventArgs e)
        {
            UnitPicker unitPicker = new UnitPicker(Units);
            unitPicker.UnitPicked += UnitPicked;
            unitPicker.StartPosition = FormStartPosition.CenterParent;
            unitPicker.ShowDialog();
        }

        private void UnitPicked(object sender, UnitEventArgs e)
        {
            PickedUnit = e.Unit;
            UpdateData();

            UnitPicker unitPicker = (UnitPicker)sender;
            unitPicker.Close();
        }
    }
}
