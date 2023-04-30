using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Android
{

    public class PlayComponent : DrawableGameComponent
    {
        public static GameState State = GameState.INMENU;
        public static int level = 1;
        public const int TIME_TO_INCREASE_RAGE = 10;


        private int ragesNeededToChangeLevel = 0;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera camera;

        private List<SpriteAnimation> enemiesTextures;
        private List<Texture2D> backgrounds;
        private Texture2D backg;
        private Texture2D fireballTexture;
        private Texture2D headerBg;
        private Player player;
        private SpriteFont font;
        private int oldTimer = 0;
        private float timer = 0;
        private int rage = 1;
        private int enemiesSpriteIndex = 0;


        public PlayComponent(Game game,
            GraphicsDeviceManager graphics,
            SpriteBatch spriteBatch,
            List<Texture2D> backgrounds,
            Texture2D headerBg,
            Texture2D fireballTexture,
            List<SpriteAnimation> enemiesTextures,
            Player player,
            SpriteFont font
            ) : base(game)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            this.backgrounds = backgrounds;
            this.fireballTexture = fireballTexture;
            this.enemiesTextures = enemiesTextures;
            this.headerBg = headerBg;
            this.player = player;
            this.font = font;

            this.backg = backgrounds.FirstOrDefault();
            this.ragesNeededToChangeLevel = enemiesTextures.Count;
            camera = new Camera();
        }

        public override void Update(GameTime gameTime)
        {
            //if player is dead the don't do anything
            if (!player.IsAlive)
            {
                return;
            }

            player.Update(gameTime);

            //Controlling the texture of creatures
            /*        if (enemiesTextures.Count >= rage)
                    {
                        CreationController.Update(gameTime, enemiesTextures.ElementAt(rage - 1));
                    }
                    else
                    {
                        CreationController.Update(gameTime, enemiesTextures.ElementAt(enemiesTextures.Count() - 1));
                    } */




            //Updating the position of the camera
            camera.Follow(player);

            if (this.enemiesSpriteIndex == ragesNeededToChangeLevel)
                this.enemiesSpriteIndex--;

            CreationController.Update(gameTime, enemiesTextures.ElementAt(enemiesSpriteIndex));

            foreach (Fireball f in Fireball.Fireballs)
            {

                f.Update(gameTime);
            }

            foreach (Enemy e in Enemy.Enemies)
            {
                e.Update(gameTime, player.Position);
                if (player.Rectangle.Intersects(e.Rectangle))
                {
                    player.IsAlive = false;
                    Game1.sounds[SfxNames.PLAYER_SCREAM].Play();
                    Game1.sounds[SfxNames.ENEMY_LAUGH].Play();
                    Enemy.Restart();
                    Fireball.Fireballs.Clear();
                    PlayComponent.level = 1;
                    Game1.restartGame = true;
                    return;

                }
                //int minSeparation = e.Radius + player.Radius;

                //if (Vector2.Distance(e.Position, player.Position) < minSeparation)
                //{
                //    player.IsAlive = false;
                //    Enemy.Restart();
                //    Game1.restartGame = true;
                //    return;
                //}
            }


            foreach (Fireball f in Fireball.Fireballs)
            {
                foreach (Enemy e in Enemy.Enemies)
                {
                    if (!f.Hit)
                    {
                        //int minSeparation = f.rad + e.radius;

                        //if (Vector2.Distance(f.Position, e.Position) < minSeparation)
                        //{
                        //    e.HitTimes--;
                        //    f.Hit = true;
                        //    if (e.HitTimes == 0)
                        //    {
                        //        e.Alive = false;
                        //        player.Kills++;
                        //    }
                        //} 

                        if (e.Rectangle.Contains(f.Position))
                        {
                            var impactSound = Game1.impactSound.CreateInstance();
                            impactSound.Stop();
                            impactSound.Play();
                            e.HitTimes--;
                            f.Hit = true;
                            if (e.HitTimes == 0)
                            {
                                e.Alive = false;
                                player.Kills++;
                            }
                        }

                    }

                }
            }
            Fireball.Fireballs.RemoveAll(p => p.Hit);
            Enemy.Enemies.RemoveAll(p => !p.Alive);


            //select the background according to the level
            //if (PlayComponent.level < backgrounds.Count)
            //{
            //    backg = backgrounds[PlayComponent.level - 1];
            //}
            //else
            //{
            //    backg = backgrounds[backgrounds.Count - 1];
            //}


            if (backgrounds.Count >= PlayComponent.level)
            {
                this.backg = backgrounds.ElementAt(PlayComponent.level - 1);
            }
            else
            {
                this.backg = backgrounds.ElementAt(backgrounds.Count() - 1);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (PlayComponent.State == GameState.INMENU)
            {
                return;
            }
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!player.IsAlive)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(font, "Game Over", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), Color.Black);


                _spriteBatch.End();
                base.Draw(gameTime);
                return;
            }
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            _spriteBatch.Begin(transformMatrix: camera.Transform);

            //Drawing the background

            _spriteBatch.Draw(backg, new Vector2(0, 0), Color.White);

            //Drawing the score bar
            string textHeader = $"Kills: {player.Kills} - Timer: {(int)timer} - Level: {PlayComponent.level} - Rage: {this.rage} - Enemies Alive: {Enemy.Enemies.Where(e => e.Alive).Count()}";
            _spriteBatch.Draw(headerBg, new Vector2(player.Position.X - 600, player.Position.Y - 400), Color.White);
            _spriteBatch.DrawString(font, textHeader, new Vector2(player.Position.X - 550, player.Position.Y - 400), Color.White);

            //Drawing the player
            player.Animation.Draw(_spriteBatch);


            //Drawing the fireballs
            foreach (Fireball f in Fireball.Fireballs)
            {
                _spriteBatch.Draw(fireballTexture, f.Position, Color.White);
            }

            //Drawing the enemies
            foreach (Enemy e in Enemy.Enemies)
            {
                //this is to increase the power and the number of hits to kill the enemies
                if ((int)timer % TIME_TO_INCREASE_RAGE == 0 && (int)timer != oldTimer)
                {
                    oldTimer = (int)timer;
                    this.IncreaseLevel();
                }
                e.animation.Draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //Function to increase the level of the enemies
        private void IncreaseLevel()
        {
            this.rage++;
            this.enemiesSpriteIndex++;

            if ((this.rage - 1) % ragesNeededToChangeLevel == 0)
            {
                PlayComponent.level++;
                Random rand = new Random();
                int r = rand.Next(256);
                int g = rand.Next(256);
                int b = rand.Next(256);
                Enemy.color = new Color(r, g, b);
                Enemy.globalSpeed += 10;
                Enemy.animationSpeed += 2;
                Enemy.hitsNeeded++;
                this.enemiesSpriteIndex = 0;
            }

        }
    }
}
