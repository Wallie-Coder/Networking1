using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using System;
using System.Collections.Generic;
using System.Threading;
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

        internal int messagessent = 0;

        internal string IP = "192.168.100.117";
        internal int portNr = 13055;

        internal NetClient client;
        internal static NetServer server;

        internal NetPeerConfiguration config = new NetPeerConfiguration("chat") { Port = 13055};

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
                server = new NetServer(config);
                server.Start();

                main.messages.Add("started Server: " + portNr);

                SorC.Server.pressed = false;
                SorC.Server.Name = "Server";
                SorC.PeerType = ServerOrClient.Peer.Server;
            }


            if (SorC.Client.pressed == true && SorC.PeerType == ServerOrClient.Peer.None)
            {
                client = new NetClient(config);
                client.Start();
                client.Connect(host: IP, port: portNr, client.CreateMessage("hello it works"));

                main.messages.Add("connectd to host: " + portNr);

                SorC.Client.pressed = false;
                SorC.Client.Name = "Client";
                SorC.PeerType = ServerOrClient.Peer.Client;
            }


            if(SorC.PeerType == ServerOrClient.Peer.Server)
            {
                ReceiveMessages(server);
            }
            if(SorC.PeerType == ServerOrClient.Peer.Client)
            {
                ReceiveMessages(client);
            }

            if(messagessent != main.messages.Count)
            {
                if (SorC.PeerType == ServerOrClient.Peer.Client)
                {
                    NetOutgoingMessage message = client.CreateMessage(main.messages[main.messages.Count - 1]);
                    client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
                    messagessent = main.messages.Count;
                }
                if (SorC.PeerType == ServerOrClient.Peer.Server)
                {
                    NetOutgoingMessage message = server.CreateMessage(main.messages[main.messages.Count - 1]);
                    server.SendToAll(message, NetDeliveryMethod.ReliableOrdered);
                }
            }

            base.Update(gameTime);
        }

        public void ReceiveMessages(NetPeer peer)
        {
            NetIncomingMessage im;
            while ((im = peer.ReadMessage()) != null)
            {
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        main.messages.Add(im.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (im.SenderConnection.Status)
                        {

                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        main.messages.Add(im.ReadString());
                        break;

                    /* .. */
                    default:
                        Console.WriteLine("unhandled message with type: "
                            + im.MessageType);
                        break;
                }
            }
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