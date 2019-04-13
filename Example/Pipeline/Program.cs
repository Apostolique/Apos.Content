using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using Apos.Content.Compile;

namespace Pipeline {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            string contentPath = "Content";
            string buildPath = "bin";

            string textExtension = ".txt";
            string pngExtension = ".png";

            string helloFile = "Hello";
            string redImageFile = "RedImage";

            string helloInput = createInputPath(contentPath, helloFile, textExtension);
            string redImageInput = createInputPath(contentPath, redImageFile, pngExtension);

            string helloOutput = createOutputPath(buildPath, helloFile);
            string redImageOutput = createOutputPath(buildPath, redImageFile);

            Target target = new Target(Target.TargetPlatform.Windows, Target.TargetGraphicsBackend.OpenGL);

            CompileString cs = new CompileString();
            CompileTexture2D ct = new CompileTexture2D();

            Settings<Texture2D> settingsTexture = new Settings<Texture2D>(target);
            Settings<string> settingsString = new Settings<string>(target);

            buildContent<string>(cs, helloInput, helloOutput, settingsString);
            buildContent<Texture2D>(ct, redImageInput, redImageOutput, settingsTexture);
        }
        private static void buildContent<T>(Compiler<T> c, string inputPath, string outputPath, Settings<T> settings) {
            c.Build(inputPath, outputPath, settings);
        }
        private static string createInputPath(string contentPath, string fileName, string extension) {
            return Path.Combine(contentPath, fileName + extension);
        }
        private static string createOutputPath(string buildPath, string fileName) {
            return Path.Combine(buildPath, fileName + ".xnb");
        }
    }
}