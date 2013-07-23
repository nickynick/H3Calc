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
        public event EventHandler DataChanged;

        private CombatDamageCalculatorInputData data;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CombatDamageCalculatorInputData Data
        {
            get
            {
                if (data == null)
                {
                    data = new CombatDamageCalculatorInputData();
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


        private List<Unit> units;
        public List<Unit> Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;

                dataUpdateSuspended = true;
                attackerPickPanel.Units = defenderPickPanel.Units = units;
                dataUpdateSuspended = false;
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

                dataUpdateSuspended = true;
                attackerPickPanel.Heroes = defenderPickPanel.Heroes = heroes;
                dataUpdateSuspended = false;
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

                dataUpdateSuspended = true;
                terrainComboBox.DataSource = terrains;
                terrainComboBox.DisplayMember = "Name";
                dataUpdateSuspended = false;
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

        protected CombatDamageCalculator calculator;

        protected Control[] AttackerHeroControls;
        protected Control[] DefenderHeroControls;
        protected bool dataUpdateSuspended;

        public CombatDamagePanel()
        {
            InitializeComponent();                        

            calculator = new CombatDamageCalculator();                                              

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

            Height = attackerGroupBox.Bottom + terrainGroupBox.Height + 16;               
        }        

        private void UpdateData()
        {
            if (!dataUpdateSuspended)
            {
                attackerPickPanel.OpponentHeroStats = defenderPickPanel.Data.HeroStats;
                defenderPickPanel.OpponentHeroStats = attackerPickPanel.Data.HeroStats;                

                UpdateDataFromControls(Data);

                if (DataChanged != null)
                {
                    DataChanged(this, null);
                }

                UpdateCalculatedDamage();
            }
        }

        void UpdateDataFromControls(CombatDamageCalculatorInputData data)
        {
            data.Attacker = attackerPickPanel.Data.Unit;
            data.Defender = defenderPickPanel.Data.Unit;

            data.Terrain = (Terrain)terrainComboBox.SelectedValue;
            if (data.Terrain.Id == -1)
            {
                data.Terrain = null;
            }

            data.AttackerHeroStats = attackerPickPanel.Data.HeroStats;
            data.AttackerSpells = attackerPickPanel.Data.Spells;

            data.DefenderHeroStats = defenderPickPanel.Data.HeroStats;
            data.DefenderSpells = defenderPickPanel.Data.Spells;

            data.AttackerCount = (int)attackerCountUpDn.Value;
            data.DefenderCount = (int)defenderCountUpDn.Value;   
        }

        void SetControlsWithData(CombatDamageCalculatorInputData data)
        {
            dataUpdateSuspended = true;

            PickPanelData panelData = attackerPickPanel.Data;
            panelData.Unit = data.Attacker;
            panelData.HeroStats = data.AttackerHeroStats;
            panelData.Spells = data.AttackerSpells;
            attackerPickPanel.Data = panelData;

            panelData = defenderPickPanel.Data;
            panelData.Unit = data.Defender;
            panelData.HeroStats = data.DefenderHeroStats;
            panelData.Spells = data.DefenderSpells;
            defenderPickPanel.Data = panelData;

            if (data.Terrain != null)
            {
                terrainComboBox.SelectedValue = data.Terrain;
            }
            else
            {
                terrainComboBox.SelectedIndex = 0;
            }

            attackerCountUpDn.Value = data.AttackerCount;
            defenderCountUpDn.Value = data.DefenderCount;

            dataUpdateSuspended = false;
        }


        private void UpdateCalculatedDamage()
        {
            bool hasInputData = ((Data.Attacker != null) && (Data.Defender != null));
            resultPanel.Visible = hasInputData;
            if (!hasInputData)
            {
                return;
            }            

            int minDamage, maxDamage;
            string notes;
            calculator.CalculateDamage(Data, out minDamage, out maxDamage, out notes);            

            int minKills = minDamage / Data.Defender.InitialStats.Health;
            int maxKills = maxDamage / Data.Defender.InitialStats.Health;

            calculatedDamageLbl.Text = FormatRange(minDamage, maxDamage);
            calculatedKillsLbl.Text = FormatRange(minKills, maxKills);
            notesLbl.Text = (notes != null) ? "(" + notes + ")" : null;

            //// TODO: refactor this crap

            int minRetDamage, tempRetDamage, maxRetDamage;
            string retNotes;

            CombatDamageCalculatorInputData retData = Data.InverseData();

            retData.AttackerCount = Math.Max(0, Data.DefenderCount - minKills);
            calculator.CalculateDamage(retData, out tempRetDamage, out maxRetDamage, out retNotes);

            retData.AttackerCount = Math.Max(0, Data.DefenderCount - maxKills);
            calculator.CalculateDamage(retData, out minRetDamage, out tempRetDamage, out retNotes);

            int minRetKills = minRetDamage / retData.Defender.InitialStats.Health;
            int maxRetKills = maxRetDamage / retData.Defender.InitialStats.Health;

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
            UpdateData();
        }

        private void PickPanelDataChanged(object sender, EventArgs e)
        {
            UpdateData();
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
