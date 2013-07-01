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
    public partial class CombatDamagePanel : UserControl
    {
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

                attackerPickPanel.Heroes = defenderPickPanel.Heroes = heroes;
            }
        }

        private List<Terrain> terrains;
        public List<Terrain> Terrains
        {
            get
            {
                return terrains;
            }
            set
            {
                terrains = value;

                terrainComboBox.DataSource = terrains;
                terrainComboBox.DisplayMember = "Name";
            }
        }

        private ApplicationMode mode;
        public ApplicationMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;

                UpdateControlsOnModeChange();
                UpdateCalculatedDamage();
            }
        }

        protected DamageCalculator calculator;

        protected Control[] AttackerHeroControls;
        protected Control[] DefenderHeroControls;                


        public CombatDamagePanel()
        {
            InitializeComponent();                        

            calculator = new DamageCalculator();                                              

            attackerCountUpDn.ValueChanged += ControlValueChanged;
            defenderCountUpDn.ValueChanged += ControlValueChanged;

            attackerPickPanel.DataChanged += PickPanelDataChanged;
            defenderPickPanel.DataChanged += PickPanelDataChanged;

            terrainComboBox.SelectedValueChanged += ControlValueChanged;            
        }        

        private void UpdateControlsOnModeChange()
        {
            attackerPickPanel.Mode = defenderPickPanel.Mode = Mode;

            // TODO: remove this crap, use proper layout (how?)

            attackerGroupBox.Height = attackerPickPanel.Height + 25;
            defenderGroupBox.Height = defenderPickPanel.Height + 25;

            //resultPanel.Top = terrainGroupBox.Top = attackerGroupBox.Top + attackerGroupBox.Height + 6;

            this.Height = attackerGroupBox.Bottom + terrainGroupBox.Height + 16;               
        }
        
        private void UpdateCalculatedDamage()
        {
            DamageCalculatorInputData inputData = new DamageCalculatorInputData();

            inputData.Attacker = attackerPickPanel.Data.Unit;
            inputData.Defender = defenderPickPanel.Data.Unit;

            bool haveInputData = ((inputData.Attacker != null) && (inputData.Defender != null));

            resultPanel.Visible = haveInputData;

            if (!haveInputData)
            {
                return;
            }          

            inputData.Terrain = (Terrain)terrainComboBox.SelectedValue;
            if (inputData.Terrain.Id == -1)
            {
                inputData.Terrain = null;
            }

            inputData.AttackerHeroStats = attackerPickPanel.Data.HeroStats;
            inputData.AttackerSpells = attackerPickPanel.Data.Spells;

            inputData.DefenderHeroStats = defenderPickPanel.Data.HeroStats;
            inputData.DefenderSpells = defenderPickPanel.Data.Spells;

            inputData.AttackerCount = (int)attackerCountUpDn.Value;

            int minDamage, maxDamage;
            string notes;
            calculator.CalculateDamage(inputData, out minDamage, out maxDamage, out notes);

            int minKills = minDamage / inputData.Defender.InitialStats.Health;
            int maxKills = maxDamage / inputData.Defender.InitialStats.Health;

            calculatedDamageLbl.Text = FormatRange(minDamage, maxDamage);
            calculatedKillsLbl.Text = FormatRange(minKills, maxKills);
            notesLbl.Text = (notes != null) ? "(" + notes + ")" : null;

            //// TODO: refactor this crap

            inputData.Attacker = defenderPickPanel.Data.Unit;
            inputData.AttackerHeroStats = defenderPickPanel.Data.HeroStats; 
            inputData.AttackerSpells = defenderPickPanel.Data.Spells;

            inputData.Defender = attackerPickPanel.Data.Unit;
            inputData.DefenderHeroStats = attackerPickPanel.Data.HeroStats;
            inputData.DefenderSpells = attackerPickPanel.Data.Spells;

            int minRetDamage, tempRetDamage, maxRetDamage;            
            string retNotes;

            inputData.AttackerCount = Math.Max(0, (int)defenderCountUpDn.Value - minKills);
            calculator.CalculateDamage(inputData, out tempRetDamage, out maxRetDamage, out retNotes);

            inputData.AttackerCount = Math.Max(0, (int)defenderCountUpDn.Value - maxKills);
            calculator.CalculateDamage(inputData, out minRetDamage, out tempRetDamage, out retNotes);

            int minRetKills = minRetDamage / inputData.Defender.InitialStats.Health;
            int maxRetKills = maxRetDamage / inputData.Defender.InitialStats.Health;

            calculatedRetDamageLbl.Text = FormatRange(minRetDamage, maxRetDamage);
            calculatedRetKillsLbl.Text = FormatRange(minRetKills, maxRetKills);
            retNotesLbl.Text = (retNotes != null) ? "(" + retNotes + ")" : null;
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

        private void ControlValueChanged(object sender, EventArgs e)
        {            
            UpdateCalculatedDamage();
        }

        private void PickPanelDataChanged(object sender, EventArgs e)
        {
            attackerPickPanel.OpponentHeroStats = defenderPickPanel.Data.HeroStats;
            defenderPickPanel.OpponentHeroStats = attackerPickPanel.Data.HeroStats;

            UpdateCalculatedDamage();
        }        

        private void swapBtn_Click(object sender, EventArgs e)
        {
            PickPanelData tempData = attackerPickPanel.Data;
            attackerPickPanel.Data = defenderPickPanel.Data;
            defenderPickPanel.Data = tempData;

            attackerPickPanel.OpponentHeroStats = defenderPickPanel.Data.HeroStats;
            defenderPickPanel.OpponentHeroStats = attackerPickPanel.Data.HeroStats;

            int tempCount = (int)attackerCountUpDn.Value;
            attackerCountUpDn.Value = defenderCountUpDn.Value;
            defenderCountUpDn.Value = tempCount;            

            UpdateCalculatedDamage();
        }
    }
}
