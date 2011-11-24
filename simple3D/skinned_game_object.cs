using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CpuSkinningDataTypes;

namespace xna_morijobi_win.simple3D
{
    abstract class skinned_game_object: game_object
    {
        protected AnimationPlayer animation_player { get; set; }
        protected new CpuSkinnedModel model { get; set; }

        public skinned_game_object(Game game) : base(game) { }

        protected void start_animation_clip(string clip_name)
        { animation_player.StartClip(model.SkinningData.AnimationClips[clip_name]) ; }

        protected virtual void load_model(string asset_name)
        {
            model = Game.Content.Load<CpuSkinnedModel>(asset_name);
            animation_player = new AnimationPlayer(model.SkinningData);

            foreach (CpuSkinnedModelPart p in model.Parts)
                p.Effect.EnableDefaultLighting();

        }

        public override void Update(GameTime gameTime)
        {
            animation_player.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (CpuSkinnedModelPart p in model.Parts)
            {
                p.SetBones(animation_player.SkinTransforms);

                p.Effect.SpecularColor = Vector3.Zero;

                var e = p.Effect;
                e.World = world;
                e.View = camera.view;
                e.Projection = camera.projection;
 
                p.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
