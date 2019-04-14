using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using Apos.Content.Compile;
using CommandLine;

namespace Pipeline {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => {
                Console.WriteLine("Input path: " + o.Input);
                Console.WriteLine("Output path: " + o.Output);

                string inputPath = o.Input;
                string outputPath = o.Output;

                string helloFile = "Hello.txt";
                string redImageFile = "RedImage.png";
                string loadingImageFile = "Loading.png";

                string helloInput = createInputPath(inputPath, helloFile);
                string redImageInput = createInputPath(inputPath, redImageFile);
                string loadingImageInput = createInputPath(inputPath, loadingImageFile);

                string helloOutput = createOutputPath(outputPath, helloFile);
                string redImageOutput = createOutputPath(outputPath, redImageFile);
                string loadingImageOutput = createOutputPath(outputPath, loadingImageFile);

                Target target = new Target(Target.TargetPlatform.Windows, Target.TargetGraphicsBackend.OpenGL);

                CompileString cs = new CompileString();
                CompileTexture2D ct = new CompileTexture2D();

                Settings<Texture2D> settingsTexture = new Settings<Texture2D>(target);
                Settings<string> settingsString = new Settings<string>(target);

                buildContent<string>(cs, helloInput, helloOutput, settingsString);
                buildContent<Texture2D>(ct, redImageInput, redImageOutput, settingsTexture);
                buildContent<Texture2D>(ct, loadingImageInput, loadingImageOutput, settingsTexture);
            });
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

        private class Options {
            [Option('i', "input", Required = true, HelpText = "Sets content input path.")]
            public string Input {
                get;
                set;
            }
            [Option('o', "output", Required = true, HelpText = "Sets content output path.")]
            public string Output {
                get;
                set;
            }
        }
    }
}