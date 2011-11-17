using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace xna_morijobi_win.simple3D.game_objects
{
    public class camera : GameComponent
    {
        public virtual Vector3 position { get { return position_; } }
        public virtual Vector3 target { get { return target_; } }
        public virtual Vector3 up { get { return up_; } }
        public virtual Vector3 forward { get { return Vector3.Normalize(target - position); } }
        
        public virtual Matrix view { get { return Matrix.CreateLookAt(position_, target_, up_); } }
        public virtual Matrix projection { get { return Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4, Game.GraphicsDevice.Viewport.AspectRatio, 1.0f, 1000.0f); } }

        protected Vector3 position_, target_, up_;

        protected KeyboardState keyboard_state { get { return (Game as game).keyboard_state; } }

        public camera(Game game) : base(game) { }

        public override void Initialize()
        {
            base.Initialize();
            position_ = new Vector3(25, 25, 50);
            up_ = Vector3.Up;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
