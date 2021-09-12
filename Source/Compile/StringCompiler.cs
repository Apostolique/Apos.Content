using System.IO;

namespace Apos.Content.Compile {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class StringCompiler : Compiler<string, Settings<string>> {
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
