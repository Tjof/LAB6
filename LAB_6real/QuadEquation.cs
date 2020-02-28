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
    public class QuadEquation : Equation
    {
        private readonly double a;

        public QuadEquation(double a)
        {
            this.a = a;
        }
        public override double Value(double x)
        {
            return Math.Sin(a*x)/x;
        }
    }

}
