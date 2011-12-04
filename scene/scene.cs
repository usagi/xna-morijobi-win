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

using System.Diagnostics;

namespace xna_morijobi_win.scene
{
    public abstract class scene
        : DrawableGameComponent, iscene
    {
        public GameComponentCollection components { get; protected set; }
        public Game Game { get; protected set; }
        public scene_manager scene_manager { get; protected set; }
        public input.input_manager input_manager { get; protected set; }

        public scene(Game g)
            : base(g)
        { 
            components = new GameComponentCollection();
            Game = g;
            
            var sms = find_game_components<scene_manager>();
            Debug.Assert(sms.Count() > 0);
            scene_manager = sms.First();

            var ims = find_game_components<input.input_manager>();
            Debug.Assert(ims.Count() > 0);
            input_manager = ims.First();
        }

        protected IEnumerable<t> find_game_components<t>()
            where t : class
        { return find_components<t>(Game.Components); }

        protected IEnumerable<t> find_scene_components<t>()
            where t : class
        { return find_components<t>(components); }

        protected IEnumerable<t> find_components<t>(GameComponentCollection cs)
            where t : class
        { return cs.Where(c => c as t != null).Select(c => (t)c); }

        public virtual void resume() { }
        public virtual void suspend() { }

        public override void Initialize()
        {
            foreach (var c in components)
                c.Initialize();
            base.Initialize();

            resume();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var updatable in enumerate_from_components<IUpdateable>())
                updatable.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var updatable in enumerate_from_components<IDrawable>())
                updatable.Draw(gameTime);

            base.Update(gameTime);
        }

        protected virtual IEnumerable<t> enumerate_from_components<t>()
            where t : class
        {
            return components
                .Where(c => c as t != null)
                .Select(c => (t)c)
            ;
        }
    }
}
