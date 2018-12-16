using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Core.Data.Components.Enemies
{
    class OctorokBullet
    {
        private Sprite _sprite;
        private BaseObject _player;
        private Direction _direction;
        private float _speed;

        public OctorokBullet(Sprite sprite, BaseObject player, Direction direction)
        {
            _sprite = sprite;
            _player = player;
            _direction = direction;
            _speed = 0.5f;
        }

        public void Update(double gameTime)
        {
            switch (_direction)
            {
                case Direction.Up:
                    _sprite.Move(0, -_speed);
                    break;
                case Direction.Down:
                    _sprite.Move(0, _speed);
                    break;
                case Direction.Left:
                    _sprite.Move(-_speed, 0);
                    break;
                case Direction.Right:
                    _sprite.Move(_speed, 0);
                    break;
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }
    }
}
