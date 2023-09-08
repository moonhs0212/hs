
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    partial class Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn
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
            this.tv_content_1 = new System.Windows.Forms.Label();
            this.pb_confirm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // tv_content_1
            // 
            this.tv_content_1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tv_content_1.ForeColor = System.Drawing.Color.Black;
            this.tv_content_1.Location = new System.Drawing.Point(109, 68);
            this.tv_content_1.Name = "tv_content_1";
            this.tv_content_1.Size = new System.Drawing.Size(806, 367);
            this.tv_content_1.TabIndex = 8;
            this.tv_content_1.Text = "충전 종료 사유";
            this.tv_content_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_confirm
            // 
            this.pb_confirm.Image = global::EL_DC_Charger.Properties.Resources.img_btn_confirm_normal;
            this.pb_confirm.Location = new System.Drawing.Point(417, 463);
            this.pb_confirm.Name = "pb_confirm";
            this.pb_confirm.Size = new System.Drawing.Size(181, 69);
            this.pb_confirm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_confirm.TabIndex = 7;
            this.pb_confirm.TabStop = false;
            this.pb_confirm.Click += new System.EventHandler(this.pb_confirm_Click);
            this.pb_confirm.MouseEnter += new System.EventHandler(this.pb_confirm_MouseEnter);
            this.pb_confirm.MouseLeave += new System.EventHandler(this.pb_confirm_MouseLeave);
            this.pb_confirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_confirm_MouseUp);
            // 
            // Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tv_content_1);
            this.Controls.Add(this.pb_confirm);
            this.Name = "Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn";
            this.Size = new System.Drawing.Size(1024, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pb_confirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tv_content_1;
        private System.Windows.Forms.PictureBox pb_confirm;
    }
}
