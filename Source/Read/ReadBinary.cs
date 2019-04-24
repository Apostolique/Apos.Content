using System;
using System.IO;
using Optional;

namespace Apos.Content.Read {
    /// <summary>
    /// A binary content is simply a byte array.
    /// </summary>
    public class ReadBinary : Reader<byte[]> {
        /// <summary>
        /// Reads a binary content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override Option<byte[]> Read(Stream input, Context context) {
            using (BinaryReader br = new BinaryReader(input)) {
                int count = br.ReadInt32();
                return Option.Some(br.ReadBytes(count));
            }
        }
    }
}