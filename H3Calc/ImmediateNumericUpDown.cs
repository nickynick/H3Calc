using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace H3Calc
{
    public class ImmediateNumericUpDown : NumericUpDown
    {
        public ImmediateNumericUpDown() : base()
        {
        }

        bool bTyping = false;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (UserEdit)
            {
                string str = this.Text;

                Decimal val;
                if (Decimal.TryParse(str, out val))
                {
                    if ((val >= Minimum) && (val <= Maximum))
                    {
                        bTyping = true;
                        Value = val;
                        bTyping = false;
                    }
                }
            }
        }

        protected override void UpdateEditText()
        {
            if (bTyping)
                return;

            base.UpdateEditText();
        }

        protected override void OnValidated(EventArgs e)
        {
            UpdateEditText();
            base.OnValidated(e);
        }
    }
}
