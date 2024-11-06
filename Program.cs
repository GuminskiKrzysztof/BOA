using System;


namespace BOA
{
    class Program
    {

        static double f1(double[] xi)
        {
            double ret = 0;
            foreach (double x in xi)
            {
                ret += Math.Abs(x);
            }
            double ret1 = 1;
            foreach (double x in xi)
            {
                ret1 *= Math.Abs(x);
            }
            ret += ret1;
            return ret;
        }
        static void Main(string[] args)
        {
            int[,]  range = new  int[4,2];
       
            Butterfly but = new Butterfly(f1,4,100,range,20);
            Console.WriteLine("Hello World!");
        }
       
       
    }
}
