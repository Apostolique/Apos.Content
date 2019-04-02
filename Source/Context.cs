using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content {
    /// <summary>
    /// Provides useful objects to read and initialize content correctly.
    /// </summary>
    public class Context {
        public Context(GraphicsDevice graphicsDevice) {
            GraphicsDevice = graphicsDevice;
        }

        public GraphicsDevice GraphicsDevice {
            get;
            set;
        }
    }
}