using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6real
{
    public class IntegratorOdin : IntegratorOfficial
    {
        public override string ToString()
        {
            return "Метод прямоугольников";
        }
        /// <summary>
        /// Функция интегрирования
        /// </summary>
        /// <param name="x1">левая граница интегрирования</param>
        /// <param name="x2">правая граница интегрирования</param>
        public override double Integrate(double x1, double x2, Equation equation, int N)
        {
            if (equation == null)
            {
                throw new ArgumentNullException();
            }
            double h = (x2 - x1) / N;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += equation.Value(x1 + i * h) * h;
                RaiseStepEvent(x1 + i * h, equation.Value(x1 + i * h)*h, sum);
            }
            RaiseFinishEvent(sum);
            return sum;
        }
    }

}
