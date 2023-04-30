using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Android
{
    public class Fireball
    {
        public static List<Fireball> Fireballs = new List<Fireball>();
        private Vector2 position;
        private Vector2 mousePosition;
        private int speed = 1000;
        public int rad = 18;
        private Movement movement;
        private bool hit = false;
        public Vector2 Position { get => position; }
        public bool Hit { get => hit; set => hit = value; }
        public SpriteAnimation animation { get; set; }
        private string inputType;

        public Fireball(Vector2 position, Movement movement, string inputType, Point mousePosition)
        {
            this.position = position;
            this.movement = movement;
            this.mousePosition = mousePosition.ToVector2();
            this.inputType = inputType;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (inputType == "mouse")
            {
                //TODO
                //Vector2 moveDir = new Vector2(0,0) - position;
                //moveDir.Normalize();
                //position += moveDir ;
            }
            else
            {
                switch (movement)
                {
                    case Movement.UP:
                        position.Y += speed * dt;
                        break;
                    case Movement.DOWN:
                        position.Y -= speed * dt;
                        break;
                    case Movement.LEFT:
                        position.X -= speed * dt;
                        break;
                    case Movement.RIGHT:
                        position.X += speed * dt;
                        break;

                }
            }


        }

    }
}
