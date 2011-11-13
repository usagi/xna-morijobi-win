using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.simple3D.game_objects
{
    class cat : game_object
    {
        public cat(Game game)
            : base(game)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();

            model = this.Game.Content.Load<Model>("cat");

            foreach (var m in model.Meshes)
                foreach (var e in m.Effects)
                    (e as BasicEffect).EnableDefaultLighting();

            scaling_ = Vector3.One * 5.0f;
            angle_velocity_ = Vector3.Right * (float)Math.PI / 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var t = gameTime.TotalGameTime.TotalSeconds;
            position_.X = (float)Math.Sin(t * 0.5);
            position_.Z = (float)Math.Cos(t * 0.5);
            position_ *= 20.0f;

            if ((int)t % 100 == 0)
                Game.Components.Add(new cat(Game));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var m in model.Meshes)
                foreach (var e in m.Effects)
                {
                    sync_effect_matrices(e);
                    m.Draw();
                }
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
