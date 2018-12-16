using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Core.Data.Managers;

namespace Zelda.Core.Data.Map
{
    public class TileCollision
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public Rectangle Rectangle { get { return new Rectangle(XPos * Tile.Width, YPos * Tile.Height, Tile.Width, Tile.Height); } }

        public ManagerCamera ManagerCamera { get; set; }

        public TileCollision()
        {

        }

        public TileCollision(ManagerCamera managerCamera)
        {
            ManagerCamera = managerCamera;
        }

        public bool Intersect(Rectangle rectangle)
        {
            var position = new Vector2(Rectangle.X, Rectangle.Y);

            return ManagerCamera.InScreenCheck(position) 
                && rectangle.Intersects(
                    new Rectangle((int)position.X, (int)position.Y, Tile.Width, Tile.Height));
        }
    }
}
