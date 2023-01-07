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

        internal Menu Menu;

        internal static string IP = "192.168.100.117";
        internal static int portNr = 13055;

        internal static NetPeerConfiguration config = new NetPeerConfiguration("chat") { Port = portNr};

        internal static Peer activePeer;

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

            Menu = new Menu();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Input.Update(gameTime);

            ChatFunction.Update(gameTime);

            if (activePeer == null)
            {
                Menu.Update(gameTime);
            }

            if (Menu.Server.pressed)
            {
                Menu.Server.pressed = false;
                Menu.Server.Name = "Server";
                activePeer = new Server();
                activePeer.Initialize();
            }
            if (Menu.Client.pressed)
            {
                Menu.Client.pressed = false;
                Menu.Client.Name = "Client";
                activePeer = new Client();
                activePeer.Initialize();
            }


            if (activePeer != null)
                activePeer.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            Menu.Draw(_spriteBatch);
            ChatFunction.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}