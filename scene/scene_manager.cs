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
    public class scene_manager
        : DrawableGameComponent, iscene_manager
    {
        protected Stack<scene> scenes = new Stack<scene>();
        protected Stack<Song> bgms = new Stack<Song>();

        public scene_manager(Game g)
            : base(g)
        {
            Debug.WriteLine(this + "()");
        }

        public virtual void push(scene s)
        {
            Debug.WriteLine(this + "::push");
            Debug.WriteLine(" scene = " + s);

            if (scenes.Count > 0)
                scenes.Peek().suspend();

            s.Initialize();

            scenes.Push(s);
        }

        public virtual void push(ICollection<scene> ss)
        {
            foreach (var s in ss)
                push(s);
        }

        public virtual void swap(scene s)
        {
            Debug.WriteLine(this + "::swap");
            Debug.WriteLine(" scene = " + s);
            pop();
            push(s);
        }

        public virtual void pop()
        {
            Debug.WriteLine(this + "::pop ");
            Debug.WriteLine(" scene = " + scenes.Peek());
            Debug.Assert(scenes.Count > 1);
            
            scenes.Pop();

            if (scenes.Count == 0)
                Game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            iscene s;
            do
            {
                (s = scenes.Peek()).Update(gameTime);
                if (!s.Enabled)
                    pop();
            }while(s != scenes.Peek());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            scenes.Peek().Draw(gameTime);
        }
    }
}
