
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_chargingwattage = new System.Windows.Forms.Label();
            this.label_payment_first = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label_payment_partcancel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_payment_real = new System.Windows.Forms.Label();
            this.pb_chargingcomplete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingcomplete)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(153, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(712, 45);
            this.label1.TabIndex = 15;
            this.label1.Text = "충전 금액 정산 요청이 완료 되었습니다.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(37, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(945, 28);
            this.label2.TabIndex = 15;
            this.label2.Text = "정산 요청이 완료된 후, 최종 결산까지 2~3일이 소요될 수 있습니다.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(242, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 32);
            this.label3.TabIndex = 15;
            this.label3.Text = "충전량 (kWh)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(242, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 32);
            this.label4.TabIndex = 15;
            this.label4.Text = "선결제 금액 (원)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_chargingwattage
            // 
            this.label_chargingwattage.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label_chargingwattage.ForeColor = System.Drawing.Color.Black;
            this.label_chargingwattage.Location = new System.Drawing.Point(554, 224);
            this.label_chargingwattage.Name = "label_chargingwattage";
            this.label_chargingwattage.Size = new System.Drawing.Size(218, 32);
            this.label_chargingwattage.TabIndex = 15;
            this.label_chargingwattage.Text = "0";
            this.label_chargingwattage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_payment_first
            // 
            this.label_payment_first.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label_payment_first.ForeColor = System.Drawing.Color.Black;
            this.label_payment_first.Location = new System.Drawing.Point(554, 263);
            this.label_payment_first.Name = "label_payment_first";
            this.label_payment_first.Size = new System.Drawing.Size(218, 32);
            this.label_payment_first.TabIndex = 15;
            this.label_payment_first.Text = "0";
            this.label_payment_first.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(228, 335);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 2);
            this.panel1.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(242, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "부분취소 금액 (원)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_payment_partcancel
            // 
            this.label_payment_partcancel.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label_payment_partcancel.ForeColor = System.Drawing.Color.Red;
            this.label_payment_partcancel.Location = new System.Drawing.Point(554, 340);
            this.label_payment_partcancel.Name = "label_payment_partcancel";
            this.label_payment_partcancel.Size = new System.Drawing.Size(218, 32);
            this.label_payment_partcancel.TabIndex = 15;
            this.label_payment_partcancel.Text = "0";
            this.label_payment_partcancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(61)))), ((int)(((byte)(239)))));
            this.label9.Location = new System.Drawing.Point(242, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(218, 32);
            this.label9.TabIndex = 15;
            this.label9.Text = "실결제 금액 (원)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_payment_real
            // 
            this.label_payment_real.Font = new System.Drawing.Font("휴먼둥근헤드라인", 13F);
            this.label_payment_real.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(61)))), ((int)(((byte)(239)))));
            this.label_payment_real.Location = new System.Drawing.Point(554, 301);
            this.label_payment_real.Name = "label_payment_real";
            this.label_payment_real.Size = new System.Drawing.Size(218, 32);
            this.label_payment_real.TabIndex = 15;
            this.label_payment_real.Text = "0";
            this.label_payment_real.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pb_chargingcomplete
            // 
            this.pb_chargingcomplete.BackColor = System.Drawing.Color.Transparent;
            this.pb_chargingcomplete.Image = global::EL_DC_Charger.Properties.Resources.img_btn_confirm_normal;
            this.pb_chargingcomplete.Location = new System.Drawing.Point(365, 469);
            this.pb_chargingcomplete.Name = "pb_chargingcomplete";
            this.pb_chargingcomplete.Size = new System.Drawing.Size(283, 69);
            this.pb_chargingcomplete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_chargingcomplete.TabIndex = 17;
            this.pb_chargingcomplete.TabStop = false;
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_chargingcomplete);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_payment_partcancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_payment_real);
            this.Controls.Add(this.label_payment_first);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_chargingwattage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_chargingcomplete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_chargingwattage;
        private System.Windows.Forms.Label label_payment_first;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_payment_partcancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_payment_real;
        private System.Windows.Forms.PictureBox pb_chargingcomplete;
    }
}
