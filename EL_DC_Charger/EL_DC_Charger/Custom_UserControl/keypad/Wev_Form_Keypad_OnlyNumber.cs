using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad
{
    public partial class Wev_Form_Keypad_OnlyNumber : Form, IOnClickListener_Button, IKeypad_Manager, IOnButtonClickListener
    {

        protected int mKeypad_Type = Wev_Keypad_Type.NUMBER;

        public Wev_Form_Keypad_OnlyNumber()
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
            //Wev_ImageButtonManager_Confirm imageButton = new Wev_ImageButtonManager_Confirm(pb_chargingcomplete);
            //imageButton.setOnClickListener(this);

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
                panel1.Controls.Clear();
                mKeypad_Number.setOnButtonClickListener(this);
                panel1.Controls.Add(mKeypad_Number);
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
            label_notify.Text = "";

            if (mKeypad_Type == Wev_Keypad_Type.NONFORMAT)
            {
                string inputText = ((Button)sender).Text;
                if (inputText.Equals("←"))
                {
                    if (mValue_Input.Length > 0)
                    {
                        mValue_Input = mValue_Input.Substring(0, mValue_Input.Length - 1);
                        setText();
                    }
                }
                else if (inputText.Equals("전체삭제"))
                {
                    mValue_Input = "";
                    setText();
                }
                else
                {
                    mValue_Input = mValue_Input + inputText;
                    setText();
                }


                return;
            }

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
                int addValue = Int16.Parse(((Button)sender).Text);

                switch (mKeypad_Type)
                {
                    case Wev_Keypad_Type.NUMBER:
                        int input = getValue_Intput_Integer();
                        input = input * 10 + addValue;
                        if (mValue_Maximum < input)
                        {

                        }
                        else
                        {
                            mValue_Input = "" + input;
                        }
                        break;
                    case Wev_Keypad_Type.TEXT:
                        if (label_input.Text.Length < mValue_Maximum)
                            mValue_Input = mValue_Input + addValue;
                        break;
                }
            }



            setText();
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
            label_subtitle.Text = title;
            setVariable(content, lengthMax, lengthMin);
            label_notify.Text = "";
            label_unit.Text = contentDescript;
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
            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:
                case Wev_Keypad_Type.TEXT:
                case Wev_Keypad_Type.NONFORMAT:
                    label_input.Text = mValue_Input;
                    break;
                case Wev_Keypad_Type.PASSWORD:
                    string inputText = "";
                    for (int i = 0; i < mValue_Input.Length - 1; i++)
                        inputText += "*";
                    inputText = mValue_Input.Substring(label_input.Text.Length - 1, label_input.Text.Length);
                    label_input.Text = inputText;
                    break;
            }

        }

        private void button_back_Click(object sender, EventArgs e)
        {
            label_notify.Text = "";

            if (mValue_Input.Length < 1)
                return;

            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:
                    int input = getValue_Intput_Integer();
                    if (input < 1)
                    {
                        mValue_Input = "0";
                        label_input.Text = "" + mValue_Input;
                        return;
                    }

                    mValue_Input = "" + (input / 10);
                    label_input.Text = "" + mValue_Input;
                    break;
                case Wev_Keypad_Type.NONFORMAT:
                case Wev_Keypad_Type.PASSWORD:
                case Wev_Keypad_Type.TEXT:
                    mValue_Input = mValue_Input.Substring(0, mValue_Input.Length - 1);
                    break;
            }
            setText();

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
            label_notify.Text = "";
            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:

                    mValue_Input = "";

                    break;
                case Wev_Keypad_Type.PASSWORD:
                case Wev_Keypad_Type.TEXT:
                case Wev_Keypad_Type.NONFORMAT:
                    mValue_Input = "";
                    break;
            }
            label_input.Text = "" + mValue_Input;

            setText();

        }

        public void onClick_Confirm(object sender)
        {
            switch (mKeypad_Type)
            {
                case Wev_Keypad_Type.NUMBER:
                    int input = getValue_Intput_Integer();
                    if (input < mValue_Minimum || (label_input.Text.Equals("")))
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
            mKeypad_EventListener.onCancel(mObj, 0, "" + label_subtitle.Text, "" + mValue_Init);
        }

        private void button_complete_Click(object sender, EventArgs e)
        {
            onClick_Confirm(sender);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            onClick_Cancel(sender);
        }
        
    }
}
