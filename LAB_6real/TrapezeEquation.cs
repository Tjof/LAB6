using System;

namespace LAB_6real
{
    public class TrapezeEquation : Equation
    {
        private readonly double a;

        public TrapezeEquation(double a)
        {
            this.a = a;
        }

        public override double Value(double x)
        {
            return x*Math.Cos(x) / a;
        }
    }
}
