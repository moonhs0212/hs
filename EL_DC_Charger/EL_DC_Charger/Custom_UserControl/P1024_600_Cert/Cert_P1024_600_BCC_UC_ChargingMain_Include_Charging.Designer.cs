
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging
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
            this.pb_chargingstop = new System.Windows.Forms.PictureBox();
            this.progressBar_soc = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_chrgingtime = new System.Windows.Forms.Label();
            this.label_remaintime = new System.Windows.Forms.Label();
            this.label_chargingwattage = new System.Windows.Forms.Label();
            this.label_amount = new System.Windows.Forms.Label();
            this.label_amount_unit = new System.Windows.Forms.Label();
            this.label_soc = new System.Windows.Forms.Label();
            this.label_chargingvoltagecurrent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingstop)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_chargingstop
            // 
            this.pb_chargingstop.Image = global::EL_DC_Charger.Properties.Resources.wev_img_btn_chargingstop_normal;
            this.pb_chargingstop.Location = new System.Drawing.Point(353, 458);
            this.pb_chargingstop.Name = "pb_chargingstop";
            this.pb_chargingstop.Size = new System.Drawing.Size(347, 105);
            this.pb_chargingstop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_chargingstop.TabIndex = 12;
            this.pb_chargingstop.TabStop = false;
            // 
            // progressBar_soc
            // 
            this.progressBar_soc.Location = new System.Drawing.Point(151, 78);
            this.progressBar_soc.Name = "progressBar_soc";
            this.progressBar_soc.Size = new System.Drawing.Size(718, 49);
            this.progressBar_soc.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 54);
            this.label1.TabIndex = 14;
            this.label1.Text = "충전시간";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 54);
            this.label2.TabIndex = 14;
            this.label2.Text = "남은시간";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(494, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 54);
            this.label3.TabIndex = 14;
            this.label3.Text = "충전량 (kWh)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(494, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 54);
            this.label4.TabIndex = 14;
            this.label4.Text = "충전금액 (원)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(494, 319);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(262, 54);
            this.label5.TabIndex = 14;
            this.label5.Text = " 충전단가 (원)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_chrgingtime
            // 
            this.label_chrgingtime.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chrgingtime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_chrgingtime.Location = new System.Drawing.Point(271, 196);
            this.label_chrgingtime.Name = "label_chrgingtime";
            this.label_chrgingtime.Size = new System.Drawing.Size(262, 54);
            this.label_chrgingtime.TabIndex = 14;
            this.label_chrgingtime.Text = "00:00:00";
            this.label_chrgingtime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_remaintime
            // 
            this.label_remaintime.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_remaintime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_remaintime.Location = new System.Drawing.Point(271, 259);
            this.label_remaintime.Name = "label_remaintime";
            this.label_remaintime.Size = new System.Drawing.Size(262, 54);
            this.label_remaintime.TabIndex = 14;
            this.label_remaintime.Text = "00:00:00";
            this.label_remaintime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chargingwattage
            // 
            this.label_chargingwattage.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingwattage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_chargingwattage.Location = new System.Drawing.Point(762, 196);
            this.label_chargingwattage.Name = "label_chargingwattage";
            this.label_chargingwattage.Size = new System.Drawing.Size(262, 54);
            this.label_chargingwattage.TabIndex = 14;
            this.label_chargingwattage.Text = "0.00";
            this.label_chargingwattage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_amount
            // 
            this.label_amount.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_amount.Location = new System.Drawing.Point(762, 257);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(262, 54);
            this.label_amount.TabIndex = 14;
            this.label_amount.Text = "0";
            this.label_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_amount_unit
            // 
            this.label_amount_unit.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_amount_unit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_amount_unit.Location = new System.Drawing.Point(762, 319);
            this.label_amount_unit.Name = "label_amount_unit";
            this.label_amount_unit.Size = new System.Drawing.Size(262, 54);
            this.label_amount_unit.TabIndex = 14;
            this.label_amount_unit.Text = "0";
            this.label_amount_unit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_soc
            // 
            this.label_soc.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold);
            this.label_soc.ForeColor = System.Drawing.Color.Black;
            this.label_soc.Location = new System.Drawing.Point(875, 78);
            this.label_soc.Name = "label_soc";
            this.label_soc.Size = new System.Drawing.Size(122, 49);
            this.label_soc.TabIndex = 14;
            this.label_soc.Text = "0%";
            this.label_soc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chargingvoltagecurrent
            // 
            this.label_chargingvoltagecurrent.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingvoltagecurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
            this.label_chargingvoltagecurrent.Location = new System.Drawing.Point(271, 319);
            this.label_chargingvoltagecurrent.Name = "label_chargingvoltagecurrent";
            this.label_chargingvoltagecurrent.Size = new System.Drawing.Size(262, 54);
            this.label_chargingvoltagecurrent.TabIndex = 14;
            this.label_chargingvoltagecurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(3, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 54);
            this.label7.TabIndex = 14;
            this.label7.Text = "충전 전압/전류";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EL_DC_Charger.Properties.Resources.wev_img_background_bright;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_amount_unit);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.label_chargingwattage);
            this.Controls.Add(this.label_chargingvoltagecurrent);
            this.Controls.Add(this.label_remaintime);
            this.Controls.Add(this.label_chrgingtime);
            this.Controls.Add(this.label_soc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar_soc);
            this.Controls.Add(this.pb_chargingstop);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingstop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pb_chargingstop;
        private System.Windows.Forms.ProgressBar progressBar_soc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_chrgingtime;
        private System.Windows.Forms.Label label_remaintime;
        private System.Windows.Forms.Label label_chargingwattage;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Label label_amount_unit;
        private System.Windows.Forms.Label label_soc;
        private System.Windows.Forms.Label label_chargingvoltagecurrent;
        private System.Windows.Forms.Label label7;
    }
}
