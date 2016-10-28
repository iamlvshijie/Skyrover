using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Collections;

namespace PSS
{
    
    public partial class FormMain : Form
    {
        //定义成员变量
        private SerialPort comm = new SerialPort();  
        private long received_count = 0;//接收计数  
        private long send_count = 0;//发送计数  
        private int FrontDeviceID1;
        private int FrontDeviceID2;
        private int RearDeviceID;
        private double PSS_L;
        private double PSS_H1;
        private double PSS_H2;

        private double SinBeta1;
        private double CosBeta1;

        private double SinBeta2;
        private double CosBeta2;

        private double[] PSS_W = new double[3];
        private double PSS_avW;
        private const double CONST_G = 9.780327;
        private double PSS_G;
        private double PSS_PH1;
        private double PSS_PH2;
        private double PSS_a;
        private double PSS_b;
        private uint[] devDataArray = new uint[6];
        private byte[] devIDArray = new byte[6]{0,0,0,0,0,0};

        private int operation_state=0;

        //Server从bin文件名中截取到的参数Product_ID
        byte[] byte2_Server_Product_ID = new byte[2];
        //Server从bin文件名中截取到的参数 硬件版本Hardware_Rev
        byte byte1_Server_Hardware_Rev;
        //Server从bin文件名中截取到的参数 Firmwire_Rev 
        byte[] byte2_Server_Firmwire_Rev = new byte[2];

        //从 固件更新请求包 中 截取出 设备型号Product_ID
        byte[] byte2_Client_Product_ID = new byte[2] { 0x00, 0x01 };
        //从 固件更新请求包 中 截取出 设备的硬件版本Hardware_Rev,和Constant中的Server Hardware_Rev进行比较
        byte byte1_Client_Hardware_Rev;
        //从 固件更新请求包 中 截取出 设备的当前固件版本Firmwire_Rev 
        byte[] byte2_Client_Firmwire_Rev = new byte[2];
        //待发送的 固件更新初始包 
        byte[] byte14_Transmit_Preamble = new byte[14];

        //用于导入文件的全局变量
        string fileNameDir = null;
        string fileName = null;
        FileStream inputFileStream = null;
        //存放文件的byte数组
        byte[] byteFileData = null;
        //当前文件的分片个数，对应了IWB协议中的“固件更新初始包”的“Total_Num”参数
        //计算方式sliceNumber= byteData.length/SLICE_SIZE 再上取整
        int sliceNumber = 0;
        //固件更新初始包 中的Pad_Num ，最后一个固件更新数据包中的 有效字节数
        int preamble_Pad_Num = 0;
        //分片记数，用于count当前收到的是第几个 数据回应包，即确定了 前sliceReceiveCount个数据包已经被接收
        //再根据分片来计算出下一个 待发送的数据包分片的位置
        int sliceReceiveCount = 0;
        //固件更新 数据包的整体长度，用于构建comm串口的发送数据
       // byte[] byteTransmitData = new byte[Constants.FW_DATA_PKT_TRANSMIT_TOTAL_LEN];
        //是否为第一次发送
        bool isfirstTransmit = true;
        //是否正在传送app.bin数据包
        bool isTransmitting = false;

        public FormMain()
        {
            InitializeComponent();
        }

        //窗体初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //默认波特率选择 115200
            baudRate.SelectedIndex = baudRate.Items.IndexOf("9600");
            parity.SelectedIndex = parity.Items.IndexOf("None");
            dataBits.SelectedIndex = dataBits.Items.IndexOf("8");
            stopBits.SelectedIndex = stopBits.Items.IndexOf("One");

            //初始化SerialPort对象  
            comm.NewLine = "/r/n";
            //在串行通信中是否启用请求发送 (RTS) 信号？
            //comm.RtsEnable = true;
            comm.ReceivedBytesThreshold = 1;
            //添加事件注册  
            comm.DataReceived += comm_DataReceived;
        }
        
        //
        public void getArgsFromBinFileName()
        {
            string fileNameExceptPostfix = fileName.Replace(".jpg","");
            //string[] strArray = fileNameExceptPostfix.Split('_');
            //byte2_Server_Product_ID[0] = (byte)(fileNameExceptPostfix[0]) - '0';


        }

        //portNumber点击事件，每次点击，都重新初始化下拉串口名称列表框
        private void portNumber_Click(object sender, EventArgs e)
        {
            //初始化下拉串口名称列表框  
            string[] ports = SerialPort.GetPortNames();
            ports.Distinct();
            Array.Sort(ports);
            portNumber.Items.Clear();
            portNumber.Items.AddRange(ports);
            /*
            for (int ii = 0; ii < ports.Length;ii++ )
            {
                textBox_msgInfo.Text += "可选串口" + ports[ii] + "" + Environment.NewLine;
            }*/

            
            //默认选择下拉列表中的第一个串口 
            portNumber.SelectedIndex = 0;
        }

        /**
         * 响应事件处理
         * */
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {     
                int n = comm.BytesToRead;//收到的数据包长度  
                Rxlabel.Text = received_count.ToString();

                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据  
                received_count += 1;//增加接收计数  
                comm.Read(buf, 0, n);//读取缓冲数据 
            
                byte packetLength = buf[0];
                byte tempDevID = buf[1];
                uint tempDevData = (uint)buf[2] + (uint)(buf[3]<<8) + (uint)(buf[4]<<16) + (uint)(buf[5]<<24);
                devDataArray[tempDevID] = tempDevData;

                switch (operation_state)
                { 
                    case 3:
                        if (tempDevID == FrontDeviceID1)
                        {
                            if(devIDArray[tempDevID]==0)                           
                                this.noticelabel.Text += "双轮（左）设备找到,";
                        }
                        else if (tempDevID == FrontDeviceID2)
                        {
                            if(devIDArray[tempDevID] == 0)
                                noticelabel.Text += "双轮（右）设备找到,";
                        }
                        else if (tempDevID == RearDeviceID) 
                        {
                            if(devIDArray[tempDevID] == 0)
                                noticelabel.Text += "单轮设备找到,";
                        }
                        else
                            noticelabel.Text += "存在多余设备,";
                        
                        devIDArray[tempDevID] = 1;

                        if(inLBt.Enabled==false)
                        {
                            if(devIDArray[FrontDeviceID1]==1&&devIDArray[FrontDeviceID2]==1&&devIDArray[RearDeviceID]==1)
                            {
                                noticelabel.Text += "搜索成功，将模型尽量水平放在若干个重力采集设备上，并输入L，然后确认！";
                                inLBt.Enabled = true;
                                operation_state = 4;
                            }
                        }
                        break;
                    case 5:
                        //平稳状态，只读取重量
                        devIDArray[tempDevID] = 1;
                        if (devIDArray[FrontDeviceID1] == 1 && devIDArray[FrontDeviceID2] == 1 && devIDArray[RearDeviceID] == 1)
                        {
                            PSS_W[0] = devDataArray[FrontDeviceID1] + devDataArray[FrontDeviceID2] + devDataArray[RearDeviceID];
                            if (inH1Bt.Enabled == false)
                            {
                                inH1Bt.Enabled = true;
                                noticelabel.Text = "获取初始重量成功，" + devDataArray[FrontDeviceID1].ToString() + ", " + devDataArray[FrontDeviceID2].ToString() + ", " + devDataArray[RearDeviceID].ToString() + ", " + "请倾斜模型，并输入第一次倾斜时的H1，然后确认！";
                                operation_state = 6;
                            }
                            devIDArray[FrontDeviceID1] = 0;
                            devIDArray[FrontDeviceID2] = 0;
                            devIDArray[RearDeviceID] = 0;
                        }
                        break;
                    case 7:
                        devIDArray[tempDevID] = 1;
                        if (devIDArray[FrontDeviceID1] == 1 && devIDArray[FrontDeviceID2] == 1 && devIDArray[RearDeviceID] == 1)
                        {
                            SinBeta1 = PSS_H1 / PSS_L;
                            CosBeta1 = Math.Sqrt(PSS_L * PSS_L - PSS_H1 * PSS_H1) / PSS_L;
                            PSS_W[1] = devDataArray[FrontDeviceID1] + devDataArray[FrontDeviceID2] + devDataArray[RearDeviceID];
                            PSS_PH1 = devDataArray[RearDeviceID] * CONST_G / 1000;
                            if (inH2Bt.Enabled == false)
                            {
                                inH2Bt.Enabled = true;
                                //noticelabel.Text = "获取第一次倾斜时重量成功，PH1:" + PSS_PH1.ToString("0.0000") + ", sinbeta1,cosbeta1:" + SinBeta1.ToString("0.0000") + ", " + CosBeta1.ToString("0.0000") + ", data: " + devDataArray[FrontDeviceID1].ToString() + ", " + devDataArray[FrontDeviceID2].ToString() + ", " + devDataArray[RearDeviceID].ToString() + ", " + "请加大幅度倾斜模型，输入第二次倾斜时的H2，然后确认！";
                                noticelabel.Text = "获取第一次倾斜时重量成功，"+ ", 重量依次为: " + devDataArray[FrontDeviceID1].ToString() + ", " + devDataArray[FrontDeviceID2].ToString() + ", " + devDataArray[RearDeviceID].ToString() + ", " + "请加大幅度倾斜模型，输入第二次倾斜时的H2，然后确认！";
                                operation_state = 8;
                            }
                            devIDArray[FrontDeviceID1] = 0;
                            devIDArray[FrontDeviceID2] = 0;
                            devIDArray[RearDeviceID] = 0;
                        }
                        break;
                    case 9:
                         devIDArray[tempDevID] = 1;
                         if (devIDArray[FrontDeviceID1] == 1 && devIDArray[FrontDeviceID2] == 1 && devIDArray[RearDeviceID] == 1)
                         {
                             SinBeta2 = PSS_H2 / PSS_L;
                             CosBeta2 = Math.Sqrt(PSS_L * PSS_L - PSS_H2 * PSS_H2) / PSS_L;
                             PSS_W[2] = devDataArray[FrontDeviceID1] + devDataArray[FrontDeviceID2] + devDataArray[RearDeviceID];
                             
                             PSS_avW = (PSS_W[0] + PSS_W[1] + PSS_W[2]) / 3;
                             PSS_G = PSS_avW * CONST_G / 1000;

                             PSS_PH2 = devDataArray[RearDeviceID] * CONST_G / 1000;
                             //noticelabel.Text = "第三次获取重量成功，PH2:" + PSS_PH2.ToString("0.0000") + ", sinbeta2 cosbeta2: " + SinBeta2.ToString("0.0000") + ", " + CosBeta2.ToString("0.0000") + ", data:" + devDataArray[FrontDeviceID1].ToString() + ", " + devDataArray[FrontDeviceID2].ToString() + ", " + devDataArray[RearDeviceID].ToString() + ", " + "";
                             noticelabel.Text = "第三次获取重量成功，"  + ", 重量依次为:" + devDataArray[FrontDeviceID1].ToString() + ", " + devDataArray[FrontDeviceID2].ToString() + ", " + devDataArray[RearDeviceID].ToString() + ", " + "";
                             PSS_a = PSS_L * (PSS_PH1 * CosBeta1 * SinBeta2 - PSS_PH2 * CosBeta2 * SinBeta1) / (PSS_G * (SinBeta2 * CosBeta1 - SinBeta1 * CosBeta2));
                             PSS_b = PSS_L * CosBeta1 * CosBeta2 * (PSS_PH1 - PSS_PH2) / (PSS_G * (SinBeta2 * CosBeta1 - SinBeta1 * CosBeta2));

                             a_label.Text = "a = " + PSS_a.ToString("0.000");
                             b_label.Text = "b = " + PSS_b.ToString("0.000");
                             devIDArray[FrontDeviceID1] = 0;
                             devIDArray[FrontDeviceID2] = 0;
                             devIDArray[RearDeviceID] = 0;
                         }
                        break;
                    default:
                        break;                                        
                }
            }
            catch(Exception)
            {
                //Console.WriteLine("comm_DataReceived Exception");
                noticelabel.Text = "通信出错！";               
            }
            finally
            {
                
            }
        }        

        //响应“打开串口”按钮的触发事件
        private void openPortBt_Click(object sender, EventArgs e)
        {
            //在打开串口之前需要先检查 是否已经导入所要传送的文件
            if (openPortBt.Text == "打开串口")
            {
                //检查文件是否准备好
                if (operation_state < 1)
                {
                    //尚未导入文件
                    MessageBox.Show("请先打开待测量机器的图纸");
                    return;
                }
                else
                {
                   
                }
                //打开串口
                try
                {
                    //设置串口号
                    comm.PortName = portNumber.SelectedItem.ToString();
                    //波特率
                    comm.BaudRate = Convert.ToInt32(baudRate.SelectedItem.ToString());
                    //校验位——获取或设置奇偶校验检查协议
                    switch (parity.SelectedIndex)
                    {
                        case 0:
                            comm.Parity = Parity.None;
                            break;
                        case 1:
                            comm.Parity = Parity.Odd;
                            break;
                        case 2:
                            comm.Parity = Parity.Even;
                            break;
                        case 3:
                            comm.Parity = Parity.Mark;
                            break;
                        case 4:
                            comm.Parity = Parity.Space;
                            break;
                    }
                    //数据位——
                    comm.DataBits = Convert.ToInt32(dataBits.SelectedItem.ToString());
                    //停止位
                    switch (stopBits.SelectedIndex)
                    {
                        case 0:
                            comm.StopBits = StopBits.One;
                            break;
                        case 1:
                            comm.StopBits = StopBits.Two;
                            break;
                        case 2:
                            comm.StopBits = StopBits.OnePointFive;
                            break;
                    }
                    //串口设置完成，此时可以打开串口了
                    comm.Open();

                    //串口打开，更改指示灯状态为 绿色
                    //openPortBt.BackColor = Color.LawnGreen;
                    //串口配置中的下拉选框 状态置为 不可选
                    portNumber.Enabled = false;
                    baudRate.Enabled = false;
                    parity.Enabled = false;
                    dataBits.Enabled = false;
                    stopBits.Enabled = false;

                    noticelabel.Text = "串口" + portNumber.SelectedItem.ToString() + "已经打开！请打开并选择对应的设备！";
                    openPortBt.Text = "关闭串口";
                    operation_state = 2;

                    Front1comboBox.Enabled = true;
                    Front2comboBox.Enabled = true;
                    RearcomboBox.Enabled = true;
                    devComfirmBt.Enabled = true;

                }
                catch(Exception)
                {
                    MessageBox.Show("串口打开失败");
                }
            }
            else //当前串口已经是处于“打开状态”,再次点击按钮 将关闭串口
            {
                //关闭串口
                comm.Close();
                //串口状态指示灯颜色改为 红色
                //openPortBt.BackColor = Color.Red;
                //使能 串口配置 中的下拉框
                portNumber.Enabled = true;
                baudRate.Enabled = true;
                parity.Enabled = true;
                dataBits.Enabled = true;
                stopBits.Enabled = true;

                devComfirmBt.Enabled = false;
                inLBt.Enabled = false;
                inH1Bt.Enabled = false;
                inH2Bt.Enabled = false;


                noticelabel.Text = "串口" + portNumber.SelectedItem.ToString() + "已经关闭！请重新打开串口！";
                openPortBt.Text = "打开串口";
                operation_state = 1;
            }
        }

        //导入文件 按钮 事件处理
        private void openFileBt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "jpg文件|*.jpg";
            openFile.RestoreDirectory = true;

            //openFile.ShowDialog(); 加上这行代码反而 会让 对话框弹出两次 =，=b
            //将文件路径赋值给 textBox_fileDir 以显示在面板上
            if(openFile.ShowDialog()==DialogResult.OK)
            {
                ////将对话框得到的文件路径赋值给 全局变量名 fileNameDir
                fileNameDir = openFile.FileName;
                //将对话框得到的文件名赋值给 全局变量名 fileName
                fileName = openFile.SafeFileName;
                textBox_fileDir.Text = openFile.FileName;
            }

            if (fileNameDir != null)
            {
                Bitmap bmPic = new Bitmap(fileNameDir);
                Point ptLoction = new Point(bmPic.Size);
                if (ptLoction.X > pictureBox1.Size.Width || ptLoction.Y > pictureBox1.Size.Height)
                {

                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                   
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                pictureBox1.LoadAsync(fileNameDir);

                operation_state = 1;
                portNumber.Enabled = true;
                baudRate.Enabled = true;
                parity.Enabled = true;
                dataBits.Enabled = true;
                stopBits.Enabled = true;
                openPortBt.Enabled = true;
            }         
            noticelabel.Text = "读取文件成功！请打开PSS Doggle对应的串口！如不确认串口号，请打开Windows的设备管理器查找设备！";
        }

        //将收到的byte数据转换成16进制的字符串
        public string byteArray2String0x(byte[] buf)
        {            
            string receivedData = "";
            for (int i = 0; i < buf.Length; i++)
            {
                if (i == buf.Length - 1)
                {
                    receivedData += "0x" + Convert.ToString(buf[i], 16);
                    break;
                }
                receivedData += "0x" + Convert.ToString(buf[i], 16) + "-";
            }
            return receivedData;
        }

        public string byteArray2String0x(byte[] buf,int begin,int length)
        {

            string receivedData = "";
            for (int i = begin; i < length; i++)
            {
                if (i == length - 1)
                {
                    receivedData += "0x" + Convert.ToString(buf[i], 16);
                    break;
                }
                receivedData += "0x" + Convert.ToString(buf[i], 16) + "-";
            }
            return receivedData;
        }

        //选择前后轮的设备
        private void devComfirmBt_Click(object sender, EventArgs e)
        {
            if (operation_state < 2)
                MessageBox.Show("请先配置串口");
            else
            {
                if (devComfirmBt.Text == "确认")
                {
                    FrontDeviceID1 = Front1comboBox.SelectedIndex;
                    FrontDeviceID2 = Front2comboBox.SelectedIndex;
                    RearDeviceID = RearcomboBox.SelectedIndex;
                    if ((FrontDeviceID2 - FrontDeviceID1) * (FrontDeviceID1 - RearDeviceID) * (FrontDeviceID2 - RearDeviceID) == 0)
                        MessageBox.Show("设备未选择或者选择重复，请重新选择！");
                    else
                    {
                        operation_state = 3;
                        devComfirmBt.Text = "取消";
                        Front1comboBox.Enabled = false;
                        Front2comboBox.Enabled = false;
                        RearcomboBox.Enabled = false;
                        noticelabel.Text = "搜索结果：";
                    }
                }
                else
                {
                    Front1comboBox.Enabled = true;
                    Front2comboBox.Enabled = true;
                    RearcomboBox.Enabled = true;
                    Array.Clear(devIDArray, 0, devIDArray.Length);
                    inLBt.Enabled = false;
                    inH1Bt.Enabled = false;
                    inH2Bt.Enabled = false;
                    devComfirmBt.Text = "确认";
                    operation_state = 2;
                }
            }
        }

        private void inLBt_Click(object sender, EventArgs e)
        {
            if (operation_state <4)
                MessageBox.Show("未选择轮子对应的设备或者选择失败！");
            else
            {
                if (inLBt.Text == "确认")
                {
                    try
                    {                  
                        PSS_L = Convert.ToDouble(LtextBox.Text.ToString());
                        inLBt.Text = "修改";
                        operation_state = 5;
                        devIDArray[FrontDeviceID1] = 0;
                        devIDArray[FrontDeviceID2] = 0;
                        devIDArray[RearDeviceID] = 0;
                        //inH1Bt.Enabled = true;
                        //noticelabel.Text = "L输入值为"+PSS_L.ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("未输入或者输入格式不正确！");
                    } 
                }
                else
                {
                    inLBt.Text = "确认";
                    inH1Bt.Text = "确认";
                    inH2Bt.Text = "确认";
                    a_label.Text = "a = ?";
                    b_label.Text = "b = ?";
                    noticelabel.Text = "请重新输入L,H1和H2！";
                    operation_state = 4;              
                    inLBt.Enabled = true;
                }   
            }
        }

        private void inH1Bt_Click(object sender, EventArgs e)
        {
            if (inH1Bt.Text == "确认")
            {
                try{
                    PSS_H1 = Convert.ToDouble(H1textBox.Text.ToString());         
                    inH1Bt.Text = "修改";
               
                    devIDArray[FrontDeviceID1] = 0;
                    devIDArray[FrontDeviceID2] = 0;
                    devIDArray[RearDeviceID] = 0; 
                    operation_state = 7;
                   // inH2Bt.Enabled = true;
                    //noticelabel.Text += ",H1输入值为" + PSS_H1.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("未输入或者输入格式不正确！");
                } 
            }
            else
            {
                inH1Bt.Text = "确认";
                inH2Bt.Text = "确认";
                //noticelabel.Text = "L输入值为" + PSS_L.ToString();
                a_label.Text = "a = ?";
                b_label.Text = "b = ?";
                noticelabel.Text = "请重新输入H1和H2！";
                operation_state = 6;
            }
        }

        private void inH2Bt_Click(object sender, EventArgs e)
        {
            if (inH2Bt.Text == "确认")
            {
                try{
                    PSS_H2 = Convert.ToDouble(H2textBox.Text.ToString());
                    inH2Bt.Text = "修改";
                
                    noticelabel.Text = "L输入值为" + PSS_L.ToString();
                    noticelabel.Text += ",H1输入值为" + PSS_H1.ToString();
                    noticelabel.Text += ",H2输入值为" + PSS_H2.ToString();
                    noticelabel.Text += ",实时计算重心中...";
                    devIDArray[FrontDeviceID1] = 0;
                    devIDArray[FrontDeviceID2] = 0;
                    devIDArray[RearDeviceID] = 0;
                    operation_state = 9;
                }
                catch (Exception)
                {
                    MessageBox.Show("未输入或者输入格式不正确！");
                } 
            }
            else
            {
                inH2Bt.Text = "确认";
                //noticelabel.Text = "L输入值为" + PSS_L.ToString();
                //noticelabel.Text += ",H1输入值为" + PSS_H1.ToString();
                a_label.Text = "a = ?";
                b_label.Text = "b = ?";
                noticelabel.Text = "请重新输入H2！";
                operation_state = 8;
            }
        }        
    }
}
