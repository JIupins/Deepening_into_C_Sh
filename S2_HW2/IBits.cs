using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_HW2
{
    internal interface IBits
    {
        bool GetBit(int index);
        void SetBit(int index, bool value);
    }
}