using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    private static bool[] population;
    private static bool check = false; // flag to see if one robor has cross the terrain
    private static bool[] matingPool;
    private static int generations = 0;
    private static int target;//actually its a terrain
    private static mutationRate;
    //perfectScore
    //best
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num; i++)
        {
            population[i] = 0;//new Robot(target.lenght)
        }

        //ac[a se inicaliza el objeto y se pasan los parametros de el
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /***
    calculateFitness() 4 each element
    que sea que se calcula en el mismo terrain
    depending on their fitness theyre selected 
    to be in the matingpool
     */
}
