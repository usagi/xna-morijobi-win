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

namespace input
{
    public class input_manager
        : GameComponent
    {
        public KeyboardState before_keyboard_state { get; protected set; }
        public KeyboardState current_keyboard_state { get; protected set; }

        public input_manager(Game g) : base(g) { }

        public bool is_key_down_begin(Keys k)
        { return current_keyboard_state.IsKeyDown(k) && before_keyboard_state.IsKeyUp(k); }

        public bool is_key_up_begin(Keys k)
        { return current_keyboard_state.IsKeyUp(k) && before_keyboard_state.IsKeyDown(k); }

        public override void Initialize()
        {
            before_keyboard_state = current_keyboard_state = new KeyboardState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            before_keyboard_state = current_keyboard_state;
            current_keyboard_state = Keyboard.GetState();
        }
    }
}
