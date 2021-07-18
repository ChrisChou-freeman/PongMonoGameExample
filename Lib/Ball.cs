using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoPong
{
    public class Ball: PongSprite
    {
        private float _timer;
        private Vector2? _startPosition;
        private float? _startSpeed;
        public bool isPlaying;
        public Score score;
        public int SpeedIncrementSpan;
        public GamingTimer gamingTimer;

        public Ball(Texture2D texture) : base(texture)
        {
            this.SpeedIncrementSpan = 15;
            this.speed = 2f;
        }

        public void Restart()
        {
            var direction = Pong.random.Next(0, 4);

            switch (direction)
            {
                case 0:
                this.velocity = new Vector2(1, 1);
                break;
                case 1:
                this.velocity = new Vector2(1, -1);
                break;
                case 2:
                this.velocity = new Vector2(-1, -1);
                break;
                case 3:
                this.velocity = new Vector2(-1, 1);
                break;
            }

            this.position = (Vector2)_startPosition;
            this.speed = (float)_startSpeed;
            this._timer = 0;
            this.gamingTimer.timer = TimeSpan.Zero;
            this.isPlaying = false;
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

        public override void Update(GameTime gameTime, List<PongSprite> pongSprite)
        {
            if(this._startPosition == null)
            {
                this._startPosition = this.position;
                this._startSpeed = this.speed;
                this.Restart();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && Pong.BattelComputer != null)
                this.isPlaying = true;
            
            if(!isPlaying)
                return;
            
            this.gamingTimer.timer += gameTime.ElapsedGameTime;
            
            this._timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(this._timer > this.SpeedIncrementSpan)
            {
                this.speed++;
                this._timer = 0;
            }

            foreach(var sprite in pongSprite)
            {
                if(sprite == this)
                    continue;
                if(this.velocity.X > 0 && this.IsCollectionLeft(sprite))
                    this.velocity.X = -this.velocity.X;
                if (this.velocity.X < 0 && this.IsCollectionRight(sprite))
                    this.velocity.X = -this.velocity.X;
                if (this.velocity.Y > 0 && this.IsCollectionTop(sprite))
                    this.velocity.Y = -this.velocity.Y;
                if (this.velocity.Y < 0 && this.IsCollectionBottom(sprite))
                    this.velocity.Y = -this.velocity.Y;                
            }

            
            if (this.position.Y <= 0 || this.position.Y + _texture.Height >= Pong.originalScreenSize.Y)
                velocity.Y = -velocity.Y;

            if (this.position.X <= 0)
            {
                this.score.Score2++;
                this.Restart();
            }

            if (this.position.X + _texture.Width >= Pong.originalScreenSize.X)
            {
                this.score.Score1++;
                this.Restart();
            }

            this.position += this.velocity * this.speed;   
        }

    }
}