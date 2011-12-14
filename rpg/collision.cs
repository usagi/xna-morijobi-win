using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xna_morijobi_win.rpg
{
    public abstract class collision
        : icollision
    {
        public virtual BoundingSphere bounding
        { get; protected set; }

        public virtual void collide_against(icollision target)
        { }

        static public void collisions(icollision a, icollision b)
        {
            if (a.bounding.Intersects(b.bounding))
            {
                Parallel.Invoke(
                    () => a.collide_against(b),
                    () => b.collide_against(a)
                );
            }
        }
    }
}
