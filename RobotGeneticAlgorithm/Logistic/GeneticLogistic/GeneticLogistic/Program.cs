using System;
using static GeneticLogistic.Terrain;
using static GeneticLogistic.Robot;
using static GeneticLogistic.MarkovChain;

namespace GeneticLogistic
{
    class Program
    {
        private static int popmax  = 50;
        private static double mutationRate = 0.01;
        private static Terrain terrain =  new Terrain();
        private static Population p;
        
        public static void Main(string[] args)
        {
            p = new Population(terrain, mutationRate, popmax);

            while (p.evaluate())
            {
                p.naturalSelection();
                p.generate();
            }
        }
    }
}