using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rtChart;
using System.Diagnostics;
using System.Threading;
using System.IO;

//KODUN DÜZGÜN ÇALIŞABİLMESİ İÇİN ÇÖZÜMGEZGİNİ>BAŞVURULAR>SAĞCLICK>NUGETPAKETLERİNİYÖNET 
//YAPTIKTAN SONRA "kayChart" adlı nuggeti ekleyin. Program düzgün çalışacaktır.

namespace MultipleSensorRealTimeMonitoring
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
        }


       

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                openFile.Filter = "Textfile(*txt) |*.txt";
                openFile.ShowDialog();   
                StreamReader Reading = new StreamReader(openFile.FileName);
                richTextBox1.Text = Reading.ReadToEnd();
                Reading.Close();
            }
            catch(Exception hata)
            {
                MessageBox.Show(hata.Message);
            }

        }

        PerformanceCounter Data = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

        //RASTGELE VERILER BU FONKSIYON ILE SAĞLANIYOR.
        private double randomzahlen()
        {
            Random zahlen = new Random();
            return zahlen.Next(0, 1000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kayChart SensorData1 = new kayChart(chart1, 60);
            SensorData1.serieName = "Series1";



            Task.Factory.StartNew(() =>
            {
                SensorData1.updateChart(randomzahlen, 60);

            });

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText("Deger: " + randomzahlen() + "\n"); });
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
