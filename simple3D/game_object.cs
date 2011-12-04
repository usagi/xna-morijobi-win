using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.simple3D
{
    public abstract class game_object: DrawableGameComponent
    {
        protected game_objects.camera camera { get { return (Game as game).camera; } }

        public virtual Vector3 position { get { return position_; } }
        public virtual Vector3 position_velocity { get { return position_velocity_; } }
        public virtual Vector3 angle { get { return angle_; } }
        public virtual Vector3 angle_velocity { get { return angle_velocity_; } }
        public virtual Vector3 scaling { get { return scaling_; } }

        protected Vector3
            position_ = Vector3.Zero,
            position_velocity_ = Vector3.Zero,
            angle_ = Vector3.Zero,
            angle_velocity_ = Vector3.Zero,
            scaling_ = Vector3.One;

        public Matrix world { get { return Matrix.Identity * Matrix.CreateScale(scaling_) * Matrix.CreateFromYawPitchRoll(angle_.Y, angle_.X, angle_.Z) * Matrix.CreateTranslation(position_); } }
        
        protected virtual Model model { get; set; }

        public game_object(Game game) : base(game) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position_ += position_velocity_ * t;
            angle_ += angle_velocity_ * t;
        }
    }
}
