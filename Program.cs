using System;


namespace BOA
{
    public class OptimizationResult
    {
        public double a { get; set; }
        public double c { get; set; }
        public double p { get; set; }
        public double MeanResult { get; set; }
        public double StdResult { get; set; }
        public double[] MeanPos { get; set; }
        public double[] StdPos { get; set; }
        public double[] BestPositions { get; set; } 

        // Constructor to initialize the arrays for best results and positions
        public OptimizationResult()
        {
            BestPositions = new double[2];
            MeanPos = new double[2];
            StdPos = new double[2];
        }
    }

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
                ret += Math.Pow(x, 2);
            }
            return ret;
        }

        static double Rastrigin(double[] xi)
        {
            double ret = 0;
            foreach (double x in xi)
            {
                ret += (Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x) + 10);
            }
            return ret;
        }

        static double Rosenbrock(double[] xi)
        {
            return Math.Pow((1.5 - xi[0] + xi[0] * xi[1]), 2) + Math.Pow((2.25 - xi[0] + xi[0] * Math.Pow(xi[1], 2)), 2) +
                Math.Pow((2.625 - xi[0] + xi[0] * Math.Pow(xi[1], 3)), 2);
        }

        static double Beale(double[] xi)
        {
            double ret = 0;
            for (int i = 0; i < xi.Length - 1; i++)
            {
                ret += (100 * Math.Pow(xi[i + 1] - Math.Pow(xi[i], 2), 2) + Math.Pow(xi[i] - 1, 2));
            }
            return ret;
        }

        static double BukinN6(double[] xi)
        {
            return 100 * Math.Sqrt(Math.Abs(xi[1] - 0.01 * xi[0] * xi[0])) + 0.01 * Math.Abs(xi[0] + 10);
        }

        static double HimmelblauN6(double[] xi)
        {
            return Math.Pow(xi[0] * xi[0] + xi[1] - 11, 2) + Math.Pow(xi[0] + xi[1] * xi[1] - 7, 2);
        }




        static void Main(string[] args)
        {

            int[] N = { 10, 20, 40, 80 };
            int[] I = { 5, 10, 20, 40, 60, 80};
            //foreach(int n in N)
            //{
            //    foreach (int i in I)
            //    {
            //        // Butterfly but = new Butterfly(Sphere, 30, i, -100, 100, n, 0.1, 0.7, 0.8);
            //        Butterfly but = new Butterfly(Rastrigin, 30, i, -5, 5, n, 0.1, 0.7, 0.8);
            //        but.Solve();
            //        Console.WriteLine("N: "+ n.ToString()+" I: "+i.ToString()+ " Value: "+but.FBest.ToString());
            //        foreach (double j in but.XBest)
            //        {
            //            Console.Write(j.ToString()+" ");
            //        }
            //        Console.WriteLine();
            //    }
            //}

            //int test_nb = 100;
            //double[] results = new double[test_nb];
            //double mean = 0;
            //for (int i =0; i < test_nb; i++)
            //{
            //    Butterfly but = new Butterfly(Rosenbrock, 2, 30, -10, 10, 20, 0.1, 0.7, 0.8);
            //    but.Solve();
            //    results[i] = but.FBest;
            //    mean += but.FBest;
            //}
            //mean /= test_nb;
            //double std = 0;
            //for (int i = 0; i < test_nb; i++)
            //{
            //    std += Math.Pow(results[i] - mean, 2);
            //}
            //std = Math.Sqrt(std / test_nb);
            //Console.WriteLine("Ratio change:" + (std/ mean * 100).ToString());


            double[] a_list = {0.02, 0.05, 0.1, 0.15, 0.2, 0.25};
            double[] c_list = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9};
            double[] p_list = { 0.4,0.5,0.6,0.7,0.8 };
            OptimizationResult opt = new OptimizationResult();
            opt.MeanResult = 1000000;
            foreach (double a in a_list)
            {
                foreach (double c in c_list)
                {
                    foreach (double p in p_list)
                    {
                        int test_nb = 100;
                        double[] results = new double[test_nb];
                        double[,] results_pos = new double[test_nb, 2];
                        double mean = 0;
                        double [] mean_pos = new double[2];
                        for (int i = 0; i < test_nb; i++)
                        {
                            Butterfly but = new Butterfly(Rosenbrock, 2, 50, -10, 10, 50, a, c, p);
                            but.Solve();
                            results[i] = but.FBest;
                            mean += but.FBest;
                            for (int j = 0; j < 2; j++)
                            {
                                results_pos[i, j] = but.XBest[j];
                                mean_pos[j] += but.XBest[j];
                            }
                        }
                        mean /= test_nb;
                        for (int j = 0; j < 2; j++)
                        {
                            mean_pos[j] /= test_nb;
                        }
                        double std = 0;
                        double[] std_pos = new double[2];
                        for (int i = 0; i < test_nb; i++)
                        {
                            std += Math.Pow(results[i] - mean, 2);
                            for (int j = 0; j < 2; j++)
                            {
                                std_pos[j] = Math.Pow(results_pos[i,j] - mean_pos[j], 2);
                            }
                        }
                        std = Math.Sqrt(std / test_nb);
                        for (int j = 0; j < 2; j++)
                        {
                            std_pos[j] = Math.Sqrt(std_pos[j] / test_nb);
                        }
                        if (mean < opt.MeanResult)
                        {
                            opt.a = a;
                            opt.c = c;
                            opt.p = p;
                            opt.MeanResult = mean;
                            opt.StdResult = std;
                            opt.MeanPos = mean_pos;
                            opt.StdPos = std_pos;
                        }

                    }
                }
                Console.WriteLine(a);
            }

            Console.WriteLine("a:" + opt.a.ToString());
            Console.WriteLine("c:" + opt.c.ToString());
            Console.WriteLine("p:" + opt.p.ToString());
            Console.WriteLine("mean: " + opt.MeanResult.ToString());
            Console.WriteLine("std: " + opt.StdResult.ToString());
            for(int j = 0; j < 2; j++)
            {
                Console.Write(" mean_pos: " + opt.MeanPos[j].ToString());
            }
            Console.WriteLine();
            for (int j = 0; j < 2; j++)
            {
                Console.Write(" mean_pos: " + opt.StdPos[j].ToString());
            }



        }
       
       
    }
}
