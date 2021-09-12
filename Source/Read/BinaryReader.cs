using System.IO;

namespace Apos.Content.Read {
    /// <summary>
    /// A binary content is simply a byte array.
    /// </summary>
    public class BinaryReader : Reader<byte[]> {
        /// <summary>
        /// Reads a binary content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override byte[] Read(Stream input, Context context) {
            using (System.IO.BinaryReader br = new System.IO.BinaryReader(input)) {
                int count = br.ReadInt32();
                return br.ReadBytes(count);
            }
        }
    }
}
