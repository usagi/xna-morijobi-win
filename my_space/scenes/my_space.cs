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

namespace xna_morijobi_win.my_space
{
    public class my_space
        : scene.scene
    {
        protected SoundEffect se_collision;
        protected simple3D.game_objects.polar_camera camera { get; set; }

        public my_space(Game g) : base(g) { }

        public override void Initialize()
        {
            se_collision = Game.Content.Load<SoundEffect>(@"misc\metal-attack");

            camera = new simple3D.game_objects.polar_camera(Game);
            components.Add(camera);

            components.Add(new simple3D.game_objects.axes(Game, camera));

            var r = new Random();
            for (var n = 16; n > 0; --n)
                components.Add(new star(Game, camera, r));

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (input_manager.is_key_down_begin(Keys.Space))
                scene_manager.pop();

            update_stars_gravity(gameTime);

            camera_update(gameTime);

            base.Update(gameTime);

            collisions();
        }

        protected void collisions()
        {
            var ss = new List<star>(find_scene_components<star>());
            var c = ss.Count;
            var removes = new Queue<star>();
            // for(var ia = 0; ia < c; ++ia)
            Parallel.For(0, c, ia =>
            {
                for (var ib = ia + 1; ib < c; ++ib){
                    var a = ss[ia];
                    var b = ss[ib];
                    if (a.bounding.Intersects(b.bounding))
                    {
                        //if (a.mass > b.mass)
                        //    removes.Enqueue(b);
                        //else
                        //    removes.Enqueue(a);
                        removes.Enqueue((a.mass > b.mass) ? b : a);
                        se_collision.Play();
                    }
                }
            });

            foreach (var s in removes)
                components.Remove(s);
        }

        protected void update_stars_gravity(GameTime gameTime)
        {
            var ss = find_scene_components<star>();
            var sc = from sa in ss from sb in ss where sa != sb select new {sa, sb};
            Parallel.ForEach(sc, sp => {
                sp.sa.effect_from(sp.sb);
            });
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
