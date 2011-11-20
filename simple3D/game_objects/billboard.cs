using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.simple3D.game_objects
{
    abstract class billboard : DrawableGameComponent
    {
        protected VertexBuffer vertex_buffer;
        public Effect effect { get; protected set; }
        protected camera camera { get { return (Game as game).camera; } }

        public virtual Vector3 position { get; protected set; }
        public virtual float width { get; protected set; }
        public virtual float height { get; protected set; }

        public virtual string texture_asset_name { get { throw new NotImplementedException(); } }
        
        public billboard(Game game)
            : base(game)
        { }

        public override void Initialize()
        {
            base.Initialize();
            generate_vertex_buffer();
            generate_effect();
            load_texture();
        }

        protected void load_texture()
        {
            var e = effect as BasicEffect;
            e.Texture = Game.Content.Load<Texture2D>(texture_asset_name);
        }

        protected void generate_effect()
        {
            var e = new BasicEffect(GraphicsDevice);
            e.TextureEnabled = true;
            effect = e;
        }

        protected void generate_vertex_buffer()
        {
            vertex_buffer = new VertexBuffer(Game.GraphicsDevice, typeof(VertexPositionTexture), 6, BufferUsage.None);
            var vs = new VertexPositionTexture[]{
                new VertexPositionTexture(new Vector3(-0.5f,  0.5f, 0), Vector2.Zero),
                new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0), Vector2.UnitY),
                new VertexPositionTexture(new Vector3( 0.5f,  0.5f, 0), Vector2.UnitX), 
                new VertexPositionTexture(new Vector3( 0.5f, -0.5f, 0), Vector2.One)
            };
            vertex_buffer.SetData(vs);
        }

        protected void sync_effect_camera()
        {
            var e = effect as BasicEffect;
            e.World = Matrix.CreateScale(width, height, 1.0f) * Matrix.CreateBillboard(position, camera.position, camera.up, null);
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
                Game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }
        }
    }
}
