using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace xna_morijobi_win.simple3D.game_objects
{
    public class polar_camera : camera
    {
        public virtual float theta { get; protected set; }
        public virtual float phi { get; protected set; }
        public virtual float distance { get; protected set; }

        public new Vector3 target
        {
            get { return target_; }
            set { target_ = value; }
        }

        public polar_camera(Game game)
            : base(game)
        { }

        public override void Initialize()
        {
            base.Initialize();
            target_ = Vector3.Zero;
            theta = 0.0f;
            phi = 0.0f;
            distance = 100.0f;
        }

        public virtual void set_position(float theta, float phi, float distance)
        {
            update_polar_params(theta, phi, distance);
            update_position_with_polar_params();
        }

        protected virtual void update_position_with_polar_params()
        {
            position_.X = (float)(Math.Cos(theta)) * (float)(Math.Cos(phi));
            position_.Y = (float)(Math.Sin(phi));
            position_.Z = (float)(Math.Sin(theta)) * (float)(Math.Cos(phi));
            position_ *= distance;
            position_ += target_;
        }

        protected virtual void update_polar_params(float theta, float phi, float distance)
        {
            this.theta = theta;
            this.phi = phi;
            this.distance = distance;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var d1 = t * 0.5f;           
            if (keyboard_state.IsKeyDown(Keys.Left))
                theta += d1;
            if (keyboard_state.IsKeyDown(Keys.Right))
                theta -= d1;
            if (keyboard_state.IsKeyDown(Keys.Up))
                phi += d1;
            if (keyboard_state.IsKeyDown(Keys.Down))
                phi -= d1;

            var d2 = t * 100.0f;
            if (keyboard_state.IsKeyDown(Keys.PageUp))
                distance += d2;
            if (keyboard_state.IsKeyDown(Keys.PageDown))
                distance -= d2;

            update_position_with_polar_params();
        }
    }
}