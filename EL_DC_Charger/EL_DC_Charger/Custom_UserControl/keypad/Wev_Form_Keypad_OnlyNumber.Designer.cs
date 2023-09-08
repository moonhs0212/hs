
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad
{
    partial class Wev_Form_Keypad_OnlyNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_input = new System.Windows.Forms.Label();
            this.label_subtitle = new System.Windows.Forms.Label();
            this.label_unit = new System.Windows.Forms.Label();
            this.label_notify = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_number = new System.Windows.Forms.Button();
            this.button_alphabet = new System.Windows.Forms.Button();
            this.button_Shift = new System.Windows.Forms.Button();
            this.button_complete = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_input
            // 
            this.label_input.BackColor = System.Drawing.Color.LightGray;
            this.label_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_input.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_input.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_input.Location = new System.Drawing.Point(29, 146);
            this.label_input.Name = "label_input";
            this.label_input.Size = new System.Drawing.Size(366, 125);
            this.label_input.TabIndex = 1;
            this.label_input.Text = "0";
            this.label_input.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_subtitle
            // 
            this.label_subtitle.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_subtitle.Location = new System.Drawing.Point(29, 60);
            this.label_subtitle.Name = "label_subtitle";
            this.label_subtitle.Size = new System.Drawing.Size(401, 72);
            this.label_subtitle.TabIndex = 2;
            this.label_subtitle.Text = "충전단가를 입력해 주세요.";
            this.label_subtitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label_unit
            // 
            this.label_unit.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_unit.Location = new System.Drawing.Point(395, 205);
            this.label_unit.Name = "label_unit";
            this.label_unit.Size = new System.Drawing.Size(50, 66);
            this.label_unit.TabIndex = 2;
            this.label_unit.Text = "원";
            this.label_unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_notify
            // 
            this.label_notify.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_notify.ForeColor = System.Drawing.Color.Red;
            this.label_notify.Location = new System.Drawing.Point(12, 274);
            this.label_notify.Name = "label_notify";
            this.label_notify.Size = new System.Drawing.Size(404, 31);
            this.label_notify.TabIndex = 2;
            this.label_notify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(449, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 563);
            this.panel1.TabIndex = 17;
            // 
            // button_number
            // 
            this.button_number.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_number.Location = new System.Drawing.Point(29, 500);
            this.button_number.Name = "button_number";
            this.button_number.Size = new System.Drawing.Size(91, 75);
            this.button_number.TabIndex = 18;
            this.button_number.Text = "0123\r\n!~@#";
            this.button_number.UseVisualStyleBackColor = true;
            this.button_number.Click += new System.EventHandler(this.button_number_Click);
            // 
            // button_alphabet
            // 
            this.button_alphabet.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_alphabet.Location = new System.Drawing.Point(139, 500);
            this.button_alphabet.Name = "button_alphabet";
            this.button_alphabet.Size = new System.Drawing.Size(91, 75);
            this.button_alphabet.TabIndex = 18;
            this.button_alphabet.Text = "abcd\r\nEFGH";
            this.button_alphabet.UseVisualStyleBackColor = true;
            this.button_alphabet.Click += new System.EventHandler(this.button_alphabet_Click);
            // 
            // button_Shift
            // 
            this.button_Shift.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Shift.Location = new System.Drawing.Point(251, 500);
            this.button_Shift.Name = "button_Shift";
            this.button_Shift.Size = new System.Drawing.Size(165, 75);
            this.button_Shift.TabIndex = 18;
            this.button_Shift.Text = "Shift";
            this.button_Shift.UseVisualStyleBackColor = true;
            this.button_Shift.Click += new System.EventHandler(this.button_Shift_Click);
            // 
            // button_complete
            // 
            this.button_complete.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_complete.Location = new System.Drawing.Point(29, 362);
            this.button_complete.Name = "button_complete";
            this.button_complete.Size = new System.Drawing.Size(165, 75);
            this.button_complete.TabIndex = 18;
            this.button_complete.Text = "확   인";
            this.button_complete.UseVisualStyleBackColor = true;
            this.button_complete.Click += new System.EventHandler(this.button_complete_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_cancel.Location = new System.Drawing.Point(251, 362);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(165, 75);
            this.button_cancel.TabIndex = 18;
            this.button_cancel.Text = "취   소";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Wev_Form_Keypad_OnlyNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_complete);
            this.Controls.Add(this.button_Shift);
            this.Controls.Add(this.button_alphabet);
            this.Controls.Add(this.button_number);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.label_notify);
            this.Controls.Add(this.label_subtitle);
            this.Controls.Add(this.label_input);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Wev_Form_Keypad_OnlyNumber";
            this.Text = "Wev_Form_Keypad";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_input;
        private System.Windows.Forms.Label label_subtitle;
        private System.Windows.Forms.Label label_unit;
        private System.Windows.Forms.Label label_notify;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_number;
        private System.Windows.Forms.Button button_alphabet;
        private System.Windows.Forms.Button button_Shift;
        private System.Windows.Forms.Button button_complete;
        private System.Windows.Forms.Button button_cancel;
    }
}