using System;
using System.Linq;

namespace GeneticLogistic
{
    public class Robot
    {
        private byte batery;
        private byte camera;
        private byte motor;
        
        public int bateryLeft;
        public int fitness;
        
        //to generate the random number
        private static readonly Random random = new Random(); 
        private static readonly object syncLock = new object(); 

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
        
        public static int RandomNumber(int min, int max)
        {
            lock(syncLock) { // synchronize
                return random.Next(min, max);
            }
        }
    }
}