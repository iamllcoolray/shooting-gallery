using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _sky;
        Texture2D _target;
        Texture2D _crosshair;

        private Random _random;

        private MouseState _mouseState;

        private Vector2 _targetPosition;
        private Vector2 _crosshairsPostion;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _sky = Content.Load<Texture2D>("sky");
            _target = Content.Load<Texture2D>("target");
            _crosshair = Content.Load<Texture2D>("crosshairs");

            _targetPosition = new Vector2(_random.NextInt64(0, _graphics.PreferredBackBufferWidth) - (_target.Width / 2), _random.NextInt64(0, _graphics.PreferredBackBufferHeight) - (_target.Height / 2));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _mouseState = Mouse.GetState();

            _crosshairsPostion = new Vector2(_mouseState.X - (_crosshair.Width / 2), _mouseState.Y - (_crosshair.Height / 2));

            if (Vector2.Distance(_crosshairsPostion, _targetPosition) < (_target.Width / 2f) || Vector2.Distance(_crosshairsPostion, _targetPosition) < (_target.Height / 2f))
            {
                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    _targetPosition = new Vector2(_random.NextInt64(0, _graphics.PreferredBackBufferWidth) - (_target.Width / 2), _random.NextInt64(0 , _graphics.PreferredBackBufferHeight) - (_target.Height / 2));

                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(_sky, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(_target, _targetPosition, Color.White);
            _spriteBatch.Draw(_crosshair, _crosshairsPostion, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
