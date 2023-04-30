using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Android
{
    internal class AboutScene : SceneHandler
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            spriteBatch = g.SpriteBatch;

            texture = g.Content.Load<Texture2D>("Images/Menu/About");
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
