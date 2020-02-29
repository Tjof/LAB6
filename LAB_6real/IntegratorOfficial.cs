using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6real
{
    public abstract class IntegratorOfficial
    {
        public event EventHandler<IntegratorEventArgs> OnStep;
        public event EventHandler<double> OnFinish;
        public abstract double Integrate(double x1, double x2, Equation equation, int N);

        protected void RaiseStepEvent(double x, double f, double sum)
        {
            if (OnStep != null)
            {
                IntegratorEventArgs args = new IntegratorEventArgs()
                {
                    X = x,
                    F = f,
                    Integr = sum
                };
                OnStep(this, args);
            }
        }

        protected void RaiseFinishEvent(double sum)
        {
            OnFinish?.Invoke(this, sum);
        }
    }

}
