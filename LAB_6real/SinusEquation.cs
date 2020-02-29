using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6real
{
    /// <summary>
    /// Класс, представляющий квадратное уравнение
    /// </summary>
    public class SinusEquation : Equation
    {
        private readonly double a;

        public SinusEquation(double a)
        {
            this.a = a;
        }

        public override double Value(double x)
        {
            return Math.Sin(a*x)/x;
        }
    }
}
