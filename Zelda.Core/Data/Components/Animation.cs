using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Components
{
    class Animation : Component
    {
        public State CurrentState { get; set; }
        public Direction CurrentDirection { get; set; }
        private double _counter;
        private int _animationIndex;
        private int _width;
        private int _height;

        public override ComponentType ComponentType
        {
            get
            {
                return ComponentType.Animation;
            }
        }

        public Rectangle TextureRectangle { get; private set; }

        public Animation(int width, int height)
        {
            _width = width;
            _height = height;
            _counter = 0;
            _animationIndex = 0;
            CurrentState = State.Standing;
            TextureRectangle = new Rectangle(_width * _animationIndex, 0, _width, _height);
        }

        public override void Update(double gameTime)
        {
            switch (CurrentState)
            {
                case State.Standing:
                   
                    break;
                case State.Walking:
                    _counter += gameTime;

                    if (_counter > 200)
                    {
                        ChangeState();
                        _counter = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void ResetCounter(State state, Direction direction)
        {
            if(CurrentDirection != direction)
            {
                _counter = 1000;
                _animationIndex = 0;
            }

            CurrentState = state;
            CurrentDirection = direction;
        }

        private void ChangeState()
        {
            switch (CurrentDirection)
            {
                case Direction.Down:
                    TextureRectangle = new Rectangle(_width * _animationIndex, 0, _width, _height);
                    break;
                case Direction.Up:
                    TextureRectangle = new Rectangle(_width * _animationIndex, _height, _width, _height);
                    break;
                case Direction.Left:
                    TextureRectangle = new Rectangle(_width * _animationIndex, _height * 2, _width, _height);
                    break;
                case Direction.Right:
                    TextureRectangle = new Rectangle(_width * _animationIndex, _height * 3, _width, _height);
                    break;
            }

            _animationIndex = _animationIndex == 0 ? 1 : 0;
            CurrentState = State.Standing;
        }
    }
}
