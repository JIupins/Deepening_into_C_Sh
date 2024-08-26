using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_HW2
{
    internal class Bits : IBits
    {
        private const int BitsPerLong = sizeof(long) * 8;
        private long value;

        public bool GetBit(int index)
        {
            return (value >> index & 1L) == 1;
        }

        public void SetBit(int index, bool value)
        {
            if (value)
                this.value |= 1L << index;
            else
                this.value &= ~(1L << index);
        }

        public Bits(long value = 0)
        {
            this.value = value;
        }

        public static explicit operator long(Bits bits) => bits.value;
        public static implicit operator Bits(long value) => new Bits(value);

        public static explicit operator int(Bits bits) => (int)bits.value;
        public static implicit operator Bits(int value) => new Bits(value);

        public static explicit operator byte(Bits bits) => (byte)bits.value;
        public static implicit operator Bits(byte value) => new Bits(value);
    }
}