using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components
{
    class Sprite : Component
    {
        private Texture2D _texture2D;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Position { get; private set; }

        public Sprite(Texture2D texture2D, int width, int height, Vector2 position)
        {
            _texture2D = texture2D;
            Width = width;
            Height = height;
            Position = position;
        }

        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.Sprite;
            }
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var camera = GetComponent<Camera>(ComponentType.Camera);
            Vector2 position;
            if(!(camera != null && camera.GetPosition(Position, out position)))
            {
                position = Position;
            }

            var animation = GetComponent<Animation>(ComponentType.Animation);

            if(animation != null)
            {
                spriteBatch.Draw(_texture2D, new Rectangle((int)position.X, (int)position.Y, Width, Height),
                    animation.TextureRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture2D, new Rectangle((int)position.X, (int)position.Y, Width, Height), Color.White);
            }
        }

        public void Move(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);

            var animation = GetComponent<Animation>(ComponentType.Animation);

            if (animation == null)
            {
                return;
            }

            if (x > 0)
            {
                animation.ResetCounter(State.Walking, Direction.Right);
            }
            else if (x < 0)
            {
                animation.ResetCounter(State.Walking, Direction.Left);
            }
            else if(y > 0)
            {
                animation.ResetCounter(State.Walking, Direction.Down);
            }
            else if (y < 0)
            {
                animation.ResetCounter(State.Walking, Direction.Up);
            }
        }
    }
}
