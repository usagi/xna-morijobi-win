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
    public class stopwatch
        : diagnostics
    {
        protected class circular_buffer<t>
            : IEnumerable<t>
        {
            protected List<t> list;
            protected int position = 0;

            public int capacity { get; protected set; }
            
            public circular_buffer(int capacity)
            { list = new List<t>(capacity); }
            
            public void push_back(t value)
            { list[position++ % capacity] = value; }

            public t this[int index]
            {
                get { return list[index % capacity]; }
                set { list[index % capacity] = value; }
            }

            public IEnumerator<t> GetEnumerator()
            { return list.GetEnumerator(); }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            { return list.GetEnumerator(); }
        }

        protected Dictionary<string, circular_buffer<double>> times = new Dictionary<string, circular_buffer<double>>();
        static protected Dictionary<string, System.Diagnostics.Stopwatch> stopwatches = new Dictionary<string, System.Diagnostics.Stopwatch>();
        static protected stopwatch instance_ = null;

        protected stopwatch(Game g) : base(g) { }

        static public stopwatch instance(Game g)
        {
            lock (g)
                if (instance_ == null)
                    instance_ = new stopwatch(g);

            return instance_;
        }

        public double this[string key]
        {
            get { return times[key].Average(); }
            set { times[key].push_back(value); }
        }

        static public void measuring_begin(string key)
        { stopwatches[key].Restart(); }

        static public void measuring_end(string key)
        {
            var w = stopwatches[key];
            w.Stop();
            instance_[key] = w.Elapsed.TotalSeconds;
        }
    }
}
