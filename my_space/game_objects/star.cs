using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xna_morijobi_win.my_space
{
    public class star
        : simple3D.game_object
    {
        public const float const_g = 6.6738480e-11f;
        public const float const_e = 5.97219e+24f;

        public float mass { get; protected set; }
        public Vector3 position_acceleration { get { return position_acceleration_; } }

        protected Vector3 position_acceleration_ = Vector3.Zero;

        protected readonly simple3D.game_objects.polar_camera camera;

        public BoundingSphere bounding = new BoundingSphere();

        public star(Game game, simple3D.game_objects.polar_camera camera)
            : base(game)
        {
            Debug.Assert(camera != null);
            this.camera = camera;
        }

        public star(Game game, simple3D.game_objects.polar_camera camera,  Random r)
            : this(game, camera)
        {
            Debug.Assert(r != null);
            Func<float> fu = () => (float)r.NextDouble();
            Func<float> fs = () => fu() - fu();
            Func<Vector3> fv = () => Vector3.Normalize(new Vector3(fs(), fs(), fs()));
            position_ = fv() * 100f;
            position_velocity_ = fv();
            angle_velocity_ = fv();
            mass = fu() * const_e * 10.0f;
        }

        public override void Initialize()
        {
            base.Initialize();
            model = Game.Content.Load<Model>(@"misc\cat");

            foreach (var m in model.Meshes)
            {
                BoundingSphere.CreateMerged(bounding, m.BoundingSphere);
                foreach (var e in m.Effects)
                    (e as BasicEffect).EnableDefaultLighting();
            }

            scaling_ *= bounding.Radius = mass / const_e;
        }

        public void effect_from(star t)
        {
            var orientation = Vector3.Normalize(t.position - position);
            var radius = Vector3.Distance(position, t.position);
            var force_without_my_mass = const_g * t.mass / (radius * radius);
            position_acceleration_ += orientation * force_without_my_mass * 1.0e-15f;
        }

        public override void Update(GameTime gameTime)
        {
            position_velocity_ += position_acceleration_ * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            bounding.Center = position_;

            if (Math.Abs(position_.X) > 1000.0f)
                position_.X = -position_.X;
            if (Math.Abs(position_.Y) > 1000.0f)
                position_.Y = -position_.Y;
            if (Math.Abs(position_.Z) > 1000.0f)
                position_.Z = -position_.Z;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var m in model.Meshes)
                foreach (var e in m.Effects)
                {
                    sync_effect_matrices(e);
                    m.Draw();
                }

            base.Draw(gameTime);
        }

        protected void sync_effect_matrices(Effect e)
        {
            var e_ = e as BasicEffect;
            e_.View = camera.view;
            e_.Projection = camera.projection;
            e_.World = world;
        }
    }
}
