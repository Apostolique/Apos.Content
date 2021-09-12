using Microsoft.Xna.Framework.Graphics;

namespace GameExample {
    public interface GameObject {
        void Update();
        void Draw(SpriteBatch s);
    }
}
