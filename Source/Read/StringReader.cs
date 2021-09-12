using System.IO;

namespace Apos.Content.Read {
    /// <summary>
    /// A string content is simply a file with text.
    /// </summary>
    public class StringReader : Reader<string> {
        /// <summary>
        /// Reads a string content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override string Read(Stream input, Context context) {
            using (System.IO.BinaryReader br = new System.IO.BinaryReader(input)) {
                return br.ReadString();
            }
        }
    }
}
