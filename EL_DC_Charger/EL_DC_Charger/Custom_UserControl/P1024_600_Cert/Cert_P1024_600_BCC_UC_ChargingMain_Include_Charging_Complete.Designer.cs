
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_amount = new System.Windows.Forms.Label();
            this.label_chargingwattage = new System.Windows.Forms.Label();
            this.label_chrgingtime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_chargingcomplete = new System.Windows.Forms.PictureBox();
            this.label_reason = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingcomplete)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(224, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 51);
            this.label4.TabIndex = 17;
            this.label4.Text = "충전금액 (원)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(224, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 51);
            this.label3.TabIndex = 18;
            this.label3.Text = "충전량 (kWh)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_amount
            // 
            this.label_amount.BackColor = System.Drawing.Color.Transparent;
            this.label_amount.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_amount.Location = new System.Drawing.Point(498, 219);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(330, 51);
            this.label_amount.TabIndex = 21;
            this.label_amount.Text = "0";
            this.label_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chargingwattage
            // 
            this.label_chargingwattage.BackColor = System.Drawing.Color.Transparent;
            this.label_chargingwattage.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label_chargingwattage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_chargingwattage.Location = new System.Drawing.Point(498, 158);
            this.label_chargingwattage.Name = "label_chargingwattage";
            this.label_chargingwattage.Size = new System.Drawing.Size(330, 51);
            this.label_chargingwattage.TabIndex = 22;
            this.label_chargingwattage.Text = "0.00";
            this.label_chargingwattage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chrgingtime
            // 
            this.label_chrgingtime.BackColor = System.Drawing.Color.Transparent;
            this.label_chrgingtime.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label_chrgingtime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_chrgingtime.Location = new System.Drawing.Point(498, 99);
            this.label_chrgingtime.Name = "label_chrgingtime";
            this.label_chrgingtime.Size = new System.Drawing.Size(330, 51);
            this.label_chrgingtime.TabIndex = 24;
            this.label_chrgingtime.Text = "00:00:00";
            this.label_chrgingtime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(224, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 51);
            this.label1.TabIndex = 25;
            this.label1.Text = "충전시간";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pb_chargingcomplete
            // 
            this.pb_chargingcomplete.BackColor = System.Drawing.Color.Transparent;
            this.pb_chargingcomplete.Image = global::EL_DC_Charger.Properties.Resources.img_btn_confirm_normal;
            this.pb_chargingcomplete.Location = new System.Drawing.Point(361, 366);
            this.pb_chargingcomplete.Name = "pb_chargingcomplete";
            this.pb_chargingcomplete.Size = new System.Drawing.Size(297, 87);
            this.pb_chargingcomplete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_chargingcomplete.TabIndex = 15;
            this.pb_chargingcomplete.TabStop = false;
            // 
            // label_reason
            // 
            this.label_reason.BackColor = System.Drawing.Color.Transparent;
            this.label_reason.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label_reason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_reason.Location = new System.Drawing.Point(498, 281);
            this.label_reason.Name = "label_reason";
            this.label_reason.Size = new System.Drawing.Size(330, 51);
            this.label_reason.TabIndex = 20;
            this.label_reason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(224, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 51);
            this.label5.TabIndex = 16;
            this.label5.Text = "종료사유";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1004, 114);
            this.label2.TabIndex = 26;
            this.label2.Text = "충전이 완료되었습니다.\r\n충전정보 확인 후, \'확인\' 버튼을 눌러 주세요.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EL_DC_Charger.Properties.Resources.wev_img_background_bright2;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_reason);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.label_chargingwattage);
            this.Controls.Add(this.label_chrgingtime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_chargingcomplete);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingcomplete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Label label_chargingwattage;
        private System.Windows.Forms.Label label_chrgingtime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pb_chargingcomplete;
        private System.Windows.Forms.Label label_reason;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}
