
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType
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
            this.button_qrcode = new System.Windows.Forms.Button();
            this.button_carddevice = new System.Windows.Forms.Button();
            this.pb_home = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_home)).BeginInit();
            this.SuspendLayout();
            // 
            // button_qrcode
            // 
            this.button_qrcode.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_qrcode.ForeColor = System.Drawing.Color.Black;
            this.button_qrcode.Location = new System.Drawing.Point(246, 241);
            this.button_qrcode.Name = "button_qrcode";
            this.button_qrcode.Size = new System.Drawing.Size(224, 96);
            this.button_qrcode.TabIndex = 2;
            this.button_qrcode.Text = "QR 결제";
            this.button_qrcode.UseVisualStyleBackColor = true;
            this.button_qrcode.Click += new System.EventHandler(this.button_qrcode_Click);
            // 
            // button_carddevice
            // 
            this.button_carddevice.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_carddevice.ForeColor = System.Drawing.Color.Black;
            this.button_carddevice.Location = new System.Drawing.Point(641, 241);
            this.button_carddevice.Name = "button_carddevice";
            this.button_carddevice.Size = new System.Drawing.Size(224, 96);
            this.button_carddevice.TabIndex = 2;
            this.button_carddevice.Text = "현장결제\r\n(신용카드, 삼성페이)";
            this.button_carddevice.UseVisualStyleBackColor = true;
            this.button_carddevice.Click += new System.EventHandler(this.button_carddevice_Click);
            // 
            // pb_home
            // 
            this.pb_home.Image = global::EL_DC_Charger.Properties.Resources.wev_btn_home_normal;
            this.pb_home.Location = new System.Drawing.Point(927, 26);
            this.pb_home.Name = "pb_home";
            this.pb_home.Size = new System.Drawing.Size(70, 70);
            this.pb_home.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_home.TabIndex = 10;
            this.pb_home.TabStop = false;
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_home);
            this.Controls.Add(this.button_carddevice);
            this.Controls.Add(this.button_qrcode);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_home)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_qrcode;
        private System.Windows.Forms.Button button_carddevice;
        private System.Windows.Forms.PictureBox pb_home;
    }
}
