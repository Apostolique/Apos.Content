using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content.Read {
    /// <summary>
    /// Provides useful objects to read and initialize content correctly.
    /// </summary>
    public class Context {
        /// <summary>
        /// Initializes a Context class.
        /// </summary>
        public Context(string buildPath, GraphicsDevice graphicsDevice) {
            BuildPath = buildPath;
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// The path where the content should be read from.
        /// </summary>
        public string BuildPath {
            get;
            set;
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