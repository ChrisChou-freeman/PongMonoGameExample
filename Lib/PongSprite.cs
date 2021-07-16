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

        #region Colloision
        protected bool IsCollectionLeft(PongSprite sprite)
        {
            return this.Rectangle.Right + this.velocity.X > sprite.Rectangle.Left &&
            this.Rectangle.Left < sprite.Rectangle.Left &&
            this.Rectangle.Bottom > sprite.Rectangle.Top &&
            this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsCollectionRight(PongSprite sprite)
        {
            return this.Rectangle.Left + this.velocity.X < sprite.Rectangle.Right &&
            this.Rectangle.Right > sprite.Rectangle.Right &&
            this.Rectangle.Bottom > sprite.Rectangle.Top &&
            this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsCollectionTop(PongSprite sprite)
        {
        return this.Rectangle.Bottom + this.velocity.Y > sprite.Rectangle.Top &&
            this.Rectangle.Top < sprite.Rectangle.Top &&
            this.Rectangle.Right > sprite.Rectangle.Left &&
            this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsCollectionBottom(PongSprite sprite)
        {
        return this.Rectangle.Top + this.velocity.Y < sprite.Rectangle.Bottom &&
            this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
            this.Rectangle.Right > sprite.Rectangle.Left &&
            this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
        
    }
    
    
}