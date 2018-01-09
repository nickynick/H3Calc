using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using H3Calc.Engine;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace H3Calc
{
    public partial class UnitPicker : Form
    {
        public List<Unit> Units { get; set; }

        public Dictionary<int, int> RowColumnsCount = new Dictionary<int, int>()
        {
            {0,  13},{1, 13 },{2, 13 },{3, 13 },{4, 13 },{5, 13 },{6, 13 },{7, 13 },{8, 13 },
            {9, 14 },
            {10, 14 },
            {11, 4 }
        };

        public bool UnitExists(int clickedRow, int clickedColumn)
        {
            if (!RowColumnsCount.ContainsKey(clickedRow) || (clickedColumn > RowColumnsCount[clickedRow] ) )
            {
                return false;
            }

            return true;
        }

        public int CalculateUnitId(int clickedRow, int clickedColumn)
        {
            var unitId = 0;

            for (var i = 0; i <= clickedRow; i++)
            {
                if (i < clickedRow)
                {
                    unitId += (i == 0) ? RowColumnsCount[i] : RowColumnsCount[i] + 1;
                }
                else
                {
                    unitId += (i == 0) ? clickedColumn : clickedColumn + 1;
                }
            }

            return unitId;
        }


        public event EventHandler<UnitEventArgs> UnitPicked;

        public UnitPicker(List<Unit> units)
        {
            InitializeComponent();
            this.ClientSize = new Size(860, 768);

            Units = units;
        }        

        private Unit UnitFromPicker(int x, int y)
        {
            const int kPortraitWidth = 50;
            const int kPortraitHeight = 55;
            const int kPadding = 4;

            if ((x % (kPortraitWidth + kPadding) < kPadding) || (y % (kPortraitHeight + kPadding) < kPadding))
            {
                return null;
            }

            int row = y / (kPortraitHeight + kPadding);
            int column = x / (kPortraitWidth + kPadding);

            if (!UnitExists(row, column))
            {
                return null;
            }

            int unitId = CalculateUnitId(row, column);
            return Units.FirstOrDefault(u => u.Id == unitId);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Unit unit = UnitFromPicker(e.X, e.Y);
            pictureBox1.Cursor = (unit != null) ? Cursors.Hand : Cursors.Default;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
            {
            Unit unit = UnitFromPicker(e.X, e.Y);

            if (unit != null && UnitPicked != null)
            {
                UnitPicked(this, new UnitEventArgs(unit));
            }            
        }
    }

    public class UnitEventArgs : EventArgs
    {
        public UnitEventArgs(Unit unit)
        {
            Unit = unit;
        }

        public Unit Unit { get; set; }
    }
}
