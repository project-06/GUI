using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Data.OleDb;
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using LiveCharts;
using LiveCharts.Configurations;

//https://www.youtube.com/watch?v=m-5g3iwrS9o
//Referans aldığım youtube linki yukarıdakidir.
//Henüz yeni bir gelişme yok fakat elimdekini yüklüyorum.
//satır 108 ve 118 daki trend değeri grafiğin şeklini belirliyor. Yani oynamamız gereken yer orası benim anladığım.
//satır 147 den itibaren excelden veri çekmek için kodu yazdım fakat try bölümü çalışmıyor. Hatayı bulmaya çalışıyorum.

namespace Wpf.CartesianChart.ConstantChanges
{
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

    public partial class ConstantChangesChart : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _trend;
        private double sutun;

        public ConstantChangesChart()
        {
            
                InitializeComponent();
            

            

            //To handle live data easily, in this case we built a specialized type
            //the MeasureModel class, it only contains 2 properties
            //DateTime and Value
            //We need to configure LiveCharts to handle MeasureModel class
            //The next code configures MeasureModel  globally, this means
            //that LiveCharts learns to plot MeasureModel and will use this config every time
            //a IChartValues instance uses this type.
            //this code ideally should only run once
            //you can configure series in many ways, learn more at 
            //http://lvcharts.net/App/examples/v1/wpf/Types%20and%20Configuration

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            ChartValues = new ChartValues<MeasureModel>();

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            //AxisStep forces the distance between each separator in the X axis
            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);

            //The next code simulates data changes every 300 ms

            IsReading = false;

            DataContext = this;
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            var r = new Random();
            

            while (IsReading)
            {

                Thread.Sleep(150);
                var now = DateTime.Now;

                _trend += r.Next(-8, 10);
                //_trend = sutun;

                ChartValues.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend
                });

                SetAxisLimits(now);

                //lets only use the last 150 values
                if (ChartValues.Count > 150) ChartValues.RemoveAt(0);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
        }

        private void InjectStopOnClick(object sender, RoutedEventArgs e)
        {
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);
        }

           /* string adres = @"C:\Users\Asus\Desktop\data.xlsx";

            OleDbConnection xlxsbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + adres + ";Extended Properties = 'Excel 12.0 Xml;HDR=YES'");



            try
            {
                xlxsbaglanti.Open();

                OleDbCommand komut = new OleDbCommand("SELECT * FROM [data.txt$]", xlxsbaglanti);
                OleDbDataReader oku = komut.ExecuteReader();


                while (oku.Read())
                {

                    string sutun = oku["-3,01E-07"].ToString();


                }

                xlxsbaglanti.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata");
            }

        }*/

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        
    }
}
