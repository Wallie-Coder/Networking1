using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using LiteNetLib;
using LiteNetLib.Layers;
using LiteNetLib.Utils;

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

            if(SorC.Server.pressed)
            {
                EventBasedNetListener listener = new EventBasedNetListener();
                NetManager server = new NetManager(listener);
                server.Start(9050 /* port */);

                SorC.PeerType = ServerOrClient.Peer.Server;
                SorC.Server.Name = "Server";
            }

            if (SorC.Client.pressed)
            {
                EventBasedNetListener listener = new EventBasedNetListener();
                NetManager client = new NetManager(listener);
                client.Start();
                client.Connect("192.169.100.254" /* host ip or name */, 9050 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);

                listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
                {
                    main.messages.Add("We got: {0}" + dataReader.GetString(100 /* max length of string */));
                    dataReader.Recycle();
                };

                client.Stop();



                SorC.PeerType = ServerOrClient.Peer.Client;
                SorC.Client.Name = "Client";
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