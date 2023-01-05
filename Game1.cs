using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using Lidgren.Network;

namespace Networking1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager _content;

        internal Main main;
        internal ServerOrClient SorC;

        internal int portNr = 8888;

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
            SorC.Update(gameTime);


            if (SorC.Server.pressed == true)
            {
                var config = new NetPeerConfiguration("application name")
                { Port = portNr };
                var server = new NetServer(config);
                server.Start();

                main.messages.Add("started Server: " + portNr);

                SorC.Server.pressed = false;
                SorC.Server.Name = "Server";
                SorC.PeerType = ServerOrClient.Peer.Server;
            }







            if (SorC.Client.pressed == true && SorC.PeerType == ServerOrClient.Peer.None)
            {
                var config = new NetPeerConfiguration("application name");
                var client = new NetClient(config);
                client.Start();
                client.Connect(host: "127.0.0.1", port: portNr);

                main.messages.Add("connectd to host: " + portNr);

                var message = client.CreateMessage();
                message.Write("New Client Connected");

                main.messages.Add(client.SendMessage(message, NetDeliveryMethod.ReliableOrdered).ToString());

                SorC.Client.pressed = false;
                SorC.Client.Name = "Client";
                SorC.PeerType = ServerOrClient.Peer.Client;
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