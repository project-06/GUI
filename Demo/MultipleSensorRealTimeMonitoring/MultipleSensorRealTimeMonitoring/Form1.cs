using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using rtChart;
using System.Diagnostics;
using System.Threading;
using System.IO;
//using NetMQ.Sockets;
//using NetMQ;
using System.Windows.Forms.DataVisualization.Charting;




namespace MultipleSensorRealTimeMonitoring
{
    public partial class Form1 : Form
    {

        //Robot robot = new Robot();
        //ResponseSocket server = null;
        int eventID = 0;
        float[] data;

        public Form1()
        {
            InitializeComponent();

        }


        PerformanceCounter Data = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

        
        private double randomzahlen()
        {
            Random zahlen = new Random();
            return zahlen.Next(0, 500);
        }

        //private void receiveData()
        //{
            //ActualCurrent actualCurrent = new ActualCurrent();
            //DataProcessor dataProcessor = new DataProcessor();

        //    while (true)
        //    {
        //        Receive data through socket and send answer
        //        string message = server.ReceiveFrameString();
        //        server.SendFrame("Roger");

        //        Adjust string and convert to float and create new eventobject
        //        data = dataProcessor.String2float(message);

        //        Look for changes, print message if there is a difference; initialze for next loop
        //        for (int i = 0; i < 6; i++)
        //            {
        //                float TOL = .5F;       //Tolerance
        //                if (!((data[i] > actualCurrent.amperetData[i] - TOL) && (data[i] < actualCurrent.amperetData[i] + TOL)))
        //                {
        //                    string logMessage = "Something is happening -> Event ID: " + eventID;
        //                    panel1.BackColor = Color.Lime;
        //                    eventID++;
        //                    updateLogFile(logMessage);
             

        //                    break;
        //                }
        //                else
        //                    panel1.BackColor = Color.DarkGray;
        //            }
        //        actualCurrent.amperetData = data;
        //    }
        //}

        //delegate void SetTextCallback(string text);

        private void Form1_Load(object sender, EventArgs e)
        {

           

            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 15;       //BİTİŞ YERİ(Daha doğrusu genişliği)
            stripLine1.BorderColor = System.Drawing.Color.Red;
            stripLine1.BorderWidth = 10;
            stripLine1.BorderDashStyle = ChartDashStyle.Solid;
            stripLine1.IntervalOffset = 40;   //bAŞLANGIÇ YERİ
            stripLine1.BackColor = System.Drawing.Color.WhiteSmoke;
            chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine1);


            kayChart SensorData1 = new kayChart(chart1, 60);
            SensorData1.serieName = "Series1";



            Task.Factory.StartNew(() =>
            {
                SensorData1.updateChart(randomzahlen, 60);

            });

        }

//        private void readLogFile(string a)
//        {
//            try
//            {
//                FileStream fileStream = new FileStream("C:/Users/teozk/OneDrive/Dokumente/Log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

//                StreamReader streamReader = new StreamReader(fileStream);

//                if (this.textBox1.InvokeRequired)
//                {
//                    SetTextCallback d = new SetTextCallback(readLogFile);
//                    this.Invoke(d, new object[] { streamReader.ReadToEnd() });
//                }
//                else
//                {
//                    this.textBox1.Text = streamReader.ReadToEnd();
//                }


//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }

//        public void updateLogFile(string logMessage, bool isEvent = true)
//        {
//            string Text;
//            if (isEvent == false)
//                Text = logMessage;
//            else
//                Text = "\r\nLog Entry : " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + " | " + DateTime.Now.ToLongDateString() + " -------------------- " + logMessage;

//            string Path = "C:/Users/teozk/OneDrive/Dokumente/Log.txt";

//            string content = File.ReadAllText(Path);
//            content = Text + "\n" + content;
//            File.WriteAllText(Path, content);
//            readLogFile(" ");
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            // Initialize socket communication with robot
//            updateLogFile("\r\nBinding socket... Please start the server.", false);
//            server = new ResponseSocket();
//            server.Bind("tcp://localhost:5555");

//            Thread receiveDataThread = new Thread(receiveData);
//            receiveDataThread.Start();
//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            updateLogFile("\r\nInitializing Robot...", false);
//            robot.initialize("169.254.156.200");
//            updateLogFile("\r\nInitializing Done.", false);
//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            updateLogFile("\r\nMoving Robot Sample Path..", false);
//            Thread robotMovementThread = new Thread(robot.moveSample);
//            robotMovementThread.Start();
//        }

//        private void button4_Click(object sender, EventArgs e)
//        {
//            // Plot Data !!!
//            kayChart sensor1 = new kayChart(chart1, 20);
//            kayChart sensor2 = new kayChart(chart2, 20);
//            kayChart sensor3 = new kayChart(chart3, 20);
//            kayChart sensor4 = new kayChart(chart4, 20);
//            kayChart sensor5 = new kayChart(chart5, 20);
//            kayChart sensor6 = new kayChart(chart6, 20);

//            sensor1.serieName = "Series1";
//            sensor2.serieName = "Series1";
//            sensor3.serieName = "Series1";
//            sensor4.serieName = "Series1";
//            sensor5.serieName = "Series1";
//            sensor6.serieName = "Series1";

//            Task.Factory.StartNew(() =>
//            {
//                sensor1.updateChart(updateSensor1, 400);
//            });

//            Task.Factory.StartNew(() =>
//            {
//                sensor2.updateChart(updateSensor2, 400);
//            });

//            Task.Factory.StartNew(() =>
//            {
//                sensor3.updateChart(updateSensor3, 400);
//            });

//            Task.Factory.StartNew(() =>
//            {
//                sensor4.updateChart(updateSensor4, 400);
//            });

//            Task.Factory.StartNew(() =>
//            {
//                sensor5.updateChart(updateSensor5, 400);
//            });

//            Task.Factory.StartNew(() =>
//            {
//                sensor6.updateChart(updateSensor6, 400);
//            });
//        }

//        private double updateSensor1()
//        {
//            return data[0];
//        }

//        private double updateSensor2()
//        {
//            return data[1];
//        }

//        private double updateSensor3()
//        {
//            return data[2];
//        }

//        private double updateSensor4()
//        {
//            return data[3];
//        }

//        private double updateSensor5()
//        {
//            return data[4];
//        }

//        private double updateSensor6()
//        {
//            return data[5];
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {

//        }
//    }


}
}
