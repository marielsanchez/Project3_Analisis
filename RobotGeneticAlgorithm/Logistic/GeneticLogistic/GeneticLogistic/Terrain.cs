using System;
using System.ComponentModel;

namespace GeneticLogistic
{
    public class Terrain
    {
        public static int[,] matrixTerrain;
        
        //to generate the random number
        private static readonly Random random = new Random(); 
        private static readonly object syncLock = new object(); 
        
        //probability of each terrain to be selected shown as percentage

        public static int[,] MakeTerrain()
        {
            CreateMatrix();

            //Verifies if the start or end are blocked cells
            if (matrixTerrain[0, 19] == 0 || matrixTerrain[19, 0] == 0)
            {
                calculateStartEnd();
            }

            PrintMatrix();
            return matrixTerrain;
        }

        public static int TerrainProbability()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 25)
            {
                return 0; //blocked terrain 0-9
            }
            else if (25 <= probability &&  50 > probability)
            {
                return 1; //normal terrain 10-39
            }
            else if (50 <= probability &&  75 > probability)
            {
                return 2; //normal terrain 40-69
            }
            else
            {
                return 3; //hard terrain 69-99
            }
        }
        
        public static int RandomNumber(int min, int max)
        {
            lock(syncLock) { // synchronize
                return random.Next(min, max);
            }
        }

        private static void CreateMatrix()
        {
            matrixTerrain = new int[20,20];
            for(int i = 0; i < matrixTerrain.GetLength(0); i++) 
            {
                for (int j = 0; j < matrixTerrain.GetLength(1); j++)
                {
                    matrixTerrain[i, j] = TerrainProbability();
                }
            }
        }

        private static void PrintMatrix()
        {
            for(int i = 0; i < matrixTerrain.GetLength(0); i++) 
            {
                for (int j = 0; j < matrixTerrain.GetLength(1); j++)
                {
                    Console.Write(matrixTerrain[i,j]+" ");
                }
                Console.WriteLine();
        
            }
        }

        private static void calculateStartEnd()
        {
            calculateStart();
            calculateEnd();
        }
        
        private static void calculateStart()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[19, 0] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[19, 0] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[19, 0] = 3; //normal terrain 66-69
            }
        }
        
        private static void calculateEnd()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[0, 19] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[0, 19] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[0, 19] = 3; //normal terrain 66-69
            }
        }
    }
}