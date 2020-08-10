using System;
using System.ComponentModel;
using static GeneticLogistic.Utilities;

namespace GeneticLogistic
{
    public class Terrain
    {
        public static int[,] matrixTerrain;
        
        //probability of each terrain to be selected shown as percentage

        public static int[,] MakeTerrain()
        {
            CreateMatrix();

            //Verifies if the start or end are blocked cells
            if (matrixTerrain[0, 19] == 0 || matrixTerrain[19, 0] == 0)
            {
                calculateStartEnd();
            }

            //Verifies if the can actually get to the end 
            if (matrixTerrain[0, 18] == 0 || matrixTerrain[1, 19] == 0)
            {
                calculatePreEnd();
            }
            
            //Verifies if the can actually get out of the start 
            if (matrixTerrain[18, 0] == 0 || matrixTerrain[19, 1] == 0)
            {
                calculatePostStart();
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
                return 2; //medium terrain 40-69
            }
            else
            {
                return 3; //hard terrain 69-99
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
        
        private static void calculatePreEnd()
        {
            calculatePreEndLeft();
            calculatePreEndDown();
        }

        private static void calculatePreEndLeft()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[0, 18] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[0, 18] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[0, 18] = 3; //normal terrain 66-69
            }
        }

        private static void calculatePreEndDown()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[1, 19] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[1, 19] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[1, 19] = 3; //normal terrain 66-69
            }
        }
        
        private static void calculatePostStart()
        {
            calculatePostStartRight();
            calculatePostStartUp();
        }

        private static void calculatePostStartRight()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[19, 1] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[19, 1] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[19, 1] = 3; //normal terrain 66-69
            }
        }

        private static void calculatePostStartUp()
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 33)
            {
                matrixTerrain[18, 0] = 1; //blocked terrain 0-32
            }
            else if (33 <= probability &&  66 > probability)
            {
                matrixTerrain[18, 0] = 2; //normal terrain 33-65
            }
            else
            {
                matrixTerrain[18, 0] = 3; //normal terrain 66-69
            }
        }
        
    }
    
    
}