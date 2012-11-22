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
        
        protected DamageCalculator calculator;

        protected Control[] AttackerHeroControls;
        protected Control[] DefenderHeroControls;        

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

            ReadTerrainData();
            ReadHeroData();            
            
            calculator = new DamageCalculator();                                  

            terrainComboBox.DataSource = terrains;
            terrainComboBox.DisplayMember = "Name";

            attackerCountUpDn.ValueChanged += ControlValueChanged;
            defenderCountUpDn.ValueChanged += ControlValueChanged;

            attackerPickPanel.Heroes = defenderPickPanel.Heroes = heroes;
            attackerPickPanel.Mode = defenderPickPanel.Mode = Mode;                        

            attackerPickPanel.DataChanged += PickPanelDataChanged;
            defenderPickPanel.DataChanged += PickPanelDataChanged;

            terrainComboBox.SelectedValueChanged += ControlValueChanged;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControlsOnModeChange();            
            UpdateCalculatedDamage();            
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

            Hero genericHero = new Hero();
            genericHero.Name = "Generic hero";
            heroes.Insert(0, genericHero);
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

            attackerPickPanel.Mode = Mode;
            defenderPickPanel.Mode = Mode;

            // TODO: remove this crap, use proper layout (how?)

            attackerGroupBox.Height = attackerPickPanel.Height + 25;
            defenderGroupBox.Height = defenderPickPanel.Height + 25;

            //resultPanel.Top = terrainGroupBox.Top = attackerGroupBox.Top + attackerGroupBox.Height + 6;

            this.Height = attackerGroupBox.Top + attackerGroupBox.Height + 120;            
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

        private void menuItemMode_Click(object sender, EventArgs e)
        {
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
