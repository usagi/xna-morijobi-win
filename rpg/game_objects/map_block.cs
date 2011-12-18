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

namespace xna_morijobi_win.rpg
{
    public class map_block
        : simple3D.game_object, icollision
    {
        public new Vector3 position {
            get { return position_; }
            set { position_ = value; }
        }
        public float height {
            get { return height_; }
            set
            {
                height_ = value;
                scaling_.Y = value * 0.5f;
                bounding_unit = new Vector3(floor_length, height_ * 0.5f, floor_length);
                update_bounding();
            }
        }
        protected float height_ = 1.0f;
        public const float floor_length = 1.0f;
        protected readonly simple3D.game_objects.polar_camera camera;
        protected BoundingBox bounding_ = new BoundingBox();
        public BoundingBox bounding_box { get { return bounding_; } }
        protected Vector3 bounding_unit;

        public map_block(Game game, simple3D.game_objects.polar_camera camera)
            : base(game)
        {
            Debug.Assert(camera != null);
            this.camera = camera;
            position_velocity_ = -Vector3.UnitY * height * 10;
            bounding_unit = new Vector3(floor_length, height_, floor_length);
            update_bounding();
        }

        public override void Initialize()
        {
            base.Initialize();

            model = Game.Content.Load<Model>(@"misc\box");

            foreach (var m in model.Meshes)
                foreach (var e in m.Effects)
                    (e as BasicEffect).EnableDefaultLighting();

            scaling_.Y = height;
            scaling_.X = scaling_.Z = floor_length;
            scaling_ *= 0.5f;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            update_bounding();
        }

        protected void sync_effect_matrices(Effect e)
        {
            var e_ = e as BasicEffect;
            e_.View = camera.view;
            e_.Projection = camera.projection;
            e_.World = world;
        }

        public object bounding
        { get { return bounding_; } }

        protected void update_bounding()
        {
            bounding_.Min = -bounding_unit + position_;
            bounding_.Max = bounding_unit + position_;
        }

        public void collide_against(icollision target)
        {
            var target_types = target.GetType().GetNestedTypes().Union(new Type[] { target.GetType() });
            if (target_types.Any(t => t == typeof(base_plane)))
                collide_against_plane((base_plane)target);
            //else if (target_types.Any(t => t == typeof(map_block)))
            //    collide_against_map_block((map_block)target);
        }

        protected void collide_against_plane(base_plane target)
        {
            var target_normal = target.plane.Normal;
            var target_position = target_normal * target.plane.D;
            position_.Y = target_position.Y + height_ * 0.5f;
            update_bounding();
        }

        protected void collide_against_map_block(map_block target)
        {
            if (position.Y < target.position.Y)
                return;

            var direction = Vector3.Normalize(target.position - position_);
            var distance = Vector3.Distance(position_, target.position);
            var margin_factor = 1.01f;
            position_ += direction * (distance * margin_factor);
            update_bounding();
        }
    }
}
