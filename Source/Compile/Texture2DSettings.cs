using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content.Compile {
    /// <summary>
    /// Settings for Texture2D content.
    /// </summary>
    public class Texture2DSettings : Settings<Texture2D> {
        /// <summary>
        /// Initializes a new SettingsTexture2D class to help build Texture2D.
        /// </summary>
        public Texture2DSettings(Target target) : base(target) { }

        /// <summary>
        /// Premultiplied alpha is used in graphics rendering because it gives better results than straight alpha when filtering images or composing different layers.
        /// </summary>
        public bool IsPremultipliedAlpha { get; set; } = true;
    }
}
