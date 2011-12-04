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
        : simple3D.game_object
    {
        public const float height = 0.1f;
        public const float floor_length = 1.0f;
        protected readonly simple3D.game_objects.polar_camera camera;

        public map_block(Game game, simple3D.game_objects.polar_camera camera, Vector3 position)
            : base(game)
        {
            Debug.Assert(camera != null);
            this.camera = camera;
            position_ = position;
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

        protected void sync_effect_matrices(Effect e)
        {
            var e_ = e as BasicEffect;
            e_.View = camera.view;
            e_.Projection = camera.projection;
            e_.World = world;
        }
    }
}
