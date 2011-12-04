using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.simple3D.game_objects
{
    public class axes : DrawableGameComponent
    {
        protected VertexBuffer vertex_buffer;
        public Effect effect { get; protected set; }
        protected camera camera { get; set; }

        public axes(Game game, camera camera)
            : base(game)
        { this.camera = camera; }

        public override void Initialize()
        {
            base.Initialize();
            generate_vertex_buffer();
            generate_effect();
        }

        protected void generate_effect()
        {
            var e = new BasicEffect(GraphicsDevice);
            e.VertexColorEnabled = true;
            effect = e;
        }

        protected void generate_vertex_buffer()
        {
            vertex_buffer = new VertexBuffer(Game.GraphicsDevice, typeof(VertexPositionColor), 6, BufferUsage.None);
            var vs = new VertexPositionColor[]{
                new VertexPositionColor(new Vector3(-1000, 0, 0), Color.Red),
                new VertexPositionColor(new Vector3( 1000, 0, 0), Color.Red),
                new VertexPositionColor(new Vector3(0, -1000, 0), Color.Green),
                new VertexPositionColor(new Vector3(0,  1000, 0), Color.Green),
                new VertexPositionColor(new Vector3(0, 0, -1000), Color.Blue),
                new VertexPositionColor(new Vector3(0, 0,  1000), Color.Blue)
            };
            vertex_buffer.SetData(vs);
        }

        protected void sync_effect_camera()
        {
            var e = effect as BasicEffect;
            e.View = camera.view;
            e.Projection = camera.projection;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.GraphicsDevice.SetVertexBuffer(vertex_buffer);

            sync_effect_camera();
            foreach (var p in effect.CurrentTechnique.Passes)
            {
                p.Apply();
                Game.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, 3);
            }
        }
    }
}
