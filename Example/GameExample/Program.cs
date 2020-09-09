using System;

namespace GameExample {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            using (var game = new Core())
                game.Run();
        }
    }
}
