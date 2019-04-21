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

                Target target = new Target(TargetPlatform.Windows, TargetGraphicsBackend.OpenGL);

                CompileString cs = new CompileString();
                CompileTexture2D ct = new CompileTexture2D();

                SettingsTexture2D settingsTexture = new SettingsTexture2D(target);
                Settings<string> settingsString = new Settings<string>(target);

                buildContent(cs, helloInput, helloOutput, settingsString);
                buildContent(ct, redImageInput, redImageOutput, settingsTexture);
                buildContent(ct, loadingImageInput, loadingImageOutput, settingsTexture);
            });
        }
        private static void buildContent<T, K>(Compiler<T, K> c, string inputPath, string outputPath, K settings) where K : Settings<T> {
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