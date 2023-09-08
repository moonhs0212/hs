
namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl
{
    partial class UC_MainPage_Include_Cert_ManualControl_Output
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
            this.button_start_output = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_start_charging = new System.Windows.Forms.Button();
            this.label_state_outputcommand = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_chargingtime = new System.Windows.Forms.Label();
            this.label_chargingvoltage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_chargingcurrent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_chargingstate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button_rfid_start = new System.Windows.Forms.Button();
            this.button_rfid_stop = new System.Windows.Forms.Button();
            this.label_cardnumber = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.timer_uiupdate = new System.Windows.Forms.Timer(this.components);
            this.label_soc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_remaintime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_errorcode = new System.Windows.Forms.Label();
            this.label24234 = new System.Windows.Forms.Label();
            this.button_command_voltage = new System.Windows.Forms.Button();
            this.button_command_current = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_total_active_evergy_charging = new System.Windows.Forms.Label();
            this.label_total_active_evergy_finish = new System.Windows.Forms.Label();
            this.label_total_active_evergy_start = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cb_comstate_powerpack = new System.Windows.Forms.CheckBox();
            this.cb_comstate_controlbd = new System.Windows.Forms.CheckBox();
            this.cb_comstate_ami = new System.Windows.Forms.CheckBox();
            this.label_comstate_gun = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cb_di_relay_minus = new System.Windows.Forms.CheckBox();
            this.cb_di_relay_plus = new System.Windows.Forms.CheckBox();
            this.cb_di_emg = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cb_do_led_blue = new System.Windows.Forms.CheckBox();
            this.cb_do_led_green = new System.Windows.Forms.CheckBox();
            this.cb_do_mc = new System.Windows.Forms.CheckBox();
            this.cb_do_led_red = new System.Windows.Forms.CheckBox();
            this.cb_do_fan = new System.Windows.Forms.CheckBox();
            this.cb_do_relay_minus = new System.Windows.Forms.CheckBox();
            this.cb_do_relay_plus = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_start_output
            // 
            this.button_start_output.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_start_output.ForeColor = System.Drawing.Color.Black;
            this.button_start_output.Location = new System.Drawing.Point(23, 18);
            this.button_start_output.Name = "button_start_output";
            this.button_start_output.Size = new System.Drawing.Size(159, 44);
            this.button_start_output.TabIndex = 0;
            this.button_start_output.Text = "출력시작";
            this.button_start_output.UseVisualStyleBackColor = true;
            this.button_start_output.Click += new System.EventHandler(this.button_start_output_Click);
            // 
            // button_stop
            // 
            this.button_stop.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_stop.ForeColor = System.Drawing.Color.Black;
            this.button_stop.Location = new System.Drawing.Point(196, 18);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(136, 102);
            this.button_stop.TabIndex = 0;
            this.button_stop.Text = "종료";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_start_charging
            // 
            this.button_start_charging.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_start_charging.ForeColor = System.Drawing.Color.Black;
            this.button_start_charging.Location = new System.Drawing.Point(23, 67);
            this.button_start_charging.Name = "button_start_charging";
            this.button_start_charging.Size = new System.Drawing.Size(159, 54);
            this.button_start_charging.TabIndex = 0;
            this.button_start_charging.Text = "충전시작\r\n(차량연동)";
            this.button_start_charging.UseVisualStyleBackColor = true;
            this.button_start_charging.Click += new System.EventHandler(this.button_start_charging_Click);
            // 
            // label_state_outputcommand
            // 
            this.label_state_outputcommand.BackColor = System.Drawing.Color.DimGray;
            this.label_state_outputcommand.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_state_outputcommand.ForeColor = System.Drawing.Color.White;
            this.label_state_outputcommand.Location = new System.Drawing.Point(167, 16);
            this.label_state_outputcommand.Name = "label_state_outputcommand";
            this.label_state_outputcommand.Size = new System.Drawing.Size(140, 29);
            this.label_state_outputcommand.TabIndex = 1;
            this.label_state_outputcommand.Text = "종료요청중";
            this.label_state_outputcommand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "출력지령상태";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "경과시간";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_chargingtime
            // 
            this.label_chargingtime.BackColor = System.Drawing.Color.DimGray;
            this.label_chargingtime.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingtime.ForeColor = System.Drawing.Color.White;
            this.label_chargingtime.Location = new System.Drawing.Point(167, 157);
            this.label_chargingtime.Name = "label_chargingtime";
            this.label_chargingtime.Size = new System.Drawing.Size(140, 29);
            this.label_chargingtime.TabIndex = 1;
            this.label_chargingtime.Text = "00:00:00";
            this.label_chargingtime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_chargingvoltage
            // 
            this.label_chargingvoltage.BackColor = System.Drawing.Color.DimGray;
            this.label_chargingvoltage.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingvoltage.ForeColor = System.Drawing.Color.White;
            this.label_chargingvoltage.Location = new System.Drawing.Point(167, 191);
            this.label_chargingvoltage.Name = "label_chargingvoltage";
            this.label_chargingvoltage.Size = new System.Drawing.Size(140, 26);
            this.label_chargingvoltage.TabIndex = 1;
            this.label_chargingvoltage.Text = "0";
            this.label_chargingvoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(2, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "출력전압";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(312, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 33);
            this.label7.TabIndex = 1;
            this.label7.Text = "V";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chargingcurrent
            // 
            this.label_chargingcurrent.BackColor = System.Drawing.Color.DimGray;
            this.label_chargingcurrent.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingcurrent.ForeColor = System.Drawing.Color.White;
            this.label_chargingcurrent.Location = new System.Drawing.Point(167, 222);
            this.label_chargingcurrent.Name = "label_chargingcurrent";
            this.label_chargingcurrent.Size = new System.Drawing.Size(140, 25);
            this.label_chargingcurrent.TabIndex = 1;
            this.label_chargingcurrent.Text = "0";
            this.label_chargingcurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "출력전류";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(312, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 27);
            this.label10.TabIndex = 1;
            this.label10.Text = "A";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_chargingstate
            // 
            this.label_chargingstate.BackColor = System.Drawing.Color.DimGray;
            this.label_chargingstate.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_chargingstate.ForeColor = System.Drawing.Color.White;
            this.label_chargingstate.Location = new System.Drawing.Point(167, 251);
            this.label_chargingstate.Name = "label_chargingstate";
            this.label_chargingstate.Size = new System.Drawing.Size(140, 28);
            this.label_chargingstate.TabIndex = 1;
            this.label_chargingstate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(2, 251);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(147, 28);
            this.label12.TabIndex = 1;
            this.label12.Text = "제어상태";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(6, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 32);
            this.label14.TabIndex = 1;
            this.label14.Text = "최대 출력전압";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(148, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 31);
            this.label15.TabIndex = 7;
            this.label15.Text = "최대 출력전류";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_rfid_start
            // 
            this.button_rfid_start.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_rfid_start.ForeColor = System.Drawing.Color.Black;
            this.button_rfid_start.Location = new System.Drawing.Point(13, 26);
            this.button_rfid_start.Name = "button_rfid_start";
            this.button_rfid_start.Size = new System.Drawing.Size(126, 59);
            this.button_rfid_start.TabIndex = 0;
            this.button_rfid_start.Text = "읽기 시작";
            this.button_rfid_start.UseVisualStyleBackColor = true;
            this.button_rfid_start.Click += new System.EventHandler(this.button_rfid_start_Click);
            // 
            // button_rfid_stop
            // 
            this.button_rfid_stop.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_rfid_stop.ForeColor = System.Drawing.Color.Black;
            this.button_rfid_stop.Location = new System.Drawing.Point(157, 26);
            this.button_rfid_stop.Name = "button_rfid_stop";
            this.button_rfid_stop.Size = new System.Drawing.Size(126, 59);
            this.button_rfid_stop.TabIndex = 0;
            this.button_rfid_stop.Text = "읽기 종료";
            this.button_rfid_stop.UseVisualStyleBackColor = true;
            this.button_rfid_stop.Click += new System.EventHandler(this.button_rfid_stop_Click);
            // 
            // label_cardnumber
            // 
            this.label_cardnumber.BackColor = System.Drawing.Color.DimGray;
            this.label_cardnumber.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold);
            this.label_cardnumber.ForeColor = System.Drawing.Color.White;
            this.label_cardnumber.Location = new System.Drawing.Point(44, 99);
            this.label_cardnumber.Name = "label_cardnumber";
            this.label_cardnumber.Size = new System.Drawing.Size(249, 48);
            this.label_cardnumber.TabIndex = 1;
            this.label_cardnumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(-15, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 62);
            this.label16.TabIndex = 1;
            this.label16.Text = "카드\r\n번호";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer_uiupdate
            // 
            this.timer_uiupdate.Interval = 1000;
            this.timer_uiupdate.Tick += new System.EventHandler(this.timer_uiupdate_Tick);
            // 
            // label_soc
            // 
            this.label_soc.BackColor = System.Drawing.Color.DimGray;
            this.label_soc.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_soc.ForeColor = System.Drawing.Color.White;
            this.label_soc.Location = new System.Drawing.Point(167, 284);
            this.label_soc.Name = "label_soc";
            this.label_soc.Size = new System.Drawing.Size(140, 28);
            this.label_soc.TabIndex = 1;
            this.label_soc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 28);
            this.label4.TabIndex = 1;
            this.label4.Text = "S.O.C";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(312, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 27);
            this.label5.TabIndex = 1;
            this.label5.Text = "%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_remaintime
            // 
            this.label_remaintime.BackColor = System.Drawing.Color.DimGray;
            this.label_remaintime.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_remaintime.ForeColor = System.Drawing.Color.White;
            this.label_remaintime.Location = new System.Drawing.Point(167, 317);
            this.label_remaintime.Name = "label_remaintime";
            this.label_remaintime.Size = new System.Drawing.Size(140, 28);
            this.label_remaintime.TabIndex = 1;
            this.label_remaintime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 28);
            this.label8.TabIndex = 1;
            this.label8.Text = "남은시간";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(312, 319);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(159, 27);
            this.label11.TabIndex = 1;
            this.label11.Text = "분";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_errorcode
            // 
            this.label_errorcode.BackColor = System.Drawing.Color.DimGray;
            this.label_errorcode.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_errorcode.ForeColor = System.Drawing.Color.White;
            this.label_errorcode.Location = new System.Drawing.Point(167, 349);
            this.label_errorcode.Name = "label_errorcode";
            this.label_errorcode.Size = new System.Drawing.Size(140, 28);
            this.label_errorcode.TabIndex = 1;
            this.label_errorcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24234
            // 
            this.label24234.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24234.ForeColor = System.Drawing.Color.White;
            this.label24234.Location = new System.Drawing.Point(2, 349);
            this.label24234.Name = "label24234";
            this.label24234.Size = new System.Drawing.Size(147, 28);
            this.label24234.TabIndex = 1;
            this.label24234.Text = "오류코드";
            this.label24234.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_command_voltage
            // 
            this.button_command_voltage.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_command_voltage.ForeColor = System.Drawing.Color.Black;
            this.button_command_voltage.Location = new System.Drawing.Point(13, 56);
            this.button_command_voltage.Name = "button_command_voltage";
            this.button_command_voltage.Size = new System.Drawing.Size(126, 66);
            this.button_command_voltage.TabIndex = 0;
            this.button_command_voltage.Text = "0";
            this.button_command_voltage.UseVisualStyleBackColor = true;
            this.button_command_voltage.Click += new System.EventHandler(this.button_command_voltage_Click);
            // 
            // button_command_current
            // 
            this.button_command_current.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_command_current.ForeColor = System.Drawing.Color.Black;
            this.button_command_current.Location = new System.Drawing.Point(157, 56);
            this.button_command_current.Name = "button_command_current";
            this.button_command_current.Size = new System.Drawing.Size(126, 66);
            this.button_command_current.TabIndex = 0;
            this.button_command_current.Text = "0";
            this.button_command_current.UseVisualStyleBackColor = true;
            this.button_command_current.Click += new System.EventHandler(this.button_command_current_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_rfid_stop);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.button_rfid_start);
            this.groupBox1.Controls.Add(this.label_cardnumber);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(385, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 164);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "카드단말기";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.button_command_voltage);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.button_command_current);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(385, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 135);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "출력 설정";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_start_output);
            this.groupBox3.Controls.Add(this.button_start_charging);
            this.groupBox3.Controls.Add(this.button_stop);
            this.groupBox3.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(7, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 128);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "충전제어";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_chargingtime);
            this.groupBox4.Controls.Add(this.label_total_active_evergy_charging);
            this.groupBox4.Controls.Add(this.label_total_active_evergy_finish);
            this.groupBox4.Controls.Add(this.label_total_active_evergy_start);
            this.groupBox4.Controls.Add(this.label_chargingvoltage);
            this.groupBox4.Controls.Add(this.label_chargingcurrent);
            this.groupBox4.Controls.Add(this.label_chargingstate);
            this.groupBox4.Controls.Add(this.label_state_outputcommand);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label_soc);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label_remaintime);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label_errorcode);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label24234);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(7, 129);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(350, 387);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "제어 상태";
            // 
            // label_total_active_evergy_charging
            // 
            this.label_total_active_evergy_charging.BackColor = System.Drawing.Color.DimGray;
            this.label_total_active_evergy_charging.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_total_active_evergy_charging.ForeColor = System.Drawing.Color.White;
            this.label_total_active_evergy_charging.Location = new System.Drawing.Point(167, 120);
            this.label_total_active_evergy_charging.Name = "label_total_active_evergy_charging";
            this.label_total_active_evergy_charging.Size = new System.Drawing.Size(140, 26);
            this.label_total_active_evergy_charging.TabIndex = 1;
            this.label_total_active_evergy_charging.Text = "0";
            this.label_total_active_evergy_charging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_total_active_evergy_finish
            // 
            this.label_total_active_evergy_finish.BackColor = System.Drawing.Color.DimGray;
            this.label_total_active_evergy_finish.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_total_active_evergy_finish.ForeColor = System.Drawing.Color.White;
            this.label_total_active_evergy_finish.Location = new System.Drawing.Point(167, 88);
            this.label_total_active_evergy_finish.Name = "label_total_active_evergy_finish";
            this.label_total_active_evergy_finish.Size = new System.Drawing.Size(140, 26);
            this.label_total_active_evergy_finish.TabIndex = 1;
            this.label_total_active_evergy_finish.Text = "0";
            this.label_total_active_evergy_finish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_total_active_evergy_start
            // 
            this.label_total_active_evergy_start.BackColor = System.Drawing.Color.DimGray;
            this.label_total_active_evergy_start.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_total_active_evergy_start.ForeColor = System.Drawing.Color.White;
            this.label_total_active_evergy_start.Location = new System.Drawing.Point(167, 56);
            this.label_total_active_evergy_start.Name = "label_total_active_evergy_start";
            this.label_total_active_evergy_start.Size = new System.Drawing.Size(140, 26);
            this.label_total_active_evergy_start.TabIndex = 1;
            this.label_total_active_evergy_start.Text = "0";
            this.label_total_active_evergy_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(312, 120);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(60, 33);
            this.label29.TabIndex = 1;
            this.label29.Text = "kWh";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(312, 88);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 33);
            this.label26.TabIndex = 1;
            this.label26.Text = "kWh";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(312, 56);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 33);
            this.label23.TabIndex = 1;
            this.label23.Text = "kWh";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(7, 120);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(154, 26);
            this.label28.TabIndex = 1;
            this.label28.Text = "누적전력량(Charging)";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(7, 88);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(154, 26);
            this.label25.TabIndex = 1;
            this.label25.Text = "누적전력량(Realtime)";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(7, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(154, 26);
            this.label22.TabIndex = 1;
            this.label22.Text = "누적전력량(Start)";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cb_comstate_powerpack);
            this.groupBox5.Controls.Add(this.cb_comstate_controlbd);
            this.groupBox5.Controls.Add(this.cb_comstate_ami);
            this.groupBox5.Controls.Add(this.label_comstate_gun);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(385, 307);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(299, 209);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "통신 상태";
            // 
            // cb_comstate_powerpack
            // 
            this.cb_comstate_powerpack.ForeColor = System.Drawing.Color.Red;
            this.cb_comstate_powerpack.Location = new System.Drawing.Point(150, 110);
            this.cb_comstate_powerpack.Name = "cb_comstate_powerpack";
            this.cb_comstate_powerpack.Size = new System.Drawing.Size(144, 29);
            this.cb_comstate_powerpack.TabIndex = 0;
            this.cb_comstate_powerpack.Text = "OFF";
            this.cb_comstate_powerpack.UseVisualStyleBackColor = true;
            // 
            // cb_comstate_controlbd
            // 
            this.cb_comstate_controlbd.ForeColor = System.Drawing.Color.Red;
            this.cb_comstate_controlbd.Location = new System.Drawing.Point(150, 74);
            this.cb_comstate_controlbd.Name = "cb_comstate_controlbd";
            this.cb_comstate_controlbd.Size = new System.Drawing.Size(144, 29);
            this.cb_comstate_controlbd.TabIndex = 0;
            this.cb_comstate_controlbd.Text = "OFF";
            this.cb_comstate_controlbd.UseVisualStyleBackColor = true;
            // 
            // cb_comstate_ami
            // 
            this.cb_comstate_ami.ForeColor = System.Drawing.Color.Red;
            this.cb_comstate_ami.Location = new System.Drawing.Point(150, 38);
            this.cb_comstate_ami.Name = "cb_comstate_ami";
            this.cb_comstate_ami.Size = new System.Drawing.Size(144, 29);
            this.cb_comstate_ami.TabIndex = 0;
            this.cb_comstate_ami.Text = "OFF";
            this.cb_comstate_ami.UseVisualStyleBackColor = true;
            // 
            // label_comstate_gun
            // 
            this.label_comstate_gun.BackColor = System.Drawing.Color.DimGray;
            this.label_comstate_gun.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_comstate_gun.ForeColor = System.Drawing.Color.White;
            this.label_comstate_gun.Location = new System.Drawing.Point(147, 147);
            this.label_comstate_gun.Name = "label_comstate_gun";
            this.label_comstate_gun.Size = new System.Drawing.Size(140, 28);
            this.label_comstate_gun.TabIndex = 1;
            this.label_comstate_gun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(-7, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(147, 28);
            this.label18.TabIndex = 1;
            this.label18.Text = "파워팩 #1";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(-7, 73);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(147, 28);
            this.label17.TabIndex = 1;
            this.label17.Text = "충전제어보드";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(-7, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(147, 28);
            this.label13.TabIndex = 1;
            this.label13.Text = "전력량계";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-18, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 28);
            this.label3.TabIndex = 1;
            this.label3.Text = "충전건 상태";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cb_di_relay_minus);
            this.groupBox6.Controls.Add(this.cb_di_relay_plus);
            this.groupBox6.Controls.Add(this.cb_di_emg);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(709, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(299, 136);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Digital Input";
            // 
            // cb_di_relay_minus
            // 
            this.cb_di_relay_minus.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_di_relay_minus.ForeColor = System.Drawing.Color.Red;
            this.cb_di_relay_minus.Location = new System.Drawing.Point(178, 100);
            this.cb_di_relay_minus.Name = "cb_di_relay_minus";
            this.cb_di_relay_minus.Size = new System.Drawing.Size(211, 29);
            this.cb_di_relay_minus.TabIndex = 0;
            this.cb_di_relay_minus.Text = "OFF";
            this.cb_di_relay_minus.UseVisualStyleBackColor = true;
            // 
            // cb_di_relay_plus
            // 
            this.cb_di_relay_plus.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_di_relay_plus.ForeColor = System.Drawing.Color.Red;
            this.cb_di_relay_plus.Location = new System.Drawing.Point(178, 61);
            this.cb_di_relay_plus.Name = "cb_di_relay_plus";
            this.cb_di_relay_plus.Size = new System.Drawing.Size(211, 29);
            this.cb_di_relay_plus.TabIndex = 0;
            this.cb_di_relay_plus.Text = "OFF";
            this.cb_di_relay_plus.UseVisualStyleBackColor = true;
            // 
            // cb_di_emg
            // 
            this.cb_di_emg.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Bold);
            this.cb_di_emg.ForeColor = System.Drawing.Color.Red;
            this.cb_di_emg.Location = new System.Drawing.Point(178, 26);
            this.cb_di_emg.Name = "cb_di_emg";
            this.cb_di_emg.Size = new System.Drawing.Size(272, 29);
            this.cb_di_emg.TabIndex = 0;
            this.cb_di_emg.Text = "OFF";
            this.cb_di_emg.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(6, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(147, 28);
            this.label19.TabIndex = 1;
            this.label19.Text = "비상정지 버튼";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(6, 93);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(147, 28);
            this.label21.TabIndex = 1;
            this.label21.Text = "- Relay";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(6, 60);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(147, 28);
            this.label20.TabIndex = 1;
            this.label20.Text = "+ Relay";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cb_do_led_blue);
            this.groupBox7.Controls.Add(this.cb_do_led_green);
            this.groupBox7.Controls.Add(this.cb_do_mc);
            this.groupBox7.Controls.Add(this.cb_do_led_red);
            this.groupBox7.Controls.Add(this.cb_do_fan);
            this.groupBox7.Controls.Add(this.cb_do_relay_minus);
            this.groupBox7.Controls.Add(this.cb_do_relay_plus);
            this.groupBox7.Font = new System.Drawing.Font("굴림", 13F);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(709, 142);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(299, 374);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Digital Output";
            // 
            // cb_do_led_blue
            // 
            this.cb_do_led_blue.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_blue.Location = new System.Drawing.Point(21, 321);
            this.cb_do_led_blue.Name = "cb_do_led_blue";
            this.cb_do_led_blue.Size = new System.Drawing.Size(227, 29);
            this.cb_do_led_blue.TabIndex = 0;
            this.cb_do_led_blue.Text = "LED (Blue)";
            this.cb_do_led_blue.UseVisualStyleBackColor = true;
            this.cb_do_led_blue.CheckedChanged += new System.EventHandler(this.cb_do_led_blue_CheckedChanged);
            // 
            // cb_do_led_green
            // 
            this.cb_do_led_green.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_green.Location = new System.Drawing.Point(21, 271);
            this.cb_do_led_green.Name = "cb_do_led_green";
            this.cb_do_led_green.Size = new System.Drawing.Size(227, 29);
            this.cb_do_led_green.TabIndex = 0;
            this.cb_do_led_green.Text = "LED (Green)";
            this.cb_do_led_green.UseVisualStyleBackColor = true;
            this.cb_do_led_green.CheckedChanged += new System.EventHandler(this.cb_do_led_green_CheckedChanged);
            // 
            // cb_do_mc
            // 
            this.cb_do_mc.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_mc.Location = new System.Drawing.Point(21, 129);
            this.cb_do_mc.Name = "cb_do_mc";
            this.cb_do_mc.Size = new System.Drawing.Size(262, 29);
            this.cb_do_mc.TabIndex = 0;
            this.cb_do_mc.Text = "MC 동작 (Input 없음)";
            this.cb_do_mc.UseVisualStyleBackColor = true;
            this.cb_do_mc.CheckedChanged += new System.EventHandler(this.cb_do_mc_CheckedChanged);
            // 
            // cb_do_led_red
            // 
            this.cb_do_led_red.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_led_red.Location = new System.Drawing.Point(21, 223);
            this.cb_do_led_red.Name = "cb_do_led_red";
            this.cb_do_led_red.Size = new System.Drawing.Size(227, 29);
            this.cb_do_led_red.TabIndex = 0;
            this.cb_do_led_red.Text = "LED (Red)";
            this.cb_do_led_red.UseVisualStyleBackColor = true;
            this.cb_do_led_red.CheckedChanged += new System.EventHandler(this.cb_do_led_red_CheckedChanged);
            // 
            // cb_do_fan
            // 
            this.cb_do_fan.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_fan.Location = new System.Drawing.Point(21, 176);
            this.cb_do_fan.Name = "cb_do_fan";
            this.cb_do_fan.Size = new System.Drawing.Size(262, 29);
            this.cb_do_fan.TabIndex = 0;
            this.cb_do_fan.Text = "FAN 동작 (Input 없음)";
            this.cb_do_fan.UseVisualStyleBackColor = true;
            this.cb_do_fan.CheckedChanged += new System.EventHandler(this.cb_do_fan_CheckedChanged);
            // 
            // cb_do_relay_minus
            // 
            this.cb_do_relay_minus.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_relay_minus.Location = new System.Drawing.Point(21, 82);
            this.cb_do_relay_minus.Name = "cb_do_relay_minus";
            this.cb_do_relay_minus.Size = new System.Drawing.Size(227, 29);
            this.cb_do_relay_minus.TabIndex = 0;
            this.cb_do_relay_minus.Text = "- Relay";
            this.cb_do_relay_minus.UseVisualStyleBackColor = true;
            this.cb_do_relay_minus.CheckedChanged += new System.EventHandler(this.cb_do_relay_minus_CheckedChanged);
            // 
            // cb_do_relay_plus
            // 
            this.cb_do_relay_plus.Font = new System.Drawing.Font("굴림", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_do_relay_plus.Location = new System.Drawing.Point(21, 36);
            this.cb_do_relay_plus.Name = "cb_do_relay_plus";
            this.cb_do_relay_plus.Size = new System.Drawing.Size(227, 29);
            this.cb_do_relay_plus.TabIndex = 0;
            this.cb_do_relay_plus.Text = "+ Relay";
            this.cb_do_relay_plus.UseVisualStyleBackColor = true;
            this.cb_do_relay_plus.CheckedChanged += new System.EventHandler(this.cb_do_relay_plus_CheckedChanged);
            // 
            // UC_MainPage_Include_Cert_ManualControl_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "UC_MainPage_Include_Cert_ManualControl_Output";
            this.Size = new System.Drawing.Size(1024, 526);
            this.Load += new System.EventHandler(this.UC_MainPage_Include_Cert_ManualControl_Output_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start_output;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_start_charging;
        private System.Windows.Forms.Label label_state_outputcommand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_chargingtime;
        private System.Windows.Forms.Label label_chargingvoltage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_chargingcurrent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_chargingstate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_rfid_start;
        private System.Windows.Forms.Button button_rfid_stop;
        private System.Windows.Forms.Label label_cardnumber;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Timer timer_uiupdate;
        private System.Windows.Forms.Label label_soc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_remaintime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_errorcode;
        private System.Windows.Forms.Label label24234;
        private System.Windows.Forms.Button button_command_voltage;
        private System.Windows.Forms.Button button_command_current;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cb_comstate_powerpack;
        private System.Windows.Forms.CheckBox cb_comstate_controlbd;
        private System.Windows.Forms.CheckBox cb_comstate_ami;
        private System.Windows.Forms.Label label_comstate_gun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cb_di_relay_minus;
        private System.Windows.Forms.CheckBox cb_di_relay_plus;
        private System.Windows.Forms.CheckBox cb_di_emg;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox cb_do_led_blue;
        private System.Windows.Forms.CheckBox cb_do_led_green;
        private System.Windows.Forms.CheckBox cb_do_mc;
        private System.Windows.Forms.CheckBox cb_do_led_red;
        private System.Windows.Forms.CheckBox cb_do_fan;
        private System.Windows.Forms.CheckBox cb_do_relay_minus;
        private System.Windows.Forms.CheckBox cb_do_relay_plus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label_total_active_evergy_charging;
        private System.Windows.Forms.Label label_total_active_evergy_finish;
        private System.Windows.Forms.Label label_total_active_evergy_start;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label22;
    }
}
