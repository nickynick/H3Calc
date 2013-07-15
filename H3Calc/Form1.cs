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
using Newtonsoft.Json;

namespace H3Calc
{
    public partial class Form1 : Form
    {
        protected List<Unit> units;
        protected List<Terrain> terrains;
        protected Hero genericHero;
        protected List<Hero> heroes;
        protected List<DamageSpell> damageSpells;   

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

            InitializeData();

            combatDamagePanel.Units = units;
            combatDamagePanel.Heroes = heroes;
            combatDamagePanel.Terrains = terrains;            
            combatDamagePanel.Mode = Mode;
            combatDamagePanel.DataChanged += CombatDamagePanelDataChanged;

            spellDamagePanel.Spells = damageSpells;
            spellDamagePanel.Units = units;
            spellDamagePanel.Heroes = heroes;
            spellDamagePanel.DataChanged += SpellDamagePanelDataChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControlsOnModeChange();                        
        }

        private void InitializeData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            // units
            String unitsJson = System.Text.Encoding.UTF8.GetString(Properties.Resources.units);
            units = JsonConvert.DeserializeObject<List<Unit>>(unitsJson, settings);

            // terrains
            String terrainsJson = System.Text.Encoding.UTF8.GetString(Properties.Resources.terrains);
            terrains = JsonConvert.DeserializeObject<List<Terrain>>(terrainsJson, settings);
            Terrain emptyTerrain = new Terrain { Id = -1, Name = "Don't care" };
            terrains.Insert(0, emptyTerrain);

            // heroes
            String heroesJson = System.Text.Encoding.UTF8.GetString(Properties.Resources.heroes);
            heroes = JsonConvert.DeserializeObject<List<Hero>>(heroesJson, settings);
            heroes = heroes.OrderBy(x => x.Name).ToList();

            genericHero = new Hero();
            genericHero.Name = "Generic hero";
            heroes.Insert(0, genericHero);            

            // damage spells
            damageSpells = new List<DamageSpell>();
            damageSpells.Add(new MagicArrow());
            damageSpells.Add(new FireWall());
            damageSpells.Add(new IceBolt());
            damageSpells.Add(new DeathRipple());
            damageSpells.Add(new LightningBolt());
            damageSpells.Add(new Fireball());
            damageSpells.Add(new LandMine());
            damageSpells.Add(new FrostRing());
            damageSpells.Add(new DestroyUndead());
            damageSpells.Add(new Armageddon());
            damageSpells.Add(new Inferno());
            damageSpells.Add(new MeteorShower());
            damageSpells.Add(new ChainLightning());
            damageSpells.Add(new TitansBolt());
            damageSpells.Add(new Implosion());
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

            combatDamagePanel.Mode = Mode;

            UpdateFormSize();            
        }

        private void UpdateFormSize()
        {
            Control currentPanel;
            if (tabControl.SelectedIndex == 0)
            {
                currentPanel = combatDamagePanel;
            }
            else
            {
                currentPanel = spellDamagePanel;
            }

            tabControl.Height = currentPanel.Bottom + 30;

            Size size = this.ClientSize;
            size.Height = tabControl.Bottom + 10;            
            this.ClientSize = size;
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
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }        

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormSize();
        }

        private void CombatDamagePanelDataChanged(object sender, EventArgs e)
        {
            CombatDamageCalculatorInputData combatData = combatDamagePanel.Data;
            SpellDamageCalculatorData spellData = spellDamagePanel.Data;

            spellData.CasterHeroStats = combatData.AttackerHeroStats;
            spellData.Target = combatData.Defender;

            spellDamagePanel.Data = spellData;
        }

        private void SpellDamagePanelDataChanged(object sender, EventArgs e)
        {
            CombatDamageCalculatorInputData combatData = combatDamagePanel.Data;
            SpellDamageCalculatorData spellData = spellDamagePanel.Data;

            combatData.AttackerHeroStats = spellData.CasterHeroStats;
            combatData.Defender = spellData.Target;

            combatDamagePanel.Data = combatData;
        }
    }
}
