using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoPong
{
    public class PongSprite
    {
        protected Texture2D _texture;
        public Vector2 position;
        public Vector2 velocity;
        public float speed;
        public Input input;
        
        public PongSprite(Texture2D texture)
        {
            this._texture = texture;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    this._texture.Width,
                    this._texture.Height);
            }
        }

        public virtual void Update(GameTime gameTime, List<PongSprite> pongSprite){}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, this.position, Color.White);
        }
        
    }
    
    
}