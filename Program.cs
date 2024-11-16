using System;


namespace BOA
{
    class Program
    {

       



        static void Main(string[] args)
        {

            int[] N = { 10, 20, 40, 80 };
            int[] I = { 5, 10, 20, 40, 60, 80};
            int[] D = { 2, 5, 10, 30 };

            double[] a_list = { 0.02, 0.05, 0.1, 0.15, 0.2, 0.25 };
            double[] c_list = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
            double[] p_list = { 0.4, 0.5, 0.6, 0.7, 0.8 };

          
            foreach (int n in N)
            {
                foreach (int i in I)
                {
                    foreach (int d in D)
                    {
                        double[][] range = new double[d][];
                        for (int t = 0; t < d;t++)
                        range[t] = new double[] { -5.12, 5.12 };

                        TestsFunctions.FindBest(myFuncion: TestsFunctions.Rastrigin, dim_in: d, iterations_in: i, range_in: range, population_size_in: n,
                                        a_list: a_list, c_list: c_list, p_list: p_list, "Rastrigin", 200);
                    }
                }
            }




            //Butterfly but = new Butterfly(myFuncion: TestsFunctions.Rastrigin, dim_in: 2, iterations_in: 50, range_in: range, population_size_in: 50, 
            //                            a_in: 0.1, c_in: 0.7, p_in: 0.8);
            //but.Solve();
            //Console.WriteLine(but.FBest);
            //foreach(double d in but.XBest)
            //{
            //    Console.WriteLine(d);
            //}
        }

       
    }
}
