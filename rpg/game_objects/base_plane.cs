using System;
using System.Collections.Generic;
using System.Linq;
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
    public class base_plane
        : simple3D.game_object, icollision
    {
        protected readonly simple3D.game_objects.polar_camera camera;
        protected Plane bounding_ = new Plane(Vector3.UnitY, 0f);
        public Plane plane { get { return bounding_; } }

        public base_plane(Game game, simple3D.game_objects.polar_camera camera)
            : base(game)
        {
            Debug.Assert(camera != null);
            this.camera = camera;
        }

        public object bounding
        { get { return bounding_; } }

        public void collide_against(icollision target)
        {
        }
    }
}
