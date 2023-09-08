namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad
{
    partial class Wev_Form_Keypad
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_input = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label_notify = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(441, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 570);
            this.panel2.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_input, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_ok, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_notify, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(431, 570);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(425, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "비밀번호를 입력해 주세요.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_input
            // 
            this.label_input.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.label_input, 3);
            this.label_input.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_input.Font = new System.Drawing.Font("굴림", 25F);
            this.label_input.Location = new System.Drawing.Point(50, 152);
            this.label_input.Margin = new System.Windows.Forms.Padding(50, 3, 50, 30);
            this.label_input.MaxLength = 6;
            this.label_input.Name = "label_input";
            this.label_input.PasswordChar = '*';
            this.label_input.ReadOnly = true;
            this.label_input.Size = new System.Drawing.Size(331, 46);
            this.label_input.TabIndex = 1;
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_ok.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_ok.ForeColor = System.Drawing.Color.White;
            this.button_ok.Location = new System.Drawing.Point(25, 345);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(115, 108);
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "확인";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(289, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 108);
            this.button1.TabIndex = 11;
            this.button1.Text = "돌아가기";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_notify
            // 
            this.label_notify.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label_notify, 3);
            this.label_notify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_notify.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.label_notify.ForeColor = System.Drawing.Color.Red;
            this.label_notify.Location = new System.Drawing.Point(3, 228);
            this.label_notify.Name = "label_notify";
            this.label_notify.Size = new System.Drawing.Size(425, 114);
            this.label_notify.TabIndex = 12;
            this.label_notify.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1014, 590);
            this.panel1.TabIndex = 0;
            // 
            // Wev_Form_Keypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Wev_Form_Keypad";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Wev_Form_Keypad";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox label_input;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_notify;
        private System.Windows.Forms.Panel panel1;
    }
}