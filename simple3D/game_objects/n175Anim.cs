using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CpuSkinningDataTypes;

namespace xna_morijobi_win.simple3D.game_objects
{
    class n175Anim : skinned_game_object
    {
        public n175Anim(Game game)
            : base(game)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();

            load_model("n175Anim");
            start_animation_clip("Run");

            scaling_ = Vector3.One * 10.0f;
            angle_velocity_ = Vector3.UnitY * (float)Math.PI / 2;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var t = gameTime.TotalGameTime.TotalSeconds;
            position_.Y = (float)Math.Sin(t * 0.5);
            position_.Z = (float)Math.Cos(t * 0.5);
            position_ *= 30.0f;
        }
    }
}
