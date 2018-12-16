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
    class Camera : Component
    {
        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.Camera;
            }
        }

        private ManagerCamera _managerCamera;

        public Camera(ManagerCamera managerCamera)
        {
            _managerCamera = managerCamera;
        }

        public bool GetPosition(Vector2 position, out Vector2 newPosition)
        {
            newPosition = _managerCamera.WorlToScreenPosition(position);
            return _managerCamera.InScreenCheck(position);
        }

        public void MoveCamera(Direction direction)
        {
            _managerCamera.Move(direction);
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
