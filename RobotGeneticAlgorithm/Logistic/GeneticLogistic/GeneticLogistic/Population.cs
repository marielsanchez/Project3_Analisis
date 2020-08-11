using System;
using System.Collections.Generic;
using System.Linq;
using static GeneticLogistic.Utilities;

namespace GeneticLogistic
{
    public class Population
    {
        //private static bool[] population;
        private List<Robot> population;
        private List<Robot> matingPool;
        private static bool check; // flag to see if one robor has cross the terrain
        private static int generations;
        private static Terrain target;//actually it's a terrain
        private static double mutationRate;
        private static int popMax;
        private static Robot best;

        public Population(Terrain terrain,double mutation, int max)
        {
            generations = 0;
            check = false;
            target = terrain;
            mutationRate = mutation;
            popMax = max;
            best = null;
            
            matingPool = new List<Robot>();
            population = new List<Robot>();
            
            for (int i = 0; i < max; i++)
            {
                population.Add(new Robot());
                population.ElementAt(i).CrossPath(target);
            }


        }

        public void naturalSelection()
        {
            //generates de mating pool
            matingPool = new List<Robot>();
            int maxFitness = 0;
            for (int i = 0; i < population.Count; i++)
            {
                if (population.ElementAt(i).fitness > maxFitness)
                {
                    maxFitness = population.ElementAt(i).fitness;
                }
            }
            
            for (int i = 0; i < population.Count; i++)
            {
                double fitness = (double) population.ElementAt(i).fitness / maxFitness * 100;
                int fitnessInt = Convert.ToInt32(fitness);

                for (int j = 0; j < fitnessInt; j++)
                {
                    matingPool.Add(population.ElementAt(i));
                }


                if (population.ElementAt(i).fitness > maxFitness)
                {
                    maxFitness = population.ElementAt(i).fitness;
                }
            }
        }

        public void generate()
        {
            for (int i = 0; i < population.Count; i++)
            {
                int probabilityA  = RandomNumber(0, population.Count);
                int probabilityB  = RandomNumber(0, population.Count);
                
                Robot a = matingPool.ElementAt(probabilityA);
                Robot b = matingPool.ElementAt(probabilityB);

                Robot child = a.crossOver(b);
                child.mutate(mutationRate);
                this.population.Insert(i,child);
            }
            generations++;
        }

        public bool evaluate()
        {
            bool check = false;
            for (int i = 0; i < population.Count; i++)
            {
                if (population.ElementAt(i).arrive)
                {
                    check = true;
                }
            }
            return check;
        }

    }
}