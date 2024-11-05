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


        public Butterfly()
        {
            Name = "Butterfly optimization algorithm";
        }

        public double Solve()
        {
            return 0.2;
        }
    }
}
