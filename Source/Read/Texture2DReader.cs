using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content.Read {
    /// <summary>
    /// Reads Texture2D content.
    /// </summary>
    public class Texture2DReader : Reader<Texture2D> {
        /// <summary>
        /// Reads a Texture2D content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override Texture2D Read(Stream input, Context context) {
            using (System.IO.BinaryReader br = new System.IO.BinaryReader(input)) {
                int width = br.ReadInt32();
                int height = br.ReadInt32();

                Texture2D texture = new Texture2D(context.GraphicsDevice, width, height);

                Color[] colors = new Color[width * height];

                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        byte r = br.ReadByte();
                        byte g = br.ReadByte();
                        byte b = br.ReadByte();
                        byte a = br.ReadByte();

                        colors[i + j * width] = new Color(r, g, b, a);
                    }
                }

                texture.SetData(colors);

                return texture;
            }
        }
    }
}
