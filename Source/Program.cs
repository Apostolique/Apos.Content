using System;
using System.IO;
using Optional;

namespace Apos.Content {
    class Program {
        static void Main(string[] args) {
            string contentPath = "Content/";
            string buildPath = "bin/";

            string helloFile = "Hello";

            string inputPath = contentPath + helloFile + ".txt";
            string outputPath = buildPath + helloFile + ".xnb";

            ContentString sc = new ContentString();

            // Build a string content.
            sc.Build(inputPath, outputPath);

            // Read a string content.
            Option<string> textObject = sc.Read(outputPath);
            textObject.MatchSome(t => {
                Console.WriteLine(t);
            });
        }
    }
}