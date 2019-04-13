using System;
using System.IO;
using Optional;

namespace Apos.Content.Read {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class ReadString : Reader<string> {
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