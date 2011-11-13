using System;

namespace xna_morijobi_win
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main()
        {
            using (var g = new simple3D.game())
                g.Run();
        }
    }
#endif
}

