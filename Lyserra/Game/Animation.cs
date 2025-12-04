using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyserra.Game
{
    public class Animation
    {
        public void dogAnimation()
        {
            string[] frames = new string[]
            {


@"
    ___
 __/ / \
|    |/\
|_--\   \              /-
     \   \-___________/ /
      \                :
      |                :
      |       ___ \    )
       \|  __/     \  )
        | /         \  \
        |l           ( l
        |l            ll
        |l            |l
       / l           / l
       --/           --
",
@"
    ___
 __/ / \
|    |/\
|_--\   \              /-
     \   \-___________/ /
      \                :
      |                :
      |       ___ \    )
       \|  __/     \  )
        | /         \  \
        |l           ( l
        |l            |l
        |l            ll
       / l           / l
       --/           --
",
@"
    ___
 __/ / \
|    |/\
|_--\   \              /-
     \   \-___________/ /
      \                :
      |                :
      |       ___ \    )
       \|  __/     \  )
        | /         \  \
        |l           ( l
        |l            ll
        |l            |l
       / l           / l
       --/           --
"
        };

        int frameIndex = 0;
        int position = 0;
        int consoleWidth = 80;

        while (true)
        {
            Console.Clear();
            string spaces = new string(' ', position);
        Console.WriteLine(spaces + frames[frameIndex].Replace("\n", "\n" + spaces));
            frameIndex = (frameIndex + 1) % frames.Length;

            position++;
            if (position > consoleWidth / 2) position = 0;

            Thread.Sleep(150);
        }
}
    }
}
