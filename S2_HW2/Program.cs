namespace S2_HW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bits bits = new Bits(3);
            bits.SetBit(1, false);

            long num = (long)bits;
            Console.WriteLine(num);
            Bits bits2 = num;
        }
    }
}