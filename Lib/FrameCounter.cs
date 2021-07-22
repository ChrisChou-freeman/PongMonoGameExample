using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

public class FrameCounter
{

    private long _totalFrames;
    private float _totalSeconds;
    private float _averageFramesPerSecond;
    private float _currentFramesPerSecond;
    private SpriteFont _font;
    private const int _MAXIMUM_SAMPLES = 100;
    private Queue<float> _sampleBuffer = new Queue<float>();

    public FrameCounter(SpriteFont spriteFont)
    {
        this._font = spriteFont;
    }

    public void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _currentFramesPerSecond = 1.0f / deltaTime;

        _sampleBuffer.Enqueue(_currentFramesPerSecond);

        if (_sampleBuffer.Count > _MAXIMUM_SAMPLES)
        {
            _sampleBuffer.Dequeue();
            this._averageFramesPerSecond = _sampleBuffer.Average(i => i);
        } 
        else
        {
            this._averageFramesPerSecond = _currentFramesPerSecond;
        }

        _totalFrames++;
        _totalSeconds += deltaTime;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var fps = string.Format("FPS: {0}", Math.Round(this._averageFramesPerSecond));
        spriteBatch.DrawString(this._font, fps, new Vector2(200, 0), Color.White);
    }
}