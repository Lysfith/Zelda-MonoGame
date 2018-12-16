using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zelda.Core.Data;
using Zelda.Core.Data.Components;
using Zelda.Core.Data.Components.Enemies;
using Zelda.Core.Data.Components.Movement;
using Zelda.Core.Data.Managers;
using Zelda.Utils;

namespace Zelda.Core
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private BaseObject _player;
        private BaseObject _pnj1;
        private BaseObject _enemy1;
        private ManagerInput _managerInput;
        private ManagerMap _managerMap;
        private ManagerCamera _managerCamera;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1366;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            _player = new BaseObject();
            _pnj1 = new BaseObject();
            _enemy1 = new BaseObject();
            _managerInput = new ManagerInput();
            _managerCamera = new ManagerCamera();
            _managerMap = new ManagerMap("test", _managerCamera);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            FontManager.Instance.SetContentManager(Content);
            TextureManager.Instance.SetContentManager(Content);

            var textureBlank = new Texture2D(_graphics.GraphicsDevice, 1, 1);

            Color[] tcolor = new Color[1];
            textureBlank.GetData<Color>(tcolor);
            tcolor[0] = new Color(255, 255, 255, 255);
            textureBlank.SetData<Color>(tcolor);

            TextureManager.Instance.AddTexture("Blank", textureBlank);

            _player.AddComponent(new Sprite(TextureManager.Instance.GetTexture("Textures/link_full"), 16, 16, new Vector2(48, 48)));
            _player.AddComponent(new PlayerInput());
            _player.AddComponent(new Animation(16, 16));
            _player.AddComponent(new Collision(_managerMap));
            _player.AddComponent(new Camera(_managerCamera));

            _pnj1.AddComponent(new Sprite(TextureManager.Instance.GetTexture("Textures/Marin"), 16, 16, new Vector2(48, 48)));
            _pnj1.AddComponent(new AIMovementRandom(200));
            _pnj1.AddComponent(new Animation(16, 16));
            _pnj1.AddComponent(new Collision(_managerMap));
            _pnj1.AddComponent(new Camera(_managerCamera));


            _enemy1.AddComponent(new Sprite(TextureManager.Instance.GetTexture("Textures/Octorok"), 16, 16, new Vector2(48, 48)));
            _enemy1.AddComponent(new AIMovementRandom(200));
            _enemy1.AddComponent(new Animation(16, 16));
            _enemy1.AddComponent(new Collision(_managerMap));
            _enemy1.AddComponent(new Octorok(_player, TextureManager.Instance.GetTexture("Textures/Octorok_bullet")));
            _enemy1.AddComponent(new Camera(_managerCamera));


            _managerMap.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _managerInput.Update(gameTime.ElapsedGameTime.Milliseconds);
            _managerMap.Update(gameTime.ElapsedGameTime.Milliseconds);
            _managerCamera.Update(gameTime.ElapsedGameTime.Milliseconds);
            _player.Update(gameTime.ElapsedGameTime.Milliseconds);
            _pnj1.Update(gameTime.ElapsedGameTime.Milliseconds);
            _enemy1.Update(gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(196, 207, 161));

            _spriteBatch.Begin();

            _managerMap.Draw(_spriteBatch);
            
            _pnj1.Draw(_spriteBatch);
            _enemy1.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
