using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content.Compile {
    /// <summary>
    /// Settings for Texture2D content.
    /// </summary>
    public class SettingsTexture2D : Settings<Texture2D> {
        public SettingsTexture2D(Target target) : base(target) { }

        public bool IsPremultipliedAlpha {
            get;
            set;
        } = true;
    }
}