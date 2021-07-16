using System;

namespace MonoPong
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Pong())
                game.Run();
        }
    }
}
