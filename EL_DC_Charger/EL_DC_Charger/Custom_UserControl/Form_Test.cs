using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl
{
    public partial class Form_Test : Form
    {
        UC_Test_ControlBd mUC_Test_ControlBd = null;
        UC_Test_OCPPComm mUC_Test_OCPP = null;
        public Form_Test()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            mUC_Test_ControlBd = new UC_Test_ControlBd();
            panel_content.Controls.Add(mUC_Test_ControlBd);

            mUC_Test_OCPP = new UC_Test_OCPPComm();
            panel_ocpp.Controls.Add(mUC_Test_OCPP);
        }
    }
}
