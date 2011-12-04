using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.scenes
{
    class blanding
        :scene.scene
    {
        protected SpriteBatch sprite_batch { get; set; }
        protected Color clear_color { get; set; }
        protected Color draw_color { get; set; }

        protected Texture2D image;

        protected TimeSpan elapsed_time = TimeSpan.Zero;
        protected Action<GameTime> update_action = (t) => { };

        public blanding(Game g) : base(g) { }

        public override void resume()
        {
            base.resume();
            elapsed_time = TimeSpan.Zero;
            draw_color = new Color(1f, 1f, 1f, 0f);
            update_action = update_begining;
        }

        public override void Initialize()
        {
            sprite_batch = new SpriteBatch(Game.GraphicsDevice);
            image = Game.Content.Load<Texture2D>(@"blanding\bland");
            clear_color = Color.Black;
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clear_color);
            sprite_batch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            sprite_batch.Draw(image, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), draw_color);
            sprite_batch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            elapsed_time += gameTime.ElapsedGameTime;

            update_action(gameTime);
        }

        protected void update_begining(GameTime t)
        {
            if (elapsed_time.TotalSeconds < 2)
            {
                draw_color = new Color(1f,1f,1f, (float)(elapsed_time.TotalSeconds / 2));
                return;
            }

            update_action = update_stable;
        }

        protected void update_stable(GameTime t)
        {
            if (elapsed_time.TotalSeconds < 4)
            {
                draw_color = Color.White;
                return;
            }
            update_action = update_ending;
        }

        protected void update_ending(GameTime t)
        {
            if (elapsed_time.TotalSeconds < 6)
            {
                draw_color = new Color(1f, 1f, 1f, (float)(1.0 - (elapsed_time.TotalSeconds - 4) / 2));
                return;
            }

            Enabled = false;
        }
    }
}
