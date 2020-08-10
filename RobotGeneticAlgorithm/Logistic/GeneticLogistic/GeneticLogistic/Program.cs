using System;
using static GeneticLogistic.Terrain;
using static GeneticLogistic.Robot;
using static GeneticLogistic.MarkovChain;

namespace GeneticLogistic
{
    class Program
    {
        public static int[,] matrixTerrain;
        
        public static void Main(string[] args)
        {
            //matrixTerrain = MakeTerrain();
            //Proof();
            //double prob = 25;
            //prob = (prob / 2);
            //Console.WriteLine(prob);
            int[] array = new[] {1, 0, 1, 0};
            MarkovChain mar = new MarkovChain(array,0,0,80);
            
            int result = mar.GenerateProbability();
            Console.WriteLine(result);
            //MarkovChain(int[] stat, byte bat, byte cam, byte mot)
        }
    }
}