using System;

namespace LAB_6real
{
    public class CosinusEquation : Equation
    {
        private readonly double a;

        public CosinusEquation(double a)
        {
            this.a = a;
        }

        public override double Value(double x)
        {
            return x*Math.Cos(x) / a;
        }
    }
}
