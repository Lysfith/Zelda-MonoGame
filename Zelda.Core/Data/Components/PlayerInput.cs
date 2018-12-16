using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Core.Data.Managers;
using Zelda.Core.Data.MyEventargs;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components
{
    class PlayerInput : Component
    {
        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.PlayerInput;
            }
        }

        public PlayerInput()
        {
            ManagerInput.FireNewInput += ManagerInput_FireNewInput;
        }

        void ManagerInput_FireNewInput(object sender, NewInputEventArgs e)
        {
            var sprite = GetComponent<Sprite>(ComponentType.Sprite);

            if(sprite == null)
            {
                return;
            }

            var collision = GetComponent<Collision>(ComponentType.Collision);

            var x = 0;
            var y = 0;

            switch (e.Input)
            {
                case Input.Left:
                    x = -1;
                    break;
                case Input.Right:
                    x = 1;
                    break;
                case Input.Up:
                    y = -1;
                    break;
                case Input.Down:
                    y = 1;
                    break;
            }

            if(collision == null || 
                !collision.CheckCollision(new Rectangle(
                    (int)(sprite.Position.X + x),
                    (int)(sprite.Position.Y + y),
                    sprite.Width,
                    sprite.Height
                    )))
            {
                sprite.Move(x, y);
            }

            var camera = GetComponent<Camera>(ComponentType.Camera);
            Vector2 position;

            if (!camera.GetPosition(sprite.Position, out position))
            {
                var animation = GetComponent<Animation>(ComponentType.Animation);
                camera.MoveCamera(animation.CurrentDirection);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update(double gameTime)
        {

        }
    }
}
