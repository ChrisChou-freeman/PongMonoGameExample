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
        private bool _isPlaying;
        public Score score;
        public int SpeedIncrementSpan;

        public Ball(Texture2D texture) : base(texture)
        {
            this.SpeedIncrementSpan = 20;
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
            this._isPlaying = false;
        }

        public override void Update(GameTime gameTime, List<PongSprite> pongSprite)
        {
            if(this._startPosition == null)
            {
                this._startPosition = this.position;
                this._startSpeed = this.speed;
                this.Restart();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                this._isPlaying = true;
            
            if(!_isPlaying)
                return;
            
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