using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6real
{
    public abstract class IntegratorOfficial
    {
        public abstract double Integrate(double x1, double x2, Equation equation, int N);
    }
}
