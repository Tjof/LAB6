using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private IntegratorOfficial[] _integrators = new IntegratorOfficial[]
        {
            new IntegratorOdin(), 
            new IntegratorDva()
        };

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
            DataContext = this;
            ValueA.Focus();
        }


        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(ValueA.Text);

            var integrator = ComboBoxIntegrator.SelectedItem as IntegratorOfficial;
            if (integrator == null)
            {
                MessageBox.Show("ERROR");
                return;
            }

            var EquationName = ComboBoxEquation.Text;
            if (EquationName == "sinus")
            {
                _equation = new SinusEquation(a);
            }
            else if (EquationName == "trapeze")
            {
                _equation = new TrapezeEquation(a);
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
