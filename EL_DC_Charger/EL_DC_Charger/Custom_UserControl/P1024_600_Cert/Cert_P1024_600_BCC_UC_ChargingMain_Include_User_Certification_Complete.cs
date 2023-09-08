﻿using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.viewmanager;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
        }

        public int getChannelIndex()
        {
            return mChannelIndex;
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void initVariable()
        {

        }

        public void updateView()
        {

        }

        public void setText(int indexArray, string text)
        {
            
        }

        

        public Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            pictureBox_progressbar.Parent = panel1;

            mViewManager_ProgressCircle = new Wev_ViewManager_ProgressCircle(pictureBox_progressbar);
            mViewManager_ProgressCircle.Start();

            Wev_ImageButtonManager_Home btn_Home = new Wev_ImageButtonManager_Home(pb_home);

        }


        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        Wev_ViewManager_ProgressCircle mViewManager_ProgressCircle = null;
    }
}
