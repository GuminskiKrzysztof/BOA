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

        static double Sphere(double[] xi)
        {
            double ret = 0;
            foreach (double x in xi)
            {
                ret += Math.Pow(x,2);
            }
            return ret;
        }

        static double Rastrigin(double[] xi)
        {
            double ret = 0;
            foreach (double x in xi)
            {
                ret += (Math.Pow(x, 2)-10*Math.Cos(2*Math.PI*x)+10);
            }
            return ret;
        }

        static double Rosenbrock(double[] xi)
        {
            double ret = 0;
            for( int i = 0; i < xi.Length-1; i++)
            {
                ret += (100*Math.Pow(xi[i+1]- Math.Pow(xi[i],2),2)+ Math.Pow(xi[i]-1,2));
            }
            return ret;
        }

        static void Main(string[] args)
        {
           
            int[] N = { 10, 20, 40, 80 };
            int[] I = { 5, 10, 20, 40, 60, 80};
            foreach(int n in N)
            {
                foreach (int i in I)
                {
                    Butterfly but = new Butterfly(Rosenbrock, n, i, -10,10, 30, 0.1, 0.01, 0.8);
                    but.Solve();
                    Console.WriteLine("N: "+ n.ToString()+" I: "+i.ToString()+ " Value: "+but.FBest.ToString());
                }
            }

           
            
            
        }
       
       
    }
}
