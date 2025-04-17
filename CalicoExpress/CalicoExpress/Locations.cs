using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using CalicoExpress.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CalicoExpress.Locations
{
    
        public class Tile
        {
            public int id;
            public int x;
            public int y;
            public int tile;
            public bool passable;
            public Tile(int _x, int _y)
            {
                x = _x;
                y = _y;
                id = 0;
            }
            public Tile(int _tile)
            {
                id = _tile;
            }
            public Tile() { }
        }

        public class MapLayer
        {
            [XmlIgnore]
            public int layer;
        [XmlIgnore]
        public string name;
        [XmlIgnore]
        public int sizeX;
        [XmlIgnore]
        public int sizeY;
        [XmlIgnore]
        public Tile[] tiles;
        [XmlElement(ElementName = "data")]
        public string tileData;
        public MapLayer(int _sizeX, int _sizeY)
            {
                tiles = new Tile[_sizeX * _sizeY];
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i] = new Tile(i - (i/_sizeX)*_sizeX,(i/_sizeX));
                }
                sizeX = _sizeX;
                sizeY = _sizeY;
            }

            public MapLayer() { }

            public void ChangeTileData(int _x, int _y, Tile _tile)
            {
                int _id = _x + _y * sizeX;
                tiles[_id].id = _tile.id;
            }

            public void ChangeTileId(int _x, int _y, int _sizeX, int _mapX, int _mapY)
            {
                int _id = _mapX + _mapY * _sizeX;
                tiles[_x + _y * sizeX].id = _id;
            }
        }
        public class Map
        {
            [XmlArray]
            public string[] mapString = new string[5];
            int sizeX;
            int sizeY;
            [XmlIgnore]
            public MapLayer[] layers;
            Tileset[] tilesets;
            ContentManager contentManager;
            public Map(int _sizex, int _sizey,ContentManager _content)
            {
                layers = new MapLayer[5];
                layers[0] = new MapLayer(_sizex,_sizey);
                layers[1] = new MapLayer(_sizex, _sizey);
                layers[2] = new MapLayer(_sizex, _sizey);
                layers[3] = new MapLayer(_sizex, _sizey);
                layers[4] = new MapLayer(_sizex, _sizey);
                sizeX = _sizex;
                sizeY = _sizey;
                contentManager = _content;
            }
            protected Map()
            {

            }
            
            /*public void Load()
            {

                XmlSerializer serializer = new XmlSerializer(typeof(Map));
                Stream stream = new FileStream(@"C:\Users\PC\Desktop" + @"\i.xml", FileMode.Open);
                Map t = (Map) serializer.Deserialize(stream);
                stream.Close();
            }*/

            public void testSerialize()
            {
                
                XmlSerializer serializer = new XmlSerializer(typeof(Map));
                
                XmlWriter writer = XmlWriter.Create(@"C:\Users\PC\Desktop" + @"\untitled.tmx");
                serializer.Serialize(writer, this);
            }

        public void Serialize()
        {
            
            mapString = new string[5];
            for (int i = 0; i < mapString.Length; i++)
            {
                mapString[i] = LayerToString(layers[i]);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            XmlWriter writer = XmlWriter.Create(@"C:\Users\PC\Desktop" + @"\untitled.tmx");
            serializer.Serialize(writer, this);
            writer.Close();
        }

        protected string LayerToString(MapLayer _layer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _layer.tiles.Length; i++)
            {
                stringBuilder.Append(_layer.tiles[i].id.ToString() + ",");
            }

            return stringBuilder.ToString();
        }
        protected MapLayer StringToLayer(string _mapLayer)
        {
            MapLayer layer = new MapLayer(sizeX,sizeY);
            StringReader reader = new StringReader(_mapLayer);
            int j = 0;
            for (int i = 0; i < layer.tiles.Length; i++)
            {
                string num = "";
                while (j<_mapLayer.Length)
                {
                    char c = _mapLayer[j];
                    j++;
                    if (char.IsNumber(c))
                    {
                        num += c;
                    }
                    if(c == ',')
                    {
                        layer.tiles[i].id = Convert.ToInt32(num);
                        break;
                    }
                    
                }
            }
            return layer;
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Stream stream = File.Open(@"C:\Users\PC\Desktop" + @"\untitled.tmx", FileMode.Open);
           XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            using (XmlNodeList _strings = xmlDoc.GetElementsByTagName("data"))
            {
                for (int i = 0; i < mapString.Length; i++)
                {
                    mapString[i] = _strings.Item(i).InnerText;
                }
                for (int i = 0; i < layers.Length; i++)
                {
                    layers[i] = StringToLayer(mapString[i]);
                }
            }
            using (XmlNodeList _tilesets = xmlDoc.GetElementsByTagName("tileset"))
            {
                tilesets = new Tileset[_tilesets.Count];
                for (int i = 0; i < _tilesets.Count; i++)
                {
                    XmlAttributeCollection xmlAttribute = _tilesets.Item(i).FirstChild.Attributes;
                    tilesets[i] = new Tileset(i.ToString(),xmlAttribute.GetNamedItem("source").Value);
                    tilesets[i].Load(contentManager,xmlAttribute.GetNamedItem("source").Value,16,16);
                }
            }

                stream.Close();
            
        }
    }
    
}
