using System;
using System.Linq;
using static GeneticLogistic.Utilities;

namespace GeneticLogistic
{
    public class Robot
    {
        //Standar: 0 - 84
        //Plus: 85 - 169
        //Pro: 170 - 255
        private byte batery;
        private byte camera;
        private byte motor;
        private byte[] DNA;//contains the combination of the RobotDNA
        private string DNAstr;
        private int positionRow;
        private int positionColumn;
        private bool alive;
        public bool arrive;
        
        public int bateryLeft;
        public int fitness;

        public Robot()
        {
            CreateRobot();
            //puts the robot in the start
            positionRow = 19;
            positionColumn = 0;
            alive = true;
            arrive = false;
        }

        public Robot(byte motorR, byte bateryR)
        {
            motor = motorR;
            batery = bateryR;
            SetBateryLeft(batery);
            
            int probabilityCamera  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            camera = (byte)probabilityCamera;

            DNA = new byte[] { batery, camera, motor};
            string[] DNA_StrArray = DNA.Select( x => Convert.ToString( x, 2 ).PadLeft( 8, '0' ) ).ToArray();
            DNAstr = ConvertStringArrayToStringJoin(DNA_StrArray);
            
            positionRow = 19;
            positionColumn = 0;
            alive = true;
            arrive = false;
        }

        public void CreateRobot()
        {
            int probabilityBatery  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            batery = (byte)probabilityBatery;
            SetBateryLeft(batery);
            
            int probabilityCamera  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            camera = (byte)probabilityCamera;
            
            int probabilityMotor  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            motor = (byte)probabilityMotor;
            
            DNA = new byte[] { batery, camera, motor};
            string[] DNA_StrArray = DNA.Select( x => Convert.ToString( x, 2 ).PadLeft( 8, '0' ) ).ToArray();
            DNAstr = ConvertStringArrayToStringJoin(DNA_StrArray);
        }

        private void SetBateryLeft(byte b)
        {
            if (b < 85) // Standar Motor
            {
                bateryLeft = 500;
            }
            else if (b < 170) // Plus Motor
            {
                bateryLeft = 750;
            }
            else if (b < 256) // Pro Motor
            {
                bateryLeft = 1000;
            }
        }

        public void CrossPath(Terrain t)
        {
            // Verifies the terrain at the start 
            int terrainValue = t.getValue(positionRow, positionColumn);
            if (this.motor < 85)
            {
                // It's a standar motor
                if (terrainValue != 1)
                {
                    Console.WriteLine("Motor standar en terreno no apto terrain value 1");
                    alive = false;
                }
            }
            else if (motor < 170)
            {
                // It's a standar motor
                if (terrainValue == 3)
                {
                    Console.WriteLine("Motor plus en terreno no apto");
                    alive = false;
                }
            }

            if (alive)
            {
                fitness++;
                bateryLeft = bateryLeft - terrainValue;
            }

            while (bateryLeft > 0 && alive)
            {
                int[] markovArray = new int[4];
                markovArray[0] = t.getValue(positionRow - 1, positionColumn); // Up
                markovArray[1] = t.getValue(positionRow + 1, positionColumn); // Down
                markovArray[2] = t.getValue(positionRow, positionColumn  - 1); // Left
                markovArray[3] = t.getValue(positionRow, positionColumn + 1); // Right
                
                if ( ( markovArray[0] == 0 && markovArray[1] == 0 ) && ( markovArray[2] == 0 && markovArray[3] == 0 ) ){
                    // in case all the posible states are 0,
                    // breaks the cycle and the robot dies
                    Console.WriteLine("Todo sus alrededores bloqueados");
                    break;
                }
                
                MarkovChain mar = new MarkovChain(markovArray,batery,camera,motor);
                int result = mar.GenerateProbability();
                
                if (result == 0)
                {
                    //Robot moves Up
                    positionRow--;
                    t.moveTo(positionRow,positionColumn);
                }
                else if (result == 1)
                {
                    //Robot moves Down
                    positionRow++;
                    t.moveTo(positionRow,positionColumn);
                }
                else if (result == 2)
                {
                    //Robot moves Left
                    positionColumn--;
                    t.moveTo(positionRow,positionColumn);
                }
                else if (result == 3)
                {
                    //Robot moves Right
                    positionColumn++;
                    t.moveTo(positionRow,positionColumn);
                }
                
                if (positionRow < 0  || positionColumn < 0)
                {
                    // out of index 
                    Console.WriteLine("Out of index");
                    break;
                }
                if (positionRow > 19 || positionColumn > 19)
                {
                    // out of index 
                    Console.WriteLine("Out of index");
                    break;
                }
                
                // Verifies the terrain 
                terrainValue = t.getValue(positionRow, positionColumn);

                if (terrainValue == 0)
                {
                    // Error its a blocked cell
                    Console.WriteLine("Celda bloqueada");
                    break;
                }

                if (motor < 85)
                {
                    // It's a standar motor
                    if (terrainValue != 1)
                    {
                        Console.WriteLine("Motor standar en terreno no apto");
                        break;
                    }
                }
                else if (motor < 170)
                {
                    // It's a standar motor
                    if (terrainValue == 3)
                    {
                        Console.WriteLine("Motor plus en terreno no apto");
                        break;
                    }
                }

                int[] actual = t.getActual();
                if (actual[0] == 0 && actual[1] == 19)
                {
                    Console.WriteLine("The Robot has passed");
                    arrive = true;
                }

                fitness++;
                bateryLeft = bateryLeft - terrainValue;
                
            }
            
            Console.Write("El Robot llegó hasta fila ");
            Console.Write(positionRow);
            Console.Write(" y col ");
            Console.WriteLine(positionColumn);
            Console.Write("Fitness: " +fitness+" Batery Left: "+bateryLeft);
        }
        
        
        // ----- GENETIC ALGORITHM

        public Robot crossOver(Robot robot)
        {
            int probability  = RandomNumber(0, 99); //rnd.Next(0, 99);  // creates a number between 0 and 99
            if (probability < 50)
            {
                Robot r = new Robot(this.motor, robot.batery);
                return r;
            }
            else
            {
                Robot r = new Robot(robot.motor, this.batery);
                return r;
            }
        }

        public void mutate(double mutationRate)
        {
            int probability  = RandomNumber(0, 99);
            if (probability < (mutationRate*100) )
            {
                if (this.batery < 255)
                {
                    batery++;
                }
                if (this.motor < 255)
                {
                    motor++;
                }
            }
        }
    }
}