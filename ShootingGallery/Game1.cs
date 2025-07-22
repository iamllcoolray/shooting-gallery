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
        private Vector2 _targetCenter;
        private Vector2 _crosshairsPostion;

        private int _targetRadius;
        private bool _mReleased;

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
            _mReleased = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _sky = Content.Load<Texture2D>("sky");
            _target = Content.Load<Texture2D>("target");
            _crosshair = Content.Load<Texture2D>("crosshairs");

            _targetRadius = Math.Max(_target.Width, _target.Height) / 2;

            _targetPosition = new Vector2(_random.Next(_target.Width, _graphics.PreferredBackBufferWidth - _target.Width), _random.Next(_target.Height, _graphics.PreferredBackBufferHeight - _target.Height));
            _targetPosition += new Vector2(_target.Width / 2f, _target.Height / 2f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _mouseState = Mouse.GetState();

            _crosshairsPostion = new Vector2(_mouseState.X - (_crosshair.Width / 2), _mouseState.Y - (_crosshair.Height / 2));

            _targetCenter = _targetPosition + new Vector2(_target.Width / 2f, _target.Height / 2f);

            if (Vector2.Distance(_crosshairsPostion, _targetCenter) < _targetRadius)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _mReleased == true)
                {
                    _targetPosition = new Vector2(_random.Next(_target.Width, _graphics.PreferredBackBufferWidth - _target.Width), _random.Next(_target.Height, _graphics.PreferredBackBufferHeight - _target.Height));
                    _targetPosition += new Vector2(_target.Width / 2f, _target.Height / 2f);

                    _mReleased = false;
                }
            }

            if (_mouseState.LeftButton == ButtonState.Released)
            {
                _mReleased = true;
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
