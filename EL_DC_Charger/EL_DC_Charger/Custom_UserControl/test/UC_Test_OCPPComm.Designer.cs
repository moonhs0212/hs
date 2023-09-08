
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.test
{
    partial class UC_Test_OCPPComm
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
            this.button_statusnotification = new System.Windows.Forms.Button();
            this.button_authorize = new System.Windows.Forms.Button();
            this.button_stoptransaction = new System.Windows.Forms.Button();
            this.button_starttransaction = new System.Windows.Forms.Button();
            this.button_bootnotification = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_statusnotification
            // 
            this.button_statusnotification.Location = new System.Drawing.Point(193, 149);
            this.button_statusnotification.Name = "button_statusnotification";
            this.button_statusnotification.Size = new System.Drawing.Size(126, 55);
            this.button_statusnotification.TabIndex = 1;
            this.button_statusnotification.Text = "conf statusnotification";
            this.button_statusnotification.UseVisualStyleBackColor = true;
            this.button_statusnotification.Click += new System.EventHandler(this.button_statusnotification_Click);
            // 
            // button_authorize
            // 
            this.button_authorize.Location = new System.Drawing.Point(43, 149);
            this.button_authorize.Name = "button_authorize";
            this.button_authorize.Size = new System.Drawing.Size(126, 55);
            this.button_authorize.TabIndex = 2;
            this.button_authorize.Text = "conf authorize";
            this.button_authorize.UseVisualStyleBackColor = true;
            this.button_authorize.Click += new System.EventHandler(this.button_authorize_Click_1);
            // 
            // button_stoptransaction
            // 
            this.button_stoptransaction.Location = new System.Drawing.Point(341, 54);
            this.button_stoptransaction.Name = "button_stoptransaction";
            this.button_stoptransaction.Size = new System.Drawing.Size(126, 55);
            this.button_stoptransaction.TabIndex = 3;
            this.button_stoptransaction.Text = "Conf StopTransaction";
            this.button_stoptransaction.UseVisualStyleBackColor = true;
            this.button_stoptransaction.Click += new System.EventHandler(this.button_stoptransaction_Click);
            // 
            // button_starttransaction
            // 
            this.button_starttransaction.Location = new System.Drawing.Point(193, 54);
            this.button_starttransaction.Name = "button_starttransaction";
            this.button_starttransaction.Size = new System.Drawing.Size(126, 55);
            this.button_starttransaction.TabIndex = 4;
            this.button_starttransaction.Text = "Conf starttransaction";
            this.button_starttransaction.UseVisualStyleBackColor = true;
            this.button_starttransaction.Click += new System.EventHandler(this.button_starttransaction_Click);
            // 
            // button_bootnotification
            // 
            this.button_bootnotification.Location = new System.Drawing.Point(43, 54);
            this.button_bootnotification.Name = "button_bootnotification";
            this.button_bootnotification.Size = new System.Drawing.Size(126, 55);
            this.button_bootnotification.TabIndex = 5;
            this.button_bootnotification.Text = "Conf BootNotification";
            this.button_bootnotification.UseVisualStyleBackColor = true;
            this.button_bootnotification.Click += new System.EventHandler(this.button_bootnotification_Click_1);
            // 
            // UC_Test_OCPPComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_statusnotification);
            this.Controls.Add(this.button_authorize);
            this.Controls.Add(this.button_stoptransaction);
            this.Controls.Add(this.button_starttransaction);
            this.Controls.Add(this.button_bootnotification);
            this.Name = "UC_Test_OCPPComm";
            this.Size = new System.Drawing.Size(504, 659);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_statusnotification;
        private System.Windows.Forms.Button button_authorize;
        private System.Windows.Forms.Button button_stoptransaction;
        private System.Windows.Forms.Button button_starttransaction;
        private System.Windows.Forms.Button button_bootnotification;
    }
}
