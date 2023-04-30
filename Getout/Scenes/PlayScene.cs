using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Getout.Components;
using Getout.Models;

namespace Getout.Scenes
{

    public enum GameState
    {
        INMENU,
        INGAME,
    }

    public class PlayScene : SceneHandler
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private Dictionary<Movement, Texture2D> playerTextures;
        private Dictionary<Movement, int> playerTexturesFrames;


        private Texture2D fireballTexture;
        private Texture2D headerBg;
        private Player player;
        private SpriteFont font;

        private List<SpriteAnimation> textures = new List<SpriteAnimation>();

        public PlayScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;

            spriteBatch = g.SpriteBatch;
            graphics = g.Graphics;

            headerBg = g.Content.Load<Texture2D>("Images/Background/headerbg");
            fireballTexture = g.Content.Load<Texture2D>("Images/fireball");
            textures.Add(new SpriteAnimation(g.Content.Load<Texture2D>("Images/Enemies/skull"), 10, 3, 1));
            textures.Add(new SpriteAnimation(g.Content.Load<Texture2D>("Images/Enemies/skell_walk"), 13, 3, 4));
            textures.Add(new SpriteAnimation(g.Content.Load<Texture2D>("Images/Enemies/bat"), 2, 3, 2));
            playerTextures = new Dictionary<Movement, Texture2D>()
            {
                {Movement.UP, g.Content.Load<Texture2D>("Images/Player/player_walkup") },
                {Movement.DOWN, g.Content.Load<Texture2D>("Images/Player/player_walkdown") },
                {Movement.LEFT, g.Content.Load<Texture2D>("Images/Player/player_walkleft3") },
                {Movement.RIGHT, g.Content.Load<Texture2D>("Images/Player/player_walkright3") },
            };
            playerTexturesFrames = new Dictionary<Movement, int>()
            {
                {Movement.UP, 5 },
                {Movement.DOWN, 5 },
                {Movement.LEFT, 4 },
                {Movement.RIGHT, 4 },
            };

            List<Texture2D> backgrounds = new List<Texture2D>();

            backgrounds.Add(g.Content.Load<Texture2D>("Images/Background/background"));
            backgrounds.Add(g.Content.Load<Texture2D>("Images/Background/background2"));
            backgrounds.Add(g.Content.Load<Texture2D>("Images/Background/background3"));

            font = g.Content.Load<SpriteFont>("Font/Header");


            Texture2D rectangle = new Texture2D(graphics.GraphicsDevice, 950, 50);

            Color[] data = new Color[950 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.FromNonPremultiplied(54 ,31 ,75, 200);
            rectangle.SetData(data);

            this.player = new Player(playerTextures, playerTexturesFrames);
            player.Animation = new SpriteAnimation(playerTextures[Movement.DOWN], playerTexturesFrames[Movement.DOWN], 4, 2);

            PlayComponent playComponent = new PlayComponent(game, graphics, spriteBatch, backgrounds, rectangle, fireballTexture, textures, player, font);

            this.Components.Add(playComponent);


        }
    }
}
