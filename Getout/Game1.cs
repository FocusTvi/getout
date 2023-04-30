using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Comora;
using System.Collections.Generic;
using System;
using System.Reflection;
using Getout.Scenes;
using Getout.Components;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Getout.Models;

namespace Getout
{

    public enum Movement
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        IDLE
    }

    public enum SfxNames
    {
        PLAYER_SCREAM,
        ENEMY_LAUGH,
        PLAYING_BACKGROUND,
        IMPACT,
        BREATH,
        MENUMOVEMENT,
        MENU,
        HIT
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MenuScene _menuScene;
        private HelpScene _helpScene;
        private PlayScene _startScene;
        private AboutScene _aboutScene;


        public static Dictionary<SfxNames, SoundEffectInstance> sounds = new Dictionary<SfxNames, SoundEffectInstance>();

        public static bool restartGame = false;
        public static string gameName = "Get Out";
        public static SoundEffect menuMovementSound;
        public static SoundEffect impactSound;
        public static List<Score> highScores = new List<Score>();

        public SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }
        public GraphicsDeviceManager Graphics { get => _graphics; set => _graphics = value; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //_graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferWidth = 1280;
            //_graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferHeight = 840;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            SharedVars.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            //According to the custom background image
            SharedVars.movementStage = new Rectangle(745, 730, 1270, 1230);

            base.Initialize();

        }

        private void hideAllScenes()
        {
            foreach (SceneHandler scene in Components)
                scene.hide();

        }

        private void clearScreen()
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            _startScene = new PlayScene(this);
            _menuScene = new MenuScene(this);
            _helpScene = new HelpScene(this);
            _aboutScene = new AboutScene(this);

            this.Components.Add(_menuScene);
            this.Components.Add(_helpScene);
            this.Components.Add(_startScene);
            this.Components.Add(_aboutScene);


            sounds.Add(SfxNames.PLAYING_BACKGROUND, this.Content.Load<SoundEffect>("Sounds/Suspense").CreateInstance());
            sounds.Add(SfxNames.PLAYER_SCREAM, this.Content.Load<SoundEffect>("Sounds/Scream").CreateInstance());
            sounds.Add(SfxNames.ENEMY_LAUGH, this.Content.Load<SoundEffect>("Sounds/EnemyLaugh").CreateInstance());
            sounds.Add(SfxNames.HIT, this.Content.Load<SoundEffect>("Sounds/Hit").CreateInstance());
            sounds.Add(SfxNames.IMPACT, this.Content.Load<SoundEffect>("Sounds/Impact").CreateInstance());
            sounds.Add(SfxNames.MENU, this.Content.Load<SoundEffect>("Sounds/Menu").CreateInstance());
            sounds.Add(SfxNames.MENUMOVEMENT, this.Content.Load<SoundEffect>("Sounds/MenuMovement").CreateInstance());
            Game1.menuMovementSound = this.Content.Load<SoundEffect>("Sounds/MenuMovement");
            Game1.impactSound = this.Content.Load<SoundEffect>("Sounds/Impact");
            sounds.Add(SfxNames.BREATH, this.Content.Load<SoundEffect>("Sounds/Breath").CreateInstance());
            sounds[SfxNames.PLAYING_BACKGROUND].IsLooped = true;
            sounds[SfxNames.MENU].IsLooped = true;
            sounds[SfxNames.BREATH].IsLooped = true;
            sounds[SfxNames.MENU].Volume = 0.1f;



            hideAllScenes();
            _menuScene.show();
        }

        protected override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            int index = 0;
            if (_menuScene.Enabled)
            {
                PlayComponent.State = GameState.INMENU;

                sounds[SfxNames.PLAYING_BACKGROUND].Pause();
                sounds[SfxNames.BREATH].Play();
                sounds[SfxNames.MENU].Play();

                index = _menuScene.MenuComponent._SelectedIndex;

                if (index == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    Exit();
                }
                else if (index == 0 && ks.IsKeyDown(Keys.Enter))
                {

                    PlayComponent.State = GameState.INGAME;

                    sounds[SfxNames.PLAYING_BACKGROUND].Play();
                    sounds[SfxNames.BREATH].Stop();
                    sounds[SfxNames.MENU].Stop();

                    if (Game1.restartGame)
                    {
                        //clearScreen();
                        this.Components.Remove(_startScene);
                        _startScene = new PlayScene(this);
                        this.Components.Add(_startScene);
                        Game1.restartGame = false;
                        _startScene.hide();
                        _menuScene.show();
                    }
                    else
                    {

                        _menuScene.hide();
                        _startScene.show();
                    }
                }
                else if (index == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    _menuScene.hide();
                    _helpScene.show();
                }
                else if (index == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    _menuScene.hide();
                    _aboutScene.show();
                }


            }
            else if (ks.IsKeyDown(Keys.Escape))
            {
                // hide for all other scenes
                //_startScene.hide();
                this.clearScreen();
                this.hideAllScenes();
                //this.Components.Remove(_startScene);
                _menuScene.show();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}