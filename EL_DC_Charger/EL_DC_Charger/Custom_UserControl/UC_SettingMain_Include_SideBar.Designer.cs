
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl
{
    partial class UC_SettingMain_Include_SideBar
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
            this.button_simulation = new System.Windows.Forms.Button();
            this.button_comm_path = new System.Windows.Forms.Button();
            this.button_finish_application = new System.Windows.Forms.Button();
            this.button_reboot_system = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_simulation
            // 
            this.button_simulation.BackColor = System.Drawing.Color.White;
            this.button_simulation.FlatAppearance.BorderSize = 2;
            this.button_simulation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_simulation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button_simulation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_simulation.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_simulation.Location = new System.Drawing.Point(3, 90);
            this.button_simulation.Name = "button_simulation";
            this.button_simulation.Size = new System.Drawing.Size(143, 78);
            this.button_simulation.TabIndex = 3;
            this.button_simulation.Text = "시뮬레이션\r\n여부 설정";
            this.button_simulation.UseVisualStyleBackColor = false;
            // 
            // button_comm_path
            // 
            this.button_comm_path.BackColor = System.Drawing.Color.White;
            this.button_comm_path.FlatAppearance.BorderSize = 2;
            this.button_comm_path.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_comm_path.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button_comm_path.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_comm_path.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_comm_path.Location = new System.Drawing.Point(3, 6);
            this.button_comm_path.Name = "button_comm_path";
            this.button_comm_path.Size = new System.Drawing.Size(143, 78);
            this.button_comm_path.TabIndex = 4;
            this.button_comm_path.Text = "통신경로\r\n설정\r\n(Serial)";
            this.button_comm_path.UseVisualStyleBackColor = false;
            // 
            // button_finish_application
            // 
            this.button_finish_application.BackColor = System.Drawing.Color.White;
            this.button_finish_application.FlatAppearance.BorderSize = 2;
            this.button_finish_application.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_finish_application.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button_finish_application.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_finish_application.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_finish_application.Location = new System.Drawing.Point(3, 479);
            this.button_finish_application.Name = "button_finish_application";
            this.button_finish_application.Size = new System.Drawing.Size(143, 78);
            this.button_finish_application.TabIndex = 3;
            this.button_finish_application.Text = "프로그램\r\n종료";
            this.button_finish_application.UseVisualStyleBackColor = false;
            this.button_finish_application.Click += new System.EventHandler(this.button_finish_application_Click);
            // 
            // button_reboot_system
            // 
            this.button_reboot_system.BackColor = System.Drawing.Color.White;
            this.button_reboot_system.FlatAppearance.BorderSize = 2;
            this.button_reboot_system.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_reboot_system.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button_reboot_system.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_reboot_system.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_reboot_system.Location = new System.Drawing.Point(3, 563);
            this.button_reboot_system.Name = "button_reboot_system";
            this.button_reboot_system.Size = new System.Drawing.Size(143, 78);
            this.button_reboot_system.TabIndex = 3;
            this.button_reboot_system.Text = "시스템\r\n재시작";
            this.button_reboot_system.UseVisualStyleBackColor = false;
            this.button_reboot_system.Click += new System.EventHandler(this.button_reboot_system_Click);
            // 
            // UC_SettingMain_Include_SideBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_reboot_system);
            this.Controls.Add(this.button_finish_application);
            this.Controls.Add(this.button_simulation);
            this.Controls.Add(this.button_comm_path);
            this.Name = "UC_SettingMain_Include_SideBar";
            this.Size = new System.Drawing.Size(168, 646);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_simulation;
        private System.Windows.Forms.Button button_comm_path;
        private System.Windows.Forms.Button button_finish_application;
        private System.Windows.Forms.Button button_reboot_system;
    }
}
