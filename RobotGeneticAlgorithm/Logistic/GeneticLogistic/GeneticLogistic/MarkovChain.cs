using System;
using static GeneticLogistic.Utilities;

namespace GeneticLogistic
{
    public class MarkovChain
    {
        // the best probability where the robot could go
        private byte motor;
        private byte batery;
        private byte camera;
        private int bestProb; 
        private int statesNum;
        private int[] states;              // all states
        private int[] usedStates;          // states != 0
        private int[] usedStatesIndex;     // index of states != 0
        private double probabilityUp;
        private double probabilityDown;
        private double probabilityLeft;
        private double probabilityRight;

        public MarkovChain()
        {
            bestProb = 0;
            statesNum = 0;
            probabilityUp = 0.0;
            probabilityDown = 0.0;
            probabilityLeft = 0.0;
            probabilityRight = 0.0;
        }
        
        public MarkovChain(int[] stat, byte bat, byte cam, byte mot)
        {
            motor = mot;
            camera = cam;
            batery = bat;
            bestProb = 0;
            states = stat;
            statesNum = 0;
            probabilityUp = 0.0;
            probabilityDown = 0.0;
            probabilityLeft = 0.0;
            probabilityRight = 0.0;
            usedStates = new int[4];
            usedStatesIndex = new int[4];
        }

        public int GenerateProbability()
        {
            InitialiceProbability();
            TerrainProbability();
            return CalculateProbability();
        }

        private void InitialiceProbability()
        {
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i] != 0)
                {
                    usedStates[statesNum] = states[i];
                    usedStatesIndex[statesNum] = i;
                    statesNum++;
                }
            }
            //statesNum returns all the possibles states where the robot can go
            if (statesNum == 4)
            {
                probabilityUp = 25;
                probabilityDown = 25; 
                probabilityLeft = 25; 
                probabilityRight = 25;
                bestProb  = RandomNumber(0, 3);
            }
            
            if ( statesNum == 3)
            {
                probabilityUp = 33.3;
                probabilityDown = 33.3; 
                probabilityLeft = 33.3; 
                probabilityRight = 33.3;
                
                if ( states[0] == 0)
                {
                    probabilityUp = 0;
                    bestProb  = RandomNumber(1, 3);
                }
                else if ( states[1] == 0)
                {
                    bestProb = 0;
                    probabilityDown = 0;
                }
                else if ( states[2] == 0)
                {
                    bestProb  = RandomNumber(0, 1);
                    probabilityLeft = 0;
                }
                else if ( states[3] == 0)
                {
                    bestProb  = RandomNumber(0, 2);
                    probabilityRight = 0;
                }
            }
            
            if ( statesNum == 2)
            {
                probabilityUp = 50;
                probabilityDown = 50; 
                probabilityLeft = 50; 
                probabilityRight = 50;
                
                if ( states[0] == 0)
                {
                    probabilityUp = 0;
                }
                if ( states[1] == 0)
                {
                    probabilityDown = 0;
                }
                if ( states[2] == 0)
                {
                    probabilityLeft = 0;
                }
                if ( states[3] == 0)
                {
                    probabilityRight = 0;
                }
                
                //chooses the best probability
                if (probabilityUp == 50)
                {
                    bestProb = 0;
                }
                else if (probabilityDown == 50)
                {
                    bestProb = 1;
                } 
                else if (probabilityLeft == 50)
                {
                    bestProb = 2;
                }
            }
            
            if ( statesNum == 1)
            {
                probabilityUp = 100;
                probabilityDown = 100; 
                probabilityLeft = 100; 
                probabilityRight = 100;
                
                if ( states[0] == 0)
                {
                    probabilityUp = 0;
                }
                if ( states[1] == 0)
                {
                    probabilityDown = 0;
                }
                if ( states[2] == 0)
                {
                    probabilityLeft = 0;
                }
                if ( states[3] == 0)
                {
                    probabilityRight = 0;
                }

                //chooses the best probability
                if (probabilityUp == 100)
                {
                    bestProb = 0;
                }
                else if (probabilityDown == 100)
                {
                    bestProb = 1;
                } 
                else if (probabilityLeft == 100)
                {
                    bestProb = 2;
                }
                else if (probabilityRight == 100)
                {
                    bestProb = 3;
                }
            }
        }

        private void TerrainProbability()
        {
            // Reassign the probabilities according to the motor/terrain
            if (170 > motor) // is standar or plus motor
            {
                double restingProbability = 0.0;
                for (int i = 0; i < states.Length; i++)
                {
                    if (states[i] == 3) // its a hard terrain
                    {
                        if ( i == 0 )
                        {
                            restingProbability += (probabilityUp/2);
                            probabilityUp = (probabilityUp/2);
                        }
                        if ( i == 1 )
                        {
                            restingProbability += (probabilityDown/2);
                            probabilityDown = (probabilityDown/2);
                        }
                        if ( i == 2 )
                        {
                            restingProbability += (probabilityLeft/2);
                            probabilityLeft = (probabilityLeft/2);
                        }
                        if ( i == 3 )
                        {
                            restingProbability += (probabilityRight/2);
                            probabilityRight = (probabilityRight/2);
                        };
                    }
                }

                if (85 > motor) // is standar motor
                {
                    for (int i = 0; i < states.Length; i++)
                    {
                        if (states[i] == 2) // its a medium terrain
                        {
                            if ( i == 0 )
                            {
                                restingProbability += (probabilityUp/2);
                                probabilityUp = (probabilityUp/2);
                            }
                            if ( i == 1 )
                            {
                                restingProbability += (probabilityDown/2);
                                probabilityDown = (probabilityDown/2);
                            }
                            if ( i == 2 )
                            {
                                restingProbability += (probabilityLeft/2);
                                probabilityLeft = (probabilityLeft/2);
                            }
                            if ( i == 3 )
                            {
                                restingProbability += (probabilityRight/2);
                                probabilityRight = (probabilityRight/2);
                            };
                        }
                    }
                }
                
                //adding the bestFit to the probability
                for (int i = 0; i < states.Length; i++)
                {
                    if ( bestProb == i )
                    {
                        if ( i == 0 )
                        {
                            probabilityUp += restingProbability;
                        }
                        if ( i == 1 )
                        {
                            probabilityDown += restingProbability;
                        }
                        if ( i == 2 )
                        {
                            probabilityLeft += restingProbability;
                        }
                        if ( i == 3 )
                        {
                            probabilityRight += restingProbability;
                        };
                    }
                }
            }
            
        }

        public int CalculateProbability()
        {
            // if there's a 100% prob
            if (statesNum == 1)
            {
                return Calculate1State();
            }
            
            /*
            Adding the probabilities to create a probability range
            probabilityUp is not taking in count;
            */
            probabilityDown += probabilityUp;
            probabilityLeft += probabilityDown;
            probabilityRight += probabilityLeft;
            
            Console.WriteLine(probabilityUp);
            Console.WriteLine(probabilityDown);
            Console.WriteLine(probabilityLeft);
            Console.WriteLine(probabilityRight);
            
            // if there's a 50% prob at least
            if (statesNum == 2)
            {
                return Calculate2States();
            }
            if (statesNum == 3)
            {
                return Calculate3States();
            }
            return Calculate4States();
        }

        private int Calculate4States()
        {
            int probability  = RandomNumber(0, 99);
            Console.WriteLine("Random = " + probability);

            if (probability < probabilityDown)
            {
                //probabilityUp
                return 0;
            }
            else if (probability < probabilityLeft)
            {
                //probabilityDown
                return 1;
            }
            else if (probability < probabilityRight)
            {
                //probabilityLeft
                return 2;
            }
            return 3; //probabilityRight
        }
        
        private int Calculate3States()
        {
            int probability  = RandomNumber(0, 99);
            Console.WriteLine("Random = " + probability);
            //see if the first prob or last prob is 0
            if (probabilityUp == 0)
            {
                // do not take in cout the probabilityUp
                if (probability < probabilityLeft)
                {
                    //probabilityDown
                    return 1;
                }
                if (probability < probabilityRight)
                {
                    //probabilityLeft
                    return 2;
                }
                return 3;
            }

            // added probability
            if ( probability < probabilityDown)
            {
                //probabilityUp
                return 0;
            }
            if (probability < probabilityLeft)
            {
                //probabilityDown
                return 1;
            }
            if (probability < probabilityRight)
            {
                //probabilityLeft
                return 2;
            }
            return 3;
        }

        private int Calculate2States()
        {
            int probability  = RandomNumber(0, 99);
            Console.WriteLine("Random = " + probability);
            //see if the first prob or last prob is 0
            if (probabilityUp == 0)
            {
                if (probabilityDown == 0)
                {
                    if (probability < probabilityRight)
                    {
                        //probabilityLeft
                        return 2;
                    }

                    return 3; // probabilityRight
                }
                // does not matter really cuz the probability is added
                if (probability <= probabilityDown)
                {
                    //probabilityDown
                    return 1;
                }
                if (probability <= probabilityLeft)
                {
                    //probabilityLeft
                    return 2;
                }
                return 3; // probabilityRight
            }

            // added probability
            if ( probability <= probabilityUp)
            {
                //probabilityUp
                return 0;
            }
            if (probability <= probabilityDown)
            {
                //probabilityDown
                return 1;
            }
            if (probability <= probabilityLeft)
            {
                //probabilityLeft
                return 2;
            }
            return 3;
        }

        private int Calculate1State()
        {
            if (probabilityUp != 0)
            {
                return 0;
            }
            if (probabilityDown != 0)
            {
                return 1;
            }
            if (probabilityLeft != 0)
            {
                return 2;
            }
            return 3; //probabilityRight != 0
        }

    }
}