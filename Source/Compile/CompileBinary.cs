using System;
using System.IO;
using Optional;

namespace Apos.Content.Compile {
    /// <summary>
    /// A binary content only contains bytes.
    /// </summary>
    public class CompileBinary : Compiler<byte[], Settings<byte[]>> {
        /// <summary>
        /// Builds a binary content.
        /// </summary>
        public override void Build(Stream input, Stream output, Settings<byte[]> settings) {
            using (BinaryWriter bw = new BinaryWriter(output))
            using (MemoryStream ms = new MemoryStream()) {
                input.CopyTo(ms);
                byte[] bytes = ms.ToArray();
                bw.Write(bytes.Length);
                bw.Write(bytes);
            }
        }
    }
}