
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.test
{
    partial class UC_Test_ControlBd
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
            this.cb_emg = new System.Windows.Forms.CheckBox();
            this.cb_gun_connect = new System.Windows.Forms.CheckBox();
            this.cb_charging = new System.Windows.Forms.CheckBox();
            this.cb_card_reading_complete = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cb_emg
            // 
            this.cb_emg.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_emg.Location = new System.Drawing.Point(32, 33);
            this.cb_emg.Name = "cb_emg";
            this.cb_emg.Size = new System.Drawing.Size(155, 37);
            this.cb_emg.TabIndex = 0;
            this.cb_emg.Text = "Emergency";
            this.cb_emg.UseVisualStyleBackColor = true;
            this.cb_emg.CheckedChanged += new System.EventHandler(this.cb_emg_CheckedChanged);
            // 
            // cb_gun_connect
            // 
            this.cb_gun_connect.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_gun_connect.Location = new System.Drawing.Point(32, 98);
            this.cb_gun_connect.Name = "cb_gun_connect";
            this.cb_gun_connect.Size = new System.Drawing.Size(155, 37);
            this.cb_gun_connect.TabIndex = 0;
            this.cb_gun_connect.Text = "충전건 연결";
            this.cb_gun_connect.UseVisualStyleBackColor = true;
            this.cb_gun_connect.CheckedChanged += new System.EventHandler(this.cb_gun_connect_CheckedChanged);
            // 
            // cb_charging
            // 
            this.cb_charging.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_charging.Location = new System.Drawing.Point(32, 232);
            this.cb_charging.Name = "cb_charging";
            this.cb_charging.Size = new System.Drawing.Size(155, 37);
            this.cb_charging.TabIndex = 0;
            this.cb_charging.Text = "충전중";
            this.cb_charging.UseVisualStyleBackColor = true;
            this.cb_charging.CheckedChanged += new System.EventHandler(this.cb_charging_CheckedChanged);
            // 
            // cb_card_reading_complete
            // 
            this.cb_card_reading_complete.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_card_reading_complete.Location = new System.Drawing.Point(32, 164);
            this.cb_card_reading_complete.Name = "cb_card_reading_complete";
            this.cb_card_reading_complete.Size = new System.Drawing.Size(178, 37);
            this.cb_card_reading_complete.TabIndex = 0;
            this.cb_card_reading_complete.Text = "카드 리딩 완료";
            this.cb_card_reading_complete.UseVisualStyleBackColor = true;
            this.cb_card_reading_complete.CheckedChanged += new System.EventHandler(this.cb_card_reading_complete_CheckedChanged);
            // 
            // UC_Test_ControlBd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_charging);
            this.Controls.Add(this.cb_card_reading_complete);
            this.Controls.Add(this.cb_gun_connect);
            this.Controls.Add(this.cb_emg);
            this.Name = "UC_Test_ControlBd";
            this.Size = new System.Drawing.Size(504, 659);
            this.Load += new System.EventHandler(this.UC_Test_ControlBd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_emg;
        private System.Windows.Forms.CheckBox cb_gun_connect;
        private System.Windows.Forms.CheckBox cb_charging;
        private System.Windows.Forms.CheckBox cb_card_reading_complete;
    }
}
