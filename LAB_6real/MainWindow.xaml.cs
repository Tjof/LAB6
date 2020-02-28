using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(ValueA.Text);
            Equation equation = new QuadEquation(a);    //создаем объект класса "кв. уравнение"
            //Integrator i1 = new Integrator(equation); //создаем интегратор для этого уравнения
            //double integrValue = i1.Integrate(10, 20); //вызываем интегрирование на интвервале 10;30
            //проверяем допустимость параметров:
            double x1 = 10, x2 = 30;
            if (x1 >= x2)
            {
                MessageBox.Show("Error: x1 >= x2");
            }
            /* для интегирования разобъем исходный отрезок на 100 точек. 
             * Считаем значение функции в точке, умножаем на ширину интервала.
             * Площадь полученного прямоугольника приблизительно равна значению интеграла на этом отрезке
             * суммируем значения площадей, получаем значение интеграла на отрезке [X1;X2]*/
            int N = 100;    //количество интервалов разбиения
            //определяем ширину интервала:
            double h = (x2 - x1) / N;
            double sum = 0; //"накопитель" для значения интеграла
            for (int i = 0; i < N; i++)
            {
                sum = sum + equation.Value(x1 + i * h) * h;
                _value = sum;
            }
        }
    }
}
