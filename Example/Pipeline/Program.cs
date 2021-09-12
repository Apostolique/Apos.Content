using System;
using System.IO;
using Apos.Content.Compile;
using CommandLine;

namespace Pipeline {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => {
                Console.WriteLine("Input path: " + o.Input);
                Console.WriteLine("Output path: " + o.Output);

                _inputPath = o.Input;
                _outputPath = o.Output;

                Target target = new Target(TargetPlatform.Windows, TargetGraphicsBackend.OpenGL);

                Texture2DSettings settingsTexture = new Texture2DSettings(target);
                Settings<string> settingsString = new Settings<string>(target);
                Settings<byte[]> settingsBinary = new Settings<byte[]>(target);

                StringCompiler cs = new StringCompiler();
                Texture2DCompiler ct = new Texture2DCompiler();
                BinaryCompiler cb = new BinaryCompiler();

                string helloFile = "Hello.txt";
                string redImageFile = "RedImage.png";
                string loadingImageFile = "Loading.png";

                string backgroundFile = "Background.png";
                string boardFile = "Board.png";
                string ballFile = "Ball.png";
                string paddle1File = "Paddle1.png";
                string paddle2File = "Paddle2.png";

                string fontFile = "SourceCodePro-Medium.ttf";

                BuildContent(cs, helloFile, settingsString);
                BuildContent(ct, redImageFile, settingsTexture);
                BuildContent(ct, loadingImageFile, settingsTexture);

                BuildContent(ct, backgroundFile, settingsTexture);
                BuildContent(ct, boardFile, settingsTexture);
                BuildContent(ct, ballFile, settingsTexture);
                BuildContent(ct, paddle1File, settingsTexture);
                BuildContent(ct, paddle2File, settingsTexture);
                BuildContent(cb, fontFile, settingsBinary);
            });
        }
        private static string _inputPath { get; set; }
        private static string _outputPath { get; set; }
        private static void BuildContent<T, K>(Compiler<T, K> c, string inputFile, K settings) where K : Settings<T> {
            BuildContent(c, CreateInputPath(_inputPath, inputFile), CreateOutputPath(_outputPath, inputFile), settings);
        }
        private static void BuildContent<T, K>(Compiler<T, K> c, string inputPath, string outputPath, K settings) where K : Settings<T> {
            c.Build(inputPath, outputPath, settings);
        }
        private static string CreateInputPath(string contentPath, string fileName) {
            return Path.Combine(contentPath, fileName);
        }
        private static string CreateOutputPath(string buildPath, string fileName) {
            return Path.Combine(buildPath, Path.ChangeExtension(fileName, ".xnb"));
        }

        private class Options {
            [Option('i', "input", Required = true, HelpText = "Sets content input path.")]
            public string Input { get; set; }
            [Option('o', "output", Required = true, HelpText = "Sets content output path.")]
            public string Output { get; set; }
        }
    }
}
