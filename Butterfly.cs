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

        private double[][] range;
        private double[][] population;
        private int populationSize;
        private int dimentions;
        private int iterations;
        private double[] Ii;
        private double[] Fi;
        private double a;
        private double initial_a;
        private double c;
        private double p;
        private Random rand;
        Func<double[], double> testFunction;

        public Butterfly(Func<double[], double> myFuncion,int dim_in,int iterations_in, double[][] range_in ,int population_size_in,double a_in, double c_in,double p_in)
        {   
            Name = "BOA";
            populationSize = population_size_in;
            dimentions = dim_in;
            a = a_in;
            initial_a = a;
            c = c_in;
            p = p_in;
            iterations = iterations_in;
            testFunction = myFuncion;
            range = range_in;
            rand = new Random();
        }
        private void  CreatePopulation()
        {
            population = new double[populationSize][];
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new double[dimentions];
            }

            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < dimentions; j++)
                {
                    population[i][j] = rand.NextDouble() * (range[j][1] - range[j][0]) + range[j][0];
                }
            }
        }

        private void FindBest()
        {
            FBest = Fi[0];
            XBest = population[0];
            for (int i = 0; i < populationSize; i++)
            {
                if (Fi[i] < FBest)
                {
                    FBest = Fi[i];
                    XBest = population[i];
                }
            }
        }

        private void CalculateNextPosition()
        {
            double randomDecision;
            double[] newPopulation = new double[dimentions];
            int rand_butterfly;
            for ( int i = 0; i < populationSize; i++)
            {
            
                randomDecision = rand.NextDouble();
                if (p > randomDecision)
                {
                    for( int j = 0; j < dimentions; j++)
                    {
                        newPopulation[j] = population[i][j] + (Math.Pow(randomDecision,2)* XBest[j] - population[i][j]) * Fi[i];
                    }
                }
                else 
                {
                    rand_butterfly = rand.Next(populationSize);
                    for (int j = 0; j < dimentions; j++)
                    {
                        newPopulation[j] = population[i][j] + (Math.Pow(randomDecision, 2) * population[rand_butterfly][j] - population[i][j]) * Fi[i];
                    }
                }
                if(Ii[i] > testFunction(newPopulation))
                {
                    population[i] = newPopulation;
                }
            }
        }

        public double Solve()
        {
            CreatePopulation();

            Ii = new double[populationSize];
            Fi = new double[populationSize];
            
            for (int i = 0;i < iterations; i ++)
            {
                /// Calcualte stimulus intensity
                for (int j = 0; j < populationSize; j++)
                {
                    Ii[j] = testFunction(population[j]);
                }
                ///Calcuate perceived magnitude of the fragrance
                for (int j = 0; j < populationSize; j++)
                {
                    Fi[j] = c * Math.Pow(Ii[j], a);
                }

                FindBest();
                CalculateNextPosition();

                a = initial_a + Math.Pow((i / (double)iterations), 2) * initial_a * 2;
            }
            return FBest;
        }
    }
}
