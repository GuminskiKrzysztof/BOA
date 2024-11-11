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
            for(int i = 0; i < 4; i++)
            {
                range[i, 0] = -10;
                range[i, 1] = 10;
            }
       
            Butterfly but = new Butterfly(f1,4,500,range,30,0.1,0.01, 0.8);
            but.Solve();
            
        }
       
       
    }
}
