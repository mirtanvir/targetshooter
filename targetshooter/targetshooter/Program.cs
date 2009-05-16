using System;

namespace targetshooter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TargetShooter game = new TargetShooter())
            {
                game.Run();
            }
        }
    }
}

