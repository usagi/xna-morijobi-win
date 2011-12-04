using System;

namespace xna_morijobi_win
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main()
        {
            using (var g = new game())
                g.Run();
        }
    }
#endif
}

