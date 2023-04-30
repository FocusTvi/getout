using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Getout.Models
{
    public class Player
    {
        private Vector2 position;
        private SpriteAnimation animation;
        private bool isMoving = false;
        private float speed = 500f;
        private Movement movement = Movement.DOWN;
        private KeyboardState ksOld = Keyboard.GetState();
        private int kills = 0;
        private bool isAlive = true;
        public readonly int Radius = 15;

        public Rectangle Rectangle { get; set; }
        public Vector2 Position { get => position; set => position = value; }
        public SpriteAnimation Animation { get => animation; set => animation = value; }
        public int Kills { get => kills; set => kills = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private Dictionary<Movement, Texture2D> playerTextures;
        private Dictionary<Movement, int> playerTexturesFrames;
        public Player(Dictionary<Movement, Texture2D> playerTextures, Dictionary<Movement, int> playerTexturesFrames)
        {
            this.position = new Vector2(SharedVars.movementStage.X, SharedVars.movementStage.Y);
            this.playerTextures = playerTextures;
            this.playerTexturesFrames = playerTexturesFrames;
            //this.Rectangle = new Rectangle((int)position.X, (int)position.Y, animation.Texture.Width, animation.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            isMoving = false;

            Vector2 tentativeMovement = position;


            if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {

                tentativeMovement.Y += speed * dt;
                if (!SharedVars.movementStage.Contains(tentativeMovement))
                {
                    return;
                }

                position.Y += speed * dt;
                isMoving = true;
                movement = Movement.UP;
                Animation.setInit(playerTextures[Movement.DOWN], playerTexturesFrames[Movement.DOWN], 2);

            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                tentativeMovement.Y -= speed * dt;
                if (!SharedVars.movementStage.Contains(tentativeMovement))
                {
                    return;
                }
                position.Y -= speed * dt;
                isMoving = true;
                movement = Movement.DOWN;
                Animation.setInit(playerTextures[Movement.UP], playerTexturesFrames[Movement.UP], 2);
            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                tentativeMovement.X -= speed * dt;
                if (!SharedVars.movementStage.Contains(tentativeMovement))
                {
                    return;
                }
                position.X -= speed * dt;
                isMoving = true;
                movement = Movement.LEFT;
                Animation.setInit(playerTextures[Movement.LEFT], playerTexturesFrames[Movement.LEFT], 2);
            }
            else if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                tentativeMovement.X += speed * dt;
                if (!SharedVars.movementStage.Contains(tentativeMovement))
                {
                    return;
                }
                position.X += speed * dt;
                isMoving = true;
                movement = Movement.RIGHT;
                Animation.setInit(playerTextures[Movement.RIGHT], playerTexturesFrames[Movement.RIGHT], 2);
            }

            if (isMoving)
            {
                Animation.Update(gameTime);
            }
            else
            {
                Animation.setFrame(1);
            }

            Animation.Position = position;
            //Animation.Position = new Vector2(position.X - 12.5f, position.Y - 19);

            Rectangle = new Rectangle((int)position.X, (int)position.Y, 25, 38);

            if ((ks.IsKeyDown(Keys.Space) && ksOld.IsKeyUp(Keys.Space)))
            {
                //the position changes because of the texture size
                Fireball.Fireballs.Add(new Fireball(new Vector2(Position.X + (animation.Texture.Width / (2 * animation.Frames)), Position.Y + animation.Texture.Height / 2), movement, "keyboard", new Point(0, 0)));

                Game1.sounds[SfxNames.HIT].Stop();
                Game1.sounds[SfxNames.HIT].Play();
                Game1.sounds[SfxNames.HIT].Volume = 1f;
            }
            else if (ms.LeftButton == ButtonState.Pressed)
            {
                //TODO
                //Fireball.Fireballs.Add(new Fireball(new Vector2(Position.X + (animation.Texture.Width / (2 * animation.Frames)), Position.Y + animation.Texture.Height / 2), movement,"mouse",  ms.Position));

                //Game1.sounds[SfxNames.HIT].Stop();
                //Game1.sounds[SfxNames.HIT].Play();
                //Game1.sounds[SfxNames.HIT].Volume = 1f;
            }
            ksOld = ks;
        }



    }
}
