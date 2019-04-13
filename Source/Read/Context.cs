using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content.Read {
    /// <summary>
    /// Provides useful objects to read and initialize content correctly.
    /// </summary>
    public class Context {
        /// <summary>
        /// Initializes a Context class.
        /// </summary>
        public Context(GraphicsDevice graphicsDevice) {
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// The game's GraphicsDevice. Useful when creating new textures.
        /// </summary>
        public GraphicsDevice GraphicsDevice {
            get;
            set;
        }
    }
}