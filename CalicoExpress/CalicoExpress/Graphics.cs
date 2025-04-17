using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalicoExpress.Graphics
{
    
        public class SpriteSheet
        { 
            public Texture2D texture;
            public int sizeX;
            public int sizeY;
            public string name;
            public SpriteSheet(int _sizeX, int _sizeY, string _name)
            {
                sizeX = _sizeX;
                sizeY = _sizeY;
                name = _name;
            }
            public void Load(ContentManager _contentManager, string _path,int _sizeX,int _sizeY)
            {
                texture = _contentManager.Load<Texture2D>(_path);
                sizeX = _sizeX;
                sizeY = _sizeY;
            }
        }

    public class Tileset
    {
        public Texture2D texture;
        public int sizeX;
        public int sizeY;
        public int tileSizeX;
        public int tileSizeY;
        public string name;
        public string path;
        public Tileset(string _name, string _path)
        {
            name = _name;
            this.path = _path;
        }
        public void Load(ContentManager _contentManager, string _path, int _tileSizeX, int _tileSizeY)
        {
            StringBuilder _stringBuilder = new StringBuilder(_path);
            _stringBuilder.Remove(_stringBuilder.Length - 4, 4);
            path = _stringBuilder.ToString();
            _stringBuilder.Clear();
            texture = _contentManager.Load<Texture2D>("Debug/"+path);
            sizeX = texture.Width / _tileSizeX;
            sizeY = texture.Height / _tileSizeY;
            tileSizeX = _tileSizeX;
            tileSizeY = _tileSizeY;
        }
        public int GetTileId(int x, int y)
        {
            return x + y * sizeX;
        }

        public Rectangle GetRectangleById(int _id)
        {
            int y = (_id / sizeX);
            int x = (_id - y * sizeX);

            return new Rectangle(x * tileSizeX, y * tileSizeY, tileSizeX, tileSizeY);
        }

        public Rectangle GetRectangleByTile(int x, int y)
        {
            return new Rectangle(x * tileSizeX, y * tileSizeY, tileSizeX, tileSizeY);
        }


    }
    
}
