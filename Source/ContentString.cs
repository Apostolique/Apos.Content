using System;
using System.IO;
using Optional;

namespace Apos.Content {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class ContentString : Content<string> {
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
        /// <summary>
        /// Reads a string content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override Option<string> Read(Stream input, Context context) {
            using (BinaryReader br = new BinaryReader(input)) {
                return Option.Some(br.ReadString());
            }
        }
    }
}