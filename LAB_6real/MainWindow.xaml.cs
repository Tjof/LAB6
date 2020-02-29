using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace LAB_6real
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        private double _value;
        double[] _valueArr = null;
        private SeriesCollection _seriesCollection;
        private string[] _labels;
        Equation _equation;
        private IntegratorOfficial[] _integrators;

        public IntegratorOfficial[] Integrators
        {
            get => _integrators; set => _integrators = value;
        }
        
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }
        public Func<double, string> YFormatter { get; set; }

        public double[] Value
        {
            get
            {
                return _valueArr;
            }
            set
            {
                _valueArr = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            _integrators = new IntegratorOfficial[]
            {
                new IntegratorOdin(),
                new IntegratorDva()
            };
            foreach(var integrator in _integrators)
            {
                integrator.OnStep += WriteToFile;
                integrator.OnFinish += Integrator_OnFinish;
            }
            DataContext = this;
            ValueA.Focus();
        }

        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"file.txt", string.Empty);


            double a = double.Parse(ValueA.Text);
            var integrator = ComboBoxIntegrator.SelectedItem as IntegratorOfficial;
            if (integrator == null)
            {
                MessageBox.Show("ERROR");
                return;
            }
            var EquationName = ComboBoxEquation.Text;
            if (EquationName == "Sinus")
            {
                _equation = new SinusEquation(a);
            }
            else if (EquationName == "Cosinus")
            {
                _equation = new CosinusEquation(a);
            }

            double x1 = double.Parse(X1.Text);
            double x2 = double.Parse(X2.Text);

            var serCollection = new SeriesCollection();
            LineSeries series = new LineSeries();
            DrawFunction(x1, x2, series);
            serCollection.Add(series);
            SeriesCollection = serCollection;


            int N = Int32.Parse(ValueN.Text);
            IntegratorResult.Text = integrator.Integrate(x1, x2, _equation, N).ToString();
        }

        private void WriteToFile(object sender, IntegratorEventArgs e)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "file.txt");
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("x = " + e.X.ToString() + " f(x) = " +  e.F.ToString() + " Интеграл = " + e.Integr.ToString());
            }
        }

        private void Integrator_OnFinish(object sender, double e)
        {
            MessageBox.Show($"Интеграл: {e}");
            Process.Start("file.txt");
        }

        void DrawFunction(double x1, double x2, Series series)
        {
            int N = Int32.Parse(ValueN.Text);
            double[] valueArr = new double[N];
            double h = (x2 - x1) / N;
            double[] x = new double[N];
            for (int i = 0; i < N; i++)
            {
                _value = _equation.Value(x1 + i * h);
                x[i] = (x1 + i * h);
                valueArr[i] = _value;
            }
            Labels = x.Select(i => i.ToString()).ToArray();
            series.Values = new ChartValues<double>(valueArr);
        }
    }
}
