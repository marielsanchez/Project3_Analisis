using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private int[,] matrixTerrain;
    

        // Start is called before the first frame update
    void Start()
    {
        initialiceMatrix();
        printMatrix();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialiceMatrix()
     {
         matrixTerrain = new int[20,20];
         for(int i = 0; i < matrixTerrain.GetLength(0); i++) 
         {
             for (int j = 0; j < matrixTerrain.GetLength(1); j++)
             {
                 mat[i,j] = 0;
             }
         }
     }
    
    void printMatrix()
    {
        for(int i = 0; i < matrixTerrain.GetLength(0); i++) 
        {
            for (int j = 0; j < matrixTerrain.GetLength(1); j++)
            {
                Console.Write(mat[i,j]+" ");
            }
            Console.WriteLine();
            
        }
    }
}
