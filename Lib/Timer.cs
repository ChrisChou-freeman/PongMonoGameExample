using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPong
{
    public class Timer
    {
        public int timer;
        private SpriteFont _font;

        public Timer(SpriteFont font)
        {
            this._font = font;
        }

        string getTimerDescripe()
        {
            return string.Format($"Time: {this.timer.ToString()}");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this._font, this.getTimerDescripe(), new Vector2(0, 0), Color.White);
        }
    }
}