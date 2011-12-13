using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xna_morijobi_win.diagnostics
{
    public class diagnostics_manager
        :DrawableGameComponent, idiagnostics
    {
        public GameComponentCollection components { get; protected set; }

        public Vector2 position { get; set; }
        public Color foreground_color { get; set; }
        public Color background_color { get; set; }
        public SpriteFont font { get; set; }
        public Texture2D background { get; set; }

        protected SpriteBatch sprite_batch { get; set; }

        public diagnostics_manager(Game g)
            : base(g)
        {
            components = new GameComponentCollection();
            position = Vector2.Zero;

            foreground_color = Color.White;
            background_color = new Color(0f, 0f, 0f, 0.65f);

            UpdateOrder = DrawOrder = int.MaxValue;
        }

        public override void Initialize()
        {
            base.Initialize();
            sprite_batch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>(@"fonts\default");
            background = new Texture2D(GraphicsDevice, 1, 1);
            background.SetData(new Color[] { background_color });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var c in components)
                ((IUpdateable)c).Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            sprite_batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sprite_batch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            sprite_batch.DrawString(font, ToString(), position, foreground_color);
            sprite_batch.End();
        }

        public string message
        {
            get { return string.Join(Environment.NewLine, from c in components select ((idiagnostics)c).ToString()); }
        }

        public override string ToString()
        {
            return "<<DIAGNOSTICS>>" + Environment.NewLine + message;
        }
    }
}
