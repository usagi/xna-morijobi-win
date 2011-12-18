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

        protected Texture2D map_image;
        protected Color[] map_image_data;

        public test(Game g)
            : base(g)
        {
            map_blocks = new List<map_block>();
        }

        public override void Initialize()
        {
            components.Add(camera = new simple3D.game_objects.polar_camera(Game));

            map_image = Game.Content.Load<Texture2D>(@"rpg\map_test");
            map_image_data = new Color[map_image.Width * map_image.Height];
            map_image.GetData(map_image_data);
            Debug.Assert(map_image_data.Count() > 0);
            components.Add(new base_plane(Game, camera));

            on_update_first += generate_map_block;
            on_update_first += check_exit;
            on_update_first += camera_update;
            on_update_last += collisions;

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            base.Draw(gameTime);
        }

        protected void collisions(GameTime gameTime)
        {
            var cs = find_scene_components<icollision>().ToList();
            var csc = cs.Count;
            for (var na = 0; na < csc; ++na)
                for (var nb = na + 1; nb < csc; ++nb)
                    collision.collisions(cs[na], cs[nb]);
        }

        protected void check_exit(GameTime gameTime)
        {
            if (input_manager.is_key_down_begin(Keys.Space))
                scene_manager.pop();
        }

        protected struct map_block_loader_params
        {
            public int x, z;
            public float h;
            public double time;
        }
        protected map_block_loader_params map_block_loader_params_;
        protected void generate_map_block(GameTime gameTime)
        {
            if ((map_block_loader_params_.time += gameTime.ElapsedGameTime.TotalSeconds) < 0.05)
                return;
            map_block_loader_params_.time = 0;

            map_block_loader_params_.h = map_image_data[map_block_loader_params_.z * map_image.Width + map_block_loader_params_.x].R * 0.025f;

            var p = new Vector3(map_block.floor_length * (float)map_block_loader_params_.x, 20, map_block.floor_length * -(float)map_block_loader_params_.z) { };
            Debug.WriteLine("new block; position = " + p);
            var c = new map_block(Game, camera) { position = p, height = map_block_loader_params_.h };
            c.Initialize();
            components.Add(c);

            ++map_block_loader_params_.x;

            if (map_block_loader_params_.x >= map_image.Width)
            {
                ++map_block_loader_params_.z;
                map_block_loader_params_.x = 0;
                if (map_block_loader_params_.z >= map_image.Height)
                {
                    on_update_first -= generate_map_block;
                    return;
                }
            }
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
