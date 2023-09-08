
namespace EL_DC_Charger.BatteryChange_Charger.Settings
{
    partial class Form_Confirm_Program_Reset
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_program_finish = new System.Windows.Forms.Button();
            this.button_restart_program = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_restart_system = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.Controls.Add(this.button_program_finish, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_restart_program, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_cancel, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_restart_system, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.71428F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.71428F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(752, 561);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // button_program_finish
            // 
            this.button_program_finish.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_program_finish.Location = new System.Drawing.Point(76, 79);
            this.button_program_finish.Name = "button_program_finish";
            this.button_program_finish.Size = new System.Drawing.Size(178, 185);
            this.button_program_finish.TabIndex = 10;
            this.button_program_finish.Text = "프로그램 종료";
            this.button_program_finish.UseVisualStyleBackColor = true;
            this.button_program_finish.Click += new System.EventHandler(this.button_program_finish_Click);
            // 
            // button_restart_program
            // 
            this.button_restart_program.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_restart_program.Location = new System.Drawing.Point(285, 79);
            this.button_restart_program.Name = "button_restart_program";
            this.button_restart_program.Size = new System.Drawing.Size(178, 185);
            this.button_restart_program.TabIndex = 11;
            this.button_restart_program.Text = "프로그램 재시작";
            this.button_restart_program.UseVisualStyleBackColor = true;
            this.button_restart_program.Click += new System.EventHandler(this.button_restart_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_cancel.Location = new System.Drawing.Point(494, 295);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(178, 185);
            this.button_cancel.TabIndex = 9;
            this.button_cancel.Text = "취소";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_restart_system
            // 
            this.button_restart_system.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_restart_system.Location = new System.Drawing.Point(285, 295);
            this.button_restart_system.Name = "button_restart_system";
            this.button_restart_system.Size = new System.Drawing.Size(178, 185);
            this.button_restart_system.TabIndex = 11;
            this.button_restart_system.Text = "시스템 재시작";
            this.button_restart_system.UseVisualStyleBackColor = true;
            this.button_restart_system.Click += new System.EventHandler(this.button_restart_system_Click);
            // 
            // Form_Confirm_Program_Reset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 561);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_Confirm_Program_Reset";
            this.Text = "프로그램 리셋 확인";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_program_finish;
        private System.Windows.Forms.Button button_restart_program;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_restart_system;
    }
}