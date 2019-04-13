using System;
using System.IO;
using Optional;

namespace Apos.Content.Compile {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class CompileString : Compiler<string> {
        /// <summary>
        /// Builds a string content.
        /// </summary>
        public override void Build(Stream input, Stream output, Settings<string> settings) {
            using (StreamReader br = new StreamReader(input))
            using (BinaryWriter bw = new BinaryWriter(output)) {
                string text = br.ReadToEnd();
                bw.Write(text);
            }
        }
    }
}