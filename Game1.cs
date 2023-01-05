using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;

namespace Networking1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager _content;

        internal Main main;
        internal ServerOrClient SorC;

        internal bool connected = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _content = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.ApplyChanges();

            main = new Main();
            SorC = new ServerOrClient();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Input.Update(gameTime);

            main.Update(gameTime);
            SorC.Update(gameTime, connected);

            if (SorC.Connect.activated == true)
                if (SorC.Server.activated == true)
                {
                    var config = new NetPeerConfiguration("application name")
                    { Port = 12345 };
                    var server = new NetServer(config);
                    server.Start();

                    SorC.Connect.activated = false;
                }
                else
                {
                    var config = new NetPeerConfiguration("application name");
                    var client = new NetClient(config);
                    client.Start();
                    client.Connect(host: "127.0.0.1", port: 12345);

                    SorC.Connect.activated = false;

                    connected = true;
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            main.Draw(_spriteBatch);
            SorC.Draw(_spriteBatch);
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}