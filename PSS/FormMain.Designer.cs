namespace PSS
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.portConfig = new System.Windows.Forms.GroupBox();
            this.openPortBt = new System.Windows.Forms.Button();
            this.baudRate = new System.Windows.Forms.ComboBox();
            this.stopBits = new System.Windows.Forms.ComboBox();
            this.dataBits = new System.Windows.Forms.ComboBox();
            this.parity = new System.Windows.Forms.ComboBox();
            this.portNumber = new System.Windows.Forms.ComboBox();
            this.label_stopBits = new System.Windows.Forms.Label();
            this.label_dataBits = new System.Windows.Forms.Label();
            this.label_parity = new System.Windows.Forms.Label();
            this.label_baudRate = new System.Windows.Forms.Label();
            this.label_portNumber = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RearcomboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Front2comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Front1comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.devComfirmBt = new System.Windows.Forms.Button();
            this.textBox_fileDir = new System.Windows.Forms.TextBox();
            this.openFileBt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.inH1Bt = new System.Windows.Forms.Button();
            this.H1textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.inH2Bt = new System.Windows.Forms.Button();
            this.H2textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.inLBt = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.debugLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Rxlabel = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.b_label = new System.Windows.Forms.Label();
            this.a_label = new System.Windows.Forms.Label();
            this.noticelabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.portConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // portConfig
            // 
            this.portConfig.Controls.Add(this.openPortBt);
            this.portConfig.Controls.Add(this.baudRate);
            this.portConfig.Controls.Add(this.stopBits);
            this.portConfig.Controls.Add(this.dataBits);
            this.portConfig.Controls.Add(this.parity);
            this.portConfig.Controls.Add(this.portNumber);
            this.portConfig.Controls.Add(this.label_stopBits);
            this.portConfig.Controls.Add(this.label_dataBits);
            this.portConfig.Controls.Add(this.label_parity);
            this.portConfig.Controls.Add(this.label_baudRate);
            this.portConfig.Controls.Add(this.label_portNumber);
            this.portConfig.Location = new System.Drawing.Point(21, 126);
            this.portConfig.Name = "portConfig";
            this.portConfig.Size = new System.Drawing.Size(163, 245);
            this.portConfig.TabIndex = 2;
            this.portConfig.TabStop = false;
            this.portConfig.Text = "2.串口配置";
            // 
            // openPortBt
            // 
            this.openPortBt.Enabled = false;
            this.openPortBt.Location = new System.Drawing.Point(39, 211);
            this.openPortBt.Name = "openPortBt";
            this.openPortBt.Size = new System.Drawing.Size(75, 23);
            this.openPortBt.TabIndex = 10;
            this.openPortBt.Text = "打开串口";
            this.openPortBt.UseVisualStyleBackColor = true;
            this.openPortBt.Click += new System.EventHandler(this.openPortBt_Click);
            // 
            // baudRate
            // 
            this.baudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRate.Enabled = false;
            this.baudRate.FormattingEnabled = true;
            this.baudRate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.baudRate.Location = new System.Drawing.Point(74, 61);
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(72, 20);
            this.baudRate.TabIndex = 4;
            // 
            // stopBits
            // 
            this.stopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBits.Enabled = false;
            this.stopBits.FormattingEnabled = true;
            this.stopBits.Items.AddRange(new object[] {
            "One",
            "Two",
            "OnePointFive"});
            this.stopBits.Location = new System.Drawing.Point(74, 169);
            this.stopBits.Name = "stopBits";
            this.stopBits.Size = new System.Drawing.Size(72, 20);
            this.stopBits.TabIndex = 9;
            // 
            // dataBits
            // 
            this.dataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBits.Enabled = false;
            this.dataBits.FormattingEnabled = true;
            this.dataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.dataBits.Location = new System.Drawing.Point(74, 133);
            this.dataBits.Name = "dataBits";
            this.dataBits.Size = new System.Drawing.Size(72, 20);
            this.dataBits.TabIndex = 8;
            // 
            // parity
            // 
            this.parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parity.Enabled = false;
            this.parity.FormattingEnabled = true;
            this.parity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.parity.Location = new System.Drawing.Point(74, 97);
            this.parity.Name = "parity";
            this.parity.Size = new System.Drawing.Size(72, 20);
            this.parity.TabIndex = 7;
            // 
            // portNumber
            // 
            this.portNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNumber.Enabled = false;
            this.portNumber.FormattingEnabled = true;
            this.portNumber.Location = new System.Drawing.Point(74, 25);
            this.portNumber.Name = "portNumber";
            this.portNumber.Size = new System.Drawing.Size(72, 20);
            this.portNumber.TabIndex = 5;
            this.portNumber.Click += new System.EventHandler(this.portNumber_Click);
            // 
            // label_stopBits
            // 
            this.label_stopBits.AutoSize = true;
            this.label_stopBits.Location = new System.Drawing.Point(20, 174);
            this.label_stopBits.Name = "label_stopBits";
            this.label_stopBits.Size = new System.Drawing.Size(47, 12);
            this.label_stopBits.TabIndex = 4;
            this.label_stopBits.Text = "停止位:";
            // 
            // label_dataBits
            // 
            this.label_dataBits.AutoSize = true;
            this.label_dataBits.Location = new System.Drawing.Point(20, 138);
            this.label_dataBits.Name = "label_dataBits";
            this.label_dataBits.Size = new System.Drawing.Size(47, 12);
            this.label_dataBits.TabIndex = 3;
            this.label_dataBits.Text = "数据位:";
            // 
            // label_parity
            // 
            this.label_parity.AutoSize = true;
            this.label_parity.Location = new System.Drawing.Point(20, 102);
            this.label_parity.Name = "label_parity";
            this.label_parity.Size = new System.Drawing.Size(47, 12);
            this.label_parity.TabIndex = 2;
            this.label_parity.Text = "校验位:";
            // 
            // label_baudRate
            // 
            this.label_baudRate.AutoSize = true;
            this.label_baudRate.Location = new System.Drawing.Point(20, 66);
            this.label_baudRate.Name = "label_baudRate";
            this.label_baudRate.Size = new System.Drawing.Size(47, 12);
            this.label_baudRate.TabIndex = 1;
            this.label_baudRate.Text = "波特率:";
            // 
            // label_portNumber
            // 
            this.label_portNumber.AutoSize = true;
            this.label_portNumber.Location = new System.Drawing.Point(20, 30);
            this.label_portNumber.Name = "label_portNumber";
            this.label_portNumber.Size = new System.Drawing.Size(47, 12);
            this.label_portNumber.TabIndex = 0;
            this.label_portNumber.Text = "串口号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RearcomboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Front2comboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Front1comboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.devComfirmBt);
            this.groupBox1.Location = new System.Drawing.Point(21, 412);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 199);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3.采集设备";
            // 
            // RearcomboBox
            // 
            this.RearcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RearcomboBox.Enabled = false;
            this.RearcomboBox.FormattingEnabled = true;
            this.RearcomboBox.Items.AddRange(new object[] {
            "设备0",
            "设备1",
            "设备2",
            "设备3",
            "设备4",
            "设备5"});
            this.RearcomboBox.Location = new System.Drawing.Point(72, 104);
            this.RearcomboBox.Name = "RearcomboBox";
            this.RearcomboBox.Size = new System.Drawing.Size(72, 20);
            this.RearcomboBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "单轮";
            // 
            // Front2comboBox
            // 
            this.Front2comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Front2comboBox.Enabled = false;
            this.Front2comboBox.FormattingEnabled = true;
            this.Front2comboBox.Items.AddRange(new object[] {
            "设备0",
            "设备1",
            "设备2",
            "设备3",
            "设备4",
            "设备5"});
            this.Front2comboBox.Location = new System.Drawing.Point(73, 65);
            this.Front2comboBox.Name = "Front2comboBox";
            this.Front2comboBox.Size = new System.Drawing.Size(72, 20);
            this.Front2comboBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "双轮(右)";
            // 
            // Front1comboBox
            // 
            this.Front1comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Front1comboBox.Enabled = false;
            this.Front1comboBox.FormattingEnabled = true;
            this.Front1comboBox.Items.AddRange(new object[] {
            "设备0",
            "设备1",
            "设备2",
            "设备3",
            "设备4",
            "设备5"});
            this.Front1comboBox.Location = new System.Drawing.Point(74, 25);
            this.Front1comboBox.Name = "Front1comboBox";
            this.Front1comboBox.Size = new System.Drawing.Size(72, 20);
            this.Front1comboBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "双轮(左)";
            // 
            // devComfirmBt
            // 
            this.devComfirmBt.Enabled = false;
            this.devComfirmBt.Location = new System.Drawing.Point(44, 147);
            this.devComfirmBt.Name = "devComfirmBt";
            this.devComfirmBt.Size = new System.Drawing.Size(75, 23);
            this.devComfirmBt.TabIndex = 0;
            this.devComfirmBt.Text = "确认";
            this.devComfirmBt.UseVisualStyleBackColor = true;
            this.devComfirmBt.Click += new System.EventHandler(this.devComfirmBt_Click);
            // 
            // textBox_fileDir
            // 
            this.textBox_fileDir.Location = new System.Drawing.Point(263, 40);
            this.textBox_fileDir.Name = "textBox_fileDir";
            this.textBox_fileDir.ReadOnly = true;
            this.textBox_fileDir.Size = new System.Drawing.Size(774, 21);
            this.textBox_fileDir.TabIndex = 0;
            // 
            // openFileBt
            // 
            this.openFileBt.Location = new System.Drawing.Point(31, 26);
            this.openFileBt.Name = "openFileBt";
            this.openFileBt.Size = new System.Drawing.Size(106, 23);
            this.openFileBt.TabIndex = 1;
            this.openFileBt.Text = "打开图纸";
            this.openFileBt.UseVisualStyleBackColor = true;
            this.openFileBt.Click += new System.EventHandler(this.openFileBt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.openFileBt);
            this.groupBox2.Location = new System.Drawing.Point(21, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 63);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1.导入文件";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PSS.Properties.Resources.logo;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(1067, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(185, 94);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::PSS.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(203, 166);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1049, 478);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.inH1Bt);
            this.groupBox3.Controls.Add(this.H1textBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(424, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 96);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "5.第一次测量";
            // 
            // inH1Bt
            // 
            this.inH1Bt.Enabled = false;
            this.inH1Bt.Location = new System.Drawing.Point(58, 65);
            this.inH1Bt.Name = "inH1Bt";
            this.inH1Bt.Size = new System.Drawing.Size(75, 23);
            this.inH1Bt.TabIndex = 24;
            this.inH1Bt.Text = "确认";
            this.inH1Bt.UseVisualStyleBackColor = true;
            this.inH1Bt.Click += new System.EventHandler(this.inH1Bt_Click);
            // 
            // H1textBox
            // 
            this.H1textBox.Location = new System.Drawing.Point(58, 31);
            this.H1textBox.Name = "H1textBox";
            this.H1textBox.Size = new System.Drawing.Size(100, 21);
            this.H1textBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "输入H";
            // 
            // LtextBox
            // 
            this.LtextBox.Location = new System.Drawing.Point(60, 31);
            this.LtextBox.Name = "LtextBox";
            this.LtextBox.Size = new System.Drawing.Size(100, 21);
            this.LtextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入L";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.inH2Bt);
            this.groupBox4.Controls.Add(this.H2textBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(656, 70);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(181, 93);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "6.第二次测量";
            // 
            // inH2Bt
            // 
            this.inH2Bt.Enabled = false;
            this.inH2Bt.Location = new System.Drawing.Point(59, 63);
            this.inH2Bt.Name = "inH2Bt";
            this.inH2Bt.Size = new System.Drawing.Size(75, 23);
            this.inH2Bt.TabIndex = 25;
            this.inH2Bt.Text = "确认";
            this.inH2Bt.UseVisualStyleBackColor = true;
            this.inH2Bt.Click += new System.EventHandler(this.inH2Bt_Click);
            // 
            // H2textBox
            // 
            this.H2textBox.Location = new System.Drawing.Point(59, 24);
            this.H2textBox.Name = "H2textBox";
            this.H2textBox.Size = new System.Drawing.Size(100, 21);
            this.H2textBox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "输入H";
            // 
            // inLBt
            // 
            this.inLBt.Enabled = false;
            this.inLBt.Location = new System.Drawing.Point(50, 65);
            this.inLBt.Name = "inLBt";
            this.inLBt.Size = new System.Drawing.Size(75, 23);
            this.inLBt.TabIndex = 23;
            this.inLBt.Text = "确认";
            this.inLBt.UseVisualStyleBackColor = true;
            this.inLBt.Click += new System.EventHandler(this.inLBt_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.inLBt);
            this.groupBox5.Controls.Add(this.LtextBox);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(203, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(173, 96);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "4.输入参数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 628);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "Copyright Skyrover Tech Inc.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(217, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "路径";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(203, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(853, 41);
            this.groupBox6.TabIndex = 28;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "图纸";
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(1177, 675);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(11, 12);
            this.debugLabel.TabIndex = 26;
            this.debugLabel.Text = " ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1121, 658);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "接收:";
            // 
            // Rxlabel
            // 
            this.Rxlabel.AutoSize = true;
            this.Rxlabel.Location = new System.Drawing.Point(1166, 658);
            this.Rxlabel.Name = "Rxlabel";
            this.Rxlabel.Size = new System.Drawing.Size(11, 12);
            this.Rxlabel.TabIndex = 30;
            this.Rxlabel.Text = "0";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.b_label);
            this.groupBox7.Controls.Add(this.a_label);
            this.groupBox7.Location = new System.Drawing.Point(875, 70);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(175, 93);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "7.重心位置";
            // 
            // b_label
            // 
            this.b_label.AutoSize = true;
            this.b_label.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.b_label.Location = new System.Drawing.Point(10, 57);
            this.b_label.Name = "b_label";
            this.b_label.Size = new System.Drawing.Size(70, 21);
            this.b_label.TabIndex = 1;
            this.b_label.Text = "b = ?";
            // 
            // a_label
            // 
            this.a_label.AutoSize = true;
            this.a_label.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.a_label.Location = new System.Drawing.Point(9, 26);
            this.a_label.Name = "a_label";
            this.a_label.Size = new System.Drawing.Size(70, 21);
            this.a_label.TabIndex = 0;
            this.a_label.Text = "a = ?";
            // 
            // noticelabel
            // 
            this.noticelabel.AutoSize = true;
            this.noticelabel.Location = new System.Drawing.Point(245, 658);
            this.noticelabel.Name = "noticelabel";
            this.noticelabel.Size = new System.Drawing.Size(101, 12);
            this.noticelabel.TabIndex = 32;
            this.noticelabel.Text = "请导入图纸文件！";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1057, 658);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "通信状态：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(203, 658);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "提示：";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.noticelabel);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.Rxlabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBox_fileDir);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.portConfig);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "创壹通航-重心测量PC客户端 v1.0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.portConfig.ResumeLayout(false);
            this.portConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox portConfig;
        public System.Windows.Forms.Button openPortBt;
        private System.Windows.Forms.ComboBox baudRate;
        private System.Windows.Forms.ComboBox stopBits;
        private System.Windows.Forms.ComboBox dataBits;
        private System.Windows.Forms.ComboBox parity;
        private System.Windows.Forms.ComboBox portNumber;
        private System.Windows.Forms.Label label_stopBits;
        private System.Windows.Forms.Label label_dataBits;
        private System.Windows.Forms.Label label_parity;
        private System.Windows.Forms.Label label_baudRate;
        private System.Windows.Forms.Label label_portNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox RearcomboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Front2comboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Front1comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button devComfirmBt;
        private System.Windows.Forms.TextBox textBox_fileDir;
        private System.Windows.Forms.Button openFileBt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox H1textBox;
        private System.Windows.Forms.TextBox LtextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button inLBt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Rxlabel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label b_label;
        private System.Windows.Forms.Label a_label;
        private System.Windows.Forms.TextBox H2textBox;
        private System.Windows.Forms.Button inH1Bt;
        private System.Windows.Forms.Button inH2Bt;
        private System.Windows.Forms.Label noticelabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
    }
}

