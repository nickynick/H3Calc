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

            combatDamagePanel.Heroes = heroes;
            combatDamagePanel.Terrains = terrains;
            combatDamagePanel.Mode = Mode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControlsOnModeChange();                        
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

            combatDamagePanel.Mode = Mode;

            Size size = this.ClientSize;
            size.Height = 2 * combatDamagePanel.Top + combatDamagePanel.Height;
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
    }
}
