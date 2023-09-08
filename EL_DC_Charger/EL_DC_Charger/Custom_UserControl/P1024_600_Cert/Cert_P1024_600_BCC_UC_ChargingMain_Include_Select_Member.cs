using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member : UserControl, IUC_Channel, IOnClickListener_Button, IKeypad_EventListener
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member(int channelIndex) : this()
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
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            pb_member.Parent = this;
            pb_nonmember.Parent = this;

            Wev_ImageButtonManager_Member ibMember = new Wev_ImageButtonManager_Member(pb_member);
            ibMember.setOnClickListener(this);

            Wev_ImageButtonManager_Nonmember ibNonmember = new Wev_ImageButtonManager_Nonmember(pb_nonmember);
            ibNonmember.setOnClickListener(this);

            this.Click += new System.EventHandler(this.pictureBox1_Click);

            //label_chargingunit_member.FlatStyle = FlatStyle.Standard;
            //label_chargingunit_member.Parent = pb_member;

            //label_chargingunit_member.BackColor = Color.Transparent;
            label_chargingunit_member.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST) + "원";

            var pos = this.PointToScreen(label_chargingunit_member.Location);
            pos = pb_member.PointToClient(pos);
            label_chargingunit_member.Parent = pb_member;
            label_chargingunit_member.Location = pos;
            label_chargingunit_member.BackColor = Color.Transparent;


            pos = this.PointToScreen(label_chargingunit_nonmember.Location);
            pos = pb_nonmember.PointToClient(pos);
            label_chargingunit_nonmember.Parent = pb_nonmember;
            label_chargingunit_nonmember.Location = pos;
            label_chargingunit_nonmember.BackColor = Color.Transparent;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UserControl pic = (UserControl)sender;

            int x = Control.MousePosition.X;

            int y = Control.MousePosition.Y;

            Point mousePos = new Point(x, y); //프로그램 내 좌표

            Point mousePosPtoClient = pic.PointToClient(mousePos);  //picbox 내 좌표

            if (mousePosPtoClient.X <= 100 && mousePosPtoClient.Y <= 80)
            {
                DateTime now = DateTime.Now;
                long diff = now.Ticks - ticksSetting;
                Console.WriteLine("diff = " + diff);
                if (ticksSetting == 0 || diff > 15000000)
                    ticksSetting = now.Ticks;
                else
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

            //MessageBox.Show(string.Format("X: {0} Y: {1}", 
            //    mousePosPtoClient.X, mousePosPtoClient.Y));
        }

        long ticksSetting = 0;





        private void label_version_DoubleClick(object sender, EventArgs e)
        {
            form.Show();
        }

        public void setText(int indexArray, string text)
        {
            throw new NotImplementedException();
        }


        Form_SW_Version form = new Form_SW_Version();

        private void label_chargingunit_member_Click(object sender, EventArgs e)
        {

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mMemberType = EMemberType.MEMBER;
        }



        private void button_chargingunit_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 10000, 10,
                "충전단가를 입력해 주세요.", "" + EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST),
                "원");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        public void onEnterComplete(object sender, int type, string title, string content, string afterContent)
        {
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST, afterContent);
            label_chargingunit_member.Text = afterContent + "원";
        }

        public void onCancel(object sender, int type, string title, string content)
        {

        }

        public void onClick_Confirm(object sender)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mMemberType = EMemberType.MEMBER;
            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsClick_UsingStart = true;
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        private void button_approval_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.setCommand_Pay_First(200);
        }

        private void button_dealcancel_before_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.setCommand_Pay_Cancel(
                EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.getManager_Send().PacketManager.Packet_AddInfor_Deal_Request_Receive
            );
        }

        private void button_deal_cancel__Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.setCommand_Pay_Cancel(
                100,
                EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.getManager_Send().PacketManager.Packet_AddInfor_Deal_Request_Receive
            );
        }

        private void label_chargingunit_nonmember_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mMemberType = EMemberType.NONMEMBER;
        }

        private void button_approval_Click_1(object sender, EventArgs e)
        {

        }
    }
}
