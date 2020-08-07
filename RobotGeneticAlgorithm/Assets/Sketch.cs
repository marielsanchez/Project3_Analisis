using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sketch : MonoBehaviour
{
    private bool target; //the terrain robots have to cross
    private int popmax;
    private float mutationRate;
    private int bestTry;
    //allPhrase
    // stats
    
    // Start is called before the first frame update
    void Start()
    {
        //new Population(target,mutationRate,popmax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // in this file you'll find how the population is generate

    void Draw()
    {
        //population.caculateFitness its auto calculated in the terrain
        //population.naturalSelection generates a mating pool
        //population.generate
        //population.evaluate
    }
}
