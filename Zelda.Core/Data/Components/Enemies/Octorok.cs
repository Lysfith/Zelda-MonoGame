using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Utils;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components.Enemies
{
    class Octorok : Component
    {
        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.EnemyOctorok;
            }
        }

        private BaseObject _player;
        private List<OctorokBullet> _bullets;
        private int _cooldown;
        private double _counter;
        private Texture2D _bulletTexture;

        public Octorok(BaseObject player, Texture2D bulletTexture, int cooldown = 1000)
        {
            _player = player;
            _bullets = new List<OctorokBullet>();
            _cooldown = cooldown;
            _counter = 0;
            _bulletTexture = bulletTexture;
        }

        public override void Update(double gameTime)
        {
            foreach (var bullet in _bullets)
            {
                bullet.Update(gameTime);
            }

            _counter += gameTime;

            if (_counter < _cooldown)
                return;

            var sprite = GetComponent<Sprite>(ComponentType.Sprite);
            var playerSprite = _player.GetComponent<Sprite>(ComponentType.Sprite);
            var animation = GetComponent<Animation>(ComponentType.Animation);

            if(sprite == null ||animation == null || playerSprite == null)
            {
                return;
            }

            switch(animation.CurrentDirection)
            {
                case Direction.Up:
                    if(playerSprite.Position.Y < sprite.Position.Y)
                    {
                        NewBullet(Direction.Up);
                    }  
                    break;
                case Direction.Down:
                    if (playerSprite.Position.Y > sprite.Position.Y)
                    {
                        NewBullet(Direction.Down);
                    }
                    break;
                case Direction.Left:
                    if (playerSprite.Position.X < sprite.Position.X)
                    {
                        NewBullet(Direction.Left);
                    }
                    break;
                case Direction.Right:
                    if (playerSprite.Position.X > sprite.Position.X)
                    {
                        NewBullet(Direction.Right);
                    }
                    break;
            }

            _counter = 0;
        }

       

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        private void NewBullet(Direction direction)
        {
            var sprite = GetComponent<Sprite>(ComponentType.Sprite);
            _bullets.Add(
                new OctorokBullet(
                        new Sprite(_bulletTexture, 10, 10, sprite.Position), _player, direction));
            _counter = 0;
        }
    }
}
