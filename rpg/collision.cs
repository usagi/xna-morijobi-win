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
        protected object bounding_ = null;

        public object bounding
        {
            get { return bounding_; }
            protected set
            {
                var t = value.GetType();
                Debug.Assert(
                    t == typeof(BoundingBox) ||
                    t == typeof(BoundingSphere) ||
                    t == typeof(BoundingFrustum) ||
                    t == typeof(Plane) ||
                    t == typeof(Ray)
                    , "bounding set Ç… ñ≥å¯Ç»å^Ç™ë„ì¸Ç≥ÇÍÇΩâ¬î\ê´Ç™Ç†ÇËÇ‹Ç∑ÅB"
                );
                bounding_ = value;
            }
        }

        public virtual void collide_against(icollision target)
        { }

        static public void collisions(icollision a, icollision b)
        {
            dynamic ba = a.bounding;
            dynamic bb = b.bounding;

            try
            {
                if (ba.Intersects(bb))
                {
                    a.collide_against(b);
                    b.collide_against(a);
                }
            }
            catch (MissingMethodException e)
            {
                Debug.Assert(
                    (ba.GetType() == typeof(Plane) && bb.GetType() == typeof(Plane)) ||
                    (ba.GetType() == typeof(Plane) && bb.GetType() == typeof(Ray)) ||
                    (ba.GetType() == typeof(Ray) && bb.GetType() == typeof(Ray))
                    , e.Message
                );
            }
        }

    }
}
