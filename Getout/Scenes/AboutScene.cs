using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Getout.Scenes
{
    internal class HelpScene : SceneHandler
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            spriteBatch = g.SpriteBatch;

            texture = g.Content.Load<Texture2D>("Images/Menu/Help");
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
