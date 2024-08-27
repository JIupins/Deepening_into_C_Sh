using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_HW6
{
    public class OperandChangedEventArgs : EventArgs
    {
        public OperandChangedEventArgs(double operand)
        {
            Operand = operand;
        }

        public double Operand { get; }
    }
}
