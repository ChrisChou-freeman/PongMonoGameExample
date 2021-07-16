using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPong
{
    public class Score
    {
        public int Score1;
        public int Score2;
        private SpriteFont _font;

        public Score(SpriteFont font)
        {
            this._font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this._font, Score1.ToString(), new Vector2(320, 70), Color.White);
            spriteBatch.DrawString(this._font, Score2.ToString(), new Vector2(430, 70), Color.White);
        }
    }
}