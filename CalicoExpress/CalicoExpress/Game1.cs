using CalicoExpress.Graphics;
using CalicoExpress.Locations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CalicoExpress
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteSheet[] _spriteSheets = {new SpriteSheet(1,1,"test") };
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.DisplayMode.Height;
            _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.DisplayMode.Width;
            _graphics.ApplyChanges();
            //
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadSpriteSheets();
            Map map = new Map(30,20,Content);
            
            map.Load();
           // map.Load();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteSheets[0].texture, new Rectangle(10, 10, 1024, 1024), new Rectangle(300, 300, 500, 160), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        //--------------------------
        //          Setup           
        //--------------------------

        protected void LoadSpriteSheets()
        {
            foreach (var spriteSheet in _spriteSheets)
            {
                spriteSheet.Load(Content, "Debug/Backdrop002", 1,1);
            }
        }

        //-------------------------------
        //          Data Types           
        //-------------------------------
        

    }
}
