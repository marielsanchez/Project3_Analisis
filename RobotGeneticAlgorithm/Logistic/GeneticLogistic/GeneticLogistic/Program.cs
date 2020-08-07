using System;
using static GeneticLogistic.Terrain;

namespace GeneticLogistic
{
    class Program
    {
        public static int[,] matrixTerrain;
        
        public static void Main(string[] args)
        {
            matrixTerrain = MakeTerrain();
        }
    }
}