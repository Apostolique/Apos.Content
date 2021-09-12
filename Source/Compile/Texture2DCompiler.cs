using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Apos.Content.Compile {
    /// <summary>
    /// Compiles images into Texture2D content.
    /// </summary>
    public class Texture2DCompiler : Compiler<Texture2D, Texture2DSettings> {
        /// <summary>
        /// Builds a Texture2D content.
        /// </summary>
        public override void Build(Stream input, Stream output, Texture2DSettings settings) {
            using (Bitmap bitmap = new Bitmap(input))
            using (BinaryWriter bw = new BinaryWriter(output)) {
                bw.Write(bitmap.Width);
                bw.Write(bitmap.Height);
                for (int i = 0; i < bitmap.Width; i++) {
                    for (int j = 0; j < bitmap.Height; j++) {
                        Color c = bitmap.GetPixel(i, j);

                        if (settings.IsPremultipliedAlpha) {
                            c = ToPremultiplyAlpha(c);
                        }

                        bw.Write(c.R);
                        bw.Write(c.G);
                        bw.Write(c.B);
                        bw.Write(c.A);
                    }
                }
            }
        }

        private Color ToPremultiplyAlpha(Color c) {
            return Color.FromArgb(c.A, c.R * c.A / 255, c.G * c.A / 255, c.B * c.A / 255);
        }
    }
}
