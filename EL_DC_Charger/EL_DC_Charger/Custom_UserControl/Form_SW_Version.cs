using EL_DC_Charger.common.application;
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
    public partial class Form_SW_Version : Form
    {
        public Form_SW_Version()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            Width = EL_MyApplication_Base.SCREEN_WIDTH;
            Height = EL_MyApplication_Base.SCREEN_HEIGHT;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
