using Microsoft.Xna.Framework.Graphics;

namespace Apos.Content {
    public class Context {
        public Context(Target target, GraphicsDevice graphicsDevice) {
            Target = target;
            GraphicsDevice = graphicsDevice;
        }

        public Target Target {
            get;
            set;
        }
        public GraphicsDevice GraphicsDevice {
            get;
            set;
        }
    }
}