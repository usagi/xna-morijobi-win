using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace xna_morijobi_win.scenes
{
    class title
        :scene.scene
    {
        protected TimeSpan elapsed_time = TimeSpan.Zero;

        protected SpriteBatch sprite_batch { get; set; }
        protected SpriteFont font { get; set; }
        protected Texture2D image;

        protected Song bgm;
        protected SoundEffect se_enter;

        public title(Game g) : base(g) { }

        public override void Initialize()
        {
            sprite_batch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>(@"fonts\default");
            image = Game.Content.Load<Texture2D>(@"title\title");
            se_enter = Game.Content.Load<SoundEffect>(@"misc\synth-sweep2");
            bgm = Game.Content.Load<Song>(@"misc\01 銀の意志 金の翼");
            base.Initialize();
        }

        public override void resume()
        {
            base.resume();
            elapsed_time = TimeSpan.Zero;
            //MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            sprite_batch.Begin();
            sprite_batch.Draw(image, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            if(elapsed_time.Milliseconds < 500)
                sprite_batch.DrawString(font, "<PRESS Z to MY-SPACE>\n<PRESS X to RPG-TEST>", new Vector2(64, 320), Color.White);
            sprite_batch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            elapsed_time += gameTime.ElapsedGameTime;

            if (input_manager.is_key_down_begin(Keys.Z))
            {
                se_enter.Play();
                scene_manager.push(new my_space.my_space(Game));
            }
            else if (input_manager.is_key_down_begin(Keys.X))
            {
                se_enter.Play();
                scene_manager.push(new rpg.test(Game));
            }
        }
    }
}
