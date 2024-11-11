using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BOA
{
    class Butterfly : IOptimizationAlgorithm
    {
        public string Name { get; set; }
        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }

        private int[,] range;
        private double[,] population;
        private int population_size;
        private int dim;
        private int iterations;
        private double[] Ii;
        private double[] Fi;
        private double a;
        private double c;
        private double p;
        Func<double[], double> test_func;

        public Butterfly(Func<double[], double> myFuncion,int dim_in,int iterations_in, int[,] range_in,int population_size_in,double a_in, double c_in,double p_in)
        {   
            Name = "Butterfly optimization algorithm";
            range = range_in;
            population_size = population_size_in;
            dim = dim_in;
            a = a_in;
            c = c_in;
            p = p_in;
            iterations = iterations_in;
            test_func = myFuncion;
        }
        private void  CreatePopulation()
        {
            Random rand = new Random();
            population = new double[population_size,dim];
            for (int i = 0; i < population_size; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    population[i,j] = rand.NextDouble() * (range[j,1] - range[j, 0]) + range[j, 0];
                }
            }
        }

        private void FindBest()
        {
            FBest = Fi[0];
            double[] row = new double[population.GetLength(1)];
            for (int j = 0; j < population.GetLength(1); j++)
            {
                row[j] = population[0, j];
            }
            XBest = row;
            for (int i = 0; i < population_size; i++)
            {
                if (Fi[i] < FBest)
                {
                    FBest = Fi[i];
                    for (int j = 0; j < population.GetLength(1); j++)
                    {
                        row[j] = population[i, j];
                    }
                    XBest = row;
                }
                
            }
        }
        private void CalculateFragrance()
        {
            for (int i = 0; i < population_size; i++)
            {

                Fi[i] = c * Math.Pow(Ii[i],a);
            }
        }

        private void CalculateNextPosition()
        {
            Random rand = new Random();
            double r;
            int rand_butterfly;
            for ( int i = 0; i < population_size; i++)
            {
                r = rand.NextDouble();
                if (p > r)
                {
                    for( int j = 0; j < dim;j++)
                    {
                        population[i, j] = population[i, j] + (Math.Pow(r,2)* XBest[j] - population[i, j]) * Fi[i];
                    }
                }
                else 
                {
                    rand_butterfly = rand.Next(population_size);
                    for (int j = 0; j < dim; j++)
                    {
                        population[i, j] = population[i, j] + (Math.Pow(r, 2) * population[rand_butterfly, j] - population[i, j]) * Fi[i];
                    }
                }
            }
        }

        private void CalculateI()
        {
            for (int i = 0; i < population_size; i++)
            {
                double[] row = new double[population.GetLength(1)];
                for (int j = 0; j < population.GetLength(1); j++)
                {
                    row[j] = population[i, j];
                }
                Ii[i] = test_func(row);
            }
        }

        public double Solve()
        {
            CreatePopulation();
            Ii = new double[population_size];
            Fi = new double[population_size];
            


            for (int i = 0;i < iterations; i ++)
            {
                CalculateI();
                CalculateFragrance();
                FindBest();
                CalculateNextPosition();
                Console.WriteLine(FBest);
                Console.WriteLine(string.Join(", ", XBest));
                Console.WriteLine();
                a = 0.1 + (i / (double)iterations) * (0.3 - 0.1);

            }

            return 0.2;
        }
    }
}
