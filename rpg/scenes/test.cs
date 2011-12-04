using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class test
        : scene.scene
    {
        protected SoundEffect se_collision;
        protected simple3D.game_objects.polar_camera camera { get; set; }

        protected List<map_block> map_blocks { get; set; }

        public test(Game g)
            : base(g)
        {
            map_blocks = new List<map_block>();
        }

        public override void Initialize()
        {
            components.Add(camera = new simple3D.game_objects.polar_camera(Game));

            var map_image = Game.Content.Load<Texture2D>(@"rpg\map_test");
            var map_image_data = new Color[map_image.Width * map_image.Height];
            map_image.GetData(map_image_data);
            for(var z = 0; z < map_image.Height;++z)
            {
                var z_ = z * map_image.Width;
                for (var x = 0; x < map_image.Width; ++x)
                {
                    var h = map_image_data[z_ + x].R;
                    for (var n = 0; n < h; ++n)
                        components.Add(new map_block(Game, camera, new Vector3(map_block.floor_length * (float)x, map_block.height * n, map_block.floor_length * -(float)z)));
                }
            }

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (input_manager.is_key_down_begin(Keys.Space))
                scene_manager.pop();

            camera_update(gameTime);

            base.Update(gameTime);
        }

        protected void camera_update(GameTime gameTime)
        {
            var t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var d1 = t * 0.5f;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.Left))
                camera.theta += d1;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.Right))
                camera.theta -= d1;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.Up))
                camera.phi += d1;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.Down))
                camera.phi -= d1;

            var d2 = t * 100.0f;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.PageUp))
                camera.distance += d2;
            if (input_manager.current_keyboard_state.IsKeyDown(Keys.PageDown))
                camera.distance -= d2;
        }
    }
}
