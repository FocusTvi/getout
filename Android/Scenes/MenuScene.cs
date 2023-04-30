
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Android
{
    public class MenuScene : SceneHandler
    {
        private SpriteBatch spriteBatch;
        private MenuComponent menuComponent;
        private string[] menuItems = { "Start", "Help", "About", "Exit" };
        public MenuComponent MenuComponent { get => menuComponent; }

        public MenuScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            spriteBatch = g.SpriteBatch;
            SpriteFont font1 = g.Content.Load<SpriteFont>("Font/Menu");
            SpriteFont font2 = g.Content.Load<SpriteFont>("Font/Menu");
            Texture2D texture = g.Content.Load<Texture2D>("Images/Player/player_front");
            menuComponent = new MenuComponent(g, spriteBatch, font1, font2, 0, menuItems, texture);
            Components.Add(menuComponent);
        }

    }
}
