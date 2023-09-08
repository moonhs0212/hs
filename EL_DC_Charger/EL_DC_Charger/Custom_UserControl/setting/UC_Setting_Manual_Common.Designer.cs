namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    partial class UC_Setting_Manual_Common
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.button_rfid_stop = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_cardnumber_rfid = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_rfid_start = new System.Windows.Forms.Button();
            this.button_cardRead_start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_cardRead_stop = new System.Windows.Forms.Button();
            this.label_cardnumber_card = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_do_ControlBoadrd_Reset = new System.Windows.Forms.CheckBox();
            this.cb_do_led_blue = new System.Windows.Forms.CheckBox();
            this.cb_do_mc = new System.Windows.Forms.CheckBox();
            this.cb_do_led_green = new System.Windows.Forms.CheckBox();
            this.cb_do_fan = new System.Windows.Forms.CheckBox();
            this.cb_do_led_red = new System.Windows.Forms.CheckBox();
            this.cb_do_ir_Led = new System.Windows.Forms.CheckBox();
            this.cb_do_Rfid = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.cb_di_emg = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_di_sumRelay_plus = new System.Windows.Forms.CheckBox();
            this.cb_di_sumRelay_minus = new System.Windows.Forms.CheckBox();
            this.pb_camera = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_comstate_controlbd = new System.Windows.Forms.CheckBox();
            this.cb_comstate_powerpack = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.bck_camera = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_camera)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 524);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1018, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "공통";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 29);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.55466F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.44534F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1018, 492);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel7);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(342, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(333, 341);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Card Tag";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Controls.Add(this.button_rfid_stop, 2, 4);
            this.tableLayoutPanel7.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label_cardnumber_rfid, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.button_rfid_start, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.button_cardRead_start, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.button_cardRead_stop, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.label_cardnumber_card, 1, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00001F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(327, 315);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // button_rfid_stop
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.button_rfid_stop, 2);
            this.button_rfid_stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_rfid_stop.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_rfid_stop.ForeColor = System.Drawing.Color.Black;
            this.button_rfid_stop.Location = new System.Drawing.Point(167, 181);
            this.button_rfid_stop.Name = "button_rfid_stop";
            this.button_rfid_stop.Size = new System.Drawing.Size(151, 58);
            this.button_rfid_stop.TabIndex = 0;
            this.button_rfid_stop.Text = "읽기 종료";
            this.button_rfid_stop.UseVisualStyleBackColor = true;
            this.button_rfid_stop.Click += new System.EventHandler(this.button_rfid_stop_Click);
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 66);
            this.label7.TabIndex = 1;
            this.label7.Text = "카드\r\n번호";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel7.SetColumnSpan(this.label6, 4);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(9, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(309, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "RFID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_cardnumber_rfid
            // 
            this.label_cardnumber_rfid.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel7.SetColumnSpan(this.label_cardnumber_rfid, 3);
            this.label_cardnumber_rfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_cardnumber_rfid.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold);
            this.label_cardnumber_rfid.ForeColor = System.Drawing.Color.White;
            this.label_cardnumber_rfid.Location = new System.Drawing.Point(87, 245);
            this.label_cardnumber_rfid.Margin = new System.Windows.Forms.Padding(2);
            this.label_cardnumber_rfid.Name = "label_cardnumber_rfid";
            this.label_cardnumber_rfid.Size = new System.Drawing.Size(232, 62);
            this.label_cardnumber_rfid.TabIndex = 1;
            this.label_cardnumber_rfid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 64);
            this.label4.TabIndex = 5;
            this.label4.Text = "카드\r\n번호";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_rfid_start
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.button_rfid_start, 2);
            this.button_rfid_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_rfid_start.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_rfid_start.ForeColor = System.Drawing.Color.Black;
            this.button_rfid_start.Location = new System.Drawing.Point(9, 181);
            this.button_rfid_start.Name = "button_rfid_start";
            this.button_rfid_start.Size = new System.Drawing.Size(151, 58);
            this.button_rfid_start.TabIndex = 0;
            this.button_rfid_start.Text = "읽기 시작";
            this.button_rfid_start.UseVisualStyleBackColor = true;
            this.button_rfid_start.Click += new System.EventHandler(this.button_rfid_start_Click);
            // 
            // button_cardRead_start
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.button_cardRead_start, 2);
            this.button_cardRead_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_cardRead_start.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_cardRead_start.ForeColor = System.Drawing.Color.Black;
            this.button_cardRead_start.Location = new System.Drawing.Point(9, 30);
            this.button_cardRead_start.Name = "button_cardRead_start";
            this.button_cardRead_start.Size = new System.Drawing.Size(151, 58);
            this.button_cardRead_start.TabIndex = 3;
            this.button_cardRead_start.Text = "읽기 시작";
            this.button_cardRead_start.UseVisualStyleBackColor = true;
            this.button_cardRead_start.Click += new System.EventHandler(this.button_cardRead_start_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel7.SetColumnSpan(this.label2, 4);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "카드리더기";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_cardRead_stop
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.button_cardRead_stop, 2);
            this.button_cardRead_stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_cardRead_stop.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_cardRead_stop.ForeColor = System.Drawing.Color.Black;
            this.button_cardRead_stop.Location = new System.Drawing.Point(167, 30);
            this.button_cardRead_stop.Name = "button_cardRead_stop";
            this.button_cardRead_stop.Size = new System.Drawing.Size(151, 58);
            this.button_cardRead_stop.TabIndex = 4;
            this.button_cardRead_stop.Text = "읽기 종료";
            this.button_cardRead_stop.UseVisualStyleBackColor = true;
            this.button_cardRead_stop.Click += new System.EventHandler(this.button_cardRead_stop_Click);
            // 
            // label_cardnumber_card
            // 
            this.label_cardnumber_card.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel7.SetColumnSpan(this.label_cardnumber_card, 3);
            this.label_cardnumber_card.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_cardnumber_card.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.label_cardnumber_card.ForeColor = System.Drawing.Color.White;
            this.label_cardnumber_card.Location = new System.Drawing.Point(87, 94);
            this.label_cardnumber_card.Margin = new System.Windows.Forms.Padding(2);
            this.label_cardnumber_card.Name = "label_cardnumber_card";
            this.label_cardnumber_card.Size = new System.Drawing.Size(232, 60);
            this.label_cardnumber_card.TabIndex = 6;
            this.label_cardnumber_card.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(681, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel2.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(334, 486);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Digital Output";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.cb_do_ControlBoadrd_Reset, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_led_blue, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_mc, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_led_green, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_fan, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_led_red, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_ir_Led, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.cb_do_Rfid, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(328, 460);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // cb_do_ControlBoadrd_Reset
            // 
            this.cb_do_ControlBoadrd_Reset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_ControlBoadrd_Reset.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_ControlBoadrd_Reset.Location = new System.Drawing.Point(4, 156);
            this.cb_do_ControlBoadrd_Reset.Name = "cb_do_ControlBoadrd_Reset";
            this.cb_do_ControlBoadrd_Reset.Size = new System.Drawing.Size(156, 69);
            this.cb_do_ControlBoadrd_Reset.TabIndex = 1;
            this.cb_do_ControlBoadrd_Reset.Text = "제어보드\r\nRESET";
            this.cb_do_ControlBoadrd_Reset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_ControlBoadrd_Reset.UseVisualStyleBackColor = true;
            this.cb_do_ControlBoadrd_Reset.CheckedChanged += new System.EventHandler(this.cb_do_ControlBoadrd_Reset_CheckedChanged);
            // 
            // cb_do_led_blue
            // 
            this.cb_do_led_blue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_led_blue.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_blue.Location = new System.Drawing.Point(167, 156);
            this.cb_do_led_blue.Name = "cb_do_led_blue";
            this.cb_do_led_blue.Size = new System.Drawing.Size(157, 69);
            this.cb_do_led_blue.TabIndex = 0;
            this.cb_do_led_blue.Text = "LED (Blue)";
            this.cb_do_led_blue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_led_blue.UseVisualStyleBackColor = true;
            this.cb_do_led_blue.CheckedChanged += new System.EventHandler(this.cb_do_led_blue_CheckedChanged);
            // 
            // cb_do_mc
            // 
            this.cb_do_mc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_mc.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_mc.Location = new System.Drawing.Point(4, 4);
            this.cb_do_mc.Name = "cb_do_mc";
            this.cb_do_mc.Size = new System.Drawing.Size(156, 69);
            this.cb_do_mc.TabIndex = 0;
            this.cb_do_mc.Text = "MC 동작 (Input 없음)";
            this.cb_do_mc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_mc.UseVisualStyleBackColor = true;
            this.cb_do_mc.CheckedChanged += new System.EventHandler(this.cb_do_mc_CheckedChanged);
            // 
            // cb_do_led_green
            // 
            this.cb_do_led_green.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_led_green.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_green.Location = new System.Drawing.Point(167, 80);
            this.cb_do_led_green.Name = "cb_do_led_green";
            this.cb_do_led_green.Size = new System.Drawing.Size(157, 69);
            this.cb_do_led_green.TabIndex = 0;
            this.cb_do_led_green.Text = "LED (Green)";
            this.cb_do_led_green.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_led_green.UseVisualStyleBackColor = true;
            this.cb_do_led_green.CheckedChanged += new System.EventHandler(this.cb_do_led_green_CheckedChanged);
            // 
            // cb_do_fan
            // 
            this.cb_do_fan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_fan.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_fan.Location = new System.Drawing.Point(4, 80);
            this.cb_do_fan.Name = "cb_do_fan";
            this.cb_do_fan.Size = new System.Drawing.Size(156, 69);
            this.cb_do_fan.TabIndex = 0;
            this.cb_do_fan.Text = "FAN 동작 (Input 없음)";
            this.cb_do_fan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_fan.UseVisualStyleBackColor = true;
            this.cb_do_fan.CheckedChanged += new System.EventHandler(this.cb_do_fan_CheckedChanged);
            // 
            // cb_do_led_red
            // 
            this.cb_do_led_red.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_led_red.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_red.Location = new System.Drawing.Point(167, 4);
            this.cb_do_led_red.Name = "cb_do_led_red";
            this.cb_do_led_red.Size = new System.Drawing.Size(157, 69);
            this.cb_do_led_red.TabIndex = 0;
            this.cb_do_led_red.Text = "LED (Red)";
            this.cb_do_led_red.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_led_red.UseVisualStyleBackColor = true;
            this.cb_do_led_red.CheckedChanged += new System.EventHandler(this.cb_do_led_red_CheckedChanged);
            // 
            // cb_do_ir_Led
            // 
            this.cb_do_ir_Led.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_ir_Led.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_ir_Led.Location = new System.Drawing.Point(4, 232);
            this.cb_do_ir_Led.Name = "cb_do_ir_Led";
            this.cb_do_ir_Led.Size = new System.Drawing.Size(156, 69);
            this.cb_do_ir_Led.TabIndex = 2;
            this.cb_do_ir_Led.Text = "IR LED 동작";
            this.cb_do_ir_Led.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_ir_Led.UseVisualStyleBackColor = true;
            this.cb_do_ir_Led.CheckedChanged += new System.EventHandler(this.cb_do_ir_Led_CheckedChanged);
            // 
            // cb_do_Rfid
            // 
            this.cb_do_Rfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_do_Rfid.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_Rfid.Location = new System.Drawing.Point(167, 232);
            this.cb_do_Rfid.Name = "cb_do_Rfid";
            this.cb_do_Rfid.Size = new System.Drawing.Size(157, 69);
            this.cb_do_Rfid.TabIndex = 2;
            this.cb_do_Rfid.Text = "RFID 동작";
            this.cb_do_Rfid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_do_Rfid.UseVisualStyleBackColor = true;
            this.cb_do_Rfid.CheckedChanged += new System.EventHandler(this.cb_do_Rfid_CheckedChanged);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 139);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Digital Input";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cb_di_emg, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cb_di_sumRelay_plus, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cb_di_sumRelay_minus, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.pb_camera, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(666, 113);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(4, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(159, 36);
            this.label19.TabIndex = 1;
            this.label19.Text = "비상정지";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_di_emg
            // 
            this.cb_di_emg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_di_emg.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.cb_di_emg.ForeColor = System.Drawing.Color.Red;
            this.cb_di_emg.Location = new System.Drawing.Point(170, 4);
            this.cb_di_emg.Name = "cb_di_emg";
            this.cb_di_emg.Size = new System.Drawing.Size(159, 30);
            this.cb_di_emg.TabIndex = 0;
            this.cb_di_emg.Text = "OFF";
            this.cb_di_emg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_di_emg.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(4, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "SUM Relay +";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(4, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 37);
            this.label8.TabIndex = 1;
            this.label8.Text = "SUM Relay -";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_di_sumRelay_plus
            // 
            this.cb_di_sumRelay_plus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_di_sumRelay_plus.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.cb_di_sumRelay_plus.ForeColor = System.Drawing.Color.Red;
            this.cb_di_sumRelay_plus.Location = new System.Drawing.Point(170, 41);
            this.cb_di_sumRelay_plus.Name = "cb_di_sumRelay_plus";
            this.cb_di_sumRelay_plus.Size = new System.Drawing.Size(159, 30);
            this.cb_di_sumRelay_plus.TabIndex = 0;
            this.cb_di_sumRelay_plus.Text = "OFF";
            this.cb_di_sumRelay_plus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_di_sumRelay_plus.UseVisualStyleBackColor = true;
            // 
            // cb_di_sumRelay_minus
            // 
            this.cb_di_sumRelay_minus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_di_sumRelay_minus.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.cb_di_sumRelay_minus.ForeColor = System.Drawing.Color.Red;
            this.cb_di_sumRelay_minus.Location = new System.Drawing.Point(170, 78);
            this.cb_di_sumRelay_minus.Name = "cb_di_sumRelay_minus";
            this.cb_di_sumRelay_minus.Size = new System.Drawing.Size(159, 31);
            this.cb_di_sumRelay_minus.TabIndex = 0;
            this.cb_di_sumRelay_minus.Text = "OFF";
            this.cb_di_sumRelay_minus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_di_sumRelay_minus.UseVisualStyleBackColor = true;
            // 
            // pb_camera
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.pb_camera, 2);
            this.pb_camera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_camera.Location = new System.Drawing.Point(333, 1);
            this.pb_camera.Margin = new System.Windows.Forms.Padding(0);
            this.pb_camera.Name = "pb_camera";
            this.tableLayoutPanel3.SetRowSpan(this.pb_camera, 3);
            this.pb_camera.Size = new System.Drawing.Size(332, 111);
            this.pb_camera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_camera.TabIndex = 2;
            this.pb_camera.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel6);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(3, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(333, 341);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "통신 상태";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.cb_comstate_controlbd, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.cb_comstate_powerpack, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(327, 315);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // cb_comstate_controlbd
            // 
            this.cb_comstate_controlbd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_comstate_controlbd.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_comstate_controlbd.ForeColor = System.Drawing.Color.Red;
            this.cb_comstate_controlbd.Location = new System.Drawing.Point(167, 4);
            this.cb_comstate_controlbd.Name = "cb_comstate_controlbd";
            this.cb_comstate_controlbd.Size = new System.Drawing.Size(156, 71);
            this.cb_comstate_controlbd.TabIndex = 0;
            this.cb_comstate_controlbd.Text = "OFF";
            this.cb_comstate_controlbd.UseVisualStyleBackColor = true;
            // 
            // cb_comstate_powerpack
            // 
            this.cb_comstate_powerpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_comstate_powerpack.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_comstate_powerpack.ForeColor = System.Drawing.Color.Red;
            this.cb_comstate_powerpack.Location = new System.Drawing.Point(167, 82);
            this.cb_comstate_powerpack.Name = "cb_comstate_powerpack";
            this.cb_comstate_powerpack.Size = new System.Drawing.Size(156, 71);
            this.cb_comstate_powerpack.TabIndex = 0;
            this.cb_comstate_powerpack.Text = "OFF";
            this.cb_comstate_powerpack.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(4, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(156, 77);
            this.label17.TabIndex = 1;
            this.label17.Text = "충전제어보드";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(4, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(156, 77);
            this.label18.TabIndex = 1;
            this.label18.Text = "파워팩 #1";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ui_timer
            // 
            this.ui_timer.Interval = 1000;
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // bck_camera
            // 
            this.bck_camera.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bck_camera_DoWork);
            // 
            // UC_Setting_Manual_Common
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UC_Setting_Manual_Common";
            this.Size = new System.Drawing.Size(1024, 524);
            this.Load += new System.EventHandler(this.UC_Setting_Manual_Common_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_camera)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.CheckBox cb_di_emg;
        public System.Windows.Forms.CheckBox cb_do_mc;
        public System.Windows.Forms.CheckBox cb_do_fan;
        public System.Windows.Forms.CheckBox cb_do_led_red;
        public System.Windows.Forms.CheckBox cb_do_led_green;
        public System.Windows.Forms.CheckBox cb_do_led_blue;
        public System.Windows.Forms.CheckBox cb_comstate_controlbd;
        public System.Windows.Forms.CheckBox cb_comstate_powerpack;
        public System.Windows.Forms.CheckBox cb_do_ControlBoadrd_Reset;
        public System.Windows.Forms.CheckBox cb_do_ir_Led;
        public System.Windows.Forms.CheckBox cb_do_Rfid;
        public System.Windows.Forms.Button button_cardRead_start;
        public System.Windows.Forms.Button button_cardRead_stop;
        public System.Windows.Forms.Label label_cardnumber_card;
        public System.Windows.Forms.Button button_rfid_start;
        public System.Windows.Forms.Button button_rfid_stop;
        public System.Windows.Forms.Label label_cardnumber_rfid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.CheckBox cb_di_sumRelay_plus;
        public System.Windows.Forms.CheckBox cb_di_sumRelay_minus;
        private System.Windows.Forms.Timer ui_timer;
        public System.ComponentModel.BackgroundWorker bck_camera;
        public System.Windows.Forms.PictureBox pb_camera;
    }
}
