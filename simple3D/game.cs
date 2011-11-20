using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Threading.Tasks;

namespace xna_morijobi_win.simple3D
{
    public class game : Game
    {
        GraphicsDeviceManager graphics;

        public game_objects.camera camera { get; protected set; }
        public KeyboardState keyboard_state { get; protected set; }
        
        public game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            reset_components();
            base.Initialize();
        }

        protected void reset_components()
        {
            Components.Clear();
            generate_components();
        }

        protected void generate_components()
        {
            Components.Add(camera =
                //new game_objects.camera(this)
                new game_objects.polar_camera(this)
            );
            Components.Add(new game_objects.axes(this));
            Components.Add(new game_objects.cat(this));
            Components.Add(new game_objects.wankuma(this));
        }

        protected override void Update(GameTime gameTime)
        {
            update_keyboard_state();
            check_exit();
            check_fullscreen();
            base.Update(gameTime);
        }

        protected void check_fullscreen()
        {
            if (keyboard_state.IsKeyDown(Keys.F12))
                graphics.ToggleFullScreen();
        }

        protected void check_exit()
        {
            if (keyboard_state.IsKeyDown(Keys.Escape))
                Exit();
        }

        protected void update_keyboard_state()
        {
            keyboard_state = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
