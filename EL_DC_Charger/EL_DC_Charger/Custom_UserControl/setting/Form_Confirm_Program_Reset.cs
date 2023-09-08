using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EL_DC_Charger.common.application;

namespace EL_DC_Charger.BatteryChange_Charger.Settings
{
    public partial class Form_Confirm_Program_Reset : Form
    {
        public Form_Confirm_Program_Reset()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            Width = EL_MyApplication_Base.SCREEN_WIDTH;
            Height = EL_MyApplication_Base.SCREEN_HEIGHT;

            TopMost = true;
        }

        private void button_program_finish_Click(object sender, EventArgs e)
        {
            EL_Manager_Application.finishApplication();
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            EL_Manager_Application.finishApplication_ConfirmPopup();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_restart_system_Click(object sender, EventArgs e)
        {
            EL_Manager_Application.restartSystem_ConfirmPopup();
        }
    }
}
