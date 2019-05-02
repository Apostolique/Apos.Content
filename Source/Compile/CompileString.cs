using System;
using System.IO;
using Optional;

namespace Apos.Content.Compile {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class CompileString : Compiler<string, Settings<string>> {
        /// <summary>
        /// Builds a string content.
        /// </summary>
        public override void Build(Stream input, Stream output, Settings<string> settings) {
            using (StreamReader sr = new StreamReader(input))
            using (BinaryWriter bw = new BinaryWriter(output)) {
                string text = sr.ReadToEnd();
                bw.Write(text);
            }
        }
    }
}