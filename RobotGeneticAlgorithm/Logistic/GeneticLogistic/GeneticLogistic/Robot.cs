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
        
        public int bateryLeft;
        public int fitness;

        public Robot()
        {
            
        }

        public static  void Proof()
        {
            int probabilityBatery  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            byte batery = (byte)probabilityBatery;
            Console.WriteLine(batery);
            
            int probabilityCamera  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            byte camera = (byte)probabilityCamera;
            Console.WriteLine(camera);
            
            int probabilityMotor  = RandomNumber(0, 255); //rnd.Next(0, 99);  // creates a number between 0 and 99
            byte motor = (byte)probabilityMotor;
            Console.WriteLine(motor);
            
            byte[] DNA = new byte[] { batery, camera, motor};
            string[] DNAstr = DNA.Select( x => Convert.ToString( x, 2 ).PadLeft( 8, '0' ) ).ToArray();
            foreach (var number in DNAstr)
            {
                Console.Write(number);
            }

        }
        
    }
}