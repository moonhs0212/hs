using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
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
    public partial class UC_SettingMain_Include_SideBar : UserControl
    {
        public UC_SettingMain_Include_SideBar()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

        }

        private void button_finish_application_Click(object sender, EventArgs e)
        {
            EL_Manager_Application.finishApplication();
        }

        private void button_reboot_system_Click(object sender, EventArgs e)
        {
            EL_Manager_Application.restartApplication();
        }
    }
}
