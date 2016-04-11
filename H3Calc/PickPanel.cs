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
    public partial class PickPanel : UserControl
    {
        public event EventHandler DataChanged;

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

        private ApplicationMode mode;
        public ApplicationMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                SynchronizeHeroControls(mode);
                mode = value;
                ModeUpdated();
            }
        }

        private HeroStats opponentHeroStats;
        public HeroStats OpponentHeroStats
        {
            get
            {
                return opponentHeroStats;
            }
            set
            {
                if (opponentHeroStats != value)
                {
                    opponentHeroStats = value;
                    UpdateControlsOnHeroChange();                    
                }

                UpdateDataFromControls();
                UpdateControlsOnHeroChange();
            }
        }
        
        private PickPanelData data;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        public PickPanelData Data 
        {
            get
            {
                if (data == null)
                {
                    data = new PickPanelData();
                }
                return data;
            }
            set
            {
                data = value;
                SetControlsWithData(value);
            }
        }


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
                UnitBtn.Text = (value != null) ? value.Name : "Pick";
            }
        }

        private bool updatingControlsFromData;
        private Control[] standardModeControls;
        private Control[] scientificModeControls;
        private ComboBox[] secondarySkillComboboxes;
        private CheckBox[] spellCheckboxes;        

        public PickPanel()
        {
            InitializeComponent();            

            mode = ApplicationMode.Scientific;

            standardModeControls = new Control[] 
            {
                HeroComboBox,
                HeroLevelLbl,
                HeroLevelUpDn,
                HeroOffenseLbl,
                HeroOffenseComboBox,
                HeroArcheryLbl,
                HeroArcheryComboBox,
                HeroArmorerLbl,
                HeroArmorerComboBox
            };

            scientificModeControls = new Control[]
            {
                HeroAirLbl,
                HeroAirComboBox,
                HeroFireLbl,
                HeroFireComboBox,
                HeroEarthLbl,
                HeroEarthComboBox, 
                HeroWaterLbl,
                HeroWaterComboBox,
                ScientificModePanel
            };

            secondarySkillComboboxes = new ComboBox[]
            {
                HeroOffenseComboBox,
                HeroArcheryComboBox,
                HeroArmorerComboBox,

                HeroAirComboBox,
                HeroFireComboBox,
                HeroEarthComboBox,
                HeroWaterComboBox
            };

            spellCheckboxes = new CheckBox[]
            {
                BlessChbx,
                BloodlustChbx,
                FrenzyChbx,
                PrayerChbx,
                PrecisionChbx,
                SlayerChbx,

                ShieldChbx,
                StoneSkinChbx,
                AirShieldChbx,

                DisruptingRayChbx,
                CurseChbx,
                WeaknessChbx
            };

            foreach (ComboBox comboBox in secondarySkillComboboxes)
            {
                comboBox.DataSource = SecondarySkillLevel.Levels();
                comboBox.DisplayMember = "Name";
            }
            
            HasHeroChbx.CheckedChanged += ControlValueChanged;
            HasHeroChbx.CheckedChanged += HeroComboBoxValueChanged;
            HeroComboBox.SelectedValueChanged += ControlValueChanged;
            HeroComboBox.SelectedValueChanged += HeroComboBoxValueChanged;
            HeroLevelUpDn.ValueChanged += ControlValueChanged;
            HeroAttackUpDn.ValueChanged += ControlValueChanged;
            HeroDefenseUpDn.ValueChanged += ControlValueChanged;
            HeroOffenseComboBox.SelectedValueChanged += ControlValueChanged;
            HeroArcheryComboBox.SelectedValueChanged += ControlValueChanged;
            HeroArmorerComboBox.SelectedValueChanged += ControlValueChanged;
            HeroAirComboBox.SelectedValueChanged += ControlValueChanged;
            HeroFireComboBox.SelectedValueChanged += ControlValueChanged;
            HeroEarthComboBox.SelectedValueChanged += ControlValueChanged;
            HeroWaterComboBox.SelectedValueChanged += ControlValueChanged;
            BlessChbx.CheckedChanged += ControlValueChanged;
            BloodlustChbx.CheckedChanged += ControlValueChanged;
            FrenzyChbx.CheckedChanged += ControlValueChanged;   
            PrayerChbx.CheckedChanged += ControlValueChanged;
            PrecisionChbx.CheckedChanged += ControlValueChanged;
            SlayerChbx.CheckedChanged += ControlValueChanged;
            ShieldChbx.CheckedChanged += ControlValueChanged;
            StoneSkinChbx.CheckedChanged += ControlValueChanged;
            AirShieldChbx.CheckedChanged += ControlValueChanged;            
            DisruptingRayChbx.CheckedChanged += ControlValueChanged;
            CurseChbx.CheckedChanged += ControlValueChanged;
            WeaknessChbx.CheckedChanged += ControlValueChanged;
        }        

        private void PickPanel_Load(object sender, EventArgs e)
        {
            UpdateControlsOnHeroChange();
            ModeUpdated();
        }        

        private void UpdateHeroComboBox()
        {
            if (heroes == null)            
            {
                return;
            }

            var heroComboBoxItems = new List<KeyValuePair<string, Hero>>();
            heroComboBoxItems.Add(new KeyValuePair<string, Hero>("No hero", null));            
            foreach (Hero hero in heroes)
            {
                heroComboBoxItems.Add(new KeyValuePair<string, Hero>(hero.Name, hero));
            }

            HeroComboBox.DisplayMember = "Key";
            HeroComboBox.ValueMember = "Value";
            HeroComboBox.DataSource = heroComboBoxItems;
        }

        private void ModeUpdated()
        {
            bool standardVisible = (Mode == ApplicationMode.Standard || Mode == ApplicationMode.Scientific);
            bool scientificVisible = (Mode == ApplicationMode.Scientific);

            foreach (Control control in standardModeControls)
            {
                control.Visible = standardVisible;
            }

            foreach (Control control in scientificModeControls)
            {
                control.Visible = scientificVisible;
            }

            HasHeroChbx.Visible = !standardVisible;
            HeroComboBox.Visible = standardVisible;

            switch (Mode)
            {
                case ApplicationMode.Simple:                    
                    this.Height = StandardModePanel.Top - 8;
                    break;
                case ApplicationMode.Standard:                    
                    this.Height = StandardModePanel.Top + StandardModePanel.Height;
                    break;
                case ApplicationMode.Scientific:                    
                    this.Height = ScientificModePanel.Top + ScientificModePanel.Height;
                    break;
            }
        }

        void UpdateDataFromControls() 
        {
            Data.Spells.Clear();

            Data.Unit = PickedUnit;           

            Hero hero = GetSelectedHero();
            if (hero != null)
            {
                HeroStats stats = Data.HeroStats;

                stats.Hero = hero;
                stats.Attack = (int)HeroAttackUpDn.Value;
                stats.Defense = (int)HeroDefenseUpDn.Value;

                if (Mode != ApplicationMode.Simple)
                {
                    stats.Level = (int)HeroLevelUpDn.Value;

                    stats.SetLevelForSecondarySkillType(typeof(Offense), (SecondarySkillLevel)HeroOffenseComboBox.SelectedValue);
                    stats.SetLevelForSecondarySkillType(typeof(Archery), (SecondarySkillLevel)HeroArcheryComboBox.SelectedValue);
                    stats.SetLevelForSecondarySkillType(typeof(Armorer), (SecondarySkillLevel)HeroArmorerComboBox.SelectedValue);                    

                    if (Mode == ApplicationMode.Scientific)
                    {
                        stats.SetLevelForSecondarySkillType(typeof(AirMagic), (SecondarySkillLevel)HeroAirComboBox.SelectedValue);
                        stats.SetLevelForSecondarySkillType(typeof(FireMagic), (SecondarySkillLevel)HeroFireComboBox.SelectedValue);
                        stats.SetLevelForSecondarySkillType(typeof(EarthMagic), (SecondarySkillLevel)HeroEarthComboBox.SelectedValue);
                        stats.SetLevelForSecondarySkillType(typeof(WaterMagic), (SecondarySkillLevel)HeroWaterComboBox.SelectedValue);

                        CheckSpellCheckbox(BlessChbx, typeof(Bless), Data.Spells, stats);
                        CheckSpellCheckbox(BloodlustChbx, typeof(Bloodlust), Data.Spells, stats);
                        CheckSpellCheckbox(FrenzyChbx, typeof(Frenzy), Data.Spells, stats);
                        CheckSpellCheckbox(PrayerChbx, typeof(Prayer), Data.Spells, stats);
                        CheckSpellCheckbox(PrecisionChbx, typeof(Precision), Data.Spells, stats);
                        CheckSpellCheckbox(SlayerChbx, typeof(Slayer), Data.Spells, stats);

                        CheckSpellCheckbox(ShieldChbx, typeof(Shield), Data.Spells, stats);
                        CheckSpellCheckbox(StoneSkinChbx, typeof(StoneSkin), Data.Spells, stats);
                        CheckSpellCheckbox(AirShieldChbx, typeof(AirShield), Data.Spells, stats);
                    }
                }
            }
            else
            {
                data.HeroStats = null;
            }

            if (OpponentHeroStats != null)
            {
                CheckSpellCheckbox(DisruptingRayChbx, typeof(DisruptingRay), Data.Spells, OpponentHeroStats);
                CheckSpellCheckbox(CurseChbx, typeof(Curse), Data.Spells, OpponentHeroStats);
                CheckSpellCheckbox(WeaknessChbx, typeof(Weakness), Data.Spells, OpponentHeroStats);
            }
        }

        private void SetControlsWithData(PickPanelData data)
        {
            updatingControlsFromData = true;

            PickedUnit = data.Unit;

            if (Mode == ApplicationMode.Simple)
            {
                HasHeroChbx.Checked = (data.HeroStats != null);
            }
            else if (HeroComboBox.DataSource != null)
            {
                if (data.HeroStats != null && data.HeroStats.Hero != null)
                {
                    HeroComboBox.SelectedValue = data.HeroStats.Hero;
                }
                else
                {
                    HeroComboBox.SelectedIndex = 0;
                }                
            }

            foreach (ComboBox comboBox in secondarySkillComboboxes)
            {
                comboBox.SelectedItem = SecondarySkillLevel.None;
            }

            foreach (CheckBox checkBox in spellCheckboxes)
            {
                checkBox.Checked = false;
            }

            if (data.HeroStats != null)
            {
                HeroAttackUpDn.Value = data.HeroStats.Attack;
                HeroDefenseUpDn.Value = data.HeroStats.Defense;

                if (Mode != ApplicationMode.Simple)
                {
                    HeroLevelUpDn.Value = data.HeroStats.Level;

                    foreach (SecondarySkill skill in data.HeroStats.SecondarySkills)
                    {
                        ComboBox comboBox = ComboBoxForSecondarySkill(skill);
                        if (comboBox != null)
                        {
                            comboBox.SelectedItem = skill.SkillLevel;
                        }
                    }
                   
                    foreach (ModifierSpell spell in data.Spells)
                    {
                        CheckBox chbx = CheckBoxForSpell(spell);
                        chbx.Checked = true;                        
                    }
                }
            }
            else
            {
                HeroAttackUpDn.Value = 0;
                HeroDefenseUpDn.Value = 0;

                if (Mode != ApplicationMode.Simple)
                {
                    HeroLevelUpDn.Value = 1;
                }
            }

            updatingControlsFromData = false;
            UpdateData();
        }

        private Hero GetSelectedHero()
        {
            if (Heroes == null)
            {
                return null;
            }

            if (Mode == ApplicationMode.Simple)
            {
                return (HasHeroChbx.Checked) ? heroes[0] : null;                
            }
            else
            {
                return (Hero)HeroComboBox.SelectedValue;                                
            }        
        }        

        private void CheckSpellCheckbox(CheckBox chbx, Type spellType, List<ModifierSpell> spells, HeroStats heroStats)
        {
            if (chbx.Checked)
            {
                ModifierSpell spell = (ModifierSpell)Activator.CreateInstance(spellType);
                spell.CasterStats = heroStats.SpellCasterStatsForSpell(spell);

                spells.Add(spell);
            }
        }

        private ComboBox ComboBoxForSecondarySkill(SecondarySkill skill)
        {
            Type skillType = skill.GetType();

            if (skillType == typeof(Offense)) { return HeroOffenseComboBox; }
            if (skillType == typeof(Archery)) { return HeroArcheryComboBox; }
            if (skillType == typeof(Armorer)) { return HeroArmorerComboBox; }

            if (skillType == typeof(AirMagic)) { return HeroAirComboBox; }
            if (skillType == typeof(FireMagic)) { return HeroFireComboBox; }
            if (skillType == typeof(EarthMagic)) { return HeroEarthComboBox; }
            if (skillType == typeof(WaterMagic)) { return HeroWaterComboBox; }

            return null;
        }

        private CheckBox CheckBoxForSpell(ModifierSpell spell)
        {
            Type spellType = spell.GetType();

            if (spellType == typeof(Bless)) { return BlessChbx; }
            if (spellType == typeof(Bloodlust)) { return BloodlustChbx; }
            if (spellType == typeof(Frenzy)) { return FrenzyChbx; }
            if (spellType == typeof(Prayer)) { return PrayerChbx; }
            if (spellType == typeof(Precision)) { return PrecisionChbx; }
            if (spellType == typeof(Slayer)) { return SlayerChbx; }

            if (spellType == typeof(Shield)) { return ShieldChbx; }
            if (spellType == typeof(StoneSkin)) { return StoneSkinChbx; }
            if (spellType == typeof(AirShield)) { return AirShieldChbx; }

            if (spellType == typeof(DisruptingRay)) { return DisruptingRayChbx; }
            if (spellType == typeof(Curse)) { return CurseChbx; }
            if (spellType == typeof(Weakness)) { return WeaknessChbx; }

            return null;
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void HeroComboBoxValueChanged(object sender, EventArgs e)
        {
            UpdateControlsOnHeroChange();
        }

        private void UpdateData()
        {
            if (!updatingControlsFromData)
            {
                UpdateDataFromControls();            

                if (DataChanged != null)
                {
                    DataChanged(this, null);
                }
            }
        }

        private void UpdateControlsOnHeroChange()
        {
            SynchronizeHeroControls(Mode);
            
            bool hasHero = GetSelectedHero() != null;
            bool hasOpponentHero = false;
            if (opponentHeroStats != null)
            {
                if (opponentHeroStats.Hero != null)
                {
                    hasOpponentHero = opponentHeroStats != null;
                }
            }

            HeroAttackLbl.Enabled = hasHero;
            HeroDefenseLbl.Enabled = hasHero;
            HeroAttackUpDn.Enabled = hasHero;
            HeroDefenseUpDn.Enabled = hasHero;
            HeroLevelLbl.Enabled = hasHero;
            HeroLevelUpDn.Enabled = hasHero;

            StandardModePanel.Enabled = hasHero;

            foreach (Control control in ScientificModePanel.Controls)
            {
                control.Enabled = hasHero;
            }

            DisruptingRayChbx.Enabled = hasOpponentHero;
            CurseChbx.Enabled = hasOpponentHero;
            WeaknessChbx.Enabled = hasOpponentHero;
        }

        private void SynchronizeHeroControls(ApplicationMode sourceMode)
        {
            if (Heroes == null)
            {
                return;
            }

            if (sourceMode != ApplicationMode.Simple)
            {
                HasHeroChbx.Checked = (HeroComboBox.SelectedValue != null);                
            }
            else
            {
                if (HasHeroChbx.Checked)
                {
                    if (HeroComboBox.SelectedValue == null)
                    {
                        HeroComboBox.SelectedValue = heroes[0];
                    }
                }
                else
                {
                    HeroComboBox.SelectedIndex = 0;
                }
            }
        }

        private void UnitBtn_Click(object sender, EventArgs e)
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

    public class PickPanelData
    {        
        public Unit Unit { get; set; }

        private HeroStats heroStats;
        public HeroStats HeroStats
        {
            get
            {
                if (heroStats == null)
                {
                    heroStats = new HeroStats();
                }
                return heroStats;
            }
            set
            {
                heroStats = value;
            }
        }

        private List<ModifierSpell> spells;
        public List<ModifierSpell> Spells
        {
            get
            {
                if (spells == null)
                {
                    spells = new List<ModifierSpell>();
                }
                return spells;
            }
            set
            {
                spells = value;
            }
        }
    }
}
