using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Core.Data.Map
{
    public class TileFrame
    {
        public int TextureXPos { get; set; }
        public int TextureYPos { get; set; }

        public TileFrame()
        {

        }

        public TileFrame(int textureXPos, int textureYPos)
        {
            TextureXPos = textureXPos;
            TextureYPos = textureYPos;
        }
    }
}
