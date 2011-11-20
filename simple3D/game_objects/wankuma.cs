using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_morijobi_win.simple3D.game_objects
{
    class wankuma : billboard
    {
        public override string texture_asset_name { get{return "wankuma";} }
        
        public wankuma(Game game)
            : base(game)
        {
            width = 9.0f;
            height = 6.0f;
        }
    }
}
