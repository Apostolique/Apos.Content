using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using Apos.Content;

namespace GameExample {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            using (var game = new Core())
                game.Run();
        }
    }
}