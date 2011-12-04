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
    interface iscene
        :IDrawable, IUpdateable, IGameComponent
    {
        void resume();
        void suspend();
    }

    interface iscene_manager
        : IDrawable, IUpdateable, IGameComponent
    {
        void push(scene s);
        void push(ICollection<scene> ss);
        void swap(scene s);
        void pop();
    }
}
