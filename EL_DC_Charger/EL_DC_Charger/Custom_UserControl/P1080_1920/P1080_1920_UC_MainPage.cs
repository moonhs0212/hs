using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.JSH_Base.KeyPadForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_MainPage : UserControl, IUC_Main, Manager_AdminEnter.OnChange_AdminEnter
    {
        private DateTime touchStartTime;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                touchStartTime = DateTime.Now;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TimeSpan touchDuration = DateTime.Now - touchStartTime;
                if (touchDuration.TotalSeconds >= EL_MyApplication_Base.SETTING_TOUCH_DELAY)
                {
                    if (EL_DC_Charger_MyApplication.getInstance().getAdminMode() == EAdminMode.NONE)
                    {
                        EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.ADMIN);
                        EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();
                    }
                    else
                    {

                    }
                }
            }
        }

        Image[] imagePaths_Ads = new Image[6];
        int currentIndex_Ads = 0;
        public void ShowNextPage()
        {

            if (currentIndex_Ads < imagePaths_Ads.Length)
            {
                // Load the next image from file
                Image newImage = imagePaths_Ads[currentIndex_Ads];

                // Set the PictureBox's Image property to the new image
                pictureBox1.Image = newImage;

                // Increment the index for the next image
                currentIndex_Ads++;
            }
            else
            {
                currentIndex_Ads = 0;
                Image newImage = imagePaths_Ads[currentIndex_Ads];

                // Set the PictureBox's Image property to the new image
                pictureBox1.Image = newImage;

            }
        }


        Manager_AdminEnter mManager_AdminEnter = null;



        public P1080_1920_UC_MainPage()
        {
            InitializeComponent();

            mManager_AdminEnter = new Manager_AdminEnter(pictureBox1, 5);
            mManager_AdminEnter.setOnChange_AdminEnter(this);

            imagePaths_Ads[0] = Properties.Resources.p1080_1920_ad_01;
            imagePaths_Ads[1] = Properties.Resources.p1080_1920_ad_02;
            imagePaths_Ads[2] = Properties.Resources.p1080_1920_ad_03;
            imagePaths_Ads[3] = Properties.Resources.p1080_1920_ad_04;
            imagePaths_Ads[4] = Properties.Resources.p1080_1920_ad_05;
            imagePaths_Ads[5] = Properties.Resources.p1080_1920_ad_06;
            pictureBox1.MouseDown += new MouseEventHandler(Form1_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(Form1_MouseUp);


            timer_screen.Interval = 10000;
            timer_screen.Enabled = true;
            timer_screen.Start();

            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }
        public Panel getPanel_Main()
        {
            return panel_main;
        }


        public void setContent(int channelIndex, UserControl control)
        {
            if (channelIndex == 0)
            {
                panel_main.Controls.Clear();
                panel_main.Controls.Add(control);
            }
            else
            {
                panel_main2.Controls.Clear();
                panel_main2.Controls.Add(control);
            }
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void setBottombar_Weather()
        {

        }

        public void setBottombar_ProcessStep()
        {

        }

        public void setPanel_Main_CustomUserControl(UserControl control)
        {

        }



        public void setVisible_Button_Back(bool visible)
        {

        }

        public void setText(int indexArray, string text)
        {

        }

        public void setVisible_Button_Home(bool visible)
        {

        }

        public void setHomeBackBtn_Manager(IHomeBackBtn_Manager manager)
        {

        }

        protected IHomeBackBtn_Manager mHomeBackBtn_Manager = null;

        private void timer_screen_Tick(object sender, EventArgs e)
        {
            ShowNextPage();
        }

        public void onChange(List<Integer> touchList)
        {
            if (touchList[0] == 1
                && touchList[1] == 3
                && touchList[2] == 4
                && touchList[3] == 2
                && touchList[4] == 1)
            {
                mManager_AdminEnter.clearEvent();
                if (EL_DC_Charger_MyApplication.getInstance().getAdminMode() == EAdminMode.NONE)
                {
                    EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.ADMIN);
                    EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();
                }
                else
                {

                }
            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Wev_Form_Keypad f = new Wev_Form_Keypad();
            f.Show();

        }

        private void P1080_1920_UC_MainPage_Click(object sender, EventArgs e)
        {

        }
        //public void setHomeBackBtn_Manager()
    }
}
