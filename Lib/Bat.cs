using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public enum computerSeepd
{
    speed1 = 1,
    speed2 = 2,
    speed3 = 4,
}

namespace MonoPong
{
    public class Bat: PongSprite
    {
        private int _adjustValue = 30;

        public Bat(Texture2D texture): base(texture)
        {
            this.speed = 0.5f;
        }

        private void handleWithKey()
        {
            if (Keyboard.GetState().IsKeyDown(this.input.Up))
            this.velocity.Y -= this.speed;
            else if (Keyboard.GetState().IsKeyDown(this.input.Down))
                this.velocity.Y += this.speed;
            else if(Keyboard.GetState().IsKeyUp(this.input.Up) || Keyboard.GetState().IsKeyUp(this.input.Down) )
                this.velocity = Vector2.Zero;
        }

        private bool inBatRange()
        {
            return Pong.ball.Rectangle.Top > this.Rectangle.Top + this._adjustValue && Pong.ball.Rectangle.Top < this.Rectangle.Bottom - this._adjustValue;
        }

        private void handleWithConputer()
        {   
            float speed = (float)computerSeepd.speed2;

            // ball go left
            if(Pong.ball.isPlaying
            && Pong.ball.velocity.X < 0)
            {
                if(!this.inBatRange())
                {
                    if(this.Rectangle.Top < Pong.ball.Rectangle.Top && this.Rectangle.Bottom < Pong.ball.Rectangle.Top)
                        this.velocity.Y += speed;
                    else if (this.Rectangle.Top > Pong.ball.Rectangle.Top && this.Rectangle.Bottom > Pong.ball.Rectangle.Top)
                    {
                        this.velocity.Y -= speed;
                    }
                }else
                {
                    this.velocity = Vector2.Zero;
                }
                
            }
        }

        public override void Update(GameTime gameTime, List<PongSprite> pongSprite)
        {
            // Console.WriteLine(this.Rectangle.Bottom-this.Rectangle.Y);
            if (this.input == null)
                throw new Exception("Please give a value to 'Input'");
            
            if(Pong.BattelComputer == null)           
                return;
            
            if(this.input.Up == Keys.W && Pong.BattelComputer == true)
            {
                this.handleWithConputer();
            }else
            {
                this.handleWithKey();
            }
                

            this.position += this.velocity;

            this.position.Y = Math.Clamp(this.position.Y, 0, Pong.originalScreenSize.Y - _texture.Height);
        }
    }
}