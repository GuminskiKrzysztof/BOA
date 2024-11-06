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

        public Butterfly(Func<double[], double> myFuncion,int dim_in,int itreations, int[,] range_in,int population_size_in)
        {
            Name = "Butterfly optimization algorithm";
            range = range_in;
            population_size = population_size_in;
            dim = dim_in;
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

        }
        private double CalculateNextPosition()
        {
            return 0.0;
        }

        public double Solve()
        {
            CreatePopulation();
  
            return 0.2;
        }
    }
}
