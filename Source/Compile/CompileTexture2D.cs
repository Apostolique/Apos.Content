using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using System.Drawing;

namespace Apos.Content.Compile {
    /// <summary>
    /// Builds and reads Texture2D content.
    /// </summary>
    public class CompileTexture2D : Compiler<Texture2D> {
        /// <summary>
        /// Builds a Texture2D content.
        /// </summary>
        public override void Build(Stream input, Stream output, Settings<Texture2D> settings) {
            using (Bitmap bitmap = new Bitmap(input))
            using (BinaryWriter bw = new BinaryWriter(output)) {
                bw.Write(bitmap.Width);
                bw.Write(bitmap.Height);
                for (int i = 0; i < bitmap.Width; i++) {
                    for (int j = 0; j < bitmap.Height; j++) {
                        Color c = bitmap.GetPixel(i, j);
                        bw.Write(c.R);
                        bw.Write(c.G);
                        bw.Write(c.B);
                        bw.Write(c.A);
                    }
                }
            }
        }
    }
}