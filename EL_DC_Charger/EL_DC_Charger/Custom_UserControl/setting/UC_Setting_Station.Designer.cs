
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    partial class UC_Setting_Station
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
            this.components = new System.ComponentModel.Container();
            this.timer_init = new System.Windows.Forms.Timer(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.cb_calibrationMode = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_member_amount = new System.Windows.Forms.Button();
            this.btn_nonmember_amount = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.cb_PlatformOper = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_Platform = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_ChargerMaker = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_chargerType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_auto_start_add = new System.Windows.Forms.Button();
            this.cb_testform = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_firstsetting_complete = new System.Windows.Forms.CheckBox();
            this.button_init = new System.Windows.Forms.Button();
            this.label_auto_start_add = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tb_fromMonth = new System.Windows.Forms.TextBox();
            this.tb_fromDay = new System.Windows.Forms.TextBox();
            this.tb_toMonth = new System.Windows.Forms.TextBox();
            this.tb_toDay = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rb_pilot = new System.Windows.Forms.RadioButton();
            this.rb_odhitec = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.cb_trd = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rb_ch2 = new System.Windows.Forms.RadioButton();
            this.rb_ch1 = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.cb_isDebug = new System.Windows.Forms.CheckBox();
            this.cb_sizeMode = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_init
            // 
            this.timer_init.Enabled = true;
            this.timer_init.Interval = 2000;
            this.timer_init.Tick += new System.EventHandler(this.timer_init_Tick);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(3, 422);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(164, 34);
            this.label15.TabIndex = 29;
            this.label15.Text = "AMI 모델";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_calibrationMode
            // 
            this.cb_calibrationMode.AutoSize = true;
            this.cb_calibrationMode.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_calibrationMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_calibrationMode.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_calibrationMode.ForeColor = System.Drawing.Color.White;
            this.cb_calibrationMode.Location = new System.Drawing.Point(173, 392);
            this.cb_calibrationMode.Name = "cb_calibrationMode";
            this.cb_calibrationMode.Size = new System.Drawing.Size(164, 23);
            this.cb_calibrationMode.TabIndex = 28;
            this.cb_calibrationMode.Text = "활성화";
            this.cb_calibrationMode.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(3, 384);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(164, 34);
            this.label14.TabIndex = 26;
            this.label14.Text = "검/교정 모드";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.33533F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.66467F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_member_amount, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_nonmember_amount, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(513, 307);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(334, 108);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.label9, 2);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(328, 36);
            this.label9.TabIndex = 17;
            this.label9.Text = "충전 금액 설정";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 36);
            this.label12.TabIndex = 18;
            this.label12.Text = "회원";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 36);
            this.label13.TabIndex = 18;
            this.label13.Text = "비회원";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_member_amount
            // 
            this.btn_member_amount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_member_amount.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_member_amount.Location = new System.Drawing.Point(111, 39);
            this.btn_member_amount.Name = "btn_member_amount";
            this.btn_member_amount.Size = new System.Drawing.Size(220, 30);
            this.btn_member_amount.TabIndex = 19;
            this.btn_member_amount.UseVisualStyleBackColor = true;
            this.btn_member_amount.Click += new System.EventHandler(this.btn_member_amount_Click);
            // 
            // btn_nonmember_amount
            // 
            this.btn_nonmember_amount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_nonmember_amount.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_nonmember_amount.Location = new System.Drawing.Point(111, 75);
            this.btn_nonmember_amount.Name = "btn_nonmember_amount";
            this.btn_nonmember_amount.Size = new System.Drawing.Size(220, 30);
            this.btn_nonmember_amount.TabIndex = 20;
            this.btn_nonmember_amount.UseVisualStyleBackColor = true;
            this.btn_nonmember_amount.Click += new System.EventHandler(this.btn_nonmember_amount_Click);
            // 
            // button_save
            // 
            this.button_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_save.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_save.Location = new System.Drawing.Point(853, 459);
            this.button_save.Name = "button_save";
            this.tableLayoutPanel1.SetRowSpan(this.button_save, 2);
            this.button_save.Size = new System.Drawing.Size(168, 62);
            this.button_save.TabIndex = 24;
            this.button_save.Text = "저    장";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // cb_PlatformOper
            // 
            this.cb_PlatformOper.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_PlatformOper.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_PlatformOper.FormattingEnabled = true;
            this.cb_PlatformOper.Location = new System.Drawing.Point(173, 350);
            this.cb_PlatformOper.Name = "cb_PlatformOper";
            this.cb_PlatformOper.Size = new System.Drawing.Size(164, 27);
            this.cb_PlatformOper.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 34);
            this.label8.TabIndex = 23;
            this.label8.Text = "플랫폼 운영사";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_Platform
            // 
            this.cb_Platform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_Platform.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Platform.FormattingEnabled = true;
            this.cb_Platform.Location = new System.Drawing.Point(173, 312);
            this.cb_Platform.Name = "cb_Platform";
            this.cb_Platform.Size = new System.Drawing.Size(164, 27);
            this.cb_Platform.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 34);
            this.label7.TabIndex = 20;
            this.label7.Text = "플랫폼";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_ChargerMaker
            // 
            this.cb_ChargerMaker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_ChargerMaker.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_ChargerMaker.FormattingEnabled = true;
            this.cb_ChargerMaker.Location = new System.Drawing.Point(173, 274);
            this.cb_ChargerMaker.Name = "cb_ChargerMaker";
            this.cb_ChargerMaker.Size = new System.Drawing.Size(164, 27);
            this.cb_ChargerMaker.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 34);
            this.label6.TabIndex = 18;
            this.label6.Text = "충전기 제조사";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_chargerType
            // 
            this.cb_chargerType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_chargerType.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_chargerType.FormattingEnabled = true;
            this.cb_chargerType.Location = new System.Drawing.Point(173, 236);
            this.cb_chargerType.Name = "cb_chargerType";
            this.cb_chargerType.Size = new System.Drawing.Size(164, 27);
            this.cb_chargerType.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 34);
            this.label5.TabIndex = 16;
            this.label5.Text = "충전기 타입";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label4, 3);
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(504, 34);
            this.label4.TabIndex = 0;
            this.label4.Text = "자동실행 활성화";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(340, 152);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 38);
            this.button2.TabIndex = 5;
            this.button2.Text = "확   인";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_auto_start_confirm_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(170, 152);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "제   거";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_auto_start_remove_Click);
            // 
            // button_auto_start_add
            // 
            this.button_auto_start_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_auto_start_add.Location = new System.Drawing.Point(0, 152);
            this.button_auto_start_add.Margin = new System.Windows.Forms.Padding(0);
            this.button_auto_start_add.Name = "button_auto_start_add";
            this.button_auto_start_add.Size = new System.Drawing.Size(170, 38);
            this.button_auto_start_add.TabIndex = 10;
            this.button_auto_start_add.Text = "등   록";
            this.button_auto_start_add.UseVisualStyleBackColor = true;
            this.button_auto_start_add.Click += new System.EventHandler(this.button_auto_start_add_Click);
            // 
            // cb_testform
            // 
            this.cb_testform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_testform.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_testform.ForeColor = System.Drawing.Color.White;
            this.cb_testform.Location = new System.Drawing.Point(173, 79);
            this.cb_testform.Name = "cb_testform";
            this.cb_testform.Size = new System.Drawing.Size(164, 32);
            this.cb_testform.TabIndex = 8;
            this.cb_testform.Text = "표     시";
            this.cb_testform.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "테스트폼 표시";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "최초 설정 확인";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 6);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1018, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "환경설정";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_firstsetting_complete
            // 
            this.cb_firstsetting_complete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_firstsetting_complete.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_firstsetting_complete.ForeColor = System.Drawing.Color.White;
            this.cb_firstsetting_complete.Location = new System.Drawing.Point(173, 41);
            this.cb_firstsetting_complete.Name = "cb_firstsetting_complete";
            this.cb_firstsetting_complete.Size = new System.Drawing.Size(164, 32);
            this.cb_firstsetting_complete.TabIndex = 5;
            this.cb_firstsetting_complete.Text = "최초 설정 완료";
            this.cb_firstsetting_complete.UseVisualStyleBackColor = true;
            // 
            // button_init
            // 
            this.button_init.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_init.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_init.Location = new System.Drawing.Point(343, 41);
            this.button_init.Name = "button_init";
            this.button_init.Size = new System.Drawing.Size(164, 32);
            this.button_init.TabIndex = 9;
            this.button_init.Text = "초 기 화";
            this.button_init.UseVisualStyleBackColor = true;
            this.button_init.Visible = false;
            // 
            // label_auto_start_add
            // 
            this.label_auto_start_add.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.label_auto_start_add, 3);
            this.label_auto_start_add.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_auto_start_add.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_auto_start_add.Location = new System.Drawing.Point(3, 194);
            this.label_auto_start_add.Name = "label_auto_start_add";
            this.label_auto_start_add.Size = new System.Drawing.Size(504, 34);
            this.label_auto_start_add.TabIndex = 12;
            this.label_auto_start_add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_auto_start_add, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_init, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_firstsetting_complete, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_testform, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_auto_start_add, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cb_chargerType, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.cb_ChargerMaker, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cb_Platform, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.cb_PlatformOper, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.button_save, 5, 12);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.cb_calibrationMode, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.cb_trd, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label17, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.label18, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.cb_isDebug, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.cb_sizeMode, 4, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 524);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(513, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 38);
            this.label10.TabIndex = 19;
            this.label10.Text = "사이즈모드";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label20, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label21, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label22, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label23, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.tb_fromMonth, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.tb_fromDay, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.tb_toMonth, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.tb_toDay, 2, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(513, 41);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel5, 3);
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(334, 108);
            this.tableLayoutPanel5.TabIndex = 37;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel5.SetColumnSpan(this.label19, 3);
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(328, 22);
            this.label19.TabIndex = 17;
            this.label19.Text = "시운전 기간 시작 / 종료 기간 설정";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(225, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(106, 22);
            this.label20.TabIndex = 18;
            this.label20.Text = "일";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(114, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(105, 22);
            this.label21.TabIndex = 18;
            this.label21.Text = "월";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(3, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 22);
            this.label22.TabIndex = 18;
            this.label22.Text = "시작";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(3, 66);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 22);
            this.label23.TabIndex = 18;
            this.label23.Text = "종료";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_fromMonth
            // 
            this.tb_fromMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_fromMonth.Location = new System.Drawing.Point(114, 47);
            this.tb_fromMonth.Name = "tb_fromMonth";
            this.tb_fromMonth.Size = new System.Drawing.Size(105, 21);
            this.tb_fromMonth.TabIndex = 17;
            // 
            // tb_fromDay
            // 
            this.tb_fromDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_fromDay.Location = new System.Drawing.Point(225, 47);
            this.tb_fromDay.Name = "tb_fromDay";
            this.tb_fromDay.Size = new System.Drawing.Size(106, 21);
            this.tb_fromDay.TabIndex = 17;
            // 
            // tb_toMonth
            // 
            this.tb_toMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_toMonth.Location = new System.Drawing.Point(114, 69);
            this.tb_toMonth.Name = "tb_toMonth";
            this.tb_toMonth.Size = new System.Drawing.Size(105, 21);
            this.tb_toMonth.TabIndex = 17;
            // 
            // tb_toDay
            // 
            this.tb_toDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_toDay.Location = new System.Drawing.Point(225, 69);
            this.tb_toDay.Name = "tb_toDay";
            this.tb_toDay.Size = new System.Drawing.Size(106, 21);
            this.tb_toDay.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(170, 418);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(340, 38);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.rb_pilot, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.rb_odhitec, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 14);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(340, 24);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // rb_pilot
            // 
            this.rb_pilot.AutoSize = true;
            this.rb_pilot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_pilot.ForeColor = System.Drawing.Color.White;
            this.rb_pilot.Location = new System.Drawing.Point(173, 3);
            this.rb_pilot.Name = "rb_pilot";
            this.rb_pilot.Size = new System.Drawing.Size(164, 18);
            this.rb_pilot.TabIndex = 1;
            this.rb_pilot.TabStop = true;
            this.rb_pilot.Text = "PILOT(SPM90)";
            this.rb_pilot.UseVisualStyleBackColor = true;
            // 
            // rb_odhitec
            // 
            this.rb_odhitec.AutoSize = true;
            this.rb_odhitec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_odhitec.ForeColor = System.Drawing.Color.White;
            this.rb_odhitec.Location = new System.Drawing.Point(3, 3);
            this.rb_odhitec.Name = "rb_odhitec";
            this.rb_odhitec.Size = new System.Drawing.Size(164, 18);
            this.rb_odhitec.TabIndex = 0;
            this.rb_odhitec.TabStop = true;
            this.rb_odhitec.Text = "오디하이텍(DJSF1)";
            this.rb_odhitec.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(3, 460);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(164, 34);
            this.label16.TabIndex = 31;
            this.label16.Text = "TRD 버전";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_trd
            // 
            this.cb_trd.AutoSize = true;
            this.cb_trd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cb_trd.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_trd.ForeColor = System.Drawing.Color.White;
            this.cb_trd.Location = new System.Drawing.Point(173, 468);
            this.cb_trd.Name = "cb_trd";
            this.cb_trd.Size = new System.Drawing.Size(164, 23);
            this.cb_trd.TabIndex = 32;
            this.cb_trd.Text = "TRD";
            this.cb_trd.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(513, 232);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(164, 34);
            this.label17.TabIndex = 33;
            this.label17.Text = "충전건 (CH)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Location = new System.Drawing.Point(680, 228);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(340, 38);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.rb_ch2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rb_ch1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 14);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(340, 24);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // rb_ch2
            // 
            this.rb_ch2.AutoSize = true;
            this.rb_ch2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_ch2.ForeColor = System.Drawing.Color.White;
            this.rb_ch2.Location = new System.Drawing.Point(173, 3);
            this.rb_ch2.Name = "rb_ch2";
            this.rb_ch2.Size = new System.Drawing.Size(164, 18);
            this.rb_ch2.TabIndex = 1;
            this.rb_ch2.TabStop = true;
            this.rb_ch2.Text = "2채널";
            this.rb_ch2.UseVisualStyleBackColor = true;
            // 
            // rb_ch1
            // 
            this.rb_ch1.AutoSize = true;
            this.rb_ch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rb_ch1.ForeColor = System.Drawing.Color.White;
            this.rb_ch1.Location = new System.Drawing.Point(3, 3);
            this.rb_ch1.Name = "rb_ch1";
            this.rb_ch1.Size = new System.Drawing.Size(164, 18);
            this.rb_ch1.TabIndex = 0;
            this.rb_ch1.TabStop = true;
            this.rb_ch1.Text = "1채널";
            this.rb_ch1.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(513, 266);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(164, 34);
            this.label18.TabIndex = 35;
            this.label18.Text = "디버그모드";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_isDebug
            // 
            this.cb_isDebug.AutoSize = true;
            this.cb_isDebug.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_isDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_isDebug.Font = new System.Drawing.Font("굴림", 14F);
            this.cb_isDebug.ForeColor = System.Drawing.Color.White;
            this.cb_isDebug.Location = new System.Drawing.Point(683, 269);
            this.cb_isDebug.Name = "cb_isDebug";
            this.cb_isDebug.Size = new System.Drawing.Size(164, 32);
            this.cb_isDebug.TabIndex = 36;
            this.cb_isDebug.Text = "활성화";
            this.cb_isDebug.UseVisualStyleBackColor = true;
            this.cb_isDebug.CheckedChanged += new System.EventHandler(this.cb_isDebug_CheckedChanged);
            // 
            // cb_sizeMode
            // 
            this.cb_sizeMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_sizeMode.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_sizeMode.FormattingEnabled = true;
            this.cb_sizeMode.Location = new System.Drawing.Point(683, 155);
            this.cb_sizeMode.Name = "cb_sizeMode";
            this.cb_sizeMode.Size = new System.Drawing.Size(164, 27);
            this.cb_sizeMode.TabIndex = 38;
            // 
            // UC_Setting_Station
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_Setting_Station";
            this.Size = new System.Drawing.Size(1024, 524);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer_init;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cb_calibrationMode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_auto_start_add;
        private System.Windows.Forms.Button button_init;
        private System.Windows.Forms.CheckBox cb_firstsetting_complete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cb_testform;
        private System.Windows.Forms.Button button_auto_start_add;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_chargerType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_ChargerMaker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_Platform;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_PlatformOper;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton rb_pilot;
        private System.Windows.Forms.RadioButton rb_odhitec;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox cb_trd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RadioButton rb_ch2;
        private System.Windows.Forms.RadioButton rb_ch1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox cb_isDebug;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tb_fromMonth;
        private System.Windows.Forms.TextBox tb_fromDay;
        private System.Windows.Forms.TextBox tb_toMonth;
        private System.Windows.Forms.TextBox tb_toDay;
        private System.Windows.Forms.Button btn_member_amount;
        private System.Windows.Forms.Button btn_nonmember_amount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cb_sizeMode;
    }
}
