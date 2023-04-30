using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;

namespace Getout
{

    public class SpriteManager
    {
        public Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;
        public int Frames;
        public int Width;

        public SpriteManager(Texture2D texture, int frames, int scale)
        {
            this.init(texture, frames, scale);
        }

        public void init(Texture2D texture, int frames, int scale)
        {
            Frames = frames;
            this.Texture = texture;
            Width = Texture.Width / Frames;
            Rectangles = new Rectangle[Frames];
            Scale = scale;

            for (int i = 0; i < Frames; i++)
            {
                Rectangles[i] = new Rectangle(i * Width, 0, Width, Texture.Height);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }

    public class SpriteAnimation : SpriteManager, ICloneable
    {
        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate; //default, you may have to change it
        public int FramesPerSecond { set { timeToUpdate = (1f / value); } }

        public SpriteAnimation(Texture2D Texture, int frames, int fps, int scale = 1) : base(Texture, frames, scale)
        {
            FramesPerSecond = fps;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;

                else if (IsLooping)
                    FrameIndex = 0;
            }
        }

        public void setFrame(int frame)
        {
            FrameIndex = frame;
        }

        public void setInit(Texture2D texture, int frames, int scale)
        {
            if (Texture != texture)
            {
                FrameIndex = 1;
            }
            base.init(texture, frames, scale);
        }

        public void setColor(Color color)
        {
            this.Color = color;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}