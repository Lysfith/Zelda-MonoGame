using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Core.Data.Managers;
using Zelda.Utils;

namespace Zelda.Core.Data.Map
{
    public class Tile
    {
        public static int Width = 16;
        public static int Height = 16;

        public int XPos { get; set; }
        public int YPos { get; set; }
        public int ZPos { get; set; }

        public List<TileFrame> TileFrames { get; set; }
        public int AnimationSpeed { get; set; }

        public string TextureName { get; set; }
        public ManagerCamera ManagerCamera { get; set; }

        public Vector2 Position { get { return new Vector2(XPos * Width, YPos * Height); }  }

        protected Texture2D _texture;
       
        private double _counter;
        private int _animationIndex;

        public Tile()
        {

        }

        public Tile(int xPos, int yPos, int zPos, List<TileFrame> tileFrames, int animationSpeed, string textureName, ManagerCamera managerCamera)
        {
            XPos = xPos;
            YPos = yPos;
            ZPos = zPos;
            TextureName = textureName;
            TileFrames = tileFrames;
            AnimationSpeed = animationSpeed;
            _animationIndex = 0;
            ManagerCamera = managerCamera;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = TextureManager.Instance.GetTexture("Textures/" + TextureName);
        }

        public virtual void Update(double gameTime)
        {
            if (TileFrames.Count <= 1)
                return;

            _counter += gameTime;
            if(_counter > AnimationSpeed)
            {
                _counter = 0;
                _animationIndex++;
                if(_animationIndex >= TileFrames.Count)
                {
                    _animationIndex = 0;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var position = ManagerCamera.WorlToScreenPosition(Position);

            if (ManagerCamera.InScreenCheck(Position))
            {
                var tileFrame = TileFrames[_animationIndex];
                spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)position.Y, Width, Height),
                    new Rectangle(tileFrame.TextureXPos * (Width + 1) + 1, tileFrame.TextureYPos * (Height + 1) + 1, Width, Height), Color.White);
            }
        }

    }
}
