using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameExample {
    public class Loading {
        public Loading() {

        }

        public void Update() {

        }
        public void Draw(SpriteBatch s) {
            s.GraphicsDevice.Clear(Color.Black);
            s.Begin();
            s.Draw(Assets.LoadingImage, Vector2.Zero, Color.White);
            s.End();
        }
    }
}