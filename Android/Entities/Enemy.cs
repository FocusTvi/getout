using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Android
{
    public class Enemy
    {
        //Static Fields
        public static List<Enemy> Enemies = new List<Enemy>();
        public static Color color = Color.White;
        public static int globalSpeed = 150;
        public static int animationSpeed = 6;
        public static int hitsNeeded = 1;

        //Class fields
        private Vector2 position = new Vector2(0, 0);
        public SpriteAnimation animation;
        public int radius = 40;
        private bool alive = true;
        private int hitTimes = 1;
        private int speed = 150;
        public readonly int Radius = 15;

        //Properties
        public Rectangle Rectangle { get; set; }
        public Vector2 Position { get => position; }
        public bool Alive { get => alive; set => alive = value; }
        public int HitTimes { get => hitTimes; set => hitTimes = value; }

        //Constructor
        public Enemy(Vector2 position, SpriteAnimation animation)
        {
            this.position = position;
            this.animation = (SpriteAnimation)animation.Clone();
            this.animation.FramesPerSecond = animationSpeed;
            this.animation.Color = color;
            HitTimes = hitsNeeded;
            speed = globalSpeed;
            //this.Rectangle = new Rectangle((int)position.X, (int)position.Y, animation.Texture.Width, animation.Texture.Height);
        }



        public static void Restart()
        {
            Enemies = new List<Enemy>();
            color = Color.White;
            globalSpeed = 150;
            animationSpeed = 6;
            hitsNeeded = 1;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            animation.Position = new Vector2(position.X, position.Y);
            //animation.Position = new Vector2(position.X - 48, position.Y - 66);
            animation.Update(gameTime);
            //Setting the rectagle to handle the collision
            Rectangle = new Rectangle((int)position.X, (int)position.Y, animation.Width* (int)animation.Scale, animation.Texture.Height * (int)animation.Scale);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 moveDir = playerPosition - position;
            moveDir.Normalize();
            position += moveDir * speed * dt;
        }
    }
}
