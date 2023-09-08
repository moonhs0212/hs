
using EL_DC_Charger.common.application;
using System;
using System.Text;
using System.Windows.Forms;

namespace EL_DC_Charger.JSH_Base.KeyPadForm
{
    public partial class Form_Keypads : Form
    {
        public Form_Keypads()
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
            AssignEvent(this);
        }


        void clickEvent(object sender, EventArgs e)
        {
            

            string input = ((Button)sender).Text;
            if (input.Length < 1)
                return;

            byte[] input_Ascii = Encoding.ASCII.GetBytes(input);

            if (input.Length == 1 && input_Ascii.Length == 1)
            {
                switch (mInputType)
                {
                    case INPUTTYPE.NONE:
                        break;
                    case INPUTTYPE.TEXT:
                        tb_input.Text += input;
                        break;
                    case INPUTTYPE.INTEGER:
                        if (input_Ascii[0] >= 0x30 && input_Ascii[0] <= 0x39)
                        {
                            tb_input.Text += input;
                        }
                        break;
                    case INPUTTYPE.FLOAT:
                        if (input_Ascii[0] >= 0x2E && input_Ascii[0] <= 0x39 && input_Ascii[0] != 0x2F)
                        {
                            if (input.Equals("."))
                            {
                                if (!input.Contains("."))
                                    tb_input.Text += input;
                            }
                            else
                            {
                                tb_input.Text += input;
                            }
                        }
                        break;
                }
            }
            else
            {
                if (System.Object.ReferenceEquals(sender, btn_barrow))
                {
                    if (tb_input.Text.Length < 2)
                        tb_input.Text = "";
                    else
                        tb_input.Text = tb_input.Text.Substring(0, tb_input.Text.Length - 1);
                }
                else if (System.Object.ReferenceEquals(sender, btn_shift))
                {
                    bIsChecked_Shift = !bIsChecked_Shift;
                    setKeyboardByShift();
                }
                else if (System.Object.ReferenceEquals(sender, btn_enter))
                {
                    if (mListener != null)
                        mListener.textChanged(mLabelIndex, mText_Before, tb_input.Text);
                }

            }

        }

        private void setKeyboardByShift()
        {
            if (bIsChecked_Shift)
            {
                btn_a.Text = "A"; btn_b.Text = "B"; btn_c.Text = "C"; btn_d.Text = "D";
                btn_e.Text = "E"; btn_f.Text = "F"; btn_g.Text = "G"; btn_h.Text = "H";
                btn_i.Text = "I"; btn_j.Text = "J"; btn_k.Text = "K"; btn_l.Text = "L";
                btn_m.Text = "M"; btn_n.Text = "N"; btn_o.Text = "O"; btn_p.Text = "P";
                btn_q.Text = "Q"; btn_r.Text = "R"; btn_s.Text = "S"; btn_t.Text = "T";
                btn_u.Text = "U"; btn_v.Text = "V"; btn_w.Text = "W"; btn_x.Text = "X";
                btn_y.Text = "Y"; btn_z.Text = "Z";
            }
            else
            {
                btn_a.Text = "a"; btn_b.Text = "b"; btn_c.Text = "c"; btn_d.Text = "d";
                btn_e.Text = "e"; btn_f.Text = "f"; btn_g.Text = "g"; btn_h.Text = "h";
                btn_i.Text = "i"; btn_j.Text = "j"; btn_k.Text = "k"; btn_l.Text = "i";
                btn_m.Text = "m"; btn_n.Text = "n"; btn_o.Text = "o"; btn_p.Text = "p";
                btn_q.Text = "q"; btn_r.Text = "r"; btn_s.Text = "s"; btn_t.Text = "t";
                btn_u.Text = "u"; btn_v.Text = "v"; btn_w.Text = "w"; btn_x.Text = "x";
                btn_y.Text = "y"; btn_z.Text = "z";
            }
        }


        private void AssignEvent(Control control)
        {
            foreach (Control child in control.Controls)
            {
                control.Click += clickEvent;
            }
        }

        protected int mLabelIndex = 0;
        protected string mText_Before = "";
        protected INPUTTYPE mInputType = INPUTTYPE.TEXT;
        protected OnTextChangeListner mListener = null;
        public void setTextListener(int labelIndex, string before, INPUTTYPE inputType, OnTextChangeListner listener)
        {
            mLabelIndex = labelIndex;
            mText_Before = before;
            mInputType = inputType;
            mListener = listener;
        }

        public void setTextListener_None()
        {
            mLabelIndex = -1;
            mText_Before = "";
            mInputType = INPUTTYPE.NONE;
            mListener = null;
        }



        protected bool bIsChecked_Shift = false;

    }


    public enum INPUTTYPE
    {
        NONE,
        TEXT,
        INTEGER,
        FLOAT
    }

    public interface OnTextChangeListner
    {
        void textChanged(int labelIndex, string before, string after);
    }
}
