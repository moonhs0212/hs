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
    public partial class UC_MainPage_Include_SettingMain : UserControl
    {
        public UC_MainPage_Include_SettingMain()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            flowLayoutPanel1.Controls.Add(new UC_SettingMain_Include_SideBar());
            panel1.Controls.Add(new UC_SettingMain_Include_Content_SerialPort_Setting());
        }
    }
}
