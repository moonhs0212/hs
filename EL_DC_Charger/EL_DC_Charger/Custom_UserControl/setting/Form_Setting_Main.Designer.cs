
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    partial class Form_Setting_Main
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_main = new System.Windows.Forms.Panel();
            this.button_windowclose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.label_title_step = new System.Windows.Forms.Label();
            this.label_subtitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 600);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_main, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_windowclose, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 600);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel_main, 2);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 74);
            this.panel_main.Margin = new System.Windows.Forms.Padding(0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1024, 526);
            this.panel_main.TabIndex = 6;
            // 
            // button_windowclose
            // 
            this.button_windowclose.BackColor = System.Drawing.Color.Transparent;
            this.button_windowclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_windowclose.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_windowclose.Location = new System.Drawing.Point(927, 3);
            this.button_windowclose.Name = "button_windowclose";
            this.button_windowclose.Size = new System.Drawing.Size(94, 68);
            this.button_windowclose.TabIndex = 1;
            this.button_windowclose.Text = "창 닫기";
            this.button_windowclose.UseVisualStyleBackColor = false;
            this.button_windowclose.Click += new System.EventHandler(this.button_windowclose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label_title);
            this.panel2.Controls.Add(this.label_title_step);
            this.panel2.Controls.Add(this.label_subtitle);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 74);
            this.panel2.TabIndex = 3;
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(2, 6);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(234, 61);
            this.label_title.TabIndex = 2;
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_title_step
            // 
            this.label_title_step.BackColor = System.Drawing.Color.Transparent;
            this.label_title_step.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title_step.ForeColor = System.Drawing.Color.White;
            this.label_title_step.Location = new System.Drawing.Point(242, 6);
            this.label_title_step.Name = "label_title_step";
            this.label_title_step.Size = new System.Drawing.Size(67, 61);
            this.label_title_step.TabIndex = 2;
            this.label_title_step.Text = ">>";
            this.label_title_step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_subtitle
            // 
            this.label_subtitle.BackColor = System.Drawing.Color.Transparent;
            this.label_subtitle.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_subtitle.ForeColor = System.Drawing.Color.White;
            this.label_subtitle.Location = new System.Drawing.Point(315, 6);
            this.label_subtitle.Name = "label_subtitle";
            this.label_subtitle.Size = new System.Drawing.Size(350, 61);
            this.label_subtitle.TabIndex = 2;
            this.label_subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form_Setting_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Setting_Main";
            this.Text = "Form_Setting_Main";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_windowclose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_title_step;
        private System.Windows.Forms.Label label_subtitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Timer timer1;
    }
}