using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameExample {
    public class Loading : GameObject {
        public Loading() { }

        public void Update() { }
        public void Draw(SpriteBatch s) {
            s.Draw(Assets.LoadingImage, Vector2.Zero, Color.White);
        }
    }
}