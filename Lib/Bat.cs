using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoPong
{
    public class Bat: PongSprite
    {
        public Bat(Texture2D texture): base(texture)
        {
            this.speed = 0.5f;
        }

        public override void Update(GameTime gameTime, List<PongSprite> pongSprite)
        {
            if (this.input == null)
                throw new Exception("Please give a value to 'Input'");

            if (Keyboard.GetState().IsKeyDown(this.input.Up))
                this.velocity.Y -= this.speed;
            else if (Keyboard.GetState().IsKeyDown(this.input.Down))
                this.velocity.Y += this.speed;
            else if(Keyboard.GetState().IsKeyUp(this.input.Up) || Keyboard.GetState().IsKeyUp(this.input.Down) )
                this.velocity = Vector2.Zero;
            

            this.position += this.velocity;

            this.position.Y = Math.Clamp(this.position.Y, 0, Pong.originalScreenSize.Y - _texture.Height);
        }
    }
}