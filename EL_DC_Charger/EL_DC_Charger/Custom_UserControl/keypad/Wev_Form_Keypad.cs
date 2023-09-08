using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
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
    public partial class Wev_Form_Keypad : Form, IOnClickListener_Button, IKeypad_Manager, IOnButtonClickListener
    {

        protected int mKeypad_Type;
        public Wev_Form_Keypad(int _mKeypad_Type = Wev_Keypad_Type.NUMBER)
        {
            InitializeComponent();

            mKeypad_Type = _mKeypad_Type;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            //Width = EL_MyApplication_Base.SCREEN_WIDTH;
            //Height = EL_MyApplication_Base.SCREEN_HEIGHT;
            TopMost = true;
            setKeypad_Number();

        }
        Wev_Keypad_Alphabet mKeypad_Alphabet = new Wev_Keypad_Alphabet();
        Wev_Keypad_Number mKeypad_Number = new Wev_Keypad_Number();
        public void onClick(object obj)
        {
            //
            buttonClick(obj, EventArgs.Empty);
        }

        UserControl mTemp_Keypad = null;

        public void setKeypad_Number()
        {


            if (mTemp_Keypad == null || !mTemp_Keypad.Equals(mKeypad_Number))
            {
                mTemp_Keypad = mKeypad_Number;
                panel2.Controls.Clear();
                mKeypad_Number.setOnButtonClickListener(this);
                panel2.Controls.Add(mKeypad_Number);
            }
        }

        public void setKeypad_Alphabet()
        {
            if (mKeypad_Type == Wev_Keypad_Type.NUMBER)
                return;

            if (mTemp_Keypad == null || !mTemp_Keypad.Equals(mKeypad_Alphabet))
            {
                mTemp_Keypad = mKeypad_Alphabet;
                panel1.Controls.Clear();
                mKeypad_Alphabet.setOnButtonClickListener(this);
                panel1.Controls.Add(mKeypad_Alphabet);
            }
        }




        private void button_number_Click(object sender, EventArgs e)
        {
            setKeypad_Number();
        }

        private void button_alphabet_Click(object sender, EventArgs e)
        {
            setKeypad_Alphabet();
        }

        private void button_Shift_Click(object sender, EventArgs e)
        {
            if (mTemp_Keypad.Equals(mKeypad_Alphabet))
            {
                mKeypad_Alphabet.convertCase();
            }
        }


        private void buttonClick(object sender, EventArgs e)
        {
            if (((Button)sender).Text.Equals("←"))
            {
                button_back_Click(sender, e);
            }
            else if (((Button)sender).Text.Equals("전체삭제"))
            {
                button_clear_Click(sender, e);
            }
            else
            {

                if (label_input.Text.Length < 6)
                {
                    label_input.Text += ((Button)sender).Text;
                }
            }
        }

        protected string mValue_Input = "";

        protected string mValue_Init = "";
        protected int mValue_Maximum = 0;
        protected int mValue_Minimum = 0;

        protected void setVariable(string Value_Init, int maximum, int minimum)
        {

            mValue_Init = Value_Init;
            mValue_Input = mValue_Init;
            mValue_Maximum = maximum;
            mValue_Minimum = minimum;
            label_input.Text = "" + mValue_Init;
        }

        protected object mObj = null;

        public void setVariable(object obj, int type, int lengthMax, int lengthMin, string title, string content, string contentDescript)
        {
            mObj = obj;
            mKeypad_Type = type;
            //label_subtitle.Text = title;
            setVariable(content, lengthMax, lengthMin);
            label_notify.Text = "";
            //label_unit.Text = contentDescript;
            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:
                    setKeypad_Number();
                    break;
                default:
                    setKeypad_Alphabet();
                    break;
            }
        }

        public void setKeypad_EventListener(IKeypad_EventListener eventListener)
        {
            mKeypad_EventListener = eventListener;
        }

        IKeypad_EventListener mKeypad_EventListener = null;

        private void setText()
        {
            label_input.Text += mValue_Input;

        }

        private void button_back_Click(object sender, EventArgs e)
        {
            if (label_input.Text.Length < 1)
                return;
            label_input.Text = label_input.Text.Substring(0, label_input.Text.Length - 1);
        }

        int getValue_Intput_Integer()
        {
            if (mValue_Input.Length < 1)
                return 0;

            int input = Int32.Parse(mValue_Input);
            return input;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            label_input.Text = "";
        }

        public void onClick_Confirm(object sender)
        {
            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:
                    int input = getValue_Intput_Integer();
                    if (input < mValue_Minimum)
                    {
                        label_notify.Text = "입력값을 확인해 주세요.";
                        return;
                    }

                    mKeypad_EventListener.onEnterComplete(mObj, 0, "", "" + mValue_Init, "" + mValue_Input);
                    this.Hide();
                    break;
                case Wev_Keypad_Type.TEXT:
                case Wev_Keypad_Type.PASSWORD:

                    if (mValue_Input.Length < mValue_Minimum)
                    {
                        label_notify.Text = "입력값을 확인해 주세요.";
                        return;
                    }

                    mKeypad_EventListener.onEnterComplete(mObj, 0, "", "" + mValue_Init, "" + mValue_Input);
                    this.Hide();
                    break;
                case Wev_Keypad_Type.NONFORMAT:
                    mKeypad_EventListener.onEnterComplete(mObj, 0, "", "" + mValue_Init, "" + mValue_Input);
                    this.Hide();
                    break;
            }
        }

        public void onClick_Cancel(object sender)
        {
            this.Hide();
            //mKeypad_EventListener.onCancel(mObj, 0, "" + label_subtitle.Text, "" + mValue_Init);
        }

        private void button_complete_Click(object sender, EventArgs e)
        {
            onClick_Confirm(sender);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            string high = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.ADMIN_PASSWORD_HIGH);
            string middle = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.ADMIN_PASSWORD_MIDDLE);
            string low = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.ADMIN_PASSWORD_LOW);

            if (label_input.Text.Equals(high))
            {
                this.Close();
                EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.MANUFACTUR);

                if (mKeypad_Type == Wev_Keypad_Type.CONFIRM)
                    DialogResult = DialogResult.OK;
                else
                    EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();



            }
            else if (label_input.Text.Equals(middle))
            {
                this.Close();
                EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.ADMIN);
                EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();
            }
            else if (label_input.Text.Equals(low))
            {
                this.Close();
                EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.USER);
                EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();
            }
            else
            {
                label_notify.Text = "입력된 암호를 확인하세요.";
            }

        }
    }
}

