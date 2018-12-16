using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Core.Data.Map;
using Microsoft.Xna.Framework;

namespace Zelda.Core.Data.Managers
{
    class ManagerMap
    {
        private List<Tile> _tiles;
        private List<TileCollision> _tileCollisions;
        private string _mapName;
        private ManagerCamera _managerCamera;

        public ManagerMap(string mapName, ManagerCamera managerCamera)
        {
            _tiles = new List<Tile>();
            _tileCollisions = new List<TileCollision>();
            _mapName = mapName;
            _managerCamera = managerCamera;
        }

        public void LoadContent(ContentManager content)
        {
            var tiles = new List<Tile>();
            XMLSerialization.LoadXML(out tiles, $"Content\\Maps\\{_mapName}_map.xml");

            if(tiles != null)
            {
                _tiles = tiles;
                _tiles.Sort((n, i) => n.ZPos > i.ZPos ? 1 : 0);

                foreach(var tile in _tiles)
                {
                    tile.LoadContent(content);
                    tile.ManagerCamera = _managerCamera;
                }
            }

            var tileCollisions = new List<TileCollision>();
            XMLSerialization.LoadXML(out tileCollisions, $"Content\\Maps\\{_mapName}_map_collision.xml");
            if(tileCollisions != null)
            {
                _tileCollisions = tileCollisions;
                _tileCollisions.ForEach(t => t.ManagerCamera = _managerCamera);
            }
        }

        public bool CheckCollision(Rectangle rectangle)
        {
            return _tileCollisions.Any(tile => tile.Intersect(rectangle));
        }

        public void Update(double gameTime)
        {
            foreach (var tile in _tiles)
            {
                tile.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
