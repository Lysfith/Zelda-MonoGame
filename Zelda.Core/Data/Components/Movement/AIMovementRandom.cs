using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Core.Data.Managers;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components.Movement
{
    class AIMovementRandom : Component
    {
        private Direction _currentDirection;
        private readonly int _frequency;
        private double _counter;

        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.AIMovement;
            }
        }

        public AIMovementRandom(int frequency)
        {
            _frequency = frequency;
            ChangeDirection();
        }

        public override void Update(double gameTime)
        {
            var sprite = GetComponent<Sprite>(ComponentType.Sprite);

            if(sprite == null)
            {
                return;
            }

            _counter += gameTime;

            if(_counter > _frequency)
            {
                ChangeDirection();
            }

            var collision = GetComponent<Collision>(ComponentType.Collision);

            var x = 0;
            var y = 0;

            switch (_currentDirection)
            {
                case Direction.Left:
                    x = -1;
                    break;
                case Direction.Right:
                    x = 1;
                    break;
                case Direction.Up:
                    y = -1;
                    break;
                case Direction.Down:
                    y = 1;
                    break;
            }

            if (collision.CheckCollision(new Rectangle(
                    (int)(sprite.Position.X + x),
                    (int)(sprite.Position.Y + y),
                    sprite.Width,
                    sprite.Height
                    )))
            {
                ChangeDirection();
                return;
            }

            sprite.Move(x, y);
        }

        private void ChangeDirection()
        {
            _counter = 0;
            _currentDirection = (Direction)ManagerFunction.Random(0, 3);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
