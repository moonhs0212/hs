using EL_DC_Charger.common.interf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad
{
    public partial class Wev_Keypad_Number : UserControl
    {
        public Wev_Keypad_Number()
        {
            InitializeComponent();
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    ((Button)c).Click += Wev_Keypad_Alphabet_Click;
                }
            }
        }

        private void Wev_Keypad_Alphabet_Click(object sender, EventArgs e)
        {
            if (mListener != null)
            {
                mListener.onClick(sender);
            }
        }

        protected IOnButtonClickListener mListener = null;
        public void setOnButtonClickListener(IOnButtonClickListener listener)
        {
            mListener = listener;
        }
    }
}
