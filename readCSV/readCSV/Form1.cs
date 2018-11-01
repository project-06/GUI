using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace readCSV
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            
            using (var reader = new StreamReader(@"C:\Users\Asus\Desktop\data.csv"))
            {
                List<string> stream = new List<string>();

                //while (!reader.EndOfStream)
                //{
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    
                    Console.Out.WriteLine(values[0]);
                   
                //}

                this.chart1.Series["Series1"].Points.AddY(values[0]);
            }
        }
    }
}