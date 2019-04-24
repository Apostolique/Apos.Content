using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using Apos.Content.Compile;
using CommandLine;
using System.Collections.Generic;

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

                SettingsTexture2D settingsTexture = new SettingsTexture2D(target);
                Settings<string> settingsString = new Settings<string>(target);
                Settings<byte[]> settingsBinary = new Settings<byte[]>(target);

                CompileString cs = new CompileString();
                CompileTexture2D ct = new CompileTexture2D();
                CompileBinary cb = new CompileBinary();

                string helloFile = "Hello.txt";
                string redImageFile = "RedImage.png";
                string loadingImageFile = "Loading.png";

                string backgroundFile = "Background.png";
                string boardFile = "Board.png";
                string ballFile = "Ball.png";
                string paddle1File = "Paddle1.png";
                string paddle2File = "Paddle2.png";

                string fontFile = "SourceCodePro-Medium.ttf";

                buildContent(cs, helloFile, settingsString);
                buildContent(ct, redImageFile, settingsTexture);
                buildContent(ct, loadingImageFile, settingsTexture);

                buildContent(ct, backgroundFile, settingsTexture);
                buildContent(ct, boardFile, settingsTexture);
                buildContent(ct, ballFile, settingsTexture);
                buildContent(ct, paddle1File, settingsTexture);
                buildContent(ct, paddle2File, settingsTexture);
                buildContent(cb, fontFile, settingsBinary);
            });
        }
        private static string _inputPath {
            get;
            set;
        }
        private static string _outputPath {
            get;
            set;
        }
        private static void buildContent<T, K>(Compiler<T, K> c, string inputFile, K settings) where K : Settings<T> {
            buildContent(c, createInputPath(_inputPath, inputFile), createOutputPath(_outputPath, inputFile), settings);
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