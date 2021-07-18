using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPong
{
    public class GamingTimer
    {
        public TimeSpan timer;
        private SpriteFont _font;

        public GamingTimer(SpriteFont font)
        {
            this._font = font;
        }

        string getTimerDescripe()
        {
            return string.Format($"Time: {this.timer.Minutes.ToString("00")} : {this.timer.Seconds.ToString("00")}");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this._font, this.getTimerDescripe(), new Vector2(0, 0), Color.White);
        }
    }
}