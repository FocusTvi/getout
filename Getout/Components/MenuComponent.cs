using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getout.Components
{
    public class MenuComponent : DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        SpriteFont _selected;
        Color _selectedColor;
        SpriteFont _notSelected;
        Color _notSelectedColor;
        int _selectedIndex;
        string[] menuItems;
        Vector2 _position;
        Texture2D texture;
        KeyboardState _oldState;
        SpriteAnimation animation;
        //ADDED
        public int _SelectedIndex { get { return _selectedIndex; } }
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont selected,
            SpriteFont notSelected, int selectedIndex, string[] menuItems, Texture2D texture) : base(game)
        {
            _spriteBatch = spriteBatch;
            _selected = selected;
            _notSelected = notSelected;
            _selectedIndex = selectedIndex;
            texture = texture;
            this.menuItems = menuItems;
            _selectedColor = Color.White;
            _notSelectedColor = Color.Gray;
            _position = new Vector2(SharedVars.stage.X / 2.5f, SharedVars.stage.Y / 3);
            this.animation = new SpriteAnimation(texture, 2, 1, 1);
            this.animation.Position = new Vector2(SharedVars.stage.X / 5, SharedVars.movementStage.Height / 4);

        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 initPos = _position;
            _spriteBatch.Begin();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    _spriteBatch.DrawString(_selected, menuItems[i], initPos, _selectedColor);
                    initPos.Y += _selected.LineSpacing;

                }
                else
                {
                    _spriteBatch.DrawString(_notSelected, menuItems[i], initPos, _notSelectedColor);
                    initPos.Y += _notSelected.LineSpacing;

                }

            }

            this.animation.Draw(_spriteBatch);
            this._spriteBatch.DrawString(_selected,Game1.gameName,new Vector2(SharedVars.stage.X / 2.5f, SharedVars.stage.Y /10),Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);

        }

        public override void Update(GameTime gameTime)
        {
            this.animation.Update(gameTime);
            SoundEffectInstance sfx = Game1.menuMovementSound.CreateInstance();
            KeyboardState ks = Keyboard.GetState();
            if ((ks.IsKeyDown(Keys.Down) && _oldState.IsKeyUp(Keys.Down)) || ks.IsKeyDown(Keys.S) && _oldState.IsKeyUp(Keys.S))
            {
                sfx.Stop();
                sfx.Play();
                _selectedIndex += 1;
                if (_selectedIndex == menuItems.Length)
                {
                    _selectedIndex = 0;
                }
            }
            else if ((ks.IsKeyDown(Keys.Up) && _oldState.IsKeyUp(Keys.Up)) || ks.IsKeyDown(Keys.W) && _oldState.IsKeyUp(Keys.W))
            {
                sfx.Stop();
                sfx.Play();
                _selectedIndex -= 1;
                if (_selectedIndex == -1)
                {
                    _selectedIndex = menuItems.Length - 1;
                }
            }
            _oldState = ks;

            base.Update(gameTime);
        }
    }
}
