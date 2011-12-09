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

namespace xna_morijobi_win.diagnostics
{
    public abstract class diagnostics
        : idiagnostics, IUpdateable, IGameComponent
    {
        public diagnostics(Game g)
        {
            game = g;
            message = string.Empty;
            UpdateOrder = int.MaxValue - 1;
        }

        protected readonly Game game;

        public override string ToString()
        {
            return "<" + this + ">" + Environment.NewLine + message + Environment.NewLine;
        }

        public virtual string message { get; protected set; }

        public bool Enabled { get; set; }

        public event EventHandler<EventArgs> EnabledChanged;

        public void Update(GameTime gameTime) { }

        public int UpdateOrder { get; set; }

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Initialize() { }
    }
}
