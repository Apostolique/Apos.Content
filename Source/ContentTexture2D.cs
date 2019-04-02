using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Optional;
using System.Drawing;

namespace Apos.Content {
    /// <summary>
    /// Builds and reads Texture2D content.
    /// </summary>
    public class ContentTexture2D : Content<Texture2D> {
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
        /// <summary>
        /// Reads a Texture2D content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override Option<Texture2D> Read(Stream input, Context context) {
            using (BinaryReader br = new BinaryReader(input)) {
                int width = br.ReadInt32();
                int height = br.ReadInt32();

                Texture2D texture = new Texture2D(context.GraphicsDevice, width, height);

                Microsoft.Xna.Framework.Color[] colors = new Microsoft.Xna.Framework.Color[width * height];

                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        byte r = br.ReadByte();
                        byte g = br.ReadByte();
                        byte b = br.ReadByte();
                        byte a = br.ReadByte();

                        colors[i + j * height] = new Microsoft.Xna.Framework.Color(r, g, b, a);
                    }
                }

                texture.SetData(colors);

                return Option.Some(texture);
            }
        }
    }
}