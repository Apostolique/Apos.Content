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

            string helloFile = "Hello.txt";
            string redImageFile = "RedImage.png";
            string loadingImageFile = "Loading.png";

            string helloInput = createInputPath(contentPath, helloFile);
            string redImageInput = createInputPath(contentPath, redImageFile);
            string loadingImageInput = createInputPath(contentPath, loadingImageFile);

            string helloOutput = createOutputPath(buildPath, helloFile);
            string redImageOutput = createOutputPath(buildPath, redImageFile);
            string loadingImageOutput = createOutputPath(buildPath, loadingImageFile);

            Target target = new Target(Target.TargetPlatform.Windows, Target.TargetGraphicsBackend.OpenGL);

            CompileString cs = new CompileString();
            CompileTexture2D ct = new CompileTexture2D();

            Settings<Texture2D> settingsTexture = new Settings<Texture2D>(target);
            Settings<string> settingsString = new Settings<string>(target);

            buildContent<string>(cs, helloInput, helloOutput, settingsString);
            buildContent<Texture2D>(ct, redImageInput, redImageOutput, settingsTexture);
            buildContent<Texture2D>(ct, loadingImageInput, loadingImageOutput, settingsTexture);
        }
        private static void buildContent<T>(Compiler<T> c, string inputPath, string outputPath, Settings<T> settings) {
            c.Build(inputPath, outputPath, settings);
        }
        private static string createInputPath(string contentPath, string fileName) {
            return Path.Combine(contentPath, fileName);
        }
        private static string createOutputPath(string buildPath, string fileName) {
            return Path.Combine(buildPath, Path.ChangeExtension(fileName, ".xnb"));
        }
    }
}