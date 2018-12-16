using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Core.Data.Managers;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components
{
    class Collision : Component
    {
        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.Collision;
            }
        }

        private ManagerMap _managerMap;

        public Collision(ManagerMap managerMap)
        {
            _managerMap = managerMap;
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public bool CheckCollision(Rectangle rectangle, bool fixBox = true)
        {
            rectangle = new Rectangle(
                (int)(rectangle.X + (rectangle.Width*0.4)*0.5),
                (int)(rectangle.Y + rectangle.Height*0.5),
                (int)(rectangle.Width*0.6),
                (int)(rectangle.Height*0.5)
                );
            return _managerMap.CheckCollision(rectangle);
        }
    }
}
