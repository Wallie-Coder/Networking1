﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using System;
using System.Collections.Generic;
using System.Threading;
using Lidgren.Network;
using System.Net;

namespace Networking1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager _content;

        internal Menu Menu;

        internal static string IP = "192.168.100.117";
        internal static int portNr = 12345;

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

            IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();
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
            {
                activePeer.thisPeer.Shutdown("bye");
                Exit();
            }

            // TODO: Add your update logic here

            Input.Update(gameTime);

            ChatFunction.Update(gameTime);

            if (activePeer == null)
            {
                Menu.Update(gameTime);
            }

            if (Menu.Server.pressed)
            {
                config = new NetPeerConfiguration("chat") { Port = portNr };
                Menu.Server.pressed = false;
                Menu.Server.Name = "Server";
                activePeer = new Server();
                activePeer.Initialize();
            }
            if (Menu.Client.pressed)
            {
                config = new NetPeerConfiguration("chat") { Port = portNr };
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

            _spriteBatch.DrawString(Input.getFont("file"),"portNr: " + portNr.ToString() + "  (1024-49151)", new Vector2(30, 30), Color.Black);
            _spriteBatch.DrawString(Input.getFont("file"), "IP Adress: " + IP, new Vector2(30, 60), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}