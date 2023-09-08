
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_cancel = new System.Windows.Forms.TextBox();
            this.textBox_approval = new System.Windows.Forms.TextBox();
            this.button_dealcancel_before = new System.Windows.Forms.Button();
            this.button_deal_cancel_ = new System.Windows.Forms.Button();
            this.button_approval = new System.Windows.Forms.Button();
            this.button_chargingunit = new System.Windows.Forms.Button();
            this.label_chargingunit_nonmember = new System.Windows.Forms.Label();
            this.label_chargingunit_member = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.pb_nonmember = new System.Windows.Forms.PictureBox();
            this.pb_member = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_nonmember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_member)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_cancel
            // 
            this.textBox_cancel.Location = new System.Drawing.Point(629, 47);
            this.textBox_cancel.Multiline = true;
            this.textBox_cancel.Name = "textBox_cancel";
            this.textBox_cancel.Size = new System.Drawing.Size(111, 34);
            this.textBox_cancel.TabIndex = 25;
            this.textBox_cancel.Visible = false;
            // 
            // textBox_approval
            // 
            this.textBox_approval.Location = new System.Drawing.Point(260, 47);
            this.textBox_approval.Multiline = true;
            this.textBox_approval.Name = "textBox_approval";
            this.textBox_approval.Size = new System.Drawing.Size(111, 34);
            this.textBox_approval.TabIndex = 26;
            this.textBox_approval.Visible = false;
            // 
            // button_dealcancel_before
            // 
            this.button_dealcancel_before.Location = new System.Drawing.Point(490, 47);
            this.button_dealcancel_before.Name = "button_dealcancel_before";
            this.button_dealcancel_before.Size = new System.Drawing.Size(104, 34);
            this.button_dealcancel_before.TabIndex = 22;
            this.button_dealcancel_before.Text = "직전결제취소";
            this.button_dealcancel_before.UseVisualStyleBackColor = true;
            this.button_dealcancel_before.Visible = false;
            // 
            // button_deal_cancel_
            // 
            this.button_deal_cancel_.Location = new System.Drawing.Point(746, 47);
            this.button_deal_cancel_.Name = "button_deal_cancel_";
            this.button_deal_cancel_.Size = new System.Drawing.Size(75, 34);
            this.button_deal_cancel_.TabIndex = 23;
            this.button_deal_cancel_.Text = "부분취소";
            this.button_deal_cancel_.UseVisualStyleBackColor = true;
            this.button_deal_cancel_.Visible = false;
            // 
            // button_approval
            // 
            this.button_approval.Location = new System.Drawing.Point(376, 47);
            this.button_approval.Name = "button_approval";
            this.button_approval.Size = new System.Drawing.Size(75, 34);
            this.button_approval.TabIndex = 24;
            this.button_approval.Text = "결제시작";
            this.button_approval.UseVisualStyleBackColor = true;
            this.button_approval.Visible = false;
            this.button_approval.Click += new System.EventHandler(this.button_approval_Click_1);
            // 
            // button_chargingunit
            // 
            this.button_chargingunit.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_chargingunit.ForeColor = System.Drawing.Color.Black;
            this.button_chargingunit.Location = new System.Drawing.Point(135, 135);
            this.button_chargingunit.Name = "button_chargingunit";
            this.button_chargingunit.Size = new System.Drawing.Size(144, 66);
            this.button_chargingunit.TabIndex = 21;
            this.button_chargingunit.Text = "단가변경";
            this.button_chargingunit.UseVisualStyleBackColor = true;
            this.button_chargingunit.Visible = false;
            // 
            // label_chargingunit_nonmember
            // 
            this.label_chargingunit_nonmember.BackColor = System.Drawing.Color.Transparent;
            this.label_chargingunit_nonmember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_chargingunit_nonmember.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingunit_nonmember.ForeColor = System.Drawing.Color.White;
            this.label_chargingunit_nonmember.Location = new System.Drawing.Point(654, 403);
            this.label_chargingunit_nonmember.Margin = new System.Windows.Forms.Padding(0);
            this.label_chargingunit_nonmember.Name = "label_chargingunit_nonmember";
            this.label_chargingunit_nonmember.Size = new System.Drawing.Size(151, 70);
            this.label_chargingunit_nonmember.TabIndex = 19;
            this.label_chargingunit_nonmember.Text = "100원";
            this.label_chargingunit_nonmember.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_chargingunit_member
            // 
            this.label_chargingunit_member.BackColor = System.Drawing.Color.Transparent;
            this.label_chargingunit_member.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingunit_member.ForeColor = System.Drawing.Color.White;
            this.label_chargingunit_member.Location = new System.Drawing.Point(232, 403);
            this.label_chargingunit_member.Margin = new System.Windows.Forms.Padding(0);
            this.label_chargingunit_member.Name = "label_chargingunit_member";
            this.label_chargingunit_member.Size = new System.Drawing.Size(151, 70);
            this.label_chargingunit_member.TabIndex = 20;
            this.label_chargingunit_member.Text = "100원";
            this.label_chargingunit_member.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_version
            // 
            this.label_version.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.label_version.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_version.ForeColor = System.Drawing.Color.White;
            this.label_version.Location = new System.Drawing.Point(8, 544);
            this.label_version.Margin = new System.Windows.Forms.Padding(3);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(94, 50);
            this.label_version.TabIndex = 18;
            this.label_version.Text = "V 1.1.1";
            this.label_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_nonmember
            // 
            this.pb_nonmember.Image = global::EL_DC_Charger.Properties.Resources.img_btn_nonmember_normal;
            this.pb_nonmember.Location = new System.Drawing.Point(555, 220);
            this.pb_nonmember.Name = "pb_nonmember";
            this.pb_nonmember.Size = new System.Drawing.Size(347, 323);
            this.pb_nonmember.TabIndex = 17;
            this.pb_nonmember.TabStop = false;
            // 
            // pb_member
            // 
            this.pb_member.Image = global::EL_DC_Charger.Properties.Resources.img_btn_member_normal;
            this.pb_member.Location = new System.Drawing.Point(135, 220);
            this.pb_member.Name = "pb_member";
            this.pb_member.Size = new System.Drawing.Size(342, 323);
            this.pb_member.TabIndex = 16;
            this.pb_member.TabStop = false;
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox_cancel);
            this.Controls.Add(this.textBox_approval);
            this.Controls.Add(this.button_dealcancel_before);
            this.Controls.Add(this.button_deal_cancel_);
            this.Controls.Add(this.button_approval);
            this.Controls.Add(this.button_chargingunit);
            this.Controls.Add(this.label_chargingunit_nonmember);
            this.Controls.Add(this.label_chargingunit_member);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.pb_nonmember);
            this.Controls.Add(this.pb_member);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_nonmember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_member)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_cancel;
        private System.Windows.Forms.TextBox textBox_approval;
        private System.Windows.Forms.Button button_dealcancel_before;
        private System.Windows.Forms.Button button_deal_cancel_;
        private System.Windows.Forms.Button button_approval;
        private System.Windows.Forms.Button button_chargingunit;
        private System.Windows.Forms.Label label_chargingunit_nonmember;
        private System.Windows.Forms.Label label_chargingunit_member;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.PictureBox pb_nonmember;
        private System.Windows.Forms.PictureBox pb_member;
    }
}
