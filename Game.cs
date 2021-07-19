using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoPong
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Vector2 originalScreenSize;
        private  Vector2 _setScreenSize;
        private Matrix _globalTransformation;
        private int _backbufferWidth;
        private int _backbufferHeight;
        private Score _score;
        private GamingTimer _gamingTimer;
        private Menu _menu;
        private List<PongSprite> _pongSprites;
        public static Random random;
        public static bool? BattelComputer;
        public static Ball ball;

        public Pong()
        {
            this._graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            originalScreenSize = new Vector2(800, 480);
            this._setScreenSize = new Vector2(1280, 720);
            random = new Random();
        }

        private void InitializeScreenSize()
        {
          this._graphics.IsFullScreen = false;
          this._graphics.PreferredBackBufferWidth = (int)this._setScreenSize.X;
          this._graphics.PreferredBackBufferHeight = (int)this._setScreenSize.Y;
          this._graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.InitializeScreenSize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var batTexture = Content.Load<Texture2D>("Bat");
            var ballTexture = Content.Load<Texture2D>("Ball");
            var backGround = Content.Load<Texture2D>("Background");
            this._spriteBatch = new SpriteBatch(GraphicsDevice);
            this._score = new Score(Content.Load<SpriteFont>("Font"));
            this._gamingTimer = new GamingTimer(Content.Load<SpriteFont>("TimerFont"));
            this._menu = new Menu(
                Content.Load<SpriteFont>("MenuFont"),
                new Input()
                {
                    Up = Keys.Up,
                    Down = Keys.Down
                });

            var leftBat =  new Bat(batTexture)
            {
                position = new Vector2(20, (originalScreenSize.Y / 2) - (batTexture.Height / 2)),
                input = new Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                }
            };
            var rightBat  = new Bat(batTexture)
            {
                position = new Vector2(originalScreenSize.X - 20 - batTexture.Width, (originalScreenSize.Y / 2) - (batTexture.Height / 2)),
                input = new Input()
                {
                    Up = Keys.Up,
                    Down = Keys.Down,
                }
            };
            ball = new Ball(ballTexture)
            {
                position = new Vector2((originalScreenSize.X / 2) - (ballTexture.Width / 2), (originalScreenSize.Y / 2) - (ballTexture.Height / 2)),
                score = this._score,
                gamingTimer = this._gamingTimer,
                pongHitTableSound = Content.Load<SoundEffect>("Sounds/Pong")
            };

            this._pongSprites = new List<PongSprite>()
            {
                new PongSprite(backGround),
                leftBat,
                rightBat,
                ball
            };
            // TODO: use this.Content to load your game content here
        }

        public void ScalePresentationArea()
        {
            //Work out how much we need to scale our graphics to fill the screen
            this._backbufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            this._backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float horScaling = this._backbufferWidth / originalScreenSize.X;
            float verScaling = this._backbufferHeight / originalScreenSize.Y;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            this._globalTransformation = Matrix.CreateScale(screenScalingFactor);
        }


        protected override void Update(GameTime gameTime)
        {
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //     Exit();
            
            if(this._backbufferHeight != GraphicsDevice.PresentationParameters.BackBufferHeight || 
                this._backbufferWidth!= GraphicsDevice.PresentationParameters.BackBufferWidth)
            {
                this.ScalePresentationArea();
            }
            foreach(var sprite in this._pongSprites)
                sprite.Update(gameTime, this._pongSprites);
            this._menu.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null,null, this._globalTransformation);
            foreach(var sprite in this._pongSprites)
                sprite.Draw(this._spriteBatch);
            this._score.Draw(this._spriteBatch);
            this._gamingTimer.Draw(this._spriteBatch);
            this._menu.Draw(this._spriteBatch);
            this._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
